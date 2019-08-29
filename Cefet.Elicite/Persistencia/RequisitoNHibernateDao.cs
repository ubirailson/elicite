using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;
using NHibernate;


namespace Cefet.Elicite.Persistencia
{
	public class RequisitoNHibernateDao : NHibernateDao, IRepositoryRequisito
	{
		/// <summary>
		///  Busca todos os objetos do domínio especificado
		///         ///
		/// </summary>
		public System.Collections.ICollection GetAllFuctionalsByProjeto(Projeto projeto)
		{
            ICollection items = null;

            String hql = "from Requisito obj where obj.Projeto.Id=:p and obj.Atributo.Id=1 order by obj.Codigo asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);

                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto", ex);
            }
		}
		/// <summary>
		///  Busca todos os objetos do domínio especificado
		///         ///
		/// </summary>
		public System.Collections.ICollection GetAllNonFuctionalsByProjeto(Projeto projeto)
		{
            ICollection items = null;

            String hql = "from Requisito obj where obj.Projeto.Id=:p and obj.Atributo.Id=2 order by obj.Codigo asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);

                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto", ex);
            }
		}
		/// <summary>
		///  Busca todos os objetos do domínio especificado
		///         ///
		/// </summary>
		public System.Collections.ICollection GetAllByProjeto(IEntity obj, Projeto projeto)
		{
            return null;
		}
        public int GetMaxCodigo(int codigoProjeto, int codigoTipoRequisito)
        {
            int resultado = 0;
            String hql = "select max(u.Codigo)from Requisito u where u.Projeto.Id=:p and u.Atributo.Id=:p2 ";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", codigoProjeto);
                query.SetInt32("p2", codigoTipoRequisito);
                resultado = (int)query.UniqueResult();
                return resultado;
            }
            catch (NullReferenceException nre)
            {
                throw new DaoException("Erro ao buscar o maior número de revisável no sistema: " + nre.Message, nre);
            }
            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar o maior número de revisável no sistema:", nurex);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar o maior número de revisável no sistema: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar o maior número de revisável no sistema", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar o maior número de revisável no sistema", ex);
            }
        }
        public System.Collections.ICollection GetAllByProjetoOrderByTypeAndCodigo(Projeto projeto)
        {
            ICollection items = null;

            String hql = "from Requisito obj where obj.Projeto.Id=:p order by obj.Atributo.Id asc, obj.Codigo asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);

                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar requisitos no projeto", ex);
            }
        }
	}
}
