using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class SubFluxo : Fluxo
	{
        private TipoFluxo tipoFluxo = new TipoFluxo();
        private Fluxo fluxoPai = new Fluxo();
		private CasoDeUso casoDeUso = new CasoDeUso();
		/// <summary>
		///  
		///         ///
		/// </summary>
        /// 
        public SubFluxo():base()
        { 
            
        }
        public virtual TipoFluxo TipoFluxo
        {
            get { return tipoFluxo; }
            set 
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Um tipo de fluxo deve ser associado ao sub-fluxo", value, "null");
                tipoFluxo = value; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Fluxo FluxoPai
        {
            get
            {
                return fluxoPai;
            }
            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Um fluxo pai deve ser associado ao sub-fluxo", value, "null");
                fluxoPai = value;
            }
        }
		/// <summary>
		///  
		///         ///
		/// </summary>
        public virtual CasoDeUso CasoDeUso
        {
            get
            {
                return casoDeUso;
            }
            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Um caso de uso deve ser associado ao sub-fluxo", value, "null");
                casoDeUso = value;
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
            SubFluxo castObj = (SubFluxo)obj;
            return (castObj != null) &&
                (this.Id == castObj.Id);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {

            int hash = 57;
            hash = 27 * hash * Id.GetHashCode();
            return hash;
        }
        #endregion
	}
}
