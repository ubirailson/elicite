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
        //Este trecho de c�digo deve ser descomentado quando for para produ��o

        //try
        //{
        //    //Valores padr�o
        //    Session["ErrorMessage"] = "Ocorreu um erro desconhecido no sistema. ";
        //    Session["ExceptionType"] = String.Empty;
        //    Session["PageErrorOccured"] = Request.CurrentExecutionFilePath;
        //    Session["StackTrace"] = String.Empty;
        //    //Pega a �ltima exce��o capturada
        //    Exception lastError = Server.GetLastError();

        //    //Puxa a InnerException que foi a exce��o real. O ASP.NET lan�a uma exce��o por cima dessa chamada
        //    //HttpUnhandledException significando que a exce��o ocorrida n�o foi capturada
        //    if (lastError != null)
        //    {
        //        lastError = lastError.InnerException;
        //        Session["Exception"] = lastError.InnerException;
        //        //P�e em sess�o alguns valores relevantes do retorno da exce��o na p�gina de erro costumizada
        //        Session["ErrorMessage"] = lastError.Message;
        //        //m�todo de introspec��o(reflection para capturar tipo da classe)
        //        Session["ExceptionType"] = lastError.GetType().ToString();
        //        Session["StackTrace"] = lastError.StackTrace;
        //    }
        //    //Limpa o erro do servidor
        //    Server.ClearError();
        //    //Redireciona para a p�gina de erro costumizada
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
