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
    public partial class InserirRequisito : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(InserirRequisito));
        string erro;
        ServicoRequisito service = new ServicoRequisito();
        Usuario usuarioCorrente;
        Projeto projetoCorrente;

        Requisito requisito;

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

                txtDescricao.Attributes.Add("onkeyup", "maxLength(this,500," +
                    "'txtDescricaoD','txtDescricaoR');");
                txtDescricao.Attributes.Add("onkeypress", "maxLength(this,500," +
                    "'txtDescricaoD','txtDescricaoR');");

                Object valorDeRequest = Request.QueryString["value"];
                if (Validador.Instance.isInteger(valorDeRequest))
                {
                    ModoEdicao(int.Parse(valorDeRequest.ToString()));

                    if (Validador.Instance.isNotNull(Request.QueryString["mensagem"]))
                    {
                        ExibirMensagem(Request.QueryString["mensagem"].ToString()
                            , lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                }
                MontarDropdownList();
            }
        }
        protected void ModoEdicao(int id)
        {
            try
            {
                requisito = (Requisito)service.RepositorioRequisito.Get(typeof(Requisito),
                        id);

                ViewState["requisito"] = requisito;

                txtNome.Text = requisito.Nome;
                txtDescricao.Text = requisito.Descricao;
                ddlTipo.SelectedValue = requisito.Atributo.Id.ToString();
                grvHistorico.DataSource = requisito.Historicos;
                grvHistorico.DataBind();
                btnAtualizar.Visible = true;
                btnSalvar.Visible = false;
                pnlHistorico.Visible = true;
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
        protected void grvHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Historico bean = (Historico)e.Row.DataItem;
                Label labelNoTemplateItem = (Label)e.Row.FindControl("lblAutor");
                labelNoTemplateItem.Text = bean.Autor.Nome;
            }
        }
        protected void MontarDropdownList()
        {
            try
            {
                ddlTipo.DataTextField = "NomeTipo";
                ddlTipo.DataValueField = "Id";
                ddlTipo.DataSource = service.RepositorioTipoRequisito.GetAll(new TipoRequisito());
                ddlTipo.DataBind();

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
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                try
                {
                    requisito = service.CriarNovoRequisito(txtNome.Text.Trim(), txtDescricao.Text.Trim(),
                        int.Parse(ddlTipo.SelectedValue), usuarioCorrente, projetoCorrente);

                    Response.Redirect(Request.CurrentExecutionFilePath + "?value=" + requisito.Id + "&mensagem=Requisito cadastrado com sucesso! ");
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
        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                try
                {
                    requisito = (Requisito)ViewState["requisito"];
                    requisito.Nome = txtNome.Text.Trim();
                    requisito.Descricao = txtDescricao.Text.Trim();
                    TipoRequisito tipo = new TipoRequisito();
                    tipo.Id = int.Parse( ddlTipo.SelectedValue );

                    ViewState["requisito"] = service.Revisar(requisito, txtHistorico.Text.Trim(),
                        usuarioCorrente);

                    txtHistorico.Text = "";
                    grvHistorico.DataSource = requisito.Historicos;
                    grvHistorico.DataBind();

                    ExibirMensagem("Requisito revisado com sucesso! "
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
    }
}