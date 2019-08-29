using System;
using System.Runtime.Serialization;

namespace Cefet.Util.Dao
{

	/// <summary>
	/// Classe de exce��o da camada de dados.
	/// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    [Serializable]
	public class DaoException : ApplicationException
	{
		/// <summary>
		/// Construtor padr�o.
		/// </summary>
		public DaoException()
			:base("Falha na camada de dados do sistema")
		{
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descri��o do erro</param>
		public DaoException(string message)
			:base(message)
		{ 
		}
		/// <summary>
		/// Construtor.
		/// </summary>
		/// <param name="message">Mensagem com descri��o do erro</param>
		/// <param name="inner">Objeto que gerou a exce��o original</param>
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
