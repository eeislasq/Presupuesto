using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Reflection;
using AplicacionFalp;
using Falp;


namespace PPTO001IE
{
    public partial class Frm_Plan_Tratamiento : Form
    {
        #region Variables

        #region Variables Comunes
        string usuario = "";
        string v_super_usuario = "";
        #endregion

        #region Variable DataTable

        DataTable dt_general = new DataTable();
        DataTable dt_ejecutiva = new DataTable();
        DataTable dt_plan_tratamiento = new DataTable();

        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion



        #endregion
       
        public Frm_Plan_Tratamiento()
        {
            InitializeComponent();
        }

        private void Frm_Plan_Tratamiento_Load(object sender, EventArgs e)
        {
            Conectar();
            Cargar_Grillas();
        }


        #region Conexion

        protected void Conectar()
        {
            if (!(CnnFalp != null))
            {
                ExeConfigurationFileMap FileMap = new ExeConfigurationFileMap();
                FileMap.ExeConfigFilename = Application.StartupPath + @"\..\WF.config";
                Config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None);

                CnnFalp = new ConectarFalp(Config.AppSettings.Settings["dbServer"].Value,//ConfigurationManager.AppSettings["dbServer"],
                                           Config.AppSettings.Settings["dbUser"].Value,//ConfigurationManager.AppSettings["dbUser"],
                                           Config.AppSettings.Settings["dbPass"].Value,//ConfigurationManager.AppSettings["dbPass"],
                                           ConectarFalp.TipoBase.Oracle);

                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir(); // abre la conexion
                Conexion[0] = Config.AppSettings.Settings["dbServer"].Value;
                Conexion[1] = Config.AppSettings.Settings["dbUser"].Value;
                Conexion[2] = Config.AppSettings.Settings["dbPass"].Value;
                this.Text = this.Text + " [Versión: " + Application.ProductVersion + "] [Conectado: " + Conexion[0] + "]";

            }

        }
        #endregion 

        #region Cargar Grillas

        private void Cargar_Grillas()
        {
          //  usuario = "VCALZADILLAS";
            usuario = ValidaMenu.LeeUsuarioMenu().Equals(string.Empty) ? "SICI" : ValidaMenu.LeeUsuarioMenu();
            Validar_usuario("PPTO001IE");
            v_usuario.Text = "Usuario: " + usuario ;
            txtestado.Text = "Pendiente";
            txtestado.Tag = 1;
            Cargar_Planes_Tratamiento();
            txtnum_doc.Enabled = false;
       
            txtguion.Visible = false;
            txtdiv.Visible = false;
        }

     

       /* private void Cargar_Estadistica_Ejecutiva()
        {
            dt_ejecutiva.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGA_ESTADISTICA_EJECUTIVO");
            CnnFalp.ParametroBD("PIN_USUARIO", v_usuario.Text, DbType.String, ParameterDirection.Input);
            dt_ejecutiva.Load(CnnFalp.ExecuteReader());
            grilla_ejecutivo.DataSource = dt_ejecutiva;
            CnnFalp.Cerrar();
        }*/

        private void Cargar_Planes_Tratamiento()
        {

            string fecha_desde = string.Empty;
            string fecha_hasta = string.Empty;
           
            if (ckbSFechas.Checked)
            {
                fecha_desde = "";
                fecha_hasta = "";
            }
            else
            {
                fecha_desde = txtdesde.Text;
                fecha_hasta = txthasta.Text;
            }

            dt_plan_tratamiento.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PLAN_TRATAMIENTO");
            CnnFalp.ParametroBD("PIN_ESTADO", Valor_por_defecto(txtestado.Tag), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_FECHA_INICIO",fecha_desde, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_FECHA_TERMINO", fecha_hasta, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_NOMBRE", txtnombre.Text.ToUpper(), DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_NUM_PLAN", txtnum_plan.Text.Equals(string.Empty)? 0: Convert.ToInt64(txtnum_plan.Text), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_NUM_PRESUPUESTO", txtnum_presupuesto.Text.Equals(string.Empty) ? 0 : Convert.ToInt64(txtnum_presupuesto.Text), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_NUM_FICHA", txtnum_ficha.Text.Equals(string.Empty) ? 0 : Convert.ToInt64(txtnum_ficha.Text), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPO_DOC",  Valor_por_defecto(txttipo_doc.Tag), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_RUT", txtnum_doc.Text, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_DIV", txtdiv.Text, DbType.String, ParameterDirection.Input);

            dt_plan_tratamiento.Load(CnnFalp.ExecuteReader());
            grilla_plan.DataSource = dt_plan_tratamiento;
            Ordenar_columna();
            CnnFalp.Cerrar();
            int cont=Convert.ToInt32(dt_plan_tratamiento.Rows.Count);
            n_reg.Text = "N° Registro: " + cont;


            if (txtestado.Tag.ToString().Equals("1"))
            {
                this.grilla_plan.Columns["USUARIOS"].Visible =false ;
            }
            else
            {
                this.grilla_plan.Columns["USUARIOS"].Visible = true;
            }

            if (txtestado.Tag.Equals(1) && txtnum_presupuesto.Text=="")
            {
                this.grilla_plan.Columns["H"].Visible = false;
            }
            else
            {
                this.grilla_plan.Columns["H"].Visible = true;
            }
            if (!txtestado.Tag.Equals(1))
            {
                this.grilla_plan.Columns["P"].Visible = false;
            }
            else
            {
                this.grilla_plan.Columns["P"].Visible = true;
            }
            
        }

        private void Ordenar_columna()
        {
            grilla_plan.Columns["P"].DisplayIndex = 0;
            grilla_plan.Columns["H"].DisplayIndex = 1;
            grilla_plan.Columns["NUMERO_PLAN"].DisplayIndex = 2;
            grilla_plan.Columns["FECHA_PLAN"].DisplayIndex = 3;
            grilla_plan.Columns["NOMBRE_PAC"].DisplayIndex = 4;
            grilla_plan.Columns["NOM_MEDICO"].DisplayIndex = 5;
            grilla_plan.Columns["NOM_DIAGNOSTICO"].DisplayIndex = 6;
            grilla_plan.Columns["NOM_PREVISION"].DisplayIndex = 7;
            grilla_plan.Columns["NOM_PLAN_PREV"].DisplayIndex = 8;
            grilla_plan.Columns["NOM_CONV_ONCOLOGICO"].DisplayIndex = 9;
            grilla_plan.Columns["USUARIOS"].DisplayIndex = 10;
            grilla_plan.Columns["NOM_ESTADO"].DisplayIndex = 11;
        }

        private void grilla_plan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                e.PaintBackground(e.ClipBounds, false);
                Font drawFont = new Font("Trebuchet MS", 8, FontStyle.Bold);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;
                StrFormat.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawImage(PPTO001IE.Properties.Resources.HeaderGV, e.CellBounds);
                e.Graphics.DrawString(grilla_plan.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }


        #endregion

        #region Botones

        private void btn_pac_externo_Click(object sender, EventArgs e)
        {
            Frm_Presupuesto frm_p = new Frm_Presupuesto();
            frm_p.Inicializar_datos(1, 1, 0, 0, "", v_super_usuario, 1);
            frm_p.ShowDialog();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            if(txtestado.Tag!="")
            {
               Cargar_Planes_Tratamiento();
            }
            else{
                Mensaje("Estado", 1);
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
           limpiar(this);
        }


        private void btn_estado_Click(object sender, EventArgs e)
        {
            txtestado.Text = "";
            txtestado.Tag = "";
            Cargar_Estado();
            txtestado.Focus();
        }

        private void btn_tipo_doc_Click(object sender, EventArgs e)
        {
            txttipo_doc.Text = "";
            txttipo_doc.Tag = "";
            Cargar_Tipo_Doc();
            txtnum_doc.Focus();
         
        }

        #endregion

        #region Metodos

        protected void limpiar(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = string.Empty;
                }
                if (c.Controls.Count > 0)
                {
                    limpiar(c);
                }
            }
            txtnum_doc.Enabled = false;
            txtestado.Text = "Pendiente";
            txtestado.Tag = "1";
            txttipo_doc.Tag = 0;
            txtguion.Visible = false;
            txtdiv.Visible = false;
        }

        #region Cargar Parametros Generales

        protected void Cargar_Estado()
        {
            Cargar_Parametros(ref Ayuda, 135, txtestado.Text,"ESTADOS PRESUPUESTO");

            if (!Ayuda.EOF())
            {
                txtestado.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtestado.Text = Ayuda.Fields(1);
            }
        }
        protected void Cargar_Tipo_Doc()
        {
            Cargar_Parametros(ref Ayuda, 73, txttipo_doc.Text, "TIPO DOC");

            if (!Ayuda.EOF())
            {
                txtnum_doc.Enabled = true;
                txttipo_doc.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txttipo_doc.Text = Ayuda.Fields(1);

                if (Convert.ToInt32(txttipo_doc.Tag) == 1)
                {
                    txtguion.Visible = true;
                    txtdiv.Visible = true;
                }
                else
                {
                    txtguion.Visible = false;
                    txtdiv.Visible = false;
                    
                }
            }
        }


        protected void Cargar_Parametros(ref AyudaSpreadNet.AyudaSprNet Ayuda, int num,string descripcion,string titulo)
        {
       
            string[] NomCol = { "Código", "Descripción" };
            int[] AnchoCol = { 80, 350 };
            Ayuda.Nombre_BD_Datos = Conexion[0];
            Ayuda.Pass = Conexion[1];
            Ayuda.User = Conexion[2];
            Ayuda.TipoBase = 1;
            Ayuda.NombreColumnas = NomCol;
            Ayuda.AnchoColumnas = AnchoCol;
            Ayuda.TituloConsulta = "INGRESAR " + titulo.ToUpper() + "";
            Ayuda.Package = PCK;
            Ayuda.Procedimiento = "P_CARGAR_PARAMETRO";
            Ayuda.Generar_ParametroBD("PIN_CODIGO", num, DbType.Int32, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", descripcion.ToUpper(), DbType.String, ParameterDirection.Input);
            Ayuda.EjecutarSql();

        }

        #endregion

     



        #endregion

        #region Validaciones

        private Int64 Valor_por_defecto(Object valor)
        {
            Int64 val=0;
           if (valor == null || valor=="")
           {
               val = 0;
           }
           else
           {
               val = Convert.ToInt64(valor);
           }

           return val;
        }

        private void Validar_usuario(string programa)
        {

            try
            {
               

                DataTable tbl = new DataTable();
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK+".FC_VALIDAR_USER");
                CnnFalp.ParametroBD("PIN_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PROGRAMA", programa, DbType.String, ParameterDirection.Input);
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
                tbl.Load(CnnFalp.ExecuteReader());

                foreach (DataRow miRow1 in tbl.Rows)
                {
                    v_super_usuario = miRow1["USUARIOS"].ToString();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void ckbSFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSFechas.Checked)
            {
                txtdesde.Enabled = false;
                txthasta.Enabled = false;
            }
            else
            {
                txtdesde.Enabled = true;
                txthasta.Enabled = true;
            }
        }

        #region KeyPress de los campos

        private void txtestado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                e.Handled = true;

                return;
            }
            else
            {
                Cargar_Estado();
                if (e.KeyChar == (char)13)
                {
                    txtnum_presupuesto.Focus();
                }
            }
        }


        private void txtnum_presupuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                e.Handled = true;

                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    txtnum_plan.Focus();
                }
            }
        }

        private void txtnum_plan_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                e.Handled = true;

                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    txtnum_ficha.Focus();
                }
            }
           
        }

        private void txtnum_ficha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                e.Handled = true;

                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    txttipo_doc.Focus();
                }
            }
        }

        private void txttipo_doc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                e.Handled = true;

                return;
            }
            else
            {
                Cargar_Tipo_Doc();
                if (e.KeyChar == (char)13)
                {
                    txtnum_doc.Focus();
                }
            }
        }

        private void txtnum_doc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                e.Handled = true;

                return;
            }
            else
            {
                if (Convert.ToInt32(txttipo_doc.Tag) == 1)
                {
                    if (e.KeyChar == (char)13)
                    {
                        txtdiv.Focus();
                    }
                }
                else
                {
                    Cargar_Tipo_Doc();
                    if (e.KeyChar == (char)13)
                    {
                        txtnombre.Focus();
                    }
                }
            }
        }

        private void txtdiv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && e.KeyChar != 'K' && e.KeyChar != 'k')
            {

                e.Handled = true;

                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    txtnombre.Focus();
                }
            }
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
            {

                e.Handled = true;

                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    btn_buscar.Focus();
                }
            }
        }


        #endregion 

        private void grilla_plan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Frm_Presupuesto frm_p = new Frm_Presupuesto();
            Frm_Historial frm_h = new Frm_Historial();
            if (e.RowIndex >= 0)
            {
                int cod_plan = Convert.ToInt32(grilla_plan.Rows[e.RowIndex].Cells["NUMERO_PLAN"].Value.ToString());
                int id_plan = Convert.ToInt32(grilla_plan.Rows[e.RowIndex].Cells["ID_CPLAN"].Value.ToString());
                if (e.ColumnIndex == 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Esta Seguro de Realizar un Presupuesto al Plan "+ cod_plan +"", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                        string fecha = grilla_plan.Rows[e.RowIndex].Cells["FECHA_PLAN"].Value.ToString();
                        string medico = grilla_plan.Rows[e.RowIndex].Cells["NOM_MEDICO"].Value.ToString();
                        string cod_medico = grilla_plan.Rows[e.RowIndex].Cells["COD_MEDICO_EHCOS"].Value.ToString();
                        string onco = grilla_plan.Rows[e.RowIndex].Cells["NOM_CONV_ONCOLOGICO"].Value.ToString();
                        string uni_clinica = grilla_plan.Rows[e.RowIndex].Cells["NOM_UNI_CLINICA"].Value.ToString();
                        string cod_uni_clinica = grilla_plan.Rows[e.RowIndex].Cells["COD_UNI_CLINICA"].Value.ToString();
                        string nom_paquete = grilla_plan.Rows[e.RowIndex].Cells["NOM_CPLAN_PAQUETE"].Value.ToString();
                        string pos_paquete = grilla_plan.Rows[e.RowIndex].Cells["CPLAN_PAQUETE"].Value.ToString();
                        string correlativo = grilla_plan.Rows[e.RowIndex].Cells["CORRELATIVO_PAC"].Value.ToString();
                        string cod_paquete = grilla_plan.Rows[e.RowIndex].Cells["COD_CPLAN_PAQUETE"].Value.ToString();
                        string cod_riesgo = grilla_plan.Rows[e.RowIndex].Cells["COD_RIESGO"].Value.ToString();
                        string nom_riesgo = grilla_plan.Rows[e.RowIndex].Cells["NOM_RIESGO"].Value.ToString();

                        frm_p.Inicializar_datos(id_plan, cod_plan, fecha, medico, onco, uni_clinica, nom_paquete, pos_paquete,correlativo,cod_uni_clinica,cod_paquete,cod_medico,cod_riesgo,nom_riesgo);
                        frm_p.ShowDialog();
                    }
                }
                else
                {
                    if (e.ColumnIndex == 1)
                    {
                        string usuario_grilla = grilla_plan.Rows[e.RowIndex].Cells["USUARIOS"].Value.ToString();
                        string nombre = grilla_plan.Rows[e.RowIndex].Cells["NOMBRE_PAC"].Value.ToString();
                        frm_h.Inicializar_datos(id_plan, cod_plan, nombre,usuario,usuario_grilla,v_super_usuario);
                        frm_h.ShowDialog();
                    
                    }
                  
                }

            }
            Cargar_Grillas();
        }


        protected void Mensaje(string descripcion, int tipo)
        {
            switch (tipo)
            {
                case 1: MessageBox.Show("Estimado Usuario, El Campo " + descripcion + " se Encuentra Vacio ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
           }

        }

        #endregion

       















    }
}
