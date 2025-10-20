using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WingData.DAC;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dousei
{
    public partial class 動静詳細Form : Form
    {
        public DjDousei SelectedDousei;
        public string VesselName;
        public int MsVesselID;

        public delegate void ClearEventDelegate(DjDousei djDousei);

        private List<MsBasho> msBashos;
        private List<MsKichi> msKichis;
        private List<MsCargo> msCargos;

        #region プロパティ
        private Control.DouseiHeadControl douseiHeadControl1
        {
            get
            {
                return douseiPanel1.DouseiHeadControl1;
            }
        }
        private Control.DouseiHeadControl douseiHeadControl2
        {
            get
            {
                return douseiPanel1.DouseiHeadControl2;
            }
        }
        private Control.DouseiDetailControl douseiDetailControl1
        {
            get
            {
                return douseiPanel1.DouseiDetailControl1;
            }
        }
        private Control.DouseiDetailControl douseiDetailControl2
        {
            get
            {
                return douseiPanel1.DouseiDetailControl2;
            }
        }
        private Control.DouseiDetailControl douseiDetailControl3
        {
            get
            {
                return douseiPanel1.DouseiDetailControl3;
            }
        }
        private Control.DouseiDetailControl douseiDetailControl4
        {
            get
            {
                return douseiPanel1.DouseiDetailControl4;
            }
        }
        private Control.DouseiDetailControl douseiDetailControl5
        {
            get
            {
                return douseiPanel1.DouseiDetailControl5;
            }
        }
        #endregion

        public 動静詳細Form()
        {
            InitializeComponent();
        }

        private void 動静詳細Form_Load(object sender, EventArgs e)
        {
            VesselName_textBox.Text = VesselName;
            Init();
        }

        private void Init()
        {
            using (WingServiceReferences.WingServer.ServiceClient seriveClient = new WingServiceReferences.WingServer.ServiceClient())
            {
                msBashos = seriveClient.MsBasho_GetRecordsBy港(WingCommon.Common.LoginUser);
                msKichis = seriveClient.MsKiti_GetRecords(WingCommon.Common.LoginUser);
                msCargos = seriveClient.MsCargo_GetRecords(WingCommon.Common.LoginUser);
            }

            #region 積み１
            douseiHeadControl1.Init(msBashos, msKichis, msCargos);
            DjDousei tsumi1 = SelectedDousei.積み(1);
            douseiHeadControl1.djDousei = tsumi1;
            if (tsumi1 != null)
            {
                douseiHeadControl1.DouseDate = tsumi1.DouseiDate;
                douseiHeadControl1.MsBashoID = tsumi1.MsBashoID;
                douseiHeadControl1.MsKichiID = tsumi1.MsKichiID;
                if (tsumi1.DjDouseiCargos.Count > 0)
                {
                    douseiHeadControl1.Cargo1ID = tsumi1.DjDouseiCargos[0].MsCargoID;
                    douseiHeadControl1.Qtty1 = tsumi1.DjDouseiCargos[0].Qtty;
                }
                if (tsumi1.DjDouseiCargos.Count > 1)
                {
                    douseiHeadControl1.Cargo2ID = tsumi1.DjDouseiCargos[1].MsCargoID;
                    douseiHeadControl1.Qtty2 = tsumi1.DjDouseiCargos[1].Qtty;
                }
                douseiHeadControl1.入港時間 = tsumi1.入港時間;
                douseiHeadControl1.着桟時間 = tsumi1.着桟時間;
                douseiHeadControl1.荷役開始 = tsumi1.荷役開始;
                douseiHeadControl1.荷役終了 = tsumi1.荷役終了;
                douseiHeadControl1.離桟時間 = tsumi1.離桟時間;
                douseiHeadControl1.出港時間 = tsumi1.出港時間;

                if (tsumi1.VoyageNo.Length > 0)
                {
                    douseiHeadControl1.VoaygeNo = tsumi1.VoyageNo;
                }
            }
            douseiHeadControl1.ClearEventHandler += new ClearEventDelegate(ClearEvent);
            #endregion

            #region 積み２
            douseiHeadControl2.Init(msBashos, msKichis, msCargos);
            DjDousei tsumi2 = SelectedDousei.積み(2);
            douseiHeadControl2.djDousei = tsumi2;
            if (tsumi2 != null)
            {
                douseiHeadControl2.DouseDate = tsumi2.DouseiDate;
                douseiHeadControl2.MsBashoID = tsumi2.MsBashoID;
                douseiHeadControl2.MsKichiID = tsumi2.MsKichiID;
                if (tsumi2.DjDouseiCargos.Count > 0)
                {
                    douseiHeadControl2.Cargo1ID = tsumi2.DjDouseiCargos[0].MsCargoID;
                    douseiHeadControl2.Qtty1 = tsumi2.DjDouseiCargos[0].Qtty;
                }
                if (tsumi2.DjDouseiCargos.Count > 1)
                {
                    douseiHeadControl2.Cargo2ID = tsumi2.DjDouseiCargos[1].MsCargoID;
                    douseiHeadControl2.Qtty2 = tsumi2.DjDouseiCargos[1].Qtty;
                }
                douseiHeadControl2.入港時間 = tsumi2.入港時間;
                douseiHeadControl2.着桟時間 = tsumi2.着桟時間;
                douseiHeadControl2.荷役開始 = tsumi2.荷役開始;
                douseiHeadControl2.荷役終了 = tsumi2.荷役終了;
                douseiHeadControl2.離桟時間 = tsumi2.離桟時間;
                douseiHeadControl2.出港時間 = tsumi2.出港時間;

                if (tsumi2.VoyageNo.Length > 0)
                {
                    douseiHeadControl2.VoaygeNo = tsumi2.VoyageNo;
                }

            }
            douseiHeadControl2.ClearEventHandler += new ClearEventDelegate(ClearEvent);

            #endregion

            #region 揚げ１
            douseiDetailControl1.Init(msBashos, msKichis, msCargos);
            DjDousei age1 = SelectedDousei.揚げ(1);
            douseiDetailControl1.djDousei = age1;
            if (age1 != null)
            {
                douseiDetailControl1.DouseDate = age1.DouseiDate;
                douseiDetailControl1.MsBashoID = age1.MsBashoID;
                douseiDetailControl1.MsKichiID = age1.MsKichiID;
                if (age1.DjDouseiCargos.Count > 0)
                {
                    douseiDetailControl1.Cargo1ID = age1.DjDouseiCargos[0].MsCargoID;
                    douseiDetailControl1.Qtty1 = age1.DjDouseiCargos[0].Qtty;
                }
                if (age1.DjDouseiCargos.Count > 1)
                {
                    douseiDetailControl1.Cargo2ID = age1.DjDouseiCargos[1].MsCargoID;
                    douseiDetailControl1.Qtty2 = age1.DjDouseiCargos[1].Qtty;
                }
                douseiDetailControl1.入港時間 = age1.入港時間;
                douseiDetailControl1.着桟時間 = age1.着桟時間;
                douseiDetailControl1.荷役開始 = age1.荷役開始;
                douseiDetailControl1.荷役終了 = age1.荷役終了;
                douseiDetailControl1.離桟時間 = age1.離桟時間;
                douseiDetailControl1.出港時間 = age1.出港時間;

                if (age1.VoyageNo.Length > 0)
                {
                    douseiDetailControl1.VoaygeNo = age1.VoyageNo;
                }
            }
            douseiDetailControl1.ClearEventHandler += new ClearEventDelegate(ClearEvent);
            #endregion

            #region 揚げ２
            douseiDetailControl2.Init(msBashos, msKichis, msCargos);
            DjDousei age2 = SelectedDousei.揚げ(2);
            douseiDetailControl2.djDousei = age2;
            if (age2 != null)
            {
                douseiDetailControl2.DouseDate = age2.DouseiDate;
                douseiDetailControl2.MsBashoID = age2.MsBashoID;
                douseiDetailControl2.MsKichiID = age2.MsKichiID;
                if (age2.DjDouseiCargos.Count > 0)
                {
                    douseiDetailControl2.Cargo1ID = age2.DjDouseiCargos[0].MsCargoID;
                    douseiDetailControl2.Qtty1 = age2.DjDouseiCargos[0].Qtty;
                }
                if (age2.DjDouseiCargos.Count > 1)
                {
                    douseiDetailControl2.Cargo2ID = age2.DjDouseiCargos[1].MsCargoID;
                    douseiDetailControl2.Qtty2 = age2.DjDouseiCargos[1].Qtty;
                }
                douseiDetailControl2.入港時間 = age2.入港時間;
                douseiDetailControl2.着桟時間 = age2.着桟時間;
                douseiDetailControl2.荷役開始 = age2.荷役開始;
                douseiDetailControl2.荷役終了 = age2.荷役終了;
                douseiDetailControl2.離桟時間 = age2.離桟時間;
                douseiDetailControl2.出港時間 = age2.出港時間;

                if (age2.VoyageNo.Length > 0)
                {
                    douseiDetailControl2.VoaygeNo = age2.VoyageNo;
                }
            }
            douseiDetailControl2.ClearEventHandler += new ClearEventDelegate(ClearEvent);
            #endregion

            #region 揚げ３
            douseiDetailControl3.Init(msBashos, msKichis, msCargos);
            DjDousei age3 = SelectedDousei.揚げ(3);
            douseiDetailControl3.djDousei = age3;
            if (age3 != null)
            {
                douseiDetailControl3.DouseDate = age3.DouseiDate;
                douseiDetailControl3.MsBashoID = age3.MsBashoID;
                douseiDetailControl3.MsKichiID = age3.MsKichiID;
                if (age3.DjDouseiCargos.Count > 0)
                {
                    douseiDetailControl3.Cargo1ID = age3.DjDouseiCargos[0].MsCargoID;
                    douseiDetailControl3.Qtty1 = age3.DjDouseiCargos[0].Qtty;
                }
                if (age3.DjDouseiCargos.Count > 1)
                {
                    douseiDetailControl3.Cargo2ID = age3.DjDouseiCargos[1].MsCargoID;
                    douseiDetailControl3.Qtty2 = age3.DjDouseiCargos[1].Qtty;
                }
                douseiDetailControl3.入港時間 = age3.入港時間;
                douseiDetailControl3.着桟時間 = age3.着桟時間;
                douseiDetailControl3.荷役開始 = age3.荷役開始;
                douseiDetailControl3.荷役終了 = age3.荷役終了;
                douseiDetailControl3.離桟時間 = age3.離桟時間;
                douseiDetailControl3.出港時間 = age3.出港時間;

                if (age3.VoyageNo.Length > 0)
                {
                    douseiDetailControl3.VoaygeNo = age3.VoyageNo;
                }
            }
            douseiDetailControl3.ClearEventHandler += new ClearEventDelegate(ClearEvent);
            #endregion

            #region 揚げ４
            douseiDetailControl4.Init(msBashos, msKichis, msCargos);
            DjDousei age4 = SelectedDousei.揚げ(4);
            douseiDetailControl4.djDousei = age4;
            if (age4 != null)
            {
                douseiDetailControl4.DouseDate = age4.DouseiDate;
                douseiDetailControl4.MsBashoID = age4.MsBashoID;
                douseiDetailControl4.MsKichiID = age4.MsKichiID;
                if (age4.DjDouseiCargos.Count > 0)
                {
                    douseiDetailControl4.Cargo1ID = age4.DjDouseiCargos[0].MsCargoID;
                    douseiDetailControl4.Qtty1 = age4.DjDouseiCargos[0].Qtty;
                }
                if (age4.DjDouseiCargos.Count > 1)
                {
                    douseiDetailControl4.Cargo2ID = age4.DjDouseiCargos[1].MsCargoID;
                    douseiDetailControl4.Qtty2 = age4.DjDouseiCargos[1].Qtty;
                }
                douseiDetailControl4.入港時間 = age4.入港時間;
                douseiDetailControl4.着桟時間 = age4.着桟時間;
                douseiDetailControl4.荷役開始 = age4.荷役開始;
                douseiDetailControl4.荷役終了 = age4.荷役終了;
                douseiDetailControl4.離桟時間 = age4.離桟時間;
                douseiDetailControl4.出港時間 = age4.出港時間;

                if (age4.VoyageNo.Length > 0)
                {
                    douseiDetailControl4.VoaygeNo = age4.VoyageNo;
                }
            }
            douseiDetailControl4.ClearEventHandler += new ClearEventDelegate(ClearEvent);
            #endregion

            #region 揚げ５
            douseiDetailControl5.Init(msBashos, msKichis, msCargos);
            DjDousei age5 = SelectedDousei.揚げ(5);
            douseiDetailControl5.djDousei = age5;
            if (age5 != null)
            {
                douseiDetailControl5.DouseDate = age5.DouseiDate;
                douseiDetailControl5.MsBashoID = age5.MsBashoID;
                douseiDetailControl5.MsKichiID = age5.MsKichiID;
                if (age5.DjDouseiCargos.Count > 0)
                {
                    douseiDetailControl5.Cargo1ID = age5.DjDouseiCargos[0].MsCargoID;
                    douseiDetailControl5.Qtty1 = age5.DjDouseiCargos[0].Qtty;
                }
                if (age5.DjDouseiCargos.Count > 1)
                {
                    douseiDetailControl5.Cargo2ID = age5.DjDouseiCargos[1].MsCargoID;
                    douseiDetailControl5.Qtty2 = age5.DjDouseiCargos[1].Qtty;
                }
                douseiDetailControl5.入港時間 = age5.入港時間;
                douseiDetailControl5.着桟時間 = age5.着桟時間;
                douseiDetailControl5.荷役開始 = age5.荷役開始;
                douseiDetailControl5.荷役終了 = age5.荷役終了;
                douseiDetailControl5.離桟時間 = age5.離桟時間;
                douseiDetailControl5.出港時間 = age5.出港時間;

                if (age5.VoyageNo.Length > 0)
                {
                    douseiDetailControl5.VoaygeNo = age5.VoyageNo;
                }
            }
            douseiDetailControl5.ClearEventHandler += new ClearEventDelegate(ClearEvent);
            #endregion

            DateTime start = DateTime.Now;

            douseiHeadControl1.Visible = true;
            douseiHeadControl2.Visible = true;
            douseiDetailControl1.Visible = true;
            douseiDetailControl2.Visible = true;
            douseiDetailControl3.Visible = true;
            douseiDetailControl4.Visible = true;
            douseiDetailControl5.Visible = true;

            Console.WriteLine("9:{0}", DateTime.Now - start);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_button_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (バリデーション() == false)
                {
                    return;
                }
                DjDousei tsumi1, tsumi2;
                DjDousei age1, age2, age3, age4, age5;

                tsumi1 = null;
                tsumi2 = null;
                age1 = null;
                age2 = null;
                age3 = null;
                age4 = null;
                age5 = null;

                List<DjDousei> CheckBase;
                using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
                {
                    CheckBase = serviceClient.DjDousei_GetRecords(WingCommon.Common.LoginUser, MsVesselID);
                }

                #region 積み１
                if (douseiHeadControl1.DouseDate != DateTime.MinValue)
                {
                    tsumi1 = douseiHeadControl1.djDousei;
                    if (tsumi1 == null)
                    {
                        tsumi1 = new DjDousei();
                        tsumi1.DjDouseiID = Guid.NewGuid().ToString();
                        tsumi1.MsVesselID = MsVesselID;
                    }

                    tsumi1.DouseiDate = douseiHeadControl1.DouseDate;
                    tsumi1.MsKanidouseiInfoShubetuID = douseiHeadControl1.MsKanidouseiInfoShubetuID;
                    tsumi1.MsBashoID = douseiHeadControl1.MsBashoID;
                    tsumi1.MsKichiID = douseiHeadControl1.MsKichiID;
                    tsumi1.入港時間 = douseiHeadControl1.入港時間;
                    tsumi1.着桟時間 = douseiHeadControl1.着桟時間;
                    tsumi1.荷役開始 = douseiHeadControl1.荷役開始;
                    tsumi1.荷役終了 = douseiHeadControl1.荷役終了;
                    tsumi1.離桟時間 = douseiHeadControl1.離桟時間;
                    tsumi1.出港時間 = douseiHeadControl1.出港時間;
                    tsumi1.VoyageNo = douseiHeadControl1.VoaygeNo;
                    tsumi1.DjDouseiCargos.Clear();
                    DjDouseiCargo cargo1 = new DjDouseiCargo();
                    cargo1.DjDouseiID = tsumi1.DjDouseiID;
                    cargo1.MsCargoID = douseiHeadControl1.Cargo1ID;
                    cargo1.Qtty = douseiHeadControl1.Qtty1;
                    tsumi1.DjDouseiCargos.Add(cargo1);
                    if (douseiHeadControl1.Cargo2ID > int.MinValue)
                    {
                        DjDouseiCargo cargo2 = new DjDouseiCargo();
                        cargo2.DjDouseiID = tsumi1.DjDouseiID;
                        cargo2.MsCargoID = douseiHeadControl1.Cargo2ID;
                        cargo2.Qtty = douseiHeadControl1.Qtty2;
                        tsumi1.DjDouseiCargos.Add(cargo2);
                    }

                    #region バリデーション
                    if (GetVoyageCount(CheckBase, tsumi1,MsKanidouseiInfoShubetu.積みID) >= 2)
                    {
                        MessageBox.Show("指定した次航海番号の積みが２港以上です(次航海番号=" + tsumi1.VoyageNo + ")", "動静詳細From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AddVoyage(CheckBase, tsumi1);
                    #endregion
                }
                #endregion

                #region 積み２

                if (douseiHeadControl2.DouseDate != DateTime.MinValue)
                {
                    tsumi2 = douseiHeadControl2.djDousei;

                    if (tsumi2 == null)
                    {
                        tsumi2 = new DjDousei();
                        tsumi2.DjDouseiID = Guid.NewGuid().ToString();
                        tsumi2.MsVesselID = MsVesselID;
                    }

                    tsumi2.DouseiDate = douseiHeadControl2.DouseDate;
                    tsumi2.MsKanidouseiInfoShubetuID = douseiHeadControl2.MsKanidouseiInfoShubetuID;
                    tsumi2.MsBashoID = douseiHeadControl2.MsBashoID;
                    tsumi2.MsKichiID = douseiHeadControl2.MsKichiID;
                    tsumi2.入港時間 = douseiHeadControl2.入港時間;
                    tsumi2.着桟時間 = douseiHeadControl2.着桟時間;
                    tsumi2.荷役開始 = douseiHeadControl2.荷役開始;
                    tsumi2.荷役終了 = douseiHeadControl2.荷役終了;
                    tsumi2.離桟時間 = douseiHeadControl2.離桟時間;
                    tsumi2.出港時間 = douseiHeadControl2.出港時間;
                    tsumi2.VoyageNo = douseiHeadControl2.VoaygeNo;
                    tsumi2.DjDouseiCargos.Clear();
                    DjDouseiCargo cargo1 = new DjDouseiCargo();
                    cargo1.DjDouseiID = tsumi2.DjDouseiID;
                    cargo1.MsCargoID = douseiHeadControl2.Cargo1ID;
                    cargo1.Qtty = douseiHeadControl2.Qtty1;
                    tsumi2.DjDouseiCargos.Add(cargo1);
                    if (douseiHeadControl2.Cargo2ID > int.MinValue)
                    {
                        DjDouseiCargo cargo2 = new DjDouseiCargo();
                        cargo2.DjDouseiID = tsumi2.DjDouseiID;
                        cargo2.MsCargoID = douseiHeadControl2.Cargo2ID;
                        cargo2.Qtty = douseiHeadControl2.Qtty2;
                        tsumi2.DjDouseiCargos.Add(cargo2);
                    }
                    #region バリデーション
                    if (GetVoyageCount(CheckBase, tsumi2, MsKanidouseiInfoShubetu.積みID) >= 2)
                    {
                        MessageBox.Show("指定した次航海番号の積みが２港以上です(次航海番号=" + tsumi2.VoyageNo + ")", "動静詳細From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AddVoyage(CheckBase, tsumi2);
                    #endregion
                }

                #endregion

                #region 揚げ１

                if (douseiDetailControl1.DouseDate != DateTime.MinValue)
                {
                    age1 = douseiDetailControl1.djDousei;

                    if (SetDouseiDetailControl(ref age1, douseiDetailControl1, CheckBase) == false)
                    {
                        return;
                    }
                }

                #endregion

                #region 揚げ２

                if (douseiDetailControl2.DouseDate != DateTime.MinValue)
                {
                    age2 = douseiDetailControl2.djDousei;

                    if (SetDouseiDetailControl(ref age2, douseiDetailControl2, CheckBase) == false)
                    {
                        return;
                    }
                }

                #endregion

                #region 揚げ３

                if (douseiDetailControl3.DouseDate != DateTime.MinValue)
                {
                    age3 = douseiDetailControl3.djDousei;

                    if (SetDouseiDetailControl(ref age3, douseiDetailControl3,CheckBase) == false)
                    {
                        return;
                    }
                }

                #endregion

                #region 揚げ４

                if (douseiDetailControl4.DouseDate != DateTime.MinValue)
                {
                    age4 = douseiDetailControl4.djDousei;

                    if (SetDouseiDetailControl(ref age4, douseiDetailControl4,CheckBase) == false)
                    {
                        return;
                    }
                }

                #endregion

                #region 揚げ５

                if (douseiDetailControl5.DouseDate != DateTime.MinValue)
                {
                    age5 = douseiDetailControl5.djDousei;

                    if (SetDouseiDetailControl(ref age5, douseiDetailControl5,CheckBase) == false)
                    {
                        return;
                    }
                }

                #endregion

                if (tsumi1 != null)
                {
                    SelectedDousei.DjDouseis.Add(tsumi1);
                }
                if (tsumi2 != null)
                {
                    SelectedDousei.DjDouseis.Add(tsumi2);
                }
                if (age1 != null)
                {
                    SelectedDousei.DjDouseis.Add(age1);
                }
                if (age2 != null)
                {
                    SelectedDousei.DjDouseis.Add(age2);
                }
                if (age3 != null)
                {
                    SelectedDousei.DjDouseis.Add(age3);
                }
                if (age4 != null)
                {
                    SelectedDousei.DjDouseis.Add(age4);
                }
                if (age5 != null)
                {
                    SelectedDousei.DjDouseis.Add(age5);
                }
                #region DB更新
                using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
                {
                    serviceClient.DjDousei_UpdateRecord(WingCommon.Common.LoginUser, SelectedDousei);
                }
                #endregion

                DialogResult = DialogResult.OK;
                Close();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private int GetVoyageCount(List<DjDousei> CheckBase, DjDousei dousei, string MsKanidouseiInfoShubetuID)
        {
            var CheckData = from p in CheckBase
                            where p.DjDouseiID != dousei.DjDouseiID
                            && p.VoyageNo == dousei.VoyageNo
                            && p.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetuID
                            select p;

            if (CheckData == null)
                return 0;

            int cc = CheckData.Count<DjDousei>();
            return cc;
        }

        private void AddVoyage(List<DjDousei> CheckBase, DjDousei dousei)
        {
            var CheckData = from p in CheckBase
                            where p.DjDouseiID == dousei.DjDouseiID
                            select p;
            if (CheckData.Count<DjDousei>() != 0)
            {
                foreach (DjDousei d in CheckBase)
                {
                    if (d.DjDouseiID == dousei.DjDouseiID)
                    {
                        CheckBase.Remove(d);
                        break;
                    }
                }
            }
            CheckBase.Add(dousei);
        }

        private bool SetDouseiDetailControl(ref DjDousei age1, Control.DouseiDetailControl douseiDetailControl, List<DjDousei> CheckBase)
        {
            if (age1 == null)
            {
                age1 = new DjDousei();
                age1.DjDouseiID = Guid.NewGuid().ToString();
                age1.MsVesselID = MsVesselID;
                age1.VoyageNo = douseiDetailControl.VoaygeNo;
            }

            age1.DouseiDate = douseiDetailControl.DouseDate;
            age1.MsKanidouseiInfoShubetuID = douseiDetailControl.MsKanidouseiInfoShubetuID;
            age1.MsBashoID = douseiDetailControl.MsBashoID;
            age1.MsKichiID = douseiDetailControl.MsKichiID;
            age1.入港時間 = douseiDetailControl.入港時間;
            age1.着桟時間 = douseiDetailControl.着桟時間;
            age1.荷役開始 = douseiDetailControl.荷役開始;
            age1.荷役終了 = douseiDetailControl.荷役終了;
            age1.離桟時間 = douseiDetailControl.離桟時間;
            age1.出港時間 = douseiDetailControl.出港時間;
            age1.VoyageNo = douseiDetailControl.VoaygeNo;
            age1.DjDouseiCargos.Clear();
            DjDouseiCargo cargo1 = new DjDouseiCargo();
            cargo1.DjDouseiID = age1.DjDouseiID;
            cargo1.MsCargoID = douseiDetailControl.Cargo1ID;
            cargo1.Qtty = douseiDetailControl.Qtty1;
            age1.DjDouseiCargos.Add(cargo1);
            if (douseiDetailControl.Cargo2ID > int.MinValue)
            {
                DjDouseiCargo cargo2 = new DjDouseiCargo();
                cargo2.DjDouseiID = age1.DjDouseiID;
                cargo2.MsCargoID = douseiDetailControl.Cargo2ID;
                cargo2.Qtty = douseiDetailControl.Qtty2;
                age1.DjDouseiCargos.Add(cargo2);
            }
            #region バリデーション
            if (GetVoyageCount(CheckBase, age1, MsKanidouseiInfoShubetu.揚げID) >= 5)
            {
                MessageBox.Show("指定した次航海番号の揚げが５港以上です(次航海番号=" + age1.VoyageNo + ")", "動静詳細From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            AddVoyage(CheckBase, age1);
            #endregion

            return true;
        }

        private bool バリデーション()
        {

            if (積みバリデーション(douseiHeadControl1, "積み１") == false)
            {
                return false;
            }

            if (積みバリデーション(douseiHeadControl2, "積み２") == false)
            {
                return false;
            }

            if (揚げバリデーション(douseiDetailControl1,"揚げ１") == false)
            {
                return false;
            }

            if (揚げバリデーション(douseiDetailControl2, "揚げ２") == false)
            {
                return false;
            }
            if (揚げバリデーション(douseiDetailControl3, "揚げ３") == false)
            {
                return false;
            }
            if (揚げバリデーション(douseiDetailControl4, "揚げ４") == false)
            {
                return false;
            }
            if (揚げバリデーション(douseiDetailControl5, "揚げ５") == false)
            {
                return false;
            }

            return true;
        }

        private bool 積みバリデーション(Control.DouseiHeadControl douseiHeadControl, string Title)
        {
            if (douseiHeadControl.DouseDate != DateTime.MinValue)
            {
                if (douseiHeadControl.VoaygeNo.Length == 0)
                {
                    MessageBox.Show(Title + "の次航海番号を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (douseiHeadControl.MsKanidouseiInfoShubetuID == "")
                {
                    MessageBox.Show(Title + "の区分を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (douseiHeadControl.MsBashoID == "")
                {
                    MessageBox.Show(Title + "の港を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (douseiHeadControl.Cargo1ID == int.MinValue)
                {
                    MessageBox.Show(Title + "の貨物1を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (douseiHeadControl.Qtty1 == decimal.MinValue)
                {
                    MessageBox.Show(Title + "の数量1を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (WingCommon.Number.CheckValue((double)douseiHeadControl.Qtty1, 4, 3) == false)
                {
                    MessageBox.Show(Title + "の数量1を正しく入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (douseiHeadControl.Cargo2ID != int.MinValue)
                {
                    if (douseiHeadControl.Qtty2 == decimal.MinValue)
                    {
                        MessageBox.Show(Title + "の数量2を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (WingCommon.Number.CheckValue((double)douseiHeadControl.Qtty2, 4, 3) == false)
                    {
                        MessageBox.Show(Title + "の数量2を正しく入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                #region 時間
                if (douseiHeadControl.入港時間 != "")
                {
                    if (douseiHeadControl.入港時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の入港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiHeadControl.入港時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の入港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiHeadControl.入港時間) == false)
                    {
                        MessageBox.Show(Title + "の入港時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiHeadControl.着桟時間 != "")
                {
                    if (douseiHeadControl.着桟時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の着桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiHeadControl.着桟時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の着桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiHeadControl.着桟時間) == false)
                    {
                        MessageBox.Show(Title + "の着桟時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiHeadControl.荷役開始 != "")
                {
                    if (douseiHeadControl.荷役開始.Length != 4)
                    {
                        MessageBox.Show(Title + "の荷役開始は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiHeadControl.荷役開始, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の荷役開始は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiHeadControl.荷役開始) == false)
                    {
                        MessageBox.Show(Title + "の荷役開始が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiHeadControl.荷役終了 != "")
                {
                    if (douseiHeadControl.荷役終了.Length != 4)
                    {
                        MessageBox.Show(Title + "の荷役終了は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiHeadControl.荷役終了, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の荷役終了は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiHeadControl.荷役終了) == false)
                    {
                        MessageBox.Show(Title + "の荷役終了が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiHeadControl.離桟時間 != "")
                {
                    if (douseiHeadControl.離桟時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の離桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiHeadControl.離桟時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の離桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiHeadControl.離桟時間) == false)
                    {
                        MessageBox.Show(Title + "の離桟時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiHeadControl.出港時間 != "")
                {
                    if (douseiHeadControl.出港時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の出港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiHeadControl.出港時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の出港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiHeadControl.出港時間) == false)
                    {
                        MessageBox.Show(Title + "の出港時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                #endregion
            }

            return true;
        }

        private bool 揚げバリデーション(Control.DouseiDetailControl douseiDetailControl,string Title)
        {
            if (douseiDetailControl.DouseDate != DateTime.MinValue)
            {
                if (douseiDetailControl.VoaygeNo.Length == 0)
                {
                    MessageBox.Show(Title + "の次航海番号を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (douseiDetailControl.MsKanidouseiInfoShubetuID == "")
                {
                    MessageBox.Show(Title + "の区分を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (douseiDetailControl.MsBashoID == "")
                {
                    MessageBox.Show(Title + "の港を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (douseiDetailControl.Cargo1ID == int.MinValue)
                {
                    MessageBox.Show(Title + "の貨物1を選択して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (douseiDetailControl.Qtty1 == decimal.MinValue)
                {
                    MessageBox.Show(Title + "の数量1を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (WingCommon.Number.CheckValue((double)douseiDetailControl.Qtty1, 4, 3) == false)
                {
                    MessageBox.Show(Title + "の数量1を正しく入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (douseiDetailControl.Cargo2ID != int.MinValue)
                {
                    if (douseiDetailControl.Qtty2 == decimal.MinValue)
                    {
                        MessageBox.Show(Title + "の数量2を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (WingCommon.Number.CheckValue((double)douseiDetailControl.Qtty2, 4, 3) == false)
                    {
                        MessageBox.Show(Title + "の数量2を正しく入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                #region 時間
                if (douseiDetailControl.入港時間 != "")
                {
                    if (douseiDetailControl.入港時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の入港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiDetailControl.入港時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の入港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiDetailControl.入港時間) == false)
                    {
                        MessageBox.Show(Title + "の入港時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiDetailControl.着桟時間 != "")
                {
                    if (douseiDetailControl.着桟時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の着桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiDetailControl.着桟時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の着桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiDetailControl.着桟時間) == false)
                    {
                        MessageBox.Show(Title + "の着桟時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiDetailControl.荷役開始 != "")
                {
                    if (douseiDetailControl.荷役開始.Length != 4)
                    {
                        MessageBox.Show(Title + "の荷役開始は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiDetailControl.荷役開始, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の荷役開始は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiDetailControl.荷役開始) == false)
                    {
                        MessageBox.Show(Title + "の荷役開始が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiDetailControl.荷役終了 != "")
                {
                    if (douseiDetailControl.荷役終了.Length != 4)
                    {
                        MessageBox.Show(Title + "の荷役終了は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiDetailControl.荷役終了, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の荷役終了は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiDetailControl.荷役終了) == false)
                    {
                        MessageBox.Show(Title + "の荷役終了が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiDetailControl.離桟時間 != "")
                {
                    if (douseiDetailControl.離桟時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の離桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiDetailControl.離桟時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の離桟時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiDetailControl.離桟時間) == false)
                    {
                        MessageBox.Show(Title + "の離桟時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (douseiDetailControl.出港時間 != "")
                {
                    if (douseiDetailControl.出港時間.Length != 4)
                    {
                        MessageBox.Show(Title + "の出港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Regex.IsMatch(douseiDetailControl.出港時間, "\\d{4}") == false)
                    {
                        MessageBox.Show(Title + "の出港時間は4桁を入力して下さい", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (TimeCheck(douseiDetailControl.出港時間) == false)
                    {
                        MessageBox.Show(Title + "の出港時間が不正です", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                #endregion

            }

            return true;
        }

        private bool TimeCheck(string TIME)
        {
            try
            {
                int m = int.Parse(TIME.Substring(2, 2));

                if (m >= 60)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 各動静が削除された時の処理
        /// 論理削除フラグを立て、動静情報（表示）をクリアする
        /// </summary>
        /// <param name="ClearDouse"></param>
        public void ClearEvent(DjDousei ClearDouse)
        {
            if (ClearDouse == null)
            {
                return;
            }

            if (ClearDouse.DjDouseiID != "")
            {
                foreach (DjDousei dj in SelectedDousei.DjDouseis)
                {
                    if (dj.DjDouseiID == ClearDouse.DjDouseiID)
                    {
                        dj.DeleteFlag = 1;
                        if (douseiHeadControl1.djDousei != null && douseiHeadControl1.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiHeadControl1.djDousei = null;
                        }
                        if (douseiHeadControl2.djDousei != null && douseiHeadControl2.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiHeadControl2.djDousei = null;
                        }

                        if (douseiDetailControl1.djDousei != null && douseiDetailControl1.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiDetailControl1.djDousei = null;
                        }
                        if (douseiDetailControl2.djDousei != null && douseiDetailControl2.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiDetailControl2.djDousei = null;
                        }
                        if (douseiDetailControl3.djDousei != null && douseiDetailControl3.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiDetailControl3.djDousei = null;
                        }
                        if (douseiDetailControl4.djDousei != null && douseiDetailControl4.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiDetailControl4.djDousei = null;
                        }
                        if (douseiDetailControl5.djDousei != null && douseiDetailControl5.djDousei.DjDouseiID == dj.DjDouseiID)
                        {
                            douseiDetailControl5.djDousei = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除してもよろしいですか？","削除",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DjDousei djDousei in SelectedDousei.DjDouseis)
                {
                    djDousei.DeleteFlag = 1;
                }

                using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
                {
                    serviceClient.DjDousei_UpdateRecord(WingCommon.Common.LoginUser, SelectedDousei);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("閉じてもよろしいですか？", "閉じる", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
