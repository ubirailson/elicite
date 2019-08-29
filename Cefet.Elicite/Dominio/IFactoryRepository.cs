using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Dominio
{
    /// <summary>
    /// Interface com métodos fábrica para instanciar objetos DAO. Esta interface servirá para
    /// instanciar DAO's de forma desacoplada. Esses DAO's implementarão a interface Repository
    /// do domínio sem ser conhecidos pelo domínio.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IFactoryRepository
    {
        /// <summary>
        /// Método que retorna o Dao genérico com métodos CRUD.
        /// </summary>
        /// <returns>objeto do tipo IDao</returns>
        IRepository GetRepository();
        /// <summary>
        /// Método que retorna Dao de tipo específico passado pelo usuário através de enum. 
        /// No arquivo de configuração deve-se adicionar uma tag "add" com
        /// os atributos key="Dao_NAMESPACE" e value="nome_do_namespace".
        /// </summary>
        /// <param name="daoName">Nome da entidade do Dao específico. Por exemplo: se o Dao gerencia 
        /// acesso a dados de entidade Usuario, passe por parâmetro a string "Usuario" 
        /// </param>
        /// <returns>objeto do tipo IRepository</returns>
        IRepository GetRepository(String repositoryName);
    }
}
