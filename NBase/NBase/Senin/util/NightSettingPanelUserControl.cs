using NBaseData.DAC;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Senin.util
{
    public partial class NightSettingPanelUserControl : UserControl
    {
        private List<NightSettingDetailUserControl> nightSettingUCList;

        private List<MsSeninCompany> seninCompanyList = null;
        public List<MsSeninCompany> SeninCompanyList
        {
            set
            {
                comboBox_所属会社.Items.Clear();
                seninCompanyList = new List<MsSeninCompany>();

                comboBox_所属会社.Items.Add("");
                value.ForEach(obj => { comboBox_所属会社.Items.Add(obj); seninCompanyList.Add(obj); });
            }
        }

        private List<MsVessel> vesselList = null;
        public List<MsVessel> VesselList
        {
            set
            {
                vesselList = new List<MsVessel>();
                vesselList.AddRange(value);
                nightSettingUCList.ForEach(obj => { obj.VesselList = vesselList; });
            }
        }

        private List<SiNightSetting> nightSettingList = null;
        public List<SiNightSetting> NightSettings
        {
            set { Set(value); }
            get { return Get(); }
        }

        private string selectCompany = "";
        public string SelectCompany
        {
            set { SelectCompany = value; }
            get {
                if (comboBox_所属会社.SelectedItem is MsSeninCompany)
                {
                    return (comboBox_所属会社.SelectedItem as MsSeninCompany).CompanyName;
                }
                else return "";
                 }
        }

        public NightSettingPanelUserControl()
        {
            InitializeComponent();

            nightSettingUCList = new List<NightSettingDetailUserControl>();
            nightSettingUCList.Add(nightSettingUserControl1);
            nightSettingUCList.Add(nightSettingUserControl2);
            nightSettingUCList.Add(nightSettingUserControl3);
            nightSettingUCList.Add(nightSettingUserControl4);
            nightSettingUCList.Add(nightSettingUserControl5); 
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            NightSettingDetailUserControl nightSettingUserControl = new NightSettingDetailUserControl();
            nightSettingUserControl.VesselList = vesselList;
            nightSettingUCList.Add(nightSettingUserControl);

            flowLayoutPanel1.Controls.Add(nightSettingUserControl);
        }

        private void Set(List<SiNightSetting> settings)
        {
            nightSettingList = new List<SiNightSetting>();
            nightSettingList.AddRange(settings);

            if (nightSettingList.Count == 0) return;

            string seninCompanyId = nightSettingList[0].MsSeninCompanyID;

            var seninCompany = seninCompanyList.Where(o => o.MsSeninCompanyID == seninCompanyId).FirstOrDefault();
            if (seninCompany != null)
            {
                comboBox_所属会社.SelectedItem = seninCompany;


                for(int i = 0; i < nightSettingList.Count; i ++)
                {
                    if (nightSettingUCList.Count == i)
                    {
                        AddControl();
                    }
                    nightSettingUCList[i].NightSetting = nightSettingList[i];
                }
            }
        }

        private List<SiNightSetting> Get()
        {
            List<SiNightSetting> retList = new List<SiNightSetting>();

            string seninCompanyId = null;
            if (comboBox_所属会社.SelectedItem is MsSeninCompany)
            {
                seninCompanyId = (comboBox_所属会社.SelectedItem as MsSeninCompany).MsSeninCompanyID;
            }

            if (seninCompanyId == null)
            {
                if (nightSettingList != null)
                {
                    nightSettingList.ForEach(o => { o.DeleteFlag = 1; });
                    retList.AddRange(nightSettingList);
                }
            }
            else
            {
                nightSettingUCList.ForEach(o => {
                    var ret = o.NightSetting;
                    if (ret.MsVesselID == -1)
                    {
                        ret.DeleteFlag = 1;
                    }
                    else
                    {
                        ret.DeleteFlag = 0;
                    }
                    if (ret.IsNew() && ret.DeleteFlag == 0)
                    {
                        ret.MsSeninCompanyID = seninCompanyId;
                        retList.Add(ret);
                    }
                });
            }

            return retList;
        }

        private void comboBox_所属会社_TextChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox comb = sender as ComboBox;

                if (comb.SelectedItem is MsSeninCompany)
                {
                    flowLayoutPanel1.Enabled = true;
                    buttonAdd.Enabled = true;
                }
                else
                {
                    flowLayoutPanel1.Enabled = false;
                    buttonAdd.Enabled = false;
                }
            
            }
        }
    }
}
