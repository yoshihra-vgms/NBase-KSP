<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MitsumoriKaitou.aspx.cs" Inherits="MitsumoriKaitou" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBase(見積依頼)</title>
    <!--<script type="text/javascript">-->
    <script language="javascript" type="text/javascript">
    <!--
      function pageLoad() {
      }
      
      function checkMinus(num)
      {
        if(num < 0)
        {
            return(true);
        }
        else
        {
            return(false);
        }
      }
      
      function calcAmount(id)
      {
        var c,p,a;
        c = document.getElementById("TB_QTTY_ROW_" + id);
        p = document.getElementById("TB_PRICE_ROW_" + id);
        a = document.getElementById("TB_AMOUNT_ROW_" + id);
        
        if(checkMinus(c.value) == true)
        {
            window.alert("負の値が入っています。値が０に変更されます");
            c.value = 0;
            return false;
        }
        if(checkMinus(p.value) == true)
        {
            window.alert("負の値が入っています。値が０に変更されます");
            p.value = 0;
            return false;
        }
        
        a.value = c.value * p.value;
        
        calcTotal();
      }
      
      function calcTotal()
      {
        var rowlength; 
        var table;
        var iCnt; 
        var row;
        var id;
        var amt;
        var ttlAmount;
        var mit;
        var neb;
        var gou;
        
        table = document.getElementById("Table_Hinmoku");
        
        //return confirm('名称：' + list.length);

        ttlAmount = 0;
        rowlength=table.rows.length 
        for(iCnt=0;iCnt<rowlength;iCnt++){
            row = table.rows[iCnt];
            id = row.id.substring(4);
            amt = document.getElementById("TB_AMOUNT_ROW_" + id);
            if(amt!=null && amt.value !=""){
                ttlAmount = ttlAmount + parseFloat(amt.value);
            }
        }
 
        // 合計出力と値引き分計算
        mit = document.getElementById("Label_Mitsumorigaku");
        neb = document.getElementById("TextBox_Nebiki");
        gou = document.getElementById("Label_Goukei");
        
        hfmit = document.getElementById("HiddenField_Mitumorigaku");
        
       
        pageTTL = document.getElementById("HiddenField_Total_onPage");
        fullTTL = document.getElementById("HiddenField_Total");
        taxAmt = document.getElementById("TextBox_Tax");
        
        var trueTTL = 0;
        var pageT = pageTTL.value;
        var fullT = fullTTL.value;
        trueTTL = parseFloat(fullTTL.value) - parseFloat(pageTTL.value) + ttlAmount;
        mit.innerHTML = trueTTL;
        gou.innerHTML = trueTTL - neb.value + parseInt(taxAmt.value);
        hfmit.value = trueTTL;
        
      }
      
      function calcTax()
      {
        var mit;
        var tax;
        var taxAmt;
        
        //mit = document.getElementById("TextBox_Mitsumorigaku");
        gou = document.getElementById("Label_Goukei");
        tax = document.getElementById("HiddenField_Tax");
        taxAmt = document.getElementById("TextBox_Tax");
        
        taxAmt.innerHTML = gou.innerHTML * tax.value / 100;
      }
      
      
      s_allowed='0123456789';            //    半角数字(ここに入っていない文字があったらエラー)。
      s_error='半角数字以外は入力出来ません';    //    不正文字のエラーメッセージ。
      function CheckNumber(id,len)
      {
        s_len=0;
        
        tBox = document.getElementById(id);
        i=tBox.value;
        j='';
        
        while (i!='') {
            k=i.substring(0,1);
            i=i.substring(1,i.length);
            if (s_allowed.indexOf(k)<0)
            {
                window.alert(s_error);
                tBox.value = '0';
                return(false);
            }
            if(s_len < len)
            {
                j+=k; ++s_len;
            }
        }
        tBox.value = j; 
      }

      s2_error='桁が多すぎます';    //    不正文字のエラーメッセージ。
      function CheckDecimal(id,i_len,f_len)
      {
        sa_len=0;
        sb_len=0;
        
        tBox = document.getElementById(id);
        i=tBox.value;
        
        idx = i.indexOf('.');
        if(idx > -1)
        {
            ia = i.substring(0,idx);
            ja='';
            while (ia!='') {
                ka=ia.substring(0,1);
                ia=ia.substring(1,ia.length);
                if (s_allowed.indexOf(ka)<0)
                {
                    window.alert(s_error);
                    tBox.value = '';
                    return(false);
                }
                if(sa_len < i_len)
                {
                    ja+=ka; ++sa_len;
                }
                else{
                    window.alert(s2_error);
                    tBox.value = ja;
                    return(false);
                }
            }
            
            ib = i.substring(idx+1,i.length+1);
            jb='';
            while (ib!='') {
                kb=ib.substring(0,1);
                ib=ib.substring(1,ib.length);
                if (s_allowed.indexOf(kb)<0)
                {
                    window.alert(s_error);
                    tBox.value = '0';
                    return(false);
                }
                if(sb_len < f_len)
                {
                    jb+=kb; ++sb_len;
                }
            }
            
            tBox.value = ja + '.' + jb;
        }
        else
        {
            ia = i;
            ja='';
            while (ia!='') {
                ka=ia.substring(0,1);
                ia=ia.substring(1,i.length);
                if (s_allowed.indexOf(ka)<0)
                {
                    window.alert(s_error);
                    tBox.value = '0';
                    return(false);
                }
                if(sa_len < i_len)
                {
                    ja+=ka; ++sa_len;
                }
                else{
                    window.alert(s2_error);
                    tBox.value = ja;
                    return(false);
                }
            }
        }
      }
      
      function checkZenkaku(value) {
        for (var i = 0; i < value.length; ++i) {
          var c = value.charCodeAt(i);
          //  半角カタカナは不許可
          if (c < 256 || (c >= 0xff61 && c <= 0xff9f)) {
            return false;
          }
        }
        return true;
      }
      function getByteCount(value) {
        var count = 0;
        for ( var i = 0; i < value.length; ++i ) {
          var sub = value.substring(i, i + 1);
          //全角の場合２バイト追加。
          if( checkZenkaku(sub) ){
            count += 2;
          } else {
            count += 1;
          }
        }
        return count;
      }
      
      function CheckStringLen(id,len)
      {
        tBox = document.getElementById(id);
        i=tBox.value;
        
        if(getByteCount(i) > len)
        {
            window.alert('入力文字数が多すぎます');
            tBox.value = '';
            return(false);
        }
      }
      
      function checkTaxAmount()
      {
        taxAmtTxt = document.getElementById("TextBox_Tax");
        var tax = taxAmtTxt.value;
        
        if(tax == 0)
          return false;
        
         
        return true;
      }
      
      function visibleSw()
      {
        var p = document.getElementById("Button1");
        p.visible = false;
      }
      
      function checkDate(id_y,id_m,id_d)
      {
        //納期のチェック
        var tBox_y = document.getElementById(id_y);
        var tBox_m = document.getElementById(id_m);
        var tBox_d = document.getElementById(id_d);

        var year = tBox_y.value;
        var month = tBox_m.value;
        var day = tBox_d.value;
        
        if( year.length != 0 &&
            month.length != 0 &&
            day.length != 0)
        {
            //判定用date関数を作成
            var dt = new Date(year, month -1 , day);

            //入力されている内容に不備があるか確認
            if( dt == null || 
                dt.getFullYear() != year || 
                dt.getMonth()+1 != month || 
                dt.getDate() != day)
            {
                window.alert('正しい日付を入力してください');
                tBox_d.value = "";
                return false;
            }
        }
        
        return true;
      }
      
      var codedown = null;
      
      function checkTextLen(id,len)
      {
        var tBox = document.getElementById(id);
        var text = tBox.value;
        
        var keycode = event.keyCode;
        codedown = keycode;
        var key = String.fromCharCode(keycode);
        if(key == "\b" || key == "\t")
        {
            return true;
        }
        
        if(text.length >= len)
        {
            return false;
        }

        return true;
        
      }
      
      function cutText(id,len)
      {
        var tBox = document.getElementById(id);
        var text = tBox.value;
        
        var keycode = event.keyCode;
        
        if( codedown != 229 ||
            (codedown == 229 && keycode == 13))
        {
            if(text.length > len)
            {
                var subText = text.substring(0,len);
                tBox.value = subText;
            }
        }
      }
      
      function checkEmpty(id , def)
      {
        var tBox = document.getElementById(id);
        var text = tBox.value;
        if(text.length == 0)
        {
            tBox.value = def;
            window.alert('必須項目が入力されていません');
            return false;
        }
        return true;
      }
      
      function checkNaN(id ,def)
      {
        var tBox = document.getElementById(id);
        var text = tBox.value;
        
        idx = text.indexOf('.');
        if(idx > -1)
        {
            tBox.value = def;
            window.alert('小数点は入力できません');
            return false;
        }

        if ( isNaN( parseInt( text ) ) )
        {
            tBox.value = def;
            window.alert('数字を入力してください');
            return false;
        }
        else
        {
            var num = parseInt(text);
            if(num<0)
            {
                tBox.value = def;
                window.alert('マイナスは入力できません');
                return false;
            }
        }
        
        return true;

      }
      
      function checkHeaderInput()
      {
        // ヘッダ入力のチェック
        var tBox = document.getElementById('TextBox_Header');
        var strHeader = tBox.value;
        
        if (strHeader.length == 0 ||
            strHeader =='ここへ部署名（甲板部、機関部等）を入力してください')
        {
            window.alert('部署名（甲板部、機関部等）の入力をしてください');
            return false;
        }
        
        return true;
      }

      // 20101213Add
      function OnAddHinmokuClick()
      {
        if(checkHeaderInput() == false)
        {
            return false;
        }
      
        var iraiType = document.getElementById("HiddenField_IraiType");
      
        if(iraiType.value == '船用品')// 船用品の時
        {
            var tb = document.getElementById("HiddenField_Hinmoku");//document.getElementById("HiddenField_Hinmoku");
            tb.value = '';
            
            // 品目選択ダイアログを開き、決定の時はtrueを返す
            var sRet //ダイアログからの返り値を格納
            sRet = showModalDialog("select品目_船用品.aspx",window.document.getElementById("HiddenField_Hinmoku"),"dialogWidth:480px;dialogHeight:400px;edge:sunken ;status;no;");

            if(tb.value == '')
            {
                // ×で閉じたor入力なし
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            // 船用品以外はtrueを返す
            return true;
        }
      }
    //-->
    </script>
    <style type="text/css">
        body {
            font-size: 90%;
        }
        
        
        .style3
        {
            height: 11px;
        }
        .style5
        {
            width: 1121px;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <!--<asp:ScriptManager ID="ScriptManager1" runat="server" />-->
        <h1>見積書&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
    </div>
    <div>
        <asp:Label ID="Label_会社" runat="server" Text="会社"></asp:Label>　　
         
        <asp:TextBox ID="TextBox_担当者" runat="server" MaxLength="50" BorderColor="White" 
            BorderStyle="None"></asp:TextBox>
        　様<br />
        <br />
        <table border="2" width="900px" frame="border">
            <tr>
                <td width="100px" bgcolor="#BBCCFF">
                    見積依頼内容
                </td>
                <td colspan=3 width="780px">
                    <asp:TextBox ID="TextBox_Naiyou" runat="server" Width="100%" ReadOnly="True" 
                        BorderStyle="None"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100px" bgcolor="#BBCCFF" class="style3" >
                    見積依頼番号
                </td>
                <td width="330px">
                    <asp:TextBox ID="TextBox_MK_NO" runat="server" Width="100%" ReadOnly="True" 
                        BorderStyle="None"></asp:TextBox>
                </td>
                <td width="100px" bgcolor="#BBCCFF">
                    船名
                </td>
                <td  width="330px">
                    <asp:TextBox ID="TextBox_VesselName" runat="server" Width="100%" 
                        ReadOnly="True" BorderStyle="None"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100px" bgcolor="#BBCCFF">
                    見積回答期限
                </td>
                <td width="330px">
                    <asp:TextBox ID="TextBox_MKKigen" runat="server" Width="100%" ReadOnly="True" 
                        BorderStyle="None"></asp:TextBox>
                </td>
                <td width="100px" bgcolor="#BBCCFF">
                    支払条件
                </td>
                <td width="330px">
                    <asp:TextBox ID="TextBox_Jouken" runat="server" Width="100%" ReadOnly="True" 
                        BorderStyle="None"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100px" bgcolor="#BBCCFF">
                    送り先
                </td>
                <td width="330px">
                    <asp:TextBox ID="TextBox_Okurisaki" runat="server" Width="100%" ReadOnly="True" 
                        BorderStyle="None"></asp:TextBox>
                </td>
                <td colspan="2">&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table border="2" width="900px" frame="box">
            <tr>
                <td width="100px" bgcolor="#BBCCFF">
                    ※見積有効期限
                </td>
                <td width="330px">
                    <asp:TextBox ID="TextBox_YuukouKigen" runat="server" Width="100%" 
                        OnChange="CheckStringLen('TextBox_YuukouKigen',100);" MaxLength="50"></asp:TextBox>
                </td>
                <td width="100px" bgcolor="#BBCCFF">
                    ※納期
                </td>
                <td width="330px">
                    <asp:TextBox ID="TextBox_Nouki_Year" runat="server" Width="60px" 
                        style="text-align: right;" OnChange="CheckNumber('TextBox_Nouki_Year',4); checkDate('TextBox_Nouki_Year','TextBox_Nouki_Month','TextBox_Nouki_Day');" 
                        MaxLength="4"></asp:TextBox>
                    年<asp:TextBox ID="TextBox_Nouki_Month" runat="server" Width="36px" 
                        style="text-align: right;" OnChange="CheckNumber('TextBox_Nouki_Month',2); checkDate('TextBox_Nouki_Year','TextBox_Nouki_Month','TextBox_Nouki_Day');" 
                        MaxLength="2"></asp:TextBox>
                    月<asp:TextBox ID="TextBox_Nouki_Day" runat="server" Width="36px" 
                        style="text-align: right;" OnChange="CheckNumber('TextBox_Nouki_Day',2); checkDate('TextBox_Nouki_Year','TextBox_Nouki_Month','TextBox_Nouki_Day');" 
                        MaxLength="2"></asp:TextBox>
                    日</td>
            </tr>
        </table>
        <br />
        <table width="900px" border="2">
            <tr>
                <td width="100px" bgcolor="#BBCCFF" >
                    見積金額
                </td>
                <td width="115px">
                    <asp:Label ID="Label_Mitsumorigaku" runat="server"
                        style="text-align: right;" Width="100%"></asp:Label>
                    <asp:HiddenField ID="HiddenField_Mitumorigaku" runat="server" />
                </td>
                <td width="100px" bgcolor="#BBCCFF" >
                    ※見積値引き</td>
                <td width="115x">
                    <asp:TextBox ID="TextBox_Nebiki" runat="server" 
                        OnChange="checkNaN('TextBox_Nebiki',0); calcTotal();" 
                        style="text-align: right;" Width="97%" MaxLength="9">0</asp:TextBox>
                </td>
                <td width="100px" bgcolor="#BBCCFF" >
                    ※消費税</td>
                <td width="115px">
                    <asp:TextBox ID="TextBox_Tax" runat="server"
                        OnChange="checkNaN('TextBox_Tax',0); calcTotal();" 
                        style="text-align: right;" Width="97%" MaxLength="7"></asp:TextBox>
                </td>
                <td width="100px" bgcolor="#BBCCFF" >
                    合計金額
                </td>
                <td width="115px">
                    <asp:Label ID="Label_Goukei" runat="server"
                        style="text-align: right;" Width="100%"></asp:Label>
                </td>
           </tr>
        </table>        
        <div style="text-align:center; width: 897px;">
            <br />
            <asp:table  ID="Table_Buttons" border="2" runat="server">
                <asp:TableRow ID = "TableRow1">
                    <asp:TableCell>
                        &nbsp;&nbsp;
                        <asp:Button ID="Button_Commit" runat="server" Text="見積提出" width="80" 
                            onclick="Button_Commit_Click"/>
                        &nbsp;&nbsp; <asp:Button ID="Button_Save" runat="server" Text="保存" width="80" 
                            onclick="Button_Save_Click" UseSubmitBehavior="False"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </asp:TableCell>
                    <asp:TableCell>
                        &nbsp;
                        &nbsp;<asp:Button ID="Button_ExportCSV" runat="server" Text="CSV出力" 
                            onclick="Button_ExportCSV_Click" />
                        &nbsp;&nbsp;&nbsp; <asp:Button ID="Button_ImportCSV" runat="server" Text="CSV取込" 
                            onclick="Button_ImportCSV_Click" />
                        &nbsp;<asp:FileUpload ID="FileUpload_CSV" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID = "TableRow2">
                    <asp:TableCell ColumnSpan=2>
                        <asp:Label ID="Label_Result" runat="server" Text="" ForeColor="#FF3300"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:table>
        </div>
        <table id="Table_Hinmoku_Buttons" border="2" 
            style="border-style: none; border-color: Black; width: 1116px;">
            <tr>
                <td colspan="8" style="border-style: none" class="style5">
                    <asp:Button ID="Button_AddHinmoku" runat="server" Text="仕様・型式追加" 
                        onclick="Button_AddHinmoku_Click" style="width: 125px" 
                        onclientclick="return OnAddHinmokuClick();" />
                &nbsp;<asp:TextBox ID="TextBox_Header" runat="server" Width="378px" MaxLength="50">ここへ部署名（甲板部、機関部等）を入力してください</asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label_ItemHeader" runat="server" Font-Bold="True" 
            Font-Size="Medium" Font-Underline="True" Text="Label"></asp:Label>
        <asp:Table ID="Table_Pager" runat="server" BorderStyle="Solid" 
            GridLines="Both" Width="1280">
        </asp:Table>
        <asp:Table ID="Table_Hinmoku_Head" runat="server" BorderStyle="Solid" 
            GridLines="Both" Width="1280" BackColor="#BBCCFF">
            <asp:TableRow ID = "ROW_HEAD">
                <asp:TableCell Width="80">&nbsp;</asp:TableCell>
                <asp:TableCell width="30">No</asp:TableCell>
                <asp:TableCell width="92">区分</asp:TableCell>
                <asp:TableCell width="320">※仕様・型式／詳細品目</asp:TableCell>
                <asp:TableCell width="62">単位</asp:TableCell>
                <asp:TableCell width="80">※数量</asp:TableCell>
                <asp:TableCell width="80">※単価</asp:TableCell>
                <asp:TableCell width="80">金額</asp:TableCell>
                <asp:TableCell width="40">添付</asp:TableCell>
                <asp:TableCell width="350">備考</asp:TableCell>
                <asp:TableCell width="200" Visible="false">RowType
                    <asp:TextBox ID="TextBox_TYPE_ROW_HEAD" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell width="200" Visible="false">NextRowType
                    <asp:TextBox ID="TextBox_Next_ROW_HEAD" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell width="200" Visible="false">ID
                    <asp:TextBox ID="TextBox_ID_ROW_HEAD" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell width="200" Visible="false">NEXTID
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table_Hinmoku" runat="server" BorderStyle="Solid" 
            GridLines="Both" Width="1280">
        </asp:Table>
        <asp:ListBox ID="ListBox_RowID" runat="server" Visible="False"></asp:ListBox>
        <asp:ListBox ID="ListBox_DeleteID" runat="server" Visible="False"></asp:ListBox>
        &nbsp;<br />
        <asp:HiddenField ID="HiddenField_Tax" runat="server" />
        <asp:HiddenField ID="HiddenField_Total" runat="server" />
        <asp:HiddenField ID="HiddenField_Total_onPage" runat="server" />
        <asp:HiddenField ID="HiddenField_VesselID" runat="server" />
        <asp:HiddenField ID="HiddenField_Hinmoku" runat="server" />
        <asp:HiddenField ID="HiddenField_IraiType" runat="server" />
            <asp:Button ID="Button_MovePage" runat="server" Text="見積回答出力" 
                onclick="Button_MovePage_Click" Visible="False" />
        <br />
    </div>
    </form>
</body>
</html>
