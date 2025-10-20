<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select詳細品目.aspx.cs" Inherits="select詳細品目" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>詳細品目検索</title>
    <script language="javascript" type="text/javascript">
    <!--
    function SetValue(value)
    {
        var tb = window.dialogArguments;
        
        var list = window.document.getElementById("ListBox_Items");
        
        if(list.selectedIndex >= 0)
        {
            var selected = list[list.selectedIndex];
            
            tb.value = selected.text;
        }
    }
    
      function closeWindow() {
        //var w=window.open("","_top")
        //w.opener=window
        window.close();
      }
      -->
    </script>
    <base target="_self">
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        詳細品目選択<br />
    </div>
    <br />
    &nbsp;
    <asp:Label ID="Label1" runat="server" Text="名称の一部を入力して検索ボタンをクリックしてください"></asp:Label>
    <br />
    &nbsp;
    <asp:TextBox ID="TextBox_ItemName" runat="server" Width="290px"></asp:TextBox>
    　<asp:Button ID="Button_Search" runat="server" Text="検索" Width="57px" 
        onclick="Button_Search_Click" />
    <br />
    <br />
    &nbsp;
    <asp:ListBox ID="ListBox_Items" runat="server" Width="365px" Height="200px"></asp:ListBox>
    <br />
    <br />
    　&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <input TYPE="Button" ID="Button_決定" runat="server" 
        OnClick="SetValue('123');window.close();" value="決定" 
        style="width: 100px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <input TYPE="Button" ID="Button_取消" runat="server" 
        OnClick="closeWindow();" value="キャンセル" style="width: 100px" />
    <asp:HiddenField ID="HiddenField_VesselNo" runat="server" />
    <asp:HiddenField ID="HiddenField_Category" runat="server" />
    </form>
</body>
</html>
