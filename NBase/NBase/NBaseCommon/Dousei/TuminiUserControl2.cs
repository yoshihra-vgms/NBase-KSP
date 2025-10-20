using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseCommon
{
    public partial class TuminiUserControl2 : UserControl
    {
        public enum ModeEnum { 積み, 揚げ } ;
        private DjDouseiCargo DouseiCargo = null;
        private List<MsCargo> MsCargo_list = null;
        private List<MsDjTani> MsDjTani_list = null;

        //public TuminiUserControl2(ModeEnum mode, int no)
        //{
        //    DouseiCargo = new DjDouseiCargo();
        //    List<MsCargo> msCargo_list = new List<MsCargo>();
        //    List<MsDjTani> msDjTani_list = new List<MsDjTani>();

        //    Initialize(mode, no, msCargo_list, msDjTani_list);
        //}
        public TuminiUserControl2(ModeEnum mode, int no, List<MsCargo> msCargo_list, List<MsDjTani> msDjTani_list)
        {
            DouseiCargo = new DjDouseiCargo();
            Initialize(mode, no, msCargo_list, msDjTani_list);
        }
        private void Initialize(ModeEnum mode, int no, List<MsCargo> msCargo_list, List<MsDjTani> msDjTani_list)
        {
            InitializeComponent();
            if (no == 1)
            {
                if (mode == ModeEnum.積み)
                {
                    label_1.Text = "※ 積荷" + no.ToString();
                }
                else
                {
                    label_1.Text = "※ 揚荷" + no.ToString();
                }
            }
            else
            {
                if (mode == ModeEnum.積み)
                {
                    label_1.Text = "　　積荷" + no.ToString();
                }
                else
                {
                    label_1.Text = "　　揚荷" + no.ToString();
                }
            }

            //comboBox_積荷.Items.Clear();
            //comboBox_積荷.AutoCompleteMode = AutoCompleteMode.Suggest;
            //comboBox_積荷.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //foreach (MsCargo cargo in msCargo_list)
            //{
            //    comboBox_積荷.Items.Add(cargo);
            //    comboBox_積荷.AutoCompleteCustomSource.Add(cargo.CargoName);
            //}


            //comboBox_単位.Items.Clear();
            //foreach (MsDjTani tani in msDjTani_list)
            //{
            //    comboBox_単位.Items.Add(tani);
            //}
        }

        public void Initialize(List<MsCargo> msCargo_list, List<MsDjTani> msDjTani_list)
        {
            //if ((comboBox_積荷.Items == null || comboBox_積荷.Items.Count == 0) && msCargo_list.Count > 0)
            //{
            //    comboBox_積荷.Items.Clear();
            //    comboBox_積荷.AutoCompleteMode = AutoCompleteMode.Suggest;
            //    comboBox_積荷.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    foreach (MsCargo cargo in msCargo_list)
            //    {
            //        comboBox_積荷.Items.Add(cargo);
            //        comboBox_積荷.AutoCompleteCustomSource.Add(cargo.CargoName);
            //    }
            //}

            if ((comboBox_単位.Items == null || comboBox_単位.Items.Count == 0) && msDjTani_list.Count > 0)
            {
                comboBox_単位.Items.Clear();
                foreach (MsDjTani tani in msDjTani_list)
                {
                    comboBox_単位.Items.Add(tani);
                }
            }

            MsCargo_list = msCargo_list;
            MsDjTani_list = msDjTani_list;
        }

        public DjDouseiCargo GetInstance()
        {
            if (comboBox_積荷.SelectedItem is MsCargo)
            {
                MsCargo cargo = comboBox_積荷.SelectedItem as MsCargo;

                DouseiCargo.MsCargoID = cargo.MsCargoID;
            }
            else
            {
                DouseiCargo.MsCargoID = int.MinValue;
            }
            try
            {
                DouseiCargo.Qtty = Convert.ToDecimal(textBox_数量.Text);
            }
            catch
            {
                DouseiCargo.Qtty = decimal.MinValue;
            }
            if (comboBox_単位.SelectedItem is MsDjTani)
            {
                MsDjTani tani = comboBox_単位.SelectedItem as MsDjTani;
                DouseiCargo.MsDjTaniID = tani.MsDjTaniID;
            }
            else
            {
                DouseiCargo.MsDjTaniID = null;
            }
            return DouseiCargo;
        }


        public void SetInstance(DjDouseiCargo douseiCargo)
        {
            DouseiCargo = douseiCargo;

            //foreach (MsCargo cargo in comboBox_積荷.Items)
            //{
            //    if (cargo.MsCargoID == douseiCargo.MsCargoID)
            //    {
            //        comboBox_積荷.SelectedItem = cargo;
            //        break;
            //    }
            //}
            if ((comboBox_積荷.Items == null || comboBox_積荷.Items.Count == 0) && MsCargo_list.Count > 0)
            {
                comboBox_積荷.Items.Clear();
                comboBox_積荷.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox_積荷.AutoCompleteSource = AutoCompleteSource.CustomSource;
                foreach (MsCargo c in MsCargo_list)
                {
                    comboBox_積荷.Items.Add(c);
                    comboBox_積荷.AutoCompleteCustomSource.Add(c.CargoName);
                }
            }
            var cargo = MsCargo_list.Where(o => o.MsCargoID == douseiCargo.MsCargoID).FirstOrDefault();
            comboBox_積荷.SelectedItem = cargo;

            textBox_数量.Text = douseiCargo.Qtty.ToString(".000");
            foreach (MsDjTani tani in comboBox_単位.Items)
            {
                if (tani.MsDjTaniID == douseiCargo.MsDjTaniID)
                {
                    comboBox_単位.SelectedItem = tani;
                    break;
                }
            }
        }
        public void ClearInstance()
        {
            DouseiCargo = new DjDouseiCargo();
            Clear();
        }

        public void Clear()
        {
            comboBox_積荷.SelectedIndex = -1;
            textBox_数量.Text = "";
            comboBox_単位.SelectedIndex = -1;
        }

        public void CopyInstance()
        {
            // インスタンスは新規に作成
            // 画面は、数量をクリア状態にする
            DouseiCargo = new DjDouseiCargo();
            textBox_数量.Text = "";
        }

        public void ReadOnly()
        {
            comboBox_積荷.Enabled = false;
            textBox_数量.ReadOnly = true;
            comboBox_単位.Enabled = false;

            label_1.Text = label_1.Text.Replace("※ ", "");
            label_1.Text = label_1.Text.Replace("　　", "");

            comboBox_積荷.Location = new Point(comboBox_積荷.Location.X + 14, comboBox_積荷.Location.Y);
            textBox_数量.Location = new Point(textBox_数量.Location.X + 14, textBox_数量.Location.Y);
            comboBox_単位.Location = new Point(comboBox_単位.Location.X + 14, comboBox_単位.Location.Y);
        }

        private void comboBox_積荷_Enter(object sender, EventArgs e)
        {
            if ((comboBox_積荷.Items == null || comboBox_積荷.Items.Count == 0) && MsCargo_list.Count > 0)
            {
                comboBox_積荷.Items.Clear();
                comboBox_積荷.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox_積荷.AutoCompleteSource = AutoCompleteSource.CustomSource;
                foreach (MsCargo cargo in MsCargo_list)
                {
                    comboBox_積荷.Items.Add(cargo);
                    comboBox_積荷.AutoCompleteCustomSource.Add(cargo.CargoName);
                }
            }
        }
    }
}
