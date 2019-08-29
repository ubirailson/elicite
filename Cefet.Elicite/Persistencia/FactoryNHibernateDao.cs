using System;
using System.Configuration;

using Cefet.Util.Dao;
using Cefet.Util.Dao.NHibernate;
using Cefet.Elicite.Dominio;

namespace Cefet.Elicite.Persistencia
{
    /// <summary>
    /// Classe concreta com métodos fábrica para instanciar objetos Dao da implementação NHibernate desta fábrica.
    /// Herda implementação abstrata de método fábrica.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class FactoryNHibernateDao : IFactoryRepository
    {
        /// <summary>
        /// construtor privado para impedir que se invoque esta classe se não for através de sua fábrica abstrata.
        /// </summary>
        
        public FactoryNHibernateDao():base()
        {

        }
        #region IFactoryDao Members
        /// <summary>
        /// Método que retorna o Dao genérico com métodos CRUD.
        /// </summary>
        /// <returns>objeto do tipo IDao</returns>
        public IRepository GetRepository()
        {
            NHibernateDao dao = new NHibernateDao();

            return dao;

        }
        /// <summary>
        /// Método que retorna Dao de tipo específico passado pelo usuário através de enum. 
        /// No arquivo de configuração deve-se adicionar uma tag "add" com
        /// os atributos key="Dao_NAMESPACE" e value="nome_do_namespace".
        /// </summary>
        /// <param name="daoName">Nome da entidade do Dao específico. Por exemplo: se o Dao gerencia 
        /// acesso a dados de entidade Usuario, passe por parâmetro a string "Usuario" 
        /// </param>
        /// <returns>objeto do tipo IDao</returns>
        public IRepository GetRepository(string repositoryName)
        {
            // Dá a certeza de que o web.config contém uma declaração para o Dao_NAMESPACE appSetting
            if (ConfigurationManager.AppSettings["DAO_NAMESPACE"] == null ||
                ConfigurationManager.AppSettings["DAO_NAMESPACE"] == "")
            {
                throw new ConfigurationErrorsException("FactoryDao.GetDao: \"DAO_NAMESPACE\" deve ser " +
                    "provido como um appSetting dentro do seu arquivo de configuração. \"Dao_NAMESPACE\" informa à fábrica o namespace do assembly " +
                    " que contém os Daos específicos. Um exemplo de configuração é a declaração <add key=\"DAO_NAMESPACE\" value=\"MyProject.Dao\" />");
            }

            string nomeNamespace = System.Configuration.ConfigurationManager.AppSettings["DAO_NAMESPACE"];
            string fullName = nomeNamespace + "." + repositoryName + "NHibernateDao";
            Type tipoDao = Type.GetType(fullName);
            try
            {
                NHibernateDao obj = (NHibernateDao)Activator.CreateInstance(tipoDao);
                if (obj == null)
                {
                    throw new DaoException("Não foi possível criar objeto do tipo: '" + tipoDao.FullName + "'");
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new DaoException("Não foi possível criar objeto do tipo: '" + tipoDao.FullName + "'", ex);
            }
        }

        #endregion
    }
}
