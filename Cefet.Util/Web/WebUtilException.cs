using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Util.Web
{

	/// <summary>
	/// Classe de exceção das classes utilitárias da camada web.
	/// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
	public class WebUtilException : ApplicationException
	{
		/// <summary>
		/// Construtor padrão.
		/// </summary>
		public WebUtilException()
            : base("Falha na camada web em classe utilitária")
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descrição do erro</param>
		public WebUtilException(string message)
			:base(message)
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descrição do erro</param>
		/// <param name="inner">Objeto que gerou a exceção original</param>
        public WebUtilException(string message, Exception inner)
			:base(message, inner)
		{
		}
	}
}
