using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.DA;
using ORMapping;
using NBaseUtil;

namespace Yojitsu
{
    public partial class 予算作成Form : Form
    {
        private BgYosanHead yosanHead;
        private List<BgKadouVessel> kadouVessels;


        public 予算作成Form(BgYosanHead yosanHead)
        {
            this.yosanHead = yosanHead;

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "予算作成", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
        }

        private void Init()
        {
            textBox年度.Text = yosanHead.Year.ToString();
            textBox予算種別.Text = BgYosanSbt.ToName(yosanHead.YosanSbtID);
            textBox作成者.Text = NBaseCommon.Common.LoginUser.FullName;
        }

        private void button作成_Click(object sender, EventArgs e)
        {
            string yosanStr = yosanHead.Year + "年度" + BgYosanSbt.ToName(yosanHead.YosanSbtID) + "予算";
            bool result = false;

            if (ValidateFields() && MessageBox.Show(yosanStr + "を作成します。よろしいですか？",
               "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ProgressDialog progressDialog = new ProgressDialog(delegate
                        {
                            if (kadouVessels == null)
                            {
                                GetBgKadouVessels();
                            }

                            yosanHead.ZenteiJouken = textBox前提条件.Text;
                            yosanHead.RenewDate = DateTime.Now;
                            yosanHead.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                            result = DbAccessorFactory.FACTORY.BLC_予算作成(NBaseCommon.Common.LoginUser, yosanHead, kadouVessels);
                        }, "予算を作成しています...");
                
                progressDialog.ShowDialog();

                if (result)
                {
                    if (MessageBox.Show(yosanStr + "を作成しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        DialogResult = DialogResult.OK;
                        Dispose();
                    }
                }
                else
                {
                    MessageBox.Show(yosanStr + "の作成に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        
        private bool ValidateFields()
        {
            if (textBox前提条件.Text.Length > 2000)
            {
                MessageBox.Show("前提条件は2000文字以内で入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button船稼働設定_Click(object sender, EventArgs e)
        {
            if (kadouVessels == null)
            {
                GetBgKadouVessels();
            }

            船稼働設定Form form = new 船稼働設定Form(yosanHead, kadouVessels, 船稼働設定Form.ConfigType.船稼働設定, false);

            if (form.ShowDialog() == DialogResult.OK)
            {
                kadouVessels = form.KadouVessels;
            }
        }


        private void GetBgKadouVessels()
        {
            kadouVessels = new List<BgKadouVessel>();

            if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.当初)
            {
                CreateBgKadouVessels_当初();
            }
            else if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            {
                CreateBgKadouVessels_見直し();
            }
        }


        private void CreateBgKadouVessels_当初()
        {
            Dictionary<BgKadouVesselKey, BgKadouVessel> kadouVesselDic = new Dictionary<BgKadouVesselKey, BgKadouVessel>();

            // 直近の BG_KADOU_VESSEL を取得.
            BuildKadouVesselDic((int)BgYosanSbt.BgYosanSbtEnum.当初, kadouVesselDic);
            BuildKadouVesselDic((int)BgYosanSbt.BgYosanSbtEnum.見直し, kadouVesselDic);

            var vessels = DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);

            foreach (MsVessel v in vessels)
            {
                for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
                {
                    BgKadouVesselKey key = new BgKadouVesselKey(v.MsVesselID, yosanHead.Year + i);

                    if(kadouVesselDic.ContainsKey(key))
                    {
                        kadouVessels.Add(CreateBgKadouVessel(kadouVesselDic[key]));
                    }
                    else
                    {
                        kadouVessels.Add(CreateBgKadouVessel(v, i));
                    }
                }
            }
        }


        private static void BuildKadouVesselDic(int yosanSbtId, Dictionary<BgKadouVesselKey, BgKadouVessel> kadouVesselDic)
        {
            //BgYosanHead lastYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecord_直近(NBaseCommon.Common.LoginUser, yosanSbtId);
            BgYosanHead lastYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecord_直近(NBaseCommon.Common.LoginUser);

            if (lastYosanHead == null)
            {
                return;
            }

            List<BgKadouVessel> kadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser,
                                                                                                   lastYosanHead.YosanHeadID);
            foreach (BgKadouVessel kv in kadouVessels)
            {
                kadouVesselDic[new BgKadouVesselKey(kv.MsVesselID, kv.Year)] = kv;
            }
        }


        private void CreateBgKadouVessels_見直し()
        {
            // 直近の BG_KADOU_VESSEL を取得.
            //BgYosanHead lastYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecord_直近(NBaseCommon.Common.LoginUser, (int)BgYosanSbt.BgYosanSbtEnum.当初);
            BgYosanHead lastYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecord_直近(NBaseCommon.Common.LoginUser);

            int yearRange = NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID);
            List<BgKadouVessel> lastKadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadAndYearRange(NBaseCommon.Common.LoginUser,
                                                                                                   lastYosanHead.YosanHeadID,
                                                                                                   yosanHead.Year,
                                                                                                   yosanHead.Year + yearRange - 1);

            foreach (BgKadouVessel kv in lastKadouVessels)
            {
                kadouVessels.Add(CreateBgKadouVessel(kv));
            }
        }


        private BgKadouVessel CreateBgKadouVessel(MsVessel v, int index)
        {
            BgKadouVessel kadouVessel = new BgKadouVessel();

            kadouVessel.YosanHeadID = yosanHead.YosanHeadID;
            kadouVessel.MsVesselID = v.MsVesselID;
            kadouVessel.VesselName = v.VesselName;
            kadouVessel.Year = yosanHead.Year + index;
            kadouVessel.KadouStartDate = new DateTime(yosanHead.Year + index, 4, 1, 0, 0, 0);
            kadouVessel.KadouEndDate = new DateTime(yosanHead.Year + index + 1, 3, 31, 12, 0, 0);

            return kadouVessel;
        }


        private BgKadouVessel CreateBgKadouVessel(BgKadouVessel srcKadouVessel)
        {
            BgKadouVessel kadouVessel = new BgKadouVessel();

            kadouVessel.YosanHeadID = yosanHead.YosanHeadID;
            kadouVessel.MsVesselID = srcKadouVessel.MsVesselID;
            kadouVessel.VesselName = srcKadouVessel.VesselName;
            kadouVessel.Year = srcKadouVessel.Year;
            kadouVessel.KadouStartDate = srcKadouVessel.KadouStartDate;
            kadouVessel.KadouEndDate = srcKadouVessel.KadouEndDate;
            kadouVessel.NyukyoKind = srcKadouVessel.NyukyoKind;
            kadouVessel.NyukyoMonth = srcKadouVessel.NyukyoMonth;
            kadouVessel.Fukadoubi1 = srcKadouVessel.Fukadoubi1;
            kadouVessel.Fukadoubi2 = srcKadouVessel.Fukadoubi2;
            kadouVessel.Fukadoubi3 = srcKadouVessel.Fukadoubi3;

            return kadouVessel;
        }


        public class BgKadouVesselKey
        {
            private int msVesselId;
            private int year;


            public BgKadouVesselKey(int msVesselId, int year)
            {
                this.msVesselId = msVesselId;
                this.year = year;
            }


            public override bool Equals(object obj)
            {
                BgKadouVesselKey other = obj as BgKadouVesselKey;

                return msVesselId == other.msVesselId && year == other.year;
            }


            public override int GetHashCode()
            {
                return msVesselId ^ year;
            }
        }
    }
}
