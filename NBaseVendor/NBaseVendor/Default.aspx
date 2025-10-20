<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBase(見積依頼)</title>
    <script language="javascript" type="text/javascript">
      function visibleSw()
      {
        var p = document.getElementById("table1");
        p.visible = false;
      }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <br />
        <br />
        <h1>見積依頼書</h1>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/NBase-Splash.png"/>
        <div style="text-align:center">
            <br />
            <table align="center" border="2"  frame="border">
                <tr>
                    <td colspan="2" bgcolor="#BBCCFF">
                        ログイン
                    </td>
                </tr>
                <tr>
                    <td width="100px" bgcolor="#BBCCFF">
                        ログインID
                    </td>
                    <td>
                        <asp:TextBox ID="LoginID_TextBox" runat="server" Width="200px" 
                            BorderStyle="None" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="100px" bgcolor="#BBCCFF">
                        パスワード
                    </td>
                    <td>
                        <asp:TextBox ID="Password_TextBox" runat="server" Width="200px" 
                            BorderStyle="None" TextMode="Password" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
            </table>  
            <br />
            <asp:Button ID="Login_Button" runat="server" Text="ログイン" 
                onclick="Login_Button_Click" TabIndex="3"/>
        </div>
    </div>
    </form>
</body>
</html>
