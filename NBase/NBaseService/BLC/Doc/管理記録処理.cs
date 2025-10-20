using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_管理記録処理_登録(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile, List<DmPublisher> publishers);
        
        [OperationContract]
        bool BLC_管理記録処理_更新(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile);
        
        //[OperationContract]
        //bool BLC_管理記録処理_削除(MsUser loginUser, DmKanriKiroku kanriKiroku);
    }

    public partial class Service
    {
        public bool BLC_管理記録処理_登録(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile, List<DmPublisher> publishers)
        {
            return 管理記録処理.登録(loginUser, kanriKiroku, kanriKirokuFile, publishers);
        }

        public bool BLC_管理記録処理_更新(MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile)
        {
            return 管理記録処理.更新(loginUser, kanriKiroku, kanriKirokuFile);
        }

        //public bool BLC_管理記録処理_削除(MsUser loginUser, DmKanriKiroku kanriKiroku)
        //{
        //    return 管理記録処理.削除(loginUser, kanriKiroku);
        //}
    }

}
