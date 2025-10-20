using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;


namespace DeficiencyControl.Util
{
    /// <summary>
    /// 検索コントロールなどのアイテムを作成する。
    /// マスタデータなどはDBCacheの値を使用する
    /// </summary>
    public class ControlItemCreator
    {

        /// <summary>
        /// シングルラインコンボの作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combo"></param>
        /// <param name="datalist"></param>
        public static void CreateSingleLineCombo<T>(SingleLineCombo combo, List<T> datalist) where T : class
        {
            //既存クリア
            combo.Clear();

            //アイテム
            combo.Items.AddRange(datalist.ToArray());

            //検索用
            foreach (T data in datalist)
            {                
                combo.AutoCompleteCustomSource.Add(data.ToString());
            }

            combo.Text = "";

        }


        /// <summary>
        /// コンボボックスの作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combo"></param>
        /// <param name="datalist"></param>
        /// <param name="nonfkag">一行目に空白を入れるか true:入れる</param>
        public static void CreateComboBox<T>(ComboBox combo, List<T> datalist, bool nonfkag = false) where T : class
        {
            //既存クリア
            combo.Items.Clear();

            //空白いれる？
            if (nonfkag == true)
            {
                combo.Items.Add("");
            }

            if (datalist.Count <= 0)
            {
                return;
            }

            //Item ADD
            combo.Items.AddRange(datalist.ToArray());


            combo.SelectedIndex = 0;

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// ユーザーリストの作成 MsUser
        /// </summary>
        /// <param name="combo"></param>
        /// <returns></returns>
        public static void CreateUser(SingleLineCombo combo)
        {
            List<MsUser> datalist = DcGlobal.Global.DBCache.UserList;
            CreateSingleLineCombo<MsUser>(combo, datalist);
            
        }

        /// <summary>
        /// 船リストの作成 MsVessel
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateVessel(SingleLineCombo combo)
        {
            List<MsVessel> datalist = DcGlobal.Global.DBCache.VesselList;
            CreateSingleLineCombo<MsVessel>(combo, datalist);
        }


        /// <summary>
        /// 出力用ストリング
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="addstring"></param>
        public static void CreateVesselOutput(SingleLineCombo combo, string addstring)
        {
            List<MsVessel> datalist = DcGlobal.Global.DBCache.VesselList;
            //既存クリア
            combo.Clear();

            //アイテム
            combo.Items.Add(addstring);
            combo.Items.AddRange(datalist.ToArray());

            //検索用
            combo.AutoCompleteCustomSource.Add(addstring);
            foreach (MsVessel data in datalist)
            {
                combo.AutoCompleteCustomSource.Add(data.ToString());
            }

            combo.Text = addstring;
            
        }

        /// <summary>
        /// 船種リストの作成 MsVesselType
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateVesselType(ComboBox combo, bool flag = false)
        {
            List<MsVesselType> datalist = DcGlobal.Global.DBCache.VesselTypeList;
            CreateComboBox<MsVesselType>(combo, datalist, flag);
        }
        

        /// <summary>
        /// 検索種別の作成 MsItemKind
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag">空白可否</param>
        public static void CreateItemKind(ComboBox combo, bool flag = false)
        {
            List<MsItemKind> datalist = DcGlobal.Global.DBCache.ItemKindList;
            CreateComboBox<MsItemKind>(combo, datalist, flag);
        }


        /// <summary>
        /// ポート、場所の作成 MsBasho
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateBasho(SingleLineCombo combo)
        {
            List<MsBasho> datalist = DcGlobal.Global.DBCache.BashoList;
            CreateSingleLineCombo<MsBasho>(combo, datalist);
        }

        /// <summary>
        /// 国の作成 MsRegional
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateRegional(SingleLineCombo combo)
        {
            List<MsRegional> datalist = DcGlobal.Global.DBCache.RegionalList;
            CreateSingleLineCombo<MsRegional>(combo, datalist);
        }


      
        

        


        /// <summary>
        /// DeficiencyCodeの作成 MsDeficiencyCode
        /// </summary>
        /// <param name="combo">作成場所</param>
        /// <param name="flag">空白可否</param>
        public static void CreateDeficiencyCode(SingleLineCombo combo)
        {
            List<MsDeficiencyCode> datalist = DcGlobal.Global.DBCache.DeficiencyCodeList;
            CreateSingleLineCombo<MsDeficiencyCode>(combo, datalist);
        }




        /// <summary>
        /// ActionCodeの作成 MsActionCode 
        /// </summary>
        /// <param name="combo">作成場所</param>
        /// <param name="flag">空白可否</param>
        public static void CreateActionCode(ComboBox combo, bool flag = false)
        {
            List<MsActionCode> datalist = DcGlobal.Global.DBCache.ActionCodeList;
            CreateComboBox<MsActionCode>(combo, datalist, flag);
        }

 
    
        /// <summary>
        /// ステータスの作成 MsStatus
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateStatus(ComboBox combo, bool flag = false)
        {
            List<MsStatus> datalist = DcGlobal.Global.DBCache.StatusList;
            CreateComboBox<MsStatus>(combo, datalist, flag);
        }


        /// <summary>
        /// DeficiencyCodeの説明文を表示 DeficiencyCodeText
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateDeficiencyCodeText(SingleLineCombo combo)
        {
            List<MsDeficiencyCode> datalist = DcGlobal.Global.DBCache.DeficiencyCodeList;

            List<DeficiencyCodeText> textlist = new List<DeficiencyCodeText>();
            datalist.ForEach(x => textlist.Add(new DeficiencyCodeText(x)));

            

            CreateSingleLineCombo<DeficiencyCodeText>(combo, textlist);
        }



        /// <summary>
        /// MsAccidnetKindの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateMsAccidentKind(ComboBox combo, bool flag = false)
        {
            List<MsAccidentKind> datalist = DcGlobal.Global.DBCache.MsAccidentKindList;
            CreateComboBox<MsAccidentKind>(combo, datalist, flag);
        }

        /// <summary>
        /// MsAccidentSituationの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateMsAccidentSituation(ComboBox combo, bool flag = false)
        {
            List<MsAccidentSituation> datalist = DcGlobal.Global.DBCache.MsAccidentSituationList;
            CreateComboBox<MsAccidentSituation>(combo, datalist, flag);
        }

        /// <summary>
        /// MsKindOfAccidentの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CretaeMsKindOfAccident(ComboBox combo, bool flag = false)
        {
            List<MsKindOfAccident> datalist = DcGlobal.Global.DBCache.MsKindOfAccidentList;
            CreateComboBox<MsKindOfAccident>(combo, datalist, flag);
        }



        /// <summary>
        /// MsAccidentImportanceの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateMsAccidentImportance(ComboBox combo, bool flag = false)
        {
            List<MsAccidentImportance> datalist = DcGlobal.Global.DBCache.MsAccidentImportanceList;
            CreateComboBox<MsAccidentImportance>(combo, datalist, flag);
        }


        /// <summary>
        /// ViqVersionの作成
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateMsViqVersion(ComboBox combo, bool flag = false)
        {
            List<MsViqVersion> datalist = DcGlobal.Global.DBCache.MsViqVersionList;
            CreateComboBox<MsViqVersion>(combo, datalist, flag);
        }

        /// <summary>
        /// ViqCodeの作成
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateMsViqCode(ComboBox combo, bool flag = false)
        {
            List<MsViqCode> datalist = DcGlobal.Global.DBCache.MsViqCodeList;
            CreateComboBox<MsViqCode>(combo, datalist, flag);
        }

        /// <summary>
        /// ViqCodeの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="viq_version_id">作成VIQ Version EValで全部</param>
        /// <param name="flag"></param>
     
        public static void CreateMsViqCode(ComboBox combo, int viq_version_id, bool flag = false)
        {
            List<MsViqCode> datalist = DcGlobal.Global.DBCache.MsViqCodeList;
            
            //対象Codeの抜出
            if (viq_version_id != MsViqCode.EVal)
            {
                var n = from f in DcGlobal.Global.DBCache.MsViqCodeList where f.viq_version_id == viq_version_id select f;
                datalist = n.ToList();
            }

            CreateComboBox<MsViqCode>(combo, datalist, flag);
        }

        /// <summary>
        /// ViqNoの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="viq_code_id">作成VIQ Code EValで全部</param>
        public static void CreateMsViqNo(SingleLineCombo combo, int viq_code_id)
        {
            List<MsViqNo> nolist = DcGlobal.Global.DBCache.MsViqNoList;

            //対象番号の抜出
            if (viq_code_id != MsViqNo.EVal)
            {
                var n = from f in DcGlobal.Global.DBCache.MsViqNoList where f.viq_code_id == viq_code_id select f;
                nolist = n.ToList();
            }

            CreateSingleLineCombo<MsViqNo>(combo, nolist);
        }

        /// <summary>
        /// VIQ Noの作成　全部版
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateMsViqNo(SingleLineCombo combo)
        {
            List<MsViqNo> nolist = DcGlobal.Global.DBCache.MsViqNoList;

          

            CreateSingleLineCombo<MsViqNo>(combo, nolist);
        }



        /// <summary>
        /// MsInspectionCategoryの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateMsInspectionCategory(ComboBox combo, bool flag = false)
        {
            List<MsInspectionCategory> datalist = DcGlobal.Global.DBCache.MsInspectionCategoryList;
            CreateComboBox<MsInspectionCategory>(combo, datalist, flag);
        }



        /// <summary>
        /// 申請先の作成
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateMsCustomerAppointed(SingleLineCombo combo)
        {
            List<MsCustomer> datalist = DcGlobal.Global.DBCache.MsCustomerAppointedList;
            CreateSingleLineCombo<MsCustomer>(combo, datalist);
        }

        /// <summary>
        /// 検船実施会社の作成
        /// </summary>
        /// <param name="combo"></param>
        public static void CreateMsCustomerInspection(SingleLineCombo combo)
        {
            List<MsCustomer> datalist = DcGlobal.Global.DBCache.MsCustomerInspectionList;
            CreateSingleLineCombo<MsCustomer>(combo, datalist);
        }

        /// <summary>
        /// スケジュール種別の作成 MsScheduleKind
        /// </summary>
        /// <param name="cate">作成するカテゴリ</param>
        /// <param name="combo"></param>        
        /// <param name="flag"></param>
        public static void CreateMsScheduleKind(EScheduleCategory cate, ComboBox combo, bool flag = false)
        {
            var n = from f in DcGlobal.Global.DBCache.MsScheduleKindList where f.schedule_category_id == (int)cate select f;
            List<MsScheduleKind> datalist = n.ToList();
            
            CreateComboBox<MsScheduleKind>(combo, datalist, flag);                        
        }


        /// <summary>
        /// スケジュール種別詳細の作成 MsScheduleKindDetail
        /// </summary>
        /// <param name="schedule_kind_id"></param>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateMsScheduleKindDetail(int schedule_kind_id, ComboBox combo, bool flag = false)
        {
            var n = from f in DcGlobal.Global.DBCache.MsScheduleKindDetailList where f.schedule_kind_id == schedule_kind_id select f;
            List<MsScheduleKindDetail> datalist = n.ToList();

            CreateComboBox<MsScheduleKindDetail>(combo, datalist, flag);
        }


        /// <summary>
        /// 年度の作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateMsYear(ComboBox combo, bool flag = false)
        {
            List<MsYear> datalist = DcGlobal.Global.DBCache.MsYearList;

            CreateComboBox<MsYear>(combo, datalist, flag);
        }


        /// <summary>
        /// CrewMatrixTypeの作成
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="flag"></param>
        public static void CreateCrewMatrixType(ComboBox combo, bool flag = false)
        {
            List<MsCrewMatrixType> datalist = DcGlobal.Global.DBCache.MsCrewMatrixTypeList;
            CreateComboBox<MsCrewMatrixType>(combo, datalist, flag);
        }
    }
}
