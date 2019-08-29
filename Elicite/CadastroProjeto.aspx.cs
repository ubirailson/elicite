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
    public partial class CadastroProjeto : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CadastroProjeto));
        string erro;
        ServicoCadastro service = new ServicoCadastro();
        Usuario usuarioCorrente;
        Projeto projetoCorrente;
        Projeto projeto;
        string[] campoParaUsuario = { "Nome" };
        ArrayList colecaoUsuarios;
        ArrayList colecaoUsuariosRemocao;

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
                colecaoUsuarios = new ArrayList();
                ViewState["colecaoUsuarios"] = colecaoUsuarios;
                colecaoUsuariosRemocao = new ArrayList();
                ViewState["colecaoUsuariosRemocao"] = colecaoUsuariosRemocao;
                lstUsuariosDisponiveis.Items.Clear();
                lstUsuariosDisponiveis.Items.AddRange(MontarColecaoDeListItem(service.Repositorio.GetAll(new Usuario())
                    , "Id", campoParaUsuario));
            }
        }
        protected void btnColocar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstUsuariosDisponiveis.SelectedValue))
            {
                try
                {
                    colecaoUsuarios = (ArrayList)ViewState["colecaoUsuarios"];

                    colecaoUsuarios.Add(service.Repositorio.
                        Get(typeof(Usuario), int.Parse(lstUsuariosDisponiveis.SelectedItem.Value)));
                    lstUsuariosNoProjeto.Items.Clear();
                    lstUsuariosNoProjeto.Items.AddRange(MontarColecaoDeListItem(colecaoUsuarios, "Id", campoParaUsuario));

                    lstUsuariosDisponiveis.Items.Clear();
                    lstUsuariosDisponiveis.Items.AddRange(MontarColecaoDeListItem(
                        service.RepositorioUsuario.GetAllNotInThis(colecaoUsuarios)
                        , "Id", campoParaUsuario));

                    ViewState["colecaoUsuarios"] = colecaoUsuarios;

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
        protected void btnTirar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstUsuariosNoProjeto.SelectedValue))
            {
                try
                {
                    Usuario usuario = (Usuario)service.Repositorio.Get(typeof(Usuario), 
                        int.Parse(lstUsuariosNoProjeto.SelectedItem.Value));

                    colecaoUsuarios = (ArrayList)ViewState["colecaoUsuarios"];
                    colecaoUsuarios.Remove(usuario);

                    colecaoUsuariosRemocao = (ArrayList)ViewState["colecaoUsuariosRemocao"];
                    colecaoUsuariosRemocao.Add(usuario);

                    lstUsuariosNoProjeto.Items.Clear();
                    lstUsuariosNoProjeto.Items.AddRange(MontarColecaoDeListItem(colecaoUsuarios, "Id", campoParaUsuario));

                    lstUsuariosDisponiveis.Items.Clear();
                    lstUsuariosDisponiveis.Items.AddRange(MontarColecaoDeListItem(
                        service.RepositorioUsuario.GetAllNotInThis(colecaoUsuarios)
                        , "Id", campoParaUsuario));

                    ViewState["colecaoUsuarios"] = colecaoUsuarios;
                    ViewState["colecaoUsuariosRemocao"] = colecaoUsuariosRemocao;

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
                    projeto = new Projeto();
                    projeto.Nome = txtNome.Text.Trim();
                    colecaoUsuarios = (ArrayList)ViewState["colecaoUsuarios"];
                    projeto.Usuarios.AddAll(colecaoUsuarios);
                    service.Repositorio.Add(projeto);
                    LimparTela();
                    if (ObjectDataSource1.SelectParameters.Count > 0)
                    {
                        grvProjeto.DataSource = ObjectDataSource1;
                        grvProjeto.DataBind();
                    }
                    ExibirMensagem("Projeto cadastrado com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
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
                projeto = (Projeto)ViewState["projeto"];
                if (projeto != null)
                {
                    projeto.Nome = txtNome.Text;
                    colecaoUsuarios = (ArrayList)ViewState["colecaoUsuarios"];
                    colecaoUsuariosRemocao = (ArrayList)ViewState["colecaoUsuariosRemocao"];
                    projeto.Usuarios.RemoveAll(colecaoUsuariosRemocao);
                    projeto.Usuarios.AddAll(colecaoUsuarios);
                    try
                    {
                        service.Repositorio.Set(projeto);
                        if (ObjectDataSource1.SelectParameters.Count > 0)
                        {
                            grvProjeto.DataSource = ObjectDataSource1;
                            grvProjeto.DataBind();
                        }
                        ExibirMensagem("Projeto atualizado com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
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
            projeto = (Projeto)ViewState["projeto"];
            if (projeto != null)
            {
                try
                {
                    service.Repositorio.Remove(projeto);
                    LimparTela();
                    grvProjeto.DataSource = ObjectDataSource1;
                    grvProjeto.DataBind();

                    ExibirMensagem("Projeto removido com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
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
                grvProjeto.PageIndex = 0;
                grvProjeto.DataSource = ObjectDataSource1;
                grvProjeto.DataBind();
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
        protected void grvProjeto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvProjeto.PageIndex = e.NewPageIndex;
                grvProjeto.DataSource = ObjectDataSource1;
                grvProjeto.DataBind();
                grvProjeto.SelectedIndex = -1;
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
        protected void grvProjeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = (int)grvProjeto.SelectedValue;

                projeto = (Projeto)service.Repositorio.Get(typeof(Projeto), id);
                LimparTela();
                txtNome.Text = projeto.Nome;
                colecaoUsuarios = (ArrayList)ViewState["colecaoUsuarios"];
                colecaoUsuarios.AddRange(projeto.Usuarios);
                ViewState["colecaoUsuarios"] = colecaoUsuarios;
                lstUsuariosNoProjeto.Items.Clear();
                lstUsuariosNoProjeto.Items.AddRange(MontarColecaoDeListItem(projeto.Usuarios, "Id", campoParaUsuario));
                lstUsuariosDisponiveis.Items.Clear();
                lstUsuariosDisponiveis.Items.AddRange(MontarColecaoDeListItem(
                    service.RepositorioUsuario.GetAllNotInThis(projeto.Usuarios)
                    , "Id", campoParaUsuario));
                ViewState["projeto"] = projeto;

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
        protected void LimparTela()
        {
            txtNome.Text = "";

            btnSalvar.Visible = true;
            btnAlterar.Visible = false;
            btnExcluir.Visible = false;
            btnCancelar.Visible = false;
            lstUsuariosNoProjeto.Items.Clear();
            lstUsuariosDisponiveis.Items.Clear();
            lstUsuariosDisponiveis.Items.AddRange(MontarColecaoDeListItem(
                        service.Repositorio.GetAll(new Usuario()), "Id", campoParaUsuario));
            grvProjeto.SelectedIndex = -1;
        }
}
}