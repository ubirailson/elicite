<%@ Page Language="C#" MasterPageFile="~/MasterPageInicial.master" AutoEventWireup="true" CodeFile="Inicial.aspx.cs" Inherits="Inicial" Title="Página Inicial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<div style="text-align:center; float:left;">--%>
        <p>            
            <asp:Label ID="lblMensagem" runat="server" Text="Bem-vindo ao Elicite. Selecione um de seus projetos abaixo."></asp:Label>
        </p>
        <asp:GridView ID="grvProjetos"  runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            OnRowDataBound="grvProjetos_RowDataBound" OnSelectedIndexChanged="grvProjetos_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkProjeto" runat="server" CausesValidation="False" CommandName="Select"
                        Text="Selecionar registro"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
</asp:Content>

