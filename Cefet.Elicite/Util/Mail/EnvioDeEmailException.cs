using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Util.Mail
{
    /// <summary>
    /// Classe de exceção de envio de e-mail.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class EnvioDeEmailException : ApplicationException
    {

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public EnvioDeEmailException()
            : base("Não foi possível enviar o e-mail. ")
        {

        }
        /// <summary>
        /// Mensagem de erro padrão com exceção interna.
        /// </summary>
        /// <param name="inner">Objeto que gerou a exceção original</param>
        public EnvioDeEmailException(Exception inner)
            : base("Não foi possível enviar o e-mail. ", inner)
        {

        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descrição do erro</param>
        public EnvioDeEmailException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descrição do erro</param>
        /// <param name="inner">Objeto que gerou a exceção original</param>
        public EnvioDeEmailException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

