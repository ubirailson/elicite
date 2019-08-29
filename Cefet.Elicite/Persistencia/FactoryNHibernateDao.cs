using System;
using System.Configuration;

using Cefet.Util.Dao;
using Cefet.Util.Dao.NHibernate;
using Cefet.Elicite.Dominio;

namespace Cefet.Elicite.Persistencia
{
    /// <summary>
    /// Classe concreta com m�todos f�brica para instanciar objetos Dao da implementa��o NHibernate desta f�brica.
    /// Herda implementa��o abstrata de m�todo f�brica.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class FactoryNHibernateDao : IFactoryRepository
    {
        /// <summary>
        /// construtor privado para impedir que se invoque esta classe se n�o for atrav�s de sua f�brica abstrata.
        /// </summary>
        
        public FactoryNHibernateDao():base()
        {

        }
        #region IFactoryDao Members
        /// <summary>
        /// M�todo que retorna o Dao gen�rico com m�todos CRUD.
        /// </summary>
        /// <returns>objeto do tipo IDao</returns>
        public IRepository GetRepository()
        {
            NHibernateDao dao = new NHibernateDao();

            return dao;

        }
        /// <summary>
        /// M�todo que retorna Dao de tipo espec�fico passado pelo usu�rio atrav�s de enum. 
        /// No arquivo de configura��o deve-se adicionar uma tag "add" com
        /// os atributos key="Dao_NAMESPACE" e value="nome_do_namespace".
        /// </summary>
        /// <param name="daoName">Nome da entidade do Dao espec�fico. Por exemplo: se o Dao gerencia 
        /// acesso a dados de entidade Usuario, passe por par�metro a string "Usuario" 
        /// </param>
        /// <returns>objeto do tipo IDao</returns>
        public IRepository GetRepository(string repositoryName)
        {
            // D� a certeza de que o web.config cont�m uma declara��o para o Dao_NAMESPACE appSetting
            if (ConfigurationManager.AppSettings["DAO_NAMESPACE"] == null ||
                ConfigurationManager.AppSettings["DAO_NAMESPACE"] == "")
            {
                throw new ConfigurationErrorsException("FactoryDao.GetDao: \"DAO_NAMESPACE\" deve ser " +
                    "provido como um appSetting dentro do seu arquivo de configura��o. \"Dao_NAMESPACE\" informa � f�brica o namespace do assembly " +
                    " que cont�m os Daos espec�ficos. Um exemplo de configura��o � a declara��o <add key=\"DAO_NAMESPACE\" value=\"MyProject.Dao\" />");
            }

            string nomeNamespace = System.Configuration.ConfigurationManager.AppSettings["DAO_NAMESPACE"];
            string fullName = nomeNamespace + "." + repositoryName + "NHibernateDao";
            Type tipoDao = Type.GetType(fullName);
            try
            {
                NHibernateDao obj = (NHibernateDao)Activator.CreateInstance(tipoDao);
                if (obj == null)
                {
                    throw new DaoException("N�o foi poss�vel criar objeto do tipo: '" + tipoDao.FullName + "'");
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new DaoException("N�o foi poss�vel criar objeto do tipo: '" + tipoDao.FullName + "'", ex);
            }
        }

        #endregion
    }
}
