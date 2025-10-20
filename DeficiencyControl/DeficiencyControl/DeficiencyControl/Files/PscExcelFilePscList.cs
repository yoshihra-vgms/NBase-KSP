using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB.DAC;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;
using DeficiencyControl.Util;
using DcCommon.DB;


namespace DeficiencyControl.Files
{
    /// <summary>
    /// PSC帳票 実績一覧タブ
    /// </summary>
    class PscExcelFilePscList : BasePSCOutputExcelTab
    {
        public PscExcelFilePscList(int sheetno)
            : base(sheetno)
        {
        }

        #region Excel変数名定義

        public const string StartYear = "**StartYear";
        public const string EndYear = "**EndYear";

        public const string No = "**V{0}_No_{1}";
        public const string VesselName = "**V{0}_VesselName_{1}";
        public const string Date = "**V{0}_Date_{1}";
        public const string Port = "**V{0}_Port_{1}";
        public const string DeficiencyCode = "**V{0}_DeficiencyCode_{1}";
        public const string Deficiency = "**V{0}_Deficiency_{1}";

        /// <summary>
        /// 船番号、アクションコード番号、データ番号
        /// </summary>
        public const string ActionCode = "**V{0}_ActionCode{1}_{2}";
        

        public const string Status = "**V{0}_Status_{1}";

        public const string PIC = "**V{0}_PIC_{1}";
        public const string ActionTakenByVessel = "**V{0}_ActionTakenByVessel_{1}";
        public const string ActionTakenByCompany = "**V{0}_ActionTakenByCompany_{1}";
        public const string NK = "**V{0}_NK_{1}";

        public const string CorrectiveAction = "**V{0}_CorrectiveAction_{1}";
        public const string CauseOfDeficiency = "**V{0}_CauseOfDeficiency_{1}";
        public const string ShareToFleet = "**V{0}_ShareToFleet_{1}";

        public const string DeficiencyCount = "**V{0}_DeficiencyCount";

        /// <summary>
        /// 船ごとの最大データ件数
        /// </summary>
        public const int MaxDeficiencyCount = 100;
        #endregion



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// テンプレートタグの作成
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="vno"></param>
        /// <param name="datano"></param>
        /// <returns></returns>
        private string CreateTemplateTag(string temp, int vno, int datano)
        {
            string ans = string.Format(temp, vno, datano);
            return ans;
        }

        /// <summary>
        /// 対象船の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="vno"></param>
        /// <param name="ves"></param>
        /// <param name="datalist"></param>
        private void WriteVesselData(XlsxCreator crea, int vno, MsVessel ves, List<PSCInspectionData> datalist)
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            string tag = "";            

            int no = 1;
            foreach (PSCInspectionData data in datalist)
            {
                DcCiPscInspection psc = data.PscInspection;

                //No
                tag = this.CreateTemplateTag(No, vno, no);
                crea.Cell(tag).Value = psc.ID;


                //船名
                tag = this.CreateTemplateTag(VesselName, vno, no);
                if (ves != null)
                {
                    crea.Cell(tag).Value = ves.ToString();
                }

                //検船日
                tag = this.CreateTemplateTag(Date, vno, no);
                crea.Cell(tag).Value = DcGlobal.DateTimeToString(psc.date);

                //港名
                tag = this.CreateTemplateTag(Port, vno, no);
                MsBasho ba = db.GetMsBasho(psc.ms_basho_id);
                if (ba != null)
                {
                    crea.Cell(tag).Value = ba.ToString();
                }


                //指摘コード
                tag = this.CreateTemplateTag(DeficiencyCode, vno, no);
                MsDeficiencyCode dcode = db.GetMsDeficiencyCode(psc.deficiency_code_id);
                if (dcode != null)
                {
                    crea.Cell(tag).Value = dcode.deficiency_code_name;
                }


                //指摘内容
                tag = this.CreateTemplateTag(Deficiency, vno, no);
                crea.Cell(tag).Value = psc.deficiency;

                //ActionCode
                {
                    //全データの書き出し
                    int acno = 1;
                    foreach (DcActionCodeHistory his in data.ActionCodeHistoryList)
                    {
                        //名前の作成
                        tag = string.Format(ActionCode, vno, acno, no);
                        MsActionCode ac = db.GetMsActionCode(his.action_code_id);
                        if (ac != null)
                        {
                            crea.Cell(tag).Value = ac.action_code_name;
                        }

                        acno++;

                    }
                }

                //ステータス
                tag = this.CreateTemplateTag(Status, vno, no);
                MsStatus st = db.GetMsStatus(psc.status_id);
                if(st!= null)
                {
                    crea.Cell(tag).Value = st.ToString();
                }

                //PIC
                tag = this.CreateTemplateTag(PIC, vno, no);
                MsUser pic = db.GetMsUser(psc.ms_user_id);
                if (pic != null)
                {
                    crea.Cell(tag).Value = pic.ToString();
                }


                //本船
                tag = this.CreateTemplateTag(ActionTakenByVessel, vno, no);
                crea.Cell(tag).Value = psc.action_taken_by_vessel;


                //会社
                tag = this.CreateTemplateTag(ActionTakenByCompany, vno, no);
                crea.Cell(tag).Value = psc.action_taken_by_company;

                //NK
                tag = this.CreateTemplateTag(NK, vno, no);
                crea.Cell(tag).Value = psc.class_involved;

                //是正結果
                tag = this.CreateTemplateTag(CorrectiveAction, vno, no);
                crea.Cell(tag).Value = psc.corrective_action;

                //原因
                tag = this.CreateTemplateTag(CauseOfDeficiency, vno, no);
                crea.Cell(tag).Value = psc.cause_of_deficiency;

                //会社横展開
                tag = this.CreateTemplateTag(ShareToFleet, vno, no);
                if (psc.share_to_our_fleet == true)
                {
                    crea.Cell(tag).Value = DcGlobal.DateTimeToString(psc.share_to_our_fleet_date);
                }

                no++;
            }

            //以降のデータを消す
            string starttag = this.CreateTemplateTag(No, vno, no );
            string endtag = this.CreateTemplateTag(No, vno, MaxDeficiencyCount);
            this.DeleteRows(crea, starttag, endtag);


            //総件数の書き込み
            tag = string.Format(DeficiencyCount, vno);
            crea.Cell(tag).Value = datalist.Count;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 実績一覧の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="idata"></param>
        /// <param name="odata"></param>
        public override void Write(XlsxCreator crea, PscOutputParameter idata, PscOutputData odata)
        {
            //自分のシートへ移動
            crea.SheetNo = this.SheetNo;

            string tag = "";

            //開始年度 終了年度の書き込み
            {
                crea.Cell(StartYear).Value = idata.StartYear;
                crea.Cell(EndYear).Value = idata.EndYear;
            }

            //対象船の書き込こみ
            int vno = 1;
            foreach (decimal vesid in odata.PSCVesselDic.Keys)
            {
                MsVessel ves = DcGlobal.Global.DBCache.GetMsVessel(vesid);
                if (ves == null)
                {
                    continue;
                }

                //対象データの書き込み
                this.WriteVesselData(crea, vno, ves, odata.PSCVesselDic[vesid]);

                vno++;

            }


            //残りを削除            
            {
                //Noを基準にする
                tag = this.CreateTemplateTag(No, vno, 1);
                this.DeleteRows(crea, tag, 100000);
            }


        }
    }
}
