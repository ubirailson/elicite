<%@ Page Language="C#" MasterPageFile="~/MasterPageInicial.master" AutoEventWireup="true" 
    CodeFile="Index.aspx.cs" Inherits="Cefet.Elicite.Web.Index" Title="Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div id="formulario">
            <div class="form_login">
	            <p>
                    <strong>Usuário:</strong>
                    <asp:TextBox ID="txtUsuario" runat="server" 
                        MaxLength="45" Height="14px" Width="205px" TabIndex="1"></asp:TextBox>                        
                </p>
                <p>
                    <strong>Senha:&nbsp; </strong>&nbsp;<asp:TextBox ID="txtSenha" runat="server" 
                        Height="14px" Width="205px" TextMode="Password" MaxLength="15" TabIndex="2"></asp:TextBox>
                </p>
                <p>
                    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" TabIndex="3" />
                </p>
            </div>
        </div>
        <asp:Label ID="lblMensagem" runat="server" Width="270px" ForeColor="Black"></asp:Label>
    </div>
</asp:Content>