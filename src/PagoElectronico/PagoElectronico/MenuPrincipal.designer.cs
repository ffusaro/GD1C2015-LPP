namespace PagoElectronico
{
    partial class MenuPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarPasstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMHotelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarUsuarioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.depositosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMDepositoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retiroToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarReservaToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.transferenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarEstadiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listadosEstadisticosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarListadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarjetasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarConsumibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMRolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarRolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarRolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realizarFacturacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sesionToolStripMenuItem,
            this.cuentaToolStripMenuItem,
            this.usuarioToolStripMenuItem,
            this.clienteToolStripMenuItem,
            this.depositosToolStripMenuItem,
            this.retiroToolStripMenuItem1,
            this.transferenciaToolStripMenuItem,
            this.listadosEstadisticosToolStripMenuItem,
            this.tarjetasToolStripMenuItem,
            this.rolToolStripMenuItem,
            this.facturarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(904, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sesionToolStripMenuItem
            // 
            this.sesionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarPasstoolStripMenuItem,
            this.cerrarSesionToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.sesionToolStripMenuItem.Name = "sesionToolStripMenuItem";
            this.sesionToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.sesionToolStripMenuItem.Text = "Cuenta";
            // 
            // cambiarPasstoolStripMenuItem
            // 
            this.cambiarPasstoolStripMenuItem.Name = "cambiarPasstoolStripMenuItem";
            this.cambiarPasstoolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cambiarPasstoolStripMenuItem.Text = "Cambiar Contraseña";
            this.cambiarPasstoolStripMenuItem.Click += new System.EventHandler(this.cambiarPasstoolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // cuentaToolStripMenuItem
            // 
            this.cuentaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMHotelToolStripMenuItem});
            this.cuentaToolStripMenuItem.Name = "cuentaToolStripMenuItem";
            this.cuentaToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.cuentaToolStripMenuItem.Text = "Cuenta";
            this.cuentaToolStripMenuItem.Click += new System.EventHandler(this.hotelToolStripMenuItem_Click);
            // 
            // aBMHotelToolStripMenuItem
            // 
            this.aBMHotelToolStripMenuItem.Name = "aBMHotelToolStripMenuItem";
            this.aBMHotelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aBMHotelToolStripMenuItem.Text = "ABM Cuenta";
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMUsuarioToolStripMenuItem,
            this.buscarUsuarioToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // aBMUsuarioToolStripMenuItem
            // 
            this.aBMUsuarioToolStripMenuItem.Name = "aBMUsuarioToolStripMenuItem";
            this.aBMUsuarioToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aBMUsuarioToolStripMenuItem.Text = "ABM Usuario";
            this.aBMUsuarioToolStripMenuItem.Click += new System.EventHandler(this.aBMUsuarioToolStripMenuItem_Click);
            // 
            // buscarUsuarioToolStripMenuItem
            // 
            this.buscarUsuarioToolStripMenuItem.Name = "buscarUsuarioToolStripMenuItem";
            this.buscarUsuarioToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.buscarUsuarioToolStripMenuItem.Text = "Buscar Usuario";
            this.buscarUsuarioToolStripMenuItem.Click += new System.EventHandler(this.buscarUsuarioToolStripMenuItem_Click);
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMClienteToolStripMenuItem,
            this.buscarUsuarioToolStripMenuItem1});
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.clienteToolStripMenuItem.Text = "Cliente";
            // 
            // aBMClienteToolStripMenuItem
            // 
            this.aBMClienteToolStripMenuItem.Name = "aBMClienteToolStripMenuItem";
            this.aBMClienteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aBMClienteToolStripMenuItem.Text = "ABM Cliente";
            this.aBMClienteToolStripMenuItem.Click += new System.EventHandler(this.aBMClienteToolStripMenuItem_Click);
            // 
            // buscarUsuarioToolStripMenuItem1
            // 
            this.buscarUsuarioToolStripMenuItem1.Name = "buscarUsuarioToolStripMenuItem1";
            this.buscarUsuarioToolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.buscarUsuarioToolStripMenuItem1.Text = "Buscar Cliente";
            this.buscarUsuarioToolStripMenuItem1.Click += new System.EventHandler(this.buscarUsuarioToolStripMenuItem1_Click);
            // 
            // depositosToolStripMenuItem
            // 
            this.depositosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMDepositoToolStripMenuItem});
            this.depositosToolStripMenuItem.Name = "depositosToolStripMenuItem";
            this.depositosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.depositosToolStripMenuItem.Text = "Depositos";
            // 
            // aBMDepositoToolStripMenuItem
            // 
            this.aBMDepositoToolStripMenuItem.Name = "aBMDepositoToolStripMenuItem";
            this.aBMDepositoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.aBMDepositoToolStripMenuItem.Text = "Realizar Deposito";
            this.aBMDepositoToolStripMenuItem.Click += new System.EventHandler(this.aBMDepositoToolStripMenuItem_Click);
            // 
            // retiroToolStripMenuItem1
            // 
            this.retiroToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelarReservaToolStripMenuItem2});
            this.retiroToolStripMenuItem1.Name = "retiroToolStripMenuItem1";
            this.retiroToolStripMenuItem1.Size = new System.Drawing.Size(111, 20);
            this.retiroToolStripMenuItem1.Text = "Retiro en Efectivo";
            // 
            // cancelarReservaToolStripMenuItem2
            // 
            this.cancelarReservaToolStripMenuItem2.Name = "cancelarReservaToolStripMenuItem2";
            this.cancelarReservaToolStripMenuItem2.Size = new System.Drawing.Size(105, 22);
            this.cancelarReservaToolStripMenuItem2.Text = "Retiro";
            // 
            // transferenciaToolStripMenuItem
            // 
            this.transferenciaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarEstadiaToolStripMenuItem});
            this.transferenciaToolStripMenuItem.Name = "transferenciaToolStripMenuItem";
            this.transferenciaToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.transferenciaToolStripMenuItem.Text = "Transferencia";
            // 
            // registrarEstadiaToolStripMenuItem
            // 
            this.registrarEstadiaToolStripMenuItem.Name = "registrarEstadiaToolStripMenuItem";
            this.registrarEstadiaToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.registrarEstadiaToolStripMenuItem.Text = "Realizar Transferencia";
            // 
            // listadosEstadisticosToolStripMenuItem
            // 
            this.listadosEstadisticosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buscarListadosToolStripMenuItem});
            this.listadosEstadisticosToolStripMenuItem.Name = "listadosEstadisticosToolStripMenuItem";
            this.listadosEstadisticosToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.listadosEstadisticosToolStripMenuItem.Text = "Listados Estadisticos";
            // 
            // buscarListadosToolStripMenuItem
            // 
            this.buscarListadosToolStripMenuItem.Name = "buscarListadosToolStripMenuItem";
            this.buscarListadosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.buscarListadosToolStripMenuItem.Text = "Buscar Listados";
            this.buscarListadosToolStripMenuItem.Click += new System.EventHandler(this.buscarListadosToolStripMenuItem_Click);
            // 
            // tarjetasToolStripMenuItem
            // 
            this.tarjetasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarConsumibleToolStripMenuItem});
            this.tarjetasToolStripMenuItem.Name = "tarjetasToolStripMenuItem";
            this.tarjetasToolStripMenuItem.Size = new System.Drawing.Size(163, 20);
            this.tarjetasToolStripMenuItem.Text = "Asociar/Desasociar Tarjetas";
            // 
            // registrarConsumibleToolStripMenuItem
            // 
            this.registrarConsumibleToolStripMenuItem.Name = "registrarConsumibleToolStripMenuItem";
            this.registrarConsumibleToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.registrarConsumibleToolStripMenuItem.Text = "Registrar Consumible";
            // 
            // rolToolStripMenuItem
            // 
            this.rolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMRolToolStripMenuItem,
            this.modificarRolToolStripMenuItem,
            this.eliminarRolToolStripMenuItem});
            this.rolToolStripMenuItem.Name = "rolToolStripMenuItem";
            this.rolToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.rolToolStripMenuItem.Text = "Rol";
            // 
            // aBMRolToolStripMenuItem
            // 
            this.aBMRolToolStripMenuItem.Name = "aBMRolToolStripMenuItem";
            this.aBMRolToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.aBMRolToolStripMenuItem.Text = "Crear Rol";
            this.aBMRolToolStripMenuItem.Click += new System.EventHandler(this.aBMRolToolStripMenuItem_Click);
            // 
            // modificarRolToolStripMenuItem
            // 
            this.modificarRolToolStripMenuItem.Name = "modificarRolToolStripMenuItem";
            this.modificarRolToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.modificarRolToolStripMenuItem.Text = "Modificar Rol";
            this.modificarRolToolStripMenuItem.Click += new System.EventHandler(this.buscarRolToolStripMenuItem_Click);
            // 
            // eliminarRolToolStripMenuItem
            // 
            this.eliminarRolToolStripMenuItem.Name = "eliminarRolToolStripMenuItem";
            this.eliminarRolToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.eliminarRolToolStripMenuItem.Text = "Eliminar Rol";
            this.eliminarRolToolStripMenuItem.Click += new System.EventHandler(this.eliminarRolToolStripMenuItem_Click);
            // 
            // facturarToolStripMenuItem
            // 
            this.facturarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.realizarFacturacionToolStripMenuItem});
            this.facturarToolStripMenuItem.Name = "facturarToolStripMenuItem";
            this.facturarToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.facturarToolStripMenuItem.Text = "Facturar";
            // 
            // realizarFacturacionToolStripMenuItem
            // 
            this.realizarFacturacionToolStripMenuItem.Name = "realizarFacturacionToolStripMenuItem";
            this.realizarFacturacionToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.realizarFacturacionToolStripMenuItem.Text = "Realizar Facturacion";
            this.realizarFacturacionToolStripMenuItem.Click += new System.EventHandler(this.realizarFacturacionToolStripMenuItem_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 430);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Principal";
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depositosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferenciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listadosEstadisticosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarjetasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBMHotelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBMUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBMClienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarUsuarioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aBMDepositoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarEstadiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarListadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarConsumibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBMRolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarRolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realizarFacturacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retiroToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cambiarPasstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarRolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarReservaToolStripMenuItem2;

    }
}

