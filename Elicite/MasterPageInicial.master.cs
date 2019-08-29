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
using Cefet.Util.Web;
using Cefet.Elicite.Dominio;

public partial class MasterPageInicial : BaseMasterPage
{
    private static readonly ILog log = LogManager.GetLogger(typeof(MasterPage));
    string erro;
    Usuario usuarioCorrente;
    Projeto projetoCorrente;
    protected void Page_Load(object sender, EventArgs e)
    {
        Object testeUsuario = Session["Usuario"]; 
        
        if (testeUsuario != null)
        {
            usuarioCorrente = (Usuario)testeUsuario;
            if (usuarioCorrente.TipoUsuario.Id == 1)
            {
                lnkProjeto.Visible = true;
                lnkUsuario.Visible = true;
            }
        }
    }
}
