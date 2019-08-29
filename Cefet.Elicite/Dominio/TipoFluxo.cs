using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class TipoFluxo:IEntity
	{
        private int id = 0;
        private String nomeTipo = String.Empty;

        public TipoFluxo()
        { 
            
        }
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string NomeTipo
        {
            get { return nomeTipo; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("NomeTipo deve ser preenchido", value, "null");

                if (value.Length > 20)
                    throw new ArgumentOutOfRangeException("Nome do tipo deve ser maior que 20 caracteres", value, value.ToString());
                nomeTipo = value;
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
            TipoFluxo castObj = (TipoFluxo)obj;
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
