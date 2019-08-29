using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class Fluxo :IEntity
	{
        private int id = 0;
        private String detalhamento = String.Empty;
        private String nomeFluxo = String.Empty;


        /// <summary>
        /// default constructor
        /// </summary>
        public Fluxo()
        {
         
        }

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
		///  
		///         ///
		/// </summary>
        public virtual String Detalhamento
		{
            get { return detalhamento; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Detalhamento precisa ser preenchido", value, "null");

                if (value.Length > 2000)
                    throw new ArgumentOutOfRangeException("Detalhamento não pode ser maior que 2000 caracteres", value, value.ToString());

                detalhamento = value;
            }
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
        public virtual String NomeFluxo
		{
            get { return nomeFluxo; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Nome do fluxo precisa ser preenchido", value, "null");

                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Nome do fluxo não pode ser maior que 50 caracteres", value, value.ToString());
                
                nomeFluxo = value;
            }
		}
		
        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Fluxo castObj = (Fluxo)obj;
            return (castObj != null) &&
                (this.id == castObj.Id);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {

            int hash = 57;
            hash = 27 * hash * id.GetHashCode();
            return hash;
        }
        #endregion
	}
}
