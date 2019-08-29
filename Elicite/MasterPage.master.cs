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

public partial class MasterPage : BaseMasterPage
{
    private static readonly ILog log = LogManager.GetLogger(typeof(MasterPage));
    string erro;
    Usuario usuarioCorrente;
    Projeto projetoCorrente;
    string[] camposParaCasosUso = { "CodigoCasoUso", "Nome" };
    string[] camposParaRequisito = { "CodigoRequisito", "Nome" };
    protected void Page_Load(object sender, EventArgs e)
    {
       Object testeUsuario = Session["Usuario"]; 
       Object testeProjeto = Session["Projeto"]; 
       
        if (testeUsuario == null || testeProjeto == null)
        {
            Response.Redirect("../../Logout.aspx?ReturnUrl=" + Request.CurrentExecutionFilePath + "&mensagem=A sessão expirou. Por favor, efetue o login novamente. ",
                true);
        }
        usuarioCorrente = (Usuario)testeUsuario;
        projetoCorrente = (Projeto)testeProjeto;

        if (usuarioCorrente.TipoUsuario.Id == 1)
        {
            lnkProjeto.Visible = true;
            lnkUsuario.Visible = true;
        }

        if (!IsPostBack)
        {
            ArrayList requisitosNaoFuncionais = new ArrayList();
            ArrayList requisitosFuncionais = new ArrayList();

            foreach (Requisito requisito in projetoCorrente.Requisitos)
            {
                if (requisito.Atributo.Id == 1)
                {
                    requisitosFuncionais.Add(requisito);
                }
                if (requisito.Atributo.Id == 2)
                {
                    requisitosNaoFuncionais.Add(requisito);
                }
            }

            foreach (TreeNode node in TreeView1.Nodes)
            {
                TreeNode[] nodesSubFilhos;
                if (node.Value.Equals("CasosDeUso"))
                {
                    nodesSubFilhos = MontarColecaoDeTreeNode(projetoCorrente.CasosDeUso,
                        "Id", camposParaCasosUso, "InserirCasoDeUso.aspx");
                    foreach (TreeNode subNo in nodesSubFilhos)
                    {
                        node.ChildNodes.Add(subNo);
                    }
                }
                if (node.Value.Equals("RequisitosNaoFuncionais"))
                {
                    nodesSubFilhos = MontarColecaoDeTreeNode(requisitosNaoFuncionais,
                        "Id", camposParaRequisito, "InserirRequisito.aspx");
                    foreach (TreeNode subNo in nodesSubFilhos)
                    {
                        node.ChildNodes.Add(subNo);
                    }
                }
                if (node.Value.Equals("RequisitosFuncionais"))
                {
                    nodesSubFilhos = MontarColecaoDeTreeNode(requisitosFuncionais,
                        "Id", camposParaRequisito, "InserirRequisito.aspx");
                    foreach (TreeNode subNo in nodesSubFilhos)
                    {
                        node.ChildNodes.Add(subNo);
                    }
                }
            }
        }
    }
}
