using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;
using NHibernate;

namespace Cefet.Elicite.Persistencia
{
	public class AtorNHibernateDao : NHibernateDao, IRepositoryAtor
	{
		
        public System.Collections.ICollection GetAllByProjeto(Projeto projeto)
		{
            ICollection items = null;

            String hql = "from Ator obj where obj.Projeto.Id=:p order by obj.Nome asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", projeto.Id);
                
                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar atores no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar atores no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar atores no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar atores no projeto", ex);
            }
		}
        public ICollection GetAllByProjetoNotInThis(Projeto projeto, ICollection atores)
        {
            ICollection resultado = null;
            String hql = "from Ator u where u.Projeto.Id=:p ";
            foreach (Ator obj in atores)
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
                throw new DaoException("Erro ao buscar atores no projeto: " + nre.Message, nre);
            }
            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar atores no projeto", nurex);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar atores no projeto: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar atores no projeto: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar atores no projeto", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar atores no projeto", ex);
            }
        }
	}
}
