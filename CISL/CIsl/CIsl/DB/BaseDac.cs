using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

using System.Reflection;
using CIsl.DB.WingDAC;


namespace CIsl.DB
{
  


    /// <summary>
    /// DACクラス基底 Decon用
    /// <remarks>publicなメンバ変数はDBフィールドと一致する名前で作成を行うこと。DBフィールド以外の情報を持たせるときはプロパティを使用すること</remarks>
    /// </summary>
    public abstract class BaseDac : DacBase
    {
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseDac()
        {
         
        }
        
        /// <summary> 
        /// テーブルID 自分自身のDBTableIDを返却すること
        /// </summary>
        public virtual int TableID
        {
            get { return EVal; }

        }


        #region メンバ変数

        /// <summary>
        /// 削除フラグ true=削除 false=未削除
        /// </summary>
        public bool delete_flag = false;

        /// <summary>
        /// 作成者
        /// </summary>
        public string create_ms_user_id = "";

        /// <summary>
        /// 更新者
        /// </summary>
        public string update_ms_user_id = "";

        /// <summary>
        /// 作成日
        /// </summary>
        public DateTime create_date = EDate;

        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime update_date = EDate;
        #endregion


        /// <summary>
        /// 削除の実行
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <param name="requser">削除ユーザー</param>
        /// <param name="tablename">テーブル名</param>
        /// <param name="idname">削除のIDフィールド名</param>
        /// <param name="id">削除ID</param>
        /// <param name="idname2">キーの二つ目ないときは省略</param>
        /// M<param name="id2">削除idの二つ目、ないときは省略</param>
        /// <returns></returns>
        protected bool ExecuteDelete(NpgsqlConnection cone, MsUser requser, string tablename, string idname, object id, string idname2 = "", object id2 = null)
        {
            try
            {
                string sql = @"
UPDATE 
{0}
SET
delete_flag = true,
update_ms_user_id = :update_ms_user_id
WHERE
{1} = :del_id
";

                sql = string.Format(sql, tablename, idname);

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "update_ms_user_id", Value = requser.ms_user_id });
                plist.Add(new SqlParamData() { Name = "del_id", Value = id });

                //Keyが二つある？
                if (idname2.Length > 0 && id2 != null)
                {
                    sql += "AND " + idname2 + " = :del_id2";
                    plist.Add(new SqlParamData() { Name = "del_id2", Value = id2 });

                }


                //実行
                bool ret = this.ExecuteNonQuery(cone, sql, plist);

                return ret;
            }
            catch (Exception e)
            {
                throw new Exception("ExecuteDelete FALSE", e);
            }
        }


        /// <summary>
        /// レコードの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public virtual int InsertRecord(NpgsqlConnection cone, MsUser requser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// レコードの更新 
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public virtual bool UpdateRecord(NpgsqlConnection cone, MsUser requser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// レコードの削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public virtual bool DeleteRecord(NpgsqlConnection cone, MsUser requser)
        {
            throw new NotImplementedException();
        }


        
    }


}
