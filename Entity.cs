using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud
{
    public partial class Entity : Form
    {
        db_filmEntities filmEntities = new db_filmEntities();
        int CurrentPosition = 0;
        public Entity()
        {
            InitializeComponent();
        }
        public void navigate(film f)
        {
            txtCode.Text = f.code_film.ToString();
            txtTitle.Text = f.titre;
            rtbDescription.Text = f.description_;
            txtLangue.Text = f.langue;
            dtpProduction.Value = DateTime.Parse(f.année_prod.ToString());
            cmbCategory.SelectedValue = f.code_cat;
        }
        public void fillDataGrid()
        {
            var films = filmEntities.films
                        .Select(f => 
                        new { 
                            id = f.code_film,
                            title = f.titre,
                            production = f.année_prod,
                            description = f.description_,
                            language = f.langue,
                            category = f.category.label});
            dgvFilms.DataSource = films.ToList();
        }

        public void fillComboBox()
        {
            var categories = from c in filmEntities.categories
                             select new { c.code_cat, c.label };
            cmbCategory.DataSource = categories.ToList();
            cmbCategory.ValueMember = "code_cat";
            cmbCategory.DisplayMember = "label";
        }
        
         private void Entity_Load(object sender, EventArgs e)
         {
            fillDataGrid();
            fillComboBox();
         }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int num = int.Parse(txtCode.Text);
            int films = filmEntities.films.Where(film => film.code_film == num).Count();
            if (films > 0)
            {
                 MessageBox.Show("Film already exist!!");
                return;
            }
            film f = new film();

            //get category
            int code = int.Parse(cmbCategory.SelectedValue.ToString());
            category c = filmEntities.categories.Find(code);
            f.category = c;

            f.code_film = int.Parse(txtCode.Text);
            f.titre = txtTitle.Text;
            f.description_ = rtbDescription.Text;
            f.langue = txtLangue.Text;
            f.année_prod = DateTime.Parse(dtpProduction.Value.ToString());

            try
            {
                filmEntities.films.Add(f);
                filmEntities.SaveChanges();
                fillDataGrid();
                MessageBox.Show("Film added succefully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int code = int.Parse(txtCode.Text);
            film f = filmEntities.films.FirstOrDefault(film => film.code_film == code);
            if(f is null)
            {
                MessageBox.Show("Film with this code doesn't exist !");
                return;
            }
            filmEntities.films.Remove(f);
            filmEntities.SaveChanges();
            fillDataGrid();
            MessageBox.Show("Film removed Succefully !");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int code = int.Parse(txtCode.Text);
            film f = filmEntities.films.FirstOrDefault(film => film.code_film == code);
            if (f is null)
            {
                MessageBox.Show("Film with this code doesn't exist !");
                return;
            }
            try
            {

                int codeCat = int.Parse(cmbCategory.SelectedValue.ToString());
                category c = filmEntities.categories.Find(codeCat);
                f.category = c;

                f.titre = txtTitle.Text;
                f.description_ = rtbDescription.Text;
                f.langue = txtLangue.Text;
                f.année_prod = DateTime.Parse(dtpProduction.Value.ToString());
                filmEntities.SaveChanges();
                fillDataGrid();
                MessageBox.Show("Film updated Succefully !");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int code = int.Parse(txtCode.Text);
            film f = filmEntities.films.FirstOrDefault(film => film.code_film == code);
            txtCode.Focus();
            if (f is null)
            {
                MessageBox.Show("Film with this code doesn't exist !");
                return;
            }

            txtTitle.Text = f.titre;
            rtbDescription.Text = f.description_;
            txtLangue.Text = f.langue;
            dtpProduction.Value= DateTime.Parse(f.année_prod.ToString());
            cmbCategory.SelectedValue = f.code_cat;
           
            
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            CurrentPosition = 0;
            film f = filmEntities.films.First();
            navigate(f);

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPosition > 0)
            {
                CurrentPosition--;
                film f = filmEntities.films.ToList()[CurrentPosition];
                navigate(f);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPosition < filmEntities.films.ToList().Count-1)
            {
                CurrentPosition++;
                film f = filmEntities.films.ToList()[CurrentPosition];
                navigate(f);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            CurrentPosition = filmEntities.films.ToList().Count-1;
            film f = filmEntities.films.ToList()[CurrentPosition];
            navigate(f);
            
        }
    }
}
