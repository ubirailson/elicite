using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
    public class Historico : IEntity 
	{
        private int id = 0;
        private DateTime? data = DateTime.Now;
		private String descricao = String.Empty;
        private Usuario autor = new Usuario();
        private Revisavel revisavel = new Revisavel();
        
        public Historico()
        { 
            
        }
        public Historico(DateTime dataCriacao, String descricao, Usuario autor)
        {
            data = dataCriacao;
            this.descricao = descricao;
            this.autor = autor;
        }

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual DateTime Data
        {
            get { return (DateTime)data; }
            set
            {
                data = (DateTime?)value;
            }
        }
        public virtual string Descricao
        {
            get { return descricao; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Descrição deve ser preenchida", value, "null");

                if (value.Length > 500)
                    throw new ArgumentOutOfRangeException("Descrição dever ter no máximo 500 caracteres", value, value.ToString());

                descricao = value;
            }
        }
        public virtual Usuario Autor
        {
            get { return autor; }
            set 
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Autor do histórico deve ser preenchido", value, "null");
                autor = value; 
            }
        }
        public virtual Revisavel Revisavel
        {
            get { return revisavel; }
            set 
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Requisito ou caso de uso deve ser atrelado ao histórico", value, "null");
                revisavel = value; 
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
            Historico castObj = (Historico)obj;
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
