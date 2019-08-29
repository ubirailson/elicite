using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Dominio
{

    /// <summary>
    /// Classe de exce��o da camada de dados.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class NegocioException : ApplicationException
    {
        /// <summary>
        /// Construtor padr�o.
        /// </summary>
        public NegocioException()
            : base("Falha na camada de neg�cio do sistema")
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descri��o do erro</param>
        public NegocioException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descri��o do erro</param>
        /// <param name="inner">Objeto que gerou a exce��o original</param>
        public NegocioException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
