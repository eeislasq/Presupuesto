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
    public partial class Frm_Seleccion_Cama : Form
    {
        #region Variables

        #region Variables Comunes

        Int64 v_id_plan = 0;
        Int64 v_cod_plan = 0;
        Int64 v_cod_presupuesto = 0;
        Int64 v_cod_institucion = 0;
        Int64 v_cod_prevision = 0;
        Int64 v_cod_plan_prevision = 0;
        Int64 v_cod_cama= 0;
        Int64 v_valor = 0;
        string v_nom = "";
        string v_mov = "";
        string v_estado_fai = "";
        string usuario = "";
       public   Int64 v_selec_cama = 0;
       public   Int64 v_cant_cama = 0;
       public  Int64 v_total_cama = 0;

        #endregion

        #region Variable DataTable

     public  DataTable dt_cama = new DataTable();
     public DataTable dt_totales = new DataTable();

        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion

        #endregion
        public Frm_Seleccion_Cama()
        {
            InitializeComponent();
        }

        public Frm_Seleccion_Cama(DataTable dt,DataTable dt1, Int64 v_tipo_cama, Int64 v_cantidad, Int64 v_total, Int64 id_plan, Int64 cod_plan, Int64 cod_presupuesto, string nombre, Int64 cod_institucion, Int64 cod_prevision,Int64 cod_plan_prevision)
        {

            v_selec_cama = v_tipo_cama;
            v_cant_cama = v_cantidad;
            v_total_cama = v_total;
            v_id_plan = id_plan;
            v_cod_plan = cod_plan;
            v_nom = nombre;
            v_cod_presupuesto = cod_presupuesto;
            dt_cama = dt;
            dt_totales = dt1;
            v_cod_institucion = cod_institucion;
            v_cod_prevision = cod_prevision;
            v_cod_plan_prevision = cod_plan_prevision;
            InitializeComponent();
        }

        private void Frm_Seleccion_Cama_Load(object sender, EventArgs e)
        {
           
            Conectar();
            if (v_cod_presupuesto == 0)
            {
                grilla_prestaciones_adicionales.DataSource = dt_cama;
                grilla_totales.DataSource = dt_totales;
                gr_contenedor.Enabled = true;
            }

            else
            {
                Cargar_grilla_total();
                Cargar_grilla_Presupuesto();
                Cargar_grilla_total_presupuesto();
               // gr_contenedor.Enabled = false;
                gr_lista.Enabled = false;
                gr_total.Enabled = true;
               
            }
            Ordenar_columna();
            agregarimagen();
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

        public void Inicializar_datos(Int64 id_plan, Int64 cod_plan, Int64 cod_presupuesto, Int64 cod_institucion, Int64 cod_prevision, Int64 cod_plan_prevision, string nombre, DataTable dt, DataTable dt_2)
        {
            Conectar();

            if (v_selec_cama == 0 && v_cant_cama == 0 && v_total_cama == 0)
            {
                v_id_plan = id_plan;
                v_cod_plan = cod_plan;
                v_cod_presupuesto = cod_presupuesto;
                v_cod_institucion = cod_institucion;
                v_cod_prevision = cod_prevision;
                v_cod_plan_prevision = cod_plan_prevision;
                dt_cama = dt;
                dt_totales = dt_2;
                if (dt_cama.Rows.Count == 0)
                {
                    Cargar_grilla();
                    Cargar_grilla_total();
                }
              
                btn_confirmar.Visible = false;
            
            }

        }

        public void Cargar_grilla()
        {

                dt_cama.Clear();
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DIAS_CAMA");
                dt_cama.Load(CnnFalp.ExecuteReader());
                grilla_prestaciones_adicionales.DataSource = dt_cama;
                Ordenar_columna();
                dt_cama.Clear();

        }


        public void Cargar_grilla_Presupuesto()
        {

            dt_cama.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DIAS_CAMA_PRESUPUESTO");
            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_PREVISION", v_cod_institucion, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_PLANPREVISION", v_cod_plan_prevision, DbType.String, ParameterDirection.Input);
            dt_cama.Load(CnnFalp.ExecuteReader());
            grilla_prestaciones_adicionales.DataSource = dt_cama;
            Ordenar_columna();


        }
        private void Ordenar_columna()
        {
            grilla_prestaciones_adicionales.Columns["E"].DisplayIndex = 0;
            grilla_prestaciones_adicionales.Columns["M"].DisplayIndex = 1;
            grilla_prestaciones_adicionales.Columns["DESCRIPCION"].DisplayIndex = 2;
            grilla_prestaciones_adicionales.Columns["TIPO_CAMA"].DisplayIndex = 3;
            grilla_prestaciones_adicionales.Columns["CANTIDAD"].DisplayIndex = 4;
            grilla_prestaciones_adicionales.Columns["NOM_PRESTACION"].DisplayIndex = 5;
            grilla_prestaciones_adicionales.Columns["PRECIO"].DisplayIndex = 6;
            grilla_prestaciones_adicionales.Columns["TOTAL"].DisplayIndex = 7;
     
        }

        public void Cargar_grilla_total()
        {

                dt_totales.Clear();
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_TOTAL_PRESUPUESTO");
                CnnFalp.ParametroBD("PIN_PRES_ID", 0, DbType.String, ParameterDirection.Input);
                dt_totales.Load(CnnFalp.ExecuteReader());
                grilla_totales.DataSource = dt_totales;
                dt_totales.Clear();
              
           
        }

        public void Cargar_grilla_total_presupuesto()
        {

            dt_totales.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_TOTAL_PRESUPUESTO");
            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.String, ParameterDirection.Input);
            dt_totales.Load(CnnFalp.ExecuteReader());
            grilla_totales.DataSource = dt_totales;
   


        }

        private void grilla_cama_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_prestaciones_adicionales.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        private void grilla_totales_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_totales.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        private void Cargar_select_cama()
        {
          
            Cargar_Parametros_Especificos(ref Ayuda, txttipo_cama.Text, "TIPO CAMA", 7, 0);
            if (!Ayuda.EOF())
            {
                txttipo_cama.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txttipo_cama.Text = Ayuda.Fields(1);
            }
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

        private void txtdia_cama_KeyPress(object sender, KeyPressEventArgs e)
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
                    Cargar_DC();
                    txttipo_cama.Focus();
                }
            }
        }

        private void btn_dia_cama_Click(object sender, EventArgs e)
        {
            txtdia_cama.Text="";
            txtdia_cama.Tag="";
            Cargar_DC();
            txttipo_cama.Focus();
        }

        protected void Cargar_DC()
        {
            Cargar_Parametros(ref Ayuda, 137, txtdia_cama.Text, "DIAS CAMAS");

            if (!Ayuda.EOF())
            {
                txtdia_cama.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtdia_cama.Text = Ayuda.Fields(1);
            }
        }

        protected void Cargar_Camas()
        {
            v_cod_cama = 0;
            v_valor = 0;
            Cargar_Parametros_Cama(ref Ayuda, "", "CAMA");

            if (!Ayuda.EOF())
            {
                txtprestaciones_cama.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtprestaciones_cama.Text = Ayuda.Fields(1);
                txttotal.Text = Ayuda.Fields(2);
                txtcant_dia.Text = "1";
            }
        }


        protected void Cargar_Parametros_Cama(ref AyudaSpreadNet.AyudaSprNet Ayuda, string descripcion, string titulo)
        {

            string[] NomCol = { "Código", "Descripción", "Total" };
            int[] AnchoCol = { 70, 300, 60 };
            Ayuda.Nombre_BD_Datos = Conexion[0];
            Ayuda.Pass = Conexion[1];
            Ayuda.User = Conexion[2];
            Ayuda.TipoBase = 1;
            Ayuda.NombreColumnas = NomCol;
            Ayuda.AnchoColumnas = AnchoCol;
            Ayuda.TituloConsulta = "SELECCIONAR " + titulo.ToUpper() + "";
            Ayuda.Package = PCK;
            Ayuda.Procedimiento = "P_CARGAR_CAMAS";
            Ayuda.Generar_ParametroBD("PIN_COD_PREVISION", v_cod_institucion, DbType.Int64, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_COD_PLANPREVISION", v_cod_plan_prevision, DbType.Int32, ParameterDirection.Input);

            Ayuda.EjecutarSql();
        }

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

        private void btn_tipo_cama_Click(object sender, EventArgs e)
        {
            txttipo_cama.Tag = "";
            txttipo_cama.Text = "";
            Cargar_Tipo_DC();
            txtprestaciones_cama.Focus();
        }

        protected void Cargar_Tipo_DC()
        {
            Cargar_Parametros_Especificos(ref Ayuda, txttipo_cama.Text, "TIPO CAMAS", 7, 0);

            if (!Ayuda.EOF())
            {
                txttipo_cama.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txttipo_cama.Text = Ayuda.Fields(1);
            }
        }

        private void txttipo_cama_KeyPress(object sender, KeyPressEventArgs e)
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
                    Cargar_Tipo_DC();
                    txtprestaciones_cama.Focus();
                }
            }
        }

        protected void Cargar_Prestaciones_DC()
        {
            Cargar_Parametros_Especificos(ref Ayuda, txttipo_cama.Text, "PRESTACIONES CAMAS", 7, 0);

            if (!Ayuda.EOF())
            {
                txtprestaciones_cama.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txtprestaciones_cama.Text = Ayuda.Fields(1);
            }
        }

        private void btn_prestacion_cama_Click(object sender, EventArgs e)
        {
            txtprestaciones_cama.Tag = "";
            txtprestaciones_cama.Text = "";
            Cargar_Camas();
            btn_agregar_dc.Focus();
        }

        private void txtprestaciones_cama_KeyPress(object sender, KeyPressEventArgs e)
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
                    Cargar_Camas();
                    btn_agregar_dc.Focus();
                }
            }
        }

        private void txtcant_dia_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtcant_dia.Focus();
                }
            }
        }

        private void txttotal_KeyPress(object sender, KeyPressEventArgs e)
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
                    txttotal.Focus();
                }
            }
        }

        private void btn_agregar_dc_Click(object sender, EventArgs e)
        {

            if (Validar_DC())
            {
                if (dt_cama.Rows.Count==0)
                {
                    Agregar_dias_camas();
                    Agregar_Totales_camas();
                    Mensaje("", 5);
                }
                else
                {
                    restablecer_seleccion();
                    Agregar_dc();
                }
            }
            limpiar(this);
        
            txtdia_cama.Focus();
        }

        private void Agregar_dias_camas()
        
        {
            Int64 cant = txtcant_dia.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtcant_dia.Text);
            Int64 total = txttotal.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txttotal.Text);
            DataRow Fila = dt_cama.NewRow();

            Fila["CODIGO"] = txtdia_cama.Tag;
            Fila["DESCRIPCION"] = txtdia_cama.Text;
            Fila["COD_TIPO_CAMA"] = txttipo_cama.Tag;
            Fila["TIPO_CAMA"] = txttipo_cama.Text;
            Fila["COD_PRESTACION"] = txtprestaciones_cama.Tag;
            Fila["NOM_PRESTACION"] = txtprestaciones_cama.Text;
            Fila["CANTIDAD"] = cant;
            Fila["PRECIO"] = total;
            Fila["TOTAL"] = cant * total;
            Fila["MANUAL"] = 1;
            Fila["VIGENCIA"] = "S";
  
            dt_cama.Rows.Add(Fila);
            grilla_prestaciones_adicionales.DataSource = new DataView(dt_cama, "VIGENCIA ='S'", "COD_TIPO_CAMA", DataViewRowState.CurrentRows);
        }

        private void Agregar_Totales_camas()
        {
            DataRow Fila = dt_totales.NewRow();

            Fila["T_COD_TIPO_CAMA"] = txttipo_cama.Tag;
            Fila["T_TIPO_CAMA"] = txttipo_cama.Text;
            Fila["T_CANTIDAD"] =txtcant_dia.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtcant_dia.Text);
            Fila["T_TOTAL"] = txttotal.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txttotal.Text);
            if (dt_totales.Rows.Count == 0)
            {
                Fila["T_SELECCION"] = "S";
            }
            else
            {
                Fila["T_SELECCION"] = "N";
                restablecer_seleccion();
            }

            dt_totales.Rows.Add(Fila);
            grilla_totales.DataSource = new DataView(dt_totales, "", "T_COD_TIPO_CAMA", DataViewRowState.CurrentRows);
            agregarimagen();
        }

        protected void Cargar_Totales_Presupuesto()
        {

            foreach (DataRow miRow1 in dt_cama.Rows)
                {
                    Int64 cod = Convert.ToInt64(miRow1["COD_TIPO_CAMA"].ToString());
                    Int64 cant = Convert.ToInt64(miRow1["CANTIDAD"].ToString());
                    Int64 precio = Convert.ToInt64(miRow1["PRECIO"].ToString());
                    string selec = miRow1["SELECCIONADO"].ToString();
                    if (selec == "1")
                    {
                        selec = "S";
                    }
                    else
                    {
                        selec = "N";
                    }
                    if (!Validar_tipo_cama(cod))
                    {
                        DataRow Fila = dt_totales.NewRow();


                        Fila["T_COD_TIPO_CAMA"] = miRow1["COD_TIPO_CAMA"].ToString();
                        Fila["T_TIPO_CAMA"] = miRow1["TIPO_CAMA"].ToString();
                        Fila["T_CANTIDAD"] = miRow1["CANTIDAD"].ToString();
                        Fila["T_TOTAL"] = miRow1["TOTAL"].ToString();
                        Fila["T_SELECCION"] = selec;
                       
                        dt_totales.Rows.Add(Fila);
                    }
                    else
                    {
                        foreach (DataRow miRow2 in dt_totales.Select("T_COD_TIPO_CAMA = '" + cod + "'"))
                        {
                           miRow2["T_CANTIDAD"] = Convert.ToInt64(miRow2["T_CANTIDAD"].ToString()) + cant;
                           miRow2["T_TOTAL"] = Convert.ToInt64(miRow2["T_TOTAL"].ToString()) + (precio * cant);  
                        }
                    }
              }

            dt_totales.AcceptChanges();
            grilla_totales.DataSource = new DataView(dt_totales, "", "T_COD_TIPO_CAMA", DataViewRowState.CurrentRows);
            agregarimagen();
            
        }

        private Boolean Validar_tipo_cama(Int64 cod)
        {
             Boolean var = false;
             foreach (DataRow miRow1 in dt_totales.Select("T_COD_TIPO_CAMA = '" + cod + "'"))
            {
                var = true;
            }

            return var;
        }

        private Boolean Validar_existencia_DC(int tipo)
        {
            Boolean var = false;

            if (tipo == 1)
            {
                foreach (DataRow miRow1 in dt_cama.Select("CODIGO = '" + txtdia_cama.Tag + "' and COD_TIPO_CAMA=  '" + txttipo_cama.Tag + "' "))
                {

                        var = true;
                        if (v_mov == "S")
                        {
                            Int64 cant = txtcant_dia.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtcant_dia.Text);
                            Int64 total = txttotal.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txttotal.Text);
                            miRow1["CANTIDAD"] = cant;
                            miRow1["PRECIO"] = total;
                            miRow1["TOTAL"] = (total * cant);
                        }
                        Calcula_Total_M();
                  

                }

            }
            else
            {
                int cont = 0;
                foreach (DataRow miRow1 in dt_totales.Select("T_COD_TIPO_CAMA = '" + Convert.ToInt32(txttipo_cama.Tag) + "'"))
                {
                    cont++;
                }

                if(cont>0)
                {
                    var = true;
                }
              
              
            }

            return var;

        }

        private void Calcula_Total_M()
        {
            foreach (DataRow miRow1 in dt_totales.Select("T_COD_TIPO_CAMA = '" + Convert.ToInt32(txttipo_cama.Tag) + "'"))
            {

                Int64 cant = txtcant_dia.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txtcant_dia.Text);
                Int64 total = txttotal.Text.Equals(string.Empty) ? 1 : Convert.ToInt64(txttotal.Text);
                miRow1["T_CANTIDAD"] = Calcula_DIAS_M();
                miRow1["T_TOTAL"] = Calcula_Precio_M();

            }
            dt_totales.AcceptChanges();
        }

        private Int64 Calcula_Precio_M()
        
        {
            Int64 total = 0;
            foreach (DataRow miRow1 in dt_cama.Select("COD_TIPO_CAMA = '" + Convert.ToInt32(txttipo_cama.Tag) + "'"))
            {
                total += Convert.ToInt64(miRow1["TOTAL"].ToString());

            }
            return total;
        }

        private Int64 Calcula_DIAS_M()
        {
            Int64  dias=0;
            foreach (DataRow miRow1 in dt_cama.Select("COD_TIPO_CAMA = '" + Convert.ToInt32(txttipo_cama.Tag) + "'"))
            {

              dias +=  Convert.ToInt64(miRow1["CANTIDAD"].ToString()) ;
         
            }
            return dias;
        }






        private void Agregar_dc()

        {
            if (!Validar_existencia_DC(1))
            {
                if (v_mov != "S")
                {
                    Agregar_dias_camas();
                }
                else
                {
                    Agregar_dias_camas();
                }

                if (dt_totales.Rows.Count==0)
                {
                    Agregar_Totales_camas();
                }
                else
                {
                    if (!Validar_existencia_DC(2))
                    {

                        Agregar_Totales_camas();
                    }
                    else
                    {
                        Calcula_Total_M();
                    }
                }
                Mensaje("", 5);
            }
            else
            {
                if (v_mov != "S")
                {
                    Mensaje("Dia Cama", 3);
                }
            }
            dt_cama.AcceptChanges();
        }

        private Boolean Validar_DC()
        
        {
            Boolean var = false;

            if (txtdia_cama.Tag != null)
            {
                if (txttipo_cama.Tag != null)
                {
                    if (txtprestaciones_cama.Tag != null)
                    {
                        var = true;
                    }
                    else
                    {
                        Mensaje("Prestaciones",4);
                    }
                }
                else
                {
                    Mensaje("Tipo Cama", 4);
                }
            }
            else
            {
                Mensaje("Dia Cama", 4);
            }
          


            return var;
        }

        protected void Mensaje(string descripcion, int tipo)
        {
            switch (tipo)
            {
                case 1: MessageBox.Show("Estimado Usuario, No fue Seleccionado Ningún Día Cama para Calculo ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 2: MessageBox.Show("Estimado Usuario, El tipo " + descripcion + " Seleccionado debe ser Mayor a Cero ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 3: MessageBox.Show("Estimado Usuario, La Información " + descripcion + " ya se encuentra registrada ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 4: MessageBox.Show("Estimado Usuario, El Campo  " + descripcion + " se encuentra Vacio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 5: MessageBox.Show("Estimado Usuario, La Información fue Registrada Correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 6: MessageBox.Show("Estimado Usuario, La Información fue Modificada Correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 7: MessageBox.Show("Estimado Usuario, No fue Seleccionado Ningun Fila en grilla " + descripcion + "  ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 9: MessageBox.Show("Estimado Usuario, Fue Realizado Correctamente el Cambio de Cotización Cama ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 10: MessageBox.Show("Estimado Usuario, No se Puede Realizar Cambio en la Cotización Cama, debido a que la Fai se encuentra en Estado "+ descripcion +" ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
            }

        }

        protected void limpiar(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = string.Empty;
                    c.Tag = null;
                }
                if (c.Controls.Count > 0)
                {
                    limpiar(c);
                }
            }

        }

        private void grilla_cama_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Int64 cod_cama = Convert.ToInt64(grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString());
                Int64 cod_tipo_cama = Convert.ToInt64(grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["COD_TIPO_CAMA"].Value.ToString());
                Int64 cod_prestacion = Convert.ToInt64(grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["COD_PRESTACION"].Value.ToString());
                Int64 manual = Convert.ToInt64(grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["MANUAL"].Value.ToString());
                Int64 total=Convert.ToInt64(grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["TOTAL"].Value.ToString());
                Int64 cantidad = Convert.ToInt64(grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["CANTIDAD"].Value.ToString());
                string nom = grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["NOM_PRESTACION"].Value.ToString();
                if (e.ColumnIndex == 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Esta  Seguro de Eliminar la Prestación " + nom + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                         if(manual==1)
                         {
                             eliminar_dia_cama(cod_cama,cod_tipo_cama, total, cantidad);
                             grilla_prestaciones_adicionales.Rows.RemoveAt(grilla_prestaciones_adicionales.CurrentRow.Index);
                             dt_cama.AcceptChanges();
                             
                         }

                         else
                         {
                             foreach (DataRow miRow1 in dt_cama.Select(" CODIGO ='" + cod_cama + "'  AND  COD_TIPO_CAMA = '"+ cod_tipo_cama+ "' AND  COD_PRESTACION= '"+ cod_prestacion +"' "))
                             {
                                 miRow1["VIGENCIA"] = "N";
                             }
                         }

                    }
                }
                else
                {
                    if (e.ColumnIndex == 1)
                    {
                       
                            btn_agregar_dc.Visible = false;
                            btn_confirmar.Visible = true;
                            string nom_dia_cama = grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["DESCRIPCION"].Value.ToString();
                            string nom_tipo_cama = grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["TIPO_CAMA"].Value.ToString();
                            txttotal.Text = grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["PRECIO"].Value.ToString();
                            txtcant_dia.Text = grilla_prestaciones_adicionales.Rows[e.RowIndex].Cells["CANTIDAD"].Value.ToString();
                            txtdia_cama.Tag = cod_cama;
                            txtdia_cama.Text = nom_dia_cama;
                            txttipo_cama.Tag = cod_tipo_cama;
                            txttipo_cama.Text = nom_tipo_cama;
                            txtprestaciones_cama.Tag = cod_prestacion;
                            txtprestaciones_cama.Text = nom;
                            txttotal.Enabled = true;
                            v_mov = "S";

                        
                    }
                }

            }
          
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (Validar_DC())
            {
                DialogResult opc = MessageBox.Show("Estimado Usuario, Esta Seguro de Modificar la Prestación " + txtprestaciones_cama.Text + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opc == DialogResult.Yes)
                {
                    txtdia_cama.Enabled = false;
                    txttipo_cama.Enabled = false;
                    btn_dia_cama.Enabled = false;
                    btn_tipo_cama.Enabled = false;
                    Agregar_dc();
                }
            }

            Mensaje("",5);
            limpiar(this);
            v_mov = "";
            txtdia_cama.Focus();
            txtdia_cama.Enabled = true;
            txttipo_cama.Enabled = true;
            btn_dia_cama.Enabled = true;
            btn_tipo_cama.Enabled = true;
            btn_agregar_dc.Visible = true;
            btn_confirmar.Visible = false;
        
        }

        private void eliminar_dia_cama(Int64 cod_cama, Int64 cod_tipo_cama, Int64 valor, Int64 cant)
        {
            foreach (DataRow miRow1 in dt_totales.Select("T_COD_TIPO_CAMA = '" + cod_tipo_cama + "' "))
            {
                miRow1["T_TOTAL"] = Convert.ToInt64(miRow1["T_TOTAL"].ToString()) - valor;
                miRow1["T_CANTIDAD"] = Convert.ToInt64(miRow1["T_CANTIDAD"].ToString()) - cant;
            }

            foreach (DataRow miRow2 in dt_cama.Select(" CODIGO ='" + cod_cama + "' AND  COD_TIPO_CAMA = '" + cod_tipo_cama + "' "))
            {
                miRow2["TOTAL"] = Convert.ToInt64(miRow2["TOTAL"].ToString()) - valor;
                miRow2["CANTIDAD"] = Convert.ToInt64(miRow2["CANTIDAD"].ToString()) - cant;
            }
        }

        private void grilla_totales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Validar_estado_fai())
            {
                string tipo_cama = grilla_totales.Rows[e.RowIndex].Cells["T_TIPO_CAMA"].Value.ToString();
                Int64 cod_tipo_cama = Convert.ToInt64(grilla_totales.Rows[e.RowIndex].Cells["T_COD_TIPO_CAMA"].Value.ToString());
                DialogResult opc = MessageBox.Show("Estimado Usuario, Esta Seguro de Seleccionar este Tipo de Cama  " + tipo_cama + " al Presupuesto", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opc == DialogResult.Yes)
                {

                    foreach (DataRow miRow1 in dt_totales.Select("T_COD_TIPO_CAMA = '" + cod_tipo_cama + "' "))
                    {
                        string valor = miRow1["T_SELECCION"].ToString();


                        if (valor == "N")
                        {
                            restablecer_seleccion();
                            miRow1["T_SELECCION"] = "S";

                        }
                        else
                        {
                            miRow1["T_SELECCION"] = "N";
                        }

                        if (miRow1["T_SELECCION"].ToString() == "S")
                        {

                            v_selec_cama = Convert.ToInt64(miRow1["T_COD_TIPO_CAMA"].ToString());
                            v_cant_cama = Convert.ToInt64(miRow1["T_CANTIDAD"].ToString());
                            v_total_cama = Convert.ToInt64(miRow1["T_TOTAL"].ToString());
                        }
                        else
                        {
                            v_selec_cama = 0;
                            v_cant_cama = 0;
                            v_total_cama = 0;
                        }

                    }
                }
                dt_totales.AcceptChanges();
                Mensaje("", 6);
                agregarimagen();
            }
            else
            {
                switch (v_estado_fai)
                {
                    case "2": Mensaje("CERRADA", 10); break;
                    case "3": Mensaje("ANULADA", 10); break;
                    case "4": Mensaje("NO CONCRETADA", 10); break;


                }
            }
        }


        protected void agregarimagen()
        {
            foreach (DataGridViewRow row in grilla_totales.Rows)
            {
                string S = Convert.ToString(row.Cells["T_SELECCION"].Value);
                DataGridViewImageCell Imagen = row.Cells["S"] as DataGridViewImageCell;

                if (S == "S")
                {
                    Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.Check;
                }
                else
                {
                    Imagen.Value = (System.Drawing.Image)PPTO001IE.Properties.Resources.bloqueo;
                }
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (Validar_seleccion())
            {
                foreach (DataRow miRow1 in dt_totales.Select("T_SELECCION = 'S' "))
                {
                    v_selec_cama = Convert.ToInt64(miRow1["T_COD_TIPO_CAMA"].ToString());
                    v_cant_cama = Convert.ToInt64(miRow1["T_CANTIDAD"].ToString());
                    v_total_cama = Convert.ToInt64(miRow1["T_TOTAL"].ToString());
                }

               


                this.Hide();
            }
            else
            {
                Mensaje("ToTales", 7);
            }
        }

        private Boolean Validar_seleccion()
        {
            Boolean  var =false;
            int cont = 0;
            foreach (DataRow miRow1 in dt_totales.Select("T_SELECCION = 'S' "))
            {
                cont++;
            }

            if (cont > 0)
            {
                var = true;
            }

            return var;
        }

        protected void restablecer_seleccion()
        {
            foreach (DataRow miRow1 in dt_totales.Select("T_SELECCION = 'S' "))
            {
                miRow1["T_SELECCION"] = "N";
            }

        }

        private void Frm_Seleccion_Cama_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
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
                    v_estado_fai = miRow1["COD_ESTADO"].ToString();
                }


                if (v_estado_fai == "1")
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



    
    }
}
