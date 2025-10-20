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
using System.IO;
using Yojitsu.util;
using NBaseUtil;

namespace Yojitsu
{
    public partial class Excelファイル読込Form : Form
    {
        private NenjiForm nenjiForm;
        private BgYosanHead yosanHead;
        private MsVessel vessel;
        private BgYosanExcel.ShubetsuEnum shubetsu;

        private BgYosanExcel yosanExcel;

        private TreeListViewDelegateExcelファイル読込 treeListViewDelegate;

        private IExcelSource excelSource;


        public Excelファイル読込Form(NenjiForm nenjiForm, BgYosanHead yosanHead, BgYosanExcel.ShubetsuEnum shubetsu)
            : this(nenjiForm, yosanHead, null, shubetsu)
        {
        }


        public Excelファイル読込Form(NenjiForm nenjiForm, BgYosanHead yosanHead, MsVessel vessel, BgYosanExcel.ShubetsuEnum shubetsu)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            this.vessel = vessel;
            this.shubetsu = shubetsu;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (shubetsu == BgYosanExcel.ShubetsuEnum.共通割掛船員)
            {
                this.Text = NBaseCommon.Common.WindowTitle("", "共通割掛船員費入力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
                this.excelSource = new ExcelSource_共通割掛船員(yosanHead);
            }
            else if (shubetsu == BgYosanExcel.ShubetsuEnum.貸船借船料)
            {
                this.Text = NBaseCommon.Common.WindowTitle("", "貸船・借船料入力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
                this.excelSource = new ExcelSource_貸船借船料(yosanHead);
            }

            treeListViewDelegate = new TreeListViewDelegateExcelファイル読込(treeListView1);

            LoadData();
        }


        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                int msVesselId = int.MinValue;

                if (vessel != null)
                {
                    msVesselId = vessel.MsVesselID;
                }
                
                yosanExcel = DbAccessorFactory.FACTORY.BgYosanExcel_GetRecordsByYosanHeadIDAndMsVesselIdAndShubetsu(NBaseCommon.Common.LoginUser,
                                                                                                            yosanHead.YosanHeadID,
                                                                                                            msVesselId,
                                                                                                            (int)shubetsu);
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            EnableButtons();

            if (yosanExcel != null)
            {
                treeListViewDelegate.CreateRow(yosanExcel);
            }

            this.Cursor = Cursors.Default;
        }


        private void EnableButtons()
        {
            if (yosanExcel == null)
            {
                button出力.Enabled = false;
            }
            else
            {
                button出力.Enabled = true;
            }

            if (yosanHead.IsFixed())
            {
                button設定.Enabled = false;
            }
        }


        private void button設定_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Save(openFileDialog1.FileName))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }


        private bool Save(string fileName)
        {
            YosanObjectCollection yosanObject;

            int msVesselId = int.MinValue;

            if (vessel != null)
            {
                msVesselId = vessel.MsVesselID;
            }
            
            if (excelSource.ReadFile(fileName, out yosanObject, msVesselId))
            {
                BuildData(fileName);

                bool result = DbAccessorFactory.FACTORY.BgYosanExcel_InsertOrUpdate(NBaseCommon.Common.LoginUser, yosanExcel);

                if (!result)
                {
                    MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    nenjiForm.SetExcelData(yosanObject);
                    MessageBox.Show("データを保存しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }


        internal static Dictionary<string, MsVessel> CreateMsVesselDic()
        {
            Dictionary<string, MsVessel> msVesselDic = new Dictionary<string, MsVessel>();

            foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser))
            {
                msVesselDic[v.VesselName] = v;
            }

            return msVesselDic;
        }


        internal static Dictionary<string, MsHimoku> CreateMsHimokuDic()
        {
            Dictionary<string, MsHimoku> msHimokuDic = new Dictionary<string, MsHimoku>();

            foreach (MsHimoku h in DbAccessorFactory.FACTORY.MsHimoku_GetRecords(NBaseCommon.Common.LoginUser))
            {
                msHimokuDic[h.HimokuName] = h;
            }

            return msHimokuDic;
        }


        private void BuildData(string fileName)
        {
            if (yosanExcel == null)
            {
                yosanExcel = new BgYosanExcel();
                yosanExcel.YosanHeadID = yosanHead.YosanHeadID;
                yosanExcel.Shubetsu = (int)shubetsu;

                if (vessel != null)
                {
                    yosanExcel.MsVesselID = vessel.MsVesselID;
                }
            }

            yosanExcel.FileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            yosanExcel.FileData = ToBytes(fileName);
        }


        public static byte[] ToBytes(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] b = new byte[fs.Length];
                fs.Read(b, 0, b.Length);

                return b;
            }
        }


        private void button出力_Click(object sender, EventArgs e)
        {
            if (yosanExcel != null)
            {
                saveFileDialog1.FileName = yosanExcel.FileName;
                
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;

                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(yosanExcel.FileData, 0, yosanExcel.FileData.Length);
                    }
                    
                    MessageBox.Show("ファイルを出力しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        public class YosanObjectCollection
        {
            // <MsVesselID, <MsHimokuID, <Nengetsu, YosanObject>>>
            public Dictionary<int, Dictionary<int, Dictionary<string, YosanObject>>> YosanDic { get; private set; }


            public YosanObjectCollection()
            {
                YosanDic = new Dictionary<int, Dictionary<int, Dictionary<string, YosanObject>>>();
            }


            internal void AddAmount(int msVesselId, int msHimokuId, string nengetsu, decimal amount)
            {
                AddAmount(msVesselId, msHimokuId, nengetsu, amount, 0);
            }


            internal void AddAmount(int msVesselId, int msHimokuId, string nengetsu, decimal amount, decimal dollarAmount)
            {
                if (!YosanDic.ContainsKey(msVesselId))
                {
                    YosanDic[msVesselId] = new Dictionary<int, Dictionary<string, YosanObject>>();
                }

                if (!YosanDic[msVesselId].ContainsKey(msHimokuId))
                {
                    YosanDic[msVesselId][msHimokuId] = new Dictionary<string, YosanObject>();
                }

                if (!YosanDic[msVesselId][msHimokuId].ContainsKey(nengetsu))
                {
                    YosanDic[msVesselId][msHimokuId][nengetsu] = new YosanObject();
                }

                YosanDic[msVesselId][msHimokuId][nengetsu].Amount += amount;
                YosanDic[msVesselId][msHimokuId][nengetsu].DollerAmount += dollarAmount;
            }
        }


        public class YosanObject
        {
            public string Nengetsu { get; set; }

            public decimal Amount { get; set; }
            public decimal DollerAmount { get; set; }
        }
    }
}
