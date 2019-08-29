using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cefet.Util.Dao;

namespace Cefet.Elicite.Dominio
{
    /// <summary>
    /// Interface com métodos de persistência de objetos de domínio. 
    /// Esta interface usa o padrão <code>Repository</code> de Eric Evans. Com ela é possível 
    /// realizar qualquer das operações CRUD(Create-criar, retrieve-recuperar, update-atualizar, delete-deletar)
    /// de persistência para qualquer objeto de domínio. Ela é parte do domínio e será implementada por um 
    /// DAO (DAta Access Object) que ficará omitido por uma fábrica. Para acessar 
    /// isntância dessa interface é necessário buscar o método fábrica IFactoryDAO
    /// usando a fábrica abstrata <code>AbstractFactoryDAO</code> onde se escolhe a implementação 
    /// de persistência que se deseja utilizar.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IRepository
    {
        /// <summary>
        /// Salva um novo objeto persistente.
        /// </summary>
        /// <param name="obj">objeto a ser salvo</param>
        /// <param name="autoTransacao">diz se essa execução iniciará e fechará uma transação em si, ou se será parte de outra transação iniciada pelo método IniciarTransacao()</param>
        /// <returns>Retorna objeto chave primária do objeto persistido. 
        /// Pode ser desde inteiro e string até a objetos complexos de chave composta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        Object Add(IEntity obj);
        
        
        /// <summary>
        /// Exclui objeto persistido
        /// </summary>
        /// <param name="obj">objeto do domínio com chave primária preenchida</param>
        /// <param name="autoTransacao">diz se essa execução iniciará e fechará uma transação em si, ou se será parte de outra transação iniciada pelo método IniciarTransacao()</param>
        ///<exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        void Remove(IEntity obj);
        
        
        /// <summary>
        /// Atualiza objeto persistente já existente
        /// </summary>
        /// <param name="obj">objeto com chave primária e atributos no estado que devem ser persistidos</param>
        /// <param name="autoTransacao">diz se essa execução iniciará e fechará uma transação em si, ou se será parte de outra transação iniciada pelo método IniciarTransacao()</param>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        void Set(IEntity obj);
        
        /// <summary>
        /// Busca objeto pela chave primária. 
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="objetoId">Objeto chave primária do objeto persistido. 
        /// Pode ser desde inteiro e string até a objetos complexos de chave composta</param>
        /// <returns>objeto específico persistido que tem essa chave primária</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        IEntity Get(Type tipo, Object objetoId);
        
        
        /// <summary>
        /// Busca todos os objetos do domínio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <returns>coleção do tipo especificado</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAll(IEntity obj);
        
        
        /// <summary>
        /// Busca todos os objetos do domínio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="parametro">parâmetro de ordenação de resultado(qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado ordenado pelo parâmetro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAll(IEntity obj, ParametroOrder parametro);
        
        
        /// <summary>
        /// Busca todos os objetos do domínio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="listaAscDesc">lista de parâmetros de ordenação de resultado(qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado ordenado pelo parâmetro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAll(IEntity obj, ArrayList listaAscDesc);

       
        /// <summary>
        /// Busca todos os objetos do domínio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="tamanhoPagina">tamanho da página</param>
        /// <param name="numeroPagina">número da página</param>
        /// <param name="parametro">parâmetro de ordenação de resultado(qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado ordenado pelo parâmetro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllPaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro);
       
        
        /// <summary>
        /// Busca todos os objetos do domínio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="tamanhoPagina">tamanho da página</param>
        /// <param name="numeroPagina">número da página</param>
        /// <param name="listaAscDesc">lista de parâmetros de ordenação de resultado(qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado ordenado pelo parâmetro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllPaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo. Preenche-se objeto do domínio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo. Preenche-se objeto do domínio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos parâmetros que não devem 
        /// ser utilizados como filtro.</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo. Preenche-se objeto do domínio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="parametro">lista de parâmetros de ordenação de resultado
        /// (qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, ParametroOrder parametro);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo. Preenche-se objeto do domínio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos parâmetros que não devem 
        /// ser utilizados como filtro.</param>
        /// <param name="parametro">lista de parâmetros de ordenação de resultado
        /// (qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos,
            ParametroOrder parametro);
        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo. Preenche-se objeto do domínio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="listaAscDesc">lista de parâmetros de ordenação de resultado
        /// (qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, ArrayList listaAscDesc);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo. Preenche-se objeto do domínio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos parâmetros que não devem 
        /// ser utilizados como filtro.</param>
        /// <param name="listaAscDesc">lista de parâmetros de ordenação de resultado
        /// (qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos,
            ArrayList listaAscDesc);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo com resultado paginado. Preenche-se objeto do domínio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="tamanhoPagina">tamanho da página</param>
        /// <param name="numeroPagina">número da página</param>
        /// <param name="parametro">parâmetro de ordenação de resultado
        /// (qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro);


        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo com resultado paginado. Preenche-se objeto do domínio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos parâmetros que não devem 
        /// ser utilizados como filtro.</param>
        /// <param name="tamanhoPagina">tamanho da página</param>
        /// <param name="numeroPagina">número da página</param>
        /// <param name="parametro">parâmetro de ordenação de resultado
        /// (qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, string[] atributosExcluidos,
            int tamanhoPagina, int numeroPagina, ParametroOrder parametro);
        
        
        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo com resultado paginado. Preenche-se objeto do domínio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="tamanhoPagina">tamanho da página</param>
        /// <param name="numeroPagina">número da página</param>
        /// <param name="listaAscDesc">lista de parâmetros de ordenação de resultado(qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc);
        
        
        /// <summary>
        /// Busca de coleção de objetos de domínio específico por exemplo com resultado paginado. Preenche-se objeto do domínio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que deverão ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos parâmetros que não devem 
        /// ser utilizados como filtro.</param>
        /// <param name="tamanhoPagina">tamanho da página</param>
        /// <param name="numeroPagina">número da página</param>
        /// <param name="listaAscDesc">lista de parâmetros de ordenação de resultado(qual parâmetro e em se em ascendente ou descendente)</param>
        /// <returns>coleção do tipo especificado que se identificam com os parâmetros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, string[] atributosExcluidos, 
            int tamanhoPagina, int numeroPagina, ArrayList listaAscDesc);


        /// <summary>
        /// Retorna o número de registros na coleção de objetos(tabela de banco de dados)
        /// do tipo do objeto passado por parâmetro.
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo persistente que se quer buscar</param>
        /// <returns>valor inteiro com contagem do número de objetos persistidos</returns>
        int Size(IEntity obj);
        
        
        /// <summary>
        /// Retorna o número de registros na coleção de objetos(tabela de banco de dados)
        /// do tipo do objeto passado por parâmetro.
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo persistente que se quer buscar e com os parâmetros 
        /// que se quer preencher como filtro. Vale salientar que só funcionará com atributos 
        /// primitivos(int, string, double)</param>
        /// <returns>valor inteiro com contagem do número de objetos persistidos</returns>
        int SizePorExemplo(IEntity obj);
    }
}
