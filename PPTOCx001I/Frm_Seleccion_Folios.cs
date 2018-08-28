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
    public partial class Frm_Seleccion_Folios : Form
    {
        #region Variables

        #region Variables Comunes

        Int64 v_id_plan = 0;
        Int64 v_cod_plan = 0;
        Int64 v_cod_presupuesto = 0;
        Int64 v_cod_institucion = 0;
        Int64 v_cod_prevision = 0;
        Int64 v_cod_plan_prevision = 0;
        Int64 v_cod_cama = 0;
        Int64 v_valor = 0;
        Int64 cod_folio = 0;
        Int64 Excluir = 0;
        int cantidadFolios = 0;
        string folios = "";
        string v_nom = "";
        string v_mov = "";
        string v_estado_fai = "";
        string usuario = "";
        string prestaciones = "";
        public Int64 v_selec_cama = 0;
        public Int64 v_cant_cama = 0;
        public Int64 v_total_cama = 0;

        #endregion

        #region Variable DataTable

        public DataTable dt_folio = new DataTable();
        public DataTable dt_conceptos_folios = new DataTable();

        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion

        #endregion
        public Frm_Seleccion_Folios()
        {
            InitializeComponent();
        }

        private void Frm_Seleccion_Folios_Load(object sender, EventArgs e)
        {
            Conectar();
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

        public void Inicializar_datos(Int64 id_plan, Int64 cod_plan, Int64 cod_presupuesto, Int64 cod_institucion, Int64 cod_prevision, Int64 cod_plan_prevision,string nom_institucion,string nom_prevision,string plan_prev, string cod_prestaciones, DataTable dt, int v_excluir)
        {
            Conectar();
                v_id_plan = id_plan;
                v_cod_plan = cod_plan;
                v_cod_presupuesto = cod_presupuesto;
                v_cod_institucion = cod_institucion;
                v_cod_prevision = cod_prevision;
                v_cod_plan_prevision = cod_plan_prevision;
                txtprevision.Text = nom_institucion;
                txtinstitucion.Text = nom_prevision;
                txtplan_prev.Text = plan_prev;
                prestaciones = cod_prestaciones;
                dt_folio = dt;
                if (dt_folio.Rows.Count > 0)
                {
                    grilla_folios.DataSource = dt_folio;
                }
                else
                {
                    Buscar_Folios(prestaciones);
                    grilla_folios.DataSource = dt_folio;
                }
                Filtrar_concepto();
                if (v_excluir == 1) { Excluir = 1; ck_disp.Checked = true; }
                else { Excluir = 2; ck_disp.Checked = false;  }
                Calcular_disperciones();
        }

        protected void Buscar_Folios(string prestaciones)
        {
           
            Cargar_Folios(prestaciones, 12);
            if (!dt_folio.Equals(0))
            {
                if (dt_folio.Rows.Count > 0 && dt_folio.Rows.Count <= 10)
                {
                    Cargar_Folios(prestaciones, 24);
                }
            }
            else
            {
                Cargar_Folios(prestaciones, 24);
            }

            if (Validar_dt(dt_folio))
            {
                Mensaje("Folios", 6);
            }

        }
        protected void Cargar_Folios(string prestaciones, int tipo)
        {

            dt_folio.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_FOLIOS");
            CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CODIGO", prestaciones, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPOPREVISION", v_cod_prevision, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PREVISION",v_cod_institucion, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PLAN_PREVISION", v_cod_plan_prevision, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_SIGMA", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_PROMEDIO", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_EXCLUIR", 0, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_LIMITE", tipo, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_CALCULO", 0, DbType.Int64, ParameterDirection.Input);


            dt_folio.Load(CnnFalp.ExecuteReader());
            if (Validar_dt(dt_folio))
            {
                dt_folio.Clear();
            }
            int cont = dt_folio.Rows.Count;
            CnnFalp.Cerrar();

        }

        private Boolean Validar_dt(DataTable dt)
        {
            Boolean var = false;
            string cad = "";
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
        private void Calcular_disperciones()
        {
           Int64 sigma = 0;
           Int64 promedio = 0;
            foreach (DataRow miRow1 in dt_folio.Select("EXTREMO= '"+ Excluir +"'"))
            {
                sigma = Convert.ToInt64(miRow1["SIGMA"].ToString());
                promedio = Convert.ToInt64(miRow1["PROMEDIO"].ToString());
            }

            txtdispersion.Text = sigma.ToString("#,##0");
            txtdispersion_men.Text = ((sigma - promedio) * -1).ToString("#,##0");
            txtdispersion_may.Text = (sigma + promedio).ToString("#,##0");
        }
        private void grilla_folios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_folios.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        private void grilla_folios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cod_folio = Convert.ToInt64(grilla_folios.Rows[e.RowIndex].Cells["FOLIO"].Value.ToString());

                if (e.ColumnIndex >-1)
                {

                    Filtrar_concepto();
                    Buscar_conceptos_asociados();
                }

                cantidadFolios = 0;
                folios = "";
            }
        }

        private void Filtrar_concepto()
        {
            cantidadFolios = 0;
                      //folios y cantidad
            if (!dt_folio.Equals(0))
            {
                DataView dv = new DataView(dt_folio);
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

                grilla_folios.DataSource = dv;

           
            }
            else
            {
                cantidadFolios = 0;
                folios = "0";
            }
            txtcant.Text = "Cantidad Folios= " + cantidadFolios;
            string F = folios;
        }

        private void Buscar_conceptos_asociados()
        {
             if (cod_folio != 0)
            {
                dt_conceptos_folios.Clear();
                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_BUSCARVALFOLIOS");
                CnnFalp.ParametroBD("PIN_FOLIO", cod_folio, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_NUMERO", "00002", DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PREVISION", v_cod_prevision, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PLAN_PREVISION", v_cod_plan_prevision, DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CODIGO", prestaciones, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_FOLIOS", folios, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_CANTIDAD", cantidadFolios, DbType.Int64, ParameterDirection.Input);
                dt_conceptos_folios.Load(CnnFalp.ExecuteReader());
                grilla_conceptos_folios.DataSource = dt_conceptos_folios;
            }
             else
             {
                 Mensaje("", 1);

             }
        }

        private void ck_disp_CheckedChanged(object sender, EventArgs e)
        {
            
                if (ck_disp.Checked == true)
                {
                    Excluir = 1;
                }
                else
                {
                    Excluir = 2;
                }
                Filtrar_concepto();
                Calcular_disperciones();
           
        }

        private void grilla_conceptos_folios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_conceptos_folios.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        protected void Mensaje(string descripcion, int tipo)
        {
            switch (tipo)
            {
                case 1: MessageBox.Show("Estimado Usuario, Debe Seleccionar un Folio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
               
            }

        }
    }
}
