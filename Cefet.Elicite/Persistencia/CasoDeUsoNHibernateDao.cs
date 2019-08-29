using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;
using NHibernate;

namespace Cefet.Elicite.Persistencia
{
	public class CasoDeUsoNHibernateDao : NHibernateDao, IRepositoryCasoDeUso
	{
        public System.Collections.ICollection GetAllByProjeto(Projeto projeto)
        {
            ICollection items = null;

            String hql = "from CasoDeUso obj where obj.Projeto.Id=:p order by obj.Nome asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);

                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", ex);
            }
        }
        public ICollection GetAllByProjetoNotInThis(Projeto projeto, ICollection casosDeUso)
        {
            ICollection resultado = null;
            String hql = "from CasoDeUso u where u.Projeto.Id=:p ";
            foreach (CasoDeUso obj in casosDeUso)
            {
                if (obj.Id > 0)
                {
                   hql += "and u.Id <> " + obj.Id + " ";
                }
            }
            hql += " order by u.Nome asc ";
            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);
                resultado = (ICollection)query.List();
                return resultado;
            }
            catch (NullReferenceException nre)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + nre.Message, nre);
            }
            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", nurex);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", ex);
            }
        }
        public int GetMaxCodigo(int codigoProjeto)
        {
            int resultado = 0;
            String hql = "select max(u.Codigo)from CasoDeUso u where u.Projeto.Id=:p ";
                
            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", codigoProjeto);
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
        public System.Collections.ICollection GetAllByProjetoOrderByCodigoCasoUso(Projeto projeto)
        {
            ICollection items = null;

            String hql = "from CasoDeUso obj where obj.Projeto.Id=:p order by obj.Codigo asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);

                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar casos de uso no projeto", ex);
            }
        }
	}
}
