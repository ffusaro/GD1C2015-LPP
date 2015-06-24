namespace PagoElectronico.ABM_Cuenta
{
    partial class ConsultaSaldos
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
            this.cmbNroCuenta = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btTransf = new System.Windows.Forms.Button();
            this.btRetiros = new System.Windows.Forms.Button();
            this.btDepositos = new System.Windows.Forms.Button();
            this.btSaldo = new System.Windows.Forms.Button();
            this.btLimpiar = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbNroCuenta
            // 
            this.cmbNroCuenta.FormattingEnabled = true;
            this.cmbNroCuenta.Location = new System.Drawing.Point(132, 38);
            this.cmbNroCuenta.Name = "cmbNroCuenta";
            this.cmbNroCuenta.Size = new System.Drawing.Size(289, 21);
            this.cmbNroCuenta.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Número de Cuenta ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btTransf);
            this.groupBox1.Controls.Add(this.btRetiros);
            this.groupBox1.Controls.Add(this.btDepositos);
            this.groupBox1.Controls.Add(this.btSaldo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbNroCuenta);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 117);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione la cuenta y la consulta que desea hacer de la cuenta";
            // 
            // btTransf
            // 
            this.btTransf.Location = new System.Drawing.Point(316, 80);
            this.btTransf.Name = "btTransf";
            this.btTransf.Size = new System.Drawing.Size(105, 23);
            this.btTransf.TabIndex = 21;
            this.btTransf.Text = "Ver Transferencias";
            this.btTransf.UseVisualStyleBackColor = true;
            this.btTransf.Click += new System.EventHandler(this.btTransf_Click);
            // 
            // btRetiros
            // 
            this.btRetiros.Location = new System.Drawing.Point(226, 80);
            this.btRetiros.Name = "btRetiros";
            this.btRetiros.Size = new System.Drawing.Size(75, 23);
            this.btRetiros.TabIndex = 20;
            this.btRetiros.Text = "Ver Retiros";
            this.btRetiros.UseVisualStyleBackColor = true;
            this.btRetiros.Click += new System.EventHandler(this.btRetiros_Click);
            // 
            // btDepositos
            // 
            this.btDepositos.Location = new System.Drawing.Point(121, 80);
            this.btDepositos.Name = "btDepositos";
            this.btDepositos.Size = new System.Drawing.Size(90, 23);
            this.btDepositos.TabIndex = 19;
            this.btDepositos.Text = "Ver Depositos";
            this.btDepositos.UseVisualStyleBackColor = true;
            this.btDepositos.Click += new System.EventHandler(this.btDepositos_Click);
            // 
            // btSaldo
            // 
            this.btSaldo.Location = new System.Drawing.Point(30, 80);
            this.btSaldo.Name = "btSaldo";
            this.btSaldo.Size = new System.Drawing.Size(75, 23);
            this.btSaldo.TabIndex = 18;
            this.btSaldo.Text = "Ver Saldo";
            this.btSaldo.UseVisualStyleBackColor = true;
            this.btSaldo.Click += new System.EventHandler(this.btSaldo_Click);
            // 
            // btLimpiar
            // 
            this.btLimpiar.Location = new System.Drawing.Point(12, 139);
            this.btLimpiar.Name = "btLimpiar";
            this.btLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btLimpiar.TabIndex = 17;
            this.btLimpiar.Text = "Limpiar";
            this.btLimpiar.UseVisualStyleBackColor = true;
            this.btLimpiar.Click += new System.EventHandler(this.btLimpiar_Click);
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(373, 146);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(75, 23);
            this.btSalir.TabIndex = 18;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // ConsultaSaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 175);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btLimpiar);
            this.Name = "ConsultaSaldos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consultas acerca de una cuenta";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cmbNroCuenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btDepositos;
        private System.Windows.Forms.Button btSaldo;
        private System.Windows.Forms.Button btLimpiar;
        private System.Windows.Forms.Button btTransf;
        private System.Windows.Forms.Button btRetiros;
        private System.Windows.Forms.Button btSalir;

    }
}