using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void Refrescar()
        {

            DataSet1TableAdapters.PersonaTableAdapter ta = new DataSet1TableAdapters.PersonaTableAdapter();

            DataSet1.PersonaDataTable dt = ta.GetPersonas(); // guarda los datos de la tabla en una variable dataTable

            dataGridView1.DataSource = dt;
            //dataGridView1.Columns[0].Visible = false; // ocultar columna en el datagrid
            dataGridView1.Columns["id"].Visible = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].HeaderText.ToUpper(); // modificar los titulos de la tabla
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ajusta el tamaño de la tabla de datos al datagrid
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertarPP frm = new InsertarPP();

            frm.ShowDialog(); // para visualizar el diagolo hijo
            frm.Dispose();
            Refrescar();

        }

        private int? GetID()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            int? id = (int)GetID();
            if (id != null)
            {
                InsertarPP modi = new InsertarPP(id);
                modi.ShowDialog();
                Refrescar();
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            int? id = (int)GetID();
            if (id != null)
            {
                DataSet1TableAdapters.PersonaTableAdapter ta = new DataSet1TableAdapters.PersonaTableAdapter();

                ta.EliminarPersona((int)id);
                Refrescar();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSet1.Persona' Puede moverla o quitarla según sea necesario.
            this.personaTableAdapter.Fill(this.dataSet1.Persona);
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Deshabilitar que se pueda cambiar de tamaño el form
            Refrescar();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el programa?",
               "Cerrar el programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string TipoBusqueda = comboBox1.Text.ToString();
            string busqueda = textBox1.Text.ToString().Trim();
            DataSet1TableAdapters.PersonaTableAdapter dp = new DataSet1TableAdapters.PersonaTableAdapter();
            if (!busqueda.Equals(""))
            {
                DataSet1.PersonaDataTable dt = dp.GetPersonas();
                EnumerableRowCollection<DataSet1.PersonaRow> bus = null;
                switch (TipoBusqueda)
                {
                    case "Elegir":
                         bus = dt.AsEnumerable().Where(x => x.nombre.ToUpper().Contains(busqueda.ToUpper())
                                    || x.descripcion.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                    case "Nombre":
                         bus = dt.Where(x => x.nombre.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                    case "Edad":
                        bus = dt.Where(x => x.edad == Convert.ToInt32(busqueda));
                        break;
                    case "Descripcion":
                        bus = dt.Where(x => x.descripcion.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                }

                dataGridView1.DataSource = bus.AsDataView();
                //var lista = from myRow in dt.AsEnumerable()
                //            where myRow.Field<string>("nombre").ToUpper().Contains(busqueda.ToUpper())
                //            select myRow;

            }
               
        }
    }
}
