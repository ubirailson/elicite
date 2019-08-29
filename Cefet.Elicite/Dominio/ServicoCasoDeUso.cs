using System;
using System.Collections;
using Cefet.Elicite.Persistencia;
using System.Configuration;
using Cefet.Util.Dao;

namespace Cefet.Elicite.Dominio
{
	public class ServicoCasoDeUso 
	{
		private IRepositoryCasoDeUso repositorioCasoDeUso ;
		private IRepositoryAtor repositorioAtor ;
		private IRepositoryTipoFluxo repositorioTipoFluxo ;
		private System.Collections.ICollection tiposFluxo ;
		private System.Collections.ICollection casosDeUsoNoProjeto ;
		private System.Collections.ICollection atoresNoProjeto ;
		private CasoDeUso casoDeUso;
		private System.Collections.ICollection casosDeUsoIncluidos ;
		private System.Collections.ICollection casosDeUsoExtendidos ;
		private System.Collections.ICollection atoresNoCasoDeUso ;
		private FluxoBasico fluxoBasico ;
		private System.Collections.ICollection subFluxos ;

        public ServicoCasoDeUso()
        {
            repositorioCasoDeUso = (IRepositoryCasoDeUso)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("CasoDeUso");

            repositorioAtor = (IRepositoryAtor)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("Ator");

            repositorioTipoFluxo = (IRepositoryTipoFluxo)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("TipoFluxo");
        }
		public CasoDeUso CriarNovoCasoDeUso(String nome, String resumo, String preCondicoes, 
            String posCondicoes, Projeto projeto, Usuario usuario, 
            FluxoBasico fluxoBasico, System.Collections.ICollection subFluxos, 
            System.Collections.ICollection atores,ICollection casosDeUsoIncluidos, 
            ICollection casosDeUsoExtendidos)
		{
            try
            {
                CasoDeUso casoUso = new CasoDeUso(nome, resumo, preCondicoes,
                posCondicoes, fluxoBasico, subFluxos, atores, projeto, usuario,
                casosDeUsoIncluidos, casosDeUsoExtendidos);

                repositorioCasoDeUso.Add(casoUso);
                return casoUso;
            }
            catch (NegocioException nex)
            {
                throw nex;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                throw new NegocioException(aor.Message + " Valor inserido: " + aor.ActualValue.ToString());
            }
            catch (DaoException daoex)
            {
                throw new NegocioException("Erro ao adicionar ao repositório. ", daoex);
            }
		}
        public void AdicionarAtor(int id)
        {
        }
        public void RemoverAtor(int id)
        {
        }
        public void AdicionarTodosAtores()
        {
        }
        public void RemoverTodosAtores()
        {
        }
        public void AdicionarCasoDeUsoEmInclude(int id)
        {
        }
        public void RemoverCasoDeUsoDeInclude(int id)
        {
        }
        public void AdicionarTodosCasosDeUsoInclude()
        {
        }
        public void RemoverTodosCasosDeUsoInclude()
        {
        }
        public void AdicionarCasoDeUsoEmExtend(int id)
        {
        }
        public void RemoverCasoDeUsoDeExtend(int id)
        {
        }
        public void AdicionarTodosCasosDeUsoExtend()
        {
        }
        public void RemoverTodosCasosDeUsoExtend()
        {
        }
        public CasoDeUso Revisar(CasoDeUso casoUso, String historico, Usuario autor)
        {
            try
            {
                casoUso.AdicionarHistorico(historico, autor);
                repositorioCasoDeUso.Set(casoUso);
                return casoUso;
            }
            catch (NegocioException nex)
            {
                throw nex;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                throw new NegocioException(aor.Message + " Valor inserido: " + aor.ActualValue.ToString());
            }
            catch (DaoException daoex)
            {
                throw new NegocioException("Erro ao adicionar ao repositório. ", daoex);
            }
        }

		/// <summary>
		///  
		///         ///
		/// </summary>
		public IRepositoryCasoDeUso RepositorioCasoDeUso
		{
			get
			{
                return repositorioCasoDeUso;
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public IRepositoryAtor RepositorioAtor
		{
			get
			{
                return repositorioAtor;
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public IRepositoryTipoFluxo RepositorioTipoFluxo
		{
			get
			{
                return repositorioTipoFluxo;
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection TiposFluxo
		{
			get
			{
                return tiposFluxo;
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection CasosDeUsoNoProjeto
		{
			get
			{
                return casosDeUsoNoProjeto;
			}
			set
			{
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection AtoresNoProjeto
		{
			get
			{
                return atoresNoProjeto;
			}
			set
			{
			}
		}
		
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection CasosDeUsoIncluidos
		{
			get
			{
                return casosDeUsoIncluidos;
			}
			set
			{
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection CasosDeUsoExtendidos
		{
			get
			{
                return casosDeUsoExtendidos;
			}
			set
			{
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection AtoresNoCasoDeUso
		{
			get
			{
                return atoresNoCasoDeUso;
			}
			set
			{
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public System.Collections.ICollection SubFluxos
		{
			get
			{
                return subFluxos;
			}
			set
			{
			}
		}
		
		/// <summary>
		///  
		///         ///
		/// </summary>
		public CasoDeUso CasoDeUso
		{
			get
			{
                return casoDeUso;
			}
			set
			{
			}
		}
	}
}
