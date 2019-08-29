using System;
using System.Collections.Generic;
using System.Text;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;

namespace Cefet.Elicite.Persistencia
{
    /// <summary>
    /// F�brica abstrata onde se escolhe a implementa��o da persist�ncia. Chama o m�todo f�brica que instanciar�
    /// a implementa��o desejada. Existe um m�todo f�brica abstrato contendo a implementa��o de cada tipo de 
    /// persist�ncia suportado. Voc� criar uma classe que herde essa classe abstrara para utilizar seus m�todos
    /// dentro do assembly da sua aplica��o. Isso se faz necess�rio devido ao fato de as implementa��es espec�ficas
    /// de classe Dao (que n�o s�o o Dao gen�rico) ficarem no assembly da aplica��o (e n�o no assembly de 
    /// classes utilit�rias), o que tornaria necess�ria uma refer�ncia do assembly da aplcia��o no assembly
    /// utilit�rio, o que � inconceb�vel at� o presente momento.
    /// </summary>
    /// <exception cref=""></exception>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public class AbstractFactoryRepository
    {
        private static IFactoryRepository obj;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static IFactoryRepository GetInstance(String nomeTipoFabricaConcreta)
        {
            //"Cefet.Elicite.Persistencia.FactoryNHibernateDao":
            Type tipoFabricaConcreta = Type.GetType(nomeTipoFabricaConcreta);
            try
            {
                obj = (IFactoryRepository)Activator.CreateInstance(tipoFabricaConcreta);
                if (obj == null)
                {
                    throw new DaoException("N�o foi poss�vel criar objeto do tipo: '" + tipoFabricaConcreta.FullName + "'");
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new DaoException("N�o foi poss�vel criar objeto do tipo: '" + tipoFabricaConcreta.FullName + "'", ex);
            }

        }
    }
}
