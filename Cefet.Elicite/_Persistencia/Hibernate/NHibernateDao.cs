using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using NHibernate;
using NHibernate.Expression;

using System.Reflection;

namespace Cefet.Elicite.Persistencia.Hibernate
{
    public enum Operacao
    {
        Inserir, Atualizar, Remover
    }
    /// <summary>
    /// Classe gen�rica DAO, para se utilizar com NHibernate, contendo m�todos CRUD
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class NHibernateDao
    {

       Object id;

       public NHibernateDao() 
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
        	    switch (operacao) {
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
                    ". Mensagem do Hibernate: "+ adoex.Message, adoex);
            }
            catch (InstantiationException ie)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(ie.Message, ie);
            }
            catch (PropertyValueException pre)
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException("N�o foi poss�vel acessar propriedade de entidade. "+pre.Message, pre);
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
            catch (Exception e) {
                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(e.Message, e);
            }
        }
        /// <summary>
        /// M�todo utilit�rio utilizado pelas consultas dessa classe para alimentar um <code>ICriteria</code>.
        /// Nele pode-se inserir par�metros de ordena��o ascendente ou descendente, pagin�vel ou n�o,
        /// o n�mero de p�ginas e seu tamanho.
        /// </summary>
        /// <param name="obj">tipo do objeto de dom�nio</param>
        /// <param name="tamanhoPagina">Tamanho da p�gina caso seja paginado. Valor padr�o � 20</param>
        /// <param name="numeroPagina">N�mero da p�gina. Valor padr�o � 1</param>
        /// <param name="listaAscDesc">ArrayList com objetos do tipo ParametroOrder que cont�m
        ///     string com nome da propriedade e propriedade enum Asc ou Desc, dizendo se � 
        ///     ascendente ou descendente</param>
        /// <param name="paginavel">par�metro booleano que diz se deve ser pagin�vel ou n�o</param>
        /// <returns>objeto ICriteria do NHibernate</returns>
        public ICriteria GetCriteria(Type obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc, bool paginavel)
        {
            ICriteria criteria = Session.CreateCriteria(obj);
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        /// <seealso cref="Petrobras.TINE.Util.DAO.IDAO"/>
        public Object Criar(IPersistente obj)
        {
            ChangeOperation(obj, Operacao.Inserir);

            return id;
        }

        public void Remover(IPersistente obj)
        {
            ChangeOperation(obj, Operacao.Remover);
        }

        public void Atualizar(IPersistente obj)
        {
            ChangeOperation(obj, Operacao.Atualizar);
        }
        /// <summary>
        /// M�todo para buscar objeto por chave prim�ria. Passa-se um objeto qualquer com o tipo que se deseja carregar e o objeto que representa chave(pode ser int, string, ou mesmo objeto que representa chave composta como PessoaPk)
        /// </summary>
        /// <param name="obj">Objeto qualquer do tipo que se deseja carregar</param>
        /// <param name="objetoId">chave prim�ria do objeto espec�fico que se deseja carregar</param>
        /// <returns>Objeto espec�fico com chave prim�ria especificada</returns>
        public IPersistente BuscarPorChavePrimaria(IPersistente obj, Object objetoId)
        {
            try
            {
                IPersistente objetoPersistido = (IPersistente)Session.Get(obj.GetType(), objetoId);
                return objetoPersistido;
            }
            catch (InstantiationException ie)
            {
                throw new DaoException("Erro recuperando item " + objetoId + ". Mensagem de erro original: " + ie.Message, ie);
            }
            catch (ADOException adoex)
            {
                throw new DaoException("Erro recuperando item " + objetoId + ". Mensagem de erro original: " + adoex.Message, adoex);
            }
            catch (HibernateException hex)
            {
                throw new DaoException("Erro recuperando item " + objetoId + ". Mensagem de erro original: " + hex.Message, hex);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro recuperando item " + objetoId, ex);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public System.Collections.ICollection BuscarTodos(IPersistente obj)
        {
            ICollection items = null;

            try
            {
                Type tipo = obj.GetType();
                ICriteria criteria = Session.CreateCriteria(tipo);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarTodos(IPersistente obj, ParametroOrder parametro)
        {
            ICollection items = null;

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                ICriteria criteria = GetCriteria(obj.GetType(), 0, 0, listaAscDesc, false);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarTodos(IPersistente obj, ArrayList listaAscDesc)
        {
            ICollection items = null;

            try
            {
                ICriteria criteria = GetCriteria(obj.GetType(), 0, 0, listaAscDesc, false);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarTodosPaginavel(IPersistente obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro)
        {
            ICollection items = null;

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                ICriteria criteria = GetCriteria(obj.GetType(), tamanhoPagina, numeroPagina, listaAscDesc, true);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarTodosPaginavel(IPersistente obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc)
        {
            ICollection items = null;

            try
            {
                ICriteria criteria = GetCriteria(obj.GetType(), tamanhoPagina, numeroPagina, listaAscDesc, true);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarPorExemplo(IPersistente obj)
        {
            ICollection items = null;

            try
            {
                Type tipo = obj.GetType();
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();

                ICriteria criteria = Session.CreateCriteria(tipo).Add(queryExample);
                items = criteria.List();
                return items;
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
        /// <param name="atributosExcluidos"></param>
        /// <returns></returns>
        public System.Collections.ICollection BuscarPorExemplo(IPersistente obj, string[] atributosExcluidos)
        {
            ICollection items = null;

            try
            {
                Type tipo = obj.GetType();
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                
                foreach (string atributo in atributosExcluidos)
                {
                    queryExample.ExcludeProperty(atributo);
                }

                ICriteria criteria = Session.CreateCriteria(tipo).Add(queryExample);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarPorExemplo(IPersistente obj, ParametroOrder parametro)
        {
            ICollection items = null;

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                ICriteria criteria = GetCriteria(obj.GetType(), 0, 0, listaAscDesc, false);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarPorExemplo(IPersistente obj, string[] atributosExcluidos,
            ParametroOrder parametro)
        {
            ICollection items = null;

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                ICriteria criteria = GetCriteria(obj.GetType(), 0, 0, listaAscDesc, false);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                foreach (string atributo in atributosExcluidos)
                {
                    queryExample.ExcludeProperty(atributo);
                }
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarPorExemplo(IPersistente obj, ArrayList listaAscDesc)
        {
            ICollection items = null;

            try
            {
                ICriteria criteria = GetCriteria(obj.GetType(), 0, 0, listaAscDesc, false);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        /// <param name="atributosExcluidos"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        public System.Collections.ICollection BuscarPorExemplo(IPersistente obj, string[] atributosExcluidos, 
            ArrayList listaAscDesc)
        {
            ICollection items = null;

            try
            {
                ICriteria criteria = GetCriteria(obj.GetType(), 0, 0, listaAscDesc, false);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                foreach (string atributo in atributosExcluidos)
                {
                    queryExample.ExcludeProperty(atributo);
                }
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarPorExemploPaginavel(IPersistente obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro)
        {
            ICollection items = null;

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                ICriteria criteria = GetCriteria(obj.GetType(), tamanhoPagina, numeroPagina, listaAscDesc, true);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        /// <param name="atributosExcluidos"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public System.Collections.ICollection BuscarPorExemploPaginavel(IPersistente obj, string[] atributosExcluidos,
            int tamanhoPagina, int numeroPagina, ParametroOrder parametro)
        {
            ICollection items = null;

            try
            {
                ArrayList listaAscDesc = new ArrayList();
                listaAscDesc.Add(parametro);
                ICriteria criteria = GetCriteria(obj.GetType(), tamanhoPagina, numeroPagina, listaAscDesc, true);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                foreach (string atributo in atributosExcluidos)
                {
                    queryExample.ExcludeProperty(atributo);
                }
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        public System.Collections.ICollection BuscarPorExemploPaginavel(IPersistente obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc)
        {
            ICollection items = null;

            try
            {
                ICriteria criteria = GetCriteria(obj.GetType(), tamanhoPagina, numeroPagina, listaAscDesc, true);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        /// <param name="atributosExcluidos"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        public System.Collections.ICollection BuscarPorExemploPaginavel(IPersistente obj, string[] atributosExcluidos, 
            int tamanhoPagina, int numeroPagina, ArrayList listaAscDesc)
        {
            ICollection items = null;

            try
            {
                ICriteria criteria = GetCriteria(obj.GetType(), tamanhoPagina, numeroPagina, listaAscDesc, true);
                Example queryExample = Example.Create(obj).EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase().ExcludeNulls().ExcludeZeroes();
                foreach (string atributo in atributosExcluidos)
                {
                    queryExample.ExcludeProperty(atributo);
                }
                criteria.Add(queryExample);
                items = criteria.List();
                return items;
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
        public int GetObjectCount(IPersistente obj)
        {
            try
            {
                Type objectType = obj.GetType();
                int count = -1;
                count = (int)Session.CreateQuery("select count(*) from " + objectType.ToString()).UniqueResult();
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
        /// <summary>
        /// Retorna o n�mero de registros na cole��o de objetos(tabela de banco de dados) do tipo do objeto passado
        /// por par�metro.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetObjectCountPorExemplo(IPersistente obj)
        {
            try
            {
                Type objectType = obj.GetType();
                int count = -1;
                String filtro = "select count(*) from " + objectType.Name.ToString()+" "+
                    objectType.Name.ToString().ToLower()+" ";
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
                        if (tipoPropriedade.Equals("String") || tipoPropriedade.Equals("string") )
                        {
                            //captura o valor da propriedade ou, caso esteja nulo, captura nulo mas sem lan�ar exce��o.
                            valorPropriedade = propriedade.GetValue(obj, null) as String;

                            if (!String.IsNullOrEmpty(valorPropriedade))
                            {
                                if (!filtro.Contains("where"))
                                {
                                    filtro += "where (upper(" + nomeClasse.ToLower() + "." + nomePropriedade + ") like '%"+
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
                            valorPropriedade = ((Object)propriedade.GetValue(obj, null)).ToString();
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
                            valorPropriedade = ((Object)propriedade.GetValue(obj, null)).ToString();
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
        #endregion
    }
}
