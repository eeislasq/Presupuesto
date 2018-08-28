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
    public partial class Frm_Seleccion_HM : Form
    {
        #region Variables

        #region Variables Comunes

        Int64 v_id_plan = 0;
        Int64 v_cod_plan = 0;
        Int64 v_cod_presupuesto = 0;
        Int64 v_cod_institucion = 0;
        Int64 v_cod_prevision = 0;
        Int64 v_cod_plan_prevision = 0;
        string v_cod_fonasa="";
        string usuario = "";

        #endregion

        #region Variable DataTable


        public DataTable dt_hm = new DataTable();
        public DataTable dt_prestaciones = new DataTable();
        public DataTable dt_fono = new DataTable();
        #endregion

        #region Variables Conexion

        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string PCK = "PCK_PPTO001I";

        #endregion

        #endregion

        public Frm_Seleccion_HM()
        {
            InitializeComponent();
        }

        private void Frm_Seleccion_HM_Load(object sender, EventArgs e)
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


        public void Inicializar_datos(Int64 id_plan, Int64 cod_plan, Int64 cod_institucion, Int64 cod_prevision, Int64 cod_plan_prevision, string cod_fonasa,DataTable dt ,Int64  cod_presupuesto,DataTable dt_prest)
        {
            Conectar();

            
                v_id_plan = id_plan;
                v_cod_plan = cod_plan;
                v_cod_institucion = cod_institucion;
                v_cod_prevision = cod_prevision;
                v_cod_plan_prevision = cod_plan_prevision;
                v_cod_fonasa=cod_fonasa;
                dt_hm = dt;
                dt_prestaciones = dt_prest;
                v_cod_presupuesto = cod_presupuesto;
                grilla_fonasa.DataSource = dt_prestaciones;

                if (v_cod_presupuesto == 0)
                {
                    if (dt_hm.Rows.Count == 0)
                    {
                        Cargar_grilla_HM();
                    }
                    else
                    {
                        grilla_hm.DataSource = dt_hm;
                    }
                }

                else
                {
                    Cargar_grilla_HM_Presupuesto();
                    gr_grilla.Enabled = false;
                }

                if (dt_hm.Rows.Count > 0)
                {
                    gr_calculo.Enabled = false;
                }
                else
                {
                    gr_calculo.Enabled = true;
                }
                txttotal.Text = Calcula_Precio_M().ToString("#,##0");
        }



        public void Cargar_grilla_HM()
        {
            string cod = "";
            dt_hm.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_DET_HM");
            CnnFalp.ParametroBD("PIN_CODIGO", v_cod_fonasa, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_TIPOPREVISION",v_cod_prevision , DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_INSTITUCION", v_cod_institucion, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_PLANPREVISION", v_cod_plan_prevision, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_TIPOCALCULO",1, DbType.String, ParameterDirection.Input);
            dt_hm.Load(CnnFalp.ExecuteReader());
            grilla_hm.DataSource = dt_hm;

            foreach (DataRow miRow1 in dt_hm.Rows)
            {
                cod = miRow1["COD_FONASA"].ToString().Trim();
               
            }
            if (cod == "0" || cod == "")
            {
                dt_hm.Clear();
            }

            if (dt_hm.Rows.Count == 0)
            {
                int cont = 1;
                    foreach (DataRow miRow1 in dt_prestaciones.Rows)
                    {
                        if (cont == 1)
                        {
                            txtcod_fonasa.Text = miRow1["COD_FONASA"].ToString();
                            buscar_factor();
                            txtcod_fonasa.Enabled = false;
                           

                        }
                    }
                
            }
        }

        public void Cargar_grilla_HM_Presupuesto()
        {
            string cod = "";
            dt_hm.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_HM_PRESUPUESTO");
            CnnFalp.ParametroBD("PIN_PRES_ID", v_cod_presupuesto, DbType.String, ParameterDirection.Input);

            dt_hm.Load(CnnFalp.ExecuteReader());
            grilla_hm.DataSource = dt_hm;

            foreach (DataRow miRow1 in dt_hm.Rows)
            {
                cod = miRow1["COD_FONASA"].ToString();

            }
            if (cod == "0" || cod == "")
            {
                dt_hm.Clear();
            }

            txttotal.Text = Calcula_Precio_M().ToString("#,##0");
        }

        private void grilla_hm_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                e.Graphics.DrawString(grilla_hm.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

                e.Handled = true;
                drawBrush.Dispose();
            }
        }

        private void grilla_hm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
              

                if (e.ColumnIndex == 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Cambiar los Valores de HM ", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                        gr_calculo.Enabled = true;
                        btn_agregar_hm.Visible = false;
                        btn_confirmar.Visible = true;
                        txtcod_fonasa.Enabled = false;

                    
                        double factor = Convert.ToDouble(grilla_hm.Rows[e.RowIndex].Cells["FACTOR"].Value.ToString());
                        txtcod_fonasa.Text = grilla_hm.Rows[e.RowIndex].Cells["COD_FONASA"].Value.ToString();

                        string fa = grilla_hm.Rows[e.RowIndex].Cells["FACTOR"].Value.ToString();
                        if (fa.Length == 1)
                        {
                            txtfactor.Text = grilla_hm.Rows[e.RowIndex].Cells["FACTOR"].Value.ToString().Substring(0, 1);
                        }
                        else
                        {
                            txtfactor.Text = grilla_hm.Rows[e.RowIndex].Cells["FACTOR"].Value.ToString().Substring(0, 3);
                        }
                        txtciru_1.Text = (Math.Round(Convert.ToInt32(grilla_hm.Rows[e.RowIndex].Cells["CIRUJANO1_FACT"].Value.ToString().Replace(".","")) / factor)).ToString();
                        txtciru_2.Text = (Math.Round(Convert.ToInt32(grilla_hm.Rows[e.RowIndex].Cells["CIRUJANO2_FACT"].Value.ToString().Replace(".", "")) / factor)).ToString();
                        txtciru_3.Text = (Math.Round(Convert.ToInt32(grilla_hm.Rows[e.RowIndex].Cells["CIRUJANO3_FACT"].Value.ToString().Replace(".", "")) / factor)).ToString();
                        txtciru_4.Text = (Math.Round(Convert.ToInt32(grilla_hm.Rows[e.RowIndex].Cells["CIRUJANO4_FACT"].Value.ToString().Replace(".", "")) / factor)).ToString();
                        txtanestesista.Text = (Math.Round(Convert.ToInt32(grilla_hm.Rows[e.RowIndex].Cells["ANESTESISTA_FACT"].Value.ToString().Replace(".", "")) / factor)).ToString();

                    }
                }

            }
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
           
                foreach (DataRow miRow1 in dt_hm.Select("COD_FONASA='" + txtcod_fonasa.Text + "'"))
                {
                    double factor = Convert.ToDouble(validar_factor(txtfactor.Text));

                    double c1 = txtciru_1.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_1.Text) * Convert.ToDouble(factor));
                    double c2 = txtciru_2.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_2.Text) * Convert.ToDouble(factor));
                    double c3 = txtciru_3.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_3.Text) * Convert.ToDouble(factor));
                    double c4 = txtciru_4.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_4.Text) * Convert.ToDouble(factor));
                    double a = txtanestesista.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtanestesista.Text) * Convert.ToDouble(factor));

                     miRow1["FACTOR"] = validar_factor(txtfactor.Text);
                      miRow1["CIRUJANO1_FACT"] = Math.Round(c1).ToString();
                      miRow1["CIRUJANO2_FACT"] = Math.Round(c2).ToString();
                      miRow1["CIRUJANO3_FACT"] = Math.Round(c3).ToString();
                      miRow1["CIRUJANO4_FACT"] = Math.Round(c4).ToString();
                      miRow1["ANESTESISTA_FACT"] = Math.Round(a).ToString();

                      miRow1["VALOR_HHMM"] = Math.Round(c1 + c2 + c3 + c4 + a).ToString();              
                }

                dt_hm.AcceptChanges();
                Mensaje("", 1);
         

            btn_agregar_hm.Visible = true;
            btn_confirmar.Visible = false;
            limpiar(this);
            gr_calculo.Enabled = false;
            txttotal.Text = Calcula_Precio_M().ToString("#,##0");
        }

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
         
        }

        public  Int64 Total_hm()
        {
            Int64 valor = 0;
            foreach (DataRow miRow1 in dt_hm.Rows)
            {
                valor = valor + Convert.ToInt64(miRow1["VALOR_HHMM"].ToString().Replace(".",""));
            }
            return valor;
        }


        protected void Mensaje(string descripcion, int tipo)
        {
            switch (tipo)
            {
                case 1: MessageBox.Show("Estimado Usuario, Fue Modificado Correctamente la Información ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 2: MessageBox.Show("Estimado Usuario, El Codigo Fonasa "+txtcod_fonasa.Text.Trim() +" ya existe en la Grilla", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 3: MessageBox.Show("Estimado Usuario, El Factor Ingresado " + txtfactor.Text.Trim() + " esta fuera de rango entre  1 y 6", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); break; 
            }

        }

        private void btn_agregar_hm_Click(object sender, EventArgs e)
        {
            if (!Validar_exist_Codigo())
            {
                double factor = Convert.ToDouble(validar_factor(txtfactor.Text));
                double c1 = txtciru_1.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_1.Text) * factor);
                double c2 = txtciru_2.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_2.Text) * factor);
                double c3 = txtciru_3.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_3.Text) * factor);
                double c4 = txtciru_4.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtciru_4.Text) * factor);
                double a = txtanestesista.Text.Equals(string.Empty) ? 0 : Math.Round(Convert.ToDouble(txtanestesista.Text) * factor);
                DataRow Fila = dt_hm.NewRow();

                Fila["COD_FONASA"] = txtcod_fonasa.Text.Trim();
                Fila["FACTOR"] = validar_factor(txtfactor.Text);
                Fila["CIRUJANO1_FACT"] = Math.Round(c1).ToString();
                Fila["CIRUJANO2_FACT"] = Math.Round(c2).ToString();
                Fila["CIRUJANO3_FACT"] = Math.Round(c3).ToString();
                Fila["CIRUJANO4_FACT"] = Math.Round(c4).ToString();
                Fila["ANESTESISTA_FACT"] = Math.Round(a).ToString();
                Fila["VALOR_HHMM"] = Math.Round(c1 + c2 + c3 + c4 + a).ToString();
                if (dt_hm.Rows.Count ==0)
                {
                    Fila["PORCENTAJE"] = "100%";
                }
                else
                {
                    Fila["PORCENTAJE"] = "50%";
                }
                dt_hm.Rows.Add(Fila);
                grilla_hm.DataSource = new DataView(dt_hm, "", "", DataViewRowState.CurrentRows);
            }
            else
            {
                Mensaje("", 0);
            }
            limpiar(this);
            gr_calculo.Enabled = false;
            txttotal.Text = Calcula_Precio_M().ToString("#,##0");
        }

        private void txtcod_fonasa_KeyPress(object sender, KeyPressEventArgs e)
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
                    if (Validar_codigo())
                    {
                        buscar_factor();
                        txtciru_1.Focus();
                    }
                }
            }
        }

        private void txtciru_1_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtciru_2.Focus();
                }
            }
        }

        private void txtciru_2_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtciru_3.Focus();
                }
            }
        }

        private void txtciru_3_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtciru_4.Focus();
                }
            }
        }

        private void txtciru_4_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtanestesista.Focus();
                }
            }
        }

        private void txtanestesista_KeyPress(object sender, KeyPressEventArgs e)
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
                    btn_agregar_hm.Focus();
                }
            }
        }


        private Boolean Validar_codigo()
        {
            Boolean var= false;

            foreach (DataRow miRow1 in dt_prestaciones.Select("COD_FONASA like '%" + txtcod_fonasa.Text.Trim() + "%'"))
            {
                var=true;
                txtcod_fonasa.Text = miRow1["COD_FONASA"].ToString().Trim();
            }
            return var;
        }

        private Boolean Validar_exist_Codigo()
        {
            Boolean var = false;

            double fact = Convert.ToDouble(validar_factor(txtfactor.Text));
            if (fact < 0 && fact > 6)
            {
                Mensaje("Factor", 5);
                var = true;
            }
            else
            {
                var = false;
                foreach (DataRow miRow1 in dt_hm.Select("COD_FONASA = '" + txtcod_fonasa.Text + "'"))
                {
                    var = true;
                }
            }
            return var;
        }

        private void  buscar_factor()
        {
           DataTable dt=new DataTable ();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_FACTOR");
            CnnFalp.ParametroBD("PIN_CODIGO", txtcod_fonasa.Text, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_INSTITUCION", v_cod_institucion, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_COD_PLANPREVISION", v_cod_plan_prevision, DbType.Int64, ParameterDirection.Input);
            dt.Load(CnnFalp.ExecuteReader());

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow miRow1 in dt.Rows)
                {
                    txtfactor.Text = miRow1["FACTOR"].ToString();
                }
            }
            else
            {
                txtfactor.Text = "1";
            }
        }

        private void grilla_fonasa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
                Int64 codigo = Convert.ToInt64(grilla_fonasa.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString());
                if (e.ColumnIndex > 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Seleccionar El Codigo Fonasa " + codigo + " ", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                        gr_calculo.Enabled = true;
                        txtcod_fonasa.Enabled = false;
                        txtcod_fonasa.Text = codigo.ToString();
                        if (!Validar_exist_Codigo())
                        {
                            buscar_factor();
                        }
                        else
                        {
                            Mensaje("",2);
                            gr_calculo.Enabled = false;
                            limpiar(this);
                        }

                    }
                }

            }
        }

        private Int64 Calcula_Precio_M()
        {
            Int64 total = 0;
            foreach (DataRow miRow1 in dt_hm.Rows)
            {
                total += Convert.ToInt64(miRow1["VALOR_HHMM"].ToString());

            }
            return total;
        }

        private void txtfactor_KeyPress(object sender, KeyPressEventArgs e)
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
                    
                    string val = validar_factor(txtfactor.Text);
                    if (Convert.ToDouble(val) > 0 && Convert.ToDouble(val) < 6)
                    {
                        txtciru_1.Focus();
                    }
                    else
                    {
                        Mensaje("Factor", 5);
                    }
                }
            }
        }

        private string validar_factor(string valor)
        {
            string cadena="";
            if(valor=="" || valor == null)
            {
                valor = "1";
            }

            if (valor.Length == 1)
            {
                string n1 = valor.Substring(0, 1);
                cadena = n1;
                txtfactor.Text = cadena;            
            }
            else
            {
                if (valor.Length == 2)
                {
                    string n1 = valor.Substring(0, 1);
                    string n2 = valor.Substring(1, 1);
                    cadena = n1 + "," + n2;
                    txtfactor.Text = cadena;
                }
                else
                    {
                        if (valor.Length == 3)
                        {
                             string n1 = valor.Substring(0,1);
                             string n2 = valor.Substring(2);
                             cadena = n1 + "," + n2;
                             txtfactor.Text = cadena;
                         }

                     }
            }

            return cadena;
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            btn_agregar_hm.Visible = true;
            btn_confirmar.Visible = false;
            limpiar(this);
            gr_calculo.Enabled = false;
            txttotal.Text = Calcula_Precio_M().ToString("#,##0");
        }


    }
}
