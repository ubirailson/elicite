using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class Projeto:IEntity 
	{
        private int id = 0;
        private String nome = String.Empty;
		private DateTime? dataCriacao = null;
        private ISet usuarios = new HashedSet();
        private ISet casosDeUso = new HashedSet();
        private ISet requisitos = new HashedSet();
        private ISet atoresEnvolvidos = new HashedSet();

        public Projeto()
        {
            dataCriacao = DateTime.Now;
        }
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
                    throw new ArgumentOutOfRangeException("Nome deve ser preenchido", value, "null");

                if (value.Length > 30)
                    throw new ArgumentOutOfRangeException("Nome não pode ser maior que 30 caracteres", value, value.ToString());

                nome = value;
            }
        }
        public virtual DateTime DataCriacao
        {
            get { return (DateTime)dataCriacao; }
            set
            {
                dataCriacao = (DateTime?)value;
            }

        }
        public virtual ISet AtoresEnvolvidos
        {
            get { return atoresEnvolvidos; }
            set { atoresEnvolvidos = value; }
        }
        public virtual ISet Usuarios
        {
            get { return usuarios; }
            set { usuarios = value; }
        }
        public virtual ISet Requisitos
        {
            get { return requisitos; }
            set { requisitos = value; }
        }
        public virtual ISet CasosDeUso
        {
            get { return casosDeUso; }
            set { casosDeUso = value; }
        }

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Projeto castObj = (Projeto)obj;
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
