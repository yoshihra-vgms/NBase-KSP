using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseData.DAC.DJ
{
    [DataContract()]
    [TableAttribute("TKJNAIPLAN_AMT_BILL")]
    public class 内航海運輸送実績調査票Data
    {

        #region データメンバ
        /// <summary>
        /// 貨物NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("CAG_NO")]
        public string CargoNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("QTY")]
        //public decimal Qtty { get; set; }
        public double Qtty { get; set; }

        [DataMember]
        [ColumnAttribute("PROFITS_KBN")]
        public string ProfitKbn { get; set; }

        [DataMember]
        [ColumnAttribute("FUNE_NO")]
        public string FuneNo { get; set; }

        [DataMember]
        [ColumnAttribute("PORT_NO")]
        public string PortNo { get; set; }






        /// <summary>
        /// 貨物名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NAME")]
        public string CargoName { get; set; }

        /// <summary>
        /// 主な荷主
        /// </summary>
        [DataMember]
        [ColumnAttribute("NINUSHI")]
        public string Ninushi { get; set; }


        /// <summary>
        /// 輸送品目コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUSO_ITEM_CODE")]
        public string YusoItemCode { get; set; }


        /// <summary>
        /// 輸送品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUSO_ITEM_NAME")]
        public string YusoItemName { get; set; }


        /// <summary>
        /// 船種コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENSHU_CODE")]
        public string SenshuCode { get; set; }


        /// <summary>
        /// 船種名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENSHU_NAME")]
        public string SenshuName { get; set; }






        #endregion

        public static 内航海運輸送実績調査票Data Clone(内航海運輸送実績調査票Data src)
        {
            内航海運輸送実績調査票Data ret = new 内航海運輸送実績調査票Data();

            ret.CargoNo = src.CargoNo;
            ret.Qtty = src.Qtty;
            ret.ProfitKbn = src.ProfitKbn;
            ret.FuneNo = src.FuneNo;
            ret.PortNo = src.PortNo;
            ret.CargoName = src.CargoName;
            ret.Ninushi = src.Ninushi;
            ret.YusoItemCode = src.YusoItemCode;
            ret.YusoItemName = src.YusoItemName;
            ret.SenshuCode = src.SenshuCode;
            ret.SenshuName = src.SenshuName;

            return ret;
        }

        public static List<内航海運輸送実績調査票Data> 集計(MsUser loginUser, string JikoNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(内航海運輸送実績調査票Data), MethodBase.GetCurrentMethod());

            List<内航海運輸送実績調査票Data> ret = new List<内航海運輸送実績調査票Data>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("JIKO_NO", JikoNo));

            MappingBase<内航海運輸送実績調査票Data> mapping = new MappingBase<内航海運輸送実績調査票Data>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<内航海運輸送実績調査票Data> 荷主取得(MsUser loginUser, string JikoNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(内航海運輸送実績調査票Data), MethodBase.GetCurrentMethod());

            List<内航海運輸送実績調査票Data> ret = new List<内航海運輸送実績調査票Data>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("JIKO_NO", JikoNo));

            MappingBase<内航海運輸送実績調査票Data> mapping = new MappingBase<内航海運輸送実績調査票Data>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<内航海運輸送実績調査票Data> GetRecords(MsUser loginUser, string JikoNo, List<MsVessel> targetVessels, List<MsCargoYusoItem> targetYusoItems, List<MsCargo> cargos, List<string> targetBashoNos)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(内航海運輸送実績調査票Data), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("JIKO_NO", JikoNo));

            MappingBase<内航海運輸送実績調査票Data> mapping = new MappingBase<内航海運輸送実績調査票Data>();
            List<内航海運輸送実績調査票Data> all = new List<内航海運輸送実績調査票Data>();
            all = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            var funeNos = targetVessels.Select(obj => obj.VesselNo);
            var tmp1 = all.Where(obj => funeNos.Contains(obj.FuneNo));


            var tmp2 = tmp1.Where(obj => targetBashoNos.Contains(obj.PortNo));


            List<内航海運輸送実績調査票Data> ret = new List<内航海運輸送実績調査票Data>();

            foreach(内航海運輸送実績調査票Data data in tmp2)
            {
                var tmp3 = cargos.Where(obj => obj.CargoNo == data.CargoNo);
                if (tmp3.Count() > 0)
                {
                    var tmp4 = targetYusoItems.Where(obj => obj.MsCargoID == (tmp3.First()).MsCargoID);



                    data.YusoItemCode = (tmp4.First()).YusoItemCode;
                    data.YusoItemName = (tmp4.First()).YusoItemName;
                    data.SenshuCode = (tmp4.First()).SenshuCode;
                    data.SenshuName = (tmp4.First()).SenshuName;

                    data.CargoName = (tmp3.First()).CargoName;
                    data.Ninushi = (tmp3.First()).Ninushi;

                    ret.Add(data);
                }
            }

            return ret;
        }
    }
}
