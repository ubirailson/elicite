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
using Cefet.Util.Dao;


namespace Cefet.Elicite.Web
{
    public partial class CadastroUsuario : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CadastroUsuario));
        string erro;
        ServicoCadastro service = new ServicoCadastro();
        Usuario usuarioCorrente;
        Projeto projetoCorrente;

        Usuario usuario;

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
                ddlTipo.DataTextField = "NomeTipo";
                ddlTipo.DataValueField = "Id";
                ddlTipo.DataSource = service.Repositorio.GetAll(new TipoUsuario());
                ddlTipo.DataBind();
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                try
                {
                    usuario = new Usuario();
                    usuario.Nome = txtNome.Text.Trim();
                    usuario.Email = txtEmail.Text.Trim();
                    usuario.Senha = txtSenha.Text.Trim();
                    TipoUsuario tipo = new TipoUsuario();
                    tipo.Id = int.Parse(ddlTipo.SelectedValue);
                    usuario.TipoUsuario = tipo;
                    service.CriarUsuario(usuario);
                    LimparTela();
                    if (ObjectDataSource1.SelectParameters.Count > 0)
                    {
                        grvUsuario.DataSource = ObjectDataSource1;
                        grvUsuario.DataBind();
                    }
                    ExibirMensagem("Usuário cadastrado com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (DaoException daoe)
                {
                    erro = daoe.Message;
                    log.Error(erro, daoe);
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
        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                usuario = (Usuario)ViewState["usuario"];
                if (usuario != null)
                {
                    usuario.Nome = txtNome.Text.Trim();
                    usuario.Email = txtEmail.Text.Trim();
                    usuario.TipoUsuario.Id = int.Parse(ddlTipo.SelectedValue);
                    try
                    {
                        service.AtualizarUsuario(usuario,txtSenha.Text);
                        if (ObjectDataSource1.SelectParameters.Count > 0)
                        {
                            grvUsuario.DataSource = ObjectDataSource1;
                            grvUsuario.DataBind();
                        }
                        ExibirMensagem("Usuário atualizado com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                    catch (NegocioException ne)
                    {
                        erro = ne.Message;
                        log.Error(erro, ne);
                        ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                    catch (DaoException daoe)
                    {
                        erro = daoe.Message;
                        log.Error(erro, daoe);
                        ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                    catch (Exception ex)
                    {
                        erro = "Erro desconhecido. Favor abrir chamado através do LIG-TI 881" +
                            " e informar as circunstâncias do ocorrido.";
                        log.Error(erro, ex);
                        ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                }
                else
                {
                    ExibirMensagem("Selecione um registro na listagem.", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            usuario = (Usuario)ViewState["usuario"];
            if (usuario != null)
            {
                try
                {
                    service.Repositorio.Remove(usuario);
                    LimparTela();
                    grvUsuario.DataSource = ObjectDataSource1;
                    grvUsuario.DataBind();

                    ExibirMensagem("Usuário removido com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (NegocioException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (DaoException daoe)
                {
                    erro = daoe.Message;
                    log.Error(erro, daoe);
                    ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = "Erro desconhecido. Favor abrir chamado através do LIG-TI 881" +
                        " e informar as circunstâncias do ocorrido.";
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
            else
            {
                ExibirMensagem("Selecione um registro na listagem.", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparTela();
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                grvUsuario.PageIndex = 0;
                grvUsuario.DataSource = ObjectDataSource1;
                grvUsuario.DataBind();
            }
            catch (NegocioException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (Exception ex)
            {
                erro = "Erro desconhecido. ";
                log.Error(erro, ex);
                ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
        }
        protected void grvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvUsuario.PageIndex = e.NewPageIndex;
                grvUsuario.DataSource = ObjectDataSource1;
                grvUsuario.DataBind();
                grvUsuario.SelectedIndex = -1;
            }
            catch (NegocioException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (Exception ex)
            {
                erro = "Erro desconhecido. ";
                log.Error(erro, ex);
                ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
        }
        protected void grvUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = (int)grvUsuario.SelectedValue;

                usuario = (Usuario)service.Repositorio.Get(typeof(Usuario), id);
                LimparTela();
                txtNome.Text = usuario.Nome;
                txtEmail.Text = usuario.Email;
                ddlTipo.SelectedValue = usuario.TipoUsuario.Id.ToString();
                ViewState["usuario"] = usuario;

                btnSalvar.Visible = false;
                btnAlterar.Visible = true;
                btnExcluir.Visible = true;
                btnCancelar.Visible = true;
            }
            catch (NegocioException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (Exception ex)
            {
                erro = "Erro desconhecido. ";
                log.Error(erro, ex);
                ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
        }
        protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Usuario bean = (Usuario)e.Row.DataItem;
                Label labelNoTemplateItem = (Label)e.Row.FindControl("lblTipo");
                labelNoTemplateItem.Text = bean.TipoUsuario.NomeTipo;
            }
        }

        protected void LimparTela()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            txtConfirmacao.Text = "";
            btnSalvar.Visible = true;
            btnAlterar.Visible = false;
            btnExcluir.Visible = false;
            btnCancelar.Visible = false;
            grvUsuario.SelectedIndex = -1;
        }
}
}