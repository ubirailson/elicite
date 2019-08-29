using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
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
using Cefet.Util.Criptografia;

namespace Cefet.Elicite.Web
{
    public partial class Index : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Index));
        string erro;
        ServicoAutenticacao service;
        protected void Page_Load(object sender, EventArgs e)
        {
            service = new ServicoAutenticacao();
            if (!IsPostBack)
            {
                Object mensagem = Request.QueryString["mensagem"];
                String descricaoMensagem = "";
                if (mensagem != null)
                {
                    descricaoMensagem = mensagem.ToString();
                }
                Object erroDoContext = Context.Items["erro"];
                String descricaoErroDoContext = "";
                if (erroDoContext != null)
                {
                    descricaoErroDoContext += erroDoContext.ToString();
                    descricaoMensagem += erroDoContext.ToString();
                }
                if (!String.IsNullOrEmpty(descricaoMensagem))
                {
                    ExibirMensagem(descricaoMensagem, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], "100", ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                try
                {
                    GeradorDeHash hash = new GeradorDeHash(HashProvider.MD5);
                    Criptografia crypt = new Criptografia(CryptProvider.TripleDES);
                    crypt.Key = Convert.ToString(ConfigurationManager.AppSettings["CHAVE_CRYPTOGRAFIA"]);
                    Usuario usuario = service.Autenticar(txtUsuario.Text,
                        crypt.Encrypt(hash.GetHash(txtSenha.Text)));
                    if (usuario == null)
                    {
                        ExibirMensagem("Login ou senha errada. Ou usuário desativado.", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], "100", ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                    }
                    else
                    {                        
                        Session["Usuario"] = usuario;
                        FormsAuthentication.RedirectFromLoginPage(usuario.TipoUsuario.Id.ToString(), false);
                    }
                }
                catch (NegocioException nex)
                {
                    erro = nex.Message;
                    ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], "100", ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (HttpException ne)
                {
                    erro = ne.Message;
                    log.Error(erro, ne);
                    ExibirMensagem("Problemas ocorreram na autenticação. Tente novamente. ", lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], "100", ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
                catch (Exception ex)
                {
                    erro = "Erro desconhecido. ";
                    log.Error(erro, ex);
                    ExibirMensagem(erro, lblMensagem, ConfigurationManager.AppSettings["ESQUERDA_MENSAGEM"], "100", ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
                }
            }
        }
}
}