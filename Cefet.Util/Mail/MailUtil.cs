using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;

namespace Cefet.Util.Mail
{
    public class MailUtil
    {
        /// <summary>
        /// método de envio de correio via SMTP
        /// </summary>
        /// <param name="remetente">e-mail de quem envia</param>
        /// <param name="destinatario">e-mail de quem recebe</param>
        /// <param name="assunto">assunto da mensagem</param>
        /// <param name="corpo">corpo da mensagem</param>
        /// <param name="servidor">servidor de e-mail</param>
        public static void Enviar(String remetente, String destinatario, String assuntoMensagem,
            String corpoMensagem,String servidor) 
        {
            try
            {
                MailMessage mensagem = new MailMessage();

                mensagem.From = remetente;
                mensagem.To = destinatario;
                mensagem.Subject = assuntoMensagem;
                mensagem.Body = corpoMensagem;
                SmtpMail.SmtpServer = servidor;
                SmtpMail.Send(mensagem);
            }
            catch (System.Runtime.InteropServices.COMException come)
            {
                throw new EnvioDeEmailException("Ocorreu erro no envio de e-mail. Mensagem técnica da aplicação: "+come.Message, come);
            }
            catch (System.Configuration.ConfigurationErrorsException coe)
            {
                throw new EnvioDeEmailException("Ocorreu erro no envio de e-mail. Mensagem técnica da aplicação: " + coe.Message, coe);
            }
            catch (System.Web.HttpException he)
            {
                throw new EnvioDeEmailException("Ocorreu erro no envio de e-mail. Mensagem técnica da aplicação: " + he.Message, he);
            }
            catch (Exception ex)
            {
                throw new EnvioDeEmailException("Ocorreu erro no envio de e-mail. Mensagem técnica da aplicação: " + ex.Message, ex);
            }
        }
    }
}
