<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="CadastroProjeto.aspx.cs" Inherits="Cefet.Elicite.Web.CadastroProjeto" Title="Cadastro de Projetos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            HeaderText="Campos preenchidos incorretamente" ValidationGroup="Geral" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
            HeaderText="Campos preenchidos incorretamente" ValidationGroup="Atualizacao" />
         <p>
            Nome do Projeto:
            <asp:TextBox ID="txtNome" runat="server" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                ErrorMessage="O campo nome do projeto deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvNomeAtualizacao" runat="server" 
                ErrorMessage="O campo nome do projeto deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <table>
                <tr>
                    <td>Usuários disponíveis:</td>
                    <td></td>
                    <td>Usuários no projeto:</td>
                </tr>
                <tr>
                    <td>
                         <asp:ListBox ID="lstUsuariosDisponiveis" runat="server" SelectionMode="Single" 
                            Height="79px" Width="280px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="btnColocar" runat="server" Text=">  " 
                            Height="17px" OnClick="btnColocar_Click" /><br />
                        <br />
                        <br />
                        <asp:Button ID="btnTirar" runat="server" Text="<  " 
                            Height="17px" OnClick="btnTirar_Click" />
                    </td>
                    <td>
                        <asp:ListBox ID="lstUsuariosNoProjeto" runat="server" SelectionMode="Single" 
                            Height="79px" Width="280px"></asp:ListBox>
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
                ValidationGroup="Geral" OnClientClick="return confirma()" OnClick="btnExcluir_Click"/>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Edição" Visible="false" 
                OnClick="btnCancelar_Click"/>
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"/>
            <asp:Label ID="lblMensagem" runat="server" Visible="False"></asp:Label>
        </p>
        </contenttemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grvProjeto" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAlterar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
        <p>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            EnablePaging="True"
            SelectCountMethod="CountProjetos"
            StartRowIndexParameterName="numeroPagina"
            MaximumRowsParameterName="tamanhoPagina"
            SelectMethod="BuscarProjetos"
            TypeName="Cefet.Elicite.Dominio.ServicoCadastro" 
            DataObjectTypeName="Cefet.Elicite.Dominio.Projeto">
        </asp:ObjectDataSource>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <contenttemplate>
        <asp:GridView ID="grvProjeto" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="grvProjeto_SelectedIndexChanged" DataKeyNames="Id" 
                OnPageIndexChanging="grvProjeto_PageIndexChanging" PageSize="5">
            <Columns>
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/imagens/schview.gif" ShowSelectButton="True" SelectText="Selecionar registro" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
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