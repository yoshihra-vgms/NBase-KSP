using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NBaseData.BLC;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class MsSeninPlus
    {
        [DataMember]
        public MsSenin Senin { get; set; }

        [DataMember]
        public MsSeninAddress Address { get; set; }

        [DataMember]
        public MsSeninCareer Career { get; set; }

        [DataMember]
        public MsSeninEtc Etc { get; set; }

        [DataMember]
        public SiCard Card { get; set; }

        [DataMember]
        public SiMenjou Menjou_K { get; set; }

        [DataMember]
        public SiMenjou Menjou_S { get; set; }

        [DataMember]
        public SiMenjou Menjou_M { get; set; }


        [DataMember]
        public SiKazoku KazokuEmg1 { get; set; }

        [DataMember]
        public SiKazoku KazokuEmg2 { get; set; }

        [DataMember]
        public SiKazoku Kazoku { get; set; }








        [DataMember]
        public SiKenshin Kenshin { get; set; }



        [DataMember]
        public SiShobyo Shobyo { get; set; }

        [DataMember]
        public SiKoushu Koushu { get; set; }

        [DataMember]
        public List<CrewMatrix> CrewMatrixList { get; set; }

        [DataMember]
        public List<SiExperienceCargo> experienceCargoList { get; set; }

    }
}
