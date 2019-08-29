using System;
using System.Collections;
using Iesi.Collections;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class Usuario: IEntity
	{
        private int id = 0;
        private String nome = String.Empty;
        private String email = String.Empty;
        private String senha = String.Empty;
        private TipoUsuario tipoUsuario;
        private ISet projetos = new HashedSet();

        public Usuario()
        {
        }

        public virtual bool AutenticarSenha(String senha)
		{
            bool retorno = false;
            if (senha.Equals(this.senha))
                retorno = true;
            return retorno;
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
        public virtual ISet Projetos
		{
            get
            {
                return projetos;
            }
            set 
            {
                projetos = value;
            }
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projeto"></param>
        public virtual void AdicionarProjeto(Projeto projeto)
        {
            if (projeto == null)
                throw new ArgumentOutOfRangeException("Projeto nulo", projeto, projeto.ToString());
            projetos.Add(projeto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public virtual bool RemoverProjeto(Projeto projeto)
        {
            return projetos.Remove(projeto);
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
                    throw new ArgumentOutOfRangeException("Nome precisa ser preenchido", value, "null");

                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Nome não pode ser maior que 50 caracteres", value, value.ToString());
                nome = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Email
        {
            get { return email; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("E-mail precisa ser preenchido", value, "null");

                if (value.Length > 30)
                    throw new ArgumentOutOfRangeException("E-mail não pode ser maior que 30 caracteres", value, value.ToString());
                email = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Senha
        {
            get { return senha; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Senha precisa ser preenchida", value, "null");
                senha = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual TipoUsuario TipoUsuario
        {
            get { return tipoUsuario; }
            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Tipo de Usuário precisa ser preenchido", value, "null");

                tipoUsuario = value;
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
            Usuario castObj = (Usuario)obj;
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
