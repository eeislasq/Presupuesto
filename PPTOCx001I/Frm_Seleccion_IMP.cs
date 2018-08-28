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
    public partial class Frm_Seleccion_IMP : Form
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
        Int64 v_cod_concepto = 0;
        Int64 v_valor = 0;
        string v_nom = "";
        string v_mov = "";
        public Int64 v_selec_cama = 0;
        public Int64 v_cant_cama = 0;
        public Int64 v_total_cama = 0;
        string usuario = "";
        string v_folios = "";

        #endregion

        #region Variable DataTable


        public DataTable dt_medyins = new DataTable();
        public DataTable dt_prestadi = new DataTable();
        public DataTable dt_medyins_total = new DataTable();
        public DataTable dt_prestadi_total = new DataTable();

        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion

        #endregion
        public Frm_Seleccion_IMP()
        {
            InitializeComponent();
        }

        private void Frm_Seleccion_IMP_Load(object sender, EventArgs e)
        {

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


        public void Inicializar_datos(Int64 id_plan, Int64 cod_plan,  Int64 cod_institucion, Int64 cod_prevision, Int64 cod_plan_prevision,Int64 cod_concepto, string folios)
        {
            Conectar();

            if (v_selec_cama == 0 && v_cant_cama == 0 && v_total_cama == 0)
            {
                v_id_plan = id_plan;
                v_cod_plan = cod_plan;
                v_cod_institucion = cod_institucion;
                v_cod_prevision = cod_prevision;
                v_cod_plan_prevision = cod_plan_prevision;


                if (cod_concepto == 10 || cod_concepto == 11 || cod_concepto == 17)
                {
                    switch (cod_concepto)
                     {
                         case 10:    txttitulo.Text = "Fármacos";  break;
                         case 11:    txttitulo.Text = "Insumos";break;
                         case 17:    txttitulo.Text = "Drogas"; break;
                     }
                  
                    v_folios = folios;
                    v_cod_concepto = cod_concepto;
                    Cargar_grilla_IM();
                }
                else
                {
                    txttitulo.Text = "Prestaciones Adicionales";
                    Cargar_grilla_P();
                }             
            }

        }

        public void Cargar_grilla_IM()
        {

            dt_medyins.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
        //    CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DET_IM");
         //   CnnFalp.ParametroBD("PIN_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_FAR_INS_DROG");
              CnnFalp.ParametroBD("PIN_FOLIOS", v_folios, DbType.String, ParameterDirection.Input);
              CnnFalp.ParametroBD("PIN_COD_CONCEPTO", v_cod_concepto, DbType.Int64, ParameterDirection.Input);
              CnnFalp.ParametroBD("PIN_DESCRIPCION", txtdescripcion.Text, DbType.String, ParameterDirection.Input);
            dt_medyins.Load(CnnFalp.ExecuteReader());
            grilla_prestaciones.DataSource = dt_medyins;

            if (txtdescripcion.Text == "")
            {
                txttotal.Text = Total_ins().ToString("#,##0");
            }
        }

        public Int64 Total_ins()
        {
            Int64 valor = 0;
            foreach (DataRow miRow1 in dt_medyins.Rows)
            {
                valor = valor + Convert.ToInt64(miRow1["VALOR"].ToString().Replace(".", ""));
            }
            return valor;
        }

        public void Cargar_grilla_P()
        {
            dt_prestadi.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DET_PRESTACION");
            CnnFalp.ParametroBD("PIN_CODIGO", v_id_plan, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_INSTITUCION", v_cod_institucion, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_PLANPREVISION", v_cod_plan_prevision, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_UO_NUMERO", "00002", DbType.String, ParameterDirection.Input);
            dt_prestadi.Load(CnnFalp.ExecuteReader());
            grilla_prestaciones.DataSource = dt_prestadi;


        }

        private void txtdescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
                return;
            }
            else
            {
                if (e.KeyChar == (char)13)
                {

                    Cargar_grilla_IM();
                }
            }
        }

  

    }
}
