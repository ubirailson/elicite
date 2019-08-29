using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cache;
using System.Web;
using System.Data;
//using System.Configuration;

namespace Cefet.Elicite.Persistencia.Hibernate
{
    /// <summary>
    /// Handles creation and management of sessions and transactions.  It is a singleton because 
    /// building the initial session factory is very expensive. Inspiration for this class came 
    /// from Chapter 8 of Hibernate in Action by Bauer and King.  Although it is a sealed singleton
    /// you can use TypeMock (http://www.typemock.com) for more flexible testing.
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static NHibernateSessionManager Instance
        {
            get
            {
                return Nested.nHibernateSessionManager;
            }
        }

        /// <summary>
        /// Initializes the NHibernate session factory upon instantiation.
        /// </summary>
        private NHibernateSessionManager()
        {
            InitSessionFactory();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly NHibernateSessionManager nHibernateSessionManager = new NHibernateSessionManager();
        }

        #endregion

        private void InitSessionFactory()
        {
            
            Configuration cfg = new Configuration();

            // D� a certeza de que o web.config cont�m uma declara��o para o HBM_ASSEMBLY appSetting
            if (System.Configuration.ConfigurationManager.AppSettings["HBM_ASSEMBLY"] == null ||
                System.Configuration.ConfigurationManager.AppSettings["HBM_ASSEMBLY"] == "")
            {
                throw new System.Configuration.ConfigurationErrorsException("NHibernateManager.InitSessionFactory: \"HBM_ASSEMBLY\" deve ser " +
                    "provido como um appSetting dentro do seu arquivo de configura��o. \"HBM_ASSEMBLY\" informa ao NHibernate o assembly que " +
                    "cont�m os arquivos HBM. Ele assume que os arquivos HBM est�o com suas propriedades configuradas como \"embedded resources\"." +
                    "Um exemplo de configura��o � a declara��o <add key=\"HBM_ASSEMBLY\" value=\"MyProject.Core\" />");
            }

            String assembly = (String)System.Configuration.ConfigurationManager.AppSettings["HBM_ASSEMBLY"];
            cfg.AddAssembly(assembly);
            sessionFactory = cfg.BuildSessionFactory();
        }

        /// <summary>
        /// Allows you to register an interceptor on a new session.  This may not be called if there is already
        /// an open session attached to the HttpContext.  If you have an interceptor to be used, modify
        /// the HttpModule to call this before calling BeginTransaction().
        /// </summary>
        public void RegisterInterceptor(IInterceptor interceptor)
        {
            ISession session = threadSession;

            if (session != null && session.IsOpen)
            {
                throw new CacheException("Voc� n�o pode registrar um interceptador uma vez que uma sess�o j� foi aberta.");
            }

            GetSession(interceptor);
        }

        public ISession GetSession()
        {
            return GetSession(null);
        }

        /// <summary>
        /// Gets a session with or without an interceptor.  This method is not called directly; instead,
        /// it gets invoked from other public methods.
        /// </summary>
        private ISession GetSession(IInterceptor interceptor)
        {
            ISession session = threadSession;

            if (session == null)
            {
                if (interceptor != null)
                {
                    session = sessionFactory.OpenSession(interceptor);
                }
                else
                {
                    session = sessionFactory.OpenSession();
                }

                threadSession = session;
            }

            return session;
        }

        public void CloseSession()
        {
            ISession session = threadSession;
            threadSession = null;

            if (session != null && session.IsOpen)
            {
                session.Close();
            }
        }

        public void BeginTransaction()
        {
            ITransaction transaction = threadTransaction;

            if (transaction == null)
            {
                transaction = GetSession().BeginTransaction();
                threadTransaction = transaction;
            }
        }

        public void CommitTransaction()
        {
            ITransaction transaction = threadTransaction;

            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Commit();
                    threadTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        public void RollbackTransaction()
        {
            ITransaction transaction = threadTransaction;

            try
            {
                threadTransaction = null;

                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Rollback();
                }
            }
            catch (HibernateException ex)
            {
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        private ITransaction threadTransaction
        {
            get
            {
                return (ITransaction)CallContext.GetData("THREAD_TRANSACTION");
            }
            set
            {
                CallContext.SetData("THREAD_TRANSACTION", value);
            }
        }

        private ISession threadSession
        {
            get
            {
                return (ISession)CallContext.GetData("THREAD_SESSION");
            }
            set
            {
                CallContext.SetData("THREAD_SESSION", value);
            }
        }

        private ISessionFactory sessionFactory;
    }
}