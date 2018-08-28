namespace PPTO001IE
{
    partial class Frm_Historial
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grilla_historial = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Ayuda = new AyudaSpreadNet.AyudaSprNet();
            this.txtnombre = new System.Windows.Forms.Label();
            this.txtnum_plan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.A = new System.Windows.Forms.DataGridViewImageColumn();
            this.R = new System.Windows.Forms.DataGridViewImageColumn();
            this.COD_PRESUPUESTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COD_PLAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA_EMISION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA_CONCRETADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA_ANULADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USUARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CORRELATIVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_HC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_HM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COD_ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_historial)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grilla_historial);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(4, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 238);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grilla_historial
            // 
            this.grilla_historial.AllowUserToAddRows = false;
            this.grilla_historial.AllowUserToDeleteRows = false;
            this.grilla_historial.AllowUserToResizeColumns = false;
            this.grilla_historial.AllowUserToResizeRows = false;
            this.grilla_historial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_historial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.A,
            this.R,
            this.COD_PRESUPUESTO,
            this.COD_PLAN,
            this.FECHA_EMISION,
            this.FECHA_CONCRETADO,
            this.FECHA_ANULADO,
            this.USUARIO,
            this.ESTADO,
            this.CORRELATIVO,
            this.TOTAL_HC,
            this.TOTAL_HM,
            this.COD_ESTADO});
            this.grilla_historial.Location = new System.Drawing.Point(0, 27);
            this.grilla_historial.Name = "grilla_historial";
            this.grilla_historial.ReadOnly = true;
            this.grilla_historial.RowHeadersVisible = false;
            this.grilla_historial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilla_historial.Size = new System.Drawing.Size(627, 205);
            this.grilla_historial.TabIndex = 238;
            this.grilla_historial.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilla_historial_CellDoubleClick);
            this.grilla_historial.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grilla_historial_CellPainting);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Image = global::PPTO001IE.Properties.Resources.HeaderGV1024;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(627, 25);
            this.label11.TabIndex = 237;
            this.label11.Text = "Listado Historial";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Ayuda);
            this.groupBox2.Controls.Add(this.txtnombre);
            this.groupBox2.Controls.Add(this.txtnum_plan);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Location = new System.Drawing.Point(4, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(633, 58);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // Ayuda
            // 
            this.Ayuda.AnchoColumnas = null;
            this.Ayuda.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Ayuda.Location = new System.Drawing.Point(436, -8);
            this.Ayuda.Multi_Seleccion = false;
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Nombre_BD_Datos = null;
            this.Ayuda.NombreColumnas = null;
            this.Ayuda.Package = null;
            this.Ayuda.Pass = null;
            this.Ayuda.Procedimiento = null;
            this.Ayuda.Size = new System.Drawing.Size(58, 66);
            this.Ayuda.TabIndex = 236;
            this.Ayuda.TipoBase = 0;
            this.Ayuda.TituloConsulta = null;
            this.Ayuda.User = null;
            this.Ayuda.UseWaitCursor = true;
            this.Ayuda.Visible = false;
            // 
            // txtnombre
            // 
            this.txtnombre.AutoSize = true;
            this.txtnombre.BackColor = System.Drawing.Color.Transparent;
            this.txtnombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombre.ForeColor = System.Drawing.Color.Red;
            this.txtnombre.Location = new System.Drawing.Point(68, 33);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(11, 13);
            this.txtnombre.TabIndex = 235;
            this.txtnombre.Text = ".";
            // 
            // txtnum_plan
            // 
            this.txtnum_plan.AutoSize = true;
            this.txtnum_plan.BackColor = System.Drawing.Color.Transparent;
            this.txtnum_plan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnum_plan.ForeColor = System.Drawing.Color.Red;
            this.txtnum_plan.Location = new System.Drawing.Point(68, 16);
            this.txtnum_plan.Name = "txtnum_plan";
            this.txtnum_plan.Size = new System.Drawing.Size(11, 13);
            this.txtnum_plan.TabIndex = 234;
            this.txtnum_plan.Text = ".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 233;
            this.label1.Text = "Nombre:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(8, 16);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 13);
            this.label28.TabIndex = 232;
            this.label28.Text = "N° Plan:";
            // 
            // A
            // 
            this.A.HeaderText = "A";
            this.A.Image = global::PPTO001IE.Properties.Resources.delete_doc;
            this.A.Name = "A";
            this.A.ReadOnly = true;
            this.A.Visible = false;
            this.A.Width = 25;
            // 
            // R
            // 
            this.R.HeaderText = "R";
            this.R.Image = global::PPTO001IE.Properties.Resources.reportes;
            this.R.Name = "R";
            this.R.ReadOnly = true;
            this.R.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.R.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.R.Visible = false;
            this.R.Width = 25;
            // 
            // COD_PRESUPUESTO
            // 
            this.COD_PRESUPUESTO.DataPropertyName = "COD_PRESUPUESTO";
            this.COD_PRESUPUESTO.HeaderText = "N° Presupuesto";
            this.COD_PRESUPUESTO.Name = "COD_PRESUPUESTO";
            this.COD_PRESUPUESTO.ReadOnly = true;
            this.COD_PRESUPUESTO.Width = 120;
            // 
            // COD_PLAN
            // 
            this.COD_PLAN.DataPropertyName = "COD_PLAN";
            this.COD_PLAN.HeaderText = "COD_PLAN";
            this.COD_PLAN.Name = "COD_PLAN";
            this.COD_PLAN.ReadOnly = true;
            this.COD_PLAN.Visible = false;
            // 
            // FECHA_EMISION
            // 
            this.FECHA_EMISION.DataPropertyName = "FECHA_EMISION";
            this.FECHA_EMISION.HeaderText = "Fecha Emisión";
            this.FECHA_EMISION.Name = "FECHA_EMISION";
            this.FECHA_EMISION.ReadOnly = true;
            // 
            // FECHA_CONCRETADO
            // 
            this.FECHA_CONCRETADO.DataPropertyName = "FECHA_CONCRETADO";
            this.FECHA_CONCRETADO.HeaderText = "FECHA_CONCRETADO";
            this.FECHA_CONCRETADO.Name = "FECHA_CONCRETADO";
            this.FECHA_CONCRETADO.ReadOnly = true;
            this.FECHA_CONCRETADO.Visible = false;
            // 
            // FECHA_ANULADO
            // 
            this.FECHA_ANULADO.DataPropertyName = "FECHA_ANULADO";
            this.FECHA_ANULADO.HeaderText = "Fecha Anulación";
            this.FECHA_ANULADO.Name = "FECHA_ANULADO";
            this.FECHA_ANULADO.ReadOnly = true;
            // 
            // USUARIO
            // 
            this.USUARIO.DataPropertyName = "Usuario";
            this.USUARIO.HeaderText = "Usuario";
            this.USUARIO.Name = "USUARIO";
            this.USUARIO.ReadOnly = true;
            this.USUARIO.Width = 120;
            // 
            // ESTADO
            // 
            this.ESTADO.DataPropertyName = "NOM_ESTADO";
            this.ESTADO.HeaderText = "Estado";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.ReadOnly = true;
            this.ESTADO.Width = 150;
            // 
            // CORRELATIVO
            // 
            this.CORRELATIVO.DataPropertyName = "CORRELATIVO";
            this.CORRELATIVO.HeaderText = "CORRELATIVO";
            this.CORRELATIVO.Name = "CORRELATIVO";
            this.CORRELATIVO.ReadOnly = true;
            this.CORRELATIVO.Visible = false;
            // 
            // TOTAL_HC
            // 
            this.TOTAL_HC.DataPropertyName = "TOTAL_HC";
            this.TOTAL_HC.HeaderText = "TOTAL_HC";
            this.TOTAL_HC.Name = "TOTAL_HC";
            this.TOTAL_HC.ReadOnly = true;
            this.TOTAL_HC.Visible = false;
            // 
            // TOTAL_HM
            // 
            this.TOTAL_HM.DataPropertyName = "TOTAL_HM";
            this.TOTAL_HM.HeaderText = "TOTAL_HM";
            this.TOTAL_HM.Name = "TOTAL_HM";
            this.TOTAL_HM.ReadOnly = true;
            this.TOTAL_HM.Visible = false;
            // 
            // COD_ESTADO
            // 
            this.COD_ESTADO.DataPropertyName = "COD_ESTADO";
            this.COD_ESTADO.HeaderText = "COD_ESTADO";
            this.COD_ESTADO.Name = "COD_ESTADO";
            this.COD_ESTADO.ReadOnly = true;
            this.COD_ESTADO.Visible = false;
            // 
            // Frm_Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 306);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Historial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.Frm_Historial_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_historial)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label txtnombre;
        private System.Windows.Forms.Label txtnum_plan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.DataGridView grilla_historial;
        private System.Windows.Forms.Label label11;
        private AyudaSpreadNet.AyudaSprNet Ayuda;
        private System.Windows.Forms.DataGridViewImageColumn A;
        private System.Windows.Forms.DataGridViewImageColumn R;
        private System.Windows.Forms.DataGridViewTextBoxColumn COD_PRESUPUESTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn COD_PLAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA_EMISION;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA_CONCRETADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA_ANULADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn USUARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CORRELATIVO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_HC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_HM;
        private System.Windows.Forms.DataGridViewTextBoxColumn COD_ESTADO;
    }
}