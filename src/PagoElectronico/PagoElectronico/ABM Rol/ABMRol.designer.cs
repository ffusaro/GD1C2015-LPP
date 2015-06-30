namespace PagoElectronico.ABM_Rol
{
    partial class ABMRol
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
            this.boxBotones = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.boxRol = new System.Windows.Forms.GroupBox();
            this.chkListFuncionalidades = new System.Windows.Forms.CheckedListBox();
            this.chkBoxHabilitado = new System.Windows.Forms.CheckBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.boxBotones.SuspendLayout();
            this.boxRol.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxBotones
            // 
            this.boxBotones.Controls.Add(this.btnLimpiar);
            this.boxBotones.Controls.Add(this.btnSalir);
            this.boxBotones.Controls.Add(this.btnGrabar);
            this.boxBotones.Location = new System.Drawing.Point(12, 286);
            this.boxBotones.Name = "boxBotones";
            this.boxBotones.Size = new System.Drawing.Size(324, 68);
            this.boxBotones.TabIndex = 2;
            this.boxBotones.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(123, 29);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(225, 29);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(19, 29);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 3;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // boxRol
            // 
            this.boxRol.Controls.Add(this.chkListFuncionalidades);
            this.boxRol.Controls.Add(this.chkBoxHabilitado);
            this.boxRol.Controls.Add(this.txtNombre);
            this.boxRol.Controls.Add(this.lblNombre);
            this.boxRol.Location = new System.Drawing.Point(12, 12);
            this.boxRol.Name = "boxRol";
            this.boxRol.Size = new System.Drawing.Size(324, 268);
            this.boxRol.TabIndex = 3;
            this.boxRol.TabStop = false;
            this.boxRol.Text = "Datos";
            // 
            // chkListFuncionalidades
            // 
            this.chkListFuncionalidades.FormattingEnabled = true;
            this.chkListFuncionalidades.Location = new System.Drawing.Point(22, 91);
            this.chkListFuncionalidades.Name = "chkListFuncionalidades";
            this.chkListFuncionalidades.Size = new System.Drawing.Size(281, 169);
            this.chkListFuncionalidades.TabIndex = 6;
            // 
            // chkBoxHabilitado
            // 
            this.chkBoxHabilitado.AutoSize = true;
            this.chkBoxHabilitado.Location = new System.Drawing.Point(88, 66);
            this.chkBoxHabilitado.Name = "chkBoxHabilitado";
            this.chkBoxHabilitado.Size = new System.Drawing.Size(73, 17);
            this.chkBoxHabilitado.TabIndex = 5;
            this.chkBoxHabilitado.Text = "Habilitado";
            this.chkBoxHabilitado.UseVisualStyleBackColor = true;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(87, 33);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(198, 20);
            this.txtNombre.TabIndex = 4;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(16, 33);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // ABMRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 366);
            this.Controls.Add(this.boxRol);
            this.Controls.Add(this.boxBotones);
            this.MaximizeBox = false;
            this.Name = "ABMRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizar Rol";
            this.boxBotones.ResumeLayout(false);
            this.boxRol.ResumeLayout(false);
            this.boxRol.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox boxBotones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.GroupBox boxRol;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.CheckBox chkBoxHabilitado;
        private System.Windows.Forms.CheckedListBox chkListFuncionalidades;
    }
}