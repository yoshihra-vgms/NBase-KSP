using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SqlMapper
{
    public class DatabaseConfigManager
    {
        public static readonly DatabaseConfigManager INSTANCE = new DatabaseConfigManager();

        public string Provider
        {
            get
            {
                return ORMapping.Common.DBTYPE.ToString();
            }
        }
        public string ConnectionString { get; private set; }


        private DatabaseConfigManager()
        {
            Configure();
        }
        
        private void Configure()
        {
        }

        public static DatabaseConfigManager instance()
        {
            return INSTANCE;
        }
    }
}
