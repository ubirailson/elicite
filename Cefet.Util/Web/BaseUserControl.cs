using System.Configuration;
using System.Collections;
using System.Web.Security;
using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cefet.Util.Web
{

	/// <summary>
    /// Classe base para as classes Web.UI.UserControl, usadas no code-behind dos usercontrols ASP.NET, contendo 
	/// os métodos e propriedades utilitários para Web UserControls. 
	/// </summary>
    /// <author>Ubirailson Jersy Soares de Medeiros</author>
    public partial class BaseUserControl : System.Web.UI.UserControl
	{
        public BaseUserControl() { }
		private void Page_Load(object sender, System.EventArgs e) 
		{	

		}

        /// <summary>
        /// Método para gerar mensagem flutuante na página
        /// </summary>
        /// <param name="mensagem">String contendo a mensagem</param>
        /// <param name="label">Referência ao webcontrol que renderizará a mensagem</param>
        /// <param name="left">número com posição left da caixa</param>
        /// <param name="top">número com posição top da caixa</param>
        /// <param name="background">cor de fundo da caixa(hexadecimal ou texto css válido)</param>
        /// <param name="corTexto">cor de do texto(hexadecimal ou texto css válido)</param>
        protected void ExibirMensagem(String mensagem, System.Web.UI.WebControls.Label label,
            String left, String top, String background, String corTexto)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<div style=\"position:absolute; z-index:1; width: 300px; left:" + left + "px; top:" + top + "px; color: " + corTexto + "; background-color: " + background + ";\n");
            buffer.Append("        visibility: visible;\" id=\"resposta\">\n");
            buffer.Append("  <table  border=\"0\">\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\">" + mensagem + "</td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"><a href=\"#\" onclick=\"document.getElementById('resposta').style.visibility='hidden'\">Fechar</a></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("  </table>\n");
            buffer.Append("</div>\n");
            label.Text = buffer.ToString();
            label.Visible = true;
            label.Enabled = true;
            label.EnableViewState = false;
        }
        /// <summary>
        /// Método para gerar mensagem flutuante na página
        /// </summary>
        /// <param name="mensagem">String contendo a mensagem</param>
        /// <param name="label">Referência ao webcontrol que renderizará a mensagem</param>
        /// <param name="className">Classe contendo estilo a ser aplicado na caixa de mensagem. 
        ///     Podendo conter entre outros: left, top, background-color e color.</param>
        protected void ExibirMensagem(String mensagem, System.Web.UI.WebControls.Label label,
            String className)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<div class=\"" + className + "\" style=\"position:absolute; z-index:1; width: 300px;visibility: visible;\" id=\"resposta\">\n");
            buffer.Append("  <table  border=\"0\">\n");
            buffer.Append("        <tr>");
            buffer.Append("          <td align=\"center\"></td>\n");
            buffer.Append("        </tr>");
            buffer.Append("        <tr>");
            buffer.Append("          <td align=\"center\">" + mensagem + "</td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"><a href=\"#\" onclick=\"document.getElementById('resposta').style.visibility='hidden'\">Fechar</a></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("  </table>\n");
            buffer.Append("</div>\n");
            label.Text = buffer.ToString();
            label.Visible = true;
            label.Enabled = true;
            label.EnableViewState = false;
        }
        /// <summary>
        /// Método para gerar mensagem flutuante na página
        /// </summary>
        /// <param name="mensagem">String contendo a mensagem</param>
        /// <param name="label">Referência ao webcontrol que renderizará a mensagem</param>
        /// <param name="left">número com posição left da caixa</param>
        /// <param name="top">número com posição top da caixa</param>
        /// <param name="className">Classe contendo estilo a ser aplicado na caixa de mensagem. 
        ///     Podendo conter entre outros: left, top, background-color e color.</param>
        protected void ExibirMensagem(String mensagem, System.Web.UI.WebControls.Label label,
            String left, String top, String className)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<div class=\"" + className + "\" style=\"position:absolute; z-index:1; width: 300px; left:" + left + "px; top:" + top + "px; visibility: visible;\" id=\"resposta\">\n");
            buffer.Append("  <table  border=\"0\">\n");
            buffer.Append("        <tr>");
            buffer.Append("          <td align=\"center\"></td>\n");
            buffer.Append("        </tr>");
            buffer.Append("        <tr>");
            buffer.Append("          <td align=\"center\">" + mensagem + "</td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("        <tr>\n");
            buffer.Append("          <td align=\"center\"><a href=\"#\" onclick=\"document.getElementById('resposta').style.visibility='hidden'\">Fechar</a></td>\n");
            buffer.Append("        </tr>\n");
            buffer.Append("  </table>\n");
            buffer.Append("</div>\n");
            label.Text = buffer.ToString();
            label.Visible = true;
            label.Enabled = true;
            label.EnableViewState = false;
        }

		/// <summary>
		/// Método para gerar classe DateTime apartir de valores de data separados.
		/// </summary>
		/// <param name="dia"> String com o dia</param>
		/// <param name="mes"> String com o mês</param>
		/// <param name="ano"> String com o ano</param>
		/// <returns>DateTime</returns>
        protected DateTime GerarData(string dia, string mes, string ano)
		{
			int iDia;
			int iMes;
			int iAno;
			if (dia==null || dia=="") { iDia = 0; }
			else { iDia = int.Parse(dia); }

			if (mes==null || mes=="") { iMes = 0; }
			else { iMes = int.Parse(mes); }

			if (ano==null || ano=="") { iAno = 0; }
			else { iAno = int.Parse(ano); }

			return new DateTime(iAno,iMes,iDia,0,0,0,0);
		}
		/// <summary>
		/// Método para gerar classe DateTime apartir de valores de data separados.
		/// </summary>
		/// <param name="dia"> String com o dia</param>
		/// <param name="mes"> String com o mês</param>
		/// <param name="ano"> String com o ano</param>
		/// <param name="hora"> String com a hora</param>
		/// <returns>DateTime</returns>
        protected DateTime GerarData(string dia, string mes, string ano, string hora)
		{
			int iDia;
			int iMes;
			int iAno;
			int iHora;
			if (dia==null || dia=="") { iDia = 0; }
			else { iDia = int.Parse(dia); }

			if (mes==null || mes=="") { iMes = 0; }
			else { iMes = int.Parse(mes); }

			if (ano==null || ano=="") { iAno = 0; }
			else { iAno = int.Parse(ano); }

			if (hora==null || hora=="") { iHora = 0; }
			else { iHora = int.Parse(hora); }

			return new DateTime(iAno,iMes,iDia,iHora,0,0,0);
		}
		/// <summary>
		/// Método para gerar classe DateTime apartir de valores de data separados.
		/// </summary>
		/// <param name="dia"> String com o dia</param>
		/// <param name="mes"> String com o mês</param>
		/// <param name="ano"> String com o ano</param>
		/// <param name="hora"> String com a hora</param>
		/// <param name="minuto"> String com o minuto</param>
		/// <returns>DateTime</returns>
        protected DateTime GerarData(string dia, string mes, string ano, string hora, 
										string minuto)
		{
			int iDia;
			int iMes;
			int iAno;
			int iHora;
			int iMinuto;
			if (dia==null || dia=="") { iDia = 0; }
			else { iDia = int.Parse(dia); }

			if (mes==null || mes=="") { iMes = 0; }
			else { iMes = int.Parse(mes); }

			if (ano==null || ano=="") { iAno = 0; }
			else { iAno = int.Parse(ano); }

			if (hora==null || hora=="") { iHora = 0; }
			else { iHora = int.Parse(hora); }

			if (minuto==null || minuto=="") { iMinuto = 0; }
			else { iMinuto = int.Parse(minuto); }

			return new DateTime(iAno,iMes,iDia,iHora,iMinuto,0,0);
		}
		/// <summary>
		/// Método para gerar classe DateTime apartir de valores de data separados.
		/// </summary>
		/// <param name="dia"> String com o dia</param>
		/// <param name="mes"> String com o mês</param>
		/// <param name="ano"> String com o ano</param>
		/// <param name="hora"> String com a hora</param>
		/// <param name="minuto"> String com o minuto</param>
		/// <param name="segundo"> String com o segundo</param>
		/// <returns>DateTime</returns>
        protected DateTime GerarData(string dia, string mes, string ano, string hora, 
			string minuto, string segundo)
		{
			int iDia;
			int iMes;
			int iAno;
			int iHora;
			int iMinuto;
			int iSegundo;
			if (dia==null || dia=="") { iDia = 0; }
			else { iDia = int.Parse(dia); }

			if (mes==null || mes=="") { iMes = 0; }
			else { iMes = int.Parse(mes); }

			if (ano==null || ano=="") { iAno = 0; }
			else { iAno = int.Parse(ano); }

			if (hora==null || hora=="") { iHora = 0; }
			else { iHora = int.Parse(hora); }

			if (minuto==null || minuto=="") { iMinuto = 0; }
			else { iMinuto = int.Parse(minuto); }

			if (segundo==null || segundo=="") { iSegundo = 0; }
			else { iSegundo = int.Parse(segundo); }
			
			return new DateTime(iAno,iMes,iDia,iHora,iMinuto,iSegundo,0);
		}
		
		/// <summary>
		/// Método para gerar classe DateTime apartir de valores de data separados.
		/// </summary>
		/// <param name="dia"> String com o dia</param>
		/// <param name="mes"> String com o mês</param>
		/// <param name="ano"> String com o ano</param>
		/// <param name="hora"> String com a hora</param>
		/// <param name="minuto"> String com o minuto</param>
		/// <param name="segundo"> String com o segundo</param>
		/// <param name="milisegundo"> String com o milisegundo</param>
		/// <returns>DateTime</returns>
		protected DateTime GerarData(string dia, string mes, string ano, string hora, 
			string minuto, string segundo, string milisegundo)
		{
			int iDia;
			int iMes;
			int iAno;
			int iHora;
			int iMinuto;
			int iSegundo;
			int iMiliSegundo;
			if (dia==null || dia=="") { iDia = 0; }
			else { iDia = int.Parse(dia); }

			if (mes==null || mes=="") { iMes = 0; }
			else { iMes = int.Parse(mes); }

			if (ano==null || ano=="") { iAno = 0; }
			else { iAno = int.Parse(ano); }

			if (hora==null || hora=="") { iHora = 0; }
			else { iHora = int.Parse(hora); }

			if (minuto==null || minuto=="") { iMinuto = 0; }
			else { iMinuto = int.Parse(minuto); }

			if (segundo==null || segundo=="") { iSegundo = 0; }
			else { iSegundo = int.Parse(segundo); }

			if (milisegundo==null || milisegundo=="") { iMiliSegundo = 0; }
			else { iMiliSegundo = int.Parse(milisegundo); }
			
			return new DateTime(iAno,iMes,iDia,iHora,iMinuto,iSegundo,iMiliSegundo);
		}

        /// <summary>
        /// Método que monta uma coleção de <code>ListItem</code> para alimentar um <code>DropDownList</code> 
        /// contento no campo texto(que será exibido para o usuário) "N" propriedades concatenadas e separadas 
        /// por "-".
        /// </summary>
        /// <remarks>
        /// Para efetuar a mesma ação contendo apenas um campo texto(que será exibido para o usuário) basta utilizar
        /// a maneira nativa que é: alimentar a propriedade <code>DataTextField</code> com o campo texto e
        /// <code>DataValueField</code> com o campo valor.
        /// Quando se trata de utilizar atributos de classes que estão em um relacionamento nos nomes de campo
        /// basta colocar no nome do atributo o nome do relacionamento  separador que é ponto (".") e o nome do atributo,
        /// ou seja, utilizar NomeDoRelacionamento.NomeDoAtributo.
        /// Por exemplo: numa classe "Pessoa" que tem um relacionamento chamado "RelatPerfil" com uma classe "Perfil",
        /// onde a classe perfil possui um atributo "NomePerfil", para utilizar esse atributo bastaria utilizar
        /// "RelatPerfil.NomePerfil". Assim como num atributo do tipo string chamado "Nome" na classe Pessoa seria
        /// chamado apenas de "Nome". Outra funcionalidade interessante é limitar o tamanho máximo da string.
        /// Quando chamamos um atributo com um limitador de caracteres que é ("|") o valor do atributo aparecerá
        /// com o número máximo de caracteres equivalente ao número que segue o "|". Por exemplo, quando quero
        /// que apareça o atributo "Nome" da classe "Pessoa" com no máximo 10 caracteres eu chamarei assim:
        /// "Nome|10".
        /// </remarks>
        /// <param name="colecaoEntidades">coleção que conterá os objetos tornados em item de exibição do 
        ///     dropdownlist</param>
        /// <param name="nomeValor">string com o nome da propriedade que será o valor do item do dropdownlist</param>
        /// <param name="nomesCampos">array de strings com os campos que serão o texto dos items do dropdownlist</param>
        /// <returns>coleção com os ListItens de dropdownlist</returns>
        /// <exception cref="Petrobras.TINE.Util.Web.WebUtilException">Se passar por parâmetro 
        ///     propriedades inexistentes</exception>
        protected ListItem[] MontarColecaoDeListItem(ICollection colecaoEntidades,
            string nomeValor, string[] nomesCampos)
        {
            ListItem[] colecaoListItem = new ListItem[colecaoEntidades.Count];

            PropertyReflector pr = new PropertyReflector();

            try
            {
                int contador = -1;
                foreach (Object item in colecaoEntidades)
                {
                    contador += 1;
                    String valor = pr.GetValue(item, nomeValor).ToString();
                    String texto = "";
                    String temp = "";
                    foreach (String nome in nomesCampos)
                    {
                        //Se existe este caracter separador no nome do atributo, então o tamanho da string deve
                        //ser limitado, senão entra no atributo do ListItem a string inteira.
                        if (nome.Contains("|"))
                        {
                            //Separa-se em duas strings, a string com o nome do atributo cujo valor deve ser
                            //buscado por reflection e a segunda string dizendo quantos caracteres do valor devem
                            //ser colocados no listitem. Por exemplo em "NomeDoAtributo|30" buscar-se-á
                            //o valor contido no atributo NomeDoAtributo, porém apenas sua substring de até
                            //30 caracteres.
                            char separador = '|';
                            String[] atributo = nome.Split(separador);
                            int tamanhoString = int.Parse(atributo[1]);
                            temp = pr.GetValue(item, atributo[0]).ToString();
                            //Testa se o tamanho da string é maior que o tamanho pedido, 
                            //para saber se deve usar sibstring ou não
                            if (tamanhoString < temp.Length)
                            {
                                if (!string.IsNullOrEmpty(texto))

                                    texto = texto + " - " + temp.Substring(0, tamanhoString) +
                                        "...";
                                else
                                    texto = temp.Substring(0, tamanhoString) +
                                        "...";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(texto))
                                    texto = texto + " - " + temp;
                                else
                                    texto = temp;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(texto))
                                texto = texto + " - " + pr.GetValue(item, nome).ToString();
                            else
                                texto = pr.GetValue(item, nome).ToString();
                        }
                    }
                    ListItem listItem = new ListItem(texto, valor);
                    colecaoListItem[contador] = listItem;
                }
            }
            //Exceções do namespace refletion
            catch (AmbiguousMatchException ame)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + ame.Message, ame);
            }
            catch (TargetParameterCountException tpe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + tpe.Message, tpe);
            }
            catch (TargetInvocationException tie)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetInvocationException\": " + tie.Message, tie);
            }
            catch (TargetException te)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetException\": " + te.Message, te);
            }

            //Exceções do namespace system
            catch (ArgumentNullException ane)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentNullException\": " + ane.Message, ane);
            }
            catch (MethodAccessException mae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"MethodAccessException\": " + mae.Message, mae);
            }
            catch (InvalidOperationException ioe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"InvalidOperationException\": " + ioe.Message, ioe);
            }
            catch (ArgumentException ae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentException\": " + ae.Message, ae);
            }
            catch (NullReferenceException nre)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"NullReferenceException\": " + nre.Message +
                    " Provavelmente você utilizou uma propriedade que o objeto não tem", nre);
            }
            catch (Exception e)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de tipo desconhecido: " + e.Message, e);
            }

            return colecaoListItem;
        }
        /// <summary>
        /// Método que monta uma coleção de <code>ListItem</code> para alimentar um <code>DropDownList</code> 
        /// contento no campo texto(que será exibido para o usuário) "N" propriedades concatenadas e separadas 
        /// por "-".
        /// </summary>
        /// <remarks>
        /// Para efetuar a mesma ação contendo apenas um campo texto(que será exibido para o usuário) basta utilizar
        /// a maneira nativa que é: alimentar a propriedade <code>DataTextField</code> com o campo texto e
        /// <code>DataValueField</code> com o campo valor.
        /// Quando se trata de utilizar atributos de classes que estão em um relacionamento nos nomes de campo
        /// basta colocar no nome do atributo o nome do relacionamento  separador que é ponto (".") e o nome do atributo,
        /// ou seja, utilizar NomeDoRelacionamento.NomeDoAtributo.
        /// Por exemplo: numa classe "Pessoa" que tem um relacionamento chamado "RelatPerfil" com uma classe "Perfil",
        /// onde a classe perfil possui um atributo "NomePerfil", para utilizar esse atributo bastaria utilizar
        /// "RelatPerfil.NomePerfil". Assim como num atributo do tipo string chamado "Nome" na classe Pessoa seria
        /// chamado apenas de "Nome". Outra funcionalidade interessante é limitar o tamanho máximo da string.
        /// Quando chamamos um atributo com um limitador de caracteres que é ("|") o valor do atributo aparecerá
        /// com o número máximo de caracteres equivalente ao número que segue o "|". Por exemplo, quando quero
        /// que apareça o atributo "Nome" da classe "Pessoa" com no máximo 10 caracteres eu chamarei assim:
        /// "Nome|10".
        /// </remarks>
        /// <param name="colecaoEntidades">coleção que conterá os objetos tornados em item de exibição do 
        ///     dropdownlist</param>
        /// <param name="nomeValor">string com o nome da propriedade que será o valor do item do dropdownlist</param>
        /// <param name="nomesCampos">array de strings com os campos que serão o texto dos items do dropdownlist</param>
        /// <param name="selectedValue">string com o valor(Value) do listitem que será selecionado no dropdownlist</param>
        /// <returns>coleção com os ListItens de dropdownlist</returns>
        /// <exception cref="Petrobras.TINE.Util.Web.WebUtilException">Se passar por parâmetro 
        ///     propriedades inexistentes</exception>
        protected ListItem[] MontarColecaoDeListItem(ICollection colecaoEntidades,
            string nomeValor, string[] nomesCampos, string selectedValue)
        {
            ListItem[] colecaoListItem = new ListItem[colecaoEntidades.Count];

            PropertyReflector pr = new PropertyReflector();

            try
            {
                int contador = -1;
                foreach (Object item in colecaoEntidades)
                {
                    contador += 1;
                    String valor = pr.GetValue(item, nomeValor).ToString();
                    String texto = "";
                    String temp = "";
                    foreach (String nome in nomesCampos)
                    {
                        //Se existe este caracter separador no nome do atributo, então o tamanho da string deve
                        //ser limitado, senão entra no atributo do ListItem a string inteira.
                        if (nome.Contains("|"))
                        {
                            //Separa-se em duas strings, a string com o nome do atributo cujo valor deve ser
                            //buscado por reflection e a segunda string dizendo quantos caracteres do valor devem
                            //ser colocados no listitem. Por exemplo em "NomeDoAtributo|30" buscar-se-á
                            //o valor contido no atributo NomeDoAtributo, porém apenas sua substring de até
                            //30 caracteres.
                            char separador = '|';
                            String[] atributo = nome.Split(separador);
                            int tamanhoString = int.Parse(atributo[1]);
                            temp = pr.GetValue(item, atributo[0]).ToString();
                            //Testa se o tamanho da string é maior que o tamanho pedido, 
                            //para saber se deve usar sibstring ou não
                            if (tamanhoString < temp.Length)
                            {
                                if (!string.IsNullOrEmpty(texto))

                                    texto = texto + " - " + temp.Substring(0, tamanhoString) +
                                        "...";
                                else
                                    texto = temp.Substring(0, tamanhoString) +
                                        "...";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(texto))
                                    texto = texto + " - " + temp;
                                else
                                    texto = temp;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(texto))
                                texto = texto + " - " + pr.GetValue(item, nome).ToString();
                            else
                                texto = pr.GetValue(item, nome).ToString();
                        }
                    }
                    ListItem listItem = new ListItem(texto, valor);

                    if (listItem.Value.Equals(selectedValue))
                    {
                        listItem.Selected = true;
                    }
                    colecaoListItem[contador] = listItem;
                }
            }
            //Exceções do namespace refletion
            catch (AmbiguousMatchException ame)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + ame.Message, ame);
            }
            catch (TargetParameterCountException tpe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + tpe.Message, tpe);
            }
            catch (TargetInvocationException tie)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetInvocationException\": " + tie.Message, tie);
            }
            catch (TargetException te)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetException\": " + te.Message, te);
            }

            //Exceções do namespace system
            catch (ArgumentNullException ane)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentNullException\": " + ane.Message, ane);
            }
            catch (MethodAccessException mae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"MethodAccessException\": " + mae.Message, mae);
            }
            catch (InvalidOperationException ioe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"InvalidOperationException\": " + ioe.Message, ioe);
            }
            catch (ArgumentException ae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentException\": " + ae.Message, ae);
            }
            catch (NullReferenceException nre)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"NullReferenceException\": " + nre.Message +
                    " Provavelmente você utilizou uma propriedade que o objeto não tem", nre);
            }
            catch (Exception e)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de tipo desconhecido: " + e.Message, e);
            }

            return colecaoListItem;
        }
        protected TreeNode[] MontarColecaoDeTreeNode(ICollection colecaoEntidades,
            string nomeValor, string[] nomesCampos)
        {
            TreeNode[] colecaoTreeNode = new TreeNode[colecaoEntidades.Count];

            PropertyReflector pr = new PropertyReflector();

            try
            {
                int contador = -1;
                foreach (Object item in colecaoEntidades)
                {
                    contador += 1;
                    String valor = pr.GetValue(item, nomeValor).ToString();
                    String texto = "";
                    String temp = "";
                    foreach (String nome in nomesCampos)
                    {
                        //Se existe este caracter separador no nome do atributo, então o tamanho da string deve
                        //ser limitado, senão entra no atributo do TreeNode a string inteira.
                        if (nome.Contains("|"))
                        {
                            //Separa-se em duas strings, a string com o nome do atributo cujo valor deve ser
                            //buscado por reflection e a segunda string dizendo quantos caracteres do valor devem
                            //ser colocados no TreeNode. Por exemplo em "NomeDoAtributo|30" buscar-se-á
                            //o valor contido no atributo NomeDoAtributo, porém apenas sua substring de até
                            //30 caracteres.
                            char separador = '|';
                            String[] atributo = nome.Split(separador);
                            int tamanhoString = int.Parse(atributo[1]);
                            temp = pr.GetValue(item, atributo[0]).ToString();
                            //Testa se o tamanho da string é maior que o tamanho pedido, 
                            //para saber se deve usar sibstring ou não
                            if (tamanhoString < temp.Length)
                            {
                                if (!string.IsNullOrEmpty(texto))

                                    texto = texto + " - " + temp.Substring(0, tamanhoString) +
                                        "...";
                                else
                                    texto = temp.Substring(0, tamanhoString) +
                                        "...";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(texto))
                                    texto = texto + " - " + temp;
                                else
                                    texto = temp;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(texto))
                                texto = texto + " - " + pr.GetValue(item, nome).ToString();
                            else
                                texto = pr.GetValue(item, nome).ToString();
                        }
                    }
                    TreeNode treeNode = new TreeNode(texto, valor);
                    colecaoTreeNode[contador] = treeNode;
                }
            }
            //Exceções do namespace refletion
            catch (AmbiguousMatchException ame)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + ame.Message, ame);
            }
            catch (TargetParameterCountException tpe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + tpe.Message, tpe);
            }
            catch (TargetInvocationException tie)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetInvocationException\": " + tie.Message, tie);
            }
            catch (TargetException te)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetException\": " + te.Message, te);
            }

            //Exceções do namespace system
            catch (ArgumentNullException ane)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentNullException\": " + ane.Message, ane);
            }
            catch (MethodAccessException mae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"MethodAccessException\": " + mae.Message, mae);
            }
            catch (InvalidOperationException ioe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"InvalidOperationException\": " + ioe.Message, ioe);
            }
            catch (ArgumentException ae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentException\": " + ae.Message, ae);
            }
            catch (NullReferenceException nre)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"NullReferenceException\": " + nre.Message +
                    " Provavelmente você utilizou uma propriedade que o objeto não tem", nre);
            }
            catch (Exception e)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de tipo desconhecido: " + e.Message, e);
            }

            return colecaoTreeNode;
        }
        protected TreeNode[] MontarColecaoDeTreeNode(ICollection colecaoEntidades,
            string nomeValor, string[] nomesCampos, string navigateURLWithEntityId)
        {
            TreeNode[] colecaoTreeNode = new TreeNode[colecaoEntidades.Count];

            PropertyReflector pr = new PropertyReflector();

            try
            {
                int contador = -1;
                foreach (Object item in colecaoEntidades)
                {
                    contador += 1;
                    String valor = pr.GetValue(item, nomeValor).ToString();
                    String texto = "";
                    String temp = "";
                    foreach (String nome in nomesCampos)
                    {
                        //Se existe este caracter separador no nome do atributo, então o tamanho da string deve
                        //ser limitado, senão entra no atributo do TreeNode a string inteira.
                        if (nome.Contains("|"))
                        {
                            //Separa-se em duas strings, a string com o nome do atributo cujo valor deve ser
                            //buscado por reflection e a segunda string dizendo quantos caracteres do valor devem
                            //ser colocados no TreeNode. Por exemplo em "NomeDoAtributo|30" buscar-se-á
                            //o valor contido no atributo NomeDoAtributo, porém apenas sua substring de até
                            //30 caracteres.
                            char separador = '|';
                            String[] atributo = nome.Split(separador);
                            int tamanhoString = int.Parse(atributo[1]);
                            temp = pr.GetValue(item, atributo[0]).ToString();
                            //Testa se o tamanho da string é maior que o tamanho pedido, 
                            //para saber se deve usar sibstring ou não
                            if (tamanhoString < temp.Length)
                            {
                                if (!string.IsNullOrEmpty(texto))

                                    texto = texto + " - " + temp.Substring(0, tamanhoString) +
                                        "...";
                                else
                                    texto = temp.Substring(0, tamanhoString) +
                                        "...";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(texto))
                                    texto = texto + " - " + temp;
                                else
                                    texto = temp;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(texto))
                                texto = texto + " - " + pr.GetValue(item, nome).ToString();
                            else
                                texto = pr.GetValue(item, nome).ToString();
                        }
                    }
                    TreeNode treeNode = new TreeNode(texto, valor, "",
                        navigateURLWithEntityId + "?value=" + valor, "");
                    colecaoTreeNode[contador] = treeNode;
                }
            }
            //Exceções do namespace refletion
            catch (AmbiguousMatchException ame)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + ame.Message, ame);
            }
            catch (TargetParameterCountException tpe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"AmbiguousMatchException\": " + tpe.Message, tpe);
            }
            catch (TargetInvocationException tie)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetInvocationException\": " + tie.Message, tie);
            }
            catch (TargetException te)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de reflection do tipo \"TargetException\": " + te.Message, te);
            }

            //Exceções do namespace system
            catch (ArgumentNullException ane)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentNullException\": " + ane.Message, ane);
            }
            catch (MethodAccessException mae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"MethodAccessException\": " + mae.Message, mae);
            }
            catch (InvalidOperationException ioe)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"InvalidOperationException\": " + ioe.Message, ioe);
            }
            catch (ArgumentException ae)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"ArgumentException\": " + ae.Message, ae);
            }
            catch (NullReferenceException nre)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro do tipo \"NullReferenceException\": " + nre.Message +
                    " Provavelmente você utilizou uma propriedade que o objeto não tem", nre);
            }
            catch (Exception e)
            {
                throw new WebUtilException("Erro interno de sistema. Favor contactar 881. Erro de tipo desconhecido: " + e.Message, e);
            }

            return colecaoTreeNode;
        }
        /// <summary>
        /// Método que garante que o texto no input n]ao é malicioso
        /// </summary>
        /// <param name="text">Texto digitado pelo usuário no Input</param>
        /// <param name="maxLength">Max length do texto digitado pelo usuário no Input</param>
        /// <returns>Versão limpa do texto digitado pelo usuário no Input</returns>
        public String InputText(string text, int maxLength)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;
        }

        /// <summary>
        /// Método que checa se o texto tem outro caracter que não numérico.
        /// </summary>
        public String CleanNonWord(string text)
        {
            return Regex.Replace(text, "\\W", "");
        }
        /// <summary>
        /// Desabilita todos os WebControl'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos WebControl'es serão desabilitados</param>
        protected void DesabilitarComponentesWebControl(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarComponentesWebControl(childControl);
                }
                else if ((childControl is WebControl))
                {
                    ((WebControl)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos os WebControl'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos WebControl'es serão habilitados</param>
        protected void HabilitarComponentesWebControl(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarComponentesWebControl(childControl);
                }
                else if ((childControl is WebControl))
                {
                    ((WebControl)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Desabilita todos webcontrols que implementam <code>IButtonControl</code> de existentes dentro do 
        /// Control que for passado por parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos botões serão desabilitados</param>
        protected void DesabilitarBotoesEmControl(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarBotoesEmControl(childControl);
                }
                else if ((childControl is IButtonControl) && (childControl is WebControl))
                {
                    ((WebControl)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos webcontrols que implementam <code>IButtonControl</code> de existentes dentro do 
        /// Control que for passado por parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos botões serão habilitados</param>
        protected void HabilitarBotoesEmControl(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarBotoesEmControl(childControl);
                }
                else if ((childControl is IButtonControl) && (childControl is WebControl))
                {
                    ((WebControl)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Desabilita todos os TextBox'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos textbox'es serão desabilitados</param>
        protected void DesabilitarComponentesTextBox(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarComponentesTextBox(childControl);
                }
                else if ((childControl is TextBox))
                {
                    ((TextBox)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos os TextBox'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos textbox'es serão habilitados</param>
        protected void HabilitarComponentesTextBox(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarComponentesTextBox(childControl);
                }
                else if ((childControl is TextBox))
                {
                    ((TextBox)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Desabilita todos os DropDownList'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos DropDownList'es serão desabilitados</param>
        protected void DesabilitarComponentesDropDownList(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarComponentesDropDownList(childControl);
                }
                else if ((childControl is DropDownList))
                {
                    ((DropDownList)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos os DropDownList'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos DropDownList'es serão habilitados</param>
        protected void HabilitarComponentesDropDownList(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarComponentesDropDownList(childControl);
                }
                else if ((childControl is DropDownList))
                {
                    ((DropDownList)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Desabilita todos os RadioButtonList'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos RadioButtonList'es serão desabilitados</param>
        protected void DesabilitarComponentesRadioButtonList(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarComponentesRadioButtonList(childControl);
                }
                else if ((childControl is RadioButtonList))
                {
                    ((RadioButtonList)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos os RadioButtonList'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos RadioButtonList'es serão habilitados</param>
        protected void HabilitarComponentesRadioButtonList(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarComponentesRadioButtonList(childControl);
                }
                else if ((childControl is RadioButtonList))
                {
                    ((RadioButtonList)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Desabilita todos os CheckBox'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos CheckBox'es serão desabilitados</param>
        protected void DesabilitarComponentesCheckBox(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarComponentesCheckBox(childControl);
                }
                else if ((childControl is CheckBox))
                {
                    ((CheckBox)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos os CheckBox'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos CheckBox'es serão habilitados</param>
        protected void HabilitarComponentesCheckBox(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarComponentesCheckBox(childControl);
                }
                else if ((childControl is CheckBox))
                {
                    ((CheckBox)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Limpa todos os TextBox'es existentes dentro do Control que for passado por parâmetro 
        /// desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos textbox'es serão limpos</param>
        protected void LimparTextBoxes(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    LimparTextBoxes(childControl);
                }
                else if (childControl is TextBox)
                {
                    ((TextBox)childControl).Text = "";
                }
            }
        }
        /// <summary>
        /// Desabilita todos os GridView'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos GridView'es serão desabilitados</param>
        protected void DesabilitarComponentesGridView(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    DesabilitarComponentesGridView(childControl);
                }
                else if ((childControl is GridView))
                {
                    ((GridView)childControl).Enabled = false;
                }
            }
        }
        /// <summary>
        /// Habilita todos os GridView'es existentes dentro do Control que for passado por 
        /// parâmetro desde Panel's até classes Page
        /// </summary>
        /// <param name="control">control cujos GridView'es serão habilitados</param>
        protected void HabilitarComponentesGridView(System.Web.UI.Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    HabilitarComponentesGridView(childControl);
                }
                else if ((childControl is GridView))
                {
                    ((GridView)childControl).Enabled = true;
                }
            }
        }
        /// <summary>
        /// Retorna coleção com os ListItens selecionados de um ListBox
        /// </summary>
        /// <param name="listBox"></param>
        /// <returns></returns>
        protected IList ExtrairItensSelecionadosDeListBox(System.Web.UI.WebControls.ListBox listBox)
        {
            ArrayList listItens = new ArrayList();
            foreach (ListItem item in listBox.Items)
            {
                if (item.Selected)
                {
                    listItens.Add(item);
                }
            }
            return listItens;
        }
    }

} 