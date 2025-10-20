using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseHonsen.util
{
    class ItemHeader<T>
    {
        public int Id { get; private set; }
        
        public string Header { get; set; }
        public int SendFlag { get; set; }
        
        private List<T> items;
        public List<T> Items
        {
            get
            {
                if (items == null)
                {
                    items = new List<T>();
                }

                return items;
            }
        }
    
        
        public ItemHeader(int id)
        {
            this.Id = id;
        }
    }
}
