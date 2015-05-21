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
      
        public void cargarUsuario(string usuario, string hot, LogIn form)
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

            
            query = "SELECT  D.Nombre FROM LPP.FUNCIONALIDADXROL F JOIN LPP.ROLESXUSUARIO R ON R.username = '"+ user +"' JOIN LPP.ROLES O ON O.nombre = R.rol AND O.habilitado = 1 AND R.rol = F.Rol JOIN LPP.FUNCIONALIDAD D ON D.id_funcionalidad = F.funcionalidad ORDER BY D.id_funcionalidad";
            
           

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
                    if (lector1.GetString(0) == "Listado Estadistico")
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
                    if (lector1.GetString(0) == "ABM de Cliente")
                    {
                        entro = true;
                        clienteToolStripMenuItem.Visible = true;
                  
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM de Usuario")
                    {
                        entro = true;
                        usuarioToolStripMenuItem.Visible = true;
                      
                    }
                }
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM de Cuenta")
                    {
                        entro = true;
                        cuentaToolStripMenuItem.Visible = true;
                        
                    }
                }
                
                if (!entro)
                {
                    if (lector1.GetString(0) == "ABM de Rol")
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
            
            CambiarContraseña cambiarPass = new CambiarContraseña(user);
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
            BuscarCliente buscarCliente = new BuscarCliente();
            buscarCliente.Show();
            buscarCliente.padre_mp = this;
            
        }

      
        private void aBMRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMRol crearRol = new ABMRol("A");
            crearRol.Show();
            crearRol.mp = this;
           
        }

        private void buscarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarRol buscarRol = new BuscarRol("M");
            buscarRol.Show();
            buscarRol.mp = this;
           
        }

        private void eliminarRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
           BuscarRol eliminarRol = new BuscarRol("B");
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
            ABMUsuario abmUsuario = new ABMUsuario(user,"A");
            abmUsuario.mp = this;
            abmUsuario.Show();
           
        }

        private void buscarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarUsuario buscarUsuario = new BuscarUsuario(1);
            buscarUsuario.mp = this;
            buscarUsuario.Show();
           
        }

     
        private void buscarListadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Listados listados = new Listados();
            //listados.mp = this;
            //listados.Show();
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

        private void hotelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

        

        
    }
}
