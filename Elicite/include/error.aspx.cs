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
using System.Text;

using log4net;

namespace Cefet.Util.Web
{
    public partial class error : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(error));

        protected void Page_Load(object sender, EventArgs e)
        {
            string erro = "";
            try
            {
                if (!IsPostBack)
                {
                    string errorMessage = Session["ErrorMessage"].ToString();
                    string pageErrorOccured = Session["PageErrorOccured"].ToString();
                    string exceptionType = Session["ExceptionType"].ToString();
                    string stackTrace = Session["StackTrace"].ToString();

                    Session["ErrorMessage"] = null;
                    Session["PageErrorOccured"] = null;
                    Session["ExceptionType"] = null;
                    Session["StackTrace"] = null;

                    StringBuilder buffer = new StringBuilder();


                    buffer.Append("<h1>Erro!</h1><br/>");

                    buffer.Append("Ocorreu um erro desconhecido no sistema.<br/>\n");
                    buffer.Append("<br/><br/>\n");

                    buffer.Append("Para tentar novamente, clique <a href='javascript:history.back();'>aqui</a>.\n");
                    buffer.Append("<br/><br/>\n");
                    buffer.Append("Página onde ocorreu o erro: " + pageErrorOccured + ".\n<br/><br/>");

                    erro += buffer.ToString();

                    erro += "Mensagem de erro: " + errorMessage + "\n";
                    erro += "ExceptionType: " + exceptionType + "\n";
                    erro += "Stack Trace: " + stackTrace + "\n";

                    Object exception = Session["Exception"];
                    if (exception != null)
                    {
                        log.Error(erro, (Exception)exception);
                    }
                    else
                    {
                        log.Error(erro);
                    }
                    lblMessage.Text = buffer.ToString();
                }
            }
            catch (Exception ex)
            {

                StringBuilder buffer = new StringBuilder();


                buffer.Append("<h1>Erro!</h1><br/>\n");

                buffer.Append("Ocorreu um erro desconhecido no sistema.<br/>\n");
                buffer.Append("<br/><br/>\n");

                buffer.Append("Para tentar novamente, clique <a href='javascript:history.back();'>aqui</a>.\n");
                buffer.Append("<br/><br/>\n");

                erro += buffer.ToString();
                erro += "Erro: " + ex.Message + "<br/>\n  StackTrace: " + ex.StackTrace + "\n";

                log.Error(erro, ex);

                lblMessage.Text = buffer.ToString();

            }
        }
    }
}