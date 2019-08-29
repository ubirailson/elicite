<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserirRequisito.aspx.cs" 
Inherits="Cefet.Elicite.Web.InserirRequisito" Title="Inserção de Caso de Uso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            HeaderText="Campos preenchidos incorretamente" ValidationGroup="Geral" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
            HeaderText="Campos preenchidos incorretamente" ValidationGroup="Atualizacao" />
         <asp:Panel ID="pnlHistorico" runat="server" Visible="false">
            <p>
                <asp:Label ID="lblHistorico" runat="server" Text="Histórico de Revisão:" ></asp:Label>
                <br />
                <asp:TextBox ID="txtHistorico" runat="server" Height="47px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvHistorico" runat="server" 
                    ErrorMessage="O campo histórico deve ser preenchido" 
                    ControlToValidate="txtHistorico" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
                <br />
                <span id="txtHistoricoDigitado">0</span> caracteres digitados. 
                Restam <span id="txtHistoricoRestante">500.</span>
                <br />
                <asp:GridView ID="grvHistorico"  runat="server" AutoGenerateColumns="False" OnRowDataBound="grvHistorico_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Autor">
                            <ItemTemplate>
                                <asp:Label ID="lblAutor" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descricao" HeaderText="Descri&#231;&#227;o" />
                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}" 
                            HtmlEncode="False" />
                    </Columns>
                </asp:GridView>
            </p>
        </asp:Panel>
        <p>
            Nome do Requisito:
            <asp:TextBox ID="txtNome" runat="server" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                ErrorMessage="O campo nome do requisito deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvNomeAtualizacao" runat="server" 
                ErrorMessage="O campo nome do requisito deve ser preenchido" 
                ControlToValidate="txtNome" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
            <br />
            Tipo Requisito:
            <asp:DropDownList ID="ddlTipo" runat="server" AppendDataBoundItems="True">
            </asp:DropDownList>
            <br />
            Descrição:
            <br />
            <asp:TextBox ID="txtDescricao" runat="server" Height="47px" TextMode="MultiLine" Width="600px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvResumo" runat="server" 
                ErrorMessage="O campo descrição do requisito deve ser preenchido" 
                ControlToValidate="txtDescricao" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvResumoAtualizacao" runat="server" 
                ErrorMessage="O campo descrição do requisito deve ser preenchido" 
                ControlToValidate="txtDescricao" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
            <br />
            <span id="txtDescricaoD">0</span> caracteres digitados. 
                Restam <span id="txtDescricaoR">500.</span>
        </p>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CausesValidation="True" 
                    ValidationGroup="Geral" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnAtualizar" runat="server" Text="Salvar" CausesValidation="true" Visible="false" 
                        ValidationGroup="Atualizacao" OnClick="btnAtualizar_Click" />
            <asp:Label ID="lblMensagem" runat="server" Visible="False"></asp:Label>
     </div>
</asp:Content>