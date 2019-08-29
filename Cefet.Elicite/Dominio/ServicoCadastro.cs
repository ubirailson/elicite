using System;
using System.Collections;
using Cefet.Elicite.Persistencia;
using Cefet.Util.Dao;
using System.Configuration;
using Cefet.Util.Criptografia;

namespace Cefet.Elicite.Dominio
{
	public class ServicoCadastro 
	{
        IRepository repositorio;
        IRepositoryUsuario repositorioUsuario;
        IRepositoryProjeto repositorioProjeto;

        public ServicoCadastro()
        {
            repositorio = (IRepository)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository();
            repositorioUsuario = (IRepositoryUsuario)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("Usuario");
            repositorioProjeto = (IRepositoryProjeto)AbstractFactoryRepository.GetInstance(
                    ConfigurationManager.AppSettings["METODO_FABRICA"]).GetRepository("Projeto");
        }

        public IRepository Repositorio
        {
            get { return repositorio; }
        }
        public IRepositoryUsuario RepositorioUsuario
        {
            get { return repositorioUsuario; }
        }
        public ICollection BuscarAtores(int tamanhoPagina, int numeroPagina)
        { 
            return repositorio.GetAllPaginavel(new Ator(),tamanhoPagina,numeroPagina,
                new ParametroOrder("Nome",AscDesc.Asc));
        }
        public ICollection BuscarProjetos(int tamanhoPagina, int numeroPagina)
        {
            return repositorio.GetAllPaginavel(new Projeto(), tamanhoPagina, numeroPagina,
                new ParametroOrder("Nome", AscDesc.Asc));
        }
        public ICollection BuscarUsuarios(int tamanhoPagina, int numeroPagina)
        {
            return repositorio.GetAllPaginavel(new Usuario(), tamanhoPagina, numeroPagina,
                new ParametroOrder("Nome", AscDesc.Asc));
        }
        public int Count()
        {
            return repositorio.Size(new Ator());
        }
        public int CountProjetos()
        {
            return repositorio.Size(new Projeto());
        }
        public int CountUsuarios()
        {
            return repositorio.Size(new Usuario());
        }
        public IRepositoryProjeto RepositorioProjeto
        {
            get { return repositorioProjeto; }
        }
        public Usuario CriarUsuario(Usuario usuario)
        {
            try
            {
                GeradorDeHash hash = new GeradorDeHash(HashProvider.MD5);
                Criptografia crypt = new Criptografia(CryptProvider.TripleDES);
                crypt.Key = Convert.ToString(ConfigurationManager.AppSettings["CHAVE_CRYPTOGRAFIA"]);
                usuario.Senha = crypt.Encrypt(hash.GetHash(usuario.Senha));
                repositorioUsuario.Add(usuario);
                return usuario;
            }
            catch (NegocioException nex)
            {
                throw nex;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                throw new NegocioException(aor.Message + " Valor inserido: " + aor.ActualValue.ToString());
            }
            catch (DaoException daoex)
            {
                throw new NegocioException("Erro ao adicionar ao repositório. ", daoex);
            }
        }
        public Usuario AtualizarUsuario(Usuario usuario, String senha)
        {
            try
            {
                if (!String.IsNullOrEmpty(senha))
                {
                    GeradorDeHash hash = new GeradorDeHash(HashProvider.MD5);
                    Criptografia crypt = new Criptografia(CryptProvider.TripleDES);
                    crypt.Key = Convert.ToString(ConfigurationManager.AppSettings["CHAVE_CRYPTOGRAFIA"]);
                    usuario.Senha = crypt.Encrypt(hash.GetHash(usuario.Senha));
                }
                repositorioUsuario.Set(usuario);
                return usuario;
            }
            catch (NegocioException nex)
            {
                throw nex;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                throw new NegocioException(aor.Message + " Valor inserido: " + aor.ActualValue.ToString());
            }
            catch (DaoException daoex)
            {
                throw new NegocioException("Erro ao adicionar ao repositório. ", daoex);
            }
        }
	}
}
