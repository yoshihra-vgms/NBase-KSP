using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NBaseData.DS
{
    [DataContract()]
    public class OdStatus
    {
        [DataMember]
        public int DefaultValue = 0;
        [DataMember]
        public List<StatusValue> Values = null;

        [DataMember]
        public int MaxValue
        {
            get
            {
                int maxValue = 0;
                try
                {
                    StatusValue value = Values[Values.Count - 1];
                    maxValue = value.Value;
                }
                catch
                {
                }
                return maxValue;
            }
            set
            {
            }
        }
        public string GetName(int value)
        {
            string name = "";
            foreach (StatusValue sv in Values)
            {
                if (sv.Value == value)
                {
                    name = sv.Name;
                    break;
                }
            }
            return name;
        }

        [DataContract()]
        public class StatusValue
        {
            [DataMember]
            public int Value { get; set; }
            [DataMember]
            public string Name { get; set; }
        }
    }
}
