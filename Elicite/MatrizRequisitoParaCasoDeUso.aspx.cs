using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cefet.Util.Web;
using Cefet.Elicite.Dominio;
using log4net;

namespace Cefet.Elicite.Web
{
    public partial class MatrizRequisitoParaCasoDeUso : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MatrizRequisitoParaCasoDeUso));
        ServicoRastreabilidade service = new ServicoRastreabilidade();
        Usuario usuarioCorrente;
        Projeto projetoCorrente;
        protected void Page_Load(object sender, EventArgs e)
        {
            Object testeUsuario = Session["Usuario"];
            Object testeProjeto = Session["Projeto"];

            if (testeUsuario == null || testeProjeto == null)
            {
                Response.Redirect("Logout.aspx?mensagem=A sessão expirou. Por favor, efetue o login novamente. ",
                    true);
            }
            usuarioCorrente = (Usuario)testeUsuario;
            projetoCorrente = (Projeto)testeProjeto;

            MontarGridViewComColunasDinamicas(projetoCorrente);
        }
        /// <summary>
        /// Cria colunas com TemplateField contendo checkboxes dinamicamente adicionando
        /// tantas colunas quanto requisitos houver, bem como montando
        /// a primeira coluna com todos os requisitos funcionais
        /// </summary>
        /// <param name="projetoCorrente">projeto contendo todos os requisitos</param>
        protected void MontarGridViewComColunasDinamicas(Projeto projetoCorrente)
        {
            service = new ServicoRastreabilidade();
            ICollection requisitos = service.RepositorioRequisito.GetAllByProjetoOrderByTypeAndCodigo(projetoCorrente);
            foreach (Requisito requisito in requisitos)
            {
                TemplateField customField = new TemplateField();
                customField.ItemTemplate = new CheckBoxMatrizTemplateCasoDeUso(DataControlRowType.DataRow,
                    requisito.Id.ToString());
                customField.HeaderTemplate = new CheckBoxMatrizTemplateCasoDeUso(DataControlRowType.Header,
                    requisito.CodigoRequisito);
                grvMatrizCasoDeUso.Columns.Add(customField);
            }
            grvMatrizCasoDeUso.DataSource = service.RepositorioCasoDeUso.GetAllByProjetoOrderByCodigoCasoUso(projetoCorrente);
            grvMatrizCasoDeUso.DataBind();
        }
        protected void grvMatrizCasoDeUso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Coloca o código do registro na primeira coluna do grid e 
                //adiciona label invisível com o seu ID para colocar posteriormente
                //nos checkboxes
                CasoDeUso bean = (CasoDeUso)e.Row.DataItem;
                Label labelTexto = (Label)e.Row.FindControl("lblTexto");
                labelTexto.Text = bean.CodigoCasoUso;
                Label labelId = (Label)e.Row.FindControl("lblId");
                labelId.Text = bean.Id.ToString();

                GridViewRow row = e.Row;

                //verifica requisitos rastreados pela primeira coluna e marca o checkbox
                //das colunas seguintes onde houver rastreabilidade marcada 
                foreach (Requisito rastreado in bean.RequisitosRastreados)
                {
                    CheckBox check = (CheckBox)e.Row.FindControl(rastreado.Id.ToString());
                    check.Checked = true;
                }
                //Concatena o ID do requisito jah renderizado no template com o id dos casos de uso
                //para gerar o relacionamento posteriormente quando marcado ou desmarcado
                foreach (Control checkBox in row.Controls)
                {
                    AdicionaIdEmCheckBox(checkBox, labelId.Text);
                }
            }
        }
        /// <summary>
        /// Adiciona o id passado recursivamente a todos os checkboxes contidos no Control passado
        /// por parâmetro
        /// </summary>
        /// <param name="control"></param>
        /// <param name="id"></param>
        protected void AdicionaIdEmCheckBox(System.Web.UI.Control control, String id)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.Controls.Count > 0)
                {
                    AdicionaIdEmCheckBox(childControl, id);
                }
                else if ((childControl is CheckBox))
                {
                    childControl.ID = childControl.ID + "-" + id;
                }
            }
        }
    }
    /// <summary>
    /// Classe de Template para gerar dinamicamente as colunas do gridview em uma quantidade variável de colunas
    /// Porém no mesmo formato. Contendo um checkbox em cada célula, e em seu ID o id da coluna mais hífen(-) id
    /// da linha
    /// </summary>
    public class CheckBoxMatrizTemplateCasoDeUso : BaseTemplate
    {
        string erro;
        private static readonly ILog log = LogManager.GetLogger(typeof(CheckBoxMatrizTemplateCasoDeUso));
        private DataControlRowType templateType;
        private string columnName;
        private ServicoRastreabilidade service;
        Label lblMensagem;

        public CheckBoxMatrizTemplateCasoDeUso(DataControlRowType type, string colname)
        {
            templateType = type;
            columnName = colname;
        }

        public override void InstantiateIn(System.Web.UI.Control container)
        {
            // Create the content for the different row types.
            switch (templateType)
            {
                case DataControlRowType.Header:
                    // Create the controls to put in the header
                    // section and set their properties.
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";

                    // Add the controls to the Controls collection
                    // of the container.
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:
                    // Create the controls to put in a data row
                    // section and set their properties.
                    CheckBox rastreador = new CheckBox();
                    rastreador.ID = columnName;
                    rastreador.AutoPostBack = true;
                    rastreador.CheckedChanged += new EventHandler(this.CheckBoxGrvMatrizCasoDeUso_CheckedChanged);

                    Literal spacer = new Literal();
                    spacer.Text = "";

                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event
                    // handler.
                    //rastreador.DataBinding += new EventHandler(this.Rastreador_DataBinding);

                    // Add the controls to the Controls collection
                    // of the container.
                    container.Controls.Add(spacer);
                    container.Controls.Add(rastreador);

                    break;

                // Insert cases to create the content for the other 
                // row types, if desired.

                default:
                    // Insert code to handle unexpected values.
                    break;
            }
        }
        /// <summary>
        /// Busca o Control Pai recursivamente até achar o gridview. Achando gridview
        /// Ele usa o findcontrol no pai de gridview para pegar o label
        /// </summary>
        /// <param name="control"></param>
        protected void BuscarLabel(System.Web.UI.Control control)
        {
            if (control.Parent.GetType() != typeof(GridView))
            {
                BuscarLabel(control.Parent);
            }
            else
            {
                lblMensagem = (Label)control.Parent.Parent.FindControl("lblMensagem");
            }
        }
        /// <summary>
        /// Evento que persiste a marcação ou desmarcação do checkbox que rastreia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckBoxGrvMatrizCasoDeUso_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox control = (CheckBox)sender;
            BuscarLabel(control);

            try
            {
                string[] ids = control.ID.Split('-');
                service = new ServicoRastreabilidade();
                Requisito requisito = (Requisito)service.RepositorioRequisito.Get(typeof(Requisito),
                    int.Parse(ids[0]));
                CasoDeUso casoDeUso = (CasoDeUso)service.RepositorioCasoDeUso.Get(typeof(CasoDeUso),
                    int.Parse(ids[1]));
                if (control.Checked)
                {
                    service.AdicionarRastreamento(casoDeUso, requisito);
                }
                else
                {
                    service.RemoverRastreamento(casoDeUso, requisito);
                }
            }
            catch (NegocioException ne)
            {
                erro = ne.Message;
                log.Error(erro, ne);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
            catch (Exception ex)
            {
                erro = "Erro desconhecido. ";
                log.Error(erro, ex);
                ExibirMensagem(erro, lblMensagem, "60", ConfigurationManager.AppSettings["TOPO_MENSAGEM"], ConfigurationManager.AppSettings["COR_FUNDO_MENSAGEM"], ConfigurationManager.AppSettings["COR_TEXTO_MENSAGEM"]);
            }
        }
    }
}