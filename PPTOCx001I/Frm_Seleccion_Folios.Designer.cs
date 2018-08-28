namespace PPTO001IE
{
    partial class Frm_Seleccion_Folios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gr_contenedor = new System.Windows.Forms.GroupBox();
            this.gr_concepto = new System.Windows.Forms.GroupBox();
            this.grilla_conceptos_folios = new System.Windows.Forms.DataGridView();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOM_CONCEPTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_REAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_ACTUAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROMEDIO_C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.gr_folio = new System.Windows.Forms.GroupBox();
            this.txtcant = new System.Windows.Forms.Label();
            this.grilla_folios = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtdispersion_may = new System.Windows.Forms.TextBox();
            this.ck_disp = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtdispersion_men = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtdispersion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gr_prestaciones = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtplan_prev = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtinstitucion = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtprevision = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.FOLIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA_CREACION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMBRES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALFOLIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXTREMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIGMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROMEDIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gr_contenedor.SuspendLayout();
            this.gr_concepto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_conceptos_folios)).BeginInit();
            this.gr_folio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_folios)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gr_prestaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr_contenedor
            // 
            this.gr_contenedor.Controls.Add(this.gr_concepto);
            this.gr_contenedor.Controls.Add(this.gr_folio);
            this.gr_contenedor.Controls.Add(this.groupBox1);
            this.gr_contenedor.Controls.Add(this.gr_prestaciones);
            this.gr_contenedor.Location = new System.Drawing.Point(4, 3);
            this.gr_contenedor.Name = "gr_contenedor";
            this.gr_contenedor.Size = new System.Drawing.Size(975, 518);
            this.gr_contenedor.TabIndex = 0;
            this.gr_contenedor.TabStop = false;
            // 
            // gr_concepto
            // 
            this.gr_concepto.Controls.Add(this.grilla_conceptos_folios);
            this.gr_concepto.Controls.Add(this.label9);
            this.gr_concepto.Location = new System.Drawing.Point(551, 142);
            this.gr_concepto.Name = "gr_concepto";
            this.gr_concepto.Size = new System.Drawing.Size(424, 289);
            this.gr_concepto.TabIndex = 262;
            this.gr_concepto.TabStop = false;
            // 
            // grilla_conceptos_folios
            // 
            this.grilla_conceptos_folios.AllowUserToAddRows = false;
            this.grilla_conceptos_folios.AllowUserToDeleteRows = false;
            this.grilla_conceptos_folios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_conceptos_folios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CODIGO,
            this.NOM_CONCEPTO,
            this.TOTAL_REAL,
            this.TOTAL_ACTUAL,
            this.PROMEDIO_C});
            this.grilla_conceptos_folios.Location = new System.Drawing.Point(4, 32);
            this.grilla_conceptos_folios.Name = "grilla_conceptos_folios";
            this.grilla_conceptos_folios.ReadOnly = true;
            this.grilla_conceptos_folios.RowHeadersVisible = false;
            this.grilla_conceptos_folios.Size = new System.Drawing.Size(420, 250);
            this.grilla_conceptos_folios.TabIndex = 265;
            this.grilla_conceptos_folios.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grilla_conceptos_folios_CellPainting);
            // 
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            this.CODIGO.HeaderText = "CODIGO";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            this.CODIGO.Visible = false;
            // 
            // NOM_CONCEPTO
            // 
            this.NOM_CONCEPTO.DataPropertyName = "NOM_CONCEPTO";
            this.NOM_CONCEPTO.HeaderText = "Conceptos";
            this.NOM_CONCEPTO.Name = "NOM_CONCEPTO";
            this.NOM_CONCEPTO.ReadOnly = true;
            this.NOM_CONCEPTO.Width = 150;
            // 
            // TOTAL_REAL
            // 
            this.TOTAL_REAL.DataPropertyName = "TOTAL_REAL";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.TOTAL_REAL.DefaultCellStyle = dataGridViewCellStyle1;
            this.TOTAL_REAL.HeaderText = "Total Real";
            this.TOTAL_REAL.Name = "TOTAL_REAL";
            this.TOTAL_REAL.ReadOnly = true;
            // 
            // TOTAL_ACTUAL
            // 
            this.TOTAL_ACTUAL.DataPropertyName = "TOTAL_ACTUAL";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.TOTAL_ACTUAL.DefaultCellStyle = dataGridViewCellStyle2;
            this.TOTAL_ACTUAL.HeaderText = "Total Actual";
            this.TOTAL_ACTUAL.Name = "TOTAL_ACTUAL";
            this.TOTAL_ACTUAL.ReadOnly = true;
            // 
            // PROMEDIO_C
            // 
            this.PROMEDIO_C.DataPropertyName = "PROMEDIO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.PROMEDIO_C.DefaultCellStyle = dataGridViewCellStyle3;
            this.PROMEDIO_C.HeaderText = "Promedio";
            this.PROMEDIO_C.Name = "PROMEDIO_C";
            this.PROMEDIO_C.ReadOnly = true;
            this.PROMEDIO_C.Width = 70;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.label9.Location = new System.Drawing.Point(4, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(420, 25);
            this.label9.TabIndex = 264;
            this.label9.Text = "Listado Conceptos  ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gr_folio
            // 
            this.gr_folio.Controls.Add(this.txtcant);
            this.gr_folio.Controls.Add(this.grilla_folios);
            this.gr_folio.Controls.Add(this.label8);
            this.gr_folio.Location = new System.Drawing.Point(5, 142);
            this.gr_folio.Name = "gr_folio";
            this.gr_folio.Size = new System.Drawing.Size(540, 289);
            this.gr_folio.TabIndex = 260;
            this.gr_folio.TabStop = false;
            // 
            // txtcant
            // 
            this.txtcant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcant.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcant.ForeColor = System.Drawing.Color.White;
            this.txtcant.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.txtcant.Location = new System.Drawing.Point(374, 7);
            this.txtcant.Name = "txtcant";
            this.txtcant.Size = new System.Drawing.Size(166, 25);
            this.txtcant.TabIndex = 265;
            this.txtcant.Text = "  ";
            this.txtcant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grilla_folios
            // 
            this.grilla_folios.AllowUserToAddRows = false;
            this.grilla_folios.AllowUserToDeleteRows = false;
            this.grilla_folios.AllowUserToResizeColumns = false;
            this.grilla_folios.AllowUserToResizeRows = false;
            this.grilla_folios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_folios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FOLIO,
            this.FECHA_CREACION,
            this.NOMBRES,
            this.TOTALFOLIO,
            this.EXTREMO,
            this.SIGMA,
            this.PROMEDIO});
            this.grilla_folios.Location = new System.Drawing.Point(-1, 33);
            this.grilla_folios.Name = "grilla_folios";
            this.grilla_folios.ReadOnly = true;
            this.grilla_folios.RowHeadersVisible = false;
            this.grilla_folios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilla_folios.Size = new System.Drawing.Size(541, 250);
            this.grilla_folios.TabIndex = 264;
            this.grilla_folios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilla_folios_CellDoubleClick);
            this.grilla_folios.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grilla_folios_CellPainting);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.label8.Location = new System.Drawing.Point(-1, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(541, 25);
            this.label8.TabIndex = 263;
            this.label8.Text = "Listado Folios  ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtdispersion_may);
            this.groupBox1.Controls.Add(this.ck_disp);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtdispersion_men);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtdispersion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(0, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(975, 64);
            this.groupBox1.TabIndex = 259;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(462, 62);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 100);
            this.groupBox3.TabIndex = 262;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(738, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 261;
            this.label7.Text = "Y";
            // 
            // txtdispersion_may
            // 
            this.txtdispersion_may.BackColor = System.Drawing.Color.White;
            this.txtdispersion_may.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdispersion_may.Location = new System.Drawing.Point(767, 32);
            this.txtdispersion_may.MaxLength = 10;
            this.txtdispersion_may.Name = "txtdispersion_may";
            this.txtdispersion_may.ReadOnly = true;
            this.txtdispersion_may.Size = new System.Drawing.Size(136, 20);
            this.txtdispersion_may.TabIndex = 260;
            // 
            // ck_disp
            // 
            this.ck_disp.AutoSize = true;
            this.ck_disp.Location = new System.Drawing.Point(91, 36);
            this.ck_disp.Name = "ck_disp";
            this.ck_disp.Size = new System.Drawing.Size(15, 14);
            this.ck_disp.TabIndex = 259;
            this.ck_disp.UseVisualStyleBackColor = true;
            this.ck_disp.CheckedChanged += new System.EventHandler(this.ck_disp_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(548, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 258;
            this.label3.Text = "Entre";
            // 
            // txtdispersion_men
            // 
            this.txtdispersion_men.BackColor = System.Drawing.Color.White;
            this.txtdispersion_men.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdispersion_men.Location = new System.Drawing.Point(590, 33);
            this.txtdispersion_men.MaxLength = 10;
            this.txtdispersion_men.Name = "txtdispersion_men";
            this.txtdispersion_men.ReadOnly = true;
            this.txtdispersion_men.Size = new System.Drawing.Size(142, 20);
            this.txtdispersion_men.TabIndex = 256;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(306, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 255;
            this.label4.Text = "Dispersión:";
            // 
            // txtdispersion
            // 
            this.txtdispersion.BackColor = System.Drawing.Color.White;
            this.txtdispersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdispersion.Location = new System.Drawing.Point(385, 34);
            this.txtdispersion.MaxLength = 10;
            this.txtdispersion.Name = "txtdispersion";
            this.txtdispersion.ReadOnly = true;
            this.txtdispersion.Size = new System.Drawing.Size(160, 20);
            this.txtdispersion.TabIndex = 253;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(112, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 252;
            this.label5.Text = "Excluir Extremos:";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.label6.Location = new System.Drawing.Point(-9, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(984, 25);
            this.label6.TabIndex = 239;
            this.label6.Text = "Datos Dispersión ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gr_prestaciones
            // 
            this.gr_prestaciones.Controls.Add(this.label1);
            this.gr_prestaciones.Controls.Add(this.txtplan_prev);
            this.gr_prestaciones.Controls.Add(this.label2);
            this.gr_prestaciones.Controls.Add(this.txtinstitucion);
            this.gr_prestaciones.Controls.Add(this.label18);
            this.gr_prestaciones.Controls.Add(this.txtprevision);
            this.gr_prestaciones.Controls.Add(this.label11);
            this.gr_prestaciones.Location = new System.Drawing.Point(0, 0);
            this.gr_prestaciones.Name = "gr_prestaciones";
            this.gr_prestaciones.Size = new System.Drawing.Size(975, 78);
            this.gr_prestaciones.TabIndex = 0;
            this.gr_prestaciones.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 258;
            this.label1.Text = "Plan Prev.:";
            // 
            // txtplan_prev
            // 
            this.txtplan_prev.BackColor = System.Drawing.Color.White;
            this.txtplan_prev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtplan_prev.Location = new System.Drawing.Point(91, 51);
            this.txtplan_prev.MaxLength = 10;
            this.txtplan_prev.Name = "txtplan_prev";
            this.txtplan_prev.ReadOnly = true;
            this.txtplan_prev.Size = new System.Drawing.Size(812, 20);
            this.txtplan_prev.TabIndex = 256;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(306, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 255;
            this.label2.Text = "Institución:";
            // 
            // txtinstitucion
            // 
            this.txtinstitucion.BackColor = System.Drawing.Color.White;
            this.txtinstitucion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtinstitucion.Location = new System.Drawing.Point(385, 27);
            this.txtinstitucion.MaxLength = 10;
            this.txtinstitucion.Name = "txtinstitucion";
            this.txtinstitucion.ReadOnly = true;
            this.txtinstitucion.Size = new System.Drawing.Size(518, 20);
            this.txtinstitucion.TabIndex = 253;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(4, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 13);
            this.label18.TabIndex = 252;
            this.label18.Text = "Previsión:";
            // 
            // txtprevision
            // 
            this.txtprevision.BackColor = System.Drawing.Color.White;
            this.txtprevision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtprevision.Location = new System.Drawing.Point(91, 26);
            this.txtprevision.MaxLength = 10;
            this.txtprevision.Name = "txtprevision";
            this.txtprevision.ReadOnly = true;
            this.txtprevision.Size = new System.Drawing.Size(179, 20);
            this.txtprevision.TabIndex = 250;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.label11.Location = new System.Drawing.Point(-9, -1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(984, 25);
            this.label11.TabIndex = 239;
            this.label11.Text = "Datos Previsión";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Selec.";
            this.dataGridViewImageColumn1.Image = global::PPTO001IE.Properties.Resources.seo;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // FOLIO
            // 
            this.FOLIO.DataPropertyName = "FOLIO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FOLIO.DefaultCellStyle = dataGridViewCellStyle4;
            this.FOLIO.HeaderText = "Folio";
            this.FOLIO.Name = "FOLIO";
            this.FOLIO.ReadOnly = true;
            this.FOLIO.Width = 60;
            // 
            // FECHA_CREACION
            // 
            this.FECHA_CREACION.DataPropertyName = "FECHA_CREACION";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.FECHA_CREACION.DefaultCellStyle = dataGridViewCellStyle5;
            this.FECHA_CREACION.HeaderText = "Fecha ";
            this.FECHA_CREACION.MaxInputLength = 10;
            this.FECHA_CREACION.Name = "FECHA_CREACION";
            this.FECHA_CREACION.ReadOnly = true;
            this.FECHA_CREACION.Width = 70;
            // 
            // NOMBRES
            // 
            this.NOMBRES.DataPropertyName = "NOMBRES";
            this.NOMBRES.HeaderText = "Nombres";
            this.NOMBRES.Name = "NOMBRES";
            this.NOMBRES.ReadOnly = true;
            this.NOMBRES.Width = 280;
            // 
            // TOTALFOLIO
            // 
            this.TOTALFOLIO.DataPropertyName = "TOTALFOLIO";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.TOTALFOLIO.DefaultCellStyle = dataGridViewCellStyle6;
            this.TOTALFOLIO.HeaderText = "Total";
            this.TOTALFOLIO.Name = "TOTALFOLIO";
            this.TOTALFOLIO.ReadOnly = true;
            this.TOTALFOLIO.Width = 60;
            // 
            // EXTREMO
            // 
            this.EXTREMO.DataPropertyName = "EXTREMO";
            this.EXTREMO.HeaderText = "EXTREMO";
            this.EXTREMO.Name = "EXTREMO";
            this.EXTREMO.ReadOnly = true;
            this.EXTREMO.Visible = false;
            // 
            // SIGMA
            // 
            this.SIGMA.DataPropertyName = "SIGMA";
            this.SIGMA.HeaderText = "SIGMA";
            this.SIGMA.Name = "SIGMA";
            this.SIGMA.ReadOnly = true;
            this.SIGMA.Visible = false;
            // 
            // PROMEDIO
            // 
            this.PROMEDIO.DataPropertyName = "PROMEDIO";
            this.PROMEDIO.HeaderText = "PROMEDIO";
            this.PROMEDIO.Name = "PROMEDIO";
            this.PROMEDIO.ReadOnly = true;
            this.PROMEDIO.Visible = false;
            // 
            // Frm_Seleccion_Folios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 522);
            this.Controls.Add(this.gr_contenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Seleccion_Folios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Seleccion_Folios";
            this.Load += new System.EventHandler(this.Frm_Seleccion_Folios_Load);
            this.gr_contenedor.ResumeLayout(false);
            this.gr_concepto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_conceptos_folios)).EndInit();
            this.gr_folio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_folios)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gr_prestaciones.ResumeLayout(false);
            this.gr_prestaciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr_contenedor;
        private System.Windows.Forms.GroupBox gr_prestaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtprevision;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ck_disp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtdispersion_men;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtdispersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtplan_prev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtinstitucion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtdispersion_may;
        private System.Windows.Forms.GroupBox gr_concepto;
        private System.Windows.Forms.DataGridView grilla_conceptos_folios;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gr_folio;
        private System.Windows.Forms.DataGridView grilla_folios;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOM_CONCEPTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_REAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_ACTUAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROMEDIO_C;
        private System.Windows.Forms.Label txtcant;
        private System.Windows.Forms.DataGridViewTextBoxColumn FOLIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA_CREACION;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMBRES;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALFOLIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXTREMO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIGMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROMEDIO;
    }
}