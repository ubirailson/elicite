using System;
using System.Collections.Generic;
using System.Text;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;

namespace Cefet.Elicite.Persistencia
{
    /// <summary>
    /// Fábrica abstrata onde se escolhe a implementação da persistência. Chama o método fábrica que instanciará
    /// a implementação desejada. Existe um método fábrica abstrato contendo a implementação de cada tipo de 
    /// persistência suportado. Você criar uma classe que herde essa classe abstrara para utilizar seus métodos
    /// dentro do assembly da sua aplicação. Isso se faz necessário devido ao fato de as implementações específicas
    /// de classe Dao (que não são o Dao genérico) ficarem no assembly da aplicação (e não no assembly de 
    /// classes utilitárias), o que tornaria necessária uma referência do assembly da aplciação no assembly
    /// utilitário, o que é inconcebível até o presente momento.
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
                    throw new DaoException("Não foi possível criar objeto do tipo: '" + tipoFabricaConcreta.FullName + "'");
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new DaoException("Não foi possível criar objeto do tipo: '" + tipoFabricaConcreta.FullName + "'", ex);
            }

        }
    }
}
