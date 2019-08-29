using System;
using System.Collections.Generic;
using System.Text;

namespace Petrobras.Tine.Util.Dao
{
    /// <summary>
    /// 
    /// </summary>
    public enum AscDesc 
    { 
        Asc,Desc
    }
    /// <summary>
    /// Objeto para utilizar nos fitros de ordena��o dos m�todos de busca 
    /// da classe DAO gen�rica. Cont�m o nome do parametro que deve ser o ordenador e um enum dizendo se deve
    /// ser ordena��o ascendente ou descendente.
    /// <remarks>Foi feito pensando na implementa��o NHibernate mas pode ser aproveitado em outras implementa��es
    /// na constru��o de queries.</remarks>
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class ParametroOrder
    {
        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public ParametroOrder() 
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametro"></param>
        /// <param name="ascDesc"></param>
        public ParametroOrder(String parametro, AscDesc ascDesc)
        {
            this.parametro = parametro;
            this.ascDesc = ascDesc;
        }
        String parametro;
        /// <summary>
        /// 
        /// </summary>
        public String Parametro
        {
            get { return parametro; }
            set { parametro = value; }
        }
        AscDesc ascDesc;
        /// <summary>
        /// 
        /// </summary>
        internal AscDesc AscDesc
        {
            get { return ascDesc; }
            set { ascDesc = value; }
        }


    }
}
