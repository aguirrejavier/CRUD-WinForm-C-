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
    public partial class InsertarPP : Form
    {
        int? id;
        public InsertarPP(int? id=null) // atributo opcional, si no se informa toma el valor null
        {
            InitializeComponent();
            this.id = id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.PersonaTableAdapter ta = new DataSet1TableAdapters.PersonaTableAdapter();

            if(this.id == null)
            {
                ta.InsertPersona(textBoxName.Text.Trim(), Convert.ToInt32(textBoxEdad.Text.Trim()), textBoxDescripcion.Text.Trim());
            }
            else
            {
                ta.EditarPersona(textBoxName.Text.Trim(), Convert.ToInt32(textBoxEdad.Text.Trim()), textBoxDescripcion.Text.Trim(), (int)id);
            }
            this.Dispose();
        }

        private void InsertarPP_Load(object sender, EventArgs e)
        {
            if ( id != null)
            {
                DataSet1TableAdapters.PersonaTableAdapter ta = new DataSet1TableAdapters.PersonaTableAdapter();

                DataSet1.PersonaDataTable dt = ta.GetDataByID((int)id);// devuelve una tabla
                DataSet1.PersonaRow row = (DataSet1.PersonaRow)dt.Rows[0];

                textBoxName.Text = row.nombre;
                textBoxEdad.Text = row.edad.ToString();
                textBoxDescripcion.Text = row.descripcion;
            }
        }
    }
}
