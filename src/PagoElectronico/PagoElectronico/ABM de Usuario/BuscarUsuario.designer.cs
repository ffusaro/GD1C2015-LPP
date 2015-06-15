namespace PagoElectronico.ABM_de_Usuario
{
    partial class BuscarUsuario
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
            this.dgvUsuario = new System.Windows.Forms.DataGridView();
            this.btnSalir = new System.Windows.Forms.Button();
            this.gpDatosFiltrados = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUsuarios = new System.Windows.Forms.TextBox();
            this.btnLimp = new System.Windows.Forms.Button();
            this.btnBusca = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).BeginInit();
            this.gpDatosFiltrados.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUsuario
            // 
            this.dgvUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuario.Location = new System.Drawing.Point(15, 36);
            this.dgvUsuario.Name = "dgvUsuario";
            this.dgvUsuario.Size = new System.Drawing.Size(715, 146);
            this.dgvUsuario.TabIndex = 1;
            this.dgvUsuario.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuario_CellDoubleClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(289, 190);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(167, 36);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // gpDatosFiltrados
            // 
            this.gpDatosFiltrados.Controls.Add(this.label6);
            this.gpDatosFiltrados.Controls.Add(this.btnSalir);
            this.gpDatosFiltrados.Controls.Add(this.dgvUsuario);
            this.gpDatosFiltrados.Location = new System.Drawing.Point(9, 70);
            this.gpDatosFiltrados.Name = "gpDatosFiltrados";
            this.gpDatosFiltrados.Size = new System.Drawing.Size(748, 231);
            this.gpDatosFiltrados.TabIndex = 3;
            this.gpDatosFiltrados.TabStop = false;
            this.gpDatosFiltrados.Text = "Datos Filtrados";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Doble click en el Usuario que quiera Modificar/Eliminar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUsuarios);
            this.groupBox1.Controls.Add(this.btnLimp);
            this.groupBox1.Controls.Add(this.btnBusca);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(746, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // txtUsuarios
            // 
            this.txtUsuarios.Location = new System.Drawing.Point(127, 21);
            this.txtUsuarios.Name = "txtUsuarios";
            this.txtUsuarios.Size = new System.Drawing.Size(178, 20);
            this.txtUsuarios.TabIndex = 5;
            this.txtUsuarios.TextChanged += new System.EventHandler(this.txtUsuarios_TextChanged);
            // 
            // btnLimp
            // 
            this.btnLimp.Location = new System.Drawing.Point(540, 12);
            this.btnLimp.Name = "btnLimp";
            this.btnLimp.Size = new System.Drawing.Size(167, 36);
            this.btnLimp.TabIndex = 4;
            this.btnLimp.Text = "Limpiar";
            this.btnLimp.UseVisualStyleBackColor = true;
            this.btnLimp.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBusca
            // 
            this.btnBusca.Location = new System.Drawing.Point(352, 12);
            this.btnBusca.Name = "btnBusca";
            this.btnBusca.Size = new System.Drawing.Size(167, 36);
            this.btnBusca.TabIndex = 3;
            this.btnBusca.Text = "Buscar";
            this.btnBusca.UseVisualStyleBackColor = true;
            this.btnBusca.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Usuario";
            // 
            // BuscarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 351);
            this.Controls.Add(this.gpDatosFiltrados);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "BuscarUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Usuario";
            this.Load += new System.EventHandler(this.BuscarUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).EndInit();
            this.gpDatosFiltrados.ResumeLayout(false);
            this.gpDatosFiltrados.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuario;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox gpDatosFiltrados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimp;
        private System.Windows.Forms.Button btnBusca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUsuarios;
    }
}