using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Elicite.Util.Dao
{
    /// <summary>
    /// Interface com métodos fábrica para instanciar objetos DAO
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IFactoryDao<D> 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
    //    D GetDao(Type objeto, Type tipoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="daoName"></param>
        /// <returns></returns>
        D GetDao();
    }
}
