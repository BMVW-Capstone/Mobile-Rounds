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
    /// Represents a way to interface with Regions in the database.
    /// </summary>
    public sealed class SpecificationRepository 
        : AbstractRepository<SpecificationModel, Specification>
    {
        /// <summary>
        /// Creates a new instance for working with database based regions.
        /// </summary>
        /// <param name="database">The database to use.</param>
        public SpecificationRepository(DatabaseContext database)
            : base (new SpecificationDataSource(database))
        {
        }

        /// <inheritdoc />
        public override async Task<SpecificationModel> InsertAsync(
            SpecificationModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<SpecificationModel> UpdateAsync(
            SpecificationModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override SpecificationModel BuildViewModel(Specification model)
        {
            if (model == null) return null;
            return new SpecificationModel
            {
                Id = model.ItemId,
                ComparisonType = model.ComparisionTypeName,
                LowerBound = model.LowerBoundValue,
                UpperBound = model.UpperBoundValue,
                UnitOfMeasureId = model.UnitId,
                IsDeleted = model.IsMarkedAsDeleted
            };
        }

        protected override Specification BuildModel(SpecificationModel model)
        {
            if (model == null) return null;
            return new Specification
            {
                ItemId = model.Id,
                UnitId = model.UnitOfMeasureId,
                ComparisionTypeName = model.ComparisonType,
                LowerBoundValue = model.LowerBound,
                UpperBoundValue = model.UpperBound,
                IsMarkedAsDeleted = model.IsDeleted
            };
        }
    }
}
