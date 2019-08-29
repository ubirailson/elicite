using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Util.Web
{

	/// <summary>
	/// Classe de exce��o das classes utilit�rias da camada web.
	/// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
	public class WebUtilException : ApplicationException
	{
		/// <summary>
		/// Construtor padr�o.
		/// </summary>
		public WebUtilException()
            : base("Falha na camada web em classe utilit�ria")
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descri��o do erro</param>
		public WebUtilException(string message)
			:base(message)
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descri��o do erro</param>
		/// <param name="inner">Objeto que gerou a exce��o original</param>
        public WebUtilException(string message, Exception inner)
			:base(message, inner)
		{
		}
	}
}
