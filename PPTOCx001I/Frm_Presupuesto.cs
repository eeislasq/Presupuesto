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
using PreparacionDroga;
using AplicacionFalp;
using Falp;

namespace PPTO001IE
{
    public partial class Frm_Presupuesto : Form
    {
        #region Variables

        #region Variables Comunes

        Int64 v_id_plan=0;
        Int64 v_cod_plan = 0;
        Int64 v_cod_presupuesto = 0;
        Int64 v_cod_riesgo = 0;
        Int64 v_cod_fai = 0;
        Int64 v_estado_fai = 0;
        Int64 v_correlativo = 0;
        Int64 v_cod_estado = 0;
        Int64 v_cod_anulacion = 0;
        string nom_lap = "";
        string v_usuario_grilla = "";
        string v_super_usuario = "";
        Int64 cod_lap=0;
        string Super_usuario = "";
        string usuario = "";
        string folios = "";
        int Excluir = 0;
        string uo_numero = "00002";
        Int64 cant_cama = 0;
        Int64 total_cama = 0;
        Int64 selec_cama = 0;
        string prestaciones = "";
        string guarismo = "";
        
     
        #endregion

        #region Variable DataTable

        DataTable dt_diagnostico = new DataTable();
        DataTable dt_presupuesto = new DataTable();
        DataTable dt_prestaciones = new DataTable();
        DataTable dt_folios = new DataTable();
        DataTable dt_camas = new DataTable();
        DataTable dt_totales = new DataTable();
        DataTable dt_historial = new DataTable();
        DataTable dt_datos_presupuesto = new DataTable();
        DataTable dt_hm = new DataTable();
        public DataTable dt_medyins_total_p = new DataTable();
        public DataTable dt_prestadi_total_p = new DataTable();


        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion

        #endregion
        public Frm_Presupuesto()
        {
            InitializeComponent();
        }

        private void Frm_Presupuesto_Load(object sender, EventArgs e)
        {
            Conectar();
            agregarimagen_concepto();
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
                usuario = ValidaMenu.LeeUsuarioMenu().Equals(string.Empty) ? "SICI" : ValidaMenu.LeeUsuarioMenu();
            }

        }
        #endregion 
     
        #region  Metodo

        #region Agregar Imagen

        protected void agregarimagen()
        {
            foreach (DataGridViewRow row in grilla_prestaciones.Rows)
            {
                string ve = Convert.ToString(row.Cells["SELECCION"].Value);
                DataGridViewImageCell Imagen = row.Cells["S"] as DataGridViewImageCell;

                if (ve == "S")
                {
                    Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.Check;
                }
                else
                {
                    Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.bloqueo;
                }
            }
        }

    
        #endregion

        #region  Inicializa Pantalla

        public void Inicializar_datos(Int64 id_plan, Int64 cod_plan, string fecha, string medico, string onco, string uni_clinica, string nom_paquete, string pos_paquete, string correlativo,string cod_uni_clinica,string cod_paquete,string cod_medico,string cod_riesgo, string nom_riesgo)
        {
            Conectar();
            v_usuario.Text = "Usuario: " + usuario;
           
            v_id_plan = id_plan;
            v_cod_plan = cod_plan;
            txtcod_plan.Text = cod_plan.ToString();
            txtfecha_plan.Text = fecha;
            txtmedico.Text = medico;
            txtmedico.Tag = cod_medico;
            txt_conv_onco.Text = onco;
            txtpaquete.Text = nom_paquete;
            txtpaquete.Tag = cod_paquete;
            txtuni_clinica.Text = uni_clinica;
            txtuni_clinica.Tag = cod_uni_clinica;
            txtpos_paquete.Text = pos_paquete;
            txtriesgo_clinico.Tag = cod_riesgo;
            txtriesgo_clinico.Text = nom_riesgo;
            Cargar_diagnostico();
            Buscar_Paciente(correlativo);
            v_correlativo = Convert.ToInt64(correlativo);
            Cargar_Prestaciones();
            gr_concepto.Enabled = false;
            Validar_usuario();
            btn_confirmar.Visible = false;
            barra.Visible = false;
            txtcont.Visible = false;
            CrearConceptos();
            CrearFolios();
            btn_guardar.Enabled = false;
            btn_anular.Enabled = false;
            Cargar_Historial();
            n_presupuesto.Text = "0";
            txtestado.Text = "PENDIENTE";
            btnBuscarFolios.Enabled = false; 
        }

        public void Inicializar_datos(Int64 id_plan, Int64 cod_plan,Int64 cod_presupuesto,Int64 cod_correlativo,string usuario_grilla,string super_usuario,Int64 cod_estado)
        {
            Conectar();
            v_id_plan = id_plan;
            v_cod_plan = cod_plan;
            txtcod_plan.Text = cod_plan.ToString();
            v_correlativo = cod_correlativo;
            v_cod_presupuesto = cod_presupuesto;
            n_presupuesto.Text = v_cod_presupuesto.ToString();
            Validar_usuario();
            v_usuario_grilla=usuario_grilla;
            v_super_usuario=super_usuario;
            v_cod_estado = cod_estado;
            Cargar_Historial();       
          //  gr_concepto.Enabled = false;
          //  btn_guardar.Enabled = false;

            if (usuario.ToUpper() == v_usuario_grilla.ToUpper() || v_super_usuario != "")
            {
                btn_anular.Enabled = true;
            }
            else
            {
                btn_anular.Enabled = false;
            }
            CrearConceptos();
            CrearFolios();
            Cargar_Prestaciones();
            btn_confirmar.Visible = false;
            barra.Visible = false;
            txtcont.Visible = false;

            if (v_cod_presupuesto > 0)
            {
                Cargar_datos();
                btnBuscarFolios.Enabled = true;
              btn_guardar.Visible = false;
              btn_limpiar.Enabled = false;
            }
            else
            {
                btn_guardar.Visible = true;
                btn_limpiar.Enabled = true;
                if (cod_correlativo == 0)
                {
                    Buscar_Paciente("0");
                }
                else
                {
                    Buscar_Paciente(cod_correlativo.ToString());
                }
                btnBuscarFolios.Enabled = false; 
            }

            if (id_plan == 1)
            {
                txtcod_plan.Text = "0000000" + id_plan;
            }
        }


        #endregion

        #region  Cargar  Datos

        protected void Cargar_datos()
        {
            Cargar_Paciente();
            Cargar_Conceptos_Presupuesto();
            Cargar_Prestaciones_Presupuestos();
            prestaciones = Concatenar_prestaciones();
            Cargar_diagnostico();
            CalculoHonorarios();
            agregarimagen_concepto();
           // gr_calcular.Enabled = false;
            grilla_prestaciones.Enabled = false;
            txtcodigo.Enabled = false;
            txtdescripcion.Enabled = false;
            btn_codigo_fonasa.Enabled = false;
            btn_calcular.Enabled = false;
            txtconcepto.Enabled = false;
            txtcantidad.Enabled = false;
            txtvalor.Enabled = false;
            btn_añadir_concepto.Enabled = false;
            btn_concepto.Enabled = false;
            gr_plan.Enabled = false;
            btn_imprimir.Enabled = true;
      
        }

        #endregion


        #region  Busca Paciente

        protected void Buscar_Paciente(string correlativo)
        {
            CargarPaciente(ref ctrl_Paciente, Convert.ToInt32(correlativo));
            if (ctrl_Paciente.Correlativo != 0)
            {
                v_correlativo = ctrl_Paciente.Correlativo;
                gr_contenedor.Enabled = true;
                gr_historial.Enabled = true;
                txtficha.Text = ctrl_Paciente.Ficha.ToString();
                txttipo_doc.Text = ctrl_Paciente.Desc_TipoDoc;
                txtnum_doc.Text = ctrl_Paciente.Documento;
                txtdiv.Text = ctrl_Paciente.DV;
                txtnombres.Text = ctrl_Paciente.Nombres;
                txtpaterno.Text = ctrl_Paciente.ApPaterno;
                txtmaterno.Text = ctrl_Paciente.ApMaterno;
                txtsexo.Text = ctrl_Paciente.Sexo;
                txtfecha_nac.Text = ctrl_Paciente.Fecha_Nacimiento;
                txtedad.Text = ctrl_Paciente.Edad.ToString();
                txtcivil.Text = ctrl_Paciente.Desc_Est_Civil;
                txtinstitucion.Text = ctrl_Paciente.Desc_Prevision;
                txtinstitucion.Tag = ctrl_Paciente.Cod_Prevision;
                txtprevicion.Text = ctrl_Paciente.Desc_Tipo_Prev;
                txtprevicion.Tag = ctrl_Paciente.Cod_Tipo_Prev;
                txtplan_previsional.Text = ctrl_Paciente.Desc_Convenio;
                txtplan_previsional.Tag = ctrl_Paciente.Cod_Plan_Prev;
                Cargar_Historial();
            }
            else
            {
                gr_contenedor.Enabled = false;
                gr_historial.Enabled = false;
                Mensaje("", 13);
            }
        }

        public void CargarPaciente(ref Paciente.Ctrl_Paciente pac, int correlativo)
        {
            try
            {
                pac.Select(correlativo, 2, Conexion[0], Conexion[1], Conexion[2], 1, true, "SICI", "S");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }


        protected void Cargar_Paciente()
        {
            dt_datos_presupuesto.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PRESUPUESTO");
            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            dt_datos_presupuesto.Load(CnnFalp.ExecuteReader());
            Extraer_Presupuesto();
            CnnFalp.Cerrar();
        }

        protected void Extraer_Presupuesto()
        {
            foreach (DataRow miRow1 in dt_datos_presupuesto.Rows)
            {
                txtficha.Text = miRow1["FICHA"].ToString().Trim();
                txttipo_doc.Text = miRow1["TIPO_DOC"].ToString().Trim();
                txtnum_doc.Text = miRow1["RUT"].ToString().Trim();
                txtdiv.Text = miRow1["DIV"].ToString().Trim();
                txtnombres.Text = miRow1["NOMBRES"].ToString().Trim();
                txtpaterno.Text = miRow1["PATERNO"].ToString().Trim();
                txtmaterno.Text = miRow1["MATERNO"].ToString().Trim();
                txtsexo.Text = miRow1["SEXO"].ToString().Trim();
                txtfecha_nac.Text = miRow1["FECHA_NAC"].ToString().Trim();
                txtedad.Text = miRow1["EDAD"].ToString().Trim();
                txtcivil.Text = miRow1["ECIVIL"].ToString().Trim();
                txtinstitucion.Text = miRow1["NOM_PREVISION"].ToString().Trim();  
                txtinstitucion.Tag = miRow1["COD_PREVISION"].ToString().Trim();
                txtprevicion.Text = miRow1["NOM_TIPO_PREVISION"].ToString().Trim();
                txtprevicion.Tag = miRow1["COD_TIPO_PREVISION"].ToString().Trim();
                txtplan_previsional.Text = miRow1["NOM_PLAN_PREVISIONAL"].ToString().Trim();
                txtplan_previsional.Tag = miRow1["COD_PLAN_PREVISIONAL"].ToString().Trim();
                txtanula.Text = miRow1["NOM_MOTIVO"].ToString().Trim();
                txtcod_plan.Text= miRow1["COD_PLAN"].ToString().Trim();
                txtfecha_plan.Text = miRow1["FECHA_PLAN"].ToString().Trim();
                txtuni_clinica.Text = miRow1["NOM_UNI_CLINICA"].ToString().Trim();
                txtmedico.Text = miRow1["NOM_MEDICO"].ToString().Trim();
                txtpos_paquete.Text = miRow1["PRES_PAQUETE"].ToString().Trim();
                txtpaquete.Text = miRow1["NOM_PAQUETE"].ToString().Trim();
                txt_conv_onco.Text = miRow1["NOM_CONV_ONCOLOGICO"].ToString().Trim();
                txtHonorario.Tag = miRow1["COD_TIPO_HONORARIOS"].ToString().Trim();
                txtHonorario.Text = miRow1["NOM_TIPO_HONORARIOS"].ToString().Trim();
                txtestado.Text = miRow1["NOM_ESTADO"].ToString().Trim();
                txtriesgo_clinico.Tag = miRow1["COD_RIESGO"].ToString().Trim();
                txtriesgo_clinico.Text = miRow1["NOM_RIESGO"].ToString().Trim();
             

            }

        }

        #endregion

        #region  Diagnostico

        protected void Cargar_diagnostico()
        {
            dt_diagnostico.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PLAN_DET_DIAGNOSTICO");
            CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            dt_diagnostico.Load(CnnFalp.ExecuteReader());
            grilla_diagnostico.DataSource = dt_diagnostico;
            CnnFalp.Cerrar();

        }


        private void grilla_diagnostico_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_diagnostico.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        #endregion

        #region  Conceptos

        protected void Cargar_Conceptos()
        
        {
             Frm_Seleccion_Cama frm_c = new Frm_Seleccion_Cama();
            Cargar_Parametros_Especificos(ref Ayuda, txtconcepto.Text, "ESTADOS PRESUPUESTO", 4, 0);

            if (!Ayuda.EOF())
            {
                txtconcepto.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtconcepto.Text = Ayuda.Fields(1);

                if (Convert.ToInt32(txtconcepto.Tag) == 1)
                {
                    txtcantidad.Enabled = false; 
                    txtvalor.Enabled = false;
                    btn_añadir_concepto.Enabled = false;      
                    Añadir_dias_camas();
                    CalculoHonorarios();
             
                }
         
            }
        }

        protected void Cargar_Parametros_Especificos_2(ref AyudaSpreadNet.AyudaSprNet Ayuda, string descripcion, string titulo, int tipo, Int64 codigo)
        {

            string[] NomCol = { "Código", "Descripción","Lap" };
            int[] AnchoCol = { 80, 350,60 };
            Ayuda.Nombre_BD_Datos = Conexion[0];
            Ayuda.Pass = Conexion[1];
            Ayuda.User = Conexion[2];
            Ayuda.TipoBase = 1;
            Ayuda.NombreColumnas = NomCol;
            Ayuda.AnchoColumnas = AnchoCol;
            Ayuda.TituloConsulta = "INGRESAR " + titulo.ToUpper() + "";
            Ayuda.Package = PCK;
            Ayuda.Procedimiento = "P_CARGAR_PARAMETRO_ESPECIFICO";
            Ayuda.Generar_ParametroBD("PIN_TIPO", tipo, DbType.Int32, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_CODIGO", codigo, DbType.Int32, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", descripcion.ToUpper(), DbType.String, ParameterDirection.Input);
            Ayuda.EjecutarSql();

        }

        protected void Cargar_Parametros_Especificos(ref AyudaSpreadNet.AyudaSprNet Ayuda, string descripcion, string titulo, int tipo, Int64 codigo)
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
            Ayuda.Procedimiento = "P_CARGAR_PARAMETRO_ESPECIFICO";
            Ayuda.Generar_ParametroBD("PIN_TIPO", tipo, DbType.Int32, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_CODIGO", codigo, DbType.Int32, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", descripcion.ToUpper(), DbType.String, ParameterDirection.Input);
            Ayuda.EjecutarSql();

        }
     
        private void btn_concepto_Click(object sender, EventArgs e)
        {
            txtconcepto.Text = "";
            txtconcepto.Tag = "";
            Cargar_Conceptos();
            if (txtconcepto.Tag != "")
            {
                if (Convert.ToInt64(txtconcepto.Tag) != 1)
                {
                    if (Convert.ToInt64(txtconcepto.Tag) == 3)
                    {
                        Modificar_Valores_HM();
                    }
                    else
                    {
                        txtcantidad.Focus();
                    }
                }
                else
                {

                    Añadir_dias_camas();
                    Limpiar_concepto();
                    txtconcepto.Focus();
                }
            }
            else
            {
                txtconcepto.Focus();
            }
           
        }

        private void txtconcepto_KeyPress(object sender, KeyPressEventArgs e)
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
                    Cargar_Conceptos();

                    if (Convert.ToInt64(txtconcepto.Tag) != 1)
                    {
                        if (Convert.ToInt64(txtconcepto.Tag) == 3)
                        {
                            Modificar_Valores_HM();
                        }
                        else
                        {
                            txtcantidad.Focus();
                        }
                    }
                    else
                    {
                            Añadir_dias_camas();
                            Limpiar_concepto();
                            txtconcepto.Focus();
                    }
                    
                }
            }
        }

        #region Anadir Conceptos

        protected string AgregarConcepto(int cod_concepto, string nom_concepto, string cantidad, string val)
        {
            barra.Visible = true;
            txtconcepto.Tag = cod_concepto;
            string res = "";
            DataRow Fila = dt_presupuesto.NewRow();

            if (Validar_concepto_grilla())
            {
                Fila["COD_CONCEPTO"] = cod_concepto;
                Fila["NOM_CONCEPTO"] = nom_concepto;
                Fila["CANTIDAD"] = cantidad;
                Int64 valor = Convert.ToInt64(val);
                Fila["VALOR"] = valor;
                if (Convert.ToInt32(cod_concepto) == 3)
                    Fila["COD_TIPOHONORARIO"] = "HM";
                else
                    Fila["COD_TIPOHONORARIO"] = "HC";

                Fila["MANUAL"] = 1;
                Fila["VIGENTE"] = "S";
                dt_presupuesto.Rows.Add(Fila);
                grilla_presupuesto.DataSource = new DataView(dt_presupuesto, "VIGENTE ='S'", "COD_CONCEPTO", DataViewRowState.CurrentRows);
            }

            else
            {
                if (cod_concepto == 1)
                {
                    foreach (DataRow miRow1 in dt_presupuesto.Select("COD_CONCEPTO = 1"))
                    {
                        miRow1["CANTIDAD"] = cant_cama;
                        miRow1["VALOR"] = total_cama;
                    }
                }
                dt_presupuesto.AcceptChanges();
            }
            return res;
        }

        #endregion 

        protected void Cargar_Conceptos_Presupuesto()
        {
            dt_presupuesto.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PRESUPUESTOCONCEPTO");
            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            dt_presupuesto.Load(CnnFalp.ExecuteReader());
            grilla_presupuesto.DataSource = dt_presupuesto;

             foreach (DataRow miRow1 in dt_presupuesto.Select("COD_CONCEPTO = 1"))
            {

                if (Convert.ToInt32(miRow1["VALOR"].ToString()) == 0)
                {
                    dt_presupuesto.Rows.Remove(miRow1);
                }
            }

             this.grilla_presupuesto.Columns["EC"].Visible = false;
             this.grilla_presupuesto.Columns["M"].Visible = false;

            CnnFalp.Cerrar();
        }

        #endregion

        #region Historial

        protected void Cargar_Historial()
        {
            dt_historial.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_HIST_PRES_PAC");
            CnnFalp.ParametroBD("PIN_CORRELATIVO", v_correlativo, DbType.Int64, ParameterDirection.Input);
            dt_historial.Load(CnnFalp.ExecuteReader());
            grillaHistorial.DataSource = dt_historial;
            CnnFalp.Cerrar();

        }




        #endregion 

        #region  Prestaciones

        protected void Cargar_Prestaciones()
        {
            dt_prestaciones.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PRESTACIONES");
            CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            dt_prestaciones.Load(CnnFalp.ExecuteReader());
            grilla_prestaciones.DataSource = dt_prestaciones;
            agregarimagen();
            CnnFalp.Cerrar();

        }

        protected void Cargar_Prestaciones_Presupuestos()
        {
            dt_prestaciones.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DETPTO");
            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            dt_prestaciones.Load(CnnFalp.ExecuteReader());
            grilla_prestaciones.DataSource = dt_prestaciones;
            agregarimagen();
            CnnFalp.Cerrar();

        }

        protected void Cargar_Prestaciones_M()
        {
            Int64 cod = txtcodigo.Text.Equals(string.Empty) ? 0 : Convert.ToInt64(txtcodigo.Text);
            Cargar_Parametros_Especificos_2(ref Ayuda, txtdescripcion.Text, "PRESTACIONES", 5, cod);

            if (!Ayuda.EOF())
            {
                txtcodigo.Text = Ayuda.Fields(0);
                txtdescripcion.Text = Ayuda.Fields(1);
                nom_lap = Ayuda.Fields(2);
                guarismo = Ayuda.Fields(3);
                if (nom_lap == "N")
                {
                    cod_lap = 1;
                }
                else
                {
                    cod_lap = 2;
                }
            }
            else
            {
                txtcodigo.Text = "";
                txtdescripcion.Text ="";
            }
        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    Cargar_Prestaciones_M();
                    btn_codigo_fonasa.Focus();
                }
            }
        }

        private void txtdescripcion_KeyPress(object sender, KeyPressEventArgs e)
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
                    Cargar_Prestaciones_M();
                    btn_codigo_fonasa.Focus();
                }
            }
        }

        private void grilla_prestaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int cod_fonasa = Convert.ToInt32(grilla_prestaciones.Rows[e.RowIndex].Cells["COD_FONASA"].Value.ToString());
                int cod_plan_det_id = Convert.ToInt32(grilla_prestaciones.Rows[e.RowIndex].Cells["COD_PLAN_DET"].Value.ToString());
                if (e.ColumnIndex == 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Esta  Seguro de Eliminar la Prestación " + cod_fonasa + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                        if (cod_plan_det_id == 0)
                        {
                            grilla_prestaciones.Rows.RemoveAt(grilla_prestaciones.CurrentRow.Index);
                            dt_prestaciones.AcceptChanges();
                        }

                        else
                        {
                            foreach (DataRow miRow1 in dt_prestaciones.Select(" COD_FONASA ='" + Convert.ToInt64(cod_fonasa) + "'"))
                            {
                                string v = "";
                                v=miRow1["VIGENCIA"].ToString();
                                if(v=="S")
                                {
                                miRow1["VIGENCIA"] = "N";
                                }

                                else{
                                    miRow1["VIGENCIA"] = "S";
                                }
                            }
                        }
                    }

                    Mensaje("", 4);
                    grilla_prestaciones.DataSource = new DataView(dt_prestaciones, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
                }
                else
                {
                    if (e.ColumnIndex == 1)
                    {
                        foreach (DataRow miRow1 in dt_prestaciones.Select(" COD_FONASA ='" + Convert.ToInt64(cod_fonasa) + "'"))
                        {
                           string v = miRow1["SELECCION"].ToString();
                            if (v == "S")
                            {
                                miRow1["SELECCION"] = "N";
                            }

                            else
                            {
                                miRow1["SELECCION"] = "S";
                            }
                        
                        }
                        dt_prestaciones.AcceptChanges();
                        agregarimagen();
                    }


                }

            }
        }


        protected Int32 Extraer_estado(Int64 cod_presupuesto)
        {
            Int32 valor = 0;
            DataTable dt = new DataTable();
            dt.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_VALIDAR_ESTADO_FAI");
            CnnFalp.ParametroBD("PIN_COD_PRESUPUESTO", cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            dt.Load(CnnFalp.ExecuteReader());
            string v = "";
            foreach (DataRow miRow1 in dt.Rows)
            {
                v = miRow1["COD_ESTADO"].ToString();
            }

            if (v == "")
            {
                valor = 0;
            }
            else
            {
                valor = Convert.ToInt32(v);
            }
            return valor;

        }

        private void grilla_prestaciones_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_prestaciones.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }


        #endregion

        #region Tipo Honorario

        protected void Cargar_Tipo_Honorario()
        {
            Cargar_Parametros(ref Ayuda, 6, txtHonorario.Text, "TIPO HONORARIO");

            if (!Ayuda.EOF())
            {
                txtHonorario.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtHonorario.Text = Ayuda.Fields(1);

              
            }
        }

        private void btn_honorario_Click(object sender, EventArgs e)
        {
            txtHonorario.Text = "";
            txtHonorario.Tag = "";
            Cargar_Tipo_Honorario();
        }

        private void txtHonorario_KeyPress(object sender, KeyPressEventArgs e)
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
                    Cargar_Tipo_Honorario();
                }
            }
        }

        #endregion 

        #region Tipo Riesgo

        protected void Cargar_Tipo_Riesgo()
        {
            Cargar_Parametros(ref Ayuda, 94, txtriesgo_clinico.Text, "TIPO RIESGO");

            if (!Ayuda.EOF())
            {
                txtriesgo_clinico.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtriesgo_clinico.Text = Ayuda.Fields(1);
            }
        }

        private void btn_riesgo_clinico_Click(object sender, EventArgs e)
        {
            txtriesgo_clinico.Text = "";
            txtriesgo_clinico.Tag = "";
            Cargar_Tipo_Riesgo();
        }

        private void txtriesgo_clinico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    Cargar_Tipo_Riesgo();
                }
            }
        }

        #endregion 

        #region  Parametros Generales


        protected void Cargar_Parametros(ref AyudaSpreadNet.AyudaSprNet Ayuda, int num, string descripcion, string titulo)
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

        #region Anadir Prestaciones

        protected string AgregarPrestaciones()
        {
            string res = "";
            try
            {
                DataRow fila = dt_prestaciones.NewRow();
                fila["COD_PLAN_DET"] = 0;
                fila["COD_FONASA"] = txtcodigo.Text;
                fila["NOM_FONASA"] = txtdescripcion.Text;
                fila["COD_LAPAROSCOPIA"] = cod_lap;
                fila["NOM_LAPAROSCOPIA"] = nom_lap;
                fila["GUARISMO"] = guarismo;
                Cargar_Parametros(ref Ayuda,61, "", "TIPO TECNICA");
                
                
                if (!Ayuda.EOF())
                {
                    fila["COD_TIPO_TECNICA"] = Ayuda.Fields(0);
                    fila["NOM_TIPO_TECNICA"] = Ayuda.Fields(1);
                }
                Cargar_Parametros_Especificos(ref Ayuda, "", " LATERALIDAD", 6, 0);
                if (!Ayuda.EOF())
                {

                    fila["COD_LATERALIDAD"] = Ayuda.Fields(0);
                    fila["NOM_LATERALIDAD"] = Ayuda.Fields(1);
                }
                else
                {
                    fila["COD_LATERALIDAD"] = 5;
                    MessageBox.Show("Estimado Usuario, No ha seleccionado la Lateralidad", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                fila["SELECCION"] = "S";
               
                fila["V_MANUAL"] = 1;
                fila["VIGENCIA"] = "S";

                dt_prestaciones.Rows.Add(fila);
                dt_prestaciones.AcceptChanges();
                agregarimagen();
               Mensaje("", 5);
               grilla_prestaciones.DataSource = new DataView(dt_prestaciones, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }


        #endregion 

       
        #region Limpiar

        #region Limpiar Prestacion

        protected void Limpiar_prestacion()
        {
            txtcodigo.Text = "";
            txtdescripcion.Text = "";
            cod_lap = 0;
            nom_lap = "";
            guarismo = "";
        }
        #endregion

        #region Limpiar Concepto

        protected void Limpiar_concepto()
        {
            txtconcepto.Text = "";
            txtconcepto.Tag = "";
            txtcantidad.Text = "";
            txtvalor.Text = "";
            cod_lap = 0;
            nom_lap = "";
            txtcantidad.Enabled = true;
            txtvalor.Enabled = true;
            btn_añadir_concepto.Enabled = true;
            btn_añadir_concepto.Visible = true;
            btn_confirmar.Visible = false;
        }
        #endregion

        #endregion

        #region  Calcular

        public void agregarimagen_concepto()
        {
            foreach (DataGridViewRow row in grilla_presupuesto.Rows)
            {
                Int64 cod_concepto = Convert.ToInt64(row.Cells["COD_CONCEPTO"].Value);
                DataGridViewImageCell Imagen = row.Cells["V"] as DataGridViewImageCell;

                if (cod_concepto == 10 || cod_concepto == 11 || cod_concepto == 17)
                {
                    Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.botiquin;
                }
                else
                {
                    if (cod_concepto == 1)
                    {
                        Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.camas_2;
                    }
                    else
                    {
                        if (cod_concepto == 3)
                        {
                            Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.dinero;
                        }
                        else
                        {
                            Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.lista;
                        }
                    }
                }
            }
        }


        protected void Buscar_Folios(string prestaciones)
        {
            timer1.Stop();
            DialogResult res = new DialogResult();
        
            Cargar_Folios(prestaciones,12);
            if (!dt_folios.Equals(0))
            {
                if (dt_folios.Rows.Count > 0 && dt_folios.Rows.Count <= 10)
                {
                    if (Super_usuario == "")
                    {
                        Mensaje("", 7);
                    }
                    else
                    {
                        MsgBoxUtil.HackMessageBox("Historial", "Lo encontrado", "Cancelar");
                        res = MessageBox.Show("Estimado Usuario, se encontraron " + dt_folios.Rows.Count + " folio(s) con dicha prestación el los ultimos 12 meses ¿Donde desea buscar?", "Informacion",
                              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                if (Super_usuario == "")
                {
                    Mensaje("", 7);
                }
                else
                {       
                    MsgBoxUtil.HackMessageBox("Historial", "Lo encontrado", "Cancelar");
                    res = MessageBox.Show("Estimado Usuario, Se encontraron " + dt_folios.Rows.Count  + " folio(s) con dicha prestación el los ultimos 12 meses ¿Donde desea buscar?", "Informacion",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                }
              
            }


            if (DialogResult.Cancel == res)
            {
                return;
            }

            if (DialogResult.Yes == res)
            {
                Cargar_Folios(prestaciones, 24);
            }

            if(Validar_dt(dt_folios))
            {
                Mensaje("Folios", 6);
            }
            timer1.Start();
        }

        protected void Cargar_Folios(string prestaciones,int tipo)
        {
            
            dt_folios.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_FOLIOS");
            CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CODIGO", prestaciones, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPOPREVISION", txtprevicion.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PREVISION",txtinstitucion.Tag , DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PLAN_PREVISION", txtplan_previsional.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_SIGMA", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PROMEDIO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_EXCLUIR", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_LIMITE", tipo, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CALCULO", 0, DbType.Int64, ParameterDirection.Input);


            dt_folios.Load(CnnFalp.ExecuteReader());
            if (Validar_dt(dt_folios))
            {
                dt_folios.Clear();
            }
            int cont = dt_folios.Rows.Count;
            CnnFalp.Cerrar();

        }

        protected string Concatenar_prestaciones()
        {
            timer1.Stop();
            string cad = "";
            int contador=0;
            foreach (DataRow miRow1 in dt_prestaciones.Select("SELECCION = 'S'"))
            {
                  contador++;

                    if (contador == 1)
                    {
                        cad = miRow1["COD_FONASA"].ToString().Trim();
                        cad = cad.Trim() + miRow1["COD_TIPO_TECNICA"].ToString();
                        if (miRow1["COD_LATERALIDAD"].ToString() == "3")
                        {
                            cad = cad.Trim() + "-" + miRow1["COD_FONASA"].ToString();
                            cad = cad.Trim() + miRow1["COD_TIPO_TECNICA"].ToString();
                        }
                    }
                    else
                    {
                        cad = cad.Trim() + "-" + miRow1["COD_FONASA"].ToString().Trim();
                        cad = cad.Trim() + miRow1["COD_TIPO_TECNICA"].ToString();
                        if (miRow1["COD_LATERALIDAD"].ToString() == "3")
                        {
                            cad = cad.Trim() + "-" + miRow1["COD_FONASA"].ToString();
                            cad = cad.Trim() + miRow1["COD_TIPO_TECNICA"].ToString();
                        }
                    }
            }

            if (contador >= 1)
            {
                return cad;
            }
            else
            {
               
                return string.Empty;
            }
            timer1.Start();

        }

     /*  protected void Buscar_Folios()
        {
           
            dt_presupuesto.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_BUSCARVALFOLIOS");
            CnnFalp.ParametroBD("PIN_FOLIO", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_NUMERO", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PREVISION", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PLAN_PREVISION", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_SIGMA", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PROMEDIO", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_EXCLUIR", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_FOLIOS", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CANTIDAD", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_OPCION", v_id_plan, DbType.Int64, ParameterDirection.Input);
            dt_presupuesto.Load(CnnFalp.ExecuteReader());
            grilla_diagnostico.DataSource = dt_presupuesto;
            CnnFalp.Cerrar();

        }*/

        protected void Mostrar_Conceptos(string prestaciones)
        {
            timer1.Stop();
            int cantidadFolios=0;
          
                //folios y cantidad
            if (!dt_folios.Equals(0))
            {
                DataView dv = new DataView(dt_folios);
                if (Excluir == 1) dv.RowFilter = "EXTREMO= 1";//_Cod_estado = 1";

                foreach (DataRow item in dv.Table.Rows)
                {
                    if (Excluir == 1)
                    {
                        if (item["EXTREMO"].ToString().Equals("1"))
                        {
                            if (cantidadFolios.Equals(0))
                                folios = item["FOLIO"].ToString();
                            else
                                folios = folios + "," + item["FOLIO"].ToString();

                            cantidadFolios++;
                        }
                    }
                    else
                    {
                        if (cantidadFolios.Equals(0))
                            folios = item["FOLIO"].ToString();
                        else
                            folios = folios + "," + item["FOLIO"].ToString();

                        cantidadFolios++;
                    }
                }
            }
            else
            {
                cantidadFolios = 0;
                folios = "0";
            }

                 Extraer_Conceptos(prestaciones ,cantidadFolios, folios);
                 timer1.Start();      
        }

        protected void Extraer_Conceptos(string prestaciones,int cantidad,string folios)
        {
            timer1.Stop();
            string foli = folios.Equals(string.Empty) ? "0" : folios;
            dt_presupuesto.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CALCULOFINAL");
            CnnFalp.ParametroBD("PIN_CODIGO", prestaciones, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPO_PREVISION", txtprevicion.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_INSTITUCION", txtinstitucion.Tag , DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_PLANPREVISION", txtplan_previsional.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_TIPOCALCULO", 1, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_ID", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_UO_NUMERO", uo_numero, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_SIGMA", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PROMEDIO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_EXCLUIR", Excluir, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_FOLIOS", foli, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CANTIDAD", cantidad, DbType.Int64, ParameterDirection.Input);
            dt_presupuesto.Load(CnnFalp.ExecuteReader());

            if (Validar_dt(dt_presupuesto))
            {
                dt_presupuesto.Clear();
            }

            string cod = "";
            foreach (DataRow miRow1 in dt_presupuesto.Rows)
            {
                if (miRow1["VALOR"].ToString()=="0")
                {
                    miRow1["VIGENTE"] = "N";
             
                }

                if (miRow1["COD_CONCEPTO"].ToString() == "10")
                {
                   miRow1["VALOR"]= Cargar_grilla_IM(10);
                }
                if (miRow1["COD_CONCEPTO"].ToString() == "11")
                {
                   miRow1["VALOR"]= Cargar_grilla_IM(11);
                }
                if (miRow1["COD_CONCEPTO"].ToString() == "17")
                {

                     miRow1["VALOR"]= Cargar_grilla_IM(17);
                   
                }

            }
            dt_presupuesto.AcceptChanges();
            grilla_presupuesto.DataSource = new DataView(dt_presupuesto, "VIGENTE ='S'", "COD_CONCEPTO", DataViewRowState.CurrentRows);
            agregarimagen_concepto();
            timer1.Start();
            CnnFalp.Cerrar();
        }


        public string Cargar_grilla_IM(Int64 cod)
        {
            string valor = "";
            DataTable dt = new DataTable();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            //    CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DET_IM");
            //   CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_FAR_INS_DROG");
            CnnFalp.ParametroBD("PIN_FOLIOS", folios, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_CONCEPTO", cod, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_DESCRIPCION", "", DbType.String, ParameterDirection.Input);
            dt.Load(CnnFalp.ExecuteReader());

            
             return valor = calcular_insumos(dt).ToString();
            
        }

        public Int64  calcular_insumos(DataTable dt)
        {
            Int64 valor = 0;
            foreach (DataRow miRow1 in dt.Rows)
            {
                valor = valor + Convert.ToInt64(miRow1["VALOR"].ToString().Replace(".", ""));
            }
            return valor;
        }

        private void grilla_presupuesto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Limpiar_concepto();
            if (e.RowIndex >= 0)
            {
                Int64 cod_concepto = Convert.ToInt32(grilla_presupuesto.Rows[e.RowIndex].Cells["COD_CONCEPTO"].Value.ToString());
                string nom_concepto = grilla_presupuesto.Rows[e.RowIndex].Cells["NOM_CONCEPTO"].Value.ToString();
                int manual = Convert.ToInt32(grilla_presupuesto.Rows[e.RowIndex].Cells["MANUAL"].Value.ToString());
                int cantidad = Convert.ToInt32(grilla_presupuesto.Rows[e.RowIndex].Cells["CANTIDAD"].Value.ToString());
                int total = Convert.ToInt32(grilla_presupuesto.Rows[e.RowIndex].Cells["VALOR"].Value.ToString());
                string v = grilla_presupuesto.Rows[e.RowIndex].Cells["VIGENTE"].Value.ToString();
                if (e.ColumnIndex == 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Esta  Seguro de Eliminar la Presupuesto " + nom_concepto + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                        Frm_Seleccion_Cama frm_c = new Frm_Seleccion_Cama();
                        if (manual == 1)
                        {
                            grilla_presupuesto.Rows.RemoveAt(grilla_presupuesto.CurrentRow.Index);
                            dt_presupuesto.AcceptChanges();

                            if (Convert.ToInt64(cod_concepto) == 1)
                            {
                                frm_c.dt_cama.Clear();
                                frm_c.dt_totales.Clear();
                            }
                        }

                        else
                        {
                            foreach (DataRow miRow1 in dt_presupuesto.Select(" COD_CONCEPTO ='" + Convert.ToInt64(cod_concepto) + "'"))
                            {
                                miRow1["VIGENTE"] = "N";
                            }

                            if (Convert.ToInt64(cod_concepto) == 1)
                            {
                                frm_c.dt_cama.Clear();
                                frm_c.dt_totales.Clear();
                            }
                        }
                    }

                    Mensaje("", 4);
                   
                }
                else
                {
                    if (e.ColumnIndex == 1)
                    {
                        DialogResult opc = MessageBox.Show("Estimado Usuario, Esta  Seguro de Modificar los Valores del Concepto " + nom_concepto + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (opc == DialogResult.Yes)
                        {

                            if (cod_concepto == 1)
                            {
                                Calcular_dias_camas();
                                if(v_cod_presupuesto>0)
                                {
                                  btn_guardar.Visible = true;
                                }
                            }
                            else
                            {
                                if (cod_concepto == 3)
                                {
                                    Modificar_Valores_HM();
                                }
                                else
                                {
                                    
                                    btn_añadir_concepto.Visible = false;
                                    btn_confirmar.Visible = true;
                                    txtconcepto.Tag = cod_concepto;
                                    txtconcepto.Text = nom_concepto;
                                    txtcantidad.Text = cantidad.ToString();
                                    txtvalor.Text = total.ToString();
                                    txtvalor.Enabled = true;
                                    txtcantidad.Enabled = true;
                                }
                            }
                            
                        }

                    }

                    else
                    {
                        if (e.ColumnIndex == 2)
                        {
                            if (cod_concepto == 1)
                            {
                                Calcular_dias_camas();
                                if (v_cod_presupuesto > 0)
                                {
                                    btn_guardar.Visible = true;
                                }
                            }
                            else
                            {

                                if (cod_concepto == 3)
                                {
                                    Modificar_Valores_HM();
                                }

                                else
                                {
                                    if (cod_concepto == 10 || cod_concepto == 11 || cod_concepto == 17)
                                    {
                                        Frm_Seleccion_IMP frm_imp = new Frm_Seleccion_IMP();
                                        frm_imp.Inicializar_datos(v_id_plan, v_cod_plan, Convert.ToInt64(txtinstitucion.Tag), Convert.ToInt64(txtprevicion.Tag), Convert.ToInt64(txtplan_previsional.Tag), cod_concepto, folios);
                                        frm_imp.ShowDialog();
                                    }
                                }

                            }
                        }
                    }
                }

            }
            grilla_presupuesto.DataSource = new DataView(dt_presupuesto, "VIGENTE ='S'", "COD_CONCEPTO", DataViewRowState.CurrentRows);
            agregarimagen_concepto();
            CalculoHonorarios();
            barra.Visible = false;
        }

        private void Calcular_dias_camas()
        {
            txtcantidad.Enabled = false;
            txtvalor.Enabled = false;
            btn_añadir_concepto.Enabled = false;
            string nom = txtnombres.Text + " " + txtpaterno.Text + " " + txtmaterno.Text;
            Frm_Seleccion_Cama frm_c = new Frm_Seleccion_Cama(dt_camas, dt_totales, selec_cama, cant_cama, total_cama, v_id_plan, v_cod_plan, v_cod_presupuesto, nom, Convert.ToInt64(txtinstitucion.Tag), Convert.ToInt64(txtprevicion.Tag), Convert.ToInt64(txtplan_previsional.Tag));
            frm_c.ShowDialog();
            dt_camas = frm_c.dt_cama;
            selec_cama = frm_c.v_selec_cama;
            cant_cama = frm_c.v_cant_cama;
            total_cama = frm_c.v_total_cama;
            if (selec_cama > 0)
            {
                string res = AgregarConcepto(1, "DÍAS CAMA", cant_cama.ToString(), total_cama.ToString());

                if (res == "ok")
                {
                    Mensaje("DÍAS CAMA", 8);

                }

            }
            CalculoHonorarios();
            

        }


        private void CalculoHonorarios()
        {
            try
            {
               
               Int64 v_hc = 0;
               Int64 v_hm = 0;
               foreach (DataRow miRow1 in dt_presupuesto.Select("VIGENTE='S'"))
                {
                    //string tp = row.Cells["Tipo Honorario"].Value.ToString();
                    if (miRow1["COD_CONCEPTO"].ToString() != "3")
                    {
                        v_hc = v_hc + Convert.ToInt64(miRow1["VALOR"].ToString());  
                    }
                    else
                    {
                        v_hm = v_hm + Convert.ToInt64(miRow1["VALOR"].ToString()); 
                    }
                }

                 txthc.Text=v_hc.ToString("#,##0");
                 txthm.Text= v_hm.ToString("#,##0");
                txttotal.Text = (Convert.ToInt32(txthc.Text.Replace(".", "")) + Convert.ToInt32(txthm.Text.Replace(".", ""))).ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Añadir_dias_camas()
        {
            timer1.Stop();
            Frm_Seleccion_Cama frm_c = new Frm_Seleccion_Cama();
            string nom = txtnombres.Text + " " + txtpaterno.Text + " " + txtmaterno.Text;
            frm_c.Inicializar_datos(v_id_plan, v_cod_plan, v_cod_presupuesto, Convert.ToInt64(txtinstitucion.Tag), Convert.ToInt64(txtprevicion.Tag), Convert.ToInt64(txtplan_previsional.Tag), nom, dt_camas,dt_totales);
            frm_c.ShowDialog();
            dt_camas = frm_c.dt_cama;
            dt_totales = frm_c.dt_totales;
            selec_cama = frm_c.v_selec_cama;
            cant_cama = frm_c.v_cant_cama;
            total_cama = frm_c.v_total_cama;

           if(selec_cama>0)
           {
              string res= AgregarConcepto(1, "DIAS CAMA", cant_cama.ToString(), total_cama.ToString());

              if (res == "ok")
               {
                   Mensaje("DIAS CAMA", 8);                 
               }          
           }

           timer1.Start();
        }

        #endregion 

        #region Guardar Presupuesto

        #region Presupuesto

        private string  Guardar_Presupuesto(int cant)
        {
            Int64 v_cod_presupuesto_det = 0;
            Int64 v_cod_concepto = 0;
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            string res = "";
            try
            {
              CnnFalp.IniciarTransaccion();

              v_cod_presupuesto = Crear_Presupuesto();
              v_cod_fai = Crear_Fai();
              v_cod_presupuesto_det = Crear_Presupuesto_Det(v_cod_presupuesto.ToString());
              v_cod_concepto = Crear_Conceptos(v_cod_presupuesto.ToString(), v_cod_presupuesto_det, cant);
              Crear_Diagnostico_Fai(v_cod_presupuesto.ToString());

              CnnFalp.ConfirmarTransaccion();
              return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CnnFalp.ReversarTransaccion();
                return "0";
            }

            btn_guardar.Enabled = false;
        }

        protected Int64 Crear_Presupuesto()
        {
            Int64 val = 0;

            if (txtHonorario.Tag == null)
            {
                txtHonorario.Tag = 0;
            }

            if (txtriesgo_clinico.Tag == null)
            {
                txtriesgo_clinico.Tag = 0;
            }

            if (txt_conv_onco.Text == "NO")
            {
                txt_conv_onco.Text = "0";
            }
            else{
                txt_conv_onco.Text="1";
            }

            
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_Grabar_Presupuesto");

            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_FECHA_CONCRETADOS", string.Empty, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_FECHA_ANULACION", string.Empty, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_USUARIO", usuario, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_PAC_CORRELATIVO", v_correlativo, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_PLAN_PREV", txtplan_previsional.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_CONV_PREV", txtprevicion.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_CONV_ONCOLOGICO", txt_conv_onco.Text, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_INS_RUT",txtinstitucion.Tag  , DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_PAQUETE", txtpos_paquete.Text.Equals("NO")?"N":"S", DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TBL_ID", txtpaquete.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TOTAL_HC", txthc.Text.Replace(".",""), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TOTAL_HM", txthm.Text.Replace(".",""), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_MOTIVO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_UCLINICA", txtuni_clinica.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_MEDICO", txtmedico.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TIPOHONORARIO", txtHonorario.Tag.Equals(string.Empty) ? 0 : Convert.ToInt64(txtHonorario.Tag), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_RIESGO", txtriesgo_clinico.Tag.Equals(string.Empty) ? 0 : Convert.ToInt64(txtriesgo_clinico.Tag), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("POUT_NUM_PRE", 0, DbType.Int64, ParameterDirection.Output);

            int registro = CnnFalp.ExecuteNonQuery();


          val= Convert.ToInt64(CnnFalp.ParamValue("POUT_NUM_PRE"));


            return val;
        }

        protected Int64 Crear_Presupuesto_Det(string v_cod_presupuesto)
        {
            Int64 val = 0;

            foreach (DataRow miRow in dt_prestaciones.Rows)
            {
                int id_Detalle = 0;

                string cod_fonasa = miRow["COD_FONASA"].ToString();
                int tipo_tecnica = Convert.ToInt32(miRow["COD_TIPO_TECNICA"]);
                int lateralidad = miRow["COD_LATERALIDAD"].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(miRow["COD_LATERALIDAD"]);
                string manual = miRow["V_MANUAL"].ToString();
                string vigente = miRow["VIGENCIA"].ToString();


                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_Grabar_Det_Presupuesto");

                CnnFalp.ParametroBD("PIN_DPRES_ID", 0, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_CODIGO_FONASA", cod_fonasa, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_TIPO_TECNICA", tipo_tecnica, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_LATERALIDAD", lateralidad, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_MANUAL", manual, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DPRES_VIGENTE", vigente, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("POUT_NUM_DET_DPRES", 0, DbType.Int64, ParameterDirection.Output);

                int registro = CnnFalp.ExecuteNonQuery();


                val = Convert.ToInt64(CnnFalp.ParamValue("POUT_NUM_DET_DPRES"));

               Crear_procedimientos_Fai(v_cod_presupuesto, cod_fonasa, lateralidad, vigente);
            }


            return val;

        }


        protected Int64 Crear_Conceptos(string v_cod_presupuesto,Int64 v_cod_presupuesto_det, int cant)
        {
            Int64 id_concepto = 0;

            try
            {            
                foreach (DataRow miRow in dt_presupuesto.Rows)
                {
                    id_concepto = 0;
                    int cod_concepto = Convert.ToInt32(miRow["COD_CONCEPTO"]);
                    int cantidad = Convert.ToInt32(miRow["CANTIDAD"]);
                    int precio = Convert.ToInt32(miRow["VALOR"]);
                    int preciodiascama = 0;
                    string honorario = miRow["COD_TIPOHONORARIO"].ToString();
                    string manual = miRow["manual"].ToString();
                    string vigente = miRow["VIGENTE"].ToString();

                    CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_Grabar_Concepto");

                    CnnFalp.ParametroBD("PIN_CONC_ID", 0, DbType.Int64, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_DPRES_ID", v_cod_presupuesto_det, DbType.Int64, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_CONC_CODIGO", cod_concepto, DbType.Int64, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_CANTIDAD", cantidad, DbType.Int64, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_VALOR", precio, DbType.Int64, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_MANUAL", manual, DbType.String, ParameterDirection.Input);
                    CnnFalp.ParametroBD("PIN_CONC_VIGENTE", vigente, DbType.String, ParameterDirection.Input);
                    CnnFalp.ParametroBD("POUT_NUM_CONC", 0, DbType.Int64, ParameterDirection.Output);

                    int registro = CnnFalp.ExecuteNonQuery();
                    id_concepto= Convert.ToInt64(CnnFalp.ParamValue("POUT_NUM_CONC").ToString());
                  
                  if (cod_concepto == 1)
                    {
                        Guardar_Prestacion_Dia_Cama(v_cod_presupuesto, v_cod_presupuesto_det, id_concepto, cod_concepto, cant);
                    }
                  if (cod_concepto == 3)
                  {
                      Guardar_hm(v_cod_presupuesto, v_cod_presupuesto_det, id_concepto, cod_concepto);
                  }
                  if (cod_concepto == 11 || cod_concepto == 10 || cod_concepto == 17)
                     {
                        Guardar_Prestacion_Med_Ins(v_cod_presupuesto, id_concepto, cod_concepto);
                     }

                  if (dt_prestadi_total_p.Rows.Count > 0)
                    {
                      Guardar_Pestaciones_Adicionales(v_cod_presupuesto, v_cod_presupuesto_det, id_concepto, cod_concepto);
                   }
                }
                return id_concepto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }


        private void Guardar_hm(string v_cod_presupuesto,Int64 v_cod_presupuesto_det, Int64 id_concepto, Int64 cod_concepto)
        {
            
            try
            {
                if (dt_hm.Rows.Count > 0)
                {
                    foreach (DataRow miRow in dt_hm.Rows)
                    {
                        CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GUARDAR_HM");

                        CnnFalp.ParametroBD("PIN_HM_CONC_ID", Convert.ToInt64(id_concepto), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_PRES_ID", v_cod_presupuesto, DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_DPRES_ID", Convert.ToInt64(v_cod_presupuesto_det), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_CODIGO", Convert.ToInt64(miRow["COD_FONASA"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_FACTOR", miRow["FACTOR"].ToString(), DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_CIRUJANO1", Convert.ToInt64(miRow["CIRUJANO1_FACT"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_CIRUJANO2", Convert.ToInt64(miRow["CIRUJANO2_FACT"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_CIRUJANO3", Convert.ToInt64(miRow["CIRUJANO3_FACT"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_CIRUJANO4", Convert.ToInt64(miRow["CIRUJANO4_FACT"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_ANESTESISTA", Convert.ToInt64(miRow["ANESTESISTA_FACT"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_TOTAL", Convert.ToInt64(miRow["VALOR_HHMM"].ToString()), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_PORCENTAJE", miRow["PORCENTAJE"].ToString(), DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_HM_USER_CREA", usuario, DbType.String, ParameterDirection.Input);
                        int res = CnnFalp.ExecuteNonQuery();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Guardar_Prestacion_Dia_Cama(string v_cod_presupuesto, Int64 v_cod_presupuesto_det, Int64 id_concepto, Int64 cod_concepto, int tipo)
        {
            try
            {
                string codigo_prestacion = string.Empty;
                string  cod_dia_cama = string.Empty;
                string tipo_cama = string.Empty;
                string descripcion = string.Empty;
                string cantidad = string.Empty;
                string valor = string.Empty;
                string principal = string.Empty; 
                if (dt_camas.Rows.Count > 0)
                {
                    if (dt_totales.Rows.Count == 1)
                    {
                        foreach (DataRow miRow in dt_camas.Rows)
                        {
                            cod_dia_cama = miRow["CODIGO"].ToString();
                            tipo_cama = miRow["COD_TIPO_CAMA"].ToString();
                            codigo_prestacion = miRow["COD_PRESTACION"].ToString();
                            descripcion = miRow["DESCRIPCION"].ToString();
                            cantidad = miRow["CANTIDAD"].ToString();
                            valor = miRow["PRECIO"].ToString();
                            principal = Valida_Principal(tipo_cama);
                            Guardar_Dia_Cama(id_concepto, v_cod_presupuesto, v_cod_presupuesto_det, codigo_prestacion, cantidad, valor, descripcion, tipo_cama, principal,cod_dia_cama);
                        } 
                    }
                    else
                    {
                       
                            foreach (DataRow miRow in dt_camas.Rows)
                            {
                                cod_dia_cama = miRow["CODIGO"].ToString();
                                tipo_cama = miRow["COD_TIPO_CAMA"].ToString();
                                codigo_prestacion = miRow["COD_PRESTACION"].ToString();
                                descripcion = miRow["DESCRIPCION"].ToString();
                                cantidad = miRow["CANTIDAD"].ToString();
                                valor = miRow["PRECIO"].ToString();
                                principal = Valida_Principal(tipo_cama);
                                Guardar_Dia_Cama(id_concepto, v_cod_presupuesto, v_cod_presupuesto_det, codigo_prestacion, cantidad, valor, descripcion, tipo_cama, principal, cod_dia_cama);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private string Valida_Principal(string tipo_cama)
        {
            string res = "0";

            foreach (DataRow miRow in dt_totales.Select("T_COD_TIPO_CAMA='" + tipo_cama + "' and  T_SELECCION='S'"))
            {
                res = "1";
            }

            return res;

        }

        private void Guardar_Dia_Cama(Int64 id_concepto, string v_cod_presupuesto, Int64 v_cod_presupuesto_det, string codigo_prestacion, string cantidad, string valor, string descripcion, string categoria, string principal,string cod_dia_cama)
        {
                    try
                    {
                        CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK +".P_MODIFICAR_PRES_ADICIONAL");

                        CnnFalp.ParametroBD("PIN_PREST_ID", 0, DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_CONC_ID", Convert.ToInt64(id_concepto), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_PRES_ID", v_cod_presupuesto, DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_DPRES_ID", Convert.ToInt64(v_cod_presupuesto_det), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_PREST_CODIGO", Convert.ToInt64(codigo_prestacion), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_CANTIDAD", Convert.ToInt64(cantidad), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_VALOR", Convert.ToInt64(valor), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_MANUAL", "1", DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_VIGENTE", "S", DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_DESRIPCION", descripcion, DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_TIPO", "1", DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_CATEGORIA", Convert.ToInt64(categoria), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_PREST_PRINCIPAL", Convert.ToInt64(principal), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_DIA_CAMA", Convert.ToInt64(cod_dia_cama), DbType.Int64, ParameterDirection.Input);

 
                        int res = CnnFalp.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
        }

        private void Guardar_Prestacion_Med_Ins(string v_cod_presupuesto, Int64 id_concepto, Int64 cod_concepto)
        {
            try
            {
                if (dt_medyins_total_p.Rows.Count>0)
                {
                    foreach (DataRow miRow in dt_medyins_total_p.Rows)
                    {

                        string codigo_prestacion = Convert.ToString(cod_concepto);
                        string codigo = miRow["CODIGO"].ToString();
                        string descripcion = miRow["DESCRIPCION"].ToString();
                        string cantidad = miRow["CANTIDAD"].ToString();
                        string valor = miRow["VALOR"].ToString();
                        string manual = "1";
                        string vigente = "S";
                        Guardar_Medicamentos_Adicionales(id_concepto,v_cod_presupuesto,codigo,cantidad,valor,manual,vigente);       
                    }
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);          
            }
        }

       protected  void  Guardar_Medicamentos_Adicionales(Int64 id_concepto,string v_cod_presupuesto,string codigo,string cantidad,string valor,string manual,string vigente)
       {
            try
            {
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK +".P_MODIFICAR_PRES_IM");
                CnnFalp.ParametroBD("PIN_CDIM_ID", 0, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_CONC_ID", id_concepto, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_CODIGO", codigo, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_CANTIDAD", cantidad, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_VALOR", valor, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_MANUAL", manual, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CDIM_VIGENTE", vigente, DbType.String, ParameterDirection.Input);
                int res = CnnFalp.ExecuteNonQuery();
            }
               
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       }

       private  void  Guardar_Pestaciones_Adicionales(string v_cod_presupuesto, Int64 v_cod_presupuesto_det,Int64 id_concepto,Int64 cod_concepto)
       {

            try
            {
                foreach (DataRow miRow in dt_prestadi_total_p.Rows)
                {
                    int cod_prestacion = Convert.ToInt32(miRow["CODIGO"].ToString());

                    if (cod_prestacion == cod_concepto)
                    {
                        string codigo_concepto = Convert.ToString(cod_concepto);
                        string codigo_prestacion = miRow["CODIGO"].ToString();
                        string descripcion = miRow["DESCRIPCION"].ToString();
                        string cantidad = miRow["CANTIDAD"].ToString();
                        string valor = miRow["VALOR"].ToString();
                        string manual = "0";
                        string vigente = "S";
                        string tipo_prestacion = "0";
                        Guardar_Prestacion_Adi(id_concepto, v_cod_presupuesto, v_cod_presupuesto_det, codigo_prestacion, cantidad, valor,  manual, descripcion, tipo_prestacion);
                    }
                }
          
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);              
            }
       }
   
        protected void Guardar_Prestacion_Adi(Int64 id_concepto, string v_cod_presupuesto, Int64 v_cod_presupuesto_det,string codigo_prestacion,string cantidad,string valor,string manual,string descripcion,string tipo_prestacion)
        {
            try
            {
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK+".P_MODIFICAR_PRES_ADICIONAL");

                CnnFalp.ParametroBD("PIN_PREST_ID", 0, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_CONC_ID", id_concepto, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_DPRES_ID", v_cod_presupuesto_det, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_PREST_CODIGO", codigo_prestacion, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_CANTIDAD", cantidad, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_VALOR", valor, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_MANUAL", manual, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_VIGENTE", "S", DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_DESRIPCION", descripcion, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_TIPO", tipo_prestacion, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_CATEGORIA", 0, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREST_PRINCIPAL", 0, DbType.String, ParameterDirection.Input);
                int res = CnnFalp.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Fai
        protected Int64 Crear_Fai()
        {
            Int64 val = 0;

            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GRABA_FAI");

            CnnFalp.ParametroBD("PIN_PRES_PAC_CORRELATIVO", v_correlativo, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_UCLINICA", txtuni_clinica.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TOTAL_HM", txthm.Text.Replace(".", ""), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TOTAL_HC", txthc.Text.Replace(".", ""), DbType.Int64, ParameterDirection.Input);

            CnnFalp.ParametroBD("PIN_TOTAL_COBERTURA", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TOTAL_COPAGO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COMPLICACION_PORCEN", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CAPACIDAD_PAGO_PORCEN", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_RIESGO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_USER_GRABA", usuario, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CPRE_ID", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CICLOS", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_RUT_MEDICO", txtmedico.Tag, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_FECHA_EVALUACION", string.Empty, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CONVENIO", txt_conv_onco.Text.Equals("0") ? "NO" : "SI", DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_T_PTO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CPPR", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("POUT_FAI", 0, DbType.Int64, ParameterDirection.Output);

            int registro = CnnFalp.ExecuteNonQuery();
            val = Convert.ToInt64(CnnFalp.ParamValue("POUT_FAI"));

            return val;

        }

        protected void Crear_procedimientos_Fai(string v_cod_presupuesto, string cod_fonasa, int lateralidad, string vigente)
        {
            try
            {
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GRABA_PROCEDIMIENTOS_FAI");
                CnnFalp.ParametroBD("PIN_CFAI_ID", v_cod_fai, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PROCEDIMIENTO", cod_fonasa.ToString(), DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_LATERALIDAD", lateralidad.ToString(), DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_USER_GRABA", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_VIGENCIA", vigente, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("POUT_CFP_ID", DBNull.Value, DbType.Int64, ParameterDirection.Output);
                CnnFalp.ExecuteNonQuery();
                Int64 CFP_SEQ = Convert.ToInt64(CnnFalp.ParamValue("POUT_CFP_ID").ToString());

                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GRABA_COD_FONASA_FAI");
                CnnFalp.ParametroBD("PIN_CFP_ID", CFP_SEQ, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_FONASA", cod_fonasa, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_USER_GRABA", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void Crear_Diagnostico_Fai(string v_cod_presupuesto)
        {
            try
            {
                if (dt_diagnostico.Rows.Count>0)
                {
                    foreach (DataRow FilaDiag in dt_diagnostico.Rows)
                    {
                        Int64 cod_diag = Convert.ToInt64(FilaDiag["COD_DIAGNOSTICO"].ToString());
                        Int64 cod_lat = Convert.ToInt64(FilaDiag["COD_LATERALIDAD"].ToString());
                        CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK+ ".P_GRABA_DIAGNOSTICOS_FAI");
                        CnnFalp.ParametroBD("PIN_CFAI_ID", v_cod_fai, DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_ID_DIAGNOSTICO", cod_diag, DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_LATERALIDAD", cod_lat, DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_USER_GRABA", usuario, DbType.String, ParameterDirection.Input);
                        CnnFalp.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void Cargar_Fai()
        {
            System.Diagnostics.Process MiProceso = new System.Diagnostics.Process();

            MiProceso.StartInfo.WorkingDirectory = Application.StartupPath + @"\..\MODIFICACIONES\";

          //  MiProceso.StartInfo.FileName = "ADM004M.exe"; // nombre del archivo a ejecutar con su extension
            MiProceso.StartInfo.FileName = "ADM004M.exe"; // produccion


            MiProceso.StartInfo.Arguments = v_cod_fai.ToString(); // esto es opcional en caso que el ejecutablee reciba parametros           

            MiProceso.Start(); // inicia el ejecutable

            MiProceso.WaitForExit(); // esta opción indica que el programa solo podra seguir cuando se cierre el ejecutable

            MiProceso.Close(); // cierra el ejecutable

            MiProceso.Dispose(); // libera memoria en la aplicacion
        }

        #endregion

        #endregion

        #region Anulacion 


        protected void Cargar_anulaciones()
        {
            Cargar_Parametros(ref Ayuda, 136, "", "ANULACIÓN");

            if (!Ayuda.EOF())
            {
                v_cod_anulacion = Convert.ToInt32(Ayuda.Fields(0));
                txtanula.Text = Ayuda.Fields(1);
            }
        }

        protected string Anular_presupuesto(Int64 cod_presupuesto)
        {
            string res = "";
            try
            {
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_ANULAR_PRESUPUESTO");

                CnnFalp.ParametroBD("PIN_COD_PRESUPUESTO", cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_MOTIVO", v_cod_anulacion, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_USUARIO", usuario, DbType.String, ParameterDirection.Input);

                int registro = CnnFalp.ExecuteNonQuery();


                CnnFalp.Cerrar();
                if (v_estado_fai == 2)
                {
                     res = "ok";
                }
                else
                {
                    res = "Error";
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void Grabar_Historial_Anular(Int64 cod_presupuesto, Int64 cod_estado)
        {

            try
            {

                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GRABAR_ESTADO_HISTORIAL");

                CnnFalp.ParametroBD("PIN_EPRES_ID", 0, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_EPRES_PRES_ID", cod_presupuesto, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_EPRES_ESTADO_ANT", v_cod_estado, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_EPRES_ESTADO_NUE", 4, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_EPRES_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_EPRES_MOTIVO", v_cod_anulacion, DbType.Int32, ParameterDirection.Input);

                int registro = CnnFalp.ExecuteNonQuery();
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion 

        private void timer1_Tick(object sender, EventArgs e)
        {
            barra.Increment(4);
            txtcont.Text = barra.Value + "%";
        }


        private void Imprimir(int tipo)
        {
            try
            {
                ReporteParam Reporte = new ReporteParam();
                Reporte.Base_de_Datos = Conexion[0];
                Reporte.User_de_Datos = Conexion[1];
                Reporte.Pass_de_Datos = Conexion[2];

                int v_valor = 0;
             


                    Reporte.GenerarParametro("PIN_PRES_ID", v_cod_presupuesto);
                    Reporte.GenerarParametro("PIN_SELECCION", "S");
                   Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\PPTOEHCOS\Rpt_Presupuesto.rpt";
               //     Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\Rpt_Presupuesto.rpt";

                    FrmImprime Frm = new FrmImprime(Reporte);
                    Frm.Show(this);

                    if (dt_totales.Rows.Count > 1)
                    {
                        Reporte.GenerarParametro("PIN_PRES_ID", v_cod_presupuesto);
                        Reporte.GenerarParametro("PIN_SELECCION", "N");
                        Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\PPTOEHCOS\Rpt_Presupuestos.rpt";
                        //  Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\Rpt_Presupuestos.rpt";
                        FrmImprime Frm1 = new FrmImprime(Reporte);
                        Frm1.Show(this);
                    }
                 

             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void abrir_pantalla_fai()
        {
            DialogResult opc2 = MessageBox.Show("Estimado Usuario, Desea Realizar Fai  N° " + v_cod_fai + " Presupuesto  N° " + v_cod_presupuesto + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (opc2 == DialogResult.Yes)
            {
                Cargar_Fai();
            }
        }
        #endregion


        #region  Botones

        private void btn_añadir_concepto_Click(object sender, EventArgs e)
        {
            if (Validar_conceptos())
            {
               string res= AgregarConcepto(Convert.ToInt32(txtconcepto.Tag),txtconcepto.Text,txtcantidad.Text,txtvalor.Text);
                if (res == "ok")
                {
                    Mensaje("Conceptos", 8);
                }
                CalculoHonorarios();
                Limpiar_concepto();
                barra.Visible = false;
                txtcont.Text = "0 %";
                txtcont.Visible = false;
                btn_guardar.Enabled = true;
                agregarimagen_concepto();
            }
        }

        private void btn_calcular_Click(object sender, EventArgs e)
        {
            if (dt_prestaciones.Rows.Count > 0)
            {
                btnBuscarFolios.Enabled = true;
                dt_camas.Clear();
                dt_hm.Clear();
                dt_totales.Clear();
                gr_concepto.Enabled = true;
                barra.Value = 0;
                txtcont.Text = "0 %";
                dt_presupuesto.Clear();

                barra.Visible = true;
                txtcont.Visible = true;
                this.timer1.Start();

                prestaciones = "";
                Frm_Seleccion_Cama frm_selec = new Frm_Seleccion_Cama();
                gr_concepto.Enabled = true;

                DialogResult opc = MessageBox.Show("Estimado Usuario, Esta Seguro de realizar el Calculo al Plan  " + v_cod_plan + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opc == DialogResult.Yes)
                {
                    this.timer1.Start();
                    if (dt_prestaciones.Rows.Count > 0)
                    {
                        prestaciones = Concatenar_prestaciones();

                        if (prestaciones != "")
                        {

                            Buscar_Folios(prestaciones);
                            timer1.Stop();
                            DialogResult opcexcluir;
                            if (!dt_folios.Rows.Count.Equals(0))
                            {
                                opcexcluir = MessageBox.Show("Estimado Usuario, Desea Excluir los valores Dispersos(Mayores)?", "Informacion Calcular", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (opcexcluir == DialogResult.Yes) Excluir = 1; else Excluir =0;
                            }
                            timer1.Stop();
                            Mostrar_Conceptos(prestaciones);
                            agregarimagen_concepto();
                            timer1.Start();

                            if (dt_camas.Rows.Count == 0)
                            {
                                timer1.Stop();
                                Añadir_dias_camas();
                                agregarimagen_concepto();
                                timer1.Start();
                            }
                        }
                        else
                        {
                            Mensaje("Prestaciones", 3);

                        }
                    }
                    else
                    {
                        Mensaje("PRESTACIONES", 6);
                    }
                    timer1.Stop();

                    if (dt_hm.Rows.Count == 0)
                    {
                        Frm_Seleccion_HM frm_hm = new Frm_Seleccion_HM();
                        frm_hm.Inicializar_datos(v_id_plan, v_cod_plan, Convert.ToInt64(txtinstitucion.Tag), Convert.ToInt64(txtprevicion.Tag), Convert.ToInt64(txtplan_previsional.Tag), prestaciones, dt_hm, Convert.ToInt64(v_cod_presupuesto), dt_prestaciones);
                        dt_hm = frm_hm.dt_hm;
                        agregarimagen_concepto();
                    }
                    CalculoHonorarios();
                    barra.Visible = false;
                    txtcont.Text = "0 %";
                    txtcont.Visible = false;
                    btn_guardar.Enabled = true;
                    agregarimagen_concepto();
                    timer1.Start();
                   
                }
            }
            else
            {
                Mensaje("", 16);
            }
        }

        private void btn_codigo_fonasa_Click(object sender, EventArgs e)
        {
            if (Validar_prestaciones())
            {
                AgregarPrestaciones();
                Limpiar_prestacion();
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {

            if (v_cod_presupuesto == 0)
            {
                DialogResult opc = MessageBox.Show("Estimado Usuario, Esta  Seguro de Crear el Presupuesto a el Plan de Tratamiento N° " + v_cod_plan + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opc == DialogResult.Yes)
                {
                    if (txtuni_clinica.Tag != null)
                    {
                        if (Convert.ToInt32(txtuni_clinica.Tag) == 31 || Convert.ToInt32(txtuni_clinica.Tag) == 44)
                        {
                            Mensaje("", 10);
                        }
                    }
                }

                // Levantar Pantalla Fai
                Guardar_Presupuesto(0);
                abrir_pantalla_fai();
                Imprimir(0);
                gr_contenedor.Enabled = false;

                n_presupuesto.Text = v_cod_presupuesto.ToString();
                txtestado.Text = "EMITIDO";
            }
            else
            {
                if (Validar_estado_fai())
                {
                    Modificar_valores();
                    
                    Mensaje("EN PROCESO", 9);
                }
                else
                {
                    switch (v_estado_fai)
                    {
                        case 0: Mensaje("NO EXISTE ESTADO", 15); break;
                        case 2: Mensaje("CERRADA", 15); break;
                        case 3: Mensaje("ANULADA", 15); break;
                        case 4: Mensaje("NO CONCRETADA", 15); break;
                    }

                }
                btn_guardar.Visible = false;
            }

            Cargar_Historial();
            
        }

        private void Modificar_valores()
        {

            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_MODIFICAR_DIAS_CAMAS");

            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPO", selec_cama, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_USUARIO", usuario, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TOTAL", total_cama, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CANTIDAD", cant_cama, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TOTAL_HC", txthc.Text.Replace(".", ""), DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PRES_TOTAL_HM", txthm.Text.Replace(".", ""), DbType.Int64, ParameterDirection.Input);
      


            int registro = CnnFalp.ExecuteNonQuery();

        }

        private Boolean Validar_estado_fai()
        {
            Boolean var = false;

            if (v_cod_presupuesto > 0)
            {
                DataTable dt = new DataTable();
                string valor = "";
                dt.Clear();
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_VALIDAR_ESTADO_FAI");
                CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                dt.Load(CnnFalp.ExecuteReader());

                foreach (DataRow miRow1 in dt.Rows)
                {
                    v_estado_fai = Convert.ToInt64(miRow1["COD_ESTADO"].ToString());
                }


                if (v_estado_fai == 1)
                {
                    var = true;
                }
            }
            else
            {
                var = true;
            }


            return var;

        }



        private void btn_anular_Click(object sender, EventArgs e)
        {
            DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Anular el Presupuesto N°" + v_cod_presupuesto + "", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (opc == DialogResult.Yes)
            {
                if (v_cod_estado > 1)
                {
                    if (v_usuario_grilla.ToUpper() == usuario.ToUpper() || v_super_usuario != "")
                    {
                        
                        v_estado_fai = Extraer_estado(v_cod_presupuesto);
                        if (v_estado_fai == 1)
                        {
                            Cargar_anulaciones();
                            string res = Anular_presupuesto(v_cod_presupuesto);
                            if (res == "ok")
                            {
                                Mensaje("", 11);
                                Grabar_Historial_Anular(v_cod_presupuesto, v_cod_estado);
                               
                            }
                        }
                        else
                        {
                            switch (v_estado_fai)
                            {
                                case 2: Mensaje("CERRADA", 15); break;
                                case 3: Mensaje("ANULADA", 15); break;
                                case 4: Mensaje("NO CONCRETADA", 15); break;
                            }
                        }

                    }
                    else
                    {
                        Mensaje("", 18);
                    }
            
                }
                else
                {
                    Mensaje("", 12);
                }
                gr_contenedor.Enabled = false;
            }

            txtestado.Text = "ANULADO";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (v_cod_presupuesto > 0)
            {
                Imprimir(0);
            }
            else
            {
                Mensaje("", 14);
            }
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {

            foreach (DataRow miRow1 in dt_presupuesto.Select("COD_CONCEPTO = '" + txtconcepto.Tag + "'"))
            {
                miRow1["CANTIDAD"] = txtcantidad.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtcantidad.Text);
                miRow1["VALOR"] = txtvalor.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtvalor.Text);
            }

            btn_confirmar.Visible = false;
            btn_añadir_concepto.Visible = true;
            txtvalor.Enabled = false;

            Mensaje("", 9);
            CalculoHonorarios();
            Limpiar_concepto();
        }

        #endregion

        #region  Validaciones

        protected void Mensaje(string descripcion, int tipo)
        {
            switch (tipo)
            {
                case 1: MessageBox.Show("Estimado Usuario, El Campo " + descripcion + " se Encuentra Vacio ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 2: MessageBox.Show("Estimado Usuario, La Descripción " + descripcion + " ya se encuentra Registrada en la Grilla ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 3: MessageBox.Show("Estimado Usuario, No fue Seleccionado ningún Registro " + descripcion + " ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 4: MessageBox.Show("Estimado Usuario, Fue Eliminada La Información Correctamente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 5: MessageBox.Show("Estimado Usuario, Fue Agregada La Información Correctamente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 6: MessageBox.Show("Estimado Usuario, No existen Registro " + descripcion + " ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 7: MessageBox.Show("Estimado Usuario, No esta Autorizado para Consultar el Historial, Comunicarse con su Supervisor", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 8: MessageBox.Show("Estimado Usuario, Fue Agregada La Información " + descripcion + " Correctamente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 9: MessageBox.Show("Estimado Usuario, Fue Modificada La Información  Correctamente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 10: MessageBox.Show("Estimado Usuario, Este Presupuesto debe ser revisado  por su superior, debido a que la  Unidad Clinica es Neurologia o Traumatologia.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 11: MessageBox.Show("Estimado Usuario, El Presupuesto Fue Anulado Correctamente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 12: MessageBox.Show("Estimado Usuario, El Presupuesto no puede ser Anulado, debido a que su Estado es Pendiente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 13: MessageBox.Show("Estimado Usuario, El Paciente No fue Seleccionado, por favor intentar de Nuevo o con otro Paciente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 14: MessageBox.Show("Estimado Usuario, El No se puede Imprimir , porque no existe Presupuesto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 15: MessageBox.Show("Estimado Usuario, Error  En el Estado Fai "+ descripcion+ ", por ende no se puede Anular", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 16: MessageBox.Show("Estimado Usuario, No fue Ingresada Ninguna Prestación", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 17: MessageBox.Show("Estimado Usuario, El Valor de Honorario Médico tiene que ser Mayor a Cero", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 18: MessageBox.Show("Estimado Usuario, No esta Autorizado, para Eliminar este Presupuesto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
            }
         
        }

        protected Boolean Validar_conceptos()
        {
            Boolean var = false;

            if ( txtconcepto.Tag != "")
            {
                if  (txtcantidad.Text != "")
                {
                    if (txtvalor.Text != "" )
                    {
                        if (Validar_concepto_grilla())
                        {
                            var = true;
                        }
                        else
                        {
                            Mensaje(txtconcepto.Text, 2);
                        }
                    }
                    else
                    {
                        Mensaje("Valor", 1);
                        txtvalor.Focus();
                    }
                }
                else
                {
                    Mensaje("Cantidad", 1);
                    txtcantidad.Focus();
                }
            }
            else
            {
                Mensaje("Concepto", 1);
                txtconcepto.Focus();
            }

            return var;
        }

        protected Boolean Validar_concepto_grilla()
        {
            Boolean var = false;

            if(dt_presupuesto.Rows.Count==0)
            {
                var=true;
            }
            else{
                int cont = 0;

                if (txtconcepto.Tag != null || txtconcepto.Tag !="" )
                
                {
                    foreach (DataRow miRow1 in dt_presupuesto.Select(" COD_CONCEPTO =" + Convert.ToInt32(txtconcepto.Tag)))
                    {
                        cont++;
                    }

                    if (cont == 0)
                    {
                        var = true;
                    }
                }
              
            }

            return var;
        }

        protected Boolean Validar_prestaciones()
        {
            Boolean var = false;

            if (txtcodigo.Text != string.Empty)
            {
                var = true;
            }
            else
            {
                Mensaje("Prestación", 1);
            }

            return var;
        }

        private void Validar_usuario()
        {
            try
            {
                Conectar();
                string programa = "PPTOC001IE";
                DataTable tbl = new DataTable();
                CnnFalp.CrearCommand(CommandType.StoredProcedure, "PCK_CLIN_PRESUPUESTO.FC_VALIDAR_USER");
                CnnFalp.ParametroBD("PIN_USUARIO",usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PROGRAMA", programa, DbType.String, ParameterDirection.Input);
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
                tbl.Load(CnnFalp.ExecuteReader());

                foreach (DataRow miRow1 in tbl.Rows)
                {
                    Super_usuario = miRow1["USUARIOS"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private Boolean Validar_dt(DataTable dt)
        {
            Boolean var = false;
            string cad="";
            foreach (DataRow miRow1 in dt.Rows)
            {
                cad = dt.Rows[0][0].ToString();
            }

            if (cad == "sin_datos")
            {
                var = true;
            }
            return var;       
        }

        private void Eliminar_dias_camas()
        {
            Boolean var = false; 
            foreach (DataRow miRow1 in dt_presupuesto.Select("COD_CONCEPTO=1"))
            {
                grilla_presupuesto.Rows.RemoveAt(grilla_presupuesto.CurrentRow.Index);
            }

            dt_presupuesto.AcceptChanges();
          
        }


        #region KeyPress de los campos

        #region  KeyPress Concepto

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtvalor.Focus();
                }
            }
        }

        private void txtvalor_KeyPress(object sender, KeyPressEventArgs e)
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
                    btn_añadir_concepto.Focus();
                }
            }
        }

        #endregion

        #endregion

        private void grilla_presupuesto_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_presupuesto.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        #endregion

        #region Crear Tablas

        private void CrearConceptos()
        {
            dt_presupuesto.Columns.Clear();

            dt_presupuesto.Columns.Add("COD_CONCEPTO", typeof(string));
            dt_presupuesto.Columns.Add("NOM_CONCEPTO", typeof(string));
            dt_presupuesto.Columns.Add("CANTIDAD", typeof(string));
            dt_presupuesto.Columns.Add("VALOR", typeof(Int64));
            dt_presupuesto.Columns.Add("COD_TIPOHONORARIO", typeof(string));
            dt_presupuesto.Columns.Add("MANUAL", typeof(int));
            dt_presupuesto.Columns.Add("VIGENTE", typeof(string));
        }

        private void CrearFolios()
        {
            dt_folios.Columns.Clear();         
            dt_folios.Columns.Add("FOLIO", typeof(string));
            dt_folios.Columns.Add("FECHA_CREACION", typeof(string));
            dt_folios.Columns.Add("NOMBRES", typeof(string));
            dt_folios.Columns.Add("TOTALFOLIO", typeof(string));
            dt_folios.Columns.Add("EXTREMO", typeof(string));
            dt_folios.Columns.Add("SIGMA", typeof(string));
            dt_folios.Columns.Add("PROMEDIO", typeof(string));
        }

        #endregion

        private void grillaHistorial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                v_cod_presupuesto = Convert.ToInt64(grillaHistorial.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString());
                n_presupuesto.Text=v_cod_presupuesto.ToString();
                Cargar_datos();
            }
        }


        protected void Modificar_Valores_HM()
        {
            Frm_Seleccion_HM frm_hm = new Frm_Seleccion_HM();
            frm_hm.Inicializar_datos(v_id_plan, v_cod_plan, Convert.ToInt64(txtinstitucion.Tag), Convert.ToInt64(txtprevicion.Tag), Convert.ToInt64(txtplan_previsional.Tag), prestaciones,dt_hm,v_cod_presupuesto,dt_prestaciones);
            frm_hm.ShowDialog();
            Limpiar_concepto();
            txtconcepto.Focus();
            Int64 Valor = 0;
            Valor=frm_hm.Total_hm();
            dt_hm = frm_hm.dt_hm;
            int cont = 0;

            if (Valor > 0)
            {
                foreach (DataRow miRow1 in dt_presupuesto.Select("COD_CONCEPTO = '" + 3 + "'"))
                {
                    cont++;
                    miRow1["CANTIDAD"] = txtcantidad.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtcantidad.Text);
                    miRow1["VALOR"] = Valor;
                    miRow1["VIGENTE"] = "S";
                }

                if (cont == 0)
                {
                    AgregarConcepto(3, "Honorario Médico", "1", Valor.ToString());
                }


                CalculoHonorarios();
            }
            else
            {
                Mensaje("", 17);
            }
            agregarimagen_concepto();
            barra.Visible = false;
            txtcont.Text = "0 %";
            txtcont.Visible = false;
        }

        private void btnBuscarFolios_Click(object sender, EventArgs e)
        {
            prestaciones = Concatenar_prestaciones();

            Frm_Seleccion_Folios frm_folio = new Frm_Seleccion_Folios();
            frm_folio.Inicializar_datos(v_id_plan, v_cod_plan,v_cod_presupuesto, Convert.ToInt64(txtinstitucion.Tag), Convert.ToInt64(txtprevicion.Tag), Convert.ToInt64(txtplan_previsional.Tag), txtinstitucion.Text, txtprevicion.Text, txtplan_previsional.Text, prestaciones, dt_folios,Excluir);
            frm_folio.ShowDialog();
            
        }

        private void btn_linpiar_concepto_Click(object sender, EventArgs e)
        {
            Limpiar_concepto();
        }

        private void btn_limpiar_fonasa_Click(object sender, EventArgs e)
        {
            Limpiar_prestacion();
        }

        private void txtriesgo_clinico_KeyPress_1(object sender, KeyPressEventArgs e)
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
                    Cargar_Tipo_Riesgo();
                }
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            if(v_cod_presupuesto ==0)
            {
                Cargar_Conceptos_Presupuesto();   
                Cargar_Prestaciones();
                gr_concepto.Enabled = false;
                Validar_usuario();
                btn_confirmar.Visible = false;
                barra.Visible = false;
                txtcont.Visible = false;
                btn_guardar.Enabled = false;
                btn_anular.Enabled = false;
                n_presupuesto.Text = "0";
                txtestado.Text = "PENDIENTE";
                btnBuscarFolios.Enabled = false;
                dt_camas.Clear();
                dt_totales.Clear();
                dt_presupuesto.Clear();
                txthm.Text = "0";
                txthc.Text = "0";
                txttotal.Text = "0";
            }

        }

    }
}
