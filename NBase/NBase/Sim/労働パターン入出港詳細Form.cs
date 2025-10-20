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

namespace Sim
{

    public partial class 労働パターン入出港詳細Form : Form
    {

        private int minSpan = 15; // １５分刻み
        private List<WorkContent> WorkContentList = null;

        private DateTime DefDateTime=DateTime.MinValue;

        private int EventKind = 0;
        private MsVessel Vessel = null;
        private MsSiShokumei Shokumei = null;
        private MsBasho Basho = null;
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


        public 労働パターン入出港詳細Form(List<WorkContent> WorkContentList, DateTime defdt)
        {
            InitializeComponent();

            this.WorkContentList = WorkContentList;

            this.WorkContentList.ForEach(o => { comboBox作業内容.Items.Add(new workContentItem(o.WorkContentID, o.Name)); });

            DefDateTime = defdt;
        }

        public void SetAppointment(労働パターンForm_Appointment ap,  MsSiShokumei shoku, MsBasho basho, MsVessel ves, int eventKind)
        {
            Appointment = ap;
            Shokumei = shoku;
            Basho = basho;
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

            int h1 = 0;
            if (ap.DateIDList[0].WorkDate.Hour < DefDateTime.Hour)
                h1 = -(DefDateTime.Hour - ap.DateIDList[0].WorkDate.Hour);
            else
                h1 = ap.DateIDList[0].WorkDate.Hour - DefDateTime.Hour + 1;

            comboBox2.SelectedItem = h1.ToString("").PadLeft(2, ' ');
            comboBox3.SelectedItem = ap.DateIDList[0].WorkDate.ToString("mm");

            DateTime last = ap.DateIDList[ap.DateIDList.Count - 1].WorkDate.AddMinutes(minSpan);
            int h2 = 0;
            if (last.Hour < DefDateTime.Hour)
                h2 = -(DefDateTime.Hour - last.Hour);
            else
                h2 = last.Hour - DefDateTime.Hour + 1;

            comboBox4.SelectedItem = h2.ToString("").PadLeft(2, ' ');
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

            int baseH = DefDateTime.Hour;

            int startDiff= int.Parse(comboBox2.SelectedItem as string);
            if (startDiff > 0) startDiff -= 1;
            int startM = int.Parse(comboBox3.SelectedItem as string);
            int endDiff = int.Parse(comboBox4.SelectedItem as string);
            if (endDiff > 0) endDiff -= 1;
            int endM = int.Parse(comboBox5.SelectedItem as string);

            DateTime dt1 = new DateTime(DefDateTime.Year, DefDateTime.Month, DefDateTime.Day, baseH + startDiff, startM,0);
            DateTime dt2 = new DateTime(DefDateTime.Year, DefDateTime.Month, DefDateTime.Day, baseH + endDiff, endM, 0);

            string gid = Appointment.DateIDList.Where(o => o.GID != null).Select(o => o.GID).Distinct().FirstOrDefault();

            string bashoId = Basho != null ? Basho.MsBashoId : null;

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
                pt.MsBashoID = bashoId;
                pt.MsSiShokuemiID = Shokumei.MsSiShokumeiID;
                pt.WorkDate = DateTime.MinValue;
                pt.WorkDateDiff = 0;
                if (wkdt < DefDateTime)
                {
                    TimeSpan diff = DefDateTime - wkdt;
                    pt.WorkDateDiff = -((int)diff.TotalMinutes / minSpan);
                }
                else
                {
                    TimeSpan diff = wkdt.AddMinutes(minSpan) - DefDateTime;
                    pt.WorkDateDiff = (int)diff.TotalMinutes / minSpan;
                }
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

            int startDiff = -1;
            int startM = -1;
            int endDiff = -1;
            int endM = -1;
            try
            {
                startDiff = int.Parse(comboBox2.SelectedItem as string);
                startM = int.Parse(comboBox3.SelectedItem as string);
                endDiff = int.Parse(comboBox4.SelectedItem as string);
                endM = int.Parse(comboBox5.SelectedItem as string);
            }
            catch (Exception e)
            {
                return false;
            }

            if (endDiff == 4 && endM != 0)
            {
                return false;
            }
            int baseH = DefDateTime.Hour;
            if (startDiff > 0) startDiff -= 1;
            if (endDiff > 0) endDiff -= 1;

            DateTime dt1 = new DateTime(DefDateTime.Year, DefDateTime.Month, DefDateTime.Day, baseH + startDiff, startM, 0);
            DateTime dt2 = new DateTime(DefDateTime.Year, DefDateTime.Month, DefDateTime.Day, baseH + endDiff, endM, 0);

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
