﻿using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories.DataSources;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories
{
    /// <summary>
    /// Represents a way to interface with Items in the database.
    /// </summary>
    public sealed class ItemRepository 
        : AbstractRepository<ItemModel, Item>
    {
        /// <summary>
        /// Creates a new instance for working with database based Items.
        /// </summary>
        /// <param name="database">The database to use.</param>
        public ItemRepository(DatabaseContext database)
            : base (new ItemDataSource(database))
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<ItemModel>> GetAsync(bool includeDeleted)
        {
            return await DataSource
                //Get the records in order
                .GetOrdered(includeDeleted)
                //convert records to view models 
                .Select(r => new ItemModel
                {
                    Id = r.ItemId,
                    Name = r.Name,
                    Meter = r.Meter,
                    IsDeleted = r.IsMarkedAsDeleted,
                    StationId = r.StationId,
                    Specification = new SpecificationModel
                    {
                        Id = r.Specification.ItemId,
                        IsDeleted = r.Specification.IsMarkedAsDeleted,
                        ComparisonType = r.Specification.ComparisonTypeName,
                        LowerBound = r.Specification.LowerBoundValue,
                        UpperBound = r.Specification.UpperBoundValue,
                        UnitOfMeasure = new UnitOfMeasureModel
                        {
                            Id = r.Specification.UnitId,
                            IsDeleted = r.Specification.Unit.IsMarkedAsDeleted,
                            Abbreviation = r.Specification.Unit.Abbreviation,
                            Name = r.Specification.Unit.Name
                        }
                    },
                    PastFourReadings = r.Readings
                        .Where(i => includeDeleted || i.IsMarkedAsDeleted)
                        .OrderBy(i => i.TimeTaken)
                        .Select(i => new ReadingModel
                        {
                            Id = i.Id,
                            IsOutOfSpec = i.IsOutOfSpec,
                            Value = i.Value,
                            Comments = i.Comments
                        })
                        .Take(4)
                })
                //load the data
                .ToListAsync();
        }

        /// <summary>
        /// Returns a list of items for the given station.
        /// </summary>
        /// <param name="stationId">The station to fetch items for.</param>
        /// <returns>A list of items for the station.</returns>
        public async Task<IEnumerable<ItemModel>> GetForStationAsync(Guid stationId, bool includeDeleted)
        {
            var items = await this.GetAsync(includeDeleted);
            return items.Where(i => i.StationId == stationId)
                // This final ToList is required for serialization purposes in JSON.
                .ToList();
        }

        /// <inheritdoc />
        public override async Task<ItemModel> InsertAsync(ItemModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<ItemModel> UpdateAsync(ItemModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override ItemModel BuildViewModel(Item model)
        {
            if (model == null) return null;
            var item = new ItemModel
            {
                Id = model.ItemId,
                Name = model.Name,
                IsDeleted = model.IsMarkedAsDeleted,
                Meter = model.Meter,
                StationId = model.StationId,
                Specification = new SpecificationModel
                {
                    Id = model.Specification.ItemId,
                    ComparisonType = model.Specification.ComparisonTypeName,
                    IsDeleted = model.Specification.IsMarkedAsDeleted,
                    LowerBound = model.Specification.LowerBoundValue,
                    UpperBound = model.Specification.UpperBoundValue,
                    UnitOfMeasure = new UnitOfMeasureModel
                    {
                        Id = model.Specification.UnitId,
                        Name = model.Specification.Unit.Name,
                        Abbreviation = model.Specification.Unit.Abbreviation,
                        IsDeleted = model.Specification.Unit.IsMarkedAsDeleted
                    }
                },
                PastFourReadings = new List<ReadingModel>()
            };

            if(model.Readings != null)
            {
                item.PastFourReadings = model.Readings?
                    .OrderBy(i => i.TimeTaken)
                    .Select(i => new ReadingModel
                    {
                        Id = i.Id,
                        IsOutOfSpec = i.IsOutOfSpec,
                        Value = i.Value,
                        Comments = i.Comments
                    })
                    .Take(4);
            }

            return item;
        }

        protected override Item BuildModel(ItemModel model)
        {
            if (model == null) return null;
            return new Item
            {
                ItemId = model.Id,
                Name = model.Name,
                IsMarkedAsDeleted = model.IsDeleted,
                Meter = model.Meter,
                StationId = model.StationId,
                Specification = new Specification
                {
                    ItemId = model.Id,
                    IsMarkedAsDeleted = model.IsDeleted,
                    ComparisonTypeName = model.Specification.ComparisonType,
                    LowerBoundValue = model.Specification.LowerBound,
                    UpperBoundValue = model.Specification.UpperBound,
                    UnitId = model.Specification.UnitOfMeasure.Id
                }
            };
        }
    }
}
