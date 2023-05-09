using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;



namespace ADO_NETProject01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        
        }

        ListaFornecedores lista = new ListaFornecedores();

        private void Form1_Load(object sender, EventArgs e)
        {
            fillDgv();
        }

        private void fillDgv()
        {
            var list = lista.getAllFornecedores();
            dgv.DataSource = list;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" || txtCnpj.Text != "")
            {
                string connectionString = ConfigurationManager.
   ConnectionStrings["CS_ADO_NET"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into FORNECEDORES(nome,cnpj) values(@nome, @cnpj)";
                command.Parameters.AddWithValue("@nome", txtNome.Text);
                command.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Fornecedor registrado com sucesso");
                fillDgv();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.
ConnectionStrings["CS_ADO_NET"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "update FORNECEDORES set nome=@nome, cnpj=@cnpj WHERE Id=@Id";
            command.Parameters.AddWithValue("@nome", txtNome.Text);
            command.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
            command.Parameters.AddWithValue("@Id", txtID.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Fornecedor Alterado com sucesso");
            fillDgv();
        
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtCnpj.Text = "";
            txtID.Text = "";
            MessageBox.Show("Insira os dados para inclusao na DATABASE.");

        }
        public class ListaFornecedores
        {
            public List<string> allFornecedores = new List<string>();

            public DataTable getAllFornecedores()
            {
                string connectionString = ConfigurationManager.
                    ConnectionStrings["CS_ADO_NET"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from FORNECEDORES";
                var adapter = new SqlDataAdapter(command);
                var table = new DataTable();
                adapter.Fill(table);
                return table;

                
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
            {
                txtID.Text = dgv.CurrentRow.Cells[0].Value.ToString();
                txtCnpj.Text = dgv.CurrentRow.Cells[1].Value.ToString();
                txtNome.Text = dgv.CurrentRow.Cells[2].Value.ToString();

            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.
ConnectionStrings["CS_ADO_NET"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "delete from FORNECEDORES WHERE Id=@Id";
            command.Parameters.AddWithValue("@Id", txtID.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Fornecedor Removido com sucesso");
            fillDgv();
        }
    }
}
