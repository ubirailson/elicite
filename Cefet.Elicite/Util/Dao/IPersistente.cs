using System;
using System.Collections.Generic;
using System.Text;

namespace Petrobras.Tine.Util.Dao
{
    /// <summary>
    /// Interface que deve ser implementada pelos objetos persistentes, em especial na implementação NHibernate.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IPersistente
    {
        int Id
        {
            get;
            set;
        }
    }
}
