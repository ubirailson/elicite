using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Dominio
{

    /// <summary>
    /// Classe de exceção da camada de dados.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class NegocioException : ApplicationException
    {
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public NegocioException()
            : base("Falha na camada de negócio do sistema")
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descrição do erro</param>
        public NegocioException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem com descrição do erro</param>
        /// <param name="inner">Objeto que gerou a exceção original</param>
        public NegocioException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
