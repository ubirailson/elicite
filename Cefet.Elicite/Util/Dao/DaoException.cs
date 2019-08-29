using System;
using System.Runtime.Serialization;

namespace Cefet.Elicite.Util.Dao
{

	/// <summary>
	/// Classe de exceção da camada de dados.
	/// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    [Serializable]
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

        public DaoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { 
       
        
        }
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
         
        }
	}
}
