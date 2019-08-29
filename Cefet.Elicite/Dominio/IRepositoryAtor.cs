namespace Cefet.Elicite.Dominio
{
	public interface IRepositoryAtor : IRepository 
	{
        /// <summary>
        /// Busca todos os objetos do domínio especificado
        /// </summary>
        /// <param name="projeto">projeto ao qual se atrelam</param>
        /// <returns></returns>
		System.Collections.ICollection GetAllByProjeto(Projeto projeto);
        System.Collections.ICollection GetAllByProjetoNotInThis(Projeto projeto, System.Collections.ICollection atores);
	}
}
