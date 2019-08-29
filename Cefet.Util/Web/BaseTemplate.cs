using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Cefet.Util.Web
{
    public class BaseTemplate:ITemplate
    {
        /// <summary>
        /// M�todo para gerar mensagem flutuante na p�gina
        /// </summary>
        /// <param name="mensagem">String contendo a mensagem</param>
        /// <param name="label">Refer�ncia ao webcontrol que renderizar� a mensagem</param>
        /// <param name="left">n�mero com posi��o left da caixa</param>
        /// <param name="top">n�mero com posi��o top da caixa</param>
        /// <param name="background">cor de fundo da caixa(hexadecimal ou texto css v�lido)</param>
        /// <param name="corTexto">cor de do texto(hexadecimal ou texto css v�lido)</param>
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
        /// M�todo para gerar mensagem flutuante na p�gina
        /// </summary>
        /// <param name="mensagem">String contendo a mensagem</param>
        /// <param name="label">Refer�ncia ao webcontrol que renderizar� a mensagem</param>
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
        /// M�todo para gerar mensagem flutuante na p�gina
        /// </summary>
        /// <param name="mensagem">String contendo a mensagem</param>
        /// <param name="label">Refer�ncia ao webcontrol que renderizar� a mensagem</param>
        /// <param name="left">n�mero com posi��o left da caixa</param>
        /// <param name="top">n�mero com posi��o top da caixa</param>
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
        public virtual void InstantiateIn(System.Web.UI.Control container)
        {
            
        }
    }
}
