using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Dominio
{
    /// <summary>
    /// Interface com m�todos f�brica para instanciar objetos DAO. Esta interface servir� para
    /// instanciar DAO's de forma desacoplada. Esses DAO's implementar�o a interface Repository
    /// do dom�nio sem ser conhecidos pelo dom�nio.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IFactoryRepository
    {
        /// <summary>
        /// M�todo que retorna o Dao gen�rico com m�todos CRUD.
        /// </summary>
        /// <returns>objeto do tipo IDao</returns>
        IRepository GetRepository();
        /// <summary>
        /// M�todo que retorna Dao de tipo espec�fico passado pelo usu�rio atrav�s de enum. 
        /// No arquivo de configura��o deve-se adicionar uma tag "add" com
        /// os atributos key="Dao_NAMESPACE" e value="nome_do_namespace".
        /// </summary>
        /// <param name="daoName">Nome da entidade do Dao espec�fico. Por exemplo: se o Dao gerencia 
        /// acesso a dados de entidade Usuario, passe por par�metro a string "Usuario" 
        /// </param>
        /// <returns>objeto do tipo IRepository</returns>
        IRepository GetRepository(String repositoryName);
    }
}
