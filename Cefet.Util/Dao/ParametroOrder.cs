using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Util.Dao
{
    /// <summary>
    /// 
    /// </summary>
    public enum AscDesc 
    { 
        Asc,Desc
    }
    /// <summary>
    /// Objeto para utilizar nos fitros de ordenação dos métodos de busca 
    /// da classe DAO genérica. Contém o nome do parametro que deve ser o ordenador e um enum dizendo se deve
    /// ser ordenação ascendente ou descendente.
    /// <remarks>Foi feito pensando na implementação NHibernate mas pode ser aproveitado em outras implementações
    /// na construção de queries.</remarks>
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class ParametroOrder
    {
        String parametro;
        AscDesc ascDesc;

        /// <summary>
        /// Construtor padrão
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
        
        /// <summary>
        /// 
        /// </summary>
        public String Parametro
        {
            get { return parametro; }
            set { parametro = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public AscDesc AscDesc
        {
            get { return ascDesc; }
            set { ascDesc = value; }
        }


    }
}
