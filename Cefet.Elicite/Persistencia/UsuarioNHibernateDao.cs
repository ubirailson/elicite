using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;
using NHibernate;

namespace Cefet.Elicite.Persistencia
{
	public class UsuarioNHibernateDao : NHibernateDao, IRepositoryUsuario
	{
		public Usuario GetByLogin(String email)
		{
            Usuario resultado = null;
            String hql = "from Usuario u where u.Email=:p";
            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetString("p", email);
                resultado = (Usuario)query.UniqueResult();
                return resultado;
            }
            catch (NullReferenceException nre)
            {
                throw new DaoException("Erro ao buscar usuário: " + nre.Message, nre);
            }
            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar usuário. ", nurex);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar usuário: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar usuário: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar usuário. ", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar usuário. ", ex);
            }
		}
        public ICollection GetAllNotInThis(System.Collections.ICollection usuarios)
        {
            ICollection resultado = null;
            String hql = "from Usuario u where 1=1 ";
            foreach (Usuario obj in usuarios)
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
                resultado = (ICollection)query.List();
                return resultado;
            }
            catch (NullReferenceException nre)
            {
                throw new DaoException("Erro ao buscar usuários: " + nre.Message, nre);
            }
            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar usuários. ", nurex);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar usuários: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar usuários: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar usuários. ", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar usuários. ", ex);
            }
        }
	}
}
