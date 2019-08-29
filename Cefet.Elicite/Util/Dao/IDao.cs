using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using Petrobras.Tine.Util.Dao.NHibernate;

namespace Cefet.Elicite.Util.Dao
{
    /// <summary>
    /// Interface com m�todos de persist�ncia de objetos de dom�nio. 
    /// Esta interface usa o padr�o de projeto <code>DAO(Data Access Object)</code>. Com ela � poss�vel 
    /// realizar qualquer das opera��es CRUD(Create-criar, retrieve-recuperar, update-atualizar, delete-deletar)
    /// de persist�ncia para qualquer objeto de dom�nio. Para acessar 
    /// isnt�ncia dessa interface � necess�rio buscar o m�todo f�brica IFactoryDAO
    /// usando a f�brica abstrata <code>AbstractFactoryDAO</code> onde se escolhe a implementa��o 
    /// de persist�ncia que se deseja utilizar.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IDao<T,IdT>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        T Criar(T obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void Remover(T obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        T SalvarOuAtualizar(T obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        T Atualizar(T obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        T BuscarPorChavePrimaria(IdT id);
            /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
  //      IPersistente BuscarPorChavePrimaria(IPersistente obj, Object key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> BuscarTodos();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        List<T> BuscarTodos( ParametroOrder parametro);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        List<T> BuscarTodos( ArrayList listaAscDesc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        List<T> BuscarTodosPaginavel( int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        List<T> BuscarTodosPaginavel( int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> BuscarPorExemplo(T obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        List<T> BuscarPorExemplo(T obj, ParametroOrder parametro);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        List<T> BuscarPorExemplo(T obj,ArrayList listaAscDesc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        List<T> BuscarPorExemploPaginavel(T obj,int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="listaAscDesc"></param>
        /// <returns></returns>
        List<T> BuscarPorExemploPaginavel(T obj,int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int GetObjectCount();
        int GetObjectCountPorExemplo(T obj);
    }
}
