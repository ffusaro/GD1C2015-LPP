namespace PagoElectronico.ABM_Cliente
{
    partial class AsignarUsuario
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
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnAsociar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCliente = new System.Windows.Forms.Button();
            this.btLimpiar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.btnAsociar.Text = "Asociar a Usuario";
            this.btnAsociar.UseVisualStyleBackColor = true;
            this.btnAsociar.Click += new System.EventHandler(this.btnAsociar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.btnAsociar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 86);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar el usuario al cual le quiere asignar un nuevo cliente";
            // 
            // btCliente
            // 
            this.btCliente.Location = new System.Drawing.Point(216, 104);
            this.btCliente.Name = "btCliente";
            this.btCliente.Size = new System.Drawing.Size(101, 23);
            this.btCliente.TabIndex = 34;
            this.btCliente.Text = "ABM de Cliente";
            this.btCliente.UseVisualStyleBackColor = true;
            this.btCliente.Click += new System.EventHandler(this.btCliente_Click);
            // 
            // btLimpiar
            // 
            this.btLimpiar.Location = new System.Drawing.Point(86, 104);
            this.btLimpiar.Name = "btLimpiar";
            this.btLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btLimpiar.TabIndex = 35;
            this.btLimpiar.Text = "Limpiar";
            this.btLimpiar.UseVisualStyleBackColor = true;
            this.btLimpiar.Click += new System.EventHandler(this.btLimpiar_Click);
            // 
            // AsignarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 135);
            this.Controls.Add(this.btLimpiar);
            this.Controls.Add(this.btCliente);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "AsignarUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AsignarUsuario";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnAsociar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCliente;
        private System.Windows.Forms.Button btLimpiar;
    }
}