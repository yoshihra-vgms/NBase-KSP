using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CIsl.DB.WingDAC;
using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
namespace DcCommon.Output
{

    public enum EOutputReportKind
    {
        コメントリスト,
        改善報告書,

        MAX,
    }


    /// <summary>
    /// 検船パラメータ
    /// </summary>
    public class MoiReportOutputParameter
    {
        public EOutputReportKind Kind = EOutputReportKind.MAX;

        public int moi_observation_id = DcMoiObservation.EVal;

        

        /// <summary>
        /// 宛先 会社
        /// </summary>
        public string DistCompany = "";

        /// <summary>
        /// 宛先 部署
        /// </summary>
        public string DestGroup = "";


        /// <summary>
        /// 作成者
        /// </summary>
        public MsUser CreateUser = null;

        /// <summary>
        /// 検船詳細
        /// </summary>
        public string InspectionDetail = "";

    }

    public class MoiReportOutputData
    {
        
    }
}
