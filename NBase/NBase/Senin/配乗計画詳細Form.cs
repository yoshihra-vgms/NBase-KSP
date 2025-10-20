using NBaseData.DAC;
using NBaseData.DS;
using NBaseData.BLC;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Senin
{
    public partial class 配乗計画詳細Form : Form
    {
        public Appointment Plan;
        public MsSenin Plan交代者 = null;

        private bool _enable;


        private List<object> Combobox予定種別List = new List<object>();
        List<配乗計画交代者> SeninList = new List<配乗計画交代者>();

        TimeSpan 半日時間 = new TimeSpan(12, 0, 0);

        private int PlanType;
        private int ShokumeiType = -1;

        /// <summary>
        /// 詳細画面
        /// </summary>
        /// <param name="planType">配乗計画のタイプ</param>
        /// <param name="plan">予定</param>
        /// <param name="limst">同じ人の一番近い予定の終わり</param>
        /// <param name="limend">同じ人の一番近い予定の始まり</param>
        /// <param name="cmbSyubeList">種別のリスト</param>
        /// <param name="enable">falseなら変更できない</param>
        public 配乗計画詳細Form(int planType, int shokumeiType, Appointment plan, List<object> cmbSyubeList, bool enable, List<MsSenin> seninViewList)
        {
            InitializeComponent();

            PlanType = planType;
            ShokumeiType = shokumeiType;

            Plan = plan;

            foreach (object obj in cmbSyubeList)
            {
                Combobox予定種別List.Add(obj);
            }

            foreach (MsSenin s in seninViewList)
            {
                配乗計画交代者 o = new 配乗計画交代者();
                o.Senin = new MsSenin();
                o.Senin = s;
                SeninList.Add(o);
            }

            _enable = enable;

        }

        private void 配乗計画詳細Form_Load(object sender, EventArgs e)
        {

            //職名と名前
            textBox_Shokumei.Text = Plan.ShokuName;
            textBox_Name.Text = Plan.SeninName;

            #region 職種コンボボックスセット
            Shokumei selitem = null;
            List<Shokumei> shokulist = SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, ShokumeiType);
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


            //日付
            dateTimePicker_From.Value = Plan.StartDate.Date;
            dateTimePicker_To.Value = Plan.EndDate.Date;



            #region 予定種別コンボボックスセット

            //予定種別作成
            foreach (object obj in Combobox予定種別List)
            {
                comboBox_予定種別.Items.Add(obj);

                //選択する
                if (obj is MsVessel)
                {
                    MsVessel vsl = obj as MsVessel;
                    if (Plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船) && vsl.MsVesselID == Plan.MsVesselID)
                    {
                        comboBox_予定種別.SelectedItem = obj;
                    }
                }
                else if (obj is MsSiShubetsu)
                {
                    MsSiShubetsu shube = obj as MsSiShubetsu;
                    if (Plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.傷病))
                    {
                        if (shube.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.傷病))
                        {
                            comboBox_予定種別.SelectedItem = obj;
                        }
                    }
                    else if (Plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                    {
                        if (shube.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇))
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


            if (PlanType == MsPlanType.PlanTypeOneMonth)
            {
                panelPM.Visible = true;
                groupBoxReplacement.Visible = false;

                this.Height = 273;


                //午後かどうか
                if (Plan.PmStart == 1)
                {
                    checkBox_dateFromPM.Checked = true;
                }
                if (Plan.PmEnd == 1)
                {
                    checkBox_dateToPM.Checked = true;
                }
            }
            else
            {
                panelPM.Visible = false;
                groupBoxReplacement.Visible = true;

                this.Height = 408;


                #region 港コンボボックスセット

                List<MsBasho> basholist = new List<MsBasho>();
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    List<MsBasho> wklist = serviceClient.MsBasho_GetRecordsBy港(NBaseCommon.Common.LoginUser);
                    if (wklist.Count > 0)
                    {
                        basholist = wklist.Where(obj => obj.GaichiFlag == 0).ToList();
                    }
                }

                comboBox_港.Items.Add("");
                foreach (MsBasho basho in basholist)
                {
                    comboBox_港.Items.Add(basho);
                }

                #endregion

                #region 港選択 
                if (Plan.MsBashoID!=null && Plan.MsBashoID.Length > 0)
                {
                    foreach (MsBasho basho in basholist)
                    {
                        if (Plan.MsBashoID == basho.MsBashoId)
                        {
                            comboBox_港.SelectedItem = basho;
                            break;
                        }
                    }
                }
                #endregion

 
                #region 船員コンボボックスセット
                comboBox_交代者.Items.Clear();

                comboBox_交代者.Items.Add("");
                foreach (配乗計画交代者 o in SeninList)
                {
                    comboBox_交代者.Items.Add(o);
                }
                #endregion

                //交代情報
                #region 交代者選択
                if (Plan.Replacement != null && Plan.Replacement.Length > 0)
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        Plan交代者 = serviceClient.BLC_配乗計画_GetMsSenin(NBaseCommon.Common.LoginUser, Plan.Replacement, Plan.ActFlg==0?true:false);
                    }
                    if (Plan交代者 != null)
                    {
                        foreach (配乗計画交代者 man in SeninList)
                        {
                            if (Plan交代者.MsSeninID == man.Senin.MsSeninID)
                            {
                                comboBox_交代者.SelectedItem = man;
                                break;
                            }
                        }
                    }
                }
                #endregion

                //交代として登録されているものは種別、職務は変更できない
                bool 交代 = true;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    交代 = serviceClient.BLC_配乗計画_Is交代乗船(NBaseCommon.Common.LoginUser, Plan.SiCardPlanID);
                }
                if (交代)
                {
                    comboBox_予定種別.Enabled = false;
                    comboBox_職種.Enabled = false;
                }
            }

            checkBoxLinkageReplacement.Checked = true;


            //操作不可
            if (_enable == false)
            {
                button_OK.Enabled = false;
                button_delete.Enabled = false;
            }
        }

        /// <summary>
        /// 予定種別が船になった時だけ交代情報の入力ができる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_予定種別_TextChanged(object sender, EventArgs e)
        {
            if (comboBox_予定種別.SelectedItem is MsVessel)
            {
                groupBoxReplacement.Enabled = true;
            }
            else
            {
                groupBoxReplacement.Enabled = false;
            }
        }

        /// <summary>
        /// 交代情報クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Clear_Click(object sender, EventArgs e)
        {
            comboBox_港.SelectedIndex = 0;
            comboBox_交代者.SelectedIndex = 0;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (CheckValidate() == false) return;

            #region 予定種別
            if (comboBox_予定種別.SelectedItem is MsVessel)
            {
                Plan.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船);

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
            Plan.EndDate = dateTimePicker_To.Value;

            if (panelPM.Visible)
            {
                if (checkBox_dateFromPM.Checked)
                {
                    Plan.PmStart = 1;
                }
                else
                {
                    Plan.PmStart = 0;
                }
                if (checkBox_dateToPM.Checked)
                {
                    Plan.PmEnd = 1;
                }
                else
                {
                    Plan.PmEnd = 0;
                }
            }
            #endregion

            #region 職名変更
            if (comboBox_職種.SelectedItem is Shokumei)
            {
                int shkuID = (comboBox_職種.SelectedItem as Shokumei).MsSiShokumeiID;
                Plan.MsSiShokumeiID = (comboBox_職種.SelectedItem as Shokumei).MsSiShokumeiID;
                Plan.MsSiShokumeiShousaiID = (comboBox_職種.SelectedItem as Shokumei).MsSiShokumeiShousaiID;

                Shokumei shokumei = SeninTableCache.instance().GetShokumei(NBaseCommon.Common.LoginUser, ShokumeiType, Plan.MsSiShokumeiID, Plan.MsSiShokumeiShousaiID);

                Plan.ShokuName = shokumei.Name;
                Plan.ShokuNameAbbr = shokumei.NameAbbr;
                Plan.ShokuNameEng = shokumei.NameEng;
            }
            #endregion

            System.Diagnostics.Debug.WriteLine("詳細： startdate=" + Plan.StartDate.ToString() + " , enddate=" + Plan.EndDate.ToString());


            //種別が乗船でないなら交代者情報は無しにする
            if (Plan.MsSiShubetsuID != SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
            {
                Plan.Replacement = "";
                Plan.MsBashoID = "";
            }
            else if (groupBoxReplacement.Visible)
            {
                #region 交代者がある場合
                if (comboBox_交代者.SelectedItem is 配乗計画交代者)
                {
                    //港
                    if (comboBox_港.SelectedItem is MsBasho)
                    {
                        Plan.MsBashoID = (comboBox_港.SelectedItem as MsBasho).MsBashoId;
                    }

                    //MsSenin koutaisya = (comboBox_交代者.SelectedItem as 配乗計画交代者).Senin;

                    //SiCardPlan koutaiplan = null;
                    ////交代者が新しく選択されたり、交代者の変更があった場合はPlanCardを新規作成する
                    //if (Plan交代者 == null || Plan交代者.MsSeninID != koutaisya.MsSeninID)
                    //{
                    //    koutaiplan = 交代者のPlan登録(koutaisya.MsSeninID);
                    //    Plan.Replacement = koutaiplan.SiCardPlanID;
                    //}

                    Plan.ReplacementSeninID = (comboBox_交代者.SelectedItem as 配乗計画交代者).Senin.MsSeninID;

                    Plan.LinkageReplacement = checkBoxLinkageReplacement.Checked;
                }
                #endregion
            }

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
            bool ret = true;

            DateTime wkStartDate = dateTimePicker_From.Value.Date;
            DateTime wkEndDate = dateTimePicker_To.Value.Date;
            int pmstart = 0;
            int pmend = 0;

            if (panelPM.Visible)
            {
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
                    return false;
                }
                else if (wkStartDate == wkEndDate && pmstart == 1 && pmend == 1)//日付と午後チェックも全く同じ
                {
                    MessageBox.Show("開始日は終了日より前を指定してください。");
                    return false;
                }

            }
            else if (wkStartDate >= wkEndDate)
            {
                MessageBox.Show("開始日は終了日より前を指定してください。");
                return false;
            }




            string result = "";
            //サーバーで日付や締めのチェック
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                SiCardPlan wkplan = Plan.MakeSiCardPlan();

                result = serviceClient.BLC_配乗計画_CheckValidate(NBaseCommon.Common.LoginUser, wkplan, wkStartDate, pmstart, wkEndDate, pmend, PlanType);
            }
            if (result.Length > 0)
            {
                MessageBox.Show(result, "配乗計画");
                return false;
            }

            if (groupBoxReplacement.Visible)
            {
                #region 交代者と港の入力チェック

                //交代が無い場合はチェックしない
                if ((comboBox_交代者.SelectedItem is 配乗計画交代者) == true && (comboBox_港.SelectedItem is MsBasho) == false)
                {
                    MessageBox.Show("港を選択してください。", "配乗計画");
                    return false;
                }
                if ((comboBox_交代者.SelectedItem is 配乗計画交代者) == false && (comboBox_港.SelectedItem is MsBasho) == true)
                {
                    MessageBox.Show("交代者を選択してください。", "配乗計画");
                    return false;
                }
                #endregion

                if (ret && comboBox_交代者.SelectedItem is 配乗計画交代者)
                {
                    配乗計画交代者 kotai = comboBox_交代者.SelectedItem as 配乗計画交代者;

                    //退職者のチェック(交代者)
                    if (kotai.Senin.RetireFlag == 1 && kotai.Senin.RetireDate < wkEndDate.AddDays(1))
                    {
                        MessageBox.Show("交代者は退職されています。(退職日：" + kotai.Senin.RetireDate.ToString("yyyy/M/d") + ")", "配乗計画");
                        return false;
                    }

                    //交代者の予定のチェック
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        ret = serviceClient.BLC_配乗計画_Check交代者予定(NBaseCommon.Common.LoginUser, Plan.Replacement, kotai.Senin, wkEndDate.AddDays(1));
                    }
                    if (ret == false)
                    {
                        MessageBox.Show("交代者には既に予定が入っています。", "配乗計画");
                        return false;
                    }
                }

            }

            return ret;

        }

        //private SiCardPlan 交代者のPlan登録(int seninID)
        //{
        //    SiCardPlan newplan = new SiCardPlan();
        //    newplan.MsSeninID = seninID;
        //    newplan.MsSiShubetsuID = Plan.MsSiShubetsuID;
        //    newplan.MsVesselID = Plan.MsVesselID;
        //    newplan.MsSiShokumeiID = Plan.MsSiShokumeiID;
        //    newplan.MsSiShokumeiShousaiID = Plan.MsSiShokumeiShousaiID;
        //    newplan.StartDate = Plan.EndDate.AddDays(1);
        //    newplan.EndDate = newplan.StartDate.AddDays(1);
        //    newplan.Replacement = "";
        //    newplan.MsBashoID = "";

        //    //登録月を取得
        //    DateTime dt = NBaseUtil.DateTimeUtils.ToFromMonth(newplan.StartDate);

        //    SiCardPlan retplan = null;
        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        retplan = serviceClient.BLC_配乗計画_InsertOrUpdate(NBaseCommon.Common.LoginUser, newplan, dt, NBaseCommon.Common.配乗計画TYPE);
        //    }
        //    return retplan;
        //}
    }

    public class 配乗計画交代者
    {
        public MsSenin Senin;
        public override string ToString()
        {
            return Senin.Sei + " " + Senin.Mei;
        }
    }

}
