<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MitsumoriKaitou2.aspx.cs" Inherits="MitsumoriKaitou2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBase(見積回答)</title>
    <style type="text/css">
        #Btn_Close
        {
            width: 62px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <br />
        <br />
        <h1>見積回答</h1>
        <asp:Label ID="Label_会社" runat="server" Text="会社"></asp:Label>　　
        <asp:Label ID="Label_担当者" runat="server" Text="担当者"></asp:Label>
        　様<br />
        <br />
        <br />
        <label>御見積ありがとうございました</label>
        <br />
        <br />
        <br />
        <br />
        <INPUT id="Btn_Close" onclick="window.close();" type="button" value=" 閉じる "/>     
    </div>
    </form>
</body>
</html>
