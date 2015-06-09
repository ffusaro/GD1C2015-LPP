namespace PagoElectronico.Retiros
{
    partial class RetiroDeEfectivo
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
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grpRetiros = new System.Windows.Forms.GroupBox();
            this.cmbNroCuenta = new System.Windows.Forms.ComboBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.boxBotones.SuspendLayout();
            this.grpRetiros.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxBotones
            // 
            this.boxBotones.Controls.Add(this.btnLimpiar);
            this.boxBotones.Controls.Add(this.btnSalir);
            this.boxBotones.Controls.Add(this.btnGrabar);
            this.boxBotones.Controls.Add(this.btnNuevo);
            this.boxBotones.Location = new System.Drawing.Point(16, 191);
            this.boxBotones.Name = "boxBotones";
            this.boxBotones.Size = new System.Drawing.Size(385, 68);
            this.boxBotones.TabIndex = 4;
            this.boxBotones.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(105, 29);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(290, 29);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(199, 29);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 3;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(10, 29);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // grpRetiros
            // 
            this.grpRetiros.Controls.Add(this.cmbMoneda);
            this.grpRetiros.Controls.Add(this.label4);
            this.grpRetiros.Controls.Add(this.cmbNroCuenta);
            this.grpRetiros.Controls.Add(this.txtImporte);
            this.grpRetiros.Controls.Add(this.label2);
            this.grpRetiros.Controls.Add(this.label1);
            this.grpRetiros.Location = new System.Drawing.Point(16, 10);
            this.grpRetiros.Name = "grpRetiros";
            this.grpRetiros.Size = new System.Drawing.Size(385, 175);
            this.grpRetiros.TabIndex = 3;
            this.grpRetiros.TabStop = false;
            this.grpRetiros.Text = "Datos Retiro";
            // 
            // cmbNroCuenta
            // 
            this.cmbNroCuenta.FormattingEnabled = true;
            this.cmbNroCuenta.Location = new System.Drawing.Point(147, 37);
            this.cmbNroCuenta.Name = "cmbNroCuenta";
            this.cmbNroCuenta.Size = new System.Drawing.Size(210, 21);
            this.cmbNroCuenta.TabIndex = 5;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(147, 84);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(210, 20);
            this.txtImporte.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Importe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de Cuenta";
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(147, 130);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(210, 21);
            this.cmbMoneda.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tipo de Moneda";
            // 
            // RetiroDeEfectivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 267);
            this.Controls.Add(this.boxBotones);
            this.Controls.Add(this.grpRetiros);
            this.Name = "RetiroDeEfectivo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retiro de Efectivo";
            this.boxBotones.ResumeLayout(false);
            this.grpRetiros.ResumeLayout(false);
            this.grpRetiros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox boxBotones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grpRetiros;
        private System.Windows.Forms.ComboBox cmbNroCuenta;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMoneda;
        private System.Windows.Forms.Label label4;

    }
}