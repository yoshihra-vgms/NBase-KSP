using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WtmModelBase;
using NBaseData.DAC;

namespace NBaseMaster.WP
{

    public partial class 労働パターン詳細Form : Form
    {

        private int minSpan = 15; // １５分刻み
        private List<WorkContent> WorkContentList = null;

        //private DateTime DefDateTime=DateTime.MinValue;

        private int EventKind = 0;
        private NBaseData.DAC.MsVessel Vessel = null;
        private MsSiShokumei Shokumei = null;
        private 労働パターンForm_Appointment Appointment = null;


        private class workContentItem
        {
            public string id { set; get; }
            public string name { set; get; }

            public override string ToString()
            {
                return name;
            }

            public workContentItem(string id, string name)
            {
                this.id = id;
                this.name = name;
            }

        }


        //public 労働パターン詳細Form(List<WorkContent> WorkContentList, DateTime defdt)
        public 労働パターン詳細Form(List<WorkContent> WorkContentList)
        {
            InitializeComponent();

            this.WorkContentList = WorkContentList;

            this.WorkContentList.ForEach(o => { comboBox作業内容.Items.Add(new workContentItem(o.WorkContentID, o.Name)); });

            //DefDateTime = defdt;
        }


        public void SetAppointment(労働パターンForm_Appointment ap,  MsSiShokumei shoku, NBaseData.DAC.MsVessel ves, int eventKind)
        {
            Appointment = ap;
            Shokumei = shoku;
            Vessel = ves;
            EventKind = eventKind;

            //画面にセット
            label職名.Text = shoku.Name;

            foreach(workContentItem wc in comboBox作業内容.Items)
            {
                if (wc.id == ap.WorkContentID)
                {
                    comboBox作業内容.SelectedItem = wc;
                    break;
                }
            }

            //時間
            DateTime last = ap.DateIDList[ap.DateIDList.Count - 1].WorkDate.AddMinutes(minSpan);

            comboBox2.SelectedItem = ap.DateIDList[0].WorkDate.ToString("HH");
            comboBox3.SelectedItem = ap.DateIDList[0].WorkDate.ToString("mm");
            comboBox4.SelectedItem = last.ToString("HH");
            comboBox5.SelectedItem = last.ToString("mm");
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (!(comboBox作業内容.SelectedItem is workContentItem))
            {
                MessageBox.Show("作業内容を選択してください");
                return;
            }
            if (!CheckTimeValue())
            {
                MessageBox.Show("開始時刻 < 終了時刻となるように時間を設定してください");
                return;
            }

            int startH = int.Parse(comboBox2.SelectedItem as string);
            int startM = int.Parse(comboBox3.SelectedItem as string);
            int endH = int.Parse(comboBox4.SelectedItem as string);
            int endM = int.Parse(comboBox5.SelectedItem as string);

            DateTime dt1 = new DateTime(Appointment.WorkDay.Year, Appointment.WorkDay.Month, Appointment.WorkDay.Day, startH, startM, 0);
            DateTime dt2 = new DateTime(Appointment.WorkDay.Year, Appointment.WorkDay.Month, Appointment.WorkDay.Day, endH, endM, 0);
            if (endH == 0 && endM == 0)
            {
                dt2 = dt2.AddDays(1);
            }

            string gid = Appointment.DateIDList.Where(o => o.GID != null).Select(o => o.GID).Distinct().FirstOrDefault();

            //AppointmentからWorkPatternに変換する
            // 洗い替えするので一旦すべて削除とする
            List<WorkPattern> list = new List<WorkPattern>();

            for (int i = 0; i < Appointment.DateIDList.Count; i++)
            {
                WorkPattern pt = new WorkPattern();
                if (Appointment.DateIDList[i].WorkPatternID > 0)
                {
                    pt.WorkPatternID = Appointment.DateIDList[i].WorkPatternID;
                    pt.GID = gid;
                    pt.DeleteFlag = 1;
                    list.Add(pt);
                }
            }
            int idx = 0;
            for ( DateTime wkdt = dt1; wkdt < dt2; wkdt = wkdt.AddMinutes(15) )
            {
                WorkPattern pt = null;
                if (list.Count > idx)
                {
                    pt = list[idx];
                    pt.DeleteFlag = 0;
                }
                else
                {
                    pt = new WorkPattern();
                    pt.GID = gid;
                    list.Add(pt);
                }
                pt.EventKind = EventKind;
                pt.MsVesselID = Vessel.MsVesselID;
                pt.MsSiShokuemiID = Shokumei.MsSiShokumeiID;
                pt.WorkDate = wkdt;
                pt.WorkDateDiff = wkdt.Day - 1;
                pt.WorkContentID = (comboBox作業内容.SelectedItem as workContentItem).id;

                idx++;
            }


            string errstr = "";
           
            // 労働パターン登録
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (!(serviceClient.WorkPattern_InsertOrUpdate(NBaseCommon.Common.LoginUser, list)))
                {
                    errstr = "登録失敗" + "\n";
                }
            }
            if (errstr != "") MessageBox.Show(errstr);


            DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool CheckTimeValue()
        {
            if( !(comboBox作業内容.SelectedItem is workContentItem)) return false;

            if (!(comboBox2.SelectedItem is string)) return false;
            if (!(comboBox3.SelectedItem is string)) return false;
            if (!(comboBox4.SelectedItem is string)) return false;
            if (!(comboBox5.SelectedItem is string)) return false;

            int startH = -1;
            int startM = -1;
            int endH = -1;
            int endM = -1;
            try
            {
                startH = int.Parse(comboBox2.SelectedItem as string);
                startM = int.Parse(comboBox3.SelectedItem as string);
                endH = int.Parse(comboBox4.SelectedItem as string);
                endM = int.Parse(comboBox5.SelectedItem as string);
            }
            catch (Exception e)
            {
                return false;
            }

            DateTime dt1 = new DateTime(Appointment.WorkDay.Year, Appointment.WorkDay.Month, Appointment.WorkDay.Day, startH, startM, 0);
            DateTime dt2 = new DateTime(Appointment.WorkDay.Year, Appointment.WorkDay.Month, Appointment.WorkDay.Day, endH, endM, 0);
            if (endH == 0 && endM == 0)
            {
                dt2 = dt2.AddDays(1);
            }

            if ((dt2 - dt1).TotalMinutes < 0)
            {
                return false;
            }

            return true;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string errstr = "";

            // 労働パターン削除
            List<WorkPattern> list = new List<WorkPattern>();
            for (int i = 0; i < Appointment.DateIDList.Count; i++)
            {
                WorkPattern pt = new WorkPattern();
                if (Appointment.DateIDList[i].WorkPatternID > 0)
                {
                    pt.WorkPatternID = Appointment.DateIDList[i].WorkPatternID;
                    pt.GID = Appointment.DateIDList[i].GID;
                    pt.DeleteFlag = 1;
                    list.Add(pt);
                }
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {

                if (!(serviceClient.WorkPattern_InsertOrUpdate(NBaseCommon.Common.LoginUser, list)))
                {
                    errstr = "登録失敗" + "\n";
                }

            }
            if (errstr != "") MessageBox.Show(errstr);


            DialogResult = DialogResult.OK;
            this.Close();
        }



        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
