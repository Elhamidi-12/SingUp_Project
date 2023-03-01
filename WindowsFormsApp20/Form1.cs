using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp20
{
    public partial class Form1 : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader drcharge;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strcn = "data source=NKI;initial catalog= profil;integrated security=true";
            cn = new SqlConnection(strcn); 
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Recherche()
        {
            try
            {
                cmd = new SqlCommand("select * from SingUp where Firs_Name= '"+ txtFName.Text+"'" ,cn);
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                drcharge = cmd.ExecuteReader();
                if (drcharge.HasRows)
                {
                    while (drcharge.Read())
                    {
                        txtFName.Text = drcharge[1].ToString();
                        txtSName.Text = drcharge[2].ToString();
                        txtUser.Text = drcharge[3].ToString();
                        txtEmail.Text = drcharge[4].ToString();
                        txtPassword.Text = drcharge[5].ToString();
                        txtCPassword.Text = drcharge[6].ToString();
                        DTime.Value = DateTime.Parse(drcharge[7].ToString()); ;
                    }
                }
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Vider()
        {
            txtFName.Text = "";
            txtSName.Text = "";
            txtUser.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtCPassword.Text = "";
            DTime.Value = DateTime.Now;
        }

        public void Ajouter()
        {
            cmd = new SqlCommand("insert into SingUp values('" + txtFName.Text + "','" + txtSName.Text + "','" + txtUser.Text + "','" + txtEmail.Text + "','"+txtPassword.Text+"','"+txtCPassword.Text+"','"+DTime.Value.ToString()+"')", cn);
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                MessageBox.Show("ajouter a effactues avec succesfull");
            }
            else MessageBox.Show("probleme d'insertion");

            cn.Close();
            cmd.Dispose();
        }
        public void Modifier()
        {
            cmd = new SqlCommand("update Etudiant set Num_Etu='" + txtFName.Text + "', '" + txtSName.Text + "', '" + txtUser.Text + "', '" + txtEmail.Text + "', '"+txtPassword.Text+"', '"+txtCPassword.Text+"', '"+DTime.Value.ToString()+"')", cn);
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                MessageBox.Show("Modifier a effactues avec succesfull");
            }
            else MessageBox.Show("probleme d'Modifier");

            cn.Close();
            cmd.Dispose();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Recherche();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Vider();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Ajouter();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Modifier();
        }

        private void DTime_onValueChanged(object sender, EventArgs e)
        {
            DTime.Format = DateTimePickerFormat.Custom;
            DTime.FormatCustom = "yyyy-MM-dd";
        }
    }
}
