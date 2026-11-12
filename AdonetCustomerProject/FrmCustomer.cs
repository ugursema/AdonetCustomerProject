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

namespace AdonetCustomerProject
{
    public partial class FrmCustomer : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Server=localhost;Initial Catalog=DBbCustomer;Integrated Security=True;TrustServerCertificate=True");
        public FrmCustomer()
        {
            InitializeComponent();
        }
        private void List()
        {
            Listele();
        }
        private void Listele()
        {
            sqlConnection.Open();  //bağnaltıyı aç
            SqlCommand command = new SqlCommand("select * from Tbl_Customer", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT CustomerId, CustomerName, CustomerSurname,CustomerBalance, CustomerStatus, CityName FROM Tbl_CustomeINNER JOIN Tbl_City ON Tbl_City.CityId = Tbl_Customer.CustomerCity WHERE CustomerName = @CustomerName", sqlConnection);
            command.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List();
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Add()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert Into Tbl_Customer (CustomerName, CustomerSurname, CustomerCity,CustomerBalance, CustomerStatus) values(@CustomerName,@CustomerSurname, @CustomerCity,@CustomerBalance,@CustomerStatus)", sqlConnection);
            command.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@CustomerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@CustomerCity", cmbCustomerCity.SelectedValue);
            command.Parameters.AddWithValue("@customerBalance", decimal.Parse(txtCustomerBalance.Text));
            if (rdbAktif.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", true);
            }
            else if (rdbPasif.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", false);
            }
            else
            {
                MessageBox.Show("Lütfen müşteri durumunu seçiniz.");
                return;
            }
            command.ExecuteNonQuery();//islemleri vt kaydediyor(kayıt sayısı döner)
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }



        private void Delete()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From Tbl_Customer Where CustomerId=@CustomerId", sqlConnection);
            command.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
            command.ExecuteNonQuery();//islemleri vt kaydediyor(kayıt sayısı döner)
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }


        private void Update()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update Tbl_Customer SET CustomerName=@customerName ,CustomerSurname=@customerSurname ,CustomerCity=@customerCity ,CustomerBalance=@customerBalance ,CustomerStatus=@customerStatus  Where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", Convert.ToInt32(cmbCustomerCity.SelectedValue));
            command.Parameters.AddWithValue("@customerBalance", decimal.Parse(txtMusteriBalance.Text));
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            if (rdbAktif.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", true);
            }
            else if (rdbPasif.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", false);
            }
            else
            {
                MessageBox.Show("Lütfen müşteri durumunu seçiniz.");
                return;
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();

        }


        private void btnProce_Click(object sender, EventArgs e)
        {

        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand("select * from Tbl_City", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbCustomerCity.ValueMember = "CityId";
            cmbCustomerCity.DisplayMember = "CityName";
            cmbCustomerCity.DataSource = dataTable;



        }
    }
}
