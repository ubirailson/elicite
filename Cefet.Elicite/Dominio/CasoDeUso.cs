using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Persistencia;
using System.Configuration;

namespace Cefet.Elicite.Dominio
{
    [Serializable]
	public class CasoDeUso : Revisavel
	{
        private String nome = String.Empty;
        private String resumo = String.Empty;
        private String preCondicoes = String.Empty;
        private String posCondicoes = String.Empty;
        private ISet fluxosBasicos = new HashedSet();
        private Projeto projeto = new Projeto();
        private ISet atoresEnvolvidos = new HashedSet();
        private ISet subFluxos = new HashedSet();

        private ISet casosDeUsoIncluidos = new HashedSet();
        private ISet casosDeUsoExtendidos = new HashedSet();
        private ISet casosDeUsoQueMeIncluem = new HashedSet();
        private ISet casosDeUsoQueMeExtendem = new HashedSet();
        private ISet requisitosRastreados = new HashedSet();
        
        public CasoDeUso()
        {
            
        }

		public CasoDeUso(String nome, String resumo, String preCondicoes, 
            String posCondicoes, FluxoBasico fluxoBasico, System.Collections.ICollection subFluxos,
            System.Collections.ICollection atores, Projeto projeto, Usuario usuario, 
            ICollection casosDeUsoIncluidos, ICollection casosDeUsoExtendidos)
		{
            this.Nome = nome;
            this.Resumo = resumo;
            this.PreCondicoes = preCondicoes;
            this.PosCondicoes = posCondicoes;
            this.FluxosBasicos.Add(fluxoBasico);
            fluxoBasico.CasoDeUso = this;
            this.SubFluxos.AddAll(subFluxos);
            foreach (SubFluxo subFluxo in subFluxos)
            {
                subFluxo.CasoDeUso = this;
                subFluxo.FluxoPai = fluxoBasico;
            }
            if (atores.Count < 1)
                throw new NegocioException("É necessário pelo menos um ator para criar um caso de uso. ");
            this.AtoresEnvolvidos.AddAll(atores);
            this.Projeto = projeto;

            AdicionarHistorico("Criação do documento. ", usuario);
            this.CasosDeUsoIncluidos.AddAll(casosDeUsoIncluidos);
            this.CasosDeUsoExtendidos.AddAll(casosDeUsoExtendidos);

            IRepositoryCasoDeUso repositorioCasoDeUso = (IRepositoryCasoDeUso)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("CasoDeUso");
            this.Codigo = repositorioCasoDeUso.GetMaxCodigo(this.projeto.Id) + 1;
		}

        public virtual String CodigoCasoUso
        {
            get { return "UC-"+this.Codigo; }
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
                    throw new ArgumentOutOfRangeException("Valor nulo não permitido para Nome", value, "null");

                if (value.Length > 30)
                    throw new ArgumentOutOfRangeException("Nome deve ser menor que 30 caracteres", value, value.ToString());
                nome = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Resumo
        {
            get { return resumo; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Valor nulo não permitido para Resumo", value, "null");

                if (value.Length > 2000)
                    throw new ArgumentOutOfRangeException("Resumo deve ser menor que 2000 caracteres", value, value.ToString());
                 resumo= value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string PosCondicoes
        {
            get { return posCondicoes; }

            set
            {
                if (value != null && value.Length > 2000)
                    throw new ArgumentOutOfRangeException("Pós-Condições deve ser menor que 2000 caracteres", value, value.ToString());
                posCondicoes = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string PreCondicoes
        {
            get { return preCondicoes; }

            set
            {
                if (value != null && value.Length > 2000)
                    throw new ArgumentOutOfRangeException("Pré-Condições deve ser menor que 2000 caracteres", value, value.ToString());
                preCondicoes = value;
            }
        }
        /// <summary>
		///  
		///         ///
		/// </summary>
        public virtual ISet FluxosBasicos
		{
			get
			{
                return fluxosBasicos;
			}
			set
			{
                fluxosBasicos = value;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        public virtual Projeto Projeto
        {
            get { return projeto; }
            set 
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Projeto não pode ser nulo", value, value.ToString());
                
                projeto = value; 
            }
        }
		/// <summary>
		///  
		///         ///
		/// </summary>
        public virtual ISet SubFluxos
		{
			get
			{
                return subFluxos;
			}
			set
			{                
                subFluxos = value;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subFluxo"></param>
        public virtual void AdicionarSubFluxo(SubFluxo subFluxo)
		{
            if (subFluxo == null)
                throw new ArgumentOutOfRangeException("Sub-fluxo nulo", subFluxo, subFluxo.ToString());
            subFluxos.Add(subFluxo);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subFluxo"></param>
        /// <returns></returns>
        public virtual bool RemoverSubFluxo(SubFluxo subFluxo)
		{
            return subFluxos.Remove(subFluxo);
		}
        /// <summary>
        /// 
        /// </summary>
        public virtual ISet RequisitosRastreados
        {
            get { return requisitosRastreados; }
            set { requisitosRastreados = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual ISet AtoresEnvolvidos
        {
            get { return atoresEnvolvidos; }
            set 
            {
                if (value == null)
                    throw new NegocioException("Nenhum ator selecionado");
                if(value.Count <1)
                    throw new NegocioException("Nenhum ator selecionado");
                atoresEnvolvidos = value; 
            
            }
        }
        /// <summary>
        ///  
        ///         ///
        /// </summary>
        public virtual ISet CasosDeUsoIncluidos
        {
            get
            {
                return casosDeUsoIncluidos;
            }
            set
            {
                casosDeUsoIncluidos = value;
            }
        }
        /// <summary>
        ///  
        ///         ///
        /// </summary>
        public virtual ISet CasosDeUsoExtendidos
        {
            get
            {
                return casosDeUsoExtendidos;
            }
            set
            {
                casosDeUsoExtendidos = value;
            }
        }
        public virtual ISet CasosDeUsoQueMeIncluem
        {
            get { return casosDeUsoQueMeIncluem; }
            set { casosDeUsoQueMeIncluem = value; }
        }
        public virtual ISet CasosDeUsoQueMeExtendem
        {
            get { return casosDeUsoQueMeExtendem; }
            set { casosDeUsoQueMeExtendem = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="casoDeUso"></param>
        public virtual void AdicionarIncludeCasoDeUso(CasoDeUso casoDeUso)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="casoDeUso"></param>
        public virtual void RemoverIncludeCasoDeUso(CasoDeUso casoDeUso)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="casoDeUso"></param>
        public virtual void AdicionarExtendsCasoDeUso(CasoDeUso casoDeUso)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="casoDeUso"></param>
        public virtual void RemoverExtendsCasoDeUso(CasoDeUso casoDeUso)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requisito"></param>
        public virtual void AdicionarRequisito(Requisito requisito)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requisito"></param>
        public virtual void RemoverRequisito(Requisito requisito)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="resumo"></param>
        /// <param name="preCondicoes"></param>
        /// <param name="posCondicoes"></param>
        /// <param name="usuario"></param>
        /// <param name="atores"></param>
        /// <param name="fluxoBasico"></param>
        /// <param name="fluxos"></param>
        public virtual void Revisar(String nome, String resumo, String preCondicoes, String posCondicoes, Usuario usuario, System.Collections.ICollection atores, FluxoBasico fluxoBasico, ICollection fluxos)
		{
		}

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            CasoDeUso castObj = (CasoDeUso)obj;
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
