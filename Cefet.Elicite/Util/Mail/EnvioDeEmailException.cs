using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Util.Mail
{
    /// <summary>
    /// Classe de exce��o de envio de e-mail.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class EnvioDeEmailException : ApplicationException
    {

        /// <summary>
        /// Construtor padr�o.
        /// </summary>
        public EnvioDeEmailException()
            : base("N�o foi poss�vel enviar o e-mail. ")
        {

        }
        /// <summary>
        /// Mensagem de erro padr�o com exce��o interna.
        /// </summary>
        /// <param name="inner">Objeto que gerou a exce��o original</param>
        public EnvioDeEmailException(Exception inner)
            : base("N�o foi poss�vel enviar o e-mail. ", inner)
        {

        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descri��o do erro</param>
        public EnvioDeEmailException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descri��o do erro</param>
        /// <param name="inner">Objeto que gerou a exce��o original</param>
        public EnvioDeEmailException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

