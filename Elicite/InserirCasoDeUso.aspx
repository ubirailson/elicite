<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserirCasoDeUso.aspx.cs" 
Inherits="Cefet.Elicite.Web.InserirCasoDeUso" Title="Inserção de Caso de Uso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <script type="text/javascript" src="include/InserirCasoDeUso.aspx.js"></script>
        <div id="abas">
            <asp:HyperLink ID="lnk1" runat="server" CssClass="selecionado" TabIndex="17" AccessKey="1" NavigateUrl="#" ><span>Dados Gerais</span></asp:HyperLink>
            <asp:HyperLink ID="lnk2" runat="server" CssClass="naoselecionado" TabIndex="18" AccessKey="2" NavigateUrl="#" ><span>Fluxo Básico</span></asp:HyperLink>
            <asp:HyperLink ID="lnk3" runat="server" CssClass="naoselecionado" TabIndex="19" AccessKey="3" NavigateUrl="#" ><span>Sub-Fluxos</span></asp:HyperLink>
            <asp:HyperLink ID="lnk4" runat="server" CssClass="naoselecionado" TabIndex="20" AccessKey="4" NavigateUrl="#" ><span>Extends e Includes</span></asp:HyperLink>
            <asp:HyperLink ID="lnk5" runat="server" Visible="false" CssClass="naoselecionado" TabIndex="21" AccessKey="5" NavigateUrl="#" ><span>Histórico</span></asp:HyperLink>
        </div>        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    HeaderText="Campos preenchidos incorretamente" ValidationGroup="Geral" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                    HeaderText="Campos preenchidos incorretamente" ValidationGroup="Atualizacao" />
        <asp:Panel ID="pnlConteudo1" runat="server" CssClass="blocoselecionado">
            <p>
            Nome do Caso de uso:
            <asp:TextBox ID="txtNome" runat="server" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                                ErrorMessage="O campo nome do caso de uso deve ser preenchido" 
                                ControlToValidate="txtNome" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvNomeAtualizacao" runat="server" 
                                ErrorMessage="O campo nome do caso de uso deve ser preenchido" 
                                ControlToValidate="txtNome" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
            <br />
            Resumo:
            <br />
            <asp:TextBox ID="txtResumo" runat="server" Height="47px" TextMode="MultiLine" Width="600px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvResumo" runat="server" 
                                ErrorMessage="O campo resumo do caso de uso deve ser preenchido" 
                                ControlToValidate="txtResumo" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvResumoAtualizacao" runat="server" 
                                ErrorMessage="O campo resumo do caso de uso deve ser preenchido" 
                                ControlToValidate="txtResumo" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
                                <br />
                <span id="txtResumoD">0</span> caracteres digitados. 
                Restam <span id="txtResumoR">2000.</span>
            </p>
            <table>
                <tr>
                    <td>
                        Pré-condições:
                        <br />
                        <asp:TextBox ID="txtPrecondicoes" runat="server" Height="100px" TextMode="MultiLine" Width="337px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPrecondicoes" runat="server" 
                                ErrorMessage="O campo pré-condições deve ser preenchido" 
                                ControlToValidate="txtPrecondicoes" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvPrecondicoesAtualizacao" runat="server" 
                                ErrorMessage="O campo pré-condições deve ser preenchido" 
                                ControlToValidate="txtPrecondicoes" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
                        <br />
                        <span id="preCondicoesD">0</span> caracteres digitados. 
                        Restam <span id="preCondicoesR">2000.</span>
                    </td>
                    <td>
                        Pós-condições:
                        <br />
                        <asp:TextBox ID="txtPoscondicoes" runat="server" Height="100px" TextMode="MultiLine" Width="337px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPoscondicoes" runat="server" 
                                ErrorMessage="O campo pós-condições deve ser preenchido" 
                                ControlToValidate="txtPoscondicoes" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvPoscondicoesAtualizacao" runat="server" 
                                ErrorMessage="O campo pós-condições deve ser preenchido" 
                                ControlToValidate="txtPoscondicoes" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator> 
                        <br />
                        <span id="posCondicoesD">0</span> caracteres digitados. 
                        Restam <span id="posCondicoesR">2000.</span>                       
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanelAtores" runat="server">
            <contenttemplate>
            <table>
                <tr>
                    
                    <td>Atores a selecionar:</td>
                    <td></td>
                    <td>Atores selecionados:</td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lstAtoresSelecionar" runat="server" SelectionMode="Single" Height="103px" Width="300px">    
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="btnPassarAtorParaUc" runat="server" Text=">  " OnClick="btnPassarAtorParaUc_Click" /><br />
                        <br />
                        <br />
                        <asp:Button ID="btnTirarAtor" runat="server" Text="<  " OnClick="btnTirarAtor_Click" />
                    </td>
                    <td>
                        <asp:ListBox ID="lstAtoresSelecionados" runat="server" SelectionMode="Single" Height="103px" Width="300px">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>
            </contenttemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPassarAtorParaUc" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTirarAtor" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlConteudo2" runat="server" CssClass="bloconaoselecionado">
            <p>
                Nome Fluxo Básico:
                <asp:TextBox ID="txtNomeFluxoBasico" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNomeFluxoBasico" runat="server" 
                                ErrorMessage="O campo nome do fluxo básico deve ser preenchido" 
                                ControlToValidate="txtNomeFluxoBasico" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvNomeFluxoBasicoAtualizacao" runat="server" 
                                ErrorMessage="O campo nome do fluxo básico deve ser preenchido" 
                                ControlToValidate="txtNomeFluxoBasico" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
                <br />
                Detalhamento:<br />
                <asp:TextBox ID="txtDescricaoFluxoBasico" runat="server" Height="180px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescricaoFluxoBasico" runat="server" 
                                ErrorMessage="O campo detalhamento do fluxo básico deve ser preenchido" 
                                ControlToValidate="txtDescricaoFluxoBasico" ValidationGroup="Geral">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvDescricaoFluxoBasicoAtualizacao" runat="server" 
                                ErrorMessage="O campo detalhamento do fluxo básico deve ser preenchido" 
                                ControlToValidate="txtDescricaoFluxoBasico" ValidationGroup="Atualizacao">*</asp:RequiredFieldValidator>
                <br />
                <span id="txtDescricaoFluxoBasicoD">0</span> caracteres digitados. 
                Restam <span id="txtDescricaoFluxoBasicoR">2000.</span>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlConteudo3" runat="server" CssClass="bloconaoselecionado">
            <asp:UpdatePanel ID="UpdatePanelSubFluxo" runat="server">
            <contenttemplate>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                    HeaderText="Campos preenchidos incorretamente" ValidationGroup="SubFluxo" />
            <p>
                Nome Fluxo:
                <asp:TextBox ID="txtNomeSubFluxo" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNomeSubFluxo" runat="server" 
                                ErrorMessage="O campo nome do sub-fluxo deve ser preenchido" 
                                ControlToValidate="txtNomeSubFluxo" ValidationGroup="SubFluxo">*</asp:RequiredFieldValidator>                
                <br />
                Tipo Fluxo:
                <asp:DropDownList ID="ddlTipoFluxo" runat="server" AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:Button ID="btnAdicionarSubFluxo" runat="server" CausesValidation="True" 
                    ValidationGroup="SubFluxo" Text="Adicionar Sub-fluxo" OnClick="btnAdicionarSubFluxo_Click" />
                <asp:TextBox ID="txtSubFluxo" runat="server" Height="180px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                <br />
                <span id="txtSubFluxoD">0</span> caracteres digitados. 
                Restam <span id="txtSubFluxoR">2000.</span>
                <asp:RequiredFieldValidator ID="rfvSubFluxo" runat="server" 
                                ErrorMessage="O campo nome do sub-fluxo deve ser preenchido" 
                                ControlToValidate="txtSubFluxo" ValidationGroup="SubFluxo">*</asp:RequiredFieldValidator>
                <asp:GridView ID="grvSubFluxos"  runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="grvSubFluxos_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/imagens/excluir.gif" ButtonType="Image" DeleteText="Excluir da listagem" />
                    <asp:BoundField DataField="NomeFluxo" HeaderText="Nome do Fluxo" SortExpression="NomeFluxo" />
                    <asp:BoundField DataField="Detalhamento" HeaderText="Detalhamento" SortExpression="Detalhamento" />
                </Columns>
                </asp:GridView>
                
            </p>
            </contenttemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarSubFluxo" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlConteudo4" runat="server" CssClass="bloconaoselecionado">
          <asp:UpdatePanel ID="UpdatePanelCasosUsoInclude" runat="server">
          <contenttemplate>
                <table>
                    <tr>
                        <td>Casos de uso a incluir:</td>
                        <td></td>
                        <td>Casos de uso incluídos:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstCasosDeUsoIncluir" runat="server" SelectionMode="Single" Height="79px" Width="280px"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="btnColocarInclude" runat="server" Text=">  " Height="17px" OnClick="btnColocarInclude_Click" /><br />
                            <br />
                            <br />
                            <asp:Button ID="btnTirarInclude" runat="server" Text="<  " Height="17px" OnClick="btnTirarInclude_Click"  />
                        </td>
                        <td>
                            <asp:ListBox ID="lstCasoDeUsoIncluidos" runat="server" SelectionMode="Single" Height="79px" Width="280px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
        </contenttemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btnColocarInclude" EventName="Click" />
                  <asp:AsyncPostBackTrigger ControlID="btnTirarInclude" EventName="Click" />
              </Triggers>
        </asp:UpdatePanel>
            <br />
              <asp:UpdatePanel ID="UpdatePanelCasosUsoExtends" runat="server">
              <contenttemplate>
                <table>
                    <tr>
                        <td>Casos de uso a extender:</td>
                        <td></td>
                        <td>Casos de uso extendidos:</td>
                    </tr>
                    <tr>
                        <td>
                             <asp:ListBox ID="lstCasosDeUsoExtender" runat="server" SelectionMode="Single" Height="79px" Width="280px"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="btnColocarExtensao" runat="server" Text=">  " Height="17px" OnClick="btnColocarExtensao_Click" /><br />
                            <br />
                            <br />
                            <asp:Button ID="btnTirarExtensao" runat="server" Text="<  " Height="17px" OnClick="btnTirarExtensao_Click"  />
                        </td>
                        <td>
                            <asp:ListBox ID="lstCasosDeUsoExtendidos" runat="server" SelectionMode="Single" Height="79px" Width="280px"></asp:ListBox>
                        </td>
                    </tr>
                </table> 
            </contenttemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btnColocarExtensao" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="btnTirarExtensao" EventName="Click" />
                  </Triggers>
            </asp:UpdatePanel>
            
        </asp:Panel>
        <asp:Panel ID="pnlConteudo5" runat="server" CssClass="bloconaoselecionado" Visible="false">
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
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CausesValidation="True" 
                    ValidationGroup="Geral" OnClick="btnSalvar_Click" />
        <asp:Button ID="btnAtualizar" runat="server" Text="Salvar" CausesValidation="true" Visible="false" 
                    ValidationGroup="Atualizacao" OnClick="btnAtualizar_Click" />
        <asp:Label ID="lblMensagem" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>