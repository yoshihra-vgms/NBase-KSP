

using NBaseData.BLC;
using NBaseData.DAC;
using NBaseData.DS;
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
using WtmData;

namespace WTM
{
    public partial class 出退勤登録Form : Form
    {
        class CrewStatus
        {
            public int SeninId { set; get; }

            public bool BeinWork { set; get; }
        }

        public MsVessel Vessel { set; get; }


        public int SeninId { set; get; }

        public WtmModelBase.Work BeInWork { set; get; }


        private DateTime TODAY = DateTime.MinValue;
        private DateTime preDate = DateTime.MinValue;

        private DateTime startDate = DateTime.MinValue;


        private Dictionary<Button, CrewStatus> ButtonDic { set; get; }

        private List<Color> ButtonColors { set; get; }


        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 出退勤登録Form instance = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 出退勤登録Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 出退勤登録Form();
            }
            return instance;
        }

        public 出退勤登録Form()
        {
            InitializeComponent();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 出退勤登録Form_Load(object sender, EventArgs e)
        {
            ButtonColors = new List<Color>();
            ButtonColors.Add(Color.Orange);
            ButtonColors.Add(Color.MidnightBlue);
            ButtonColors.Add(Color.SlateBlue);
            ButtonColors.Add(Color.HotPink);


            TODAY = DateTime.Today;
            startDate = TODAY.Date;

            label_Vessel.Text = Vessel.VesselName;
            label日付.Text = TODAY.ToString("yyyy年MM月dd日（ddd）");

            MakePanel();

            buttonStartWork.Enabled = false;
            buttonFinishWork.Enabled = false;

            timer1.Start();
        }

        private void 出退勤登録Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void MakePanel()
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel1.SuspendLayout();

            this.SuspendLayout();

            flowLayoutPanel1.Controls.Clear();


            WtmModelBase.Role role = null;
            //if (Common.Senin != null)
            //{
            //    role = WtmCommon.RoleList.Where(o => o.Rank == Common.Senin.MsSiShokumeiID.ToString()).FirstOrDefault();
            //}
            if (NBaseCommon.Common.siCard != null)
            {
                string shokumeiId = "";
                if (NBaseCommon.Common.siCard.SiLinkShokumeiCards != null && NBaseCommon.Common.siCard.SiLinkShokumeiCards.Count > 0)
                {
                    shokumeiId = NBaseCommon.Common.siCard.SiLinkShokumeiCards[0].MsSiShokumeiID.ToString();
                }
                role = WtmCommon.RoleList.Where(o => o.Rank == shokumeiId).FirstOrDefault();
            }

            ButtonDic = new Dictionary<Button, CrewStatus>();

            // 乗船者検索
            var cards = Common.GetOnSigner(Vessel.MsVesselID, TODAY, TODAY);

            //List<int> departments = new List<int>() { 0, 2, 1, 4 };
            //foreach (int dep in departments)
            //{
                foreach (MsSiShokumei shokumei in NBaseCommon.Common.ShokumeiList)
                {
                    if (cards.Any(o => o.SeninMsSiShokumeiID == shokumei.MsSiShokumeiID))
                    {
                        var shokumeiCards = cards.Where(o => o.SeninMsSiShokumeiID == shokumei.MsSiShokumeiID).OrderBy(o => o.SeninName);
                        foreach (SiCard card in shokumeiCards)
                        {
                            if (Common.Senin != null)
                            {
                                if (card.MsSeninID != Common.Senin.MsSeninID)
                                {
                                    if (role == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (role.RankList.Any(o => o == shokumei.MsSiShokumeiID) == false)
                                            continue;
                                    }
                                }
                            }

                            CrewStatus crewStatus = new CrewStatus();
                            crewStatus.SeninId = card.MsSeninID;
                            var beinWork = WtmAccessor.Instance().GetBeInWork(card.MsSeninID);
                            if (beinWork != null)
                            {
                                crewStatus.BeinWork = true;
                            }
                            else
                            {
                                crewStatus.BeinWork = false;
                            }


                            Button b = new Button();
                            Font f = new Font(b.Font.FontFamily, 16);
                            b.Font = f;
                            b.Text = card.SeninName + System.Environment.NewLine + shokumei.NameAbbr;
                            b.AutoSize = false;
                            b.Width = 300;
                            b.Height = 60;
                            b.FlatStyle = FlatStyle.Popup;
                            b.ForeColor = Color.White;
                            b.BackColor = crewStatus.BeinWork ? ButtonColors[1] : ButtonColors[0];

                            b.Click += new System.EventHandler(this.CrewButton_Click);


                            flowLayoutPanel1.Controls.Add(b);


                            ButtonDic.Add(b, crewStatus);
                        }

                    }
                }
            //}

            flowLayoutPanel1.AutoScrollPosition = new Point(0, 0);
            flowLayoutPanel1.ResumeLayout();
            flowLayoutPanel1.Visible = true;

            this.ResumeLayout();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label_Now.Text = DateTime.Now.ToLongTimeString();


            if (preDate == DateTime.MinValue)
            {
                preDate = TODAY;
            }

            if (preDate.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                TODAY = DateTime.Today;


                preDate = TODAY;

                label日付.Text = TODAY.ToString("yyyy年MM月dd日（ddd）");

                if (startDate.Date != TODAY.Date)
                {
                    MakePanel();
                }
            }
        }

        #region private void CrewButton_Click(object sender, EventArgs e)
        private void CrewButton_Click(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;
            SeninId = ButtonDic[clickedButton].SeninId;

            foreach(var control in flowLayoutPanel1.Controls)
            {
                if (control is Button)
                {
                    var b = control as Button;
                    var crewStatus = ButtonDic[b];


                    if (b == clickedButton)
                    {
                        BeInWork = WtmAccessor.Instance().GetBeInWork(SeninId);

                        if (BeInWork != null)
                        {
                            // 出勤中
                            b.BackColor = ButtonColors[3];

                            buttonStartWork.BackColor = SystemColors.Control;
                            buttonStartWork.ForeColor = SystemColors.ControlText;
                            buttonFinishWork.BackColor = ButtonColors[3];
                            buttonFinishWork.ForeColor = Color.White;

                            buttonStartWork.Enabled = false;
                            buttonFinishWork.Enabled = true;
                        }
                        else
                        {
                            // 退勤中
                            b.BackColor = ButtonColors[3];

                            buttonStartWork.BackColor = ButtonColors[2];
                            buttonStartWork.ForeColor = Color.White;
                            buttonFinishWork.BackColor = SystemColors.Control;
                            buttonFinishWork.ForeColor = SystemColors.ControlText;

                            buttonStartWork.Enabled = true;
                            buttonFinishWork.Enabled = false;
                        }

                    }
                    else
                    {
                        b.BackColor = crewStatus.BeinWork ? ButtonColors[1] : ButtonColors[0];
                    }
                }
            }
        }
        #endregion


        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            出勤Form frm = new 出勤Form(this);
            frm.Show();
        }


        private void buttonFinishWork_Click(object sender, EventArgs e)
        {
            退勤Form frm = new 退勤Form(this);
            frm.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SeninId = 0;
            BeInWork = null;

            MakePanel();

            buttonStartWork.BackColor = SystemColors.Control;
            buttonStartWork.ForeColor = SystemColors.ControlText;
            buttonFinishWork.BackColor = SystemColors.Control;
            buttonFinishWork.ForeColor = SystemColors.ControlText;

            buttonStartWork.Enabled = false;
            buttonFinishWork.Enabled = false;
        }
    }
}
