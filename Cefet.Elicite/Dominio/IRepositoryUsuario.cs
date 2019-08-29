using System;
using System.Collections;
using Iesi.Collections;
using Cefet.Elicite.Dominio;
using Cefet.Util.Dao;


namespace Cefet.Elicite.Dominio
{
	public interface IRepositoryUsuario : IRepository 
	{
		Usuario GetByLogin(String email);
        System.Collections.ICollection GetAllNotInThis(System.Collections.ICollection usuarios);
	}
}
