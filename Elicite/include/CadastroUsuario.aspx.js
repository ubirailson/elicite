//Funções javascript específicas da página CadastroUsuarioExterno.aspx

/*
	Valida se senha e confirmação batem.
*/
function confirmacaoSenha(source, arguments)
{
    txtSenha = document.getElementById("ctl00_ContentPlaceHolder1_txtSenha");
    txtConfirmacao = document.getElementById("ctl00_ContentPlaceHolder1_txtConfirmacao");    
    if(txtSenha.value != txtConfirmacao.value )
    {	  
        arguments.IsValid=false;
    }
    else
    {
        arguments.IsValid=true;
    }    
}