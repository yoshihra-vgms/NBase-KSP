using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NBaseData.DAC;
using NBaseCommon;
using Senin;
using NBaseData.DS;
using Senin.util;
using C1.Win.C1FlexGrid;
using NBase.util;
using NBaseUtil;

namespace NBase
{
    public partial class 動静シミュレーションForm : Form
    {
        //Portal　Form
        private PortalForm PForm;
        private MsVessel Vessel;
        private DateTime ColStartDate;
        private int Days;
        private int KomaPerVessel;
        private List<PtKanidouseiInfo> Infos;
        private List<SimFormData> DouseiInfoList = new List<SimFormData>();
        private List<SimFormData> DouseiInfoSimList = new List<SimFormData>();

        private Dictionary<int, List<int>> RowMgTbl = new Dictionary<int, List<int>>();
        private List<int> RowMgAlaramList = new List<int>();

        //アイコン
        private Image CheckdImage ;
        private Image StayImage  ;
        private Image NullImage;

        List<NBaseData.BLC.SimulationProc.DeviationAlarmInfo> AlarmInfos = null;

        //アラーム画像
        private List<Image> AlarmImg = new List<Image>();

        private enum DRAG_SITUATION { NA, TumiPast,　TumiFuture, AgePast, AgeFuture };


        public 動静シミュレーションForm(PortalForm pform, int koma, MsVessel vessel, DateTime date, int days)
        {
            InitializeComponent();

            PForm = pform;
            KomaPerVessel = koma + 1;
            Vessel = vessel;
            ColStartDate = date;
            Days = days;
        }

        private void 動静シミュレーションForm_Load(object sender, EventArgs e)
        {
            色AndスタイルAnd画像設定();

            //行管理
            RowMgTbl.Add(0, new List<int>());//固定
            RowMgTbl.Add(1, new List<int>());//ｼﾐｭﾚｰｼｮﾝ
            RowMgTbl.Add(2, new List<int>());//せぱれーた 

            //c1FlexGrid1.Cols.Count = 1;
            //c1FlexGrid1.Rows.Count = 1;

            #region カラム数と列ヘッダセット
            c1FlexGrid1.Cols.Count = Days + 1;
            for (int i = 1; i <= Days; i++)
            {
                c1FlexGrid1.Cols[i].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1FlexGrid1.Cols[i].Width = 150;
                c1FlexGrid1[0, i] = ColStartDate.AddDays(i - 1).ToString("M/d(ddd)");
            }
            #endregion

            //行数
            c1FlexGrid1.Rows.Count = 2 * KomaPerVessel+1+1;//　(固定行+シミュレーション行 )+セパレータ+カラムヘッダ
            c1FlexGrid1.Rows.DefaultSize = 35;

            //行ヘッダの幅
            c1FlexGrid1.Cols[0].Width = 125;
            c1FlexGrid1.Cols[0].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1[0, 0] = "船";

            #region 行ヘッダと表のスタイル設定
            行ヘッダ作成And表スタイル設定(0);//固定行
            行ヘッダ作成And表スタイル設定(1);//シミュレーション行
            #endregion

            c1FlexGrid1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            c1FlexGrid1.Cols[0].AllowMerging = true;


            // 船の表示期間のレコードを一括で取得する
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                DateTime fromDatetime = new DateTime(ColStartDate.Year, ColStartDate.Month, ColStartDate.Day);
                DateTime toDatetime = fromDatetime.AddDays(Days);
                toDatetime = toDatetime.AddSeconds(-1);

                Infos = serviceClient.PtKanidouseiInfo_GetRecordByEventDate(NBaseCommon.Common.LoginUser, fromDatetime, toDatetime, Vessel.MsVesselID);

                List<DjDousei> douseis = new List<DjDousei>();

                foreach (PtKanidouseiInfo p in Infos)
                {
                    SimFormData data = new SimFormData();

                    data.MsVesselId = Vessel.MsVesselID;
                    data.EventDateDisp = p.EventDate;
                    data.KInfo = p;
                    data.DInfos = serviceClient.DjDousei_GetRecordsBySameVoaygeNo(NBaseCommon.Common.LoginUser, p.DjDouseiID);

                    DouseiInfoList.Add(data);

                    DouseiInfoSimList.Add(SimFormData.Clone(data));


                    foreach(DjDousei dousei in data.DInfos)
                    {
                        if (douseis.Any(o => o.DjDouseiID == dousei.DjDouseiID) == false)
                        {
                            douseis.Add(dousei);
                        }
                    }
                }

                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                AlarmInfos = serviceClient.BLC_GetDeviationInfos(NBaseCommon.Common.LoginUser, appName, Vessel.MsVesselID, ColStartDate, ColStartDate.AddDays(Days), douseis);
            }

            データ表示(0, DouseiInfoList);//
            データ表示(1, DouseiInfoSimList);

            アラーム表示(0);//
            アラーム表示(1);//

        }

        #region private void 色AndスタイルAnd画像設定()
        private void 色AndスタイルAnd画像設定()
        {
            c1FlexGrid1.Rows.DefaultSize = 35;

            c1FlexGrid1.Styles.Add("積み");
            c1FlexGrid1.Styles.Add("揚げ");
            c1FlexGrid1.Styles.Add("待機");
            c1FlexGrid1.Styles.Add("パージ");
            c1FlexGrid1.Styles.Add("避泊");
            c1FlexGrid1.Styles.Add("その他");

            c1FlexGrid1.Styles["積み"].BackColor = NBaseCommon.Common.ColorTumi;
            c1FlexGrid1.Styles["揚げ"].BackColor = NBaseCommon.Common.ColorAge;
            c1FlexGrid1.Styles["待機"].BackColor = NBaseCommon.Common.ColorTaiki;
            c1FlexGrid1.Styles["パージ"].BackColor = NBaseCommon.Common.ColorPurge;
            c1FlexGrid1.Styles["避泊"].BackColor = NBaseCommon.Common.ColorHihaku;
            c1FlexGrid1.Styles["その他"].BackColor = NBaseCommon.Common.ColorEtc;

            c1FlexGrid1.Styles.Add("積みHover");
            c1FlexGrid1.Styles.Add("揚げHover");
            c1FlexGrid1.Styles.Add("待機Hover");
            c1FlexGrid1.Styles.Add("パージHover");
            c1FlexGrid1.Styles.Add("避泊Hover");
            c1FlexGrid1.Styles.Add("その他Hover");

            c1FlexGrid1.Styles["積みHover"].BackColor = NBaseUtil.ColorExtension.GetLightColor(NBaseCommon.Common.ColorTumi);
            c1FlexGrid1.Styles["揚げHover"].BackColor = NBaseUtil.ColorExtension.GetLightColor(NBaseCommon.Common.ColorAge);
            c1FlexGrid1.Styles["待機Hover"].BackColor = NBaseUtil.ColorExtension.GetLightColor(NBaseCommon.Common.ColorTaiki);
            c1FlexGrid1.Styles["パージHover"].BackColor = NBaseUtil.ColorExtension.GetLightColor(NBaseCommon.Common.ColorPurge);
            c1FlexGrid1.Styles["避泊Hover"].BackColor = NBaseUtil.ColorExtension.GetLightColor(NBaseCommon.Common.ColorHihaku);
            c1FlexGrid1.Styles["その他Hover"].BackColor = NBaseUtil.ColorExtension.GetLightColor(NBaseCommon.Common.ColorEtc);

            //過去の色
            c1FlexGrid1.Styles.Add("過去");
            c1FlexGrid1.Styles["過去"].BackColor = Color.Gainsboro;

            //選択された船の色
            c1FlexGrid1.Styles.Add("選択");
            c1FlexGrid1.Styles["選択"].BackColor = Color.Violet;

            //表示だけ
            c1FlexGrid1.Styles.Add("固定");
            c1FlexGrid1.Styles["固定"].BackColor = Color.Gainsboro;

            #region　アイコン　PortalFormのイメージを取得
            Image img = PForm.pictureBox_Check.Image;
            Bitmap imgbitmap = new Bitmap(img);
            CheckdImage = (Image)(new Bitmap(imgbitmap, new Size(15, 15)));

            img = PForm.pictureBox_Ikari.Image;
            imgbitmap = new Bitmap(img);
            imgbitmap.MakeTransparent();
            StayImage = (Image)(new Bitmap(imgbitmap, new Size(15, 15)));

            img = PForm.pictureBox_Null.Image;
            imgbitmap = new Bitmap(img);
            NullImage = (Image)(new Bitmap(imgbitmap, new Size(15, 15)));
            #endregion

            //Alarm画像
            for (int i = 0; i < 16; i++)
            {
                Image wkimg = Image.FromFile(@"Resources\alarm" + i.ToString() + ".png");

                AlarmImg.Add(wkimg);
            }

        }
        #endregion

        #region private void 行ヘッダ作成And表スタイル設定(int mode)
        private void 行ヘッダ作成And表スタイル設定(int mode)
        {

            int colstart = c1FlexGrid1.Cols.Fixed;
            int colend = c1FlexGrid1.Cols.Count - 1;

            int idx;
            if (mode == 0)
            {
                idx = 1;
                CellRange cr = c1FlexGrid1.GetCellRange(idx, colstart, idx + KomaPerVessel - 1, colend);
                cr.Style = c1FlexGrid1.Styles["固定"];
            }
            else
            {
                idx = 1 + KomaPerVessel;
               
                {
                    //セパレータ行
                    RowMgTbl[2].Add(idx);
                    c1FlexGrid1.Rows[idx].Height = 1;
                    CellRange cr = c1FlexGrid1.GetCellRange(idx, 0, idx, colend);
                    cr.StyleNew.Border.Color = Color.DarkGray;
                }

                {
                    idx++;
                    CellRange cr = c1FlexGrid1.GetCellRange(idx + KomaPerVessel-1, colstart, idx + KomaPerVessel-1, colend);
                    cr.Style = c1FlexGrid1.Styles["固定"];
                }
            }


            for (int i = 0; i < KomaPerVessel; i++)
            {
                var vesselCellStr = mode==1?"ｼﾐｭﾚｰｼｮﾝ"+System.Environment.NewLine:""+  Vessel.VesselName + System.Environment.NewLine;
                //vesselCellStr += Vessel.CaptainName + System.Environment.NewLine;
                //vesselCellStr += Vessel.Tel + System.Environment.NewLine;
                //vesselCellStr += Vessel.HpTel + System.Environment.NewLine;
                //vesselCellStr += "営：" + Vessel.SalesPersonName + System.Environment.NewLine;
                //vesselCellStr += "工：" + Vessel.MarineSuperintendentName + System.Environment.NewLine;

                c1FlexGrid1[idx, 0] = vesselCellStr;

                if (i < KomaPerVessel - 1)
                {
                    RowMgTbl[mode].Add(idx);
                }
                else
                {
                    //////アラーム行
                    //c1FlexGrid1.Rows[idx].ImageAlignFixed = ImageAlignEnum.Stretch;

                    //アラーム(ラスト)の行はRowMgTblには入れない
                    RowMgAlaramList.Add(idx);
                }
                idx++;
            }


            //System.Diagnostics.Debug.WriteLine($"idx = {idx}");

            // 各船の最終行は、アラーム表示行
            c1FlexGrid1.Rows[idx - 1].ImageAlign = ImageAlignEnum.Stretch;
            c1FlexGrid1.Rows[idx - 1].Height = 25;

        }
        #endregion


        #region private void データ表示(int mode)
        private void データ表示(int mode, List<SimFormData> datas)
        {
            int idx;

            //表示できる最初の行
            idx = RowMgTbl[mode][0];

            for (int i = 1; i <= Days; i++)
            {
                foreach (int r in RowMgTbl[mode])
                {
                    c1FlexGrid1[r, i] = null;
                    c1FlexGrid1.SetCellStyle(r, i, "");
                }

                if (ColStartDate.AddDays(i - 1) < DateTime.Today)
                {
                    foreach (int r in RowMgTbl[mode])
                    {
                        c1FlexGrid1.SetCellStyle(r, i, "過去");
                    }
                }

                DateTime eventDate = ColStartDate.AddDays(i - 1);

                int rowIdx = idx;
                var infos = datas.Where(o => o.EventDateDisp.ToShortDateString() == eventDate.ToShortDateString());
                var offset = 0;
                foreach (SimFormData info in infos)
                {
                    offset++;
                    if (offset == KomaPerVessel)
                        break;

                    データセット(rowIdx, i, info);

                    rowIdx++;

                }
            }
        }
        #endregion

        #region private void アラーム表示(int mode)
        private void アラーム表示(int mode)
        {
            for (int i = 1; i <= Days; i++)
            {
                DateTime eventDate = ColStartDate.AddDays(i - 1);

                var aInfo = AlarmInfos.Where(o => o.Date == eventDate).FirstOrDefault();
                アラームセット(RowMgAlaramList[mode], i, aInfo);
            }
        }
        #endregion

        #region private void button確定_Click(object sender, EventArgs e)
        private void button確定_Click(object sender, EventArgs e)
        {
            foreach(int row in RowMgTbl[1])
            {
                for (int i = 1; i <= Days; i++)
                {
                    if (c1FlexGrid1.GetData(row, i) is SimFormData)
                    {
                        var data = c1FlexGrid1.GetData(row, i) as SimFormData;

                        //
                        //日付の変更があったデータか
                        // 
                        if (data != null && data.EventDateDisp != data.KInfo.EventDate)
                        {
                            MessageBox.Show($"{data.KInfo.EventDate.ToShortDateString()} ⇒ {data.EventDateDisp.ToShortDateString()}へ移動");
                            //List<DjDousei> douseis = null;
                            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                            //{
                            //    douseis = serviceClient.DjDousei_GetRecordsBySameVoaygeNo(NBaseCommon.Common.LoginUser, data.KInfo.DjDouseiID);
                            //}

                            //foreach(DjDousei d in douseis)
                            //{
                            //    if (d.DjDouseiID == data.KInfo.DjDouseiID)
                            //    {
                            //        var diff = data.EventDateDisp.Day - d.DouseiDate.Day;
                            //        d.DouseiDate = d.DouseiDate.AddDays(diff);
                            //        if (d.PlanNiyakuStart != DateTime.MinValue)
                            //            d.PlanNiyakuStart = d.PlanNiyakuStart.AddDays(diff);
                            //        if (d.PlanNyuko != DateTime.MinValue)
                            //            d.PlanNyuko = d.PlanNyuko.AddDays(diff);
                            //        if (d.PlanChakusan != DateTime.MinValue)
                            //            d.PlanChakusan = d.PlanChakusan.AddDays(diff);
                            //        if (d.PlanRisan != DateTime.MinValue)
                            //            d.PlanRisan = d.PlanRisan.AddDays(diff);
                            //        if (d.PlanShukou != DateTime.MinValue)
                            //            d.PlanShukou = d.PlanShukou.AddDays(diff);
                            //    }
                            //}

                            //セルにセットされたDouseiデータを集める
                            List<DjDousei> douseis = new List<DjDousei>();
                            foreach (DjDousei d in data.DInfos)
                            {
                                //セルにセットされているPtkanidouseiと同じdouseiを集める
                                if (d.DjDouseiID == data.KInfo.DjDouseiID)
                                {
                                    douseis.Add(d);
                                }
                            }

                            bool ret = true;
                            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                            {
                                ret = serviceClient.BLC_動静予定登録(NBaseCommon.Common.LoginUser, douseis);
                            }


                        }
                    }
                }
            }
            PForm.RefreshGrid();
            this.Close();
        }
        #endregion

        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        /// <summary>
        /// 動静予定Formを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void c1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        private void c1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestInfo ht = c1FlexGrid1.HitTest(e.X, e.Y);

            int row = ht.Row;
            int col = ht.Column;

            if ((row > 0 && col > 0 && DragStartRow > 0 && DragStartCol > 0) == false)
                return;

            //Sim行ではない
            if (!RowMgTbl[1].Contains(row))
                return;

            // 変更するデータ
            var data = c1FlexGrid1.GetData(row, col) as SimFormData;

            if (data == null) return;

            //変更データのクローン
            SimFormData changedata =  SimFormData.Clone(data);
            
            動静予定Form3 form = 動静予定Form3.Instance();

            form.Set動静予定(changedata.KInfo, 1, changedata.DInfos);
            form.MsKanidouseiInfoShubetu_List = PForm.msKanidouseiInfoShubetu_list;
            form.MsVessel_List = PForm.msVessel_list;
            form.MsBasho_list = PForm.msBasho_list;
            form.MsKichi_list = PForm.msKichi_list;
            form.MsCargo_list = PForm.msCargo_list;
            form.MsDjTani_list = PForm.msDjTani_list;
            form.MsCustomer_list = PForm.msCustomer_list;

            if (form.ShowDialog() == DialogResult.OK)
            {
                List<DjDousei> douseis = new List<DjDousei>();

                foreach (SimFormData simd in DouseiInfoSimList)
                {
                    //SimListの 積み揚げDjDouseiを入れ替える
                    simd.DInfos = new List<DjDousei>();
                    foreach (DjDousei douse in form.反映DouseiInfos)
                    {
                        simd.DInfos.Add(douse);
                    }

                    //SimFormDataのEventDate、PtKaindouseiのEventDateを書き換える
                    if (form.反映DouseiInfos != null && form.反映DouseiInfos.Count > 0)
                    {
                        DjDousei d = null;
                        if (simd.KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み)
                        {
                            d = form.反映DouseiInfos.Where(obj => obj.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID).OrderBy(obj => obj.PlanNiyakuStart).FirstOrDefault(); 
                        }
                        else if(simd.KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ)
                        {
                            d = form.反映DouseiInfos.Where(obj => obj.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID).OrderBy(obj => obj.PlanNiyakuStart).FirstOrDefault();
                        }

                        if (d != null)
                        {
                            simd.EventDateDisp = d.PlanNiyakuStart;
                            //simd.KInfo.EventDate = d.PlanNiyakuStart; //ここで入れると「確定」で変更データがわからなくなる　動静処理.動静予定登録()で入る
                        }
                    }

                    foreach (DjDousei dousei in simd.DInfos)
                    {
                        if (douseis.Any(o => o.DjDouseiID == dousei.DjDouseiID) == false)
                        {
                            douseis.Add(dousei);
                        }
                    }
                }


                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                        AlarmInfos = serviceClient.BLC_GetDeviationInfos(NBaseCommon.Common.LoginUser, appName, Vessel.MsVesselID, ColStartDate, ColStartDate.AddDays(Days), douseis);
                    }
                }, "アラーム算出中です...");
                progressDialog.ShowDialog();


                データ表示(1, DouseiInfoSimList);
                アラーム表示(1);
            }
        }
        #endregion
        
        //--------------------------------------------------------------------------------------
        //
        //Mouse　Drag & Drop
        //
        //

        //Dragされていれば1
        private int DraggingFlg = 0;

        //Drag開始した位置
        private int DragStartRow = -1;
        private int DragStartCol = -1;

        //Dragデータ
        private SimFormData DragData = null;

        //ホバー位置保持
        private int HoverPosCol = -1;
        private int HoverPosRow = -1;

        #region private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
        private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            //つかんだ位置を保持
            DragStartRow = c1FlexGrid1.HitTest(e.X, e.Y).Row;
            DragStartCol = c1FlexGrid1.HitTest(e.X, e.Y).Column;//c1FlexGrid1.HitTest(e.X, e.Y).Column;

            //シミュレーション行でないなら抜ける
            if (!RowMgTbl[1].Contains(DragStartRow)) return;

            //Downした場所にセットされたデータを取得
            if (c1FlexGrid1.GetData(DragStartRow, DragStartCol) is SimFormData)
            {
                DragData = c1FlexGrid1.GetData(DragStartRow, DragStartCol) as SimFormData;
            }
            else
            {
                DragData = null;
            }

            //セットされたデータはがないなら抜ける
            if (DragData == null)
            {
                System.Diagnostics.Debug.WriteLine("down:開始しない1");
                DraggingFlg = 0;
                return;
            }

            //Sim行ではない。　過去ではない
            if (!RowMgTbl[1].Contains(DragStartRow) || c1FlexGrid1.GetCellStyle(DragStartRow, DragStartCol).Name == "過去")
            {
                System.Diagnostics.Debug.WriteLine("down:開始しない2");
                DraggingFlg = 0;
                return;

            }

            ////過去
            //if (c1FlexGrid1.GetCellStyle(row, col).Name == "過去") return false;



            ////データは移動可能な場所にあるか
            //if (!RowMgTbl[1].Contains(row))
            //{
            //    System.Diagnostics.Debug.WriteLine("down:開始しない2");
            //    draggingflg = 0;
            //    return;
            //}

            //System.Diagnostics.Debug.WriteLine("down");

            //ドラッグ開始
            DraggingFlg = 1;

            //マウスカーソル変更
            Cursor.Current = Cursors.Hand;

        }
        #endregion

        #region private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
        private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            //ドラッグ開始でないなら抜ける
            if (DraggingFlg == 0) return;

            // マウスポインタ位置の判別
            HitTestInfo htInfo = c1FlexGrid1.HitTest();

            int col = htInfo.Column;
            int row = htInfo.Row;

            //System.Diagnostics.Debug.WriteLine("move");

  
            //　前回のホバースタイルの解除
            if (HoverPosCol != -1 && HoverPosRow != -1 && DragData!=null)
            {
                if ((c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name == "積み" && DragData.KInfo.KanidouseiInfoShubetuName == "積み") ||
                   (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name == "揚げ" && DragData.KInfo.KanidouseiInfoShubetuName == "揚げ"))
                { }
                else
                {

                    if ((c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name != "積み") &&
                         (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name != "揚げ") &&
                         (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name != "待機") &&
                         (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name != "パージ") &&
                         (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name != "避泊") &&
                         (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name != "その他")
                       )
                    {
                        System.Diagnostics.Debug.WriteLine("Move:解除");
                        if (c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol) != null)
                        {
                            System.Diagnostics.Debug.WriteLine("      style = " + c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name);
                        }
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Move:ここにはこないはず1");
                    }
                }
                //過去 または　Sim行ではない なら抜ける
                if ((c1FlexGrid1.GetCellStyle(row, col) != null && c1FlexGrid1.GetCellStyle(row, col).Name == "過去") || !RowMgTbl[1].Contains(DragStartRow))
                {
                    System.Diagnostics.Debug.WriteLine("move c1FlexGrid1.GetCellStyle(row, col)" + c1FlexGrid1.GetCellStyle(row, col).Name);
                    HoverPosCol = -1;
                    HoverPosRow = -1;
                    DraggingFlg = 0;
                    return;
                }
            }


            //if (!移動できるか(selectrow, selectcol) && (htInfo.Type == HitTestTypeEnum.Cell) && (htInfo.Type != HitTestTypeEnum.RowHeader))
            //{
            //    System.Diagnostics.Debug.WriteLine("down:開始しない2");
            //    draggingflg = 0;

            //    // 位置の初期化
            //    HoverPosCol = -1;
            //    HoverPosRow = -1;
            //    return;
            //}

            //System.Diagnostics.Debug.WriteLine("move c1FlexGrid1.GetCellStyle(row, col)" + c1FlexGrid1.GetCellStyle(row, col).Name);


            if ((htInfo.Type == HitTestTypeEnum.Cell) && (htInfo.Type != HitTestTypeEnum.RowHeader) && RowMgTbl[1].Contains(htInfo.Row))
            {
                // 位置の保存
                HoverPosCol = col;
                HoverPosRow = row;


                // ドラッグ開始のセル位置と動かしたマウスのセル位置が変更になった なおかつ　マウスのセル位置が空　ならHover表示 
                if ((HoverPosRow != DragStartRow || HoverPosCol != DragStartCol) && c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol) == null)
                {
                    //データと違う
                    if (DragData.KInfo.KanidouseiInfoShubetuName == "積み")//c1FlexGrid1.GetCellStyle(HoverPosRow, HoverPosCol).Name!="積み"))
                    {
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "積みHover");
                    }
                    else if (DragData.KInfo.KanidouseiInfoShubetuName == "揚げ" )
                    {
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "揚げHover");
                    }
                    else if (DragData.KInfo.KanidouseiInfoShubetuName == "待機" )
                    {
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "待機Hover");
                    }
                    else if (DragData.KInfo.KanidouseiInfoShubetuName == "パージ" )
                    {
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "パージHover");
                    }
                    else if (DragData.KInfo.KanidouseiInfoShubetuName == "避泊")
                    {
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "避泊Hover");
                    }
                    else if (DragData.KInfo.KanidouseiInfoShubetuName == "その他")
                    {
                        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "その他Hover");
                    }
                    else
                    {
                        //ここにはこないはず
                        System.Diagnostics.Debug.WriteLine("Move:ここにはこないはず1");
                    }
                }
            }
            else
            {
                // 位置の初期化
                HoverPosCol = -1;
                HoverPosRow = -1;
            }

        }
        #endregion

        #region private void c1FlexGrid1_MouseUp(object sender, MouseEventArgs e)
        private void c1FlexGrid1_MouseUp(object sender, MouseEventArgs e)
        {
            if (DraggingFlg == 0)
                return;

            if (DragData == null)
                return;

            DraggingFlg = 0;

            HitTestInfo ht = c1FlexGrid1.HitTest(e.X, e.Y);

            int putrow = ht.Row;
            int putcol = ht.Column;

            if ((putrow > 0 && putcol > 0 && DragStartRow > 0 && DragStartCol > 0) == false)
                return;

            if (putrow == DragStartRow && putcol == DragStartCol)
                return;

            //Sim行ではない
            if (!RowMgTbl[1].Contains(putrow))
                return;

            //過去
            if (c1FlexGrid1.GetCellStyle(putrow, putcol).Name == "過去")
                return;


            // 何をどう操作したか
            DRAG_SITUATION situation = DragSituation(putrow, putcol);



            // 移動後の日付
            DateTime changeDate = 列から日付を取得(putcol);

            // 移動日数
            var diff = putcol - DragStartCol;



            var checkDousei = GetCheckDousei(situation, changeDate);


            ////if (移動できるか(putrow, putcol))
            if (移動できるか(situation, diff, checkDousei))
            {



                ////移動元のセルのクリア
                //if (DragStartRow != putrow || DragStartCol != putcol)
                //{
                //    c1FlexGrid1[DragStartRow, DragStartCol] = null;
                //    c1FlexGrid1.SetCellStyle(DragStartRow, DragStartCol, "");

                //    //アラーム行クリア
                //    c1FlexGrid1.SetCellImage(RowMgAlaramList[1], DragStartCol, null);

                //}




                //DragData.KInfo.EventDate = DragData.KInfo.EventDate.AddDays(diff);
                //データセット(putrow, putcol, DragData);




                List<DjDousei> modDouseis = new List<DjDousei>(); // 変更されるDJ_DOUSEI
                List<DjDousei> douseis = new List<DjDousei>(); // 変更されるDJ_DOUSEI

                // 操作したコマに紐づくDJ_DOUSEIを変更
                foreach (DjDousei d in DragData.DInfos)
                {
                    //if (d.DjDouseiID != DragData.KInfo.DjDouseiID)
                    //    continue;

                    d.DouseiDate = d.DouseiDate.AddDays(diff);
                    if (d.PlanNyuko != DateTime.MinValue)
                        d.PlanNyuko = d.PlanNyuko.AddDays(diff);
                    if (d.PlanChakusan != DateTime.MinValue)
                        d.PlanChakusan = d.PlanChakusan.AddDays(diff);
                    if (d.PlanNiyakuStart != DateTime.MinValue)
                        d.PlanNiyakuStart = d.PlanNiyakuStart.AddDays(diff);
                    if (d.PlanNiyakuEnd != DateTime.MinValue)
                        d.PlanNiyakuEnd = d.PlanNiyakuEnd.AddDays(diff);
                    if (d.PlanRisan != DateTime.MinValue)
                        d.PlanRisan = d.PlanRisan.AddDays(diff);
                    if (d.PlanShukou != DateTime.MinValue)
                        d.PlanShukou = d.PlanShukou.AddDays(diff);

                    modDouseis.Add(d);
                }

                foreach (SimFormData data in DouseiInfoSimList)
                {
                    foreach (DjDousei dousei in data.DInfos)
                    {
                        if (modDouseis.Any(o => o.DjDouseiID == dousei.DjDouseiID))
                        {
                            var modDousei = modDouseis.Where(o => o.DjDouseiID == dousei.DjDouseiID).First();
                            dousei.DouseiDate = modDousei.DouseiDate;

                            dousei.PlanNyuko = modDousei.PlanNyuko;
                            dousei.PlanChakusan = modDousei.PlanChakusan;
                            dousei.PlanNiyakuStart = modDousei.PlanNiyakuStart;
                            dousei.PlanNiyakuEnd = modDousei.PlanNiyakuEnd;
                            dousei.PlanRisan = modDousei.PlanRisan;
                            dousei.PlanShukou = modDousei.PlanShukou;
                            
                            if (data.KInfo.DjDouseiID == dousei.DjDouseiID)
                            {
                                //data.KInfo.EventDate = dousei.DouseiDate;　//ここで入れると「確定」で変更データがわからなくなる　動静処理.動静予定登録()で入る
                                data.EventDateDisp = dousei.DouseiDate; 
                            }
                        }
                        if (douseis.Any(o => o.DjDouseiID == dousei.DjDouseiID) == false)
                        {
                            douseis.Add(dousei);
                        }
                    }
                }



                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                        AlarmInfos = serviceClient.BLC_GetDeviationInfos(NBaseCommon.Common.LoginUser, appName, Vessel.MsVesselID, ColStartDate, ColStartDate.AddDays(Days), douseis);
                    }
                }, "アラーム算出中です...");
                progressDialog.ShowDialog();


                データ表示(1, DouseiInfoSimList);
                アラーム表示(1);

            }
            else
            {
                c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "");
            }


            DragStartRow = -1;
            DragStartCol = -1;

            // マウスカーソル元に戻す
            Cursor.Current = Cursors.Default;

            // ホバー位置初期化
            HoverPosCol = -1;
            HoverPosRow = -1;
        }
        #endregion


        private void データセット(int row, int col, SimFormData data)
        {
            bool pictreOn = false;

            c1FlexGrid1[row, col] = data;

            if (data.KInfo.HonsenCheckDate != DateTime.MinValue)
            {
                c1FlexGrid1.SetCellImage(row, col, CheckdImage);
                pictreOn = true;
            }

            string shubetuName = data.KInfo.KanidouseiInfoShubetuName;
            if (shubetuName == MsKanidouseiInfoShubetu.積み)
            {
                c1FlexGrid1.SetCellStyle(row, col, "積み");
            }
            else if (shubetuName == MsKanidouseiInfoShubetu.揚げ)
            {
                c1FlexGrid1.SetCellStyle(row, col, "揚げ");
            }
            else if (shubetuName == MsKanidouseiInfoShubetu.待機)
            {
                c1FlexGrid1.SetCellStyle(row, col, "待機");

                c1FlexGrid1.SetCellImage(row, col, StayImage);
                pictreOn = true;
            }
            else if (shubetuName == MsKanidouseiInfoShubetu.パージ)
            {
                c1FlexGrid1.SetCellStyle(row, col, "パージ");
            }
            else if (shubetuName == MsKanidouseiInfoShubetu.避泊)
            {
                c1FlexGrid1.SetCellStyle(row, col, "避泊");
            }
            else if (shubetuName == MsKanidouseiInfoShubetu.その他)
            {
                c1FlexGrid1.SetCellStyle(row, col, "その他");
            }

            if (pictreOn == false)
            {
                c1FlexGrid1.SetCellImage(row, col, NullImage);
            }
        }

        private void アラームセット(int row, int col, NBaseData.BLC.SimulationProc.DeviationAlarmInfo aInfo)
        {
            int alarm24h = 0;
            int alarm1w = 0;
            int alarm4w = 0;
            int alarmrest = 0;

            //
            if (aInfo != null)
            {
                if (aInfo.Alarm24H) alarm24h = 8;
                if (aInfo.Alarm1W) alarm1w = 4;
                if (aInfo.Alarm4W) alarm4w = 2;
                if (aInfo.AlarmRest) alarmrest = 1;
            }
            int imageno = alarm24h + alarm1w + alarm4w + alarmrest;

            c1FlexGrid1.SetCellImage(row, col, AlarmImg[imageno]);

        }


        private DRAG_SITUATION  DragSituation(int row, int col)
        {
            DRAG_SITUATION ret = DRAG_SITUATION.NA;

            // 移動後の日付
            DateTime changeDate = 列から日付を取得(col);

            // 移動データ確認
            var data = c1FlexGrid1.GetData(row, col) as SimFormData;

            string shubetuName = DragData.KInfo.KanidouseiInfoShubetuName;
            if (shubetuName == MsKanidouseiInfoShubetu.積み)
            {
                if (changeDate < DragData.KInfo.EventDate)
                {
                    // 積みを過去方向へ移動
                    ret = DRAG_SITUATION.TumiPast;
                }
                else
                {
                    // 積みを未来方向へ移動
                    ret = DRAG_SITUATION.TumiFuture;
                }
            }
            else if (shubetuName == MsKanidouseiInfoShubetu.揚げ)
            {
                if (changeDate < DragData.KInfo.EventDate)
                {
                    // 揚げを過去方向へ移動
                    ret = DRAG_SITUATION.AgePast;
                }
                else
                {
                    // 揚げを未来方向へ移動
                    ret = DRAG_SITUATION.AgeFuture;
                }
            }
            return ret;
        }


        private List<DjDousei> GetCheckDousei(DRAG_SITUATION situation, DateTime changeDate)
        {
            List<DjDousei> ret = new List<DjDousei>();

            DjDousei tumiDousei = null;
            DjDousei ageDousei = null;

            if (situation == DRAG_SITUATION.TumiPast || situation == DRAG_SITUATION.AgePast)
            {
                if (situation == DRAG_SITUATION.TumiPast)
                {
                    MessageBox.Show("積みを過去方向へ移動");

                    // 対象：この積み、ひとつ前の揚げ
                    tumiDousei = DragData.DInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み).OrderBy(o => o.DouseiDate).First();
                }

                else
                {
                    MessageBox.Show("揚げを過去方向へ移動");


                    // 対象：この揚げに紐づく積み、ひとつ前の揚げ
                    if (DragData.DInfos.Any(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み))
                    {
                        tumiDousei = DragData.DInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み).OrderBy(o => o.DouseiDate).First();

                        // 積みの移動日数を揚げに適用
                        int diff = (changeDate - DateTimeUtils.ToFrom(DragData.EventDateDisp)).Days;
                        changeDate = tumiDousei.DouseiDate.AddDays(diff);

                        MessageBox.Show($"紐づく積み移動（{tumiDousei.DouseiDate}）⇒（{changeDate}）");
                    }
                    else
                    {
                        MessageBox.Show($"紐づく積みなし");
                    }
                }

                var checkList = DouseiInfoSimList.Where(o => o.EventDateDisp < changeDate && o.KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ);
                if (checkList.Count() > 0)
                {
                    var douseiInfo = checkList.OrderByDescending(o => o.KInfo.EventDate).First();
                    ageDousei = douseiInfo.DInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ).OrderByDescending(o => o.DouseiDate).First();
                }
                else
                {
                    // 一つ前の揚げがこの画面内にない場合
                    for (int i = 1; i <= 5; i++)
                    {
                        if (ageDousei != null)
                            break;

                        DateTime checkDate = ColStartDate.AddDays(-i);
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            DateTime minDate = DateTime.MinValue;
                            List<DjDousei> douseis = serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, Vessel.MsVesselID, checkDate);
                            foreach (DjDousei d in douseis)
                            {
                                if (d.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
                                {
                                    if (d.DouseiDate > minDate)
                                    {
                                        minDate = d.DouseiDate;
                                        ageDousei = d;
                                    }
                                }
                            }
                        }
                    }
                }

                ret.Add(ageDousei);
                ret.Add(tumiDousei);
            }
            else if (situation == DRAG_SITUATION.TumiFuture || situation == DRAG_SITUATION.AgeFuture)
            {
                if (situation == DRAG_SITUATION.TumiFuture)
                {
                    MessageBox.Show("積みを未来方向へ移動");

                    // 対象：この積みに紐づく揚げ、次の積み
                    if (DragData.DInfos.Any(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ))
                    {
                        ageDousei = DragData.DInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ).OrderByDescending(o => o.DouseiDate).First();

                        // 積みの移動日数を揚げに適用
                        int diff = (changeDate - DateTimeUtils.ToFrom(DragData.EventDateDisp)).Days;
                        changeDate = ageDousei.DouseiDate.AddDays(diff);

                        MessageBox.Show($"紐づく揚げ移動（{ageDousei.DouseiDate}）⇒（{changeDate}）");
                    }
                    else
                    {
                        MessageBox.Show($"紐づく揚げなし");
                    }
                }

                else
                {
                    MessageBox.Show("揚げを未来方向へ移動");

                    // 対象：この揚げ、次の積み
                    ageDousei = DragData.DInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ).OrderByDescending(o => o.DouseiDate).First();
                }

                var checkList = DouseiInfoSimList.Where(o => o.EventDateDisp > changeDate && o.KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み);
                if (checkList.Count() > 0)
                {
                    var douseiInfo = checkList.OrderBy(o => o.KInfo.EventDate).First();
                    tumiDousei = douseiInfo.DInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み).OrderBy(o => o.DouseiDate).First();
                }
                else
                {
                    // 次の積みがこの画面内にない場合
                    for (int i = 1; i <= 5; i ++)
                    {
                        if (tumiDousei != null)
                            break;

                        DateTime checkDate = ColStartDate.AddDays((Days - 1) + i);
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            DateTime minDate = DateTime.MinValue;
                            List<DjDousei> douseis = serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, Vessel.MsVesselID, checkDate);
                            foreach(DjDousei d in douseis)
                            {
                                if (d.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID)
                                {
                                    if (d.DouseiDate > minDate)
                                    {
                                        minDate = d.DouseiDate;
                                        tumiDousei = d;
                                    }
                                }
                            }
                        }
                    }
                }

                ret.Add(ageDousei);
                ret.Add(tumiDousei);
            }



            return ret;
        }



        /// <summary>
        /// 指定セルが移動できるならtrue
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        #region private bool 移動できるか(int row, int col)
        //private bool 移動できるか(int row, int col)
        //{
        //    bool ret = true;

        //    // 移動後の日付
        //    DateTime changeDate = 列から日付を取得(col);

        //    // 移動データ確認
        //    var data = c1FlexGrid1.GetData(row, col) as SimFormData;

        //    string shubetuName = DragData.KInfo.KanidouseiInfoShubetuName;
        //    if (shubetuName == MsKanidouseiInfoShubetu.積み)
        //    {
        //        if (changeDate < DragData.KInfo.EventDate)
        //        {
        //            MessageBox.Show("積みを過去方向へ移動");
        //        }
        //        else
        //        {
        //            MessageBox.Show("積みを未来方向へ移動");

        //            // この場合、この積みのみ移動させる
        //        }
        //    }
        //    else if (shubetuName == MsKanidouseiInfoShubetu.揚げ)
        //    {
        //        if (changeDate < DragData.KInfo.EventDate)
        //        {
        //            MessageBox.Show("揚げを過去方向へ移動");

        //            // この場合、積みも移動させる
        //        }
        //        else
        //        {
        //            MessageBox.Show("揚げを未来方向へ移動");

        //            // この場合、この揚げのみ移動させる

        //            bool checkFlag = false;
        //            int checkMax = col + (ColStartDate.AddDays(Days) - changeDate).Days;
        //            foreach(int checkRow in RowMgTbl[1])
        //            {
        //                if (checkFlag)
        //                    break;

        //                for (int checkCell = col + 1; checkCell <= checkMax; checkCell++)
        //                {
        //                    if (checkFlag)
        //                        break;

        //                    if (c1FlexGrid1.GetData(checkRow, checkCell) !=null)
        //                    {
        //                        var nextData = c1FlexGrid1.GetData(checkRow, checkCell) as SimFormData;

        //                        if (nextData.KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み)
        //                        {
        //                            MessageBox.Show($"　次港：{nextData.KInfo.BashoName}");
        //                            MessageBox.Show($"　距離確認：{DragData.KInfo.BashoName} － {nextData.KInfo.BashoName}");

        //                            var nextBashoId = nextData.KInfo.MsBashoId;

        //                            var minutes = DouseiProc.MinutesToNextPort(Vessel, DragData.KInfo.MsBashoId, nextBashoId);

        //                            MessageBox.Show($"次の港までの時間：{minutes}");

        //                            if (changeDate.AddDays(1).AddMinutes(-1).AddMinutes(minutes) > nextData.KInfo.EventDate)
        //                            {
        //                                MessageBox.Show($"次の予定に間に合わないのでNG（次港到着時間：{changeDate.AddDays(1).AddMinutes(-1).AddMinutes(minutes)}）");
        //                                ret = false;
        //                            }
        //                            checkFlag = true; ;
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    return ret;
        //}
        #endregion
        #region private bool 移動できるか(int row, int col)
        private bool 移動できるか(DRAG_SITUATION situation, int diff, List<DjDousei> douseis)
        {
            bool ret = true;

            if (douseis.Count != 2)
                return ret;

            var age = douseis[0];
            var tumi = douseis[1];

            if (situation == DRAG_SITUATION.TumiPast || situation == DRAG_SITUATION.AgePast)
            {
                if (age == null)
                {
                    MessageBox.Show($"　前の揚げがない");
                    return ret;
                }
            }
            else if(situation == DRAG_SITUATION.TumiFuture || situation == DRAG_SITUATION.AgeFuture)
            {
                if (tumi == null)
                {
                    MessageBox.Show($"　次の積みがない");
                    return ret;
                }
            }


            MessageBox.Show($"　距離確認：{age.BashoName} － {tumi.BashoName}");

            var minutes = DouseiProc.MinutesToNextPort(Vessel, age.MsBashoID, tumi.MsBashoID);

            TimeSpan s = new TimeSpan(0,(int)minutes,0);
            MessageBox.Show($"移動時間：{s}");


            MessageBox.Show($"  揚げ：{age.DouseiDate} - 積み：{tumi.DouseiDate} ");

            if (situation == DRAG_SITUATION.TumiPast || situation == DRAG_SITUATION.AgePast)
            {
                MessageBox.Show($"  揚げ：{age.DouseiDate} - 揚げ+移動時間：{age.DouseiDate.AddMinutes(minutes)} - 移動した積み：{tumi.DouseiDate.AddDays(diff)} ");

                if (age.DouseiDate.AddMinutes(minutes) > tumi.DouseiDate.AddDays(diff))
                {
                    MessageBox.Show($"間に合わないのでNG");
                    ret = false;
                }
            }
            else if (situation == DRAG_SITUATION.TumiFuture || situation == DRAG_SITUATION.AgeFuture)
            {
                MessageBox.Show($"  揚げ：{age.DouseiDate} - 移動した揚げ：{age.DouseiDate.AddDays(diff)}- 移動した揚げ+移動時間：{age.DouseiDate.AddDays(diff).AddMinutes(minutes)} - 積み：{tumi.DouseiDate.AddDays(diff)} ");

                if (age.DouseiDate.AddDays(diff).AddMinutes(minutes) > tumi.DouseiDate)
                {
                    MessageBox.Show($"間に合わないのでNG");
                    ret = false;
                }
            }


            return ret;
        }
        #endregion

        /// <summary>
        /// 指定列の日付を取得
        /// </summary>
        /// <param name="gcol"></param>
        /// <returns></returns>
        #region private DateTime 列から日付を取得(int gcol)
        private DateTime 列から日付を取得(int gcol)
        {
            //if (!(c1FlexGrid1.GetData(0, gcol) is DateTime)) return DateTime.MinValue;
            //return (DateTime)c1FlexGrid1.GetData(0, gcol);

            return ColStartDate.AddDays(gcol - 1);
        }
        #endregion



        /// <summary>
        /// データクラス
        /// </summary>
        #region public class SimFormData
        public class SimFormData
        {
            /// <summary>
            /// VesselID
            /// </summary>
            public int MsVesselId { set; get; }

            /// <summary>
            /// 画面に表示されている日付
            /// </summary>
            public DateTime EventDateDisp { set; get; }

            /// <summary>
            /// PtKanidouseiInfoクラス
            /// </summary>
            public PtKanidouseiInfo KInfo { set; get; }

            /// <summary>
            /// セルに表示する文字列
            /// </summary>
            public string DispStr { set; get; }

            /// <summary>
            /// 揚げ積みDouseiのリスト
            /// </summary>
            public List<DjDousei> DInfos { set; get; }

            /// <summary>
            /// アラーム24H
            /// </summary>
            public bool Alarm24H { set; get; }

            /// <summary>
            /// アラーム1week
            /// </summary>
            public bool Alarm1W { set; get; }

            /// <summary>
            /// アラーム4week
            /// </summary>
            public bool Alarm4W { set; get; }

            /// <summary>
            /// アラームRest
            /// </summary>
            public bool AlarmRest { set; get; }


            /// <summary>
            /// シミュレーションならtrue
            /// </summary>
            public bool IsTemp;

            /// <summary>
            /// シミュレーション表示で使う
            /// </summary>
            public int RowSim;

            public SimFormData()
            {
                MsVesselId = 0;
                KInfo = null;
                DispStr = "";

                DInfos = new List<DjDousei>();


                Alarm24H = false;
                Alarm1W = false;
                Alarm4W = false;
                AlarmRest = false;
            }

            public override string ToString()
            {
                if (KInfo == null) return "";

                if (KInfo.BashoName.Length > 6)
                {
                    DispStr = KInfo.BashoName.Substring(0, 6);
                }
                else
                {
                    DispStr = KInfo.BashoName;
                }
                if (KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み || KInfo.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ)
                {
                    if (KInfo.KitiName != null && KInfo.KitiName.Length > 0)
                    {
                        if (KInfo.KitiName.Length > 6)
                        {
                            DispStr += " " + KInfo.KitiName.Substring(0, 6);
                        }
                        else
                        {
                            DispStr += " " + KInfo.KitiName;
                        }
                    }

                    DispStr += System.Environment.NewLine;

                    if (KInfo.MsCargoName != null && KInfo.MsCargoName.Length > 0)
                    {
                        if (KInfo.MsCargoName.Length > 6)
                        {
                            DispStr += " " + KInfo.MsCargoName.Substring(0, 6);
                        }
                        else
                        {
                            DispStr += " " + KInfo.MsCargoName;
                        }
                    }
                    if (KInfo.Qtty > 0)
                    {
                        DispStr += " " + KInfo.Qtty.ToString(".000");
                    }
                }
                return DispStr;
            }

            static public SimFormData Clone(SimFormData d)
            {
                SimFormData ret = new SimFormData();
                ret.MsVesselId = d.MsVesselId;
                ret.EventDateDisp = d.EventDateDisp;
                ret.KInfo = d.KInfo;
                ret.DispStr = d.DispStr;

                ret.DInfos = new List<DjDousei>();
                if (d.DInfos != null)
                {
                    ret.DInfos.AddRange(d.DInfos);
                }
                ret.Alarm24H = d.Alarm24H;
                ret.Alarm1W = d.Alarm1W;
                ret.Alarm4W = d.Alarm4W;
                ret.AlarmRest = d.AlarmRest;

                ret.IsTemp = d.IsTemp;
                ret.RowSim = d.RowSim;

                return ret;
            }
        }
        #endregion

       
    }
}
