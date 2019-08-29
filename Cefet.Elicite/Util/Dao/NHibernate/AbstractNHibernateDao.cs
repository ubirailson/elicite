using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using NHibernate;
using NHibernate.Expression;

using System.Reflection;

namespace Cefet.Elicite.Util.Dao.NHibernate
{
    public enum Operacao
    {
        Inserir, Atualizar, Remover
    }
    /// <summary>
    /// Classe gen�rica DAO, para se utilizar com NHibernate, contendo m�todos CRUD
    /// <author>Ubirailson Jersy Soares de Medeiros </author>   
    /// <author> Igor Saraiva Brasil </author>   
    /// </summary>
    /// <typeparam name="T">tipo da entidade</typeparam>
    /// <typeparam name="IdT">tipo do Id</typeparam>
    public abstract class AbstractNHibernateDao<T, IdT> : IDao<T, IdT>
    {

        Object id;

        public AbstractNHibernateDao()
        {
        }

        /// <summary>
        /// Propriedade do tipo get que exp�e o ISession usado no DAO.
        /// </summary>
        public ISession Session
        {
            get
            {
                return NHibernateSessionManager.Instance.GetSession();
            }
        }
        /// <summary>
        /// M�todo utilit�rio com opera��es de inser��o, atualiza��o e exclus�o utilizado pelas opera��es CRUD.
        /// </summary>
        /// <param name="obj"> Objeto que implementa a interface <code>IPersistente</code> e 
        ///     ser� utilizado na persist�ncia</param>
        /// <param name="operacao">Op��o de Enum <code>Operacao</code>para escolha da opera��o 
        ///     podendo ser: Inserir, Ataulizar, Remover</param>
        private void ChangeOperation(IPersistente obj, Operacao operacao)
        {

            try
            {
                NHibernateSessionManager.Instance.BeginTransaction();
                switch (operacao)
                {
                    case Operacao.Inserir:
                        this.id = Session.Save(obj);
                        break;
                    case Operacao.Atualizar:
                        Session.Update(obj);
                        break;
                    case Operacao.Remover:
                        Session.Delete(obj);
                        break;
                }
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            catch (ADOException adoex)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("Mensagem do banco de dados: " + adoex.InnerException.Message +
                    ". Mensagem do Hibernate: " + adoex.Message, adoex);
            }
            catch (InstantiationException ie)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(ie.Message, ie);
            }
            catch (PropertyValueException pre)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pre.Message, pre);
            }
            catch (PropertyAccessException pae)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pae.Message, pae);
            }
            catch (HibernateException he)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(he.Message, he);
            }
            catch (Exception e)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(e.Message, e);
            }
        }
        /// <summary>
        /// M�todo utilit�rio utilizado pelas consultas dessa classe para alimentar um <code>ICriteria</code>.
        /// Nele pode-se inserir par�metros de ordena��o ascendente ou descendente, pagin�vel ou n�o,
        /// o n�mero de p�ginas e seu tamanho.
        /// </summary>
        /// <param name="tamanhoPagina">Tamanho da p�gina caso seja paginado. Valor padr�o � 20</param>
        /// <param name="numeroPagina">N�mero da p�gina. Valor padr�o � 1</param>
        /// <param name="listaAscDesc">ArrayList com objetos do tipo ParametroOrder que cont�m
        ///     string com nome da propriedade e propriedade enum Asc ou Desc, dizendo se � 
        ///     ascendente ou descendente</param>
        /// <param name="paginavel">par�metro booleano que diz se deve ser pagin�vel ou n�o</param>
        /// <returns>objeto ICriteria do NHibernate</returns>
        public ICriteria GetCriteria(int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc, bool paginavel)
        {
            ICriteria criteria = Session.CreateCriteria(persitentType);
            if (paginavel)
            {
                //tamanho padr�o da p�gina � 20, podendo ser configurado na invoca��o do m�todo
                int pageSize = 20;
                if (tamanhoPagina > 0)
                    pageSize = tamanhoPagina;

                //p�gina padr�o � a primeira
                int pageNumber = 1;
                if (numeroPagina > 0)
                    pageNumber = numeroPagina;

                criteria.SetFirstResult(numeroPagina);
                criteria.SetMaxResults(tamanhoPagina);
                //criteria.SetFirstResult(pageSize * (pageNumber - 1));
                //criteria.SetMaxResults(pageSize);
            }
            //alimenta��o do ICriteria com os objetos da cole��o passada por par�metro.
            foreach (ParametroOrder parametro in listaAscDesc)
            {
                if (parametro.AscDesc == AscDesc.Desc)
                {
                    criteria.AddOrder(Order.Desc(parametro.Parametro));
                }
                if (parametro.AscDesc == AscDesc.Asc)
                {
                    criteria.AddOrder(Order.Asc(parametro.Parametro));
                }
            }
            return criteria;
        }

        #region IDAO Members(implementa��es do IDAO)
        /// <summary>
        /// Para entidade que tem id atribuidos, voce deve chamar expicitamente "Save" para adicionar um novo.
        /// Veja http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// 
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        /// <seealso cref="Petrobras.TINE.Util.DAO.IDAO"/>
        public T Criar(T obj)
        {
            try
            {
                NHibernateSessionManager.Instance.BeginTransaction();
                Session.Save(obj);
                NHibernateSessionManager.Instance.CommitTransaction();
                return obj;
            }
            catch (ADOException adoex)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("Mensagem do banco de dados: " + adoex.InnerException.Message +
                    ". Mensagem do Hibernate: " + adoex.Message, adoex);
            }
            catch (InstantiationException ie)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(ie.Message, ie);
            }
            catch (PropertyValueException pre)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pre.Message, pre);
            }
            catch (PropertyAccessException pae)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pae.Message, pae);
            }
            catch (HibernateException he)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(he.Message, he);
            }
            catch (Exception e)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(e.Message, e);
            }
        }

        public void Remover(T obj)
        {
            try
            {
                NHibernateSessionManager.Instance.BeginTransaction();
                Session.Delete(obj);
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            catch (ADOException adoex)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("Mensagem do banco de dados: " + adoex.InnerException.Message +
                    ". Mensagem do Hibernate: " + adoex.Message, adoex);
            }
            catch (InstantiationException ie)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(ie.Message, ie);
            }
            catch (PropertyValueException pre)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pre.Message, pre);
            }
            catch (PropertyAccessException pae)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pae.Message, pae);
            }
            catch (HibernateException he)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(he.Message, he);
            }
            catch (Exception e)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(e.Message, e);
            }
        }

        /// <summary>
        /// Para entidade com  gera��o automaticas de IDs, como identity, SaveOrUpdate pode 
        /// ser chamado quando estar salvando uma nova entidade.  SaveOrUpdate pode tambe ser chamado  para  _Atualizar(update)_ qualquer 
        /// entidade,  mesmo que o id tenha sido atribuido.
        /// 
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any 
        /// entity, even if its ID is assigned.
        /// </summary>
        /// <param name="obj">T entity</param>
        /// <returns>T entity</returns>

        public T SalvarOuAtualizar(T entity)
        {
            try
            {
                NHibernateSessionManager.Instance.BeginTransaction();
                Session.SaveOrUpdate(entity);
                NHibernateSessionManager.Instance.CommitTransaction();
                return entity;
            }
            catch (ADOException adoex)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("Mensagem do banco de dados: " + adoex.InnerException.Message +
                    ". Mensagem do Hibernate: " + adoex.Message, adoex);
            }
            catch (InstantiationException ie)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(ie.Message, ie);
            }
            catch (PropertyValueException pre)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pre.Message, pre);
            }
            catch (PropertyAccessException pae)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pae.Message, pae);
            }
            catch (HibernateException he)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(he.Message, he);
            }
            catch (Exception e)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(e.Message, e);
            }
        }
        /// <summary>
        /// Para entidade com  gera��o automaticas de IDs, como identity, SaveOrUpdate pode 
        /// ser chamado quando estar salvando uma nova entidade.  SaveOrUpdate pode tambe ser chamado  para  _Atualizar(update)_ qualquer 
        /// entidade,  mesmo que o id tenha sido atribuido.
        /// 
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any 
        /// entity, even if its ID is assigned.
        /// </summary>
        /// <param name="obj">T entity</param>
        /// <returns>T entity</returns>

        public T Atualizar(T entity)
        {
            try
            {
                NHibernateSessionManager.Instance.BeginTransaction();
                Session.Update(entity);
                NHibernateSessionManager.Instance.CommitTransaction();
                return entity;
            }
            catch (ADOException adoex)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("Mensagem do banco de dados: " + adoex.InnerException.Message +
                    ". Mensagem do Hibernate: " + adoex.Message, adoex);
            }
            catch (InstantiationException ie)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(ie.Message, ie);
            }
            catch (PropertyValueException pre)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pre.Message, pre);
            }
            catch (PropertyAccessException pae)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. " + pae.Message, pae);
            }
            catch (HibernateException he)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(he.Message, he);
            }
            catch (Exception e)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(e.Message, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T BuscarPorChavePrimaria(IdT id)
        {
            try
            {
                T objetoPersistido = (T)Session.Get(persitentType, id);
                return (T)objetoPersistido;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro recuperando item " + id + ". Mensagem de erro original: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro recuperando item " + id + ". Mensagem de erro original: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro recuperando item " + id + ". Mensagem de erro original: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro recuperando item " + id, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<T> BuscarTodos()
        {
            try
            {

                ICriteria criteria = Session.CreateCriteria(persitentType);
                return criteria.List<T>() as List<T>;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public List<T> BuscarTodos(ParametroOrder parametro)
        {
            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                return BuscarTodos(listaAscDesc);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        public List<T> BuscarTodos(ArrayList listaAscDesc)
        {
            try
            {
                ICriteria criteria = GetCriteria(0, 0, listaAscDesc, false);
                return criteria.List<T>() as List<T>;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public List<T> BuscarTodosPaginavel(int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro)
        {
            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);

                return BuscarTodosPaginavel(tamanhoPagina, numeroPagina, listaAscDesc);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        public List<T> BuscarTodosPaginavel(int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc)
        {
            try
            {
                ICriteria criteria = GetCriteria(tamanhoPagina, numeroPagina, listaAscDesc, true);
                return criteria.List<T>() as List<T>;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<T> BuscarPorExemplo(T obj)
        {
            try
            {

                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();

                ICriteria criteria = Session.CreateCriteria(persitentType).Add(queryExample);

                return criteria.List<T>() as List<T>;

            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        public List<T> BuscarPorExemplo(T obj, params string[] propertiesToExclude)
        {
            try
            {
                ICriteria criteria = Session.CreateCriteria(persitentType);
                Example example = Example.Create(obj);

                foreach (string propertyToExclude in propertiesToExclude)
                {
                    example.ExcludeProperty(propertyToExclude);
                }

                criteria.Add(example);

                return criteria.List<T>() as List<T>;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public List<T> BuscarPorExemplo(T obj, ParametroOrder parametro)
        {

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);

                return BuscarPorExemploPaginavel(obj, 0, 0, listaAscDesc);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        public List<T> BuscarPorExemplo(T obj, ArrayList listaAscDesc)
        {
            try
            {
                return BuscarPorExemploPaginavel(obj, 0, 0, listaAscDesc);
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public List<T> BuscarPorExemploPaginavel(T obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro)
        {
            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);

                return BuscarPorExemploPaginavel(obj, tamanhoPagina, numeroPagina, listaAscDesc);

            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        public List<T> BuscarPorExemploPaginavel(T obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc)
        {
            try
            {
                ICriteria criteria = GetCriteria(tamanhoPagina, numeroPagina, listaAscDesc, true);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();

                criteria.Add(queryExample);

                return criteria.List<T>() as List<T>;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro ao buscar itens: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar itens: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar itens: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar itens: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Retorna o n�mero de registros na cole��o de objetos(tabela de banco de dados) do tipo do objeto passado
        /// por par�metro.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetObjectCount()
        {
            try
            {
                return (int)Session.CreateCriteria(persitentType).SetProjection(Projections.RowCount()).UniqueResult();
            }
            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar contagem", nurex);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar contagem: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar contagem", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar contagem", ex);
            }
        }
        /// <summary>
        /// Retorna o n�mero de registros na cole��o de objetos(tabela de banco de dados) do tipo do objeto passado
        /// por par�metro.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetObjectCountPorExemplo(T persitent)
        {
            try
            {
                Type objectType = persitent.GetType();
                int count = -1;
                String filtro = "select count(*) from " + objectType.Name.ToString() + " " +
                    objectType.Name.ToString().ToLower() + " ";
                String nomeClasse = objectType.Name.ToString();

                MethodInfo metodoGet = null;
                PropertyInfo[] propriedades = objectType.GetProperties();

                foreach (PropertyInfo propriedade in propriedades)
                {
                    //acessador
                    metodoGet = propriedade.GetGetMethod();
                    //Nome da propriedade
                    String nomePropriedade = propriedade.Name;
                    //Nome do tipo da propriedade
                    String tipoPropriedade = propriedade.PropertyType.Name;

                    String valorPropriedade;

                    if (!nomePropriedade.Equals("Id"))
                    {
                        if (tipoPropriedade.Equals("String") || tipoPropriedade.Equals("string"))
                        {
                            //captura o valor da propriedade ou, caso esteja nulo, captura nulo mas sem lan�ar exce��o.
                            valorPropriedade = propriedade.GetValue(persitent, null) as String;

                            if (!String.IsNullOrEmpty(valorPropriedade))
                            {
                                if (!filtro.Contains("where"))
                                {
                                    filtro += "where (upper(" + nomeClasse.ToLower() + "." + nomePropriedade + ") like '%" +
                                        valorPropriedade.ToUpper() + "%') ";
                                }
                                else
                                {
                                    filtro += "and (upper(" + nomeClasse.ToLower() + "." + nomePropriedade + ") like '%" +
                                        valorPropriedade.ToUpper() + "%') ";
                                }
                            }
                        }
                        if (tipoPropriedade.Equals("Int32") || tipoPropriedade.Equals("int") || tipoPropriedade.Equals("Int64"))
                        {
                            valorPropriedade = ((Object)propriedade.GetValue(persitent, null)).ToString();
                            if (!String.IsNullOrEmpty(valorPropriedade) && !valorPropriedade.Equals("0"))
                            {
                                if (!filtro.Contains("where"))
                                {
                                    filtro += "where (" + nomeClasse.ToLower() + "." + nomePropriedade + " = " +
                                        valorPropriedade + ") ";
                                }
                                else
                                {
                                    filtro += "and (" + nomeClasse.ToLower() + "." + nomePropriedade + " = " +
                                        valorPropriedade + ") ";
                                }
                            }
                        }
                        if (tipoPropriedade.Equals("Double") || tipoPropriedade.Equals("double"))
                        {
                            valorPropriedade = ((Object)propriedade.GetValue(persitent, null)).ToString();
                            if (!String.IsNullOrEmpty(valorPropriedade))
                            {
                                if (!filtro.Contains("where"))
                                {
                                    filtro += "where (" + nomeClasse.ToLower() + "." + nomePropriedade + " = " +
                                        valorPropriedade + ") ";
                                }
                                else
                                {
                                    filtro += "and (" + nomeClasse.ToLower() + "." + nomePropriedade + " = " +
                                        valorPropriedade + ") ";
                                }
                            }
                        }
                    }
                }
                count = (int)Session.CreateQuery(filtro).UniqueResult();
                return count;
            }

            catch (NonUniqueResultException nurex)
            {
                throw new DaoException("Erro ao buscar contagem", nurex);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro ao buscar contagem: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro ao buscar contagem", hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro ao buscar contagem", ex);
            }
        }
        private Type persitentType = typeof(T);
        #endregion
    }
}
