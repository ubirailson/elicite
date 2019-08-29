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

public partial class InserirAtor : BasePage
{
    private static readonly ILog log = LogManager.GetLogger(typeof(InserirAtor));
    string erro;
    ServicoCadastro servico = new ServicoCadastro();
    Usuario usuarioCorrente;
    Projeto projetoCorrente;
    Ator ator;

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
            txtDescricao.Attributes.Add("onkeyup", "maxLength(this,300," +
                    "'txtDescricaoD','txtDescricaoR');");
            txtDescricao.Attributes.Add("onkeypress", "maxLength(this,300," +
                "'txtDescricaoD','txtDescricaoR');");
        }
    }
    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            ator = (Ator)ViewState["ator"];
            if (ator !=null)
            {
                ator.Descricao = txtDescricao.Text;
                ator.Nome = txtNome.Text;
                try
                {
                    servico.Repositorio.Set(ator); 
                    if (ObjectDataSource1.SelectParameters.Count > 0)
                    {
                        grvAtor.DataSource = ObjectDataSource1;
                        grvAtor.DataBind();
                    }
                    ExibirMensagem("Ator atualizado com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
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
        ator = (Ator)ViewState["ator"];
        if (ator != null)
        {
            try
            {
                servico.Repositorio.Remove(ator); 
                LimparTela();
                grvAtor.DataSource = ObjectDataSource1;
                grvAtor.DataBind();

                ExibirMensagem("Ator removido com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
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
            grvAtor.PageIndex = 0;
            grvAtor.DataSource = ObjectDataSource1;
            grvAtor.DataBind();
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
    protected void grvAtor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvAtor.PageIndex = e.NewPageIndex;
            grvAtor.DataSource = ObjectDataSource1;
            grvAtor.DataBind();
            grvAtor.SelectedIndex = -1;
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
    protected void grvAtor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int id = (int)grvAtor.SelectedValue;

            ator = (Ator)servico.Repositorio.Get(typeof(Ator), id);
            LimparTela();
            txtNome.Text = ator.Nome;
            txtDescricao.Text = ator.Descricao;
            ViewState["ator"] = ator;

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
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            try
            {
                ator = new Ator(); 
                ator.Nome = txtNome.Text.Trim();
                ator.Descricao = txtDescricao.Text.Trim();
                ator.Projeto = projetoCorrente;
                servico.Repositorio.Add(ator);
                LimparTela();
                if (ObjectDataSource1.SelectParameters.Count > 0)
                {
                    grvAtor.DataSource = ObjectDataSource1;
                    grvAtor.DataBind();
                }
                ExibirMensagem("Ator cadastrado com sucesso!", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
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
    protected void LimparTela()
    {
        txtDescricao.Text = "";
        txtNome.Text = "";
        btnSalvar.Visible = true;
        btnAlterar.Visible = false;
        btnExcluir.Visible = false;
        btnCancelar.Visible = false;
        grvAtor.SelectedIndex = -1;
    }
}
