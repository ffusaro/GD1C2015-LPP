namespace PagoElectronico.ABM_Cuenta
{
    partial class AsignarUsuarioCuenta
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
            this.btLimpiar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnAsociar = new System.Windows.Forms.Button();
            this.txtCancelar = new System.Windows.Forms.Button();
            this.btnCuenta = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLimpiar
            // 
            this.btLimpiar.Location = new System.Drawing.Point(32, 107);
            this.btLimpiar.Name = "btLimpiar";
            this.btLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btLimpiar.TabIndex = 39;
            this.btLimpiar.Text = "Limpiar";
            this.btLimpiar.UseVisualStyleBackColor = true;
            this.btLimpiar.Click += new System.EventHandler(this.btLimpiar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.btnAsociar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 86);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar el usuario al cual le quiere asignar una nueva cuenta";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(15, 39);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(185, 20);
            this.txtUsuario.TabIndex = 32;
            // 
            // btnAsociar
            // 
            this.btnAsociar.Location = new System.Drawing.Point(226, 37);
            this.btnAsociar.Name = "btnAsociar";
            this.btnAsociar.Size = new System.Drawing.Size(123, 23);
            this.btnAsociar.TabIndex = 31;
            this.btnAsociar.Text = "Asociar a Cuenta";
            this.btnAsociar.UseVisualStyleBackColor = true;
            this.btnAsociar.Click += new System.EventHandler(this.btnAsociar_Click);
            // 
            // txtCancelar
            // 
            this.txtCancelar.Location = new System.Drawing.Point(156, 107);
            this.txtCancelar.Name = "txtCancelar";
            this.txtCancelar.Size = new System.Drawing.Size(75, 23);
            this.txtCancelar.TabIndex = 40;
            this.txtCancelar.Text = "Cancelar";
            this.txtCancelar.UseVisualStyleBackColor = true;
            this.txtCancelar.Click += new System.EventHandler(this.txtCancelar_Click);
            // 
            // btnCuenta
            // 
            this.btnCuenta.Location = new System.Drawing.Point(274, 106);
            this.btnCuenta.Name = "btnCuenta";
            this.btnCuenta.Size = new System.Drawing.Size(75, 23);
            this.btnCuenta.TabIndex = 41;
            this.btnCuenta.Text = "ABM Cuenta";
            this.btnCuenta.UseVisualStyleBackColor = true;
            this.btnCuenta.Click += new System.EventHandler(this.btnCuenta_Click);
            // 
            // AsignarUsuarioCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 142);
            this.Controls.Add(this.btnCuenta);
            this.Controls.Add(this.txtCancelar);
            this.Controls.Add(this.btLimpiar);
            this.Controls.Add(this.groupBox1);
            this.Name = "AsignarUsuarioCuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AsignarUsuarioCuenta";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLimpiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnAsociar;
        private System.Windows.Forms.Button txtCancelar;
        private System.Windows.Forms.Button btnCuenta;
    }
}