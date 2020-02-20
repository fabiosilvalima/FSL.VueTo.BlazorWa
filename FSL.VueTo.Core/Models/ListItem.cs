using System.Collections.Generic;

namespace FSL.VueTo.Core.Models
{
    public sealed class ListItem
    {
        public ListItem()
        {
            Items = new List<Item>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public List<Item> Items { get; set; }
    }
}
