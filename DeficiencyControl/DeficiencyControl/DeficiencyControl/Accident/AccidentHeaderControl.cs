using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Util;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Accident
{
    /// <summary>
    /// Accidentヘッダーコントロール
    /// </summary>
    public partial class AccidentHeaderControl : BaseControl
    {
        public AccidentHeaderControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 出力データ
        /// </summary>
        public class AccidentHeaderControlOutputData
        {
            public DateTime Date;
            public MsUser PIC = null;
            public MsAccidentKind AccidentKind = null;
            public MsKindOfAccident KindOfAccident = null;
            public MsAccidentSituation AccidentSituation = null;
            public MsBasho Site = null;
            public MsVessel Vessel = null;
            public MsRegional Country = null;
            public string Title = "";
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public class AccidentHeaderControlData
        {
            /// <summary>
            /// これの元データ
            /// </summary>
            public AccidentData SrcData = null;
        }


        /// <summary>
        /// データ
        /// </summary>
        public AccidentHeaderControlData FData = null;
             

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロールの初期化
        /// </summary>
        private void InitDisplayControl()
        {

            //PIC
            ControlItemCreator.CreateUser(this.singleLineComboUser);

            //AccidnetKind
            ControlItemCreator.CreateMsAccidentKind(this.comboBoxAccidentKind);

            //Kind of Accident
            ControlItemCreator.CretaeMsKindOfAccident(this.comboBoxKindOfAccident);

            //Situation
            ControlItemCreator.CreateMsAccidentSituation(this.comboBoxAccidentSituation);


            //Port
            ControlItemCreator.CreateBasho(this.singleLineComboPort);
            
            //Vessel
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);            

            //Country
            ControlItemCreator.CreateRegional(this.singleLineComboCountry);
        }


        /// <summary>
        /// データの表示
        /// </summary>
        public void DispData()
        {
            DcAccident ac = this.FData.SrcData.Accident;
            DBDataCache db = DcGlobal.Global.DBCache;


            //発生日
            this.dateTimePickerDate.Value = ac.date;

            //PIC
            {
                MsUser u = db.GetMsUser(ac.ms_user_id);
                if (u != null)
                {
                    this.singleLineComboUser.Text = u.ToString();
                }
            }

            //Kind
            {
                MsAccidentKind ki = db.GetMsAccidentKind(ac.accident_kind_id);
                if (ki != null)
                {
                    this.comboBoxAccidentKind.SelectedItem = ki;
                }
            }

            //事故分類
            {
                MsKindOfAccident ak = db.GetMsKindOfAccident(ac.kind_of_accident_id);
                if (ak != null)
                {
                    this.comboBoxKindOfAccident.SelectedItem = ak;
                }

            }

            //発生状況
            {
                MsAccidentSituation sit = db.GetMsAccidentSituation(ac.accident_situation_id);
                if (sit != null)
                {
                    this.comboBoxAccidentSituation.SelectedItem = sit;
                }
            }

            //場所
            {
                MsBasho ba = db.GetMsBasho(ac.ms_basho_id);
                if (ba != null)
                {
                    this.singleLineComboPort.Text = ba.ToString();
                }
            }

            //船
            {
                MsVessel ves = db.GetMsVessel(ac.ms_vessel_id);
                if (ves != null)
                {
                    this.singleLineComboVessel.Text = ves.ToString();
                }
            }

            //国
            {
                MsRegional reg = db.GetMsRegional(ac.ms_regional_code);
                if (reg != null)
                {
                    this.singleLineComboCountry.Text = reg.ToString();
                }
            }

            //タイトル
            this.textBoxTitle.Text = ac.title;

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 入力エラーチェック
        /// </summary>
        /// <param name="chcol">色変更可否</param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {

            try
            {
                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {
                                      this.singleLineComboUser,
                                      this.singleLineComboVessel,
                                      this.singleLineComboCountry,                                      
                                      this.singleLineComboPort,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。

                //未来の日付での登録を禁止する
                DateTime dt = this.dateTimePickerDate.Value.Date;
                TimeSpan ts = dt - DateTime.Now.Date;
                if (ts.TotalMilliseconds > 0)
                {
                    this.ErList.Add(this.panelDateError);
                }


                //エラーがないなら終わり
                if (this.ErList.Count <= 0)
                {
                    return true;
                }


                throw new Exception("InputErrorException");

            }
            catch (Exception e)
            {
                //エラーの表示を行う
                if (chcol == true)
                {
                    this.DispError();
                }

            }
            
            return false;
        }


        /// <summary>
        /// データの取得
        /// </summary>
        /// <returns></returns>
        public AccidentHeaderControlOutputData GetInputData()
        {
            AccidentHeaderControlOutputData ans = new AccidentHeaderControlOutputData();

            ans.Date = this.dateTimePickerDate.Value.Date;
            ans.PIC = this.singleLineComboUser.SelectedItem as MsUser;
            ans.AccidentKind = this.comboBoxAccidentKind.SelectedItem as MsAccidentKind;
            ans.KindOfAccident = this.comboBoxKindOfAccident.SelectedItem as MsKindOfAccident;
            ans.AccidentSituation = this.comboBoxAccidentSituation.SelectedItem as MsAccidentSituation;
            ans.Site = this.singleLineComboPort.SelectedItem as MsBasho;
            ans.Vessel = this.singleLineComboVessel.SelectedItem as MsVessel;
            ans.Country = this.singleLineComboCountry.SelectedItem as MsRegional;
            ans.Title = this.textBoxTitle.Text.Trim();


            return ans;
        }

        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// このコントロールの初期化
        /// </summary>
        /// <param name="inputdata">AccidentData 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new AccidentHeaderControlData();
            this.FData.SrcData = inputdata as AccidentData;

            //画面初期化
            this.InitDisplayControl();

            if (this.FData.SrcData == null)
            {
                //新規

                //初期設定
                this.singleLineComboUser.Text = DcGlobal.Global.LoginMsUser.ToString();
            }
            else
            {
                //更新
                this.DispData();
            }


            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentHeaderControl_Load(object sender, EventArgs e)
        {

        }
    }
}
