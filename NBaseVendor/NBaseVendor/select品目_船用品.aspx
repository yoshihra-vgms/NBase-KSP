<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select品目_船用品.aspx.cs" Inherits="select品目_船用品" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>仕様・型式検索 船用品</title>
    <script language="javascript" type="text/javascript">
    <!--
    function SetValue(value)
    {
        var tb = window.dialogArguments;
        
        var list = window.document.getElementById("ListBox_Items");
        
        if(list.selectedIndex < 0)
            return false;
        
        var selected = list[list.selectedIndex];
        
        tb.value = selected.text;
        
        return true;
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
    
        船用品選択<br />
    </div>
    <br />
    <br />
    &nbsp;
    <asp:ListBox ID="ListBox_Items" runat="server" Width="365px" Height="240px"></asp:ListBox>
    <br />
    <br />
    　&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
    <input TYPE="Button" ID="Button_決定" runat="server" 
        OnClick="if(SetValue('123')==true)window.close();" value="決定" 
        style="width: 100px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <input TYPE="Button" ID="Button_取消" runat="server" visible=false
        OnClick="closeWindow();" value="キャンセル" style="width: 100px" />
    </form>
    </form>
</body>
</html>
