using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.BasicLogging.Models
{
    public class MockDatastore
    {
        List<ItemEntry> Items;
        public MockDatastore()
        {
            Items = new List<ItemEntry>() {
                new ItemEntry() { Id = 1, Name = "Apple", Price = 0.25m, Quantity = 250 },
                new ItemEntry() { Id = 2, Name = "Banana (bunch)", Price = 1.15m, Quantity = 96 },
                new ItemEntry() { Id = 3, Name = "Carrot (bag)", Price = 0.25m, Quantity = 250 },
                new ItemEntry() { Id = 4, Name = "Dragonfruit", Price = 2.25m, Quantity = 40 }
            };
        }

        public ItemEntry Add(ItemEntry newEntry)
        {
            newEntry.Id = Items.Max(i => i.Id) + 1;
            Items.Add(newEntry);

            return newEntry;
        }

        public ItemEntry Get(int itemId)
        {
            return Items.FirstOrDefault(i => i.Id == itemId);
        }

        public IEnumerable<ItemEntry> GetAll()
        {
            return Items;
        }

        public bool Update(ItemEntry updatedEntry)
        {
            if (!Delete(updatedEntry.Id))
                return false;

            Add(updatedEntry);
            return true;
        }

        public bool Delete(int itemId)
        {
            var found = Get(itemId);

            if (found == null)
                return false;

            Items.Remove(found);
            return true;
        }
    }
}
