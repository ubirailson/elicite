<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="Cefet.Util.Web.error" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="estilo.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="cabecalho">
        </div>
        <div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;<br />
            &nbsp;
            <table border="0">
                <tr>
                    <td style="height: 21px">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="red" Text="Erro!"></asp:Label></td>
                </tr>
            </table>
            <br />
            <br />
        
        </div>
        <div id="rodape">
        &nbsp;
        </div>
    </form>
</body>
</html>