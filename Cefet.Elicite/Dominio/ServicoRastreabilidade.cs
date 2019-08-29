//
//
//  Generated by StarUML(tm) C# Add-In
//
//  @ Project : Elicite
//  @ File Name : ServicoRastreabilidade.cs
//  @ Date : 18/11/2007
//  @ Author : 
//
//
using System;
using System.Collections;
using Cefet.Util.Dao;
using Cefet.Elicite.Persistencia;
using System.Configuration;


namespace Cefet.Elicite.Dominio
{
	public class ServicoRastreabilidade 
	{
		private IRepositoryRequisito repositorioRequisito ;
		private IRepositoryCasoDeUso repositorioCasoDeUso ;

        public ServicoRastreabilidade()
        {
            repositorioRequisito = (IRepositoryRequisito)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("Requisito");
            repositorioCasoDeUso = (IRepositoryCasoDeUso)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("CasoDeUso");
        }

		public Requisito AdicionarRastreamento(Requisito requisitoFuncional, Requisito requisitoRastreado)
		{
            try
            {
                requisitoFuncional.RequisitosRastreados.Add(requisitoRastreado);
                //requisitoRastreado.RequisitosRastreiam.Add(requisitoFuncional);
            
                repositorioRequisito.Set(requisitoFuncional);
                return requisitoFuncional;
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
		public Requisito RemoverRastreamento(Requisito requisitoFuncional, Requisito requisitoRastreado)
		{
            try
            {
                requisitoFuncional.RequisitosRastreados.Remove(requisitoRastreado);
                //requisitoRastreado.RequisitosRastreiam.Remove(requisitoFuncional);

                repositorioRequisito.Set(requisitoFuncional);
                return requisitoFuncional;
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
		public CasoDeUso AdicionarRastreamento(CasoDeUso casoDeUso, Requisito requisitoRastreado)
		{
            try
            {
                casoDeUso.RequisitosRastreados.Add(requisitoRastreado);

                repositorioCasoDeUso.Set(casoDeUso);
                return casoDeUso;
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
		public CasoDeUso RemoverRastreamento(CasoDeUso casoDeUso, Requisito requisitoRastreado)
		{
            try
            {
                casoDeUso.RequisitosRastreados.Remove(requisitoRastreado);

                repositorioCasoDeUso.Set(casoDeUso);
                return casoDeUso;
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
        public IRepositoryRequisito RepositorioRequisito
        {
            get
            {
                return repositorioRequisito;
            }
        }
	}
}
