using System;

namespace Cefet.Elicite.Persistencia
{

	/// <summary>
	/// Classe de exceção da camada de dados.
	/// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
	public class DaoException : ApplicationException
	{
		/// <summary>
		/// Construtor padrão.
		/// </summary>
		public DaoException()
			:base("Falha na camada de dados do sistema")
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descrição do erro</param>
		public DaoException(string message)
			:base(message)
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descrição do erro</param>
		/// <param name="inner">Objeto que gerou a exceção original</param>
		public DaoException(string message, Exception inner)
			:base(message, inner)
		{
		}
	}
}
