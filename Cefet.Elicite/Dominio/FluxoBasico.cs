using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class FluxoBasico : Fluxo
	{
        private CasoDeUso casoDeUso = new CasoDeUso();


        /// <summary>
        /// default constructor
        /// </summary>
        public FluxoBasico():base()
        {
            
        }
        public FluxoBasico(String nomeFluxo,String detalhamento)
        {
            this.NomeFluxo = nomeFluxo;
            this.Detalhamento = detalhamento;
        }
        public virtual CasoDeUso CasoDeUso
		{
			get
			{
                return casoDeUso;
			}
			set
			{
                if (value == null)
                    throw new ArgumentOutOfRangeException("Um caso de uso deve ser associado ao fluxo básico", value, "null");
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
            FluxoBasico castObj = (FluxoBasico)obj;
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
