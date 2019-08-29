using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cefet.Util.Dao;

namespace Cefet.Elicite.Dominio
{
    /// <summary>
    /// Interface com m�todos de persist�ncia de objetos de dom�nio. 
    /// Esta interface usa o padr�o <code>Repository</code> de Eric Evans. Com ela � poss�vel 
    /// realizar qualquer das opera��es CRUD(Create-criar, retrieve-recuperar, update-atualizar, delete-deletar)
    /// de persist�ncia para qualquer objeto de dom�nio. Ela � parte do dom�nio e ser� implementada por um 
    /// DAO (DAta Access Object) que ficar� omitido por uma f�brica. Para acessar 
    /// isnt�ncia dessa interface � necess�rio buscar o m�todo f�brica IFactoryDAO
    /// usando a f�brica abstrata <code>AbstractFactoryDAO</code> onde se escolhe a implementa��o 
    /// de persist�ncia que se deseja utilizar.
    /// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public interface IRepository
    {
        /// <summary>
        /// Salva um novo objeto persistente.
        /// </summary>
        /// <param name="obj">objeto a ser salvo</param>
        /// <param name="autoTransacao">diz se essa execu��o iniciar� e fechar� uma transa��o em si, ou se ser� parte de outra transa��o iniciada pelo m�todo IniciarTransacao()</param>
        /// <returns>Retorna objeto chave prim�ria do objeto persistido. 
        /// Pode ser desde inteiro e string at� a objetos complexos de chave composta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        Object Add(IEntity obj);
        
        
        /// <summary>
        /// Exclui objeto persistido
        /// </summary>
        /// <param name="obj">objeto do dom�nio com chave prim�ria preenchida</param>
        /// <param name="autoTransacao">diz se essa execu��o iniciar� e fechar� uma transa��o em si, ou se ser� parte de outra transa��o iniciada pelo m�todo IniciarTransacao()</param>
        ///<exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        void Remove(IEntity obj);
        
        
        /// <summary>
        /// Atualiza objeto persistente j� existente
        /// </summary>
        /// <param name="obj">objeto com chave prim�ria e atributos no estado que devem ser persistidos</param>
        /// <param name="autoTransacao">diz se essa execu��o iniciar� e fechar� uma transa��o em si, ou se ser� parte de outra transa��o iniciada pelo m�todo IniciarTransacao()</param>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        void Set(IEntity obj);
        
        /// <summary>
        /// Busca objeto pela chave prim�ria. 
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="objetoId">Objeto chave prim�ria do objeto persistido. 
        /// Pode ser desde inteiro e string at� a objetos complexos de chave composta</param>
        /// <returns>objeto espec�fico persistido que tem essa chave prim�ria</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        IEntity Get(Type tipo, Object objetoId);
        
        
        /// <summary>
        /// Busca todos os objetos do dom�nio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <returns>cole��o do tipo especificado</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAll(IEntity obj);
        
        
        /// <summary>
        /// Busca todos os objetos do dom�nio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="parametro">par�metro de ordena��o de resultado(qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado ordenado pelo par�metro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAll(IEntity obj, ParametroOrder parametro);
        
        
        /// <summary>
        /// Busca todos os objetos do dom�nio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="listaAscDesc">lista de par�metros de ordena��o de resultado(qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado ordenado pelo par�metro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAll(IEntity obj, ArrayList listaAscDesc);

       
        /// <summary>
        /// Busca todos os objetos do dom�nio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="tamanhoPagina">tamanho da p�gina</param>
        /// <param name="numeroPagina">n�mero da p�gina</param>
        /// <param name="parametro">par�metro de ordena��o de resultado(qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado ordenado pelo par�metro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllPaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro);
       
        
        /// <summary>
        /// Busca todos os objetos do dom�nio especificado
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo do objeto que se quer buscar</param>
        /// <param name="tamanhoPagina">tamanho da p�gina</param>
        /// <param name="numeroPagina">n�mero da p�gina</param>
        /// <param name="listaAscDesc">lista de par�metros de ordena��o de resultado(qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado ordenado pelo par�metro</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllPaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo. Preenche-se objeto do dom�nio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo. Preenche-se objeto do dom�nio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos par�metros que n�o devem 
        /// ser utilizados como filtro.</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo. Preenche-se objeto do dom�nio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="parametro">lista de par�metros de ordena��o de resultado
        /// (qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, ParametroOrder parametro);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo. Preenche-se objeto do dom�nio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos par�metros que n�o devem 
        /// ser utilizados como filtro.</param>
        /// <param name="parametro">lista de par�metros de ordena��o de resultado
        /// (qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos,
            ParametroOrder parametro);
        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo. Preenche-se objeto do dom�nio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="listaAscDesc">lista de par�metros de ordena��o de resultado
        /// (qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, ArrayList listaAscDesc);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo. Preenche-se objeto do dom�nio que 
        /// se quer pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos par�metros que n�o devem 
        /// ser utilizados como filtro.</param>
        /// <param name="listaAscDesc">lista de par�metros de ordena��o de resultado
        /// (qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExample(IEntity obj, string[] atributosExcluidos,
            ArrayList listaAscDesc);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo com resultado paginado. Preenche-se objeto do dom�nio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="tamanhoPagina">tamanho da p�gina</param>
        /// <param name="numeroPagina">n�mero da p�gina</param>
        /// <param name="parametro">par�metro de ordena��o de resultado
        /// (qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ParametroOrder parametro);


        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo com resultado paginado. Preenche-se objeto do dom�nio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos par�metros que n�o devem 
        /// ser utilizados como filtro.</param>
        /// <param name="tamanhoPagina">tamanho da p�gina</param>
        /// <param name="numeroPagina">n�mero da p�gina</param>
        /// <param name="parametro">par�metro de ordena��o de resultado
        /// (qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, string[] atributosExcluidos,
            int tamanhoPagina, int numeroPagina, ParametroOrder parametro);
        
        
        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo com resultado paginado. Preenche-se objeto do dom�nio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="tamanhoPagina">tamanho da p�gina</param>
        /// <param name="numeroPagina">n�mero da p�gina</param>
        /// <param name="listaAscDesc">lista de par�metros de ordena��o de resultado(qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, int tamanhoPagina,
            int numeroPagina, ArrayList listaAscDesc);
        
        
        /// <summary>
        /// Busca de cole��o de objetos de dom�nio espec�fico por exemplo com resultado paginado. Preenche-se objeto do dom�nio que se quer
        /// pesquisar e preenche-se os atributos que queremos utilizar como filtro.
        /// </summary>
        /// <param name="obj">objeto de exemplo com atributos(que dever�o ser filtrados) preenchidos</param>
        /// <param name="atributosExcluidos">array de strings com nomes dos par�metros que n�o devem 
        /// ser utilizados como filtro.</param>
        /// <param name="tamanhoPagina">tamanho da p�gina</param>
        /// <param name="numeroPagina">n�mero da p�gina</param>
        /// <param name="listaAscDesc">lista de par�metros de ordena��o de resultado(qual par�metro e em se em ascendente ou descendente)</param>
        /// <returns>cole��o do tipo especificado que se identificam com os par�metros da consulta</returns>
        /// <exception cref="Petrobras.TINE.Util.DAO.DAOException"></exception>
        System.Collections.ICollection GetAllByExamplePaginavel(IEntity obj, string[] atributosExcluidos, 
            int tamanhoPagina, int numeroPagina, ArrayList listaAscDesc);


        /// <summary>
        /// Retorna o n�mero de registros na cole��o de objetos(tabela de banco de dados)
        /// do tipo do objeto passado por par�metro.
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo persistente que se quer buscar</param>
        /// <returns>valor inteiro com contagem do n�mero de objetos persistidos</returns>
        int Size(IEntity obj);
        
        
        /// <summary>
        /// Retorna o n�mero de registros na cole��o de objetos(tabela de banco de dados)
        /// do tipo do objeto passado por par�metro.
        /// </summary>
        /// <param name="obj">objeto qualquer do tipo persistente que se quer buscar e com os par�metros 
        /// que se quer preencher como filtro. Vale salientar que s� funcionar� com atributos 
        /// primitivos(int, string, double)</param>
        /// <returns>valor inteiro com contagem do n�mero de objetos persistidos</returns>
        int SizePorExemplo(IEntity obj);
    }
}
