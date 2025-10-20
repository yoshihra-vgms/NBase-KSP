using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;
using CIsl.DB;

namespace DcCommon.DB
{
    /// <summary>
    /// 検船ヘッダーデータ
    /// </summary>
    public class MoiHeaderData
    {
        public MoiHeaderData()
        {

        }

        /// <summary>
        /// 検船
        /// </summary>
        public DcMoi Moi = null;

        /// <summary>
        /// 検船添付ファイル
        /// </summary>
        public List<DcAttachment> AttachmentList = new List<DcAttachment>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 対象のデータの一式を作成する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi"></param>
        /// <returns></returns>
        private static MoiHeaderData CreateSelectData(NpgsqlConnection cone, DcMoi moi)
        {
            MoiHeaderData ans = new MoiHeaderData();

            if (moi == null)
            {
                return null;
            }

            ans.Moi = moi;
            ans.AttachmentList = DcAttachment.GetRecrodsByMoiID(cone, moi.moi_id);

            return ans;

        }


        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="moi_id"></param>
        /// <returns></returns>
        public static MoiHeaderData GetDataByMoiID(NpgsqlConnection cone, int moi_id)
        {
            MoiHeaderData ans = new MoiHeaderData();


            try
            {
                //データの作成
                DcMoi moi = DcMoi.GetRecordByMoiID(cone, moi_id);

                //残りを取得
                ans = MoiHeaderData.CreateSelectData(cone, moi);

            }
            catch (Exception e)
            {
                throw new Exception("MoiHeaderData GetDataByMoiID", e);
            }



            return ans;
        }

        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>        
        /// <returns></returns>
        public int InsertData(NpgsqlConnection cone, MsUser requser)
        {
            int ansid = DcMoi.EVal;

            try
            {
                //本体挿入                
                ansid = this.Moi.InsertRecord(cone, requser);

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.InsertRecordMoi(cone, requser, ansid);
                }


            }
            catch (Exception e)
            {
                throw new Exception("MoiHeaderData InsertData", e);
            }


            return ansid;
        }


        /// <summary>
        /// 検船の更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>        
        /// <returns></returns>
        public bool UpdateData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //本体                
                ret = this.Moi.UpdateRecord(cone, requser);

                //ファイル挿入、削除
                foreach (DcAttachment att in this.AttachmentList)
                {
                    //DeleteFlagで削除する もしくは親の削除
                    if (att.delete_flag == true || this.Moi.delete_flag == true)
                    {
                        att.DeleteRecord(cone, requser);
                    }
                    else
                    {
                        att.InsertRecordMoi(cone, requser, this.Moi.moi_id);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("MoiHeaderData UpdateData", e);
            }


            return ret;
        }


        /// <summary>
        /// MoiHeaderDataの削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DeleteData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //本体削除
                ret = this.Moi.DeleteRecord(cone, requser);

                //ファイル削除
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.DeleteRecord(cone, requser);
                }

            }
            catch (Exception e)
            {
                throw new Exception("MoiHeaderData DeleteData", e);
            }


            return ret;
        }
    }
}
