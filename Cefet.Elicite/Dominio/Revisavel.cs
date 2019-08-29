using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class Revisavel :IEntity
	{
        private int id = 0;
		private int codigo = 0;
        private ISet historicos = new HashedSet();
        
        public Revisavel()
        {
        }

        public virtual void AdicionarHistorico(String descricao, Usuario usuario)
		{
            Historico historico = new Historico(DateTime.Now, descricao, usuario);
            this.historicos.Add(historico);
            historico.Revisavel = this;
		}
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int Codigo
        {
            get { return codigo; }

            set
            {
                codigo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual ISet Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }
        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Revisavel castObj = (Revisavel)obj;
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
