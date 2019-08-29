using System;
using System.Collections;
using Cefet.Elicite.Persistencia;
using Cefet.Util.Dao;
using System.Configuration;

namespace Cefet.Elicite.Dominio
{
	public class ServicoRequisito 
	{
		private IRepositoryRequisito repositorioRequisito ;
		private IRepositoryTipoRequisito repositorioTipoRequisito ;

        public ServicoRequisito()
        {
            repositorioRequisito = (IRepositoryRequisito)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("Requisito");
            repositorioTipoRequisito = (IRepositoryTipoRequisito)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("TipoRequisito");
        }

		public Requisito CriarNovoRequisito(String nome, String descricao, 
            int idTipoRequisito, Usuario usuario, Projeto projeto)
		{
            try
            {
                TipoRequisito tipo = new TipoRequisito();
                tipo.Id = idTipoRequisito;
                Requisito requisito = new Requisito(nome, descricao, tipo, projeto, usuario);

                repositorioRequisito.Add(requisito);
                return requisito;
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

        public Requisito Revisar(Requisito requisito, String historico, Usuario autor)
        {
            try
            {
                requisito.AdicionarHistorico(historico, autor);
                repositorioRequisito.Set(requisito);
                return requisito;
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
		public IRepositoryTipoRequisito RepositorioTipoRequisito
		{
			get
			{
                return repositorioTipoRequisito;
			}
		}
		/// <summary>
		///  
		///         ///
		/// </summary>
		public IRepositoryRequisito RepositorioRequisito
		{
			get
			{
                return repositorioRequisito;
			}
		}
	}
}
