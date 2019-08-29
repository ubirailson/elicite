<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="MatrizRequisitoParaCasoDeUso.aspx.cs" Inherits="Cefet.Elicite.Web.MatrizRequisitoParaCasoDeUso" 
Title="Matriz de Rastreabilidade de Caso de uso para requisito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
                <asp:GridView ID="grvMatrizCasoDeUso" AutoGenerateColumns="False" runat="server" 
                    OnRowDataBound="grvMatrizCasoDeUso_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblTexto" runat="server"></asp:Label>
                                <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </contenttemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblMensagem" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>