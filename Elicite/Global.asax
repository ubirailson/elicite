<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        log4net.Config.XmlConfigurator.Configure();

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        //**********************************************************************************************
        //Este trecho de código deve ser descomentado quando for para produção

        //try
        //{
        //    //Valores padrão
        //    Session["ErrorMessage"] = "Ocorreu um erro desconhecido no sistema. ";
        //    Session["ExceptionType"] = String.Empty;
        //    Session["PageErrorOccured"] = Request.CurrentExecutionFilePath;
        //    Session["StackTrace"] = String.Empty;
        //    //Pega a última exceção capturada
        //    Exception lastError = Server.GetLastError();

        //    //Puxa a InnerException que foi a exceção real. O ASP.NET lança uma exceção por cima dessa chamada
        //    //HttpUnhandledException significando que a exceção ocorrida não foi capturada
        //    if (lastError != null)
        //    {
        //        lastError = lastError.InnerException;
        //        Session["Exception"] = lastError.InnerException;
        //        //Põe em sessão alguns valores relevantes do retorno da exceção na página de erro costumizada
        //        Session["ErrorMessage"] = lastError.Message;
        //        //método de introspecção(reflection para capturar tipo da classe)
        //        Session["ExceptionType"] = lastError.GetType().ToString();
        //        Session["StackTrace"] = lastError.StackTrace;
        //    }
        //    //Limpa o erro do servidor
        //    Server.ClearError();
        //    //Redireciona para a página de erro costumizada
        //    Response.Redirect("~/Include/error.aspx");
        //}
        //catch (Exception ex)
        //{
        //    Response.Write("Ocorreu um erro desconhecido no sistema. ");
        //}

        //**********************************************************************************************

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
