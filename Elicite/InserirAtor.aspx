<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="InserirAtor.aspx.cs" Inherits="InserirAtor" Title="Cadastro de Atores no Projeto" %>
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
            Nome do Ator:
            <asp:TextBox ID="txtNome" runat="server" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                ErrorMessage="O campo nome do ator deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvNomeAtualizacao" runat="server" 
                ErrorMessage="O campo nome do ator deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
            <br />
            Descrição:
            <br />
            <asp:TextBox ID="txtDescricao" runat="server" Height="47px" TextMode="MultiLine" Width="600px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvResumo" runat="server" 
                ErrorMessage="O campo descrição do ator deve ser preenchido" 
                ControlToValidate="txtDescricao" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvResumoAtualizacao" runat="server" 
                ErrorMessage="O campo descrição do ator deve ser preenchido" 
                ControlToValidate="txtDescricao" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
            <br />
            <span id="txtDescricaoD">0</span> caracteres digitados. 
                Restam <span id="txtDescricaoR">300.</span>
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
            <asp:AsyncPostBackTrigger ControlID="grvAtor" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAlterar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
        <p>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            EnablePaging="True"
            SelectCountMethod="Count"
            StartRowIndexParameterName="numeroPagina"
            MaximumRowsParameterName="tamanhoPagina"
            SelectMethod="BuscarAtores"
            TypeName="Cefet.Elicite.Dominio.ServicoCadastro" 
            DataObjectTypeName="Cefet.Elicite.Dominio.Ator">
        </asp:ObjectDataSource>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <contenttemplate>
        <asp:GridView ID="grvAtor" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="grvAtor_SelectedIndexChanged" DataKeyNames="Id" 
                OnPageIndexChanging="grvAtor_PageIndexChanging" PageSize="5">
            <Columns>
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/imagens/schview.gif" ShowSelectButton="True" SelectText="Selecionar registro" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                <asp:BoundField DataField="Descricao" HeaderText="Descri&#231;&#227;o" SortExpression="Descricao" />
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

