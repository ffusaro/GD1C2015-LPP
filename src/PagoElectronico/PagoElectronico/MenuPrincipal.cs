using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace PagoElectronico
{
    public partial class MenuPrincipal : Form
    {
        public string user;
        DataGridView dgvejemplo = new DataGridView();
        public Login.LogIn log;

        public MenuPrincipal()
        {
            InitializeComponent();
        }
      
        public void cargarUsuario(string usuario, string hot, Login.LogIn form)
        {
            user = usuario;
            log = form;

           /* if (user != "")
            {
                //MenuPrincipal.ActiveForm.Text = hot + "     ";
                sesionToolStripMenuItem.Visible = true;
                
            }
            else
            {
                //MenuPrincipal.ActiveForm.Text =" Bienvenido    ";
                sesionToolStripMenuItem.Visible = true;
                
            }*/

            /*PREPARAR BOTONERA*/

            depositosToolStripMenuItem.Visible = false;
            tarjetasToolStripMenuItem.Visible = false;
            transferenciaToolStripMenuItem.Visible = false;
            listadosEstadisticosToolStripMenuItem.Visible = false;
            clienteToolStripMenuItem.Visible = false;
            cuentaToolStripMenuItem.Visible = false;
            rolToolStripMenuItem.Visible = false;
            usuarioToolStripMenuItem.Visible = false;
            facturarToolStripMenuItem.Visible = false;


            Conexion con = new Conexion();
            string query;


            query = "SELECT  F.descripcion FROM LPP.FUNCIONALIDADXROL FR" 
                    +" JOIN LPP.FUNCIONALIDAD F ON F.id_funcionalidad = FR.funcionalidad"
                    +" JOIN LPP.ROLES R ON R.id_rol = FR.rol"  
                    +" JOIN LPP.ROLESXUSUARIO RU ON RU.rol = R.id_rol"
                    +" WHERE R.habilitado = 1 and RU.username = '"+ user +"'" +"ORDER BY F.id_funcionalidad";

            con.cnn.Open();
            SqlCommand command = new SqlCommand(query, con.cnn);
            SqlDataReader lector1 = command.ExecuteReader();
            bool entro = false;
         
            while (lector1.Read())
            {
                
                entro = false;
                

                if (!entro)
                {
                   
                    if (lector1.GetString(0) == "Depositos")
                    {
                        entro = true;
                        depositosToolStripMenuItem.Visible = true;
                    
                    }
                }
                if (!entro)
                {

                    if (lector1.GetString(0) == "Consulta Saldos")
                    {
                        entro = true;
                        consultarSaldoToolStripMenuItem.Visible = true;

                    }
                }
              
                if (!entro)
                {
                    if (lector1.GetString(0) == "Listados")
                    {
                        entro = true;
                        listadosEstadisticosToolStripMenuItem.Visible = true;
                     
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "Asociar/Desasociar Tarjetas")
                    {
                        entro = true;
                        tarjetasToolStripMenuItem.Visible = true;
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "Transferencias")
                    {
                        entro = true;
                        transferenciaToolStripMenuItem.Visible = true;
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM Cliente")
                    {
                        entro = true;
                        clienteToolStripMenuItem.Visible = true;
                  
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM Usuarios")
                    {
                        entro = true;
                        usuarioToolStripMenuItem.Visible = true;
                      
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM Cuenta")
                    {
                        entro = true;
                        cuentaToolStripMenuItem.Visible = true;
                        
                    }
                }
                
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM Rol")
                    {
                        entro = true;
                        rolToolStripMenuItem.Visible = true;
               
                    }
                }
                
                if (!entro)
                {
                    if (lector1.GetString(0) == "Facturar")
                    {
                        
                        entro = true;
                        facturarToolStripMenuItem.Visible = true;
                     
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "Retiros")
                    {

                        entro = true;
                        retiroToolStripMenuItem1.Visible = true;

                    }
                }
                
               
            }
            con.cnn.Close();
            
           
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Close();
            this.Close();
        }
        
        private void cambiarPasstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Login.CambiarContraseña cambiarPass = new Login.CambiarContraseña(user);
            cambiarPass.padre_PostL = this;
            cambiarPass.Show();
            
        }

        private void aBMClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMCliente abmCliente = new ABMCliente("A","U");
            abmCliente.Show();
            abmCliente.padre_mp = this;
            
        }

        private void buscarUsuarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ABM_Cliente.BuscarCliente buscarCliente = new ABM_Cliente.BuscarCliente();
            buscarCliente.Show();
            buscarCliente.padre_mp = this;
            
        }

      
        private void aBMRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABM_Rol.ABMRol crearRol = new ABM_Rol.ABMRol("A");
            crearRol.Show();
            crearRol.mp = this;
           
        }

        private void buscarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABM_Rol.BuscarRol buscarRol = new ABM_Rol.BuscarRol("M");
            buscarRol.Show();
            buscarRol.mp = this;
           
        }

        private void eliminarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABM_Rol.BuscarRol eliminarRol = new ABM_Rol.BuscarRol("B");
            eliminarRol.Show();
            eliminarRol.mp = this;
        }

       
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Show();
            this.Close();
           
        }

        private void aBMUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.ABMUsuario abmUsuario = new ABM_de_Usuario.ABMUsuario(user, "A");
            abmUsuario.mp = this;
            abmUsuario.Show();
           
        }

        private void buscarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.BuscarUsuario buscarUsuario = new ABM_de_Usuario.BuscarUsuario(1);
            buscarUsuario.mp = this;
            buscarUsuario.Show();
           
        }

     
        private void buscarListadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Listados_Estadisticos.Listados lis = new Listados_Estadisticos.Listados();
            lis.Show();

        }

        private void realizarFacturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Facturar facturar = new Facturar(user);
            //facturar.mp = this;
            //facturar.Show();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }


        private void aBMDepositoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Depositos.Depositos dep = new Depositos.Depositos(user);
            dep.Show();
        }

  
        private void aBMRolToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            ABM_Rol.ABMRol rol = new ABM_Rol.ABMRol(user);
            rol.Show();
        }

      

        

        

        
    }
}
