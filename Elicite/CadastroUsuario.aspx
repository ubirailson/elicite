<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="CadastroUsuario.aspx.cs" Inherits="Cefet.Elicite.Web.CadastroUsuario" Title="Cadastro de Usuários" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <script type="text/javascript" src="include/CadastroUsuario.aspx.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            HeaderText="Campos preenchidos incorretamente" ValidationGroup="Geral" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
            HeaderText="Campos preenchidos incorretamente" ValidationGroup="Atualizacao" />
         <p>
            Nome do Usuário:
            <br />
            <asp:TextBox ID="txtNome" runat="server" Width="520px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                ErrorMessage="O campo nome do usuário deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvNomeAtualizacao" runat="server" 
                ErrorMessage="O campo nome do usuário deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
            <br />
           E-mail:
           <br />
            <asp:TextBox ID="txtEmail" runat="server" Width="270px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                ErrorMessage="O campo e-mail deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                ControlToValidate="txtEmail" ErrorMessage="O campo E-mail deve ser preenchido com texto válido" 
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Geral">*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvEmailAtualizacao" runat="server" 
                ErrorMessage="O campo e-mail deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator> 
            <asp:RegularExpressionValidator ID="revEmailAtualizacao" runat="server" 
                ControlToValidate="txtEmail" ErrorMessage="O campo E-mail deve ser preenchido com texto válido" 
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Atualizacao">*</asp:RegularExpressionValidator>
            Tipo do usuário:<asp:DropDownList ID="ddlTipo" runat="server" AppendDataBoundItems="True">
                </asp:DropDownList>
        </p>
        <p>
            <table>
                <tr>
                    <td>
                        Senha:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSenha" runat="server" MaxLength="8" TextMode="password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSenha" runat="server" 
                            ErrorMessage="Escolha uma senha. " 
                            ControlToValidate="txtSenha" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSenha" runat="server" 
                            ControlToValidate="txtSenha" ErrorMessage="O campo senha deve seguir a regra de senhas" 
                            ValidationExpression="^[a-z][a-zA-Z0-9]{7,7}$" ValidationGroup="Geral">*</asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="revSenhaAlteracao" runat="server" 
                            ControlToValidate="txtSenha" ErrorMessage="O campo senha deve seguir a regra de senhas" 
                            ValidationExpression="^[a-z][a-zA-Z0-9]{7,7}$" ValidationGroup="Atualizacao">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                       Confirmação:
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmacao" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
                        <asp:CustomValidator ID="cmvConfirmacao" runat="server" 
                            ErrorMessage="Senha e confirmação não batem."
                            ValidationGroup="Geral" ControlToValidate="txtSenha" ClientValidationFunction="confirmacaoSenha">*</asp:CustomValidator>
                        <asp:CustomValidator ID="cmvConfirmacaoAlteracao" runat="server" 
                            ErrorMessage="Senha e confirmação não batem."
                            ValidationGroup="Atualizacao" ControlToValidate="txtSenha" ClientValidationFunction="confirmacaoSenha">*</asp:CustomValidator>
                    </td>
                </tr>
            </table>
        </p>
        <p>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CausesValidation="True" 
                ValidationGroup="Geral" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnAlterar" runat="server" Text="Salvar" CausesValidation="true" Visible="false" 
                ValidationGroup="Atualizacao" OnClick="btnAlterar_Click" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CausesValidation="true" Visible="false" 
                ValidationGroup="Atualizacao" OnClientClick="return confirma()" OnClick="btnExcluir_Click"/>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Edição" Visible="false" 
                OnClick="btnCancelar_Click"/>
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"/>
            <asp:Label ID="lblMensagem" runat="server" Visible="False"></asp:Label>para regra de senha clique <a onclick="javascript:window.open('regras_de_senha.htm', 'regras', 'top=200, left=200,width=420,height=440,menubar=no,status=no,toolbar=no');" href="#">aqui</a>
        </p>
        </contenttemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grvUsuario" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAlterar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
        <p>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            EnablePaging="True"
            SelectCountMethod="CountUsuarios"
            StartRowIndexParameterName="numeroPagina"
            MaximumRowsParameterName="tamanhoPagina"
            SelectMethod="BuscarUsuarios"
            TypeName="Cefet.Elicite.Dominio.ServicoCadastro" 
            DataObjectTypeName="Cefet.Elicite.Dominio.Usuario">
        </asp:ObjectDataSource>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <contenttemplate>
        <asp:GridView ID="grvUsuario" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="grvUsuario_SelectedIndexChanged" DataKeyNames="Id" 
                OnPageIndexChanging="grvUsuario_PageIndexChanging" PageSize="10" OnRowDataBound="grvUsuario_RowDataBound">
            <Columns>
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/imagens/schview.gif" ShowSelectButton="True" SelectText="Selecionar registro" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:TemplateField HeaderText="Tipo de usuario">
                    <ItemTemplate>
                        <asp:Label ID="lblTipo" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
    </asp:GridView> 
    </contenttemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAlterar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        </p> 
    </div>
</asp:Content>
