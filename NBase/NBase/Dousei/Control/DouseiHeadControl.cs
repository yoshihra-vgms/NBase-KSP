using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WingData.DAC;

namespace Dousei.Control
{
    public partial class DouseiHeadControl : UserControl
    {
        public event 動静詳細Form.ClearEventDelegate ClearEventHandler;

        public DjDousei djDousei;
        private List<MsBasho> msBashos;
        private List<MsKichi> msKichis;
        private List<MsCargo> msCargos;
        public DateTime DouseDate
        {
            get
            {
                if (DouseDate_dateTimePicker.Value != null)
                {
                    DateTime ret = (DateTime)DouseDate_dateTimePicker.Value;
                    return new DateTime(ret.Year,ret.Month,ret.Day);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (value > DateTime.MinValue)
                {
                    DouseDate_dateTimePicker.Value = value;
                }
                else
                {
                    DouseDate_dateTimePicker.Value = null;
                }
            }
        }

        public string MsKanidouseiInfoShubetuID
        {
            get
            {
                return MsKanidouseiInfoShubetu.積みID;
            }
        }

        public string MsBashoID
        {
            get
            {
                if (Basho_comboBox.SelectedItem != null)
                {
                    MsBasho basho = Basho_comboBox.SelectedItem as MsBasho;
                    return basho.MsBashoId;
                }

                return "";
            }
            set
            {
                if (msBashos != null)
                {
                    for (int i = 0; i < msBashos.Count; i++)
                    {
                        if (msBashos[i].MsBashoId == value)
                        {
                            Basho_comboBox.SelectedItem = msBashos[i];
                            break;
                        }
                    }
                }
            }
        }

        public string MsKichiID
        {
            get
            {
                if (Kichi_comboBox.SelectedItem != null)
                {
                    MsKichi kichi = Kichi_comboBox.SelectedItem as MsKichi;
                    return kichi.MsKichiId;
                }

                return "";
            }
            set
            {
                if (msKichis != null)
                {
                    for (int i = 0; i < msKichis.Count; i++)
                    {
                        if (msKichis[i].MsKichiId == value)
                        {
                            Kichi_comboBox.SelectedItem = msKichis[i];
                            break;
                        }
                    }
                }
            }
        }

        public int Cargo1ID
        {
            get
            {
                if (Cargo1_comboBox.SelectedItem != null)
                {
                    MsCargo cargo = Cargo1_comboBox.SelectedItem as MsCargo;
                    return cargo.MsCargoID;
                }

                return int.MinValue;
            }
            set
            {
                if (msCargos != null)
                {
                    for (int i = 0; i < msCargos.Count; i++)
                    {
                        if (msCargos[i].MsCargoID == value)
                        {
                            Cargo1_comboBox.SelectedItem = msCargos[i];
                            break;
                        }
                    }
                }
            }
 
        }

        public decimal Qtty1
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(Qtty1_TextBox.Text);
                }
                catch
                {
                    return decimal.MinValue;
                }
            }
            set
            {
                if (value > decimal.MinValue)
                {
                    Qtty1_TextBox.Text = value.ToString();
                }
                else
                {
                    Qtty1_TextBox.Text = "";
                }
            }
        }

        public int Cargo2ID
        {
            get
            {
                if (Cargo2_comboBox.SelectedItem != null)
                {
                    MsCargo cargo = Cargo2_comboBox.SelectedItem as MsCargo;
                    return cargo.MsCargoID;
                }

                return int.MinValue;
            }
            set
            {
                if (msCargos != null)
                {
                    for (int i = 0; i < msCargos.Count; i++)
                    {
                        if (msCargos[i].MsCargoID == value)
                        {
                            Cargo2_comboBox.SelectedItem = msCargos[i];
                            break;
                        }
                    }
                }
            }
        }

        public decimal Qtty2
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(Qtty2_TextBox.Text);
                }
                catch
                {
                    return decimal.MinValue;
                }
            }
            set
            {
                if (value > decimal.MinValue)
                {
                    Qtty2_TextBox.Text = value.ToString();
                }
                else
                {
                    Qtty2_TextBox.Text = "";
                }
            }
        }

        public string GroupTitle { get; set; }

        public string 入港時間
        {
            get
            {
                return 入港_textBox.Text;
            }
            set
            {
                入港_textBox.Text = value;
            }
        }

        public string 着桟時間
        {
            get
            {
                return 着桟_textBox.Text;
            }
            set
            {
                着桟_textBox.Text = value;
            }
        }

        public string 荷役開始
        {
            get
            {
                return 荷役開始_textBox.Text;
            }
            set
            {
                荷役開始_textBox.Text = value;
            }
        }

        public string 荷役終了
        {
            get
            {
                return 荷役終了_textBox.Text;
            }
            set
            {
                荷役終了_textBox.Text = value;
            }
        }

        public string 離桟時間
        {
            get
            {
                return 離桟_textBox.Text;
            }
            set
            {
                離桟_textBox.Text = value;
            }
        }

        public string 出港時間
        {
            get
            {
                return 出港_textBox.Text;
            }
            set
            {
                出港_textBox.Text = value;
            }
        }

        public string VoaygeNo
        {
            get
            {
                return VoyageNo1_textBox.Text;
            }
            set
            {
                VoyageNo1_textBox.Text = value;
            }
        }

        public DouseiHeadControl()
        {
            InitializeComponent();
        }

        private void DouseiHeadControl_Load(object sender, EventArgs e)
        {
            Group_label.Text = GroupTitle;
        }

        public void Init(
            List<MsBasho> msBashos,
            List<MsKichi> msKichis,
            List<MsCargo> msCargos)
        {
            this.msBashos = msBashos;
            this.msKichis = msKichis;
            this.msCargos = msCargos;
            InitCombo();
            Clear();
        }
        private void InitCombo()
        {
            #region 場所
            Basho_comboBox.Items.Clear();
            Basho_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            Basho_comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (MsBasho basho in msBashos)
            {
                Basho_comboBox.Items.Add(basho);
                Basho_comboBox.AutoCompleteCustomSource.Add(basho.BashoName);
            }
            #endregion

            #region 基地
            Kichi_comboBox.Items.Clear();
            Kichi_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            Kichi_comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (MsKichi kichi in msKichis)
            {
                Kichi_comboBox.Items.Add(kichi);
                Kichi_comboBox.AutoCompleteCustomSource.Add(kichi.KichiName);
            }
            #endregion

            #region 貨物
            Cargo1_comboBox.Items.Clear();
            Cargo2_comboBox.Items.Clear();
            Cargo1_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            Cargo1_comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Cargo2_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            Cargo2_comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (MsCargo cargo in msCargos)
            {
                Cargo1_comboBox.Items.Add(cargo);
                Cargo1_comboBox.AutoCompleteCustomSource.Add(cargo.CargoName);
                Cargo2_comboBox.Items.Add(cargo);
                Cargo2_comboBox.AutoCompleteCustomSource.Add(cargo.CargoName);
            }
            #endregion
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            ClearEventHandler(djDousei);
            Clear();
        }

        private void Clear()
        {
            DouseDate_dateTimePicker.Value = null;
            VoyageNo1_textBox.Text = "";
            Basho_comboBox.SelectedIndex = -1;
            Kichi_comboBox.SelectedIndex = -1;
            Cargo1_comboBox.SelectedIndex = -1;
            Qtty1 = decimal.MinValue;
            Cargo2_comboBox.SelectedIndex = -1;
            Qtty2 = decimal.MinValue;
            GroupTitle = "";
            入港時間 = "";
            着桟時間 = "";
            荷役開始 = "";
            荷役終了 = "";
            離桟時間 = "";
            出港時間 = "";
        }

    }
}
