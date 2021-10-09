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
            Refrescar();
        }
    }
}
