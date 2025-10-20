using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// ユーザー操作種別 ms_user_operation_kind_idと対応
    /// </summary>
    public enum EUserOperationKind
    {   
        Login = 1,
        Logout,
        
    }


    public class MsUserOperationKind : BaseDac
    {
    }
}
