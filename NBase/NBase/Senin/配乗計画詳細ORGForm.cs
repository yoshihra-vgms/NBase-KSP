using NBaseData.DAC;
using NBaseData.DS;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Senin
{
    public partial class 配乗計画詳細ORGForm : Form
    {
        public Appointment Plan;

        private bool _enable;

        private List<object> Combobox予定種別List = new List<object>();
        
        //public SiCardPlan ChangeYotei = null;

        TimeSpan 半日時間 = new TimeSpan(12, 0, 0);

        private int PlanType;

        /// <summary>
        /// 詳細画面
        /// </summary>
        /// <param name="planType">配乗計画のタイプ</param>
        /// <param name="plan">予定</param>
        /// <param name="limst">同じ人の一番近い予定の終わり</param>
        /// <param name="limend">同じ人の一番近い予定の始まり</param>
        /// <param name="cmbSyubeList">種別のリスト</param>
        /// <param name="enable">falseなら変更できない</param>
        public 配乗計画詳細ORGForm(int planType, Appointment plan, List<object> cmbSyubeList, bool enable)
        {
            InitializeComponent();

            PlanType = planType;
            Plan = plan;

            foreach (object obj in cmbSyubeList)
            {
                Combobox予定種別List.Add(obj);
            }
            _enable = enable;
        }

        private void 配乗計画詳細Form_Load(object sender, EventArgs e)
        {

            #region 予定種別コンボボックス
            
            //予定種別作成
            foreach (object obj in Combobox予定種別List)
            {
                comboBox_予定種別.Items.Add(obj);

                //選択する
                if (obj is MsVessel)
                {
                    MsVessel vsl = obj as MsVessel;
                    if (Plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && vsl.MsVesselID == Plan.MsVesselID)
                    {
                        comboBox_予定種別.SelectedItem = obj;
                    }
                }
                else if (obj is MsSiShubetsu)
                {
                    MsSiShubetsu shube = obj as MsSiShubetsu;
                    if (Plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_傷病ID(NBaseCommon.Common.LoginUser))
                    {
                        if (shube.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_傷病ID(NBaseCommon.Common.LoginUser))
                        {
                            comboBox_予定種別.SelectedItem = obj;
                        }
                    }
                    else if (Plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser))
                    {
                        if (shube.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser))
                        {
                            comboBox_予定種別.SelectedItem = obj;
                        }
                    }
                }
            }
            //選択ないなら最初を選択
            if (Plan.MsSiShubetsuID == -1)
            {
                comboBox_予定種別.SelectedIndex = 0;//新規予定の場合のデフォルト
            }  
            #endregion

            #region 職務変更
            Shokumei selitem = null;
            List<Shokumei> shokulist = SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser,Shokumei.フェリー);
            foreach (Shokumei s in shokulist)
            {
                comboBox_職種.Items.Add(s);

                if (Plan.MsSiShokumeiID == s.MsSiShokumeiID && Plan.MsSiShokumeiShousaiID == s.MsSiShokumeiShousaiID)
                {
                    selitem = s;
                }
            }
            //選択する
            if (selitem != null)
            {
                comboBox_職種.SelectedItem = selitem;
            }
            else
            {
                comboBox_職種.SelectedIndex = 0;
            }
            #endregion

            //職名と名前
            textBox_Shokumei.Text = Plan.ShokuName;
            textBox_Name.Text = Plan.SeninName;

            //日付
            dateTimePicker_From.Value = Plan.StartDate.Date;  
            dateTimePicker_To.Value = Plan.EndDate.Date;

            //午後かどうか
            if (Plan.PmStart == 1)
            {
                checkBox_dateFromPM.Checked = true;
            }
            if (Plan.PmEnd  == 1)
            {
                checkBox_dateToPM.Checked = true;
            }

            //操作不可
            if (_enable == false)
            {
                button_OK.Enabled = false;
                button_delete.Enabled = false;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {

            if (CheckValidate() == false) return;
            
            #region 予定種別
            if (comboBox_予定種別.SelectedItem is MsVessel)
            {
                Plan.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser);

                MsVessel vsl = comboBox_予定種別.SelectedItem as MsVessel;
                Plan.MsVesselID = vsl.MsVesselID;
            }
            else if (comboBox_予定種別.SelectedItem is MsSiShubetsu)
            {
                MsSiShubetsu shube = comboBox_予定種別.SelectedItem as MsSiShubetsu;

                Plan.MsSiShubetsuID = shube.MsSiShubetsuID;
            }

            #endregion


            #region 期間
            Plan.StartDate = dateTimePicker_From.Value;
            if (checkBox_dateFromPM.Checked)
            {
                Plan.PmStart = 1;
            }

            Plan.EndDate = dateTimePicker_To.Value;

            if (checkBox_dateToPM.Checked)
            {
                Plan.PmEnd = 1;
            }
            #endregion


            #region 職名変更
            if (comboBox_職種.SelectedItem is Shokumei)
            {
                int shkuID = (comboBox_職種.SelectedItem as Shokumei).MsSiShokumeiID;
                Plan.MsSiShokumeiID = (comboBox_職種.SelectedItem as Shokumei).MsSiShokumeiID;
                Plan.MsSiShokumeiShousaiID = (comboBox_職種.SelectedItem as Shokumei).MsSiShokumeiShousaiID;

                Shokumei shokumei = SeninTableCache.instance().GetShokumei(NBaseCommon.Common.LoginUser, Shokumei.フェリー, Plan.MsSiShokumeiID, Plan.MsSiShokumeiShousaiID);

                Plan.ShokuName = shokumei.Name;
                Plan.ShokuNameAbbr = shokumei.NameAbbr;
                Plan.ShokuNameEng = shokumei.NameEng;
            }
            #endregion

            System.Diagnostics.Debug.WriteLine("詳細： startdate=" + Plan.StartDate.ToString() + " , enddate=" + Plan.EndDate.ToString());

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private bool CheckValidate()
        {
            string wkstr = "";
            bool ret = true;

            DateTime wkStartDate = dateTimePicker_From.Value.Date;
            int pmstart = 0;
            DateTime wkEndDate = dateTimePicker_To.Value.Date;
            int pmend = 0;

            if (checkBox_dateFromPM.Checked == true)
            {
                pmstart = 1;
            }
            if (checkBox_dateToPM.Checked == true)
            {
                pmend = 1;
            }

            if (wkStartDate > wkEndDate)
            {
                MessageBox.Show("開始日は終了日より前を指定してください。");
                ret = false;
            }
            else if (wkStartDate == wkEndDate && pmstart == 1 &&  pmend == 1)//日付と午後チェックも全く同じ
            {
                MessageBox.Show("開始日は終了日より前を指定してください。");
                ret = false;
            }

            string result = "";
            //サーバーで日付や締めのチェック
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                SiCardPlan wkplan = Plan.MakeSiCardPlan();

                //result = serviceClient.BLC_配乗計画_CheckValidate(NBaseCommon.Common.LoginUser, wkplan, wkStartDate,pmstart, wkEndDate, pmend, SiCardPlanHead.VESSEL_KIND_フェリー);
                result = serviceClient.BLC_配乗計画_CheckValidate(NBaseCommon.Common.LoginUser, wkplan, wkStartDate, pmstart, wkEndDate, pmend, PlanType) ;
            }
            if (result.Length > 0)
            {
                MessageBox.Show(result, "配乗計画");
                ret = false;
            }
            
            return ret;

        }

        

    }

}
