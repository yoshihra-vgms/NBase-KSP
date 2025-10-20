using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public partial class MitsumoriKaitou : System.Web.UI.Page
{
    /// <summary>
    /// この行数を超えた場合、次の品目は次のページにする
    /// </summary>
    private int maxRowNum_Page = 2;

    #region バリデーション・桁制限
    /// <summary>
    /// 単価入力桁制限
    /// </summary>
    private int InputLen_Price = 9;
    /// <summary>
    /// 数量入力桁制限
    /// </summary>
    private int InputLen_Count = 5;
    /// <summary>
    /// 名称入力桁制限
    /// </summary>
    private int InputLen_Name = 500;
    /// <summary>
    /// 備考入力桁制限
    /// </summary>
    private int InputLen_Remark = 500;

    #endregion

    // 品目テーブルのカラムindex
    private enum tableHimoku_ColID
    {
        AddDel = 0,
        No,
        Kubun,
        Name,
        Unit,
        Count,
        Price,
        Amount,
        Attach,
        Remark,
        Type,
        NextType,
        ID,
        NextID
    };

    // 依頼種別
    private enum Iraisyubetu
    {
        修繕,
        燃料潤滑油,
        船用品
    };

    private string msUserID_Customer = "1";

    private int width_textBox_Name = 315;

    private void Page_Init(object sender, EventArgs e)
    {
        string maxNoStr = ConfigurationManager.AppSettings["品目テーブル最大行数"];
        if (maxNoStr != null && maxNoStr.Length != 0)
        {
            maxRowNum_Page = int.Parse(maxNoStr);
        }
    }

    // 信頼できないSSL証明書を「問題なし」にするメソッド
    private bool OnRemoteCertificateValidationCallback(
      Object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
        return true;  // 「SSL証明書の使用は問題なし」と示す
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //ServicePointManager.ServerCertificateValidationCallback =
        //  new RemoteCertificateValidationCallback(
        //    OnRemoteCertificateValidationCallback);


        if (Session[MakeSessionKey("CustomerID")] == null)
        {
            // ページ移動
            string webkey = Request.QueryString["WebKey"];
            string url = "../NBaseVendor/Default.aspx?WebKey=" + webkey;
            Response.Redirect(url);
        }

        //Button_DelHinmoku.Attributes["OnClick"] = "if( confirm('チェックされた品目を削除してよろしいですか？') == false ) return false;";
        //Button_DelShousai.Attributes["OnClick"] = "if( confirm('チェックされた詳細を削除してよろしいですか？') == false ) return false;";
        Button_Commit.Attributes["OnClick"] = "if( checkTaxAmount() == false ) if( confirm('税額が０ですが、提出しますか？') == false ) return false;";
        Button_AddHinmoku.Attributes["OnClick"] = "if( checkHeaderInput() == false ) return false;"; 


        if (!this.IsPostBack)
        {
            string webKey = this.Request.QueryString["WebKey"];
            if (webKey != null && webKey != string.Empty)
            {
                ShowPage(webKey);
            }
        }
        else
        {
            // PostBack時
            // テーブルを作る
            MakeHinmokuTableForPostBack();
        }

        ControlButton();
    }

    /// <summary>
    /// WebKeyに対応するデータの取得とその表示
    /// </summary>
    /// <param name="webKey"></param>
    protected void ShowPage(string webKey)
    {
        #region データ取得
        NBaseData.DAC.OdMk odMK = null;
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList = null;
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList= null;
        NBaseData.DAC.MsVessel vessel = null;
        NBaseData.DAC.OdMm odMM = null;
        NBaseData.DAC.OdThi odthi = null;
        NBaseData.DAC.MsShrJouken msShrJouken = new NBaseData.DAC.MsShrJouken();

        decimal tax = 0;

        NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
        using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
        {
            msUser.LoginID = "1";
            odMK = serviceClient.OdMk_GetRecordByWebKey(msUser, webKey);

            odMM = serviceClient.OdMm_GetRecord(msUser, odMK.OdMmID);
            vessel = serviceClient.MsVessel_GetRecordsByMsVesselID(msUser, odMM.VesselID);
            odthi = serviceClient.OdThi_GetRecord(msUser, odMM.OdThiID);

            odMkItemList = serviceClient.OdMkItem_GetRecordsByOdMkID(msUser, odMK.OdMkID);
            odMkShousaiList = serviceClient.OdMkShousaiItem_GetRecordByOdMkID(msUser, odMK.OdMkID);

            System.Collections.Generic.List<NBaseData.DAC.MsShrJouken> msShrJoukenList
                = serviceClient.MsShrJouken_GetRecords(msUser);
            foreach (NBaseData.DAC.MsShrJouken sj in msShrJoukenList)
            {
                if (sj.MsShrJoukenID == odMM.MsShrJoukenID)
                {
                    msShrJouken = sj;
                    break;
                }
            }

            //if (odMK.Status == 0)
            //{
            //    tax = NBaseCommon.Common.Default消費税率();
            //}
            //else
            {
                tax = odMK.Tax;
            }
            // 2012.05.15: NBaseServiceに依頼するとNBaseService上にファイルが作成されてしまうのでコメントとする
            //bool ret = serviceClient.BLC_添付ファイル作成(msUser, odMK.OdMkID);
        }
        #endregion
        // 2012.05.15: NBaseServiceに依頼するとNBaseService上にファイルが作成されてしまうのでローカルで処理する
        bool ret = NBaseData.BLC.添付ファイル.作成(msUser, odMK.OdMkID);

        #region 表示
        if (odMK != null)
        {
            Session[MakeSessionKey("見積済")] = odMK.Status;

            Label_会社.Text = odMK.MsCustomerName;
            TextBox_担当者.Text = odMK.Tantousha;

            TextBox_MK_NO.Text = odMK.MkNo;
            TextBox_VesselName.Text = (vessel != null) ? vessel.VesselName : string.Empty;
            HiddenField_VesselID.Value = (vessel != null) ? vessel.MsVesselID.ToString() : string.Empty;
            TextBox_Naiyou.Text = odMK.OdThiNaiyou;//"ODTHIの依頼内容";
            TextBox_MKKigen.Text = odMK.MkKigen;
            TextBox_Jouken.Text = msShrJouken.ShiharaiJoukenName;//odMM.MsShrJoukenID;//MSSHRJOUKENより名称をとる
            TextBox_Okurisaki.Text = odMM.Okurisaki;
            TextBox_YuukouKigen.Text = odMK.MkYukouKigen;
            TextBox_Nouki_Year.Text = (odMK.Nouki != DateTime.MinValue) ? odMK.Nouki.ToString("yyyy") : string.Empty;
            TextBox_Nouki_Month.Text = (odMK.Nouki != DateTime.MinValue) ? odMK.Nouki.ToString("MM") : string.Empty;
            TextBox_Nouki_Day.Text = (odMK.Nouki != DateTime.MinValue) ? odMK.Nouki.ToString("dd") : string.Empty;

            Iraisyubetu iraitype = Iraisyubetu.修繕;
            if (odthi.ThiIraiSbtName == "燃料・潤滑油")
            {
                iraitype = Iraisyubetu.燃料潤滑油;
            }
            if (odthi.ThiIraiSbtName == "船用品")
            {
                iraitype = Iraisyubetu.船用品;
            }
            Session[MakeSessionKey("依頼種別")] = iraitype;
            HiddenField_IraiType.Value = iraitype.ToString(); ;

            #region 20091007 船用品のときは区分を表示しない為の処理
            if (iraitype == Iraisyubetu.船用品)
            {
                Table_Hinmoku_Head.Rows[0].Cells[2].Visible = false;
                Table_Hinmoku_Head.Width = 1240 - 92;
                Table_Hinmoku.Width = Table_Hinmoku_Head.Width;
            }
            #endregion

            showHinmoku_onPage(0, odMkItemList, odMkShousaiList, iraitype);
            #region 全件表示してたときの処理(不要)
            //foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
            //{
            //    AddRow_Himoku(item);

            //    // ITEMの詳細追加
            //    foreach (NBaseData.DAC.OdMkShousaiItem s_item in odMkShousaiList)
            //    {
            //        if (item.OdMkItemID == s_item.OdMkItemID)
            //        {
            //            // 行に追加
            //            AddRow_HimokuShousai(s_item);
            //        }
            //    }
            //}
            #endregion

            Label_Mitsumorigaku.Text = odMK.Amount.ToString();
            HiddenField_Mitumorigaku.Value = odMK.Amount.ToString();
            TextBox_Nebiki.Text = odMK.MkAmount.ToString();
            TextBox_Tax.Text = odMK.Tax.ToString();
            Label_Goukei.Text = (odMK.Amount - odMK.MkAmount + odMK.Tax).ToString();

            if (odMK.Status == 0)
            {
                Button_Commit.Visible = true;
                Button_Commit.Visible = true;
                //Button_MovePage.Visible = false;

                Table_Buttons.Visible = true;
            }
            else
            {
                Button_Commit.Visible = false;
                Button_Save.Visible = false;
                Button_ExportCSV.Visible = false;
                Button_ImportCSV.Visible = false;
                FileUpload_CSV.Visible = false;

                Table_Buttons.Visible = false;

                Button_AddHinmoku.Enabled = false;
                Button_AddHinmoku.Visible = false;
                TextBox_Header.Visible = false;

                TextBox_担当者.Attributes.Add("readonly", "true");
                TextBox_Nebiki.Attributes.Add("readonly", "true");
                TextBox_Tax.Attributes.Add("readonly", "true");
                TextBox_YuukouKigen.Attributes.Add("readonly", "true");
                TextBox_Nouki_Year.Attributes.Add("readonly", "true");
                TextBox_Nouki_Month.Attributes.Add("readonly", "true");
                TextBox_Nouki_Day.Attributes.Add("readonly", "true");
            }

        }

        #endregion

        #region セッションに情報記録(初回)
        Session[MakeSessionKey("LIST_ODMKITEM")] = odMkItemList;
        Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")] = odMkShousaiList;
        #endregion
    }

    /// <summary>
    /// PostBack時にテーブルを作り直す
    /// </summary>
    private void MakeHinmokuTableForPostBack()
    {
        Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];

        foreach(ListItem item in ListBox_RowID.Items)
        {
            string[] iv = item.Value.Split(':');
            string type = iv[0];
            string id = iv[1];
            string category = "";// 20101129ADD categoryの追加(AddRow_HimokuShousaiの内部分岐の為)

            switch (type)
            {
                case "1":// 品目
                    AddRow_Himoku(id, irai_type);
                    break;
                case "2":// 品目詳細
                    if (iv.Length >= 3) category = iv[2];// 20101129ADD categoryの追加(AddRow_HimokuShousaiの内部分岐の為)
                    AddRow_HimokuShousai(id, irai_type, category);
                    break;
                default:
                    break;
            }
        }

        // PageIndexを作り直す
        if (Session[MakeSessionKey("MAX_PAGE_NO")] != null)
        {
            int pageno = (int)Session[MakeSessionKey("MAX_PAGE_NO")];
            int curpageno = (Session[MakeSessionKey("CUR_PAGE_NO")] != null) ? (int)Session[MakeSessionKey("CUR_PAGE_NO")] : 1;

            makePageIndex(pageno, curpageno);
        }
    }

    private void AddRow_Himoku(NBaseData.DAC.OdMkItem item, Iraisyubetu irai_type)
    {
        string webkey = Request.QueryString["WebKey"];

        #region ROW生成

        string id = string.Empty;
        if (item == null)
        {
            id = Guid.NewGuid().ToString();
        }
        else
        {
            id = item.OdMkItemID;
        }

        TableRow row = new TableRow();
        row.ID = "row_" + id;
        for (int i = 0; i < Table_Hinmoku_Head.Rows[0].Cells.Count; i++)
        {
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            cell.Visible = Table_Hinmoku_Head.Rows[0].Cells[i].Visible;//可視指定
            cell.Width = Table_Hinmoku_Head.Rows[0].Cells[i].Width;//幅指定
        }

        #region 各セルにコントロールを追加

        #region 見積済みかstatusを取得
        int status = 0;
        if (Session[MakeSessionKey("見積済")] != null)
        {
            status = (int)Session[MakeSessionKey("見積済")];
        }
        #endregion

        {
            CheckBox cb = new CheckBox();
            cb.ID = "CB_ROW_" + id;
            //row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(cb);

            Button DelBtn = new Button();
            DelBtn.ID = "DEL_" + id;
            DelBtn.Text = "削除";
            DelBtn.Click += new EventHandler(DelBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(DelBtn);
            Button AddBtn = new Button();
            AddBtn.ID = "ADD_" + id;
            AddBtn.Text = "追加";
            AddBtn.Click += new EventHandler(AddBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(AddBtn);

            if (item != null &&
                (item.OdMmItemID != null &&
                item.OdMmItemID != string.Empty))
            {
                //ボタン使用不可
                DelBtn.Enabled = false;
                AddBtn.Enabled = false;
            }
            if (status != 0)
            {
                // 見積済みなので使用不可
                DelBtn.Enabled = false;
                AddBtn.Enabled = false;
            }
        }

        {
            Label L1 = new Label();
            L1.ID = "LB_NO_ROW_" + id;
            L1.Text = "&nbsp;";
            row.Cells[(int)tableHimoku_ColID.No].Text = "&nbsp;";
            row.Cells[(int)tableHimoku_ColID.No].Controls.Add(L1);
        }

        // 区分
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "TB_KUBUN_ROW_" + id;
            ddl.Width = 90;
            row.Cells[(int)tableHimoku_ColID.Kubun].Controls.Add(ddl);

            #region 単位の選択設定
            System.Collections.Generic.List<NBaseData.DAC.MsItemSbt> sbtList = getItemSbtList();
            foreach (NBaseData.DAC.MsItemSbt sbt in sbtList)
            {
                ListItem li = new ListItem(sbt.ItemSbtName, sbt.MsItemSbtID);
                ddl.Items.Add(li);
            }
            #endregion

            if (item != null &&
                item.MsItemSbtID != null && item.MsItemSbtID.Length != 0)
            {
                ddl.SelectedValue = item.MsItemSbtID;
            }

            if (irai_type == Iraisyubetu.燃料潤滑油)
            {
                ddl.Enabled = false;
            }
            if (status != 0)
            {
                // 見積済みなので読み込み専用
                ddl.Enabled = false;
            }
            if (item != null &&
                (item.OdMmItemID != null &&
                item.OdMmItemID != string.Empty))
            {
                // 事務所側追加分について変更不可
                ddl.Enabled = false;
            }
        }

        // 名称
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_NAME_ROW_" + id;
            T1.Width = width_textBox_Name;
            T1.TextMode = TextBoxMode.MultiLine;
            T1.Rows = 3;
            T1.MaxLength = InputLen_Name;
            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
            if (irai_type == Iraisyubetu.船用品)
            {
                T1.Attributes.Add("readonly", "true");
            }
            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T1.Attributes.Add("readonly", "true");
            }
            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

            if (item != null)
            {
                T1.Text = (item.ItemName != string.Empty) ? item.ItemName : item.MsItemSbtName;
                //T1.ReadOnly = true;
            }
            if (item != null &&
                (item.OdMmItemID != null &&
                item.OdMmItemID != string.Empty))
            {
                //使用不可
                T1.ReadOnly = true;
            }
        }

        // 備考
        {
            TextBox T2 = new TextBox();
            T2.ID = "TB_REMARK_ROW_" + id;
            T2.Width = 345;
            T2.TextMode = TextBoxMode.MultiLine;
            T2.Rows = 3;
            T2.MaxLength = InputLen_Remark;
            T2.Attributes.Add("onkeydown", "return checkTextLen('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            T2.Attributes.Add("onkeyup", "cutText('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            row.Cells[(int)tableHimoku_ColID.Remark].Controls.Add(T2);

            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T2.Attributes.Add("readonly", "true");
            }
            if (item != null)
            {
                T2.Text = item.Bikou;
            }
        }

        // TYPE この行が品目か品目詳細か
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_TYPE_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.Type].Controls.Add(T3);

            // ここでは品目
            T3.Text = "1";
        }
        // 次行が品目か品目詳細か ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXT_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextType].Controls.Add(T3);
        }
        // この行のデータのID(DBのKEY=GUID)
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_ID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.ID].Controls.Add(T3);

            T3.Text = id;
        }
        // 次行のデータのID(DBのKEY=GUID) ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXTID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextID].Controls.Add(T3);
        }

        {
            // セル埋め
            row.Cells[(int)tableHimoku_ColID.Unit].Text = "&nbsp";
            row.Cells[(int)tableHimoku_ColID.Count].Text = "&nbsp";
            row.Cells[(int)tableHimoku_ColID.Price].Text = "&nbsp";
            row.Cells[(int)tableHimoku_ColID.Amount].Text = "&nbsp";
        }

        // 添付ファイル
        {
            if (item != null && item.OdAttachFileID != null && item.OdAttachFileID.Length > 0)
            {
                NBaseData.DAC.OdAttachFile attachFile = null;
                using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
                {
                    NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
                    msUser.LoginID = "1";
                    attachFile = serviceClient.OdAttachFile_GetRecordNoData(msUser, item.OdAttachFileID);
                }

                HyperLink L1 = new HyperLink();
                L1.ID = "LB_ATTACH_ROW_" + id;
                L1.Width = 40;
                L1.Text = "〇";
                L1.NavigateUrl = "AttachFiles/" + DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + "/" + item.OdAttachFileID + "_" + attachFile.FileName;
                L1.Target = "_blank";
                row.Cells[(int)tableHimoku_ColID.Attach].Controls.Add(L1);
            }
            else
            {
                row.Cells[(int)tableHimoku_ColID.Attach].Text = "&nbsp";
            }
        }

        #endregion

        #endregion

        Table_Hinmoku.Rows.Add(row);
        ListBox_RowID.Items.Add("1" + ":" + id);
    }

    void AddBtn_Click(object sender, EventArgs e)
    {
        // セッション更新
        updateSession();

        Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];

        Button pushedBtn = (Button)sender;

        int index = 0;
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            if (row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Count == 2)// Addボタンのある行(品目行)だけ処理する
            {
                Button addBtn = (Button)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[1];

                if (pushedBtn.ID == addBtn.ID)
                {
                    // 行の種類を取得
                    string[] tmpListVal = ListBox_RowID.Items[index].Value.Split(':');
                    // 品目の名称取得(カテゴリ検索に使用)
                    TextBox ctgText = (TextBox)row.Cells[(int)tableHimoku_ColID.Name].Controls[0];
                    string category = ctgText.Text;

                    // 次の品目の行を取得して、その行に詳細を入れる
                    #region 次の品目のIndEXを取得
                    int nextIdx = index + 1;
                    for (int j = index + 1; j < Table_Hinmoku.Rows.Count; j++)
                    {
                        tmpListVal = ListBox_RowID.Items[nextIdx].Value.Split(':');
                        string tmpType = tmpListVal[0];
                        if (tmpType == "1")
                        {
                            nextIdx = j;
                            break;
                        }
                        else
                        {
                            nextIdx = j + 1;//詳細行のときはその次の行が入れる行になる(詳細が最終行のとき必要になる)
                        }
                    }
                    #endregion

                    //取得したINDEXの場所に詳細を挿入 AddAt
                    InsertRow_HimokuShousai(nextIdx, null, irai_type, category);

                    break;
                }
            }
            index++;
        }

        #region 番号振りなおし
        int rowNo = -1;
        // Sessionから情報を取込
        int curPage = (Session[MakeSessionKey("CUR_PAGE_NO")] != null) ? (int)Session[MakeSessionKey("CUR_PAGE_NO")] : 1;
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList 
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        int startRowNo = calc_1stRowNo_onPage(curPage, odMkItemList, odMkShousaiList);
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            if (row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Count == 1)// Addボタンのない行(詳細行)だけ処理する
            {
                Label lb = (Label)row.Cells[(int)tableHimoku_ColID.No].Controls[0];
                if (rowNo == -1)
                {
                    try
                    {
                        rowNo = int.Parse(lb.Text);
                    }
                    catch
                    {
                        rowNo = startRowNo;
                    }
                    lb.Text = rowNo.ToString();
                }
                else
                {
                    lb.Text = (++rowNo).ToString();
                }
            }
        }
        #endregion

    }

    /// <summary>
    /// テーブル再構成用(POSTBACK)
    /// </summary>
    /// <param name="id"></param>
    private void AddRow_Himoku(string id, Iraisyubetu irai_type)
    {
        #region ROW生成

        TableRow row = new TableRow();
        row.ID = "row_" + id;
        for (int i = 0; i < Table_Hinmoku_Head.Rows[0].Cells.Count; i++)
        {
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            cell.Visible = Table_Hinmoku_Head.Rows[0].Cells[i].Visible;//可視指定
            cell.Width = Table_Hinmoku_Head.Rows[0].Cells[i].Width;//幅指定
        }

        #region 20101210ADD
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList 
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        #endregion

        #region 各セルにコントロールを追加

        #region 見積済みかstatusを取得
        int status = 0;
        if (Session[MakeSessionKey("見積済")] != null)
        {
            status = (int)Session[MakeSessionKey("見積済")];
        }
        #endregion

        {
            CheckBox cb = new CheckBox();
            cb.Enabled = false;
            cb.ID = "CB_ROW_" + id;
            //row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(cb);

            Button DelBtn = new Button();
            DelBtn.ID = "DEL_" + id;
            DelBtn.Text = "削除";
            DelBtn.Click += new EventHandler(DelBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(DelBtn);
            Button AddBtn = new Button();
            AddBtn.ID = "ADD_" + id;
            AddBtn.Text = "追加";
            AddBtn.Click += new EventHandler(AddBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(AddBtn);
            
            if (status != 0)
            {
                // 見積済みなので使用不可
                DelBtn.Enabled = false;
                AddBtn.Enabled = false;
            }
        }

        {
            Label L1 = new Label();
            L1.ID = "LB_NO_ROW_" + id;
            L1.Text = "&nbsp;";
            //row.Cells[(int)tableHimoku_ColID.No].Text = "&nbsp;";
            row.Cells[(int)tableHimoku_ColID.No].Controls.Add(L1);
        }

        // 区分
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "TB_KUBUN_ROW_" + id;
            ddl.Width = 90;
            row.Cells[(int)tableHimoku_ColID.Kubun].Controls.Add(ddl);

            #region 品目種別の選択設定
            System.Collections.Generic.List<NBaseData.DAC.MsItemSbt> sbtList = getItemSbtList();
            foreach (NBaseData.DAC.MsItemSbt sbt in sbtList)
            {
                ListItem li = new ListItem(sbt.ItemSbtName, sbt.MsItemSbtID);
                ddl.Items.Add(li);
            }
            #endregion

            if (irai_type == Iraisyubetu.燃料潤滑油)
            {
                ddl.Enabled = false;
            }
            if (status != 0)
            {
                // 見積済みなので読み込み専用
                ddl.Enabled = false;
            }

            // 20101210ADD
            NBaseData.DAC.OdMkItem item = null;
            foreach (NBaseData.DAC.OdMkItem odMkItem in odMkItemList)
            {
                if (id == odMkItem.OdMkItemID)
                {
                    item = odMkItem;
                }
            }
            if (item != null &&
                (item.OdMmItemID != null &&
                item.OdMmItemID != string.Empty))
            {
                // 事務所側追加分について変更不可
                ddl.Enabled = false;
            }
        }

        // 名称
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_NAME_ROW_" + id;
            T1.Width = width_textBox_Name;
            T1.TextMode = TextBoxMode.MultiLine;
            T1.Rows = 3;
            T1.MaxLength = InputLen_Name;
            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
            if (irai_type == Iraisyubetu.船用品)
            {
                T1.Attributes.Add("readonly", "true");
            }
            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T1.Attributes.Add("readonly", "true");
            }
            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
        }

        // 備考
        {
            TextBox T2 = new TextBox();
            T2.ID = "TB_REMARK_ROW_" + id;
            T2.Width = 345;
            T2.TextMode = TextBoxMode.MultiLine;
            T2.Rows = 3;
            T2.MaxLength = InputLen_Remark;
            T2.Attributes.Add("onkeydown", "return checkTextLen('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            T2.Attributes.Add("onkeyup", "cutText('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            row.Cells[(int)tableHimoku_ColID.Remark].Controls.Add(T2);

            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T2.Attributes.Add("readonly", "true");
            }
        }

        // TYPE この行が品目か品目詳細か
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_TYPE_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.Type].Controls.Add(T3);
        }
        // 次行が品目か品目詳細か ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXT_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextType].Controls.Add(T3);
        }
        // この行のデータのID(DBのKEY=GUID)
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_ID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.ID].Controls.Add(T3);
        }
        // 次行のデータのID(DBのKEY=GUID) ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXTID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextID].Controls.Add(T3);
        }

        {
            // セル埋め
            row.Cells[(int)tableHimoku_ColID.Unit].Text = "&nbsp";
            row.Cells[(int)tableHimoku_ColID.Count].Text = "&nbsp";
            row.Cells[(int)tableHimoku_ColID.Price].Text = "&nbsp";
            row.Cells[(int)tableHimoku_ColID.Amount].Text = "&nbsp";


            row.Cells[(int)tableHimoku_ColID.Attach].Text = "&nbsp"; // 添付ファイル
        }

        #endregion

        #endregion

        Table_Hinmoku.Rows.Add(row);
        //ListBox_RowID.Items.Add("1" + ":" + id);// 再構成のときはIDは追加しない

    }

    private void AddRow_HimokuShousai(NBaseData.DAC.OdMkShousaiItem item,
        NBaseData.DAC.OdMkItem mkitem, Iraisyubetu irai_type)
    {
        #region ROW生成

        string id = string.Empty;
        string category = string.Empty;// 20101129ADD リストに登録用(POSTBACK)
        if (item == null)
        {
            id = Guid.NewGuid().ToString();
        }
        else
        {
            id = item.OdMkShousaiItemID;
        }

        TableRow row = new TableRow();
        row.ID = "row_" + id;
        for (int i = 0; i < Table_Hinmoku_Head.Rows[0].Cells.Count; i++)
        {
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            cell.Visible = Table_Hinmoku_Head.Rows[0].Cells[i].Visible;//可視指定
            cell.Width = Table_Hinmoku_Head.Rows[0].Cells[i].Width;//幅指定
        }

        #region 各セルにコントロールを追加

        #region 見積済みかstatusを取得
        int status = 0;
        if (Session[MakeSessionKey("見積済")] != null)
        {
            status = (int)Session[MakeSessionKey("見積済")];
        }
        #endregion

        {
            Button DelBtn = new Button();
            DelBtn.ID = "DEL_" + id;
            DelBtn.Text = "削除";
            DelBtn.Click += new EventHandler(DelBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(DelBtn);

            if (item != null &&
                (item.OdMmShousaiItemID != null &&
                item.OdMmShousaiItemID != string.Empty))
            {
                //ボタン使用不可
                DelBtn.Enabled = false;
            }
            if (status != 0)
            {
                // 見積済みなので使用不可
                DelBtn.Enabled = false;
            }
        }

        {
            Label L1 = new Label();
            L1.ID = "LB_NO_ROW_" + id;
            L1.Text = " ";
            row.Cells[(int)tableHimoku_ColID.No].Controls.Add(L1);
        }

        // 区分(空カラム)
        {
            Label L1 = new Label();
            L1.ID = "LB_KUBUN_ROW_" + id;
            L1.Text = "&nbsp;";
            row.Cells[(int)tableHimoku_ColID.Kubun].Controls.Add(L1);
        }

        // 名称
        {
            #region 旧処理(現修繕：移植後消去予定)
            //TextBox T1 = new TextBox();
            //T1.ID = "TB_NAME_ROW_" + id;
            //T1.Width = width_textBox_Name;
            //T1.TextMode = TextBoxMode.MultiLine;
            //T1.Rows = 3;
            //row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

            //if (item != null)
            //{
            //    T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
            //    //T1.ReadOnly = true;
            //}
            #endregion

            switch (irai_type)
            {
                case Iraisyubetu.修繕:
                    #region 修繕の時のコントロール
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                        if (item != null)
                        {
                            T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                            //T1.ReadOnly = true;
                        }
                        if (status != 0)
                        {
                            // 見積済みなので読み込み専用
                            T1.Attributes.Add("readonly", "true");
                        }
                        if (item != null &&
                            (item.OdMmShousaiItemID != null &&
                            item.OdMmShousaiItemID != string.Empty))
                        {
                            //使用不可
                            T1.ReadOnly = true;
                        }
                    }
                    #endregion
                    break;
                case Iraisyubetu.船用品:
                    if (mkitem.ItemName == NBaseData.DAC.MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント.ToString())
                    {
                        // ペイントのときのみこちら
                        #region 船用品の時のコントロール
                        // 船用品はMS_VESSEL_ITEMから選択入力(VESSEL/カテゴリで選択を絞る)
                        // コントロールはテキストボックスとボタン
                        {
                            TextBox T1 = new TextBox();
                            T1.ID = "TB_NAME_ROW_" + id;
                            T1.Width = width_textBox_Name - 40;
                            T1.TextMode = TextBoxMode.MultiLine;
                            T1.Rows = 3;
                            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            //T1.ReadOnly = true;
                            T1.Attributes.Add("readonly", "true");
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                            Button btn = new Button();
                            btn.Text = "変更";
                            btn.ID = "BT_NAME_ROW_" + id;
                            btn.Width = 35;
                            btn.Click += new EventHandler(btn_Select船用品_Click);
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(btn);
                            HiddenField HF_Category = new HiddenField();
                            HF_Category.Value = "";
                            HF_Category.ID = "HF_CATG_ROW_" + id;
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(HF_Category);

                            if (item != null)
                            {
                                T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                                //T1.ReadOnly = true;
                                HF_Category.Value = mkitem.ItemName;
                            }
                            if (status != 0)
                            {
                                // 見積済みなので読み込み専用
                                T1.Attributes.Add("readonly", "true");
                            }
                            if (item != null &&
                                (item.OdMmShousaiItemID != null &&
                                item.OdMmShousaiItemID != string.Empty))
                            {
                                //使用不可
                                T1.ReadOnly = true;
                            }
                        }
                        #endregion
                        category = NBaseData.DAC.MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント.ToString();// 20101129ADD POSTBACK時に使用
                    }
                    else
                    {
                        // ペイント以外のとき
                        #region 修繕の時のコントロール
                        {
                            TextBox T1 = new TextBox();
                            T1.ID = "TB_NAME_ROW_" + id;
                            T1.Width = width_textBox_Name;
                            T1.TextMode = TextBoxMode.MultiLine;
                            T1.Rows = 3;
                            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                            if (item != null)
                            {
                                T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                                //T1.ReadOnly = true;
                            }
                            if (status != 0)
                            {
                                // 見積済みなので読み込み専用
                                T1.Attributes.Add("readonly", "true");
                            }
                            if (item != null &&
                                (item.OdMmShousaiItemID != null &&
                                item.OdMmShousaiItemID != string.Empty))
                            {
                                //使用不可
                                T1.ReadOnly = true;
                            }
                        }
                        #endregion
                    }
                    break;
                case Iraisyubetu.燃料潤滑油:
                    #region 燃料潤滑油の時のコントロール
                    // 燃料潤滑油は修繕と同じ。ここでは関係ないが、行の追加はなし
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                        if (item != null)
                        {
                            T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                            //T1.ReadOnly = true;
                        }
                        if (status != 0)
                        {
                            // 見積済みなので読み込み専用
                            T1.Attributes.Add("readonly", "true");
                        }
                        if (item != null &&
                            (item.OdMmShousaiItemID != null &&
                            item.OdMmShousaiItemID != string.Empty))
                        {
                            //使用不可
                            T1.ReadOnly = true;
                        }

                    }
                    #endregion
                    break;
                default:
                    #region 標準のコントロール(修繕と同じ)
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                        if (item != null)
                        {
                            T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                            //T1.ReadOnly = true;
                        }
                        if (status != 0)
                        {
                            // 見積済みなので読み込み専用
                            T1.Attributes.Add("readonly", "true");
                        }
                    }
                    #endregion
                    break;
            }
        }

        // 数量
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_QTTY_ROW_" + id;
            T1.Width = 75;
            T1.Attributes["OnChange"] = "checkNaN('" + T1.ID + "',0); calcAmount('" + id + "');return;";
            row.Cells[(int)tableHimoku_ColID.Count].Controls.Add(T1);
            T1.MaxLength = InputLen_Count;

            if (item != null)
            {
                T1.Text = item.Count.ToString();
                //T1.ReadOnly = true;
            }
            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T1.Attributes.Add("readonly", "true");
            }
            if (item != null &&
                (item.OdMmShousaiItemID != null &&
                item.OdMmShousaiItemID != string.Empty))
            {
                //使用不可
                //T1.ReadOnly = true;
            }
        }
        // 単位
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "TB_UNIT_ROW_" + id;
            ddl.Width = 60;
            row.Cells[(int)tableHimoku_ColID.Unit].Controls.Add(ddl);

            #region 単位の選択設定
            System.Collections.Generic.List<NBaseData.DAC.MsTani> taniList = getTaniList();
            foreach (NBaseData.DAC.MsTani tani in taniList)
            {
                ListItem li = new ListItem(tani.TaniName, tani.MsTaniID);
                ddl.Items.Add(li);
            }
            #endregion

            if (item != null)
            {
                ddl.SelectedValue = item.MsTaniID;
            }
        }
        // 単価
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_PRICE_ROW_" + id;
            T1.Width = 75;
            T1.Attributes["OnChange"] = "checkNaN('" + T1.ID + "',0); calcAmount('" + id + "');return;";
            T1.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            row.Cells[(int)tableHimoku_ColID.Price].Controls.Add(T1);

            T1.MaxLength = InputLen_Price;

            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T1.Attributes.Add("readonly", "true");
            }
            if (item != null)
            {
                T1.Text = item.Tanka.ToString();
                //T1.ReadOnly = true;
            }
        }
        // 金額
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_AMOUNT_ROW_" + id;
            T1.Width = 75;
            T1.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            T1.Attributes.Add("readonly", "true");
            row.Cells[(int)tableHimoku_ColID.Amount].Controls.Add(T1);

            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T1.Attributes.Add("readonly", "true");
            }
            if (item != null)
            {
                T1.Text = (item.Tanka * item.Count).ToString();
            }
        }

        // 備考
        {
            TextBox T2 = new TextBox();
            T2.ID = "TB_REMARK_ROW_" + id;
            T2.Width = 345;
            T2.TextMode = TextBoxMode.MultiLine;
            T2.Rows = 3;
            T2.MaxLength = InputLen_Remark;
            T2.Attributes.Add("onkeydown", "return checkTextLen('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            T2.Attributes.Add("onkeyup", "cutText('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            row.Cells[(int)tableHimoku_ColID.Remark].Controls.Add(T2);

            if (status != 0)
            {
                // 見積済みなので読み込み専用
                T2.Attributes.Add("readonly", "true");
            }
            if (item != null)
            {
                T2.Text = item.Bikou.ToString();
            }
        }

        // TYPE この行が品目か品目詳細か
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_TYPE_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.Type].Controls.Add(T3);

            // ここでは品目
            T3.Text = "2";
        }
        // 次行が品目か品目詳細か ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXT_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextType].Controls.Add(T3);
        }
        // この行のデータのID(DBのKEY=GUID)
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_ID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.ID].Controls.Add(T3);

            T3.Text = id;
        }
        // 次行のデータのID(DBのKEY=GUID) ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXTID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextID].Controls.Add(T3);
        }

        // 添付ファイル
        {
            if (item != null && item.OdAttachFileID != null && item.OdAttachFileID.Length > 0)
            {
                NBaseData.DAC.OdAttachFile attachFile = null;
                using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
                {
                    NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
                    msUser.LoginID = "1";
                    attachFile = serviceClient.OdAttachFile_GetRecordNoData(msUser, item.OdAttachFileID);
                }

                HyperLink L1 = new HyperLink();
                L1.ID = "LB_ATTACH_ROW_" + id;
                L1.Width = 40;
                L1.Text = "〇";
                L1.NavigateUrl = "AttachFiles/" + DateTime.Today.Year + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + "/" + item.OdAttachFileID + "_" + attachFile.FileName;
                L1.Target = "_blank";
                row.Cells[(int)tableHimoku_ColID.Attach].Controls.Add(L1);

            }
            else
            {
                row.Cells[(int)tableHimoku_ColID.Attach].Text = "&nbsp";
            }
        }

        #endregion

        #endregion

        Table_Hinmoku.Rows.Add(row);
        //ListBox_RowID.Items.Add("2" + ":" + id);// 20101129修正 下の行に変更
        ListBox_RowID.Items.Add("2" + ":" + id + ":" + category);// 20101129修正 categoryも追加
    }

    System.Collections.Generic.List<NBaseData.DAC.MsTani> getTaniList()
    {
        System.Collections.Generic.List<NBaseData.DAC.MsTani> taniList
            = (System.Collections.Generic.List<NBaseData.DAC.MsTani>)Session[MakeSessionKey("MSTANILIST")];

        if (taniList == null)
        {
            // セッションにないのでDBから取り込む
            using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
            {
                NBaseData.DAC.MsUser msu = new NBaseData.DAC.MsUser();
                msu.MsUserID = msUserID_Customer;
                taniList = sc.MsTani_GetRecords(msu);
                Session[MakeSessionKey("MSTANILIST")] = taniList;
            }
        }

        return taniList;
    }

    System.Collections.Generic.List<NBaseData.DAC.MsItemSbt> getItemSbtList()
    {
        System.Collections.Generic.List<NBaseData.DAC.MsItemSbt> itemsbtList
            = (System.Collections.Generic.List<NBaseData.DAC.MsItemSbt>)Session[MakeSessionKey("MSITEMSBTLIST")];

        if (itemsbtList == null)
        {
            // セッションにないのでDBから取り込む
            using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
            {
                NBaseData.DAC.MsUser msu = new NBaseData.DAC.MsUser();
                msu.MsUserID = msUserID_Customer;
                itemsbtList = sc.MsItemSbt_GetRecords(msu);
                Session[MakeSessionKey("MSITEMSBTLIST")] = itemsbtList;
            }
        }

        return itemsbtList;
    }

    /// <summary>
    /// 品目詳細の追加
    /// </summary>
    /// <param name="index">追加箇所のIndex</param>
    /// <param name="item">追加するItem(基本的に空の物=Null指定)</param>
    /// <param name="category">カテゴリ（船用品のみ使用）</param>
    private void InsertRow_HimokuShousai(int index, NBaseData.DAC.OdMkShousaiItem item, Iraisyubetu irai_type, string category)
    {
        #region ROW生成

        string id = string.Empty;
        if (item == null)
        {
            id = Guid.NewGuid().ToString();
        }
        else
        {
            id = item.OdMkShousaiItemID;
        }

        TableRow row = new TableRow();
        row.ID = "row_" + id;
        for (int i = 0; i < Table_Hinmoku_Head.Rows[0].Cells.Count; i++)
        {
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            cell.Visible = Table_Hinmoku_Head.Rows[0].Cells[i].Visible;//可視指定
            cell.Width = Table_Hinmoku_Head.Rows[0].Cells[i].Width;//幅指定
        }

        #region 各セルにコントロールを追加

        {
            CheckBox cb = new CheckBox();
            cb.ID = "CB_ROW_" + id;
            //row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(cb);

            Button DelBtn = new Button();
            DelBtn.ID = "DEL_" + id;
            DelBtn.Text = "削除";
            DelBtn.Click += new EventHandler(DelBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(DelBtn);
            //Button AddBtn = new Button();
            //AddBtn.ID = "ADD_" + id;
            //AddBtn.Text = "追加";
            //AddBtn.Click += new EventHandler(AddBtn_Click);
            //row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(AddBtn);

            if (item != null &&
                (item.OdMmShousaiItemID != null &&
                item.OdMmShousaiItemID != string.Empty))
            {
                //ボタン使用不可
                DelBtn.Enabled = false;
            }
        }

        {
            Label L1 = new Label();
            L1.ID = "LB_NO_ROW_" + id;
            L1.Text = " ";
            row.Cells[(int)tableHimoku_ColID.No].Controls.Add(L1);
        }

        // 区分(空カラム)
        {
            Label L1 = new Label();
            L1.ID = "LB_KUBUN_ROW_" + id;
            L1.Text = "&nbsp;";
            row.Cells[(int)tableHimoku_ColID.Kubun].Controls.Add(L1);
        }

        // 名称
        {
            switch (irai_type)
            {
                case Iraisyubetu.修繕:
                    #region 修繕の時のコントロール
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                        if (item != null)
                        {
                            T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                            //T1.ReadOnly = true;
                        }
                    }
                    #endregion
                    break;
                case Iraisyubetu.船用品:
                    if (category == NBaseData.DAC.MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント.ToString())
                    {
                        #region 船用品の時のコントロール
                        // 船用品はMS_VESSEL_ITEMから選択入力(VESSEL/カテゴリで選択を絞る)
                        // コントロールはテキストボックスとボタン
                        {
                            TextBox T1 = new TextBox();
                            T1.ID = "TB_NAME_ROW_" + id;
                            T1.Width = width_textBox_Name - 40; ;
                            T1.TextMode = TextBoxMode.MultiLine;
                            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Rows = 3;
                            //T1.ReadOnly = true;
                            T1.Attributes.Add("readonly", "true");
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                            Button btn = new Button();
                            btn.Text = "変更";
                            btn.ID = "BT_NAME_ROW_" + id;
                            btn.Width = 35;
                            btn.Click += new EventHandler(btn_Select船用品_Click);
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(btn);
                            HiddenField HF_Category = new HiddenField();
                            HF_Category.Value = "";
                            HF_Category.ID = "HF_CATG_ROW_" + id;
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(HF_Category);

                            HF_Category.Value = category;

                            if (item != null)
                            {
                                T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                                //T1.ReadOnly = true;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 修繕の時のコントロール
                        {
                            TextBox T1 = new TextBox();
                            T1.ID = "TB_NAME_ROW_" + id;
                            T1.Width = width_textBox_Name;
                            T1.TextMode = TextBoxMode.MultiLine;
                            T1.Rows = 3;
                            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                            if (item != null)
                            {
                                T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                                //T1.ReadOnly = true;
                            }
                        }
                        #endregion
                    }
                    break;
                case Iraisyubetu.燃料潤滑油:
                    #region 燃料潤滑油の時のコントロール
                    // 燃料潤滑油は修繕と同じ。ここでは関係ないが、行の追加はなし
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                    }
                    #endregion
                    break;
                default:
                    #region 標準のコントロール(修繕と同じ)
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);

                        if (item != null)
                        {
                            T1.Text = (item.ShousaiItemName != string.Empty) ? item.ShousaiItemName : string.Empty;
                            //T1.ReadOnly = true;
                        }
                    }
                    #endregion
                    break;
            }
        }

        // 数量
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_QTTY_ROW_" + id;
            T1.Width = 75;
            T1.Attributes["OnChange"] = "checkNaN('" + T1.ID + "',0); calcAmount('" + id + "');return;";
            row.Cells[(int)tableHimoku_ColID.Count].Controls.Add(T1);
            T1.MaxLength = InputLen_Count;
            

            if (item != null)
            {
                T1.Text = item.Count.ToString();
                //T1.ReadOnly = true;
            }
        }
        // 単位
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "TB_UNIT_ROW_" + id;
            ddl.Width = 60;
            row.Cells[(int)tableHimoku_ColID.Unit].Controls.Add(ddl);

            #region 単位の選択設定
            System.Collections.Generic.List<NBaseData.DAC.MsTani> taniList = getTaniList();
            foreach (NBaseData.DAC.MsTani tani in taniList)
            {
                ListItem li = new ListItem(tani.TaniName, tani.MsTaniID);
                ddl.Items.Add(li);
            }
            #endregion


            if (item != null)
            {
                ddl.SelectedValue = item.MsTaniID;
            }
        }
        // 単価
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_PRICE_ROW_" + id;
            T1.Width = 75;
            T1.Attributes["OnChange"] = "checkNaN('" + T1.ID + "',0); calcAmount('" + id + "');return;";
            T1.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            row.Cells[(int)tableHimoku_ColID.Price].Controls.Add(T1);

            T1.MaxLength = InputLen_Price;

            if (item != null)
            {
                T1.Text = item.Tanka.ToString();
                //T1.ReadOnly = true;
            }
        }
        // 金額
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_AMOUNT_ROW_" + id;
            T1.Width = 75;
            T1.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            T1.Attributes.Add("readonly", "true");
            row.Cells[(int)tableHimoku_ColID.Amount].Controls.Add(T1);

            if (item != null)
            {
                T1.Text = (item.Tanka * item.Count).ToString();
                //T1.ReadOnly = true;
            }
        }

        // 備考
        {
            TextBox T2 = new TextBox();
            T2.ID = "TB_REMARK_ROW_" + id;
            T2.Width = 345;
            T2.TextMode = TextBoxMode.MultiLine;
            T2.Rows = 3;
            T2.MaxLength = InputLen_Remark;
            T2.Attributes.Add("onkeydown", "return checkTextLen('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            T2.Attributes.Add("onkeyup", "cutText('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            row.Cells[(int)tableHimoku_ColID.Remark].Controls.Add(T2);

            if (item != null)
            {
                T2.Text = item.Bikou.ToString();
            }
        }
        {
            // セル埋め
            row.Cells[(int)tableHimoku_ColID.Attach].Text = "&nbsp"; // 添付ファイル
        }

        // TYPE この行が品目か品目詳細か
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_TYPE_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.Type].Controls.Add(T3);

            // ここでは品目
            T3.Text = "2";
        }
        // 次行が品目か品目詳細か ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXT_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextType].Controls.Add(T3);
        }
        // この行のデータのID(DBのKEY=GUID)
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_ID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.ID].Controls.Add(T3);

            T3.Text = id;
        }
        // 次行のデータのID(DBのKEY=GUID) ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXTID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextID].Controls.Add(T3);
        }

        #endregion

        #endregion

        Table_Hinmoku.Rows.AddAt(index, row);
        //ListBox_RowID.Items.Insert(index, "2" + ":" + id);// 20101129修正 下の行に変更
        ListBox_RowID.Items.Insert(index, "2" + ":" + id + ":" + category);// 20101129修正 categoryも追加
    }

    void btn_Select船用品_Click(object sender, EventArgs e)
    {
        // 船用品選択処理

        // 選択候補絞り込みデータ(船・カテゴリ）をセッションに記録
        string str_VesselID = HiddenField_VesselID.Value;
        Session[MakeSessionKey("VesselNo")] = int.Parse(str_VesselID);
        string category = "";

        #region 行サーチ
        foreach (TableRow tr in Table_Hinmoku.Rows)
        {
            TableCell cell0 = tr.Cells[(int)tableHimoku_ColID.AddDel];
            if (cell0.Controls.Count != 1)
            {
                continue;
            }

            TableCell cell = tr.Cells[(int)tableHimoku_ColID.Name];

            // 20101210ADD ペイント以外はボタンが無い(コントロールがテキストのみ、数が１)為、その判定
            if (cell.Controls.Count < 2)
            {
                continue;
            }

            Button tmpBtn = (Button)cell.Controls[1];
            if (tmpBtn.ID == ((Button)sender).ID)
            {
                HiddenField hf = (HiddenField)cell.Controls[2];
                category = hf.Value;
                break;
            }
        }
        #endregion

        System.Collections.Generic.List<NBaseData.DAC.MsVesselItemCategory> vics = null;
        using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.MsUserID = msUserID_Customer;

            vics = sc.MsVesselItemCategory_GetRecords(msUser);
        }
        int catgID = -1;
        foreach (NBaseData.DAC.MsVesselItemCategory vic in vics)
        {
            if (vic.CategoryName == category)
            {
                catgID = vic.CategoryNumber;
                break;
            }
        }

        Session[MakeSessionKey("Category")] = catgID.ToString();

        Button btn = (Button)sender;
        string id = btn.ID;
        id = id.Replace("BT_NAME_ROW_", "TB_NAME_ROW_");
        // 登録用画面へ（モーダルで開く）
        string webkey = Request.QueryString["WebKey"];
        string url = "select詳細品目.aspx?WebKey=" + webkey;
        RegisterStartupScript("Result",
            "<script language='JavaScript'>showModalDialog(\""+ url + "\",window.document.getElementById(\"" + id + "\"),\"dialogWidth:480px;dialogHeight:400px;edge:sunken ;status;no;\");</script>");
    }


    /// <summary>
    /// テーブル再構成用(POSTBACK)
    /// </summary>
    /// <param name="id"></param>
    private void AddRow_HimokuShousai(string id, Iraisyubetu irai_type, string category)
    {
        #region ROW生成

        TableRow row = new TableRow();
        row.ID = "row_" + id;
        for (int i = 0; i < Table_Hinmoku_Head.Rows[0].Cells.Count; i++)
        {
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            cell.Visible = Table_Hinmoku_Head.Rows[0].Cells[i].Visible;//可視指定
            cell.Width = Table_Hinmoku_Head.Rows[0].Cells[i].Width;//幅指定
        }

        #region 各セルにコントロールを追加

        {
            CheckBox cb = new CheckBox();
            cb.ID = "CB_ROW_" + id;
            //row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(cb);

            Button DelBtn = new Button();
            DelBtn.ID = "DEL_" + id;
            DelBtn.Text = "削除";
            DelBtn.Click += new EventHandler(DelBtn_Click);
            row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(DelBtn);
            //Button AddBtn = new Button();
            //AddBtn.ID = "ADD_" + id;
            //AddBtn.Text = "追加";
            //AddBtn.Click += new EventHandler(AddBtn_Click);
            //row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Add(AddBtn);
        }

        {
            Label L1 = new Label();
            L1.ID = "LB_NO_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.No].Controls.Add(L1);
        }

        // 区分(空カラム)
        {
            Label L1 = new Label();
            L1.ID = "LB_KUBUN_ROW_" + id;
            L1.Text = "&nbsp;";
            row.Cells[(int)tableHimoku_ColID.Kubun].Controls.Add(L1);
        }


        // 名称
        {
            #region 旧処理
            //TextBox T1 = new TextBox();
            //T1.ID = "TB_NAME_ROW_" + id;
            //T1.Width = 345;
            //T1.TextMode = TextBoxMode.MultiLine;
            //T1.Rows = 3;
            //row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
            #endregion

            switch (irai_type)
            {
                case Iraisyubetu.修繕:
                    #region 修繕の時のコントロール
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                    }
                    #endregion
                    break;
                case Iraisyubetu.船用品:
                    // 20101129修正 他にある修繕の場合の処理が抜けていたので、条件分岐と処理を追加(元は「船用品の時のコントロール」のみ)
                    if (category == NBaseData.DAC.MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント.ToString())
                    {
                        #region 船用品の時のコントロール
                        // 船用品はMS_VESSEL_ITEMから選択入力(VESSEL/カテゴリで選択を絞る)
                        // コントロールはテキストボックスとボタン
                        {
                            TextBox T1 = new TextBox();
                            T1.ID = "TB_NAME_ROW_" + id;
                            T1.Width = width_textBox_Name - 40; ;
                            T1.TextMode = TextBoxMode.MultiLine;
                            T1.Rows = 3;
                            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            //T1.ReadOnly = true;
                            T1.Attributes.Add("readonly", "true");
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                            Button btn = new Button();
                            btn.Text = "変更";
                            btn.ID = "BT_NAME_ROW_" + id;
                            btn.Width = 35;
                            btn.Click += new EventHandler(btn_Select船用品_Click);
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(btn);
                            HiddenField HF_Category = new HiddenField();
                            HF_Category.Value = "";
                            HF_Category.ID = "HF_CATG_ROW_" + id;
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(HF_Category);
                        }
                        #endregion
                    }
                    else
                    {
                        #region 修繕の時のコントロール
                        {
                            TextBox T1 = new TextBox();
                            T1.ID = "TB_NAME_ROW_" + id;
                            T1.Width = width_textBox_Name;
                            T1.TextMode = TextBoxMode.MultiLine;
                            T1.Rows = 3;
                            T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                            row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                        }
                        #endregion
                    }
                    break;
                case Iraisyubetu.燃料潤滑油:
                    #region 燃料潤滑油の時のコントロール
                    // 燃料潤滑油は修繕と同じ。ここでは関係ないが、行の追加はなし
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        T1.Attributes.Add("onkeyup", "cutText('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                    }
                    #endregion
                    break;
                default:
                    #region 標準のコントロール(修繕と同じ)
                    {
                        TextBox T1 = new TextBox();
                        T1.ID = "TB_NAME_ROW_" + id;
                        T1.Width = width_textBox_Name;
                        T1.TextMode = TextBoxMode.MultiLine;
                        T1.Rows = 3;
                        T1.Attributes.Add("onkeydown", "return checkTextLen('" + T1.ID + "'," + InputLen_Name.ToString() + ")");
                        row.Cells[(int)tableHimoku_ColID.Name].Controls.Add(T1);
                    }
                    #endregion
                    break;
            }
        }

        // 数量
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_QTTY_ROW_" + id;
            T1.Width = 75;
            T1.Attributes["OnChange"] = "checkNaN('" + T1.ID + "',0); calcAmount('" + id + "');return;";
            row.Cells[(int)tableHimoku_ColID.Count].Controls.Add(T1);
            T1.MaxLength = InputLen_Count;
        }
        // 単位
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "TB_UNIT_ROW_" + id;
            ddl.Width = 60;
            row.Cells[(int)tableHimoku_ColID.Unit].Controls.Add(ddl);

            #region 単位の選択設定
            System.Collections.Generic.List<NBaseData.DAC.MsTani> taniList = getTaniList();
            foreach (NBaseData.DAC.MsTani tani in taniList)
            {
                ListItem li = new ListItem(tani.TaniName, tani.MsTaniID);
                ddl.Items.Add(li);
            }
            #endregion
        }
        // 単価
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_PRICE_ROW_" + id;
            T1.Width = 75;
            T1.Attributes["OnChange"] = "checkNaN('" + T1.ID + "',0); calcAmount('" + id + "');return;";
            T1.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            row.Cells[(int)tableHimoku_ColID.Price].Controls.Add(T1);

            T1.MaxLength = InputLen_Price;
        }
        // 金額
        {
            TextBox T1 = new TextBox();
            T1.ID = "TB_AMOUNT_ROW_" + id;
            T1.Width = 75;
            T1.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            T1.Attributes.Add("readonly", "true");
            row.Cells[(int)tableHimoku_ColID.Amount].Controls.Add(T1);
        }

        // 備考
        {
            TextBox T2 = new TextBox();
            T2.ID = "TB_REMARK_ROW_" + id;
            T2.Width = 345;
            T2.TextMode = TextBoxMode.MultiLine;
            T2.Rows = 3;
            T2.MaxLength = InputLen_Remark;
            T2.Attributes.Add("onkeydown", "return checkTextLen('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            T2.Attributes.Add("onkeyup", "cutText('" + T2.ID + "'," + InputLen_Remark.ToString() + ")");
            row.Cells[(int)tableHimoku_ColID.Remark].Controls.Add(T2);
        }

        {
            // セル埋め
            row.Cells[(int)tableHimoku_ColID.Attach].Text = "&nbsp"; // 添付ファイル
        }

        // TYPE この行が品目か品目詳細か
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_TYPE_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.Type].Controls.Add(T3);

            // ここでは品目
            T3.Text = "2";
        }
        // 次行が品目か品目詳細か ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXT_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextType].Controls.Add(T3);
        }
        // この行のデータのID(DBのKEY=GUID)
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_ID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.ID].Controls.Add(T3);

            T3.Text = id;
        }
        // 次行のデータのID(DBのKEY=GUID) ※POSTBACK時のテーブル復元に使用。次行追加時に入れる
        {
            TextBox T3 = new TextBox();
            T3.ID = "TB_NEXTID_ROW_" + id;
            row.Cells[(int)tableHimoku_ColID.NextID].Controls.Add(T3);
        }

        #endregion

        #endregion

        Table_Hinmoku.Rows.Add(row);
        //ListBox_RowID.Items.Add("1" + ":" + id);// 再構成のときはIDは追加しない
    }

    void DelBtn_Click(object sender, EventArgs e)
    {
        // セッション更新
        updateSession();

        Button pushedBtn = (Button)sender;

        bool delFlag = false;

        // 20101129ADD 詳細行のカウンタ
        int count_detail = 0;

        int index = 0;
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            Button delBtn = (Button)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];

            if (pushedBtn.ID == delBtn.ID)
            {
                // 行の種類を取得
                string[] tmpListVal = ListBox_RowID.Items[index].Value.Split(':');

                if (tmpListVal[0] == "1")
                {
                    //品目
                    #region 次の品目のIndEXを取得

                    int nextIdx = index + 1;
                    for (int j = index + 1; j < Table_Hinmoku.Rows.Count; j++)
                    {
                        nextIdx = j;
                        //TextBox tmptb = (TextBox)Table_Hinmoku.Rows[j].Cells[(int)tableHimoku_ColID.Type].Controls[0];
                        //string tmpType = tmptb.Text;
                        tmpListVal = ListBox_RowID.Items[nextIdx].Value.Split(':');
                        string tmpType = tmpListVal[0];
                        if (tmpType == "1")
                        {
                            break;
                        }
                        else
                        {
                            // 20101129ADD 詳細のカウント
                            count_detail++;
                        }
                    }

                    #endregion

                    // 品目に詳細がない時は、次の行が品目
                    //if (nextIdx == index + 1)
                    if (count_detail == 0)// 20101129ADD 上の条件を変更。詳細がないときは消せる。
                        delFlag = true;

                    break;
                }
                else
                {
                    // 品目詳細はそのまま消す
                    delFlag = true;
                    break;
                }
            }

            index++;
        }

        // 
        if (delFlag == true)
        {
            DeleteRow_HinmokuTbl(index);
            if (Table_Hinmoku.Rows.Count == 0)
            {
                int curpageno = (Session[MakeSessionKey("CUR_PAGE_NO")] != null) ? (int)Session[MakeSessionKey("CUR_PAGE_NO")] : 1;
                if (curpageno >= 1)
                {
                    curpageno--;
                }
                showHimoku_onPage(curpageno);
            }

            RegisterStartupScript("", "<script language='JavaScript'>calcTotal();</script>");// 20101208ADD 削除時の合計額計算スクリプト呼び出し
        }
    }

    /// <summary>
    /// 品目のテーブルから指定行を削除する。(品目・詳細品目共通)
    /// 削除リストに削除のIDを追加しておく
    /// </summary>
    /// <param name="index">削除行のインデックス</param>
    private void DeleteRow_HinmokuTbl(int index)
    {
        Table_Hinmoku.Rows.RemoveAt(index);
        string deleteID = ListBox_RowID.Items[index].Value;
        ListBox_DeleteID.Items.Add(deleteID);
        ListBox_RowID.Items.RemoveAt(index);

        #region セッションから削除
        string[] deleteIDs = deleteID.Split(':');
        string delIDonly = deleteIDs[1];

        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList = null;
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList = null;

        // Sessionから情報を取込
        odMkItemList = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        odMkShousaiList = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];

        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            if (item.OdMkItemID == delIDonly)
            {
                odMkItemList.Remove(item);
                break;
            }
        }

        foreach (NBaseData.DAC.OdMkShousaiItem detial in odMkShousaiList)
        {
            if (detial.OdMkShousaiItemID == delIDonly)
            {
                odMkShousaiList.Remove(detial);
                break;
            }
        }

        #endregion
    }

    protected void Button_AddHinmoku_Click(object sender, EventArgs e)
    {
        // セッション更新
        updateSession();

        string header = TextBox_Header.Text;

        #region 更新したSessionデータ取得
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        #endregion

        #region ヘッダーの検索とデータ追加

        #region 検索
        bool headerFind = false;

        int dataIndex = 0;
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            if (item.Header == header)
            {
                // ヘッダー一致
                headerFind = true;
            }
            if (headerFind == true)
            {
                if (item.Header != header)
                {
                    // ヘッダーの末尾到達
                    break;
                }
            }
            dataIndex++;
        }
        #endregion

        #region データ追加

        NBaseData.DAC.OdMkItem odMkItem = new NBaseData.DAC.OdMkItem();

        odMkItem.Header = header;
        odMkItem.OdMkItemID = Guid.NewGuid().ToString();

        #region 20101209ADD 品目の初期値設定（空行対策）
        Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
        if (irai_type == Iraisyubetu.船用品)
        {
            //System.Collections.Generic.List<NBaseData.DAC.MsVesselItemCategory> categorys = null;
            //using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
            //{
            //    NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            //    msUser.MsUserID = msUserID_Customer;

            //    categorys = serviceClient.MsVesselItemCategory_GetRecords(msUser);
            //}
            //if (categorys != null && categorys.Count > 0)
            //{
            //    odMkItem.ItemName = categorys[0].CategoryName;
            //}

            // 20101213変更 JavaScriptから入力ダイアログを呼出し、その指定から費目設定
            odMkItem.ItemName = HiddenField_Hinmoku.Value.ToString();
        }
        #endregion

        if (dataIndex >= odMkItemList.Count)
        {
            odMkItemList.Add(odMkItem);
        }
        else
        {
            odMkItemList.Insert(dataIndex, odMkItem);
        }
        #region 番号振り直し(ShowOrder)
        {
            int orderNo = 0;
            for (int i = 0; i < odMkItemList.Count; i++)
            {
                odMkItemList[i].ShowOrder = orderNo;
                orderNo++;
            }
        }
        #endregion

        Session[MakeSessionKey("LIST_ODMKITEM")] = odMkItemList;

        #endregion

        #region 画面再表示(Session情報での更新)

        #region 表示ページの検索
        int pageNo = 0;
        int rowNum = 0;
        string prevHeader = string.Empty;
        if (odMkItemList.Count > 0)
        {
            prevHeader = odMkItemList[0].Header;
        }
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            if (rowNum >= maxRowNum_Page ||
                item.Header != prevHeader)
            {
                pageNo++;
                rowNum = 0;
                prevHeader = item.Header;
            }

            if (item.OdMkItemID == odMkItem.OdMkItemID)
            {
                break;
            }

            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (detail.OdMkItemID == item.OdMkItemID)
                {
                    rowNum++;
                }
            }
        }

        #endregion

        #region 表示

        showHimoku_onPage(pageNo);

        #endregion

        #endregion


        #endregion

        #region 20101213コメント化（前で処理するように変更した為）
        /*
        //Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
        if (irai_type == Iraisyubetu.船用品)
        {
            // 船用品の場合：追加した行に対する編集(品目名)
            #region 編集したIDの取得
            ListItem li = ListBox_RowID.Items[ListBox_RowID.Items.Count - 1];
            string[] ids = li.Text.Split(':');
            string id = ids[1];
            #endregion
            #region 編集用の画面を開く
            // 登録用画面へ（モーダルで開く）
            // 20101213コメント化
            //RegisterStartupScript("Result",
            //    "<script language='JavaScript'>showModalDialog(\"select品目_船用品.aspx\",window.document.getElementById(\"TB_NAME_ROW_" + id + "\"),\"dialogWidth:480px;dialogHeight:400px;edge:sunken ;status;no;\");</script>");
            #endregion

        }
        */
        #endregion

        // セッション更新
        updateSession();
    }

    //ボタン変更の為に現在使用していない
    protected void Button_DelHinmoku_Click(object sender, EventArgs e)
    {
        // 品目削除。詳細が残っている場合は削除しない
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            int index = Table_Hinmoku.Rows.GetRowIndex(row);
            CheckBox cb = (CheckBox)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];
            Button btn = (Button)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];
            string[] tmpListVal = ListBox_RowID.Items[index].Value.Split(':');
            string type = tmpListVal[0];

            if (type == "1" && cb.Checked == true)
            {
                //品目にチェックがついているので、その品目の詳細の有無をチェック
                #region 次の品目のIndEXを取得
                int nextIdx = index + 1;
                for (int j = index + 1; j < Table_Hinmoku.Rows.Count; j++)
                {
                    nextIdx = j;
                    //TextBox tmptb = (TextBox)Table_Hinmoku.Rows[j].Cells[(int)tableHimoku_ColID.Type].Controls[0];
                    //string tmpType = tmptb.Text;
                    tmpListVal = ListBox_RowID.Items[nextIdx].Value.Split(':');
                    string tmpType = tmpListVal[0];
                    if (tmpType == "1")
                    {
                        break;
                    }
                }
                #endregion

                //取得したINDEXの場所に詳細を挿入 AddAt
                if (nextIdx == index + 1)
                    DeleteRow_HinmokuTbl(index);
                cb.Checked = false;
                break;
            }
        }
    }
    //ボタン変更の為に現在使用していない
    protected void Button_AddShousai_Click(object sender, EventArgs e)
    {
        Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];

        #region 詳細追加。選択されている品目に追加する
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            int index = Table_Hinmoku.Rows.GetRowIndex(row);
            CheckBox cb = (CheckBox)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];
            //TextBox typeTxt = (TextBox)row.Cells[(int)tableHimoku_ColID.Type].Controls[0];
            //string type = (typeTxt != null)? typeTxt.Text : string.Empty;
            string[] tmpListVal = ListBox_RowID.Items[index].Value.Split(':');
            string type = tmpListVal[0];

            if (type == "1" && cb.Checked == true)
            {
                //品目にチェックがついているので、その品目の末尾に詳細を追加
                #region 次の品目のIndEXを取得
                int nextIdx = index + 1;
                for (int j = index + 1; j < Table_Hinmoku.Rows.Count; j++)
                {
                    nextIdx = j;
                    //TextBox tmptb = (TextBox)Table_Hinmoku.Rows[j].Cells[(int)tableHimoku_ColID.Type].Controls[0];
                    //string tmpType = tmptb.Text;
                    tmpListVal = ListBox_RowID.Items[nextIdx].Value.Split(':');
                    string tmpType = tmpListVal[0];
                    if (tmpType == "1")
                    {
                        break;
                    }
                }
                #endregion

                //取得したINDEXの場所に詳細を挿入 AddAt
                InsertRow_HimokuShousai(nextIdx, null, irai_type, "");
                cb.Checked = false;
                break;
            }
        }
        #endregion

    }
    //ボタン変更の為に現在使用していない
    protected void Button_DelShousai_Click(object sender, EventArgs e)
    {
        // 詳細削除。単純に削除。削除分は削除リストに載せておき、Save/Commit時に処理する。
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            int index = Table_Hinmoku.Rows.GetRowIndex(row);
            CheckBox cb = (CheckBox)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];
            //TextBox typeTxt = (TextBox)row.Cells[(int)tableHimoku_ColID.Type].Controls[0];
            //string type = (typeTxt != null)? typeTxt.Text : string.Empty;
            string[] tmpListVal = ListBox_RowID.Items[index].Value.Split(':');
            string type = tmpListVal[0];

            if (type == "2" && cb.Checked == true)
            {
                //取得したINDEXの場所の削除
                DeleteRow_HinmokuTbl(index);
                cb.Checked = false;
                break;
            }
        }
    }

    /// <summary>
    /// 画面の入力情報でSessionを更新する
    /// </summary>
    private void updateSession()
    {
        #region Session処理

        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList = null;
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList = null;

        // Sessionから情報を取込
        odMkItemList = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        odMkShousaiList = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        Iraisyubetu iraiSyubetu = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];

        // 画面の情報取込(Sessionから取得した情報を書き換え)
        string tmpItemID = string.Empty;
        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            string id = row.ID.Substring(4);
            #region rowが品目か詳細か
            string type = "";
            if (row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Count == 2)
            {
                type = "1";
            }
            else
            {
                type = "2";
            }
            #endregion

            if (type == "1")
            {
                #region 品目行処理
                bool update = false;

                foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
                {
                    if (id == item.OdMkItemID)
                    {

                        #region データ更新
                        // 品目名
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Name].Controls[0];
                            item.ItemName = t.Text;
                        }
                        // 区分
                        {
                            if (iraiSyubetu == Iraisyubetu.修繕)
                            {
                                DropDownList d = (DropDownList)row.Cells[(int)tableHimoku_ColID.Kubun].Controls[0];
                                item.MsItemSbtID = d.SelectedValue;
                            }
                        }
                        // 備考
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Remark].Controls[0];
                            item.Bikou = t.Text;
                        }
                        #endregion

                        tmpItemID = id;

                        update = true;

                        break;
                    }
                }

                if (update == false)
                {
                    #region 新規データなので追加
                    NBaseData.DAC.OdMkItem newItem = new NBaseData.DAC.OdMkItem();

                    newItem.OdMkItemID = id;
                    tmpItemID = id;

                    // 品目名
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Name].Controls[0];
                        newItem.ItemName = t.Text;
                    }
                    // 備考
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Remark].Controls[0];
                        newItem.Bikou = t.Text;
                    }

                    odMkItemList.Add(newItem);

                    #endregion
                }

                #endregion
            }
            else
            {
                #region 詳細行処理
                bool update = false;

                foreach (NBaseData.DAC.OdMkShousaiItem item in odMkShousaiList)
                {
                    if (id == item.OdMkShousaiItemID)
                    {
                        #region データ更新
                        // ID
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.ID].Controls[0];
                            item.OdMkShousaiItemID = id;
                        }
                        // 品目詳細名
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Name].Controls[0];
                            item.ShousaiItemName = t.Text;
                        }
                        // 数量
                        try
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Count].Controls[0];
                            item.Count = int.Parse(t.Text);
                        }
                        catch
                        {
                            item.Count = 0;
                        }
                        // 単価
                        try
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Price].Controls[0];
                            item.Tanka = decimal.Parse(t.Text);
                        }
                        catch
                        {
                            item.Tanka = 0;
                        }
                        // 単位
                        {
                            DropDownList d = (DropDownList)row.Cells[(int)tableHimoku_ColID.Unit].Controls[0];
                            item.MsTaniID = d.SelectedValue;
                        }
                        // 備考
                        {
                            TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Remark].Controls[0];
                            item.Bikou = t.Text;
                        }

                        // その他
                        {
                            item.OdMkItemID = tmpItemID;
                        }
                        #endregion

                        update = true;
                        break;
                    }
                }

                if (update == false)
                {
                    #region 新規データなので追加

                    NBaseData.DAC.OdMkShousaiItem newItem = new NBaseData.DAC.OdMkShousaiItem();

                    #region データ編集
                    // ID
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.ID].Controls[0];
                        newItem.OdMkShousaiItemID = id;
                    }
                    // 品目詳細名
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Name].Controls[0];
                        newItem.ShousaiItemName = t.Text;
                    }
                    // 数量
                    try
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Count].Controls[0];
                        newItem.Count = int.Parse(t.Text);
                    }
                    catch
                    {
                        newItem.Count = 0;
                    }
                    // 単価
                    try
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Price].Controls[0];
                        newItem.Tanka = decimal.Parse(t.Text);
                    }
                    catch
                    {
                        newItem.Tanka = 0;
                    }
                    // 単位
                    {
                        DropDownList d = (DropDownList)row.Cells[(int)tableHimoku_ColID.Unit].Controls[0];
                        newItem.MsTaniID = d.SelectedValue;
                    }
                    // 備考
                    {
                        TextBox t = (TextBox)row.Cells[(int)tableHimoku_ColID.Remark].Controls[0];
                        newItem.Bikou = t.Text;
                    }

                    // その他
                    {
                        newItem.OdMkItemID = tmpItemID;
                    }
                    #endregion

                    odMkShousaiList.Add(newItem);

                    #endregion
                }

                #endregion
            }
        }

        // Sessionへ戻す
        Session[MakeSessionKey("LIST_ODMKITEM")] = odMkItemList;
        Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")] = odMkShousaiList;

        #endregion

        // 金額関係
        decimal Amount = decimal.Parse(HiddenField_Mitumorigaku.Value);
        decimal nebiki = decimal.Parse(TextBox_Nebiki.Text);
        decimal tax = decimal.Parse(TextBox_Tax.Text);
        decimal goukei = decimal.Parse(Label_Goukei.Text);

        //なぜかラベルからJavaスクリプトで設定した値を取れないので計算値を反映するために追加
        Label_Mitsumorigaku.Text = Amount.ToString();
        Label_Goukei.Text = (Amount - nebiki + tax).ToString();
    }

    /// <summary>
    /// 引数のデータでページ数を算出する
    /// </summary>
    /// <returns></returns>
    private int calcTotalPage(System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList,
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList)
    {
        int page = 0;
        int lineCount = 0;

        string prevHeader = (odMkItemList.Count > 0) ? odMkItemList[0].Header : string.Empty;
        if (odMkItemList.Count > 0) page++;
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            // ヘッダーが変わったとき/行数オーバーのの改ページ
            if (item.Header != prevHeader ||
                lineCount >= maxRowNum_Page)
            {
                prevHeader = item.Header;
                lineCount = 0;
                page++;
            }
            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (item.OdMkItemID == detail.OdMkItemID)
                {
                    lineCount++;
                }
            }
        }

        return page;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PageNo">ページ数(全ページ数)</param>
    /// <param name="curPageNo">選択されているページ</param>
    private void makePageIndex(int PageNo, int curPageNo)
    {
        TableRow pageRow = new TableRow();
        TableCell pageCell = new TableCell();

        Table_Pager.Rows.Clear();
        Table_Pager.Rows.Add(pageRow);
        pageRow.Cells.Add(pageCell);

        for (int i = 0; i < PageNo; i++)
        {
            LinkButton btn = new LinkButton();
            btn.ID = "Btn_Page" + i.ToString();
            btn.Text = (i + 1).ToString() + " ";
            btn.Font.Size = new FontUnit(12);
            btn.Click += new EventHandler(btn_page_Click);

            if (i == curPageNo)
            {
                btn.Enabled = false;
            }
            else
            {
                btn.Enabled = true;
            }

            pageCell.Controls.Add(btn);
        }

        Session[MakeSessionKey("MAX_PAGE_NO")] = PageNo;
    }

    /// <summary>
    /// 引数のデータから対応するページの表示する
    /// </summary>
    /// <param name="pageNo">対応するページ番号</param>
    /// <param name="odMkItemList">ODMKItem情報</param>
    /// <param name="odMkShousaiList">ODMKShousaiItem情報</param>
    private void showHinmoku_onPage(int pageNo,
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList,
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList,
        Iraisyubetu irai_type)
    {
        #region 最大ページ数とページのボタン生成
        int maxPageNum = calcTotalPage(odMkItemList, odMkShousaiList);

        // 20101209ADD 最大ページを超えていた時の保険
        if (maxPageNum - 1 < pageNo) pageNo = maxPageNum - 1;

        makePageIndex(maxPageNum, pageNo);

        Session[MakeSessionKey("CUR_PAGE_NO")] = pageNo;

        #endregion

        int startIndex = 0;
        #region 表示するODMKITEMの先頭データ(index)を求める
        int tmpRowNum = 0;
        int tmpRowNum_page = 0;
        int tmpPageNum = 0;
        string tmpHeader = string.Empty;
        if(odMkItemList.Count != 0)
        {
            tmpHeader = odMkItemList[0].Header;
            tmpPageNum++;
        }
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            // 最大行数を超えるか、ヘッダーが変わったら改ページ
            if (tmpHeader != item.Header ||
                tmpRowNum_page >= maxRowNum_Page)
            {
                tmpHeader = item.Header;
                tmpRowNum_page = 0;
                tmpPageNum++;
            }

            if (tmpPageNum >= pageNo + 1)
            {
                break;
            }
            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (item.OdMkItemID == detail.OdMkItemID)
                {
                    tmpRowNum++;
                    tmpRowNum_page++;
                }
            }

            startIndex++;
        }
        #endregion

        #region 表示する/Page上の合計額の計算
        int rowNum = tmpRowNum;
        ListBox_RowID.Items.Clear();
        Table_Hinmoku.Rows.Clear();
        decimal totalonPage = 0;

        #region ヘッダー表示
        Label_ItemHeader.Text = string.Empty;
        if (odMkItemList.Count > 0)
        {
            Label_ItemHeader.Text = odMkItemList[startIndex].Header;
        }
        #endregion

        if (odMkItemList.Count > 0)
        {
            tmpHeader = odMkItemList[startIndex].Header;
        }
        for (int i = startIndex; i < odMkItemList.Count; i++)
        {
            NBaseData.DAC.OdMkItem item = odMkItemList[i];

            if (tmpHeader != item.Header)
            {
                break;
            }

            AddRow_Himoku(item, irai_type);

            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (item.OdMkItemID == detail.OdMkItemID)
                {
                    AddRow_HimokuShousai(detail, item, irai_type);
                    rowNum++;
                    #region 行に番号付加
                    TableRow row = Table_Hinmoku.Rows[Table_Hinmoku.Rows.Count - 1];
                    Label l = (Label)row.Cells[(int)tableHimoku_ColID.No].Controls[0];
                    l.Text = rowNum.ToString();
                    #endregion

                    totalonPage += detail.Tanka * detail.Count;
                }
            }
            if (rowNum - tmpRowNum >= maxRowNum_Page)
            {
                break;
            }
        }
        #endregion

        #region 総合計額の計算
        decimal totalAmt = 0;
        foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
        {
            totalAmt += detail.Tanka * detail.Count;
        }

        HiddenField_Total.Value = totalAmt.ToString();
        HiddenField_Total_onPage.Value = totalonPage.ToString();

        #endregion
    }

    /// <summary>
    /// 引数と対応するページを表示する。データはセッションに登録されていることが前提。
    /// </summary>
    /// <param name="pageNo"></param>
    private void showHimoku_onPage(int pageNo)
    {
        // Sessionから情報を取込
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        Iraisyubetu iraiType = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
        showHinmoku_onPage(pageNo, odMkItemList, odMkShousaiList, iraiType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageNo"></param>
    /// <param name="odMkItemList"></param>
    /// <param name="odMkShousaiList"></param>
    /// <returns>ページ先頭行番号</returns>
    private int calc_1stRowNo_onPage(int pageNo,
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList,
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList)
    {
        #region 最大ページ数
        int maxPageNum = calcTotalPage(odMkItemList, odMkShousaiList);

        // 20101209ADD 最大ページを超えていた時の保険
        if (maxPageNum - 1 < pageNo) pageNo = maxPageNum - 1;
        #endregion

        #region 表示するODMKITEMの先頭データ(index)を求める
        int tmpRowNum = 0;
        int tmpRowNum_page = 0;
        int tmpPageNum = 0;
        string tmpHeader = string.Empty;
        if (odMkItemList.Count != 0)
        {
            tmpHeader = odMkItemList[0].Header;
            tmpPageNum++;
        }
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            // 最大行数を超えるか、ヘッダーが変わったら改ページ
            if (tmpHeader != item.Header ||
                tmpRowNum_page >= maxRowNum_Page)
            {
                tmpHeader = item.Header;
                tmpRowNum_page = 0;
                tmpPageNum++;
            }

            if (tmpPageNum >= pageNo + 1)
            {
                break;
            }
            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (item.OdMkItemID == detail.OdMkItemID)
                {
                    tmpRowNum++;
                    tmpRowNum_page++;
                }
            }
        }
        #endregion

        return (tmpRowNum + 1);
    }

    /// <summary>
    /// 画面のボタンの状態を制御する
    /// </summary>
    private void ControlButton()
    {
        #region 品目のテーブル上のボタン制御

        // セッションの情報と行の比較を行い、対応する情報でボタンを制御
        #region Sessionから情報を取込
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        #endregion

        foreach (TableRow row in Table_Hinmoku.Rows)
        {
            string rowID = row.ID;
            string id = rowID.Substring(4);

            #region rowが品目か詳細か
            string rowtype = "";
            if (row.Cells[(int)tableHimoku_ColID.AddDel].Controls.Count == 2)
            {
                rowtype = "1";
            }
            else
            {
                rowtype = "2";
            }
            #endregion

            if (rowtype == "1")
            {
                // 品目行の場合
                #region 対応する情報の取得
                NBaseData.DAC.OdMkItem matchedItem = null;
                foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
                {
                    if (item.OdMkItemID == id)
                    {
                        matchedItem = item;
                        break;
                    }
                }
                #endregion

                if (matchedItem != null)
                {
                    if (matchedItem.OdMmItemID != null &&
                        matchedItem.OdMmItemID != string.Empty)
                    {
                        #region 元からあるデータ用コントロール使用設定
                        Button buttonDEL = (Button)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];
                        buttonDEL.Enabled = false;
                        Button buttonADD = (Button)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[1];
                        buttonADD.Enabled = false;
                        #endregion
                    }
                }

            }
            else
            {
                // 詳細行の場合
                #region 対応する情報の取得
                NBaseData.DAC.OdMkShousaiItem matchedItem = null;
                foreach (NBaseData.DAC.OdMkShousaiItem item in odMkShousaiList)
                {
                    #region 行のコントロールの設定
                    if (item.OdMkShousaiItemID == id)
                    {
                        matchedItem = item;
                        break;
                    }
                    #endregion
                }
                #endregion

                if (matchedItem != null)
                {
                    #region 行のコントロールの設定
                    if (matchedItem.OdMmShousaiItemID != null &&
                        matchedItem.OdMmShousaiItemID != string.Empty)
                    {
                        #region 元からあるデータ用コントロール使用設定
                        Button buttonDEL = (Button)row.Cells[(int)tableHimoku_ColID.AddDel].Controls[0];
                        buttonDEL.Enabled = false;
                        #endregion
                    }
                    #endregion
                }
            }
        }
        #endregion
    }

    void btn_page_Click(object sender, EventArgs e)
    {
        updateSession();

        LinkButton btn = (LinkButton)sender;

        string id = btn.ID;
        string pageNo = id.Substring(8);

        // Sessionから情報を取込　　<<<　引数指定
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        Iraisyubetu iraiType = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
        showHinmoku_onPage(int.Parse(pageNo), odMkItemList, odMkShousaiList, iraiType);
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        // セッション更新
        //updateSession();

        #region 入力データチェック/バリデーション
        {
            try
            {
                decimal.Parse(HiddenField_Mitumorigaku.Value);
                decimal.Parse(TextBox_Nebiki.Text);
                decimal.Parse(TextBox_Tax.Text);
                decimal.Parse(Label_Goukei.Text);

                // 納期・年月日
                string yyyy = TextBox_Nouki_Year.Text;
                string MM = TextBox_Nouki_Month.Text;
                string dd = TextBox_Nouki_Day.Text;

                if (yyyy.Length != 0 &&
                    MM.Length != 0 &&
                    dd.Length != 0)
                {
                    DateTime.Parse(yyyy + "/" + MM + "/" + dd);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region データ取り込み

        // セッション更新
        updateSession();

        // 有効期限・納期
        string yuukouKigen = TextBox_YuukouKigen.Text;
        string nouki_y = TextBox_Nouki_Year.Text;
        string nouki_m = TextBox_Nouki_Month.Text;
        string nouki_d = TextBox_Nouki_Day.Text;
        string nouki = "";
        if (nouki_y.Length != 0 &&
            nouki_m.Length != 0 &&
            nouki_d.Length != 0)
        {
            nouki = nouki_y + "/" + nouki_m + "/" + nouki_d;
        }

        // 金額関係
        decimal Amount = decimal.Parse(HiddenField_Mitumorigaku.Value);
        decimal nebiki = decimal.Parse(TextBox_Nebiki.Text);
        decimal tax = decimal.Parse(TextBox_Tax.Text);
        decimal goukei = decimal.Parse(Label_Goukei.Text);

        //なぜかラベルからJavaスクリプトで設定した値を取れないので計算値を反映するために追加
        Label_Mitsumorigaku.Text = Amount.ToString();
        Label_Goukei.Text = (Amount - nebiki + tax).ToString();


        // テーブルデータ
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> items
            = new System.Collections.Generic.List<NBaseData.DAC.OdMkItem>();
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> details
            = new System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>();

        // Sessionから情報を取込
        items = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        details = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];

        int vesselID = 0;
        try
        {

            vesselID = int.Parse(HiddenField_VesselID.Value);
        }
        catch
        {
        }

        #endregion

        #region データ処理(登録・削除)
        using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.MsUserID = msUserID_Customer;
            string webkey = this.Request.QueryString["WebKey"];
            NBaseData.DAC.OdMk odmk = sc.OdMk_GetRecordByWebKey(msUser, webkey);

            System.Collections.Generic.List<NBaseData.DAC.MsVesselItemCategory> categorys = null;
            if((Iraisyubetu)Session[MakeSessionKey("依頼種別")] == Iraisyubetu.船用品)
            {
                categorys = sc.MsVesselItemCategory_GetRecords(msUser);
            }


            #region 見積回答
            odmk.Tantousha = TextBox_担当者.Text;

            if (nouki.Length != 0)
                odmk.Nouki = DateTime.Parse(nouki);
            odmk.MkYukouKigen = yuukouKigen;

            odmk.Amount = Amount;
            odmk.MkAmount = nebiki;
            odmk.Tax = tax;

            odmk.RenewDate = DateTime.Now;
            odmk.RenewUserID = msUser.MsUserID;

            sc.OdMk_Update(msUser, odmk);
            #endregion

            // 品目
            foreach (NBaseData.DAC.OdMkItem item in items)
            {
                NBaseData.DAC.OdMkItem updateItem = sc.OdMkItem_GetRecord(msUser,item.OdMkItemID);
                if (updateItem == null)
                {
                    // 新規
                    #region 必要なデータの追加

                    item.OdMkID = odmk.OdMkID;

                    item.RenewUserID = msUser.MsUserID;
                    item.RenewDate = DateTime.Now;

                    #endregion
                    sc.OdMkItem_Insert(msUser, item);
                }
                else
                {
                    // 更新
                    #region 必要なデータの複写・書換

                    updateItem.ItemName = item.ItemName;
                    updateItem.MsItemSbtID = item.MsItemSbtID;
                    updateItem.Bikou = item.Bikou;

                    updateItem.ShowOrder = item.ShowOrder;

                    updateItem.RenewUserID = msUser.MsUserID;
                    updateItem.RenewDate = DateTime.Now;

                    #endregion
                    sc.OdMkItem_Update(msUser, updateItem);
                }
            }

            // 詳細
            int detailNo = 1;//ShowOrderの番号振りなおしのためのNo
            foreach (NBaseData.DAC.OdMkShousaiItem detail in details)
            {
                NBaseData.DAC.OdMkShousaiItem updateDetail = sc.OdMkShousaiItem_GetRecord(msUser, detail.OdMkShousaiItemID);
                if (updateDetail == null)
                {
                    // 新規
                    #region 必要なデータの追加

                    // 番号の振り直し
                    detail.ShowOrder = detailNo++;

                    detail.RenewUserID = msUser.MsUserID;
                    detail.RenewDate = DateTime.Now;

                    #region 船用品・潤滑油IDの対応
                    Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
                    switch (irai_type)
                    {
                        case Iraisyubetu.修繕:
                            break;
                        case Iraisyubetu.船用品:
                            //// 船用品ID
                            //System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                            //    = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            //foreach (NBaseData.DAC.MsVesselItemVessel viv in vivs)
                            //{
                            //    if (viv.VesselItemName == detail.ShousaiItemName)
                            //    {
                            //        detail.MsVesselItemID = viv.MsVesselItemID;
                            //    }
                            //}

                            int categoryNumber = 0;
                            var item = items.Where(obj => obj.OdMkItemID == detail.OdMkItemID);
                            if (item.Count() > 0)
                            {
                                string itemName = item.First().ItemName;
                                var category = categorys.Where(obj => obj.CategoryName == itemName);
                                if (category.Count() > 0)
                                {
                                    categoryNumber = category.First().CategoryNumber;
                                }
                            }
                            System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                                = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            var viv = vivs.Where(obj => obj.CategoryNumber == categoryNumber && obj.VesselItemName == detail.ShousaiItemName);
                            if (viv.Count() > 0)
                            {
                                detail.MsVesselItemID = viv.First().MsVesselItemID;
                            }
                            break;
                        case Iraisyubetu.燃料潤滑油:
                            // 潤滑油ID
                            System.Collections.Generic.List<NBaseData.DAC.MsLoVessel> lvs
                                = sc.MsLoVessel_GetRecordsByMsVesselID(msUser, vesselID);
                            foreach (NBaseData.DAC.MsLoVessel lv in lvs)
                            {
                                if (lv.MsLoName == detail.ShousaiItemName)
                                {
                                    detail.MsLoID = lv.MsLoID;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #endregion
                    sc.OdMkShousaiItem_Insert(msUser, detail);
                }
                else
                {
                    // 更新
                    #region 必要なデータの複写

                    // 番号の振り直し
                    updateDetail.ShowOrder = detailNo++;

                    updateDetail.ShousaiItemName = detail.ShousaiItemName;
                    updateDetail.Count = detail.Count;
                    updateDetail.Tanka = detail.Tanka;
                    updateDetail.Bikou = detail.Bikou;

                    updateDetail.RenewUserID = msUser.MsUserID;
                    updateDetail.RenewDate = DateTime.Now;

                    #region 船用品・潤滑油IDの対応
                    Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
                    switch (irai_type)
                    {
                        case Iraisyubetu.修繕:
                            break;
                        case Iraisyubetu.船用品:

                            //// 船用品ID
                            //System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                            //    = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            //foreach (NBaseData.DAC.MsVesselItemVessel viv in vivs)
                            //{
                            //    if (viv.VesselItemName == updateDetail.ShousaiItemName)
                            //    {
                            //        updateDetail.MsVesselItemID = viv.MsVesselItemID;
                            //    }
                            //}

                            int categoryNumber = 0;
                            var item = items.Where(obj => obj.OdMkItemID == detail.OdMkItemID);
                            if (item.Count() > 0)
                            {
                                string itemName = item.First().ItemName;
                                var category = categorys.Where(obj => obj.CategoryName == itemName);
                                if (category.Count() > 0)
                                {
                                    categoryNumber = category.First().CategoryNumber;
                                }
                            }
                            System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                                = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            var viv = vivs.Where(obj => obj.CategoryNumber == categoryNumber && obj.VesselItemName == detail.ShousaiItemName);
                            if (viv.Count() > 0)
                            {
                                detail.MsVesselItemID = viv.First().MsVesselItemID;
                            }
                            break;
                        case Iraisyubetu.燃料潤滑油:
                            // 潤滑油ID
                            System.Collections.Generic.List<NBaseData.DAC.MsLoVessel> lvs
                                = sc.MsLoVessel_GetRecordsByMsVesselID(msUser, vesselID);
                            foreach (NBaseData.DAC.MsLoVessel lv in lvs)
                            {
                                if (lv.MsLoName == updateDetail.ShousaiItemName)
                                {
                                    updateDetail.MsLoID = lv.MsLoID;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #endregion
                    sc.OdMkShousaiItem_Update(msUser, updateDetail);
                }
            }

            #region 削除分データ
            foreach (ListItem li in ListBox_DeleteID.Items)
            {
                string[] tmpst = li.Value.Split(':');
                string type = tmpst[0];
                string id = tmpst[1];

                if (type == "1")
                {
                    // 品目の削除
                    NBaseData.DAC.OdMkItem item = new NBaseData.DAC.OdMkItem();
                    item.OdMkItemID = id;
                    sc.OdMkItem_Delete(msUser, item);
                }
                else if (type == "2")
                {
                    // 詳細の削除
                    NBaseData.DAC.OdMkShousaiItem item = new NBaseData.DAC.OdMkShousaiItem();
                    item.OdMkShousaiItemID = id;
                    sc.OdMkShousaiItem_Delete(msUser, item);
                }
            }
            #endregion

        }
        #endregion

        // アラート表示のスクリプト生成(画面内に表示↓に変更)
        //RegisterStartupScript("Result", "<script language='JavaScript'>alert('保存しました');</script>");
        Label_Result.Text = "データを保存しました(" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ")";
    }
    protected void Button_Commit_Click(object sender, EventArgs e)
    {
        #region 入力データチェック/バリデーション
        updateSession();

        {
            try
            {
                decimal.Parse(HiddenField_Mitumorigaku.Value);
                decimal.Parse(TextBox_Nebiki.Text);
                decimal.Parse(TextBox_Tax.Text);
                decimal.Parse(Label_Goukei.Text);

                // 納期・年月日
                string yyyy = TextBox_Nouki_Year.Text;
                string MM = TextBox_Nouki_Month.Text;
                string dd = TextBox_Nouki_Day.Text;

                if (yyyy.Length != 0 &&
                    MM.Length != 0 &&
                    dd.Length != 0)
                {
                    DateTime.Parse(yyyy + "/" + MM + "/" + dd);
                }

                // テーブルデータ
                System.Collections.Generic.List<NBaseData.DAC.OdMkItem> tmpitems
                    = new System.Collections.Generic.List<NBaseData.DAC.OdMkItem>();
                System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> tmpdetails
                    = new System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>();

                // Sessionから情報を取込
                tmpitems = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
                tmpdetails = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];

                foreach (NBaseData.DAC.OdMkItem item in tmpitems)
                {
                    if (item.ItemName == string.Empty)
                    {
                        RegisterStartupScript("Result", "<script language='JavaScript'>alert('必須入力項目が入力されていません');</script>");
                        return;
                    }
                }

                foreach (NBaseData.DAC.OdMkShousaiItem detail in tmpdetails)
                {
                    if (detail.ShousaiItemName == string.Empty)
                    {
                        RegisterStartupScript("Result", "<script language='JavaScript'>alert('必須入力項目が入力されていません');</script>");
                        return;
                    }
                }
                if (TextBox_YuukouKigen.Text.Length == 0)
                {
                    RegisterStartupScript("Result", "<script language='JavaScript'>alert('必須入力項目が入力されていません');</script>");
                    return;
                }
                if (TextBox_Nouki_Year.Text.Length == 0 ||
                    TextBox_Nouki_Month.Text.Length == 0 ||
                    TextBox_Nouki_Day.Text.Length == 0)
                {
                    RegisterStartupScript("Result", "<script language='JavaScript'>alert('必須入力項目が入力されていません');</script>");
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region データ取り込み

        // セッション更新
        updateSession();

        // 有効期限・納期
        string yuukouKigen = TextBox_YuukouKigen.Text;
        string nouki_y = TextBox_Nouki_Year.Text;
        string nouki_m = TextBox_Nouki_Month.Text;
        string nouki_d = TextBox_Nouki_Day.Text;
        string nouki = "";
        if (nouki_y.Length != 0 &&
            nouki_m.Length != 0 &&
            nouki_d.Length != 0)
        {
            nouki = nouki_y + "/" + nouki_m + "/" + nouki_d;
        }

        // 金額関係
        decimal Amount = decimal.Parse(HiddenField_Mitumorigaku.Value);
        decimal nebiki = decimal.Parse(TextBox_Nebiki.Text);
        decimal tax = decimal.Parse(TextBox_Tax.Text);
        decimal goukei = decimal.Parse(Label_Goukei.Text);

        // テーブルデータ
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> items
            = new System.Collections.Generic.List<NBaseData.DAC.OdMkItem>();
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> details
            = new System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>();

        // Sessionから情報を取込
        items = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        details = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];

        int vesselID = 0;
        try
        {

            vesselID = int.Parse(HiddenField_VesselID.Value);
        }
        catch
        {
        }

        #endregion

        #region データ処理(登録・削除)
        using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.MsUserID = msUserID_Customer;
            string webkey = this.Request.QueryString["WebKey"];
            NBaseData.DAC.OdMk odmk = sc.OdMk_GetRecordByWebKey(msUser, webkey);

            System.Collections.Generic.List<NBaseData.DAC.MsVesselItemCategory> categorys = null;
            if ((Iraisyubetu)Session[MakeSessionKey("依頼種別")] == Iraisyubetu.船用品)
            {
                categorys = sc.MsVesselItemCategory_GetRecords(msUser);
            }

            #region 見積回答
            odmk.Tantousha = TextBox_担当者.Text;

            if (nouki.Length != 0)
                odmk.Nouki = DateTime.Parse(nouki);
            odmk.MkYukouKigen = yuukouKigen;

            odmk.Amount = Amount;
            odmk.MkAmount = nebiki;
            odmk.Tax = tax;

            odmk.MkDate = DateTime.Today;

            odmk.Status = 1;


            odmk.RenewDate = DateTime.Now;
            odmk.RenewUserID = msUser.MsUserID;

            sc.OdMk_Update(msUser, odmk);

            sc.BLC_発注アラーム処理_見積回答アラーム停止(msUser, odmk.OdMkID);
            #endregion

            // 品目
            foreach (NBaseData.DAC.OdMkItem item in items)
            {
                NBaseData.DAC.OdMkItem updateItem = sc.OdMkItem_GetRecord(msUser, item.OdMkItemID);
                if (updateItem == null)
                {
                    // 新規
                    #region 必要なデータの追加

                    item.OdMkID = odmk.OdMkID;

                    item.RenewUserID = msUser.MsUserID;
                    item.RenewDate = DateTime.Now;

                    #endregion
                    sc.OdMkItem_Insert(msUser, item);
                }
                else
                {
                    // 更新
                    #region 必要なデータの複写・書換

                    updateItem.ItemName = item.ItemName;
                    updateItem.MsItemSbtID = item.MsItemSbtID;
                    updateItem.Bikou = item.Bikou;

                    updateItem.RenewUserID = msUser.MsUserID;
                    updateItem.RenewDate = DateTime.Now;

                    #endregion
                    sc.OdMkItem_Update(msUser, updateItem);
                }
            }

            // 詳細
            foreach (NBaseData.DAC.OdMkShousaiItem detail in details)
            {
                NBaseData.DAC.OdMkShousaiItem updateDetail = sc.OdMkShousaiItem_GetRecord(msUser, detail.OdMkShousaiItemID);
                if (updateDetail == null)
                {
                    // 新規
                    #region 必要なデータの追加

                    detail.RenewUserID = msUser.MsUserID;
                    detail.RenewDate = DateTime.Now;

                    #region 船用品・潤滑油IDの対応
                    Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
                    switch (irai_type)
                    {
                        case Iraisyubetu.修繕:
                            break;
                        case Iraisyubetu.船用品:
                            // 船用品ID
                            //System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                            //    = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            //foreach (NBaseData.DAC.MsVesselItemVessel viv in vivs)
                            //{
                            //    if (viv.VesselItemName == detail.ShousaiItemName)
                            //    {
                            //        detail.MsVesselItemID = viv.MsVesselItemID;
                            //    }
                            //}
                            int categoryNumber = 0;
                            var item = items.Where(obj => obj.OdMkItemID == detail.OdMkItemID);
                            if (item.Count() > 0)
                            {
                                string itemName = item.First().ItemName;
                                var category = categorys.Where(obj => obj.CategoryName == itemName);
                                if (category.Count() > 0)
                                {
                                    categoryNumber = category.First().CategoryNumber;
                                }
                            }
                            System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                                = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            var viv = vivs.Where(obj => obj.CategoryNumber == categoryNumber && obj.VesselItemName == detail.ShousaiItemName);
                            if (viv.Count() > 0)
                            {
                                detail.MsVesselItemID = viv.First().MsVesselItemID;
                            }
                            break;
                        case Iraisyubetu.燃料潤滑油:
                            // 潤滑油ID
                            System.Collections.Generic.List<NBaseData.DAC.MsLoVessel> lvs
                                = sc.MsLoVessel_GetRecordsByMsVesselID(msUser, vesselID);
                            foreach (NBaseData.DAC.MsLoVessel lv in lvs)
                            {
                                if (lv.MsLoName == detail.ShousaiItemName)
                                {
                                    detail.MsLoID = lv.MsLoID;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #endregion
                    sc.OdMkShousaiItem_Insert(msUser, detail);
                }
                else
                {
                    // 更新
                    #region 必要なデータの複写

                    updateDetail.ShousaiItemName = detail.ShousaiItemName;
                    updateDetail.Count = detail.Count;
                    updateDetail.Tanka = detail.Tanka;
                    updateDetail.Bikou = detail.Bikou;

                    updateDetail.RenewUserID = msUser.MsUserID;
                    updateDetail.RenewDate = DateTime.Now;

                    #region 船用品・潤滑油IDの対応
                    Iraisyubetu irai_type = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
                    switch (irai_type)
                    {
                        case Iraisyubetu.修繕:
                            break;
                        case Iraisyubetu.船用品:
                            // 船用品ID
                            //System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                            //    = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            //foreach (NBaseData.DAC.MsVesselItemVessel viv in vivs)
                            //{
                            //    if (viv.VesselItemName == updateDetail.ShousaiItemName)
                            //    {
                            //        updateDetail.MsVesselItemID = viv.MsVesselItemID;
                            //    }
                            //}
                            int categoryNumber = 0;
                            var item = items.Where(obj => obj.OdMkItemID == detail.OdMkItemID);
                            if (item.Count() > 0)
                            {
                                string itemName = item.First().ItemName;
                                var category = categorys.Where(obj => obj.CategoryName == itemName);
                                if (category.Count() > 0)
                                {
                                    categoryNumber = category.First().CategoryNumber;
                                }
                            }
                            System.Collections.Generic.List<NBaseData.DAC.MsVesselItemVessel> vivs
                                = sc.MsVesselItemVessel_GetRecordByMsVesselID(msUser, vesselID);
                            var viv = vivs.Where(obj => obj.CategoryNumber == categoryNumber && obj.VesselItemName == detail.ShousaiItemName);
                            if (viv.Count() > 0)
                            {
                                detail.MsVesselItemID = viv.First().MsVesselItemID;
                            }
                            break;
                        case Iraisyubetu.燃料潤滑油:
                            // 潤滑油ID
                            System.Collections.Generic.List<NBaseData.DAC.MsLoVessel> lvs
                                = sc.MsLoVessel_GetRecordsByMsVesselID(msUser, vesselID);
                            foreach (NBaseData.DAC.MsLoVessel lv in lvs)
                            {
                                if (lv.MsLoName == updateDetail.ShousaiItemName)
                                {
                                    updateDetail.MsLoID = lv.MsLoID;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #endregion
                    sc.OdMkShousaiItem_Update(msUser, updateDetail);
                }
            }

            #region 削除分データ
            foreach (ListItem li in ListBox_DeleteID.Items)
            {
                string[] tmpst = li.Value.Split(':');
                string type = tmpst[0];
                string id = tmpst[1];

                if (type == "1")
                {
                    // 品目の削除
                    NBaseData.DAC.OdMkItem item = new NBaseData.DAC.OdMkItem();
                    item.OdMkItemID = id;
                    sc.OdMkItem_Delete(msUser, item);
                }
                else if (type == "2")
                {
                    // 詳細の削除
                    NBaseData.DAC.OdMkShousaiItem item = new NBaseData.DAC.OdMkShousaiItem();
                    item.OdMkShousaiItemID = id;
                    sc.OdMkShousaiItem_Delete(msUser, item);
                }
            }
            #endregion


            // 20141008: 201410月度改造
            //メール送信
            string errMessage = "";
            bool ret = sc.BLC_見積回答メール送信(msUser, odmk.OdMkID, ref errMessage);

            //if (ret == false)
            //{
            //    Label_Result.Text = "メール送信に失敗しました(" + errMessage + ")";
            //    return;
            //}
            //else
            //{
            //    Label_Result.Text = "メール送信に成功";
            //    return;
            //}
        }
        #endregion


        // ページ移動
        string url = "../NBaseVendor/MitsumoriKaitou2.aspx?WebKey=" + Request.QueryString["WebKey"];
        Response.Redirect(url);
    }

    protected void Button_ExportCSV_Click(object sender, EventArgs e)
    {
        updateSession();// Session更新

        // Sessionから情報を取込
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];

        #region データ取込
        Hashtable ht_tani = new Hashtable();
        Hashtable ht_kubun = new Hashtable();
        using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.MsUserID = msUserID_Customer;

            #region 単位マスタ
            System.Collections.Generic.List<NBaseData.DAC.MsTani> tanis 
                = sc.MsTani_GetRecords(msUser);

            foreach (NBaseData.DAC.MsTani tani in tanis)
            {
                ht_tani.Add(tani.MsTaniID, tani.TaniName);
            }
            #endregion

            #region 区分
            System.Collections.Generic.List<NBaseData.DAC.MsItemSbt> kbns
                = sc.MsItemSbt_GetRecords(msUser);

            foreach (NBaseData.DAC.MsItemSbt kbn in kbns)
            {
                ht_kubun.Add(kbn.MsItemSbtID, kbn.ItemSbtName);
            }

            #endregion
        }
        #endregion

        #region 出力文字列生成
        string buf = string.Empty;

        #region 20100305 ヘッダー文字列追加\r\n
        string header = ",部署名,区分,仕様・型式,詳細品目,単位,数量,単価,備考\r\n";
        buf += header;
        #endregion

        int lineNo = 0;
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (detail.OdMkItemID == item.OdMkItemID)
                {
                    lineNo++;

                    #region 行生成 No/ヘッダー/区分/品目名/詳細品目名/単位/数量/単価/備考
                    buf += lineNo.ToString() + ",";
                    buf += item.Header + ",";
                    string kubun = (item.MsItemSbtID == null || ht_kubun[item.MsItemSbtID] == null) ? string.Empty : ht_kubun[item.MsItemSbtID].ToString();
                    buf += kubun +",";
                    buf += item.ItemName.Replace("\r", "").Replace("\n", "") + ",";
                    buf += detail.ShousaiItemName.Replace("\r", "").Replace("\n", "") + ",";
                    string tanni = (ht_tani[detail.MsTaniID] == null) ? string.Empty : ht_tani[detail.MsTaniID].ToString();
                    buf += tanni + ",";
                    buf += detail.Count + ",";
                    buf += detail.Tanka + ",";
                    buf += detail.Bikou.Replace("\r", "").Replace("\n", "");

                    buf += "\r\n";
                    #endregion
                }
            }
        }

        #endregion

        #region CSV出力

        byte[] b_arrey = System.Text.Encoding.Default.GetBytes(buf);
        Response.ContentType = "application/csv";
        Response.AppendHeader("content-disposition", "inline; filename=" + HttpUtility.UrlEncode("見積回答") + ".csv");
        Response.OutputStream.Write(b_arrey, 0, b_arrey.Length);
        Response.End();

        #endregion
    }

    protected void Button_ImportCSV_Click(object sender, EventArgs e)
    {
        #region Sessionデータの取り込み(ODMKITEM/ODMKSHOUSAIITEM)
        updateSession();// Session更新

        // Sessionから情報を取込
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkItem>)Session[MakeSessionKey("LIST_ODMKITEM")];
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList
            = (System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>)Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")];
        Iraisyubetu iraiSyubetu = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
        #endregion

        #region データ取込
        Hashtable ht_tani = new Hashtable();
        Hashtable ht_kubun = new Hashtable();
        System.Collections.Generic.List<NBaseData.DAC.MsTani> tanis = null;
        using (NBaseService.ServiceClient sc = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.MsUserID = msUserID_Customer;

            #region 単位マスタ
            tanis = sc.MsTani_GetRecords(msUser);

            foreach (NBaseData.DAC.MsTani tani in tanis)
            {
                ht_tani.Add(tani.MsTaniID, tani.TaniName);
            }
            #endregion

            #region 区分
            System.Collections.Generic.List<NBaseData.DAC.MsItemSbt> kbns
                = sc.MsItemSbt_GetRecords(msUser);

            foreach (NBaseData.DAC.MsItemSbt kbn in kbns)
            {
                ht_kubun.Add(kbn.MsItemSbtID, kbn.ItemSbtName);
            }

            #endregion
        }
        #endregion

        #region CSVからデータ取込
        string[] csvLines = new string[0];

        #region 各INDEX
        int CSVINDEX_NO = 0;
        int CSVINDEX_BUSYO = 1;// 部署
        int CSVINDEX_KUBUN = 2;// 区分
        int CSVINDEX_SIYOU = 3;// 仕様・型式
        int CSVINDEX_SYOUSAI = 4;// 詳細品目
        int CSVINDEX_TANNI = 5;// 単位
        int CSVINDEX_COUNT = 6;// 数量
        int CSVINDEX_PRICE = 7;// 単価
        int CSVINDEX_REMARK = 8;// 備考
        #endregion

        if (FileUpload_CSV.FileBytes.Length == 0)
        {
            // ファイルが空なので帰る

            return;
        }

        using (MemoryStream mem = new MemoryStream(FileUpload_CSV.FileBytes))
        {
            using (StreamReader sr = new StreamReader(mem, Encoding.GetEncoding("shift_jis")))
            {
                string str = string.Empty;
                if ((str = sr.ReadToEnd()) != null)
                {
                    csvLines = str.Split('\n');
                }
            }
        }

        System.Collections.Generic.Dictionary<string, string> csvDatas = new System.Collections.Generic.Dictionary<string, string>();

        #region CSVデータチェック

        int lineNo = 0;
        string errorString = string.Empty;
        bool checkFlg = true;
        int addNo = 1;
        foreach (string str in csvLines)
        {
            lineNo++;
            if (lineNo == 1)
            {
                // ヘッダー行を飛ばす
                continue;
            }

            if (str.Length == 0)
            {
                continue;
            }

            checkFlg = true;
            string[] tmpLine = str.Split(',');
            if (tmpLine.Length < 9)
            {
                errorString += lineNo.ToString() + ":項目が足りません\\n";
                continue;
            }

            string str_No = tmpLine[CSVINDEX_NO];
            if (str_No.Length == 0)
            {
                str_No = "New" + addNo.ToString();
                addNo++;
            }
            string str_Count = tmpLine[CSVINDEX_COUNT];
            string str_Price = tmpLine[CSVINDEX_PRICE];

            string lineNoStr = (lineNo-1).ToString() + "行目";
            if (tmpLine[CSVINDEX_BUSYO].Length == 0)
            {
                errorString += lineNoStr + ":部署がありません\\n"; ;
                checkFlg = false;
            }
            //if (tmpLine[CSVINDEX_KUBUN].Length == 0) // 2010.04.02:aki
            if (iraiSyubetu == Iraisyubetu.修繕 && tmpLine[CSVINDEX_KUBUN].Length == 0)
            {
                errorString += lineNoStr + ":区分がありません\\n"; ;
                checkFlg = false;
            }
            if (tmpLine[CSVINDEX_SIYOU].Length == 0)
            {
                errorString += lineNoStr + ":仕様・型式がありません\\n"; ;
                checkFlg = false;
            }
            if (tmpLine[CSVINDEX_SYOUSAI].Length == 0)
            {
                errorString += lineNoStr + ":詳細品目がありません\\n"; ;
                checkFlg = false;
            }
            if (tmpLine[CSVINDEX_TANNI].Length == 0)
            {
                errorString += lineNoStr + ":単位がありません\\n"; ;
                checkFlg = false;
            }
            try
            {
                if (tmpLine[CSVINDEX_COUNT].Length == 0)
                {
                    errorString += lineNoStr + ":数量がありません\\n"; ;
                    checkFlg = false;
                }
                else
                {
                    int.Parse(str_Count);
                }
            }
            catch
            {
                errorString += lineNoStr + ":数量が数値以外の値です\\n"; ;
                checkFlg = false;
            }
            try
            {
                if (tmpLine[CSVINDEX_PRICE].Length == 0)
                {
                    errorString += lineNoStr + ":単価がありません\\n"; ;
                    checkFlg = false;
                }
                else
                {
                    int.Parse(str_Price);
                }
            }
            catch
            {
                errorString += lineNoStr + ":単価が数値以外の値です\\n"; ;
                checkFlg = false;
            }

            if (csvDatas.ContainsKey(str_No))
            {
                errorString += lineNoStr + ":番号が重複しています\\n"; ;
                checkFlg = false;
            }
            if (checkFlg)
            {
                csvDatas.Add(str_No, str);
            }
        }

        if (errorString != string.Empty)
        {
            // 表示のスクリプト生成
            RegisterStartupScript("Result", "<script language='JavaScript'>alert(\"" + errorString + "\");</script>");
            return;
        }

        #endregion

        #endregion

        #region CSVデータをSessionに反映

        // 番号でマッチングする
        lineNo = 0;
        foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
        {
            foreach (NBaseData.DAC.OdMkShousaiItem detail in odMkShousaiList)
            {
                if (item.OdMkItemID == detail.OdMkItemID)
                {
                    lineNo++;
                    if (csvDatas.ContainsKey(lineNo.ToString()) == false)
                        continue;

                    #region 行データ分解
                    string[] tmpLine = csvDatas[lineNo.ToString()].Split(',');
                    #endregion

                    detail.Count = int.Parse(tmpLine[CSVINDEX_COUNT]);
                    detail.Tanka = int.Parse(tmpLine[CSVINDEX_PRICE]);
                    #region 備考纏め
                    string remark = string.Empty;
                    for (int i = 0; i < tmpLine.Length; i++)
                    {
                        if (i >= CSVINDEX_REMARK)
                        {
                            remark += tmpLine[i];
                        }
                    }
                    #endregion
                    detail.Bikou = remark.Replace("\r", "").Replace("\n", "");


                    csvDatas.Remove(lineNo.ToString());
                }
            }
        }

        // マッチしなかったデータは、すべて追加扱いとする
        #region 新規追加分データ格納領域
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> addItemList
            = new System.Collections.Generic.List<NBaseData.DAC.OdMkItem>();
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> addDetailList
            = new System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem>();
        #endregion
        foreach (string key in csvDatas.Keys)
        {
            if (csvDatas[key].Length == 0)
                continue;

            #region 新規データ登録

            #region 行データ分解
            string[] tmpLine = csvDatas[key].Split(',');
            #endregion

            #region 属するITEM判定
            NBaseData.DAC.OdMkItem mkItem = null;
            foreach (NBaseData.DAC.OdMkItem item in odMkItemList)
            {
                if (iraiSyubetu != Iraisyubetu.修繕)
                {
                    if (item.Header == tmpLine[CSVINDEX_BUSYO] &&
                        item.ItemName.Replace("\r", "").Replace("\n", "") == tmpLine[CSVINDEX_SIYOU].Replace("\r", "").Replace("\n", ""))
                    {
                        mkItem = item;
                    }
                }
                else
                {
                    if (item.Header == tmpLine[CSVINDEX_BUSYO] &&
                        item.MsItemSbtName == tmpLine[CSVINDEX_KUBUN] &&
                        item.ItemName.Replace("\r", "").Replace("\n", "") == tmpLine[CSVINDEX_SIYOU].Replace("\r", "").Replace("\n", ""))
                    {
                        mkItem = item;
                    }
                }
            }
            foreach (NBaseData.DAC.OdMkItem item in addItemList)
            {
                if (iraiSyubetu != Iraisyubetu.修繕)
                {
                    if (item.Header == tmpLine[CSVINDEX_BUSYO] &&
                        item.ItemName.Replace("\r", "").Replace("\n", "") == tmpLine[CSVINDEX_SIYOU].Replace("\r", "").Replace("\n", ""))
                    {
                        mkItem = item;
                    }
                }
                else
                {
                    if (item.Header == tmpLine[CSVINDEX_BUSYO] &&
                        item.MsItemSbtName == tmpLine[CSVINDEX_KUBUN] &&
                        item.ItemName.Replace("\r", "").Replace("\n", "") == tmpLine[CSVINDEX_SIYOU].Replace("\r", "").Replace("\n", ""))
                    {
                        mkItem = item;
                    }
                }
            }
            #endregion

            NBaseData.DAC.OdMkItem newItem = null;
            if (mkItem == null)
            {
                newItem = new NBaseData.DAC.OdMkItem();
                newItem.OdMkItemID = Guid.NewGuid().ToString();
                newItem.Header = tmpLine[CSVINDEX_BUSYO];
                if (iraiSyubetu == Iraisyubetu.修繕)
                {
                    foreach (string id in ht_kubun.Keys)
                    {
                        if ((string)ht_kubun[id] == tmpLine[CSVINDEX_KUBUN])
                        {
                            newItem.MsItemSbtID = id;
                            newItem.MsItemSbtName = tmpLine[CSVINDEX_KUBUN];
                            break;
                        }
                    }
                }
                newItem.ItemName = tmpLine[CSVINDEX_SIYOU];
                addItemList.Add(newItem);
            }
            else
            {
                newItem = mkItem;
            }
            #region 詳細データ格納
            NBaseData.DAC.OdMkShousaiItem detail = new NBaseData.DAC.OdMkShousaiItem();
            detail.OdMkItemID = newItem.OdMkItemID;
            detail.OdMkShousaiItemID = Guid.NewGuid().ToString();

            detail.ShousaiItemName = tmpLine[CSVINDEX_SYOUSAI];
            detail.Tanka = int.Parse(tmpLine[CSVINDEX_PRICE]);
            detail.Count = int.Parse(tmpLine[CSVINDEX_COUNT]);
            #region 備考纏め
            string remark = string.Empty;
            for (int i = 0; i < tmpLine.Length; i++)
            {
                if (i >= CSVINDEX_REMARK)
                {
                    remark += tmpLine[i];
                }
            }
            #endregion
            detail.Bikou = remark.Replace("\r", "").Replace("\n", "");

            foreach (NBaseData.DAC.MsTani tani in tanis)
            {
                if (tani.TaniName == tmpLine[CSVINDEX_TANNI])
                {
                    detail.MsTaniID = tani.MsTaniID;
                    break;
                }
            }

            #endregion

            addDetailList.Add(detail);

            #endregion
        }


        #region リストに追加
        foreach (NBaseData.DAC.OdMkItem item in addItemList)
        {
            odMkItemList.Add(item);
        }
        foreach (NBaseData.DAC.OdMkShousaiItem item in addDetailList)
        {
            odMkShousaiList.Add(item);
        }
        #endregion

        // 変更をSessionへ
        Session[MakeSessionKey("LIST_ODMKITEM")] = odMkItemList;
        Session[MakeSessionKey("LIST_ODMKSHOUSAIITEM")] = odMkShousaiList;

        #endregion

        #region 画面への反映
        Iraisyubetu iraiType = (Iraisyubetu)Session[MakeSessionKey("依頼種別")];
        showHinmoku_onPage(0, odMkItemList, odMkShousaiList, iraiType);

        // 見積合計額がHiddenに反映されているので、表示に反映する
        Label_Mitsumorigaku.Text = HiddenField_Total.Value;
        HiddenField_Mitumorigaku.Value = HiddenField_Total.Value;

        decimal Amount = decimal.Parse(HiddenField_Mitumorigaku.Value);
        decimal nebiki = decimal.Parse(TextBox_Nebiki.Text);
        decimal tax = decimal.Parse(TextBox_Tax.Text);

        decimal goukei = Amount - nebiki + tax;
        Label_Goukei.Text = goukei.ToString();

        #endregion

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session[MakeSessionKey("ID")] == null)
            Session[MakeSessionKey("ID")] = 1;
        // 表示のスクリプト生成
        string webkey = Request.QueryString["WebKey"];
        string url = "select詳細品目.aspx?WebKey=" + webkey;
        RegisterStartupScript("Result", "<script language='JavaScript'>showModalDialog(\"" + url + "\",window.document.getElementById(\"TextBox1\"),\"dialogWidth:300px;dialogHeight:240px;edge:sunken ;status;no;\");</script>");
    }

    private string MakeSessionKey(string key)
    {
        string retKey = key;
        string webkey = Request.QueryString["WebKey"];
        if (webkey == null ||
            webkey == string.Empty)
        {
            // ALERT表示
            RegisterStartupScript("Result", "<script language='JavaScript'>alert('URLの指定が正しくありません');</script>");
            return retKey;
        }
        retKey += "_" + webkey;
        return retKey;
    }



    // 2010.06.24 以下コードは、ボタンが使用不可なのでいらないコードでは？
    protected void Button_MovePage_Click(object sender, EventArgs e)
    {
        string buf = string.Empty;

        #region データ取り込みとbufへの格納

        #region データ取得(DBから)
        string webKey = this.Request.QueryString["WebKey"];

        NBaseData.DAC.OdMk odMK = null;
        System.Collections.Generic.List<NBaseData.DAC.OdMkItem> odMkItemList = null;
        System.Collections.Generic.List<NBaseData.DAC.OdMkShousaiItem> odMkShousaiList = null;
        NBaseData.DAC.MsVessel vessel = null;
        NBaseData.DAC.OdMm odMM = null;

        decimal tax = 0;

        using (NBaseService.ServiceClient serviceClient = new NBaseService.ServiceClient())
        {
            NBaseData.DAC.MsUser msUser = new NBaseData.DAC.MsUser();
            msUser.LoginID = "1";
            odMK = serviceClient.OdMk_GetRecordByWebKey(msUser, webKey);

            odMM = serviceClient.OdMm_GetRecord(msUser, odMK.OdMmID);
            vessel = serviceClient.MsVessel_GetRecordsByMsVesselID(msUser, odMM.VesselID);

            odMkItemList = serviceClient.OdMkItem_GetRecordsByOdMkID(msUser, odMK.OdMkID);
            odMkShousaiList = serviceClient.OdMkShousaiItem_GetRecordByOdMkID(msUser, odMK.OdMkID);

            // 2014.03 : 2013年度改造
            //if (odMK.Status == 0)
            //{
            //    tax = NBaseCommon.Common.消費税率();
            //}
            //else
            //{
            //    tax = odMK.Tax;
            //}
            if (odMK.Tax > -1)
            {
                tax = odMK.Tax;
            }
        }
        #endregion

        #region bufへ格納(CSV)
        // header相当
        buf += odMM.MmNo + ",";
        buf += vessel.VesselName + ",";
        buf += odMK.OdThiNaiyou + ",";
        buf += odMK.MkKigen + ",";
        buf += odMM.MsShrJoukenID + ",";//MSSHRJOUKENより名称をとる
        buf += odMM.Okurisaki + ",";
        buf += odMK.MkYukouKigen + ",";
        buf += odMK.Nouki.ToString("yyyyMMdd");

        buf += "\r\n";

        // 品目・品目詳細

        buf += "\r\n";

        // 合計

        buf += "\r\n";

        #endregion

        #endregion

        // CSV出力
        byte[] b_arrey = System.Text.Encoding.Unicode.GetBytes(buf);
        Response.ContentType = "application/msword";
        Response.AppendHeader("content-disposition", "inline; filename=kaitou.csv");
        Response.OutputStream.Write(b_arrey, 0, b_arrey.Length);
        Response.End();


        // ページ移動
        //string url = "../NBaseVendor/MitsumoriKaitou2.aspx?WebKey=" + Request.QueryString["WebKey"];
        //Response.Redirect(url);
    }
}
