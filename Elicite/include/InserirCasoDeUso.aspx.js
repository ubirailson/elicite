/**
 * Função genérica para mudar aba. Cada aba terá uma classe CSS que indica se está selecionado ou não
 * cujos nomes serão "selecionado" e "naoselecionado".
 * Cada aba deve ser nomeada com o nome "ctl00_ContentPlaceHolder1_lnk" concatenado com o seu número correspondente. 
 * Da mesma forma cada conteúdo deve ser nomeada com o nome "ctl00_ContentPlaceHolder1_conteudo" 
 * concatenado com o número correspondente de sua aba.
 *
 *Obs: este nome ficará assim se utilizar masterpage. Se não houve receptáculo envolvendo o panel ele ficará
 * com o id do servidor.
 *
 * @param cada aba, ao chamar a função javascript passará um número diferente de todas as demais abas
 * @param numeroAbas número de abas
 * 	
 */
function mudarAba(indiceClicado, numeroAbas) {
    
	for (var i = 1; i < (numeroAbas + 1); i++) {
	    var aba;
	    var conteudo;
	    if (i == indiceClicado)
	    {
		    aba = document.getElementById('ctl00_ContentPlaceHolder1_lnk' + i);
		    aba.className = "selecionado";
		    conteudo = document.getElementById('ctl00_ContentPlaceHolder1_pnlConteudo' + i);
		    conteudo.className = "blocoselecionado";
		}
		else
		{
		    var aba = document.getElementById('ctl00_ContentPlaceHolder1_lnk' + i);
		    aba.className = "naoselecionado";
		    var conteudo = document.getElementById('ctl00_ContentPlaceHolder1_pnlConteudo' + i);
		    conteudo.className = "bloconaoselecionado";
		}
	}
}
