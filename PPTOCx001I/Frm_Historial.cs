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
    public partial class Frm_Historial : Form
    {
        #region Variables

        #region Variables Comunes

        Int64 v_id_plan = 0;
        Int64 v_cod_plan = 0;
        Int64 v_cod_anulacion = 0;
        Int64 v_estado_fai = 0;
        string v_usuario_grilla = "";
        string v_usuario_sistema = "";
        string v_super_usuario = "";
        
        


        #endregion

        #region Variable DataTable

       public  DataTable dt_historial = new DataTable();


        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion

        #endregion
        public Frm_Historial()
        {
            InitializeComponent();
        }

        private void Frm_Historial_Load(object sender, EventArgs e)
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
                v_usuario_sistema = ValidaMenu.LeeUsuarioMenu().Equals(string.Empty) ? "SICI" : ValidaMenu.LeeUsuarioMenu();
            }

        }
        #endregion 


          public void Inicializar_datos(Int64  id_plan, Int64 cod_plan,string nombre,string usuario_sistema,string usuario_grilla,string super_usuario)
        {
            Conectar();
            v_id_plan = id_plan;
            v_cod_plan = cod_plan;
            txtnum_plan.Text = cod_plan.ToString();
            txtnombre.Text = nombre;
            v_usuario_sistema = usuario_sistema;
            v_usuario_grilla = usuario_grilla;
            v_super_usuario = super_usuario;
            Cargar_grilla();
     
        }

          public void Cargar_grilla()
          {
              dt_historial.Clear();
              if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
              CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PRESUPUESTO_HISTORIAL");
              CnnFalp.ParametroBD("PIN_CORRETATIVO", 0, DbType.Int64, ParameterDirection.Input);
              CnnFalp.ParametroBD("PIN_PRES_CPLAN_ID", v_id_plan, DbType.Int64, ParameterDirection.Input);
              dt_historial.Load(CnnFalp.ExecuteReader());
              grilla_historial.DataSource = dt_historial;
          }

          private void grilla_historial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
          {
              Frm_Presupuesto frm_p = new Frm_Presupuesto();
              if (e.RowIndex >= -1)
              {
                  Int64 cod_presupuesto = Convert.ToInt64(grilla_historial.Rows[e.RowIndex].Cells["COD_PRESUPUESTO"].Value.ToString());
                  Int64 correlativo = Convert.ToInt64(grilla_historial.Rows[e.RowIndex].Cells["CORRELATIVO"].Value.ToString());
                  Int64 cod_estado = Convert.ToInt64(grilla_historial.Rows[e.RowIndex].Cells["COD_ESTADO"].Value.ToString());

                  if (e.ColumnIndex == 0)
                  {
                      DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Anular el Presupuesto N°" + cod_presupuesto + "", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                      if (opc == DialogResult.Yes)
                      {
                          if (cod_estado > 1)
                          {
                              if (v_usuario_grilla.ToUpper() == v_usuario_sistema.ToUpper() || v_super_usuario != "")
                              {
                                 
                                  v_estado_fai = Extraer_estado(cod_presupuesto);
                            
                                  if (v_estado_fai == 1)
                                  {
                                      Cargar_anulaciones();
                                      string res = Anular_presupuesto(cod_presupuesto);
                                      if (res == "ok")
                                      {
                                          Mensaje("", 1);
                                          Grabar_Historial_Anular(cod_presupuesto, cod_estado);
                                      }
                                  }
                                  else
                                  {
                                      switch (v_estado_fai)
                                      {
                                          case 0: Mensaje("NO EXISTE ESTADO", 3); break;
                                          case 2: Mensaje("CERRADA", 3); break;
                                          case 3: Mensaje("ANULADA", 3); break;
                                          case 4: Mensaje("NO CONCRETADA", 3); break;
                                      }
                                  }
                              }
                              else
                              {
                                  Mensaje("", 4);
                              }
                          }
                          else
                          {
                              Mensaje("", 2);
                          }   
                      }
                  }
                  else
                  {
                      if (e.ColumnIndex == 1)
                      {
                           DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Imprimir el Presupuesto N°" + cod_presupuesto + "", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                           if (opc == DialogResult.Yes)
                           {
                               Imprimir(cod_presupuesto);
                           }
                      }

                      else
                      {

                          DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Ingresar Este Presupuesto N° " + cod_presupuesto + "", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                          if (opc == DialogResult.Yes)
                          {
                              frm_p.Inicializar_datos(0, 0, cod_presupuesto, correlativo, v_usuario_grilla, v_super_usuario, cod_estado);
                              frm_p.ShowDialog();
                          }

                      }
                  
                  }
                 

              }
              Cargar_grilla();
          
          }

          private void Imprimir(Int64 v_cod_presupuesto)
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
                 // Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\Rpt_Presupuesto.rpt";

                  FrmImprime Frm = new FrmImprime(Reporte);
                  Frm.Show(this);
                
                  Reporte.GenerarParametro("PIN_PRES_ID", v_cod_presupuesto);
                  Reporte.GenerarParametro("PIN_SELECCION", "N");
                     Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\PPTOEHCOS\Rpt_Presupuestos.rpt";
               //   Reporte.Ruta_Reporte = Application.StartupPath + @"\..\Reportes\Rpt_Presupuestos.rpt";
                 FrmImprime Frm1 = new FrmImprime(Reporte);
                 Frm1.Show(this);
                



              }
              catch (Exception ex)
              {
                  MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }

          private void grilla_historial_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                  e.Graphics.DrawString(grilla_historial.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                  e.Handled = true;
                  drawBrush.Dispose();
              }
          }


          protected void Cargar_anulaciones()
          {
              Cargar_Parametros(ref Ayuda, 136, "", "ANULACIÓN");

              if (!Ayuda.EOF())
              {
                  v_cod_anulacion = Convert.ToInt32(Ayuda.Fields(0));
              }
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

          protected string Anular_presupuesto(Int64 cod_presupuesto)
          {
              string res="";
              try
              {

                  if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

                  CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_ANULAR_PRESUPUESTO");

                  CnnFalp.ParametroBD("PIN_COD_PRESUPUESTO", cod_presupuesto, DbType.Int64, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_COD_MOTIVO", v_cod_anulacion, DbType.Int64, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_USUARIO", v_usuario_sistema, DbType.String, ParameterDirection.Input);

                  int registro = CnnFalp.ExecuteNonQuery();

                  CnnFalp.Cerrar();
                  if (v_estado_fai == 1)
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

          protected Int32 Extraer_estado(Int64 cod_presupuesto)
          {
              Int32 valor = 0;
              DataTable dt = new DataTable();
              dt.Clear();
              if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
              CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_VALIDAR_ESTADO_FAI");
              CnnFalp.ParametroBD("PIN_PRES_ID", cod_presupuesto, DbType.Int64, ParameterDirection.Input);
              dt.Load(CnnFalp.ExecuteReader());
              string v = "";
              foreach (DataRow miRow1 in dt.Rows)
              {
                  v = miRow1["COD_ESTADO"].ToString(); 
              }

              if (v == "")
              {
                  valor=0;
              }
              else
              {
                  valor = Convert.ToInt32(v);
              }
              return valor;

          }


          protected void Grabar_Historial_Anular(Int64 cod_presupuesto,Int64 cod_estado)
          {

              try
              {

                  if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

                  CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GRABAR_ESTADO_HISTORIAL");

                  CnnFalp.ParametroBD("PIN_EPRES_ID", 0, DbType.Int32, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_EPRES_PRES_ID", cod_presupuesto, DbType.Int32, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_EPRES_ESTADO_ANT", cod_estado, DbType.Int32, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_EPRES_ESTADO_NUE", 4, DbType.Int32, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_EPRES_USUARIO", v_usuario_sistema, DbType.String, ParameterDirection.Input);
                  CnnFalp.ParametroBD("PIN_EPRES_MOTIVO", v_cod_anulacion, DbType.Int32, ParameterDirection.Input);

                  int registro = CnnFalp.ExecuteNonQuery();
                  if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

              }
              catch (Exception ex)
              {
                  throw ex;
              }

          }


        
        

          protected void Mensaje(string descripcion, int tipo)
          {
              switch (tipo)
              {
                  case 1: MessageBox.Show("Estimado Usuario, El Presupuesto Fue Anulado Correctamente ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                  case 2: MessageBox.Show("Estimado Usuario, El Presupuesto no puede ser Anulado ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                  case 3: MessageBox.Show("Estimado Usuario, Error  En el Estado Fai " + descripcion + ", por ende no se puede Anular", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                  case 4: MessageBox.Show("Estimado Usuario, No esta Autorizado, para Eliminar este Presupuesto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
           
              }

          }
    }
}
