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

public partial class Inicial : BasePage
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Inicial));
    ServicoCadastro service;
    Usuario usuarioCorrente;
    protected void Page_Load(object sender, EventArgs e)
    {
        Object testeUsuario = Session["Usuario"]; 
        
        if (testeUsuario == null)
        {
            Response.Redirect("../../Logout.aspx?ReturnUrl=" + Request.CurrentExecutionFilePath + "&mensagem=A sessão expirou. Por favor, efetue o login novamente. ",
                true);
        }
        usuarioCorrente = (Usuario)testeUsuario;

        service = new ServicoCadastro();
        if (!IsPostBack)
        {
            ICollection projetos = service.RepositorioProjeto.GetAllByUsuario(usuarioCorrente);
            grvProjetos.DataSource = projetos;
            grvProjetos.DataBind();
        }

    }
    protected void grvProjetos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Projeto bean = (Projeto)e.Row.DataItem;
            LinkButton linkButton = (LinkButton)e.Row.FindControl("lnkProjeto");
            linkButton.Text = bean.Nome;
        }
    }
    protected void grvProjetos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)grvProjetos.SelectedValue;
        Projeto projeto = (Projeto)service.Repositorio.Get(typeof(Projeto), id);
        Session["Projeto"] = projeto;
        Server.Transfer("InserirAtor.aspx", true);
    }
}
