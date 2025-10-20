using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        MsSiSalary BLC_基本給取得(MsUser loginUser, int msSiSalaryId);
        
        [OperationContract]
        int BLC_基本給登録(MsUser loginUser, MsSiSalary salary);


        [OperationContract]
        MsSiSalary BLC_基本給検索(MsUser loginUser, int kubun, DateTime start, DateTime end);


        [OperationContract]
        bool BLC_基本給複製(MsUser loginUser, MsSiSalary prevSalary, MsSiSalary newSalary);
    }

    public partial class Service
    {
        public MsSiSalary BLC_基本給取得(MsUser loginUser, int msSiSalaryId)
        {
            return 基本給管理.BLC_基本給取得(loginUser, msSiSalaryId);
        }

        public int BLC_基本給登録(MsUser loginUser, MsSiSalary salary)
        {
            return 基本給管理.BLC_基本給登録(loginUser, salary);
        }

        public MsSiSalary BLC_基本給検索(MsUser loginUser, int kubun, DateTime start, DateTime end)
        {
            return 基本給管理.BLC_基本給検索(loginUser, kubun, start, end);
        }

        public bool BLC_基本給複製(MsUser loginUser, MsSiSalary prevSalary, MsSiSalary newSalary)
        {
            return 基本給管理.BLC_基本給複製(loginUser, prevSalary, newSalary);
        }

    }
}
