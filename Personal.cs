using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegocios;
namespace Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }
        int idCargo;
        int desde = 1;
        int hasta = 10;
        int contador;
        int idPersonal=0;
        private int items=10;
        string estado;
        int totalPaginas;
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            localizarDtw();
            panelCargo.Visible = false;
            panelPaginador.Visible = false;
            panelRegistro.Visible = true;
            panelRegistro.Dock = DockStyle.Fill;
            btnAgregar.Visible = true;
            btnCancelar.Visible = true;
            limpiarCampos();
        }
        private void localizarDtw()
        {
            dtgCargo.Location = new Point(panel14.Location.X, panel14.Location.Y);
            dtgCargo.Size = new Size(240, 112);
            dtgCargo.Visible = true;
            lblsueldo.Visible = false;
            panelop.Visible = false;
        }
        public void limpiarCampos(){
            txtNombre.Clear();
            txtCedula.Clear();
            txtCargo.Clear();
            txtSueldo.Clear();
            buscarCargo();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // string nombre = txtNombre.Text, cedula = "", pais = "", cargo = "", sueldo = "";
            if (!String.IsNullOrEmpty(txtNombre.Text) && !String.IsNullOrEmpty(txtCedula.Text) && !String.IsNullOrEmpty(cbPais.Text) && !String.IsNullOrEmpty(txtCargo.Text) && !String.IsNullOrEmpty(txtSueldo.Text))
            {
                insertarPersonal();
            }
            
        }
        private void mostrarPersonal()
        {
            DataTable dt = new DataTable();
            lPersonal person = new lPersonal();
            person.mostrarPersonal(ref dt, desde, hasta);
            dtgPersonal.DataSource = dt;
            dtgPersonal.Columns[2].Visible = false;
            diseñoDataGrid();
        }
        private void diseñoDataGrid()
        {
            bases.diseñoDtg(ref dtgPersonal);
            bases.DiseñoDTGElimina(ref dtgPersonal);
            panelPaginador.Visible = true;
        }

        //public bool validacionCampos()
        //{
        //    string nombre = "", cedula = "", pais = "", cargo = "", sueldo = "";
        //    if (!String.IsNullOrEmpty() && !String.IsNullOrEmpty(cedula) && !String.IsNullOrEmpty(pais) && !String.IsNullOrEmpty(cargo) && !String.IsNullOrEmpty(sueldo))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        private void insertarPersonal()
        {
            lPersonal person = new lPersonal();
            person.Nombre = txtNombre.Text;
            person.Cedula = txtCedula.Text;
            person.Pais = cbPais.Text;
            person.idCargo = idCargo;
            person.Sueldo = Convert.ToDouble(txtSueldo.Text);
            if (person.insertarPersonal(person) == true)
            {
                mostrarPersonal();
                panelRegistro.Visible = false;
                btnMPersonal.Visible = false;
                btnAgregar.Visible = true;
            }
        }
        private void insertarCargo()
        {
            if (!string.IsNullOrEmpty(txtcarg.Text))
            {
                if (!string.IsNullOrEmpty(txtSueld.Text))
                {
                    Cargo cargo = new Cargo();
                    cargo.cargo = txtcarg.Text;
                    cargo.Sueldo = Convert.ToDouble(txtSueld.Text);
                    if (cargo.insertarCargo(cargo) == true)
                    {
                        txtCargo.Clear();
                        buscarCargo();
                        panelCargo.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Complete", "el campo sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Complete", "el campo cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void buscarCargo()
        {
            DataTable dt = new DataTable();
            Cargo cargo = new Cargo();
            cargo.buscarCargo(ref dt, txtCargo.Text);
            dtgCargo.DataSource = dt;
            dtgCargo.Columns[1].Visible = false;
            dtgCargo.Columns[3].Visible = false;
            dtgCargo.Visible = true;
            btnMPersonal.Visible = false;
            // bases.diseñoDtg(ref  dtgCargo);
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            buscarCargo();
        }

        private void btnAgregarCar_Click(object sender, EventArgs e)
        {
            panelCargo.Visible = true;
            panelCargo.Dock =DockStyle.Fill;
            panelCargo.BringToFront();
            btnAgregarC.Visible = true;
            btnVolver.Visible = true;
            txtcarg.Clear();
            txtSueld.Clear();
        }

        private void btnAgregarC_Click(object sender, EventArgs e)
        {
            insertarCargo();
        }

        private void txtSueld_KeyPress(object sender, KeyPressEventArgs e)
        {
            bases.decimales(txtSueld, e);
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            bases.decimales(txtSueldo, e);
        }

        private void dtgCargo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          if(e.ColumnIndex == dtgCargo.Columns["EditarC"].Index)
            {
                EditarCargo();
            }
            if (e.ColumnIndex == dtgCargo.Columns["cargo"].Index)
            {
                obtenerCargo();
            }
        }
        private void obtenerCargo()
        {
            idCargo = Convert.ToInt32(dtgCargo.SelectedCells[1].Value);
            txtCargo.Text = dtgCargo.SelectedCells[2].Value.ToString();
            txtSueldo.Text=dtgCargo.SelectedCells[3].Value.ToString();
            dtgCargo.Visible=false;
            panelop.Visible=true;
            lblsueldo.Visible=true;

        }

        private void EditarCargo()
        {
            idCargo = Convert.ToInt32(dtgCargo.SelectedCells[1].Value);
            txtcarg.Text = dtgCargo.SelectedCells[2].Value.ToString();
            txtSueld.Text = dtgCargo.SelectedCells[3].Value.ToString();
            btnAgregarC.Visible = false;
            btnModificar.Visible = true;
            btnVolver.Visible = true;
            txtcarg.Focus();
            txtcarg.SelectAll();
            panelCargo.Visible = true; 
            panelCargo.Dock=DockStyle.Fill;
            panelCargo.BringToFront();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelCargo.Visible = false;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            panelRegistro.Visible = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            editarCargo();
        }
        private void editarCargo()
        {
            Cargo cargo = new Cargo();
            cargo.IdCargo = idCargo;
            cargo.cargo = txtcarg.Text;
            cargo.Sueldo = Convert.ToDouble(txtSueld.Text);
            if (cargo.editarCargo(cargo) == true)
            {
                txtCargo.Clear();
                buscarCargo();
                panelCargo.Visible = false;
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            mostrarPersonal();
        }

        private void dtgPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dtgPersonal.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("Solo se cambiara el estado para no poder acceder ¿Desea continuar?", "Eliminar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result == DialogResult.OK)
                {
                    eliminarPersonal();
                }
               
            }
            if(e.ColumnIndex == dtgPersonal.Columns["Editar"].Index)
            {
                
                 obtenerDatos();
            }
        }
        private void obtenerDatos()
        {
            idPersonal = Convert.ToInt32(dtgPersonal.SelectedCells[2].Value);
            estado = dtgPersonal.SelectedCells[8].Value.ToString();
            if(estado == "I")
            {
                 restaurarPersonal();
            }
            else
            {
                txtNombre.Text = dtgPersonal.SelectedCells[3].Value.ToString();
                txtCedula.Text = dtgPersonal.SelectedCells[4].Value.ToString();
                cbPais.Text = dtgPersonal.SelectedCells[5].Value.ToString();    
                txtCargo.Text = dtgPersonal.SelectedCells[6].Value.ToString();
                txtSueldo.Text = dtgPersonal.SelectedCells[7].Value.ToString();
                panelPaginador.Visible = false;
                panelRegistro.Visible = true;
                panelRegistro.Dock = DockStyle.Fill;
                panelCargo.Visible = false;
                lblsueldo.Visible = true;
                panelop.Visible = true;
                btnAgregar.Visible = false;
                btnMPersonal.Visible = true;
            }
        }
        private void restaurarPersonal()
        {
            DialogResult result = MessageBox.Show("Se habilitara el registro ¿Desea continuar?", "Habilitar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                habilitarPersonal();
            }
            //DialogResult result = MessageBox.Show("¿Se habilitara el registro","Desea continuar? Habilitar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if(result == DialogResult.OK)
            //{
            //    habilitarPersonal();
            //}
        }
        private void habilitarPersonal()
        {
            lPersonal person = new lPersonal();
            person.IdPersonal = idPersonal;
            if (person.restaurarPersonal(person)==true)
            {
                mostrarPersonal();
            }
        }
        private void eliminarPersonal()
        {
            idPersonal = Convert.ToInt32(dtgPersonal.SelectedCells[2].Value);
            lPersonal person = new lPersonal();
            person.IdPersonal = idPersonal;
            if(person.eliminarPersonal(person) == true)
            {
                mostrarPersonal();
            }
        }

        private void btnMPersonal_Click(object sender, EventArgs e)
        {
            editarPersonal();
        }
        private void editarPersonal()
        {
            lPersonal person = new lPersonal();
           // Cargo carg = new Cargo();
            person.IdPersonal = idPersonal;
            person.Nombre = txtNombre.Text;
            person.Cedula = txtCedula.Text;
            person.Pais = cbPais.Text;
            person.idCargo = idCargo;
            person.Sueldo = Convert.ToDouble(txtSueldo.Text);
            if(person.editarPersonal(person) == true)
            {
                mostrarPersonal();
                panelRegistro.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            diseñoDataGrid();
            timer1.Stop();
        }
    }
}
