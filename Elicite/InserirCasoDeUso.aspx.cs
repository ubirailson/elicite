using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using Cefet.Util;
using Cefet.Util.Web;
using Cefet.Elicite.Dominio;

namespace Cefet.Elicite.Web
{
    public partial class InserirCasoDeUso : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(InserirCasoDeUso));
        string erro;
        ServicoCasoDeUso serviceCasoUso = new ServicoCasoDeUso();
        Usuario usuarioCorrente;
        Projeto projetoCorrente;
        CasoDeUso casoDeUso;

        ArrayList colecaoAtores;
        ArrayList colecaoCasosUsoInclude;
        ArrayList colecaoCasosUsoExtends;
        ArrayList colecaoSubFluxos;

        string[] campoParaAtor = { "Nome" };
        string[] camposParaCasosUso = { "CodigoCasoUso", "Nome" };

        protected void Page_Load(object sender, EventArgs e)
        {
            Object testeUsuario = Session["Usuario"];
            Object testeProjeto = Session["Projeto"];

            if (testeUsuario == null || testeProjeto == null)
            {
                Response.Redirect("Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ",
                    true);
            }
            usuarioCorrente = (Usuario)testeUsuario;
            projetoCorrente = (Projeto)testeProjeto;

            if (!IsPostBack)
            {
                txtHistorico.Attributes.Add("onkeyup", "maxLength(this,500," +
                    "'txtHistoricoDigitado','txtHistoricoRestante');");
                txtHistorico.Attributes.Add("onkeypress", "maxLength(this,500," +
                    "'txtHistoricoDigitado','txtHistoricoRestante');");
                txtResumo.Attributes.Add("onkeyup", "maxLength(this,2000," +
                    "'txtResumoD','txtResumoR');");
                txtResumo.Attributes.Add("onkeypress", "maxLength(this,2000," +
                    "'txtResumoD','txtResumoR');");
                txtPrecondicoes.Attributes.Add("onkeyup", "maxLength(this,2000," +
                    "'preCondicoesD','preCondicoesR');");
                txtPrecondicoes.Attributes.Add("onkeypress", "maxLength(this,2000," +
                    "'preCondicoesD','preCondicoesR');");
                txtPoscondicoes.Attributes.Add("onkeyup", "maxLength(this,2000," +
                    "'posCondicoesD','posCondicoesR');");
                txtPoscondicoes.Attributes.Add("onkeypress", "maxLength(this,2000," +
                    "'posCondicoesD','posCondicoesR');");

                txtDescricaoFluxoBasico.Attributes.Add("onkeyup", "maxLength(this,2000," +
                    "'txtDescricaoFluxoBasicoD','txtDescricaoFluxoBasicoR');");
                txtDescricaoFluxoBasico.Attributes.Add("onkeypress", "maxLength(this,2000," +
                    "'txtDescricaoFluxoBasicoD','txtDescricaoFluxoBasicoR');");
                txtSubFluxo.Attributes.Add("onkeyup", "maxLength(this,2000," +
                    "'txtSubFluxoD','txtSubFluxoR');");
                txtSubFluxo.Attributes.Add("onkeypress", "maxLength(this,2000," +
                    "'txtSubFluxoD','txtSubFluxoR');");


                colecaoAtores = new ArrayList();
                colecaoCasosUsoExtends = new ArrayList();
                colecaoCasosUsoInclude = new ArrayList();
                colecaoSubFluxos = new ArrayList();
                ViewState["colecaoAtores"] = colecaoAtores;
                ViewState["colecaoCasosUsoExtends"] = colecaoCasosUsoExtends;
                ViewState["colecaoCasosUsoInclude"] = colecaoCasosUsoInclude;
                ViewState["colecaoSubFluxos"] = colecaoSubFluxos;
                Object valorDeRequest = Request.QueryString["value"];

                if (Validador.Instance.isInteger(valorDeRequest))
                {
                    ModoEdicao(int.Parse(valorDeRequest.ToString()));
                    ConfigurarAbasComHistorico();

                    if (Validador.Instance.isNotNull(Request.QueryString["mensagem"]))
                    {
                        ExibirMensagem(Request.QueryString["mensagem"].ToString()
                            , lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                }
                else
                {
                    ConfigurarAbasSemHistorico();

                }
                MontarDropdownListsListBoxes();
            }
        }
        protected void ModoEdicao(int id)
        {
            try
            {
                casoDeUso = (CasoDeUso)serviceCasoUso.RepositorioCasoDeUso.Get(typeof(CasoDeUso),
                        id);

                ViewState["casoDeUso"] = casoDeUso;
                colecaoSubFluxos.AddRange(casoDeUso.SubFluxos);
                grvSubFluxos.DataSource = casoDeUso.SubFluxos;
                grvSubFluxos.DataBind();
                ViewState["colecaoSubFluxos"] = colecaoSubFluxos;

                colecaoAtores.AddRange(casoDeUso.AtoresEnvolvidos);
                lstAtoresSelecionados.Items.AddRange(MontarColecaoDeListItem(colecaoAtores, "Id", campoParaAtor));
                ViewState["colecaoAtores"] = colecaoAtores;

                colecaoCasosUsoExtends.AddRange(casoDeUso.CasosDeUsoExtendidos);
                lstCasosDeUsoExtendidos.Items.AddRange(MontarColecaoDeListItem(colecaoCasosUsoExtends, "Id",
                        camposParaCasosUso));
                ViewState["colecaoCasosUsoExtends"] = colecaoCasosUsoExtends;

                colecaoCasosUsoInclude.AddRange(casoDeUso.CasosDeUsoIncluidos);
                lstCasoDeUsoIncluidos.Items.AddRange(MontarColecaoDeListItem(colecaoCasosUsoInclude, "Id",
                        camposParaCasosUso));
                ViewState["colecaoCasosUsoInclude"] = colecaoCasosUsoInclude;


                txtNome.Text = casoDeUso.Nome;
                txtResumo.Text = casoDeUso.Resumo;
                txtPrecondicoes.Text = casoDeUso.PreCondicoes;
                txtPoscondicoes.Text = casoDeUso.PosCondicoes;
                FluxoBasico fluxoBasico = new FluxoBasico();
                foreach (FluxoBasico fluxo in casoDeUso.FluxosBasicos)
                {
                    fluxoBasico = fluxo;
                    break;
                }
                txtNomeFluxoBasico.Text = fluxoBasico.NomeFluxo;
                txtDescricaoFluxoBasico.Text = fluxoBasico.Detalhamento;
                grvHistorico.DataSource = casoDeUso.Historicos;
                grvHistorico.DataBind();

                pnlConteudo5.Visible = true;
                lnk5.Visible = true;
                btnAtualizar.Visible = true;
                btnSalvar.Visible = false;

            }
            catch (NullReferenceException nue)
            {
                erro = nue.Message;
                log.Error(erro, nue);
                Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
            }
            catch (NegocioException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (WebUtilException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (Exception e)
            {
                erro = e.Message;
                log.Error(erro, e);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
        }
        protected void MontarDropdownListsListBoxes()
        {
            try
            {
                ddlTipoFluxo.DataTextField = "NomeTipo";
                ddlTipoFluxo.DataValueField = "Id";
                ddlTipoFluxo.DataSource = serviceCasoUso.RepositorioTipoFluxo.GetAll(new TipoFluxo());
                ddlTipoFluxo.DataBind();

                lstAtoresSelecionar.DataTextField = "Nome";
                lstAtoresSelecionar.DataValueField = "Id";

                lstAtoresSelecionar.DataSource = serviceCasoUso.RepositorioAtor.GetAllByProjetoNotInThis(
                    projetoCorrente, colecaoAtores);
                lstAtoresSelecionar.DataBind();

                ListItem[] itensInclude = MontarColecaoDeListItem(serviceCasoUso.RepositorioCasoDeUso.
                    GetAllByProjetoNotInThis(projetoCorrente, colecaoCasosUsoInclude), "Id", camposParaCasosUso);
                ListItem[] itensExtends = MontarColecaoDeListItem(serviceCasoUso.RepositorioCasoDeUso.
                    GetAllByProjetoNotInThis(projetoCorrente, colecaoCasosUsoExtends), "Id", camposParaCasosUso);
                lstCasosDeUsoIncluir.Items.AddRange(itensInclude);
                lstCasosDeUsoExtender.Items.AddRange(itensExtends);

            }
            catch (NullReferenceException nue)
            {
                erro = nue.Message;
                log.Error(erro, nue);
                Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
            }
            catch (NegocioException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (WebUtilException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (Exception e)
            {
                erro = e.Message;
                log.Error(erro, e);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }

        }
        protected void ConfigurarAbasSemHistorico()
        {
            lnk1.Attributes.Add("onclick", "mudarAba(1, 4);");
            lnk2.Attributes.Add("onclick", "mudarAba(2, 4);");
            lnk3.Attributes.Add("onclick", "mudarAba(3, 4);");
            lnk4.Attributes.Add("onclick", "mudarAba(4, 4);");
        }
        protected void ConfigurarAbasComHistorico()
        {
            lnk1.Attributes.Add("onclick", "mudarAba(1, 5);");
            lnk2.Attributes.Add("onclick", "mudarAba(2, 5);");
            lnk3.Attributes.Add("onclick", "mudarAba(3, 5);");
            lnk4.Attributes.Add("onclick", "mudarAba(4, 5);");
            lnk5.Attributes.Add("onclick", "mudarAba(5, 5);");
        }

        protected void btnPassarAtorParaUc_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstAtoresSelecionar.SelectedValue))
            {
                try
                {
                    colecaoAtores = (ArrayList)ViewState["colecaoAtores"];

                    colecaoAtores.Add(serviceCasoUso.RepositorioAtor.
                        Get(typeof(Ator), int.Parse(lstAtoresSelecionar.SelectedItem.Value)));
                    lstAtoresSelecionados.Items.Clear();
                    lstAtoresSelecionados.Items.AddRange(MontarColecaoDeListItem(colecaoAtores, "Id", campoParaAtor));

                    lstAtoresSelecionar.Items.Clear();
                    lstAtoresSelecionar.Items.AddRange(MontarColecaoDeListItem(
                        serviceCasoUso.RepositorioAtor.GetAllByProjetoNotInThis(projetoCorrente, colecaoAtores)
                        , "Id", campoParaAtor));

                    ViewState["colecaoAtores"] = colecaoAtores;

                }
                catch (NullReferenceException nue)
                {
                    erro = nue.Message;
                    log.Error(erro, nue);
                    Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (WebUtilException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnTirarAtor_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstAtoresSelecionados.SelectedValue))
            {
                try
                {
                    Ator atorRetirado = (Ator)serviceCasoUso.RepositorioAtor.Get(typeof(Ator), int.Parse(lstAtoresSelecionados.SelectedItem.Value));

                    colecaoAtores = (ArrayList)ViewState["colecaoAtores"];
                    colecaoAtores.Remove(atorRetirado);

                    lstAtoresSelecionados.Items.Clear();
                    lstAtoresSelecionados.Items.AddRange(MontarColecaoDeListItem(colecaoAtores, "Id", campoParaAtor));

                    lstAtoresSelecionar.Items.Clear();
                    lstAtoresSelecionar.Items.AddRange(MontarColecaoDeListItem(
                        serviceCasoUso.RepositorioAtor.GetAllByProjetoNotInThis(projetoCorrente, colecaoAtores)
                        , "Id", campoParaAtor));

                    ViewState["colecaoAtores"] = colecaoAtores;

                }
                catch (NullReferenceException nue)
                {
                    erro = nue.Message;
                    log.Error(erro, nue);
                    Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (WebUtilException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnColocarInclude_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstCasosDeUsoIncluir.SelectedValue))
            {
                try
                {
                    colecaoCasosUsoInclude = (ArrayList)ViewState["colecaoCasosUsoInclude"];

                    colecaoCasosUsoInclude.Add(serviceCasoUso.RepositorioCasoDeUso.
                        Get(typeof(CasoDeUso), int.Parse(lstCasosDeUsoIncluir.SelectedItem.Value)));
                    lstCasoDeUsoIncluidos.Items.Clear();
                    lstCasoDeUsoIncluidos.Items.AddRange(MontarColecaoDeListItem(colecaoCasosUsoInclude, "Id",
                        camposParaCasosUso));

                    lstCasosDeUsoIncluir.Items.Clear();
                    lstCasosDeUsoIncluir.Items.AddRange(MontarColecaoDeListItem(
                        serviceCasoUso.RepositorioCasoDeUso.GetAllByProjetoNotInThis(projetoCorrente, colecaoCasosUsoInclude)
                        , "Id", camposParaCasosUso));

                    ViewState["colecaoCasosUsoInclude"] = colecaoCasosUsoInclude;

                }
                catch (NullReferenceException nue)
                {
                    erro = nue.Message;
                    log.Error(erro, nue);
                    Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (WebUtilException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnTirarInclude_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstCasoDeUsoIncluidos.SelectedValue))
            {
                try
                {
                    CasoDeUso casoUsoRetirado = (CasoDeUso)serviceCasoUso.RepositorioCasoDeUso.Get(typeof(CasoDeUso),
                        int.Parse(lstCasoDeUsoIncluidos.SelectedItem.Value));

                    colecaoCasosUsoInclude = (ArrayList)ViewState["colecaoCasosUsoInclude"];
                    colecaoCasosUsoInclude.Remove(casoUsoRetirado);

                    lstCasoDeUsoIncluidos.Items.Clear();
                    lstCasoDeUsoIncluidos.Items.AddRange(MontarColecaoDeListItem(colecaoCasosUsoInclude,
                        "Id", camposParaCasosUso));

                    lstCasosDeUsoIncluir.Items.Clear();
                    lstCasosDeUsoIncluir.Items.AddRange(MontarColecaoDeListItem(
                        serviceCasoUso.RepositorioCasoDeUso.GetAllByProjetoNotInThis(projetoCorrente,
                        colecaoCasosUsoInclude), "Id", camposParaCasosUso));

                    ViewState["colecaoCasosUsoInclude"] = colecaoCasosUsoInclude;

                }
                catch (NullReferenceException nue)
                {
                    erro = nue.Message;
                    log.Error(erro, nue);
                    Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (WebUtilException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnColocarExtensao_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstCasosDeUsoExtender.SelectedValue))
            {
                try
                {
                    colecaoCasosUsoExtends = (ArrayList)ViewState["colecaoCasosUsoExtends"];

                    colecaoCasosUsoExtends.Add(serviceCasoUso.RepositorioCasoDeUso.
                        Get(typeof(CasoDeUso), int.Parse(lstCasosDeUsoExtender.SelectedItem.Value)));
                    lstCasosDeUsoExtendidos.Items.Clear();
                    lstCasosDeUsoExtendidos.Items.AddRange(MontarColecaoDeListItem(colecaoCasosUsoExtends, "Id",
                        camposParaCasosUso));

                    lstCasosDeUsoExtender.Items.Clear();
                    lstCasosDeUsoExtender.Items.AddRange(MontarColecaoDeListItem(
                        serviceCasoUso.RepositorioCasoDeUso.GetAllByProjetoNotInThis(projetoCorrente, colecaoCasosUsoExtends)
                        , "Id", camposParaCasosUso));

                    ViewState["colecaoCasosUsoExtends"] = colecaoCasosUsoExtends;

                }
                catch (NullReferenceException nue)
                {
                    erro = nue.Message;
                    log.Error(erro, nue);
                    Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (WebUtilException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnTirarExtensao_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstCasosDeUsoExtendidos.SelectedValue))
            {
                try
                {
                    CasoDeUso casoUsoRetirado = (CasoDeUso)serviceCasoUso.RepositorioCasoDeUso.Get(typeof(CasoDeUso),
                        int.Parse(lstCasosDeUsoExtendidos.SelectedItem.Value));

                    colecaoCasosUsoExtends = (ArrayList)ViewState["colecaoCasosUsoExtends"];
                    colecaoCasosUsoExtends.Remove(casoUsoRetirado);

                    lstCasosDeUsoExtendidos.Items.Clear();
                    lstCasosDeUsoExtendidos.Items.AddRange(MontarColecaoDeListItem(colecaoCasosUsoExtends,
                        "Id", camposParaCasosUso));

                    lstCasosDeUsoExtender.Items.Clear();
                    lstCasosDeUsoExtender.Items.AddRange(MontarColecaoDeListItem(
                        serviceCasoUso.RepositorioCasoDeUso.GetAllByProjetoNotInThis(projetoCorrente,
                        colecaoCasosUsoExtends), "Id", camposParaCasosUso));

                    ViewState["colecaoCasosUsoExtends"] = colecaoCasosUsoExtends;

                }
                catch (NullReferenceException nue)
                {
                    erro = nue.Message;
                    log.Error(erro, nue);
                    Response.Redirect("~/Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (WebUtilException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                try
                {
                    //Realimenta os objetos trazendo-os do viewstate
                    colecaoAtores = (ArrayList)ViewState["colecaoAtores"];
                    colecaoCasosUsoExtends = (ArrayList)ViewState["colecaoCasosUsoExtends"];
                    colecaoCasosUsoInclude = (ArrayList)ViewState["colecaoCasosUsoInclude"];
                    colecaoSubFluxos = (ArrayList)ViewState["colecaoSubFluxos"];

                    casoDeUso = serviceCasoUso.CriarNovoCasoDeUso(txtNome.Text.Trim(), txtResumo.Text.Trim(),
                        txtPrecondicoes.Text.Trim(), txtPoscondicoes.Text.Trim(), projetoCorrente,
                        usuarioCorrente, new FluxoBasico(txtNomeFluxoBasico.Text.Trim(),
                        txtDescricaoFluxoBasico.Text.Trim()), colecaoSubFluxos, colecaoAtores, colecaoCasosUsoInclude,
                        colecaoCasosUsoExtends);

                    Response.Redirect(Request.CurrentExecutionFilePath+"?value="+casoDeUso.Id+"&mensagem=Caso de uso cadastrado com sucesso! ");
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = "Erro desconhecido. ";
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnAdicionarSubFluxo_Click(object sender, EventArgs e)
        {
            colecaoSubFluxos = (ArrayList)ViewState["colecaoSubFluxos"];

            SubFluxo fluxo = new SubFluxo();
            fluxo.NomeFluxo = txtNomeSubFluxo.Text.Trim();
            fluxo.Detalhamento = txtSubFluxo.Text.Trim();
            fluxo.TipoFluxo.Id = int.Parse(ddlTipoFluxo.SelectedValue);
            colecaoSubFluxos.Add(fluxo);
            grvSubFluxos.DataSource = colecaoSubFluxos;
            grvSubFluxos.DataBind();
            txtNomeSubFluxo.Text = "";
            txtSubFluxo.Text = "";
            ViewState["colecaoSubFluxos"] = colecaoSubFluxos;
        }
        protected void grvSubFluxos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            colecaoSubFluxos = (ArrayList)ViewState["colecaoSubFluxos"];
            int contador = -1;
            foreach (SubFluxo fluxo in colecaoSubFluxos)
            {
                contador++;
                if (fluxo.Id == int.Parse(grvSubFluxos.DataKeys[e.RowIndex].Value.ToString()))
                {
                    colecaoSubFluxos.RemoveAt(contador);
                    break;
                }
            }
            grvSubFluxos.DataSource = colecaoSubFluxos;
            grvSubFluxos.DataBind();
            ViewState["colecaoSubFluxos"] = colecaoSubFluxos;

        }
        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                try
                {
                    //Realimenta os objetos trazendo-os do viewstate
                    colecaoAtores = (ArrayList)ViewState["colecaoAtores"];
                    colecaoCasosUsoExtends = (ArrayList)ViewState["colecaoCasosUsoExtends"];
                    colecaoCasosUsoInclude = (ArrayList)ViewState["colecaoCasosUsoInclude"];
                    colecaoSubFluxos = (ArrayList)ViewState["colecaoSubFluxos"];
                    casoDeUso = (CasoDeUso)ViewState["casoDeUso"];

                    casoDeUso.CasosDeUsoExtendidos.AddAll( colecaoCasosUsoExtends);
                    casoDeUso.CasosDeUsoIncluidos.AddAll( colecaoCasosUsoInclude);
                    
                    casoDeUso.SubFluxos.AddAll( colecaoSubFluxos);
                    casoDeUso.AtoresEnvolvidos.AddAll( colecaoAtores);

                    casoDeUso.Nome = txtNome.Text.Trim();
                    casoDeUso.Resumo =  txtResumo.Text.Trim();
                    casoDeUso.PreCondicoes = txtPrecondicoes.Text.Trim();
                    casoDeUso.PosCondicoes = txtPoscondicoes.Text.Trim();
                    
                    FluxoBasico fluxoBasico = new FluxoBasico();
                    foreach (FluxoBasico fluxo in casoDeUso.FluxosBasicos)
                    {
                        fluxoBasico = fluxo;
                        break;
                    }
                    fluxoBasico.NomeFluxo = txtNomeFluxoBasico.Text.Trim();
                    fluxoBasico.Detalhamento = txtDescricaoFluxoBasico.Text.Trim();

                    foreach (SubFluxo s in colecaoSubFluxos)
                    {
                        s.CasoDeUso = casoDeUso;
                        s.FluxoPai = fluxoBasico;
                    }

                    ViewState["casoDeUso"] = serviceCasoUso.Revisar(casoDeUso, txtHistorico.Text.Trim(), usuarioCorrente);

                    txtHistorico.Text = "";
                    grvHistorico.DataSource = casoDeUso.Historicos;
                    grvHistorico.DataBind();

                    ExibirMensagem("Caso de uso revisado com sucesso! "
                            , lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = "Erro desconhecido. ";
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void grvHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Historico bean = (Historico)e.Row.DataItem;
                Label labelNoTemplateItem = (Label)e.Row.FindControl("lblAutor");
                labelNoTemplateItem.Text = bean.Autor.Nome;
            }
        }
    }
}