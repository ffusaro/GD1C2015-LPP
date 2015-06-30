namespace PagoElectronico.Login
{
    partial class RolIngreso
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
            this.boxSecundario = new System.Windows.Forms.GroupBox();
            this.btnRol = new System.Windows.Forms.Button();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.boxSecundario.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxSecundario
            // 
            this.boxSecundario.Controls.Add(this.cmbRol);
            this.boxSecundario.Controls.Add(this.label4);
            this.boxSecundario.Location = new System.Drawing.Point(12, 12);
            this.boxSecundario.Name = "boxSecundario";
            this.boxSecundario.Size = new System.Drawing.Size(292, 89);
            this.boxSecundario.TabIndex = 6;
            this.boxSecundario.TabStop = false;
            this.boxSecundario.Text = "Seleccione el rol con el que desea ingresar";
            // 
            // btnRol
            // 
            this.btnRol.Location = new System.Drawing.Point(76, 107);
            this.btnRol.Name = "btnRol";
            this.btnRol.Size = new System.Drawing.Size(145, 23);
            this.btnRol.TabIndex = 2;
            this.btnRol.Text = "Ingresar al Sistema";
            this.btnRol.UseVisualStyleBackColor = true;
            this.btnRol.Click += new System.EventHandler(this.btnRol_Click);
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(64, 37);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(193, 21);
            this.cmbRol.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Rol";
            // 
            // RolIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 147);
            this.Controls.Add(this.btnRol);
            this.Controls.Add(this.boxSecundario);
            this.Name = "RolIngreso";
            this.Text = "Rol Ingreso";
            this.boxSecundario.ResumeLayout(false);
            this.boxSecundario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox boxSecundario;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRol;
    }
}