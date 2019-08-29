using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;

using NHibernate;
using NHibernate.Expression;

using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;
using Cefet.Util.Dao.NHibernate;

namespace Cefet.Elicite.Persistencia
{
    public enum Operacao
    {
        Inserir, Atualizar, Remover
    }
    /// <summary>
    /// Classe gen�rica DAO, para se utilizar com NHibernate, contendo m�todos CRUD
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class NHibernateDao:IRepository
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
                try
                {
                    return NHibernateSessionManager.Instance.GetSession();
                }
                catch (Exception e)
                {
                    throw new DaoException("Problema de conex�o", e);
                }
            }
        }
        /// <summary>
        /// M�todo utilit�rio com opera��es de inser��o, atualiza��o e exclus�o utilizado pelas opera��es CRUD.
        /// </summary>
        /// <param name="obj"> Objeto que implementa a interface <code>IEntity</code> e 
        ///     ser� utilizado na persist�ncia</param>
        /// <param name="operacao">Op��o de Enum <code>Operacao</code>para escolha da opera��o 
        ///     podendo ser: Inserir, Ataulizar, Remover</param>
        private void ChangeOperation(IEntity obj, Operacao operacao)
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
                String erro = "";
                if (adoex.InnerException != null)
                {
                     erro = adoex.InnerException.Message.ToLower();
                }
                else
                {
                    erro = adoex.Message.ToLower();
                }
                if (erro.Contains("unique"))
                {
                    erro = "Os dados que voc� tentou inserir j� existem no sistema ou tentou cadastrar " +
                        "algum campo que n�o pode ser duplicado e j� est� sendo usado por outro registro.";
                }
                if (erro.Contains("null"))
                {
                    erro = "Os dados que voc� tentou inserir cont�m um valor de preenchimento obrigat�rio " +
                        "e que tentou inserir como vazio.";
                }
                if (erro.Contains("larger"))
                {
                    erro = "Os dados que voc� tentou inserir cont�m um valor maior que o tamanho permitido.";
                }
                if (erro.Contains("constraint"))
                {
                    erro = "O(s) dado(s) que voc� tentou remover est�(�o) associados a outro(s), e portanto n�o pode(m) " +
                        "ser removido(s) at� que este(s) outro(s) seja(m) removido(s) primeiro.";
                }
                erro = "Erro ao lidar com dados dados. " + erro;

                NHibernateSessionManager.Instance.RollbackTransaction();
                throw new DaoException(erro, adoex);
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

        #region IRepository Members(implementa��es do IFactory)
        
        public Object Add(IEntity obj)
        {
            ChangeOperation(obj, Operacao.Inserir);

            return id;
        }

        public void Remove(IEntity obj)
        {
            ChangeOperation(obj, Operacao.Remover);
        }

        public void Set(IEntity obj)
        {
            ChangeOperation(obj, Operacao.Atualizar);
        }

        //public void IniciarTransacao()
        //{
        //    NHibernateSessionManager.Instance.BeginTransaction();
        //}
        //public void Commit()
        //{
        //    NHibernateSessionManager.Instance.CommitTransaction();
        //}
        //public void Rollback()
        //{
        //    NHibernateSessionManager.Instance.RollbackTransaction();
        //}
        
        public IEntity Get(Type tipo, Object objetoId)
        {
            try
            {
                IEntity objetoPersistido = (IEntity)Session.Get(tipo, objetoId);
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
        
        public System.Collections.ICollection GetAll(IEntity obj)
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
       
        public System.Collections.ICollection GetAll(IEntity obj, ParametroOrder parametro)
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
        
        public System.Collections.ICollection GetAll(IEntity obj, ArrayList listaAscDesc)
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
        
        public System.Collections.ICollection GetAllPaginavel(IEntity obj, int tamanhoPagina,
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
        
        public System.Collections.ICollection GetAllPaginavel(IEntity obj, int tamanhoPagina,
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
        
        public System.Collections.ICollection GetAllByExample(IEntity obj)
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
        
        public System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos)
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
        
        public System.Collections.ICollection GetAllByExample(IEntity obj, ParametroOrder parametro)
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
        
        public System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos,
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
        
        public System.Collections.ICollection GetAllByExample(IEntity obj, ArrayList listaAscDesc)
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
        
        public System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos, 
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
        
        public System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, int tamanhoPagina,
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
        
        public System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, string[] atributosExcluidos,
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
        
        public System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, int tamanhoPagina,
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
   
        public System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, string[] atributosExcluidos, 
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
               
        public int Size(IEntity obj)
        {
            try
            {
                //Type objectType = obj.GetType();
                //int count = -1;
                //count = (int)Session.CreateQuery("select count(*) from " + objectType.ToString()).UniqueResult();
                //return count;

                return (int)Session.CreateCriteria(obj.GetType()).SetProjection(Projections.RowCount()).UniqueResult();
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
        
        public int SizePorExemplo(IEntity obj)
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
