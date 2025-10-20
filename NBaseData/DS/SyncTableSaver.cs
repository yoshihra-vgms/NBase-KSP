using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping;
using NBaseUtil;
using System.Reflection;
using ORMapping.Attrs;

namespace NBaseData.DS
{
    public abstract class SyncTableSaver
    {
        private static readonly string HASH_SEED = "NBASE";
        
        
        public static long InsertOrUpdate(ISyncTable record, NBaseData.DAC.MsUser loginUser, StatusUtils.通信状況 sendStatus)
        {
            return InsertOrUpdate(record, loginUser, sendStatus, null);
        }


        public static long InsertOrUpdate(ISyncTable record, NBaseData.DAC.MsUser loginUser, StatusUtils.通信状況 sendStatus, DBConnect dbConnect)
        {
            record.SendFlag = (int)sendStatus;
            record.UserKey = StringUtils.CreateHash(CreateHashSource(record));           

            bool ret = false;
            try
            {
                bool isExists = record.Exists(dbConnect, loginUser);

                if (!isExists)
                {
                    ret = record.InsertRecord(dbConnect, loginUser);
                }
                else
                {
                    ret = record.UpdateRecord(dbConnect, loginUser);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("InsertOrUpdate：" + e.Message);// Debug用コード
                if (record is NBaseData.DAC.DmKoubunshoKisokuFile)
                    Console.WriteLine("ID：" + (record as NBaseData.DAC.DmKoubunshoKisokuFile).DmKoubunshoKisokuFileID);// Debug用コード
                ret = false;
            }

            if (ret)
            {
                return record.DataNo;
            }
            else
            {
                return -1;
            }
        }

        public static bool InsertOrUpdate2(ISyncTableDoc record, NBaseData.DAC.MsUser loginUser, StatusUtils.通信状況 sendStatus)
        {
            return InsertOrUpdate2(record, loginUser, sendStatus, null);
        }

        public static bool InsertOrUpdate2(ISyncTableDoc record, NBaseData.DAC.MsUser loginUser, StatusUtils.通信状況 sendStatus, DBConnect dbConnect)
        {
            record.SendFlag = (int)sendStatus;
            record.UserKey = StringUtils.CreateHash(CreateHashSource(record));

            bool isExists = record.Exists(dbConnect, loginUser);
            bool ret = false;
            if (!isExists)
            {
                ret = record.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                ret = record.UpdateRecord(dbConnect, loginUser);
            }

            return ret;
        }

        public static bool ValidateUserKey(ISyncTable record)
        {
            return record.UserKey == StringUtils.CreateHash(CreateHashSource(record));
        }

        private static string CreateHashSource(ISyncTable record)
        {
            object pk = null;
            
            foreach (PropertyInfo info in record.GetType().GetProperties())
            {
                ColumnAttribute cAttr = Attribute.GetCustomAttribute(info, typeof(ColumnAttribute)) as ColumnAttribute;

                if (cAttr != null && cAttr.PrimaryKey)
                {
                    pk = info.GetValue(record, null);
                }
            }

            return pk + HASH_SEED;
        }

        public static string GetHashSource(ISyncTable record)
        {
            object pk = null;

            foreach (PropertyInfo info in record.GetType().GetProperties())
            {
                ColumnAttribute cAttr = Attribute.GetCustomAttribute(info, typeof(ColumnAttribute)) as ColumnAttribute;

                if (cAttr != null && cAttr.PrimaryKey)
                {
                    pk = info.GetValue(record, null);
                }
            }

            return pk + HASH_SEED;
        }
    }
}
