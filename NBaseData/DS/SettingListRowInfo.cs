using NBaseData.DAC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NBaseData.DS
{
    [DataContract()]
    public class SettingListRowInfo
    {
        public Color normalColor { get; set; }

        public Color selectedColor { get; set; }





        [DataMember]
        public MsSenin senin { get; set; }

        [DataMember]
        public MsSeninAddress address { get; set; }

        [DataMember]
        public MsSeninCareer career { get; set; }

        [DataMember]
        public MsSeninEtc etc { get; set; }






        [DataMember]
        public SiCard card { get; set; }




        [DataMember]
        public SiMenjou menjouK { get; set; }

        [DataMember]
        public SiMenjou menjouS { get; set; }

        [DataMember]
        public SiMenjou menjouM { get; set; }




        [DataMember]
        public SiKazoku emg1_kazoku { get; set; }

        [DataMember]
        public SiKazoku emg2_kazoku { get; set; }

        [DataMember]
        public SiKazoku kazoku { get; set; }


        [DataMember]
        public SiKenshin kenshin { get; set; }

        [DataMember]
        public SiShobatsu shobatsu { get; set; }

        [DataMember]
        public SiRemarks remarks { get; set; }

        [DataMember]
        public SiSalary salary { get; set; }







        [DataMember]
        public OdThi thi { get; set; }
    }
}
