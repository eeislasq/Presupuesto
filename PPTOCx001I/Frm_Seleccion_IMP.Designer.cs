namespace PPTO001IE
{
    partial class Frm_Seleccion_IMP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gr_grilla = new System.Windows.Forms.GroupBox();
            this.Ayuda = new AyudaSpreadNet.AyudaSprNet();
            this.grilla_prestaciones = new System.Windows.Forms.DataGridView();
            this.txttitulo = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txttotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdescripcion = new System.Windows.Forms.TextBox();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRIPCION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOM_PRESTACION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cod_tipo_cama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOM_CONCEPTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gr_grilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_prestaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // gr_grilla
            // 
            this.gr_grilla.Controls.Add(this.txtdescripcion);
            this.gr_grilla.Controls.Add(this.label1);
            this.gr_grilla.Controls.Add(this.Ayuda);
            this.gr_grilla.Controls.Add(this.grilla_prestaciones);
            this.gr_grilla.Controls.Add(this.txttitulo);
            this.gr_grilla.Location = new System.Drawing.Point(5, 1);
            this.gr_grilla.Name = "gr_grilla";
            this.gr_grilla.Size = new System.Drawing.Size(669, 349);
            this.gr_grilla.TabIndex = 1;
            this.gr_grilla.TabStop = false;
            // 
            // Ayuda
            // 
            this.Ayuda.AnchoColumnas = null;
            this.Ayuda.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Ayuda.Location = new System.Drawing.Point(712, -16);
            this.Ayuda.Multi_Seleccion = false;
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Nombre_BD_Datos = null;
            this.Ayuda.NombreColumnas = null;
            this.Ayuda.Package = null;
            this.Ayuda.Pass = null;
            this.Ayuda.Procedimiento = null;
            this.Ayuda.Size = new System.Drawing.Size(58, 66);
            this.Ayuda.TabIndex = 223;
            this.Ayuda.TipoBase = 0;
            this.Ayuda.TituloConsulta = null;
            this.Ayuda.User = null;
            this.Ayuda.UseWaitCursor = true;
            this.Ayuda.Visible = false;
            // 
            // grilla_prestaciones
            // 
            this.grilla_prestaciones.AllowUserToAddRows = false;
            this.grilla_prestaciones.AllowUserToDeleteRows = false;
            this.grilla_prestaciones.AllowUserToResizeColumns = false;
            this.grilla_prestaciones.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilla_prestaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.grilla_prestaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_prestaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CODIGO,
            this.DESCRIPCION,
            this.CANTIDAD,
            this.VALOR,
            this.NOM_PRESTACION,
            this.Cod_tipo_cama,
            this.NOM_CONCEPTO,
            this.TIPO});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grilla_prestaciones.DefaultCellStyle = dataGridViewCellStyle14;
            this.grilla_prestaciones.Location = new System.Drawing.Point(0, 68);
            this.grilla_prestaciones.Name = "grilla_prestaciones";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilla_prestaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.grilla_prestaciones.RowHeadersVisible = false;
            this.grilla_prestaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilla_prestaciones.Size = new System.Drawing.Size(666, 275);
            this.grilla_prestaciones.TabIndex = 239;
            // 
            // txttitulo
            // 
            this.txttitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttitulo.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttitulo.ForeColor = System.Drawing.Color.White;
            this.txttitulo.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.txttitulo.Location = new System.Drawing.Point(-6, 0);
            this.txttitulo.Name = "txttitulo";
            this.txttitulo.Size = new System.Drawing.Size(672, 25);
            this.txttitulo.TabIndex = 238;
            this.txttitulo.Text = "Días Camas";
            this.txttitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "S";
            this.dataGridViewImageColumn1.Image = global::PPTO001IE.Properties.Resources.bloqueo;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 25;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "E";
            this.dataGridViewImageColumn2.Image = global::PPTO001IE.Properties.Resources.Delete;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 25;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.HeaderText = "M";
            this.dataGridViewImageColumn3.Image = global::PPTO001IE.Properties.Resources.ver_ficha;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Width = 25;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(496, 353);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(23, 31);
            this.label26.TabIndex = 275;
            this.label26.Text = ":";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(410, 353);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 31);
            this.label25.TabIndex = 274;
            this.label25.Text = "Total";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txttotal
            // 
            this.txttotal.AutoSize = true;
            this.txttotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotal.ForeColor = System.Drawing.Color.Red;
            this.txttotal.Location = new System.Drawing.Point(512, 353);
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(30, 31);
            this.txttotal.TabIndex = 273;
            this.txttotal.Text = "0";
            this.txttotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 240;
            this.label1.Text = "Descripción";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Location = new System.Drawing.Point(97, 34);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Size = new System.Drawing.Size(493, 20);
            this.txtdescripcion.TabIndex = 241;
            this.txtdescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdescripcion_KeyPress);
            // 
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            this.CODIGO.HeaderText = "Codigo";
            this.CODIGO.Name = "CODIGO";
            // 
            // DESCRIPCION
            // 
            this.DESCRIPCION.DataPropertyName = "DESCRIPCION";
            this.DESCRIPCION.HeaderText = "Descripcion";
            this.DESCRIPCION.Name = "DESCRIPCION";
            this.DESCRIPCION.ReadOnly = true;
            this.DESCRIPCION.Width = 380;
            // 
            // CANTIDAD
            // 
            this.CANTIDAD.DataPropertyName = "CANTIDAD";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CANTIDAD.DefaultCellStyle = dataGridViewCellStyle12;
            this.CANTIDAD.HeaderText = "Cantidad";
            this.CANTIDAD.MaxInputLength = 3;
            this.CANTIDAD.Name = "CANTIDAD";
            this.CANTIDAD.Width = 60;
            // 
            // VALOR
            // 
            this.VALOR.DataPropertyName = "VALOR";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.NullValue = null;
            this.VALOR.DefaultCellStyle = dataGridViewCellStyle13;
            this.VALOR.HeaderText = "Valor";
            this.VALOR.Name = "VALOR";
            this.VALOR.ReadOnly = true;
            this.VALOR.ToolTipText = "Valor ";
            // 
            // NOM_PRESTACION
            // 
            this.NOM_PRESTACION.DataPropertyName = "TOTAL_P";
            this.NOM_PRESTACION.HeaderText = "Total";
            this.NOM_PRESTACION.Name = "NOM_PRESTACION";
            this.NOM_PRESTACION.Visible = false;
            this.NOM_PRESTACION.Width = 250;
            // 
            // Cod_tipo_cama
            // 
            this.Cod_tipo_cama.DataPropertyName = "COD_CONCEPTO";
            this.Cod_tipo_cama.HeaderText = "COD_CONCEPTO";
            this.Cod_tipo_cama.Name = "Cod_tipo_cama";
            this.Cod_tipo_cama.Visible = false;
            // 
            // NOM_CONCEPTO
            // 
            this.NOM_CONCEPTO.DataPropertyName = "NOM_CONCEPTO";
            this.NOM_CONCEPTO.HeaderText = "CONCEPTO";
            this.NOM_CONCEPTO.Name = "NOM_CONCEPTO";
            this.NOM_CONCEPTO.Visible = false;
            // 
            // TIPO
            // 
            this.TIPO.DataPropertyName = "TIPO";
            this.TIPO.HeaderText = "Tipo";
            this.TIPO.Name = "TIPO";
            this.TIPO.Visible = false;
            // 
            // Frm_Seleccion_IMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 393);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.gr_grilla);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Seleccion_IMP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Conceptos Adicionales";
            this.Load += new System.EventHandler(this.Frm_Seleccion_IMP_Load);
            this.gr_grilla.ResumeLayout(false);
            this.gr_grilla.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_prestaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gr_grilla;
        private AyudaSpreadNet.AyudaSprNet Ayuda;
        private System.Windows.Forms.DataGridView grilla_prestaciones;
        private System.Windows.Forms.Label txttitulo;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label txttotal;
        private System.Windows.Forms.TextBox txtdescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRIPCION;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOM_PRESTACION;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cod_tipo_cama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOM_CONCEPTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
    }
}