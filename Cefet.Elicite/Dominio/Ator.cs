
using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
    public class Ator : IEntity 
	{
        int id = 0;
        private String nome = String.Empty;
        private String descricao = String.Empty;
        private ISet casosDeUso = new HashedSet();
        private Projeto projeto = new Projeto();
                
        public Ator()
        { }

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Nome
        {
            get { return nome; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Valor vazio no nome", value, "null");

                if (value.Length > 40)
                    throw new ArgumentOutOfRangeException("Valor maior que 40 caracteres no nome", value, value.ToString());
                nome = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Descricao
        {
            get { return descricao; }

            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Valor maior que 300 caracteres ou vazio na descrição", value, value.ToString());
                descricao = value;
            }
        }
        public virtual Projeto Projeto
        {
            get { return projeto; }
            set 
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Projeto deve ser preenchido", value, "null");
                projeto = value; 
            }
        }
        public virtual ISet CasosDeUso
        {
            get
            {
                return casosDeUso;
            }
            set
            {
                casosDeUso = value;
            }
        }
        public virtual void AdicionarCasoDeUso(CasoDeUso casoDeUso)
        {
            if (casoDeUso == null)
                throw new ArgumentOutOfRangeException("Caso de uso nulo", casoDeUso, casoDeUso.ToString());
            casosDeUso.Add(casoDeUso);
            
        }
        public virtual bool RemoverCasoDeUso(CasoDeUso casoDeUso)
        {
            return casosDeUso.Remove(casoDeUso);
        }
        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Ator castObj = (Ator)obj;
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
