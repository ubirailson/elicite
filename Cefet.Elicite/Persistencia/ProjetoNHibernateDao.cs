using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;
using NHibernate;

namespace Cefet.Elicite.Persistencia
{
	public class ProjetoNHibernateDao : NHibernateDao, IRepositoryProjeto
	{
        public ICollection GetAllByUsuario(Usuario usuario)
        {
            ICollection items = null;
            String hql = "select projeto from Usuario as usuario inner join usuario.Projetos as projeto "
                +   " where usuario.Id=:p order by projeto.Nome asc";

            try
            {
                IQuery query = Session.CreateQuery(hql);
                query.SetInt32("p", usuario.Id);

                items = query.List();

                return items;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar projetos do usuário: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar projetos do usuário: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar projetos do usuário", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar projetos do usuário", ex);
            }
        }
    }
}
