using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBaseData.DAC
{
    public class MsPlanType
    {
        public static int PlanTypeOneMonth = 0;
        public static int PlanTypeHarfPeriod = 1;



        public int Type
        {
            set; get;
        }

        public string Name
        {
            set; get;
        }


        public MsPlanType(int type, string name)
        {
            Type = type;
            Name = name;
        }

        public static List<MsPlanType> GetRecords()
        {
            List<MsPlanType> ret = new List<MsPlanType>();
            ret.Add(new MsPlanType(0, "短期"));
            ret.Add(new MsPlanType(1, "長期"));

            return ret;
        }
    }
}
