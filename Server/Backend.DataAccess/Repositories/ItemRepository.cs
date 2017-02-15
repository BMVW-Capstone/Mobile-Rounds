using Backend.DataAccess.Abstractions;
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
        public override async Task<IEnumerable<ItemModel>> GetAsync()
        {
            return await DataSource
                //Get the records in order
                .GetOrdered()
                //convert records to view models 
                .Select(r => new ItemModel
                {
                    Id = r.ItemId,
                    Name = r.Name,
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
                    }
                })
                //load the data
                .ToListAsync();
        }

        /// <summary>
        /// Returns a list of items for the given station.
        /// </summary>
        /// <param name="stationId">The station to fetch items for.</param>
        /// <returns>A list of items for the station.</returns>
        public async Task<IEnumerable<ItemModel>> GetForStationAsync(Guid stationId)
        {
            var items = await this.GetAsync();
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
            return new ItemModel
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
                }
            };
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
                StationId = model.StationId
            };
        }
    }
}
