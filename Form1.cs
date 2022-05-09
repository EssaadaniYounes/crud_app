using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Crud
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(@"server=.\YOUNES;database=db_film;integrated security=true");
        SqlDataAdapter filmsAdapter;
        SqlDataAdapter categoriesAdapter;
        DataSet Ds= new DataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void fillDataGrid()
        {
            filmsAdapter = new SqlDataAdapter("select * from film", connection);
            filmsAdapter.Fill(Ds, "films");
            dgvFilms.DataSource = Ds.Tables["films"];
        }
        public void fillComboBox()
        {
            categoriesAdapter = new SqlDataAdapter("select code_cat, label from category", connection);
            categoriesAdapter.Fill(Ds, "categories");

            cmbCategory.DataSource = Ds.Tables["categories"];
            cmbCategory.ValueMember = "code_cat";
            cmbCategory.DisplayMember = "label";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if(connection.State != ConnectionState.Open) connection.Open();
            fillDataGrid();
            fillComboBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow dr = Ds.Tables["films"].NewRow();
            dr["code_film"] = txtCode.Text;
            dr["titre"] = txtTitle.Text;
            dr["description_"] = rtbDescription.Text;
            dr["langue"] = txtLangue.Text;
            dr["année_prod"] = DateTime.Parse(dtpProduction.Value.ToString());
            dr["code_cat"] = cmbCategory.SelectedValue;

            Ds.Tables["films"].Rows.Add(dr);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(filmsAdapter);
            filmsAdapter.Update(Ds.Tables["films"]);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Ds.Tables["films"].Rows.Count; i++)
            {
                DataRow dr = Ds.Tables["films"].Rows[i];
                if (dr["code_film"].ToString() == txtCode.Text)
                {
                    dr.Delete();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Ds.Tables["films"].Rows.Count; i++)
            {
                DataRow dr = Ds.Tables["films"].Rows[i];
                if (dr["code_film"].ToString() == txtCode.Text)
                {
                    dr["titre"] = txtTitle.Text;
                    dr["description_"] = rtbDescription.Text;
                    dr["langue"] = txtLangue.Text;
                    dr["année_prod"] = DateTime.Parse(dtpProduction.Value.ToString());
                    dr["code_cat"] = cmbCategory.SelectedValue;
                    return;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Ds.Tables["films"].Rows.Count; i++)
            {
                DataRow dr = Ds.Tables["films"].Rows[i];
                if (dr["code_film"].ToString() == txtCode.Text)
                {
                    txtTitle.Text = dr["titre"].ToString() ;
                    rtbDescription.Text = dr["description_"].ToString();
                    txtLangue.Text = dr["langue"].ToString();
                    dtpProduction.Value = DateTime.Parse(dr["année_prod"].ToString());
                    cmbCategory.SelectedValue = dr["code_cat"].ToString();
                    return;
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }
    }
}
