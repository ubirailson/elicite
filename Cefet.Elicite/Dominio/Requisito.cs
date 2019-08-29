using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Persistencia;
using System.Configuration;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class Requisito : Revisavel
	{
        private String descricao = String.Empty;
        private String nome = String.Empty;
        private Projeto projeto = new Projeto();
        private TipoRequisito atributo = new TipoRequisito();
        //Requisitos da matriz de rastreabilidade
        private ISet requisitosRastreados = new HashedSet();
        private ISet requisitosRastreiam = new HashedSet();
        private ISet casosDeUsoRastreiam = new HashedSet();
        
        public Requisito()
        { 
            
        }
        public Requisito(String nome, String descricao, TipoRequisito tipoRequisito, 
            Projeto projeto, Usuario usuario)
		{
            this.Nome = nome;
            this.Descricao = descricao;
            this.Atributo = tipoRequisito;
            this.Projeto = projeto;
            projeto.Requisitos.Add(this);
            
            AdicionarHistorico("Criação do documento. ", usuario);

            IRepositoryRequisito repositorioRequisito = (IRepositoryRequisito)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("Requisito");
            this.Codigo = repositorioRequisito.GetMaxCodigo(this.projeto.Id, this.Atributo.Id) + 1;
		}

        public virtual void Revisar(String nome, String descricao, TipoRequisito tipoRequisito, 
            String textoRevisao, Usuario usuario)
		{
		}

        public virtual String CodigoRequisito
        {
            get
            {
                String subCodigo = "";
                if (this.Atributo.Id == 1)
                {
                    subCodigo = "F-";
                }
                if (this.Atributo.Id == 2)
                {
                    subCodigo = "NF-";
                }
                return subCodigo + this.Codigo;
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

                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Nome dever ter no máximo 50 caracteres", value, value.ToString());

                nome = value;
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
        public virtual TipoRequisito Atributo
        {
            get { return atributo; }
            set 
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Atributo de tipo do requisito deve ser preenchido", value, "null");
                atributo = value; 
            }
        }
        public virtual ISet RequisitosRastreados
        {
            get { return requisitosRastreados; }
            set { requisitosRastreados = value; }
        }
        public virtual ISet RequisitosRastreiam
        {
            get { return requisitosRastreiam; }
            set { requisitosRastreiam = value; }
        }
        public virtual ISet CasosDeUsoRastreiam
        {
            get { return casosDeUsoRastreiam; }
            set { casosDeUsoRastreiam = value; }
        }
        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Requisito castObj = (Requisito)obj;
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
