using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class ItemDataSource : AbstractDataSource<Item>
    {
        public ItemDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Item> Get()
        {
            return Database.Items
                .Where(r => r.IsMarkedAsDeleted == false);
        }

        public override IOrderedQueryable<Item> GetOrdered()
        {
            return Database.Items
                .Where(r => r.IsMarkedAsDeleted == false)
                .OrderBy(r => r.Name);
        }

        public override Item GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }


        public override async Task<Item> InsertAsync(Item toCreate)
        {
            toCreate.ItemId = Guid.NewGuid();

            var tracked = Database.Items.Add(toCreate);
            if(await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<Item> UpdateAsync(Item toUpdate)
        {
            var tracked = Database.Items.Find(toUpdate.ItemId);

            if (tracked == null) return null;

            tracked.Name = toUpdate.Name;
            tracked.IsMarkedAsDeleted = toUpdate.IsMarkedAsDeleted;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}
