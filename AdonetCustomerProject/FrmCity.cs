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
    public partial class FrmCity : Form
    {
        public FrmCity()
        {
            InitializeComponent();
        }
        //her yerden erişim (sayfada)
        SqlConnection sqlConnection = new SqlConnection("Server=localhost;Initial Catalog=DBbCustomer;Integrated Security=True;TrustServerCertificate=True");

        private void btnList_Click(object sender, EventArgs e)
        {
            Listele();

        }
        private void Listele()
        {
            sqlConnection.Open();  //bağnaltıyı aç
            SqlCommand command = new SqlCommand("Select * From Tbl_City", sqlConnection);//sorguyu oluşturduk
            SqlDataAdapter adapter = new SqlDataAdapter(command);//commandaki sorguyu adapter gönderdik hafoızaya alıp datatable gönderiyor (köprü görevinde) datatable sanal bi tablo adapter dan gelen verileri geçici olarak tutup datasoruce gösdtertiyo 
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert Into Tbl_City (CityName, CityCountry) values(@cityName, @cityCountry)", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.ExecuteNonQuery();//islemleri vt kaydediyor(kayıt sayısı döner)
            sqlConnection.Close();
            MessageBox.Show("Şehir Başarılı Bir Şekilde Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From Tbl_City Where CityId=@cityId", sqlConnection);
            command.Parameters.AddWithValue("@cityId", txtCityId.Text);
            command.ExecuteNonQuery();//islemleri vt kaydediyor(kayıt sayısı döner)
            sqlConnection.Close();
            MessageBox.Show("Şehir Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void Update()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update Tbl_City SET CityName=@cityName ,CityCountry=@cityCountry Where CityId=@cityId", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.Parameters.AddWithValue("@cityId", txtCityId.Text);
            command.ExecuteNonQuery();//islemleri vt kaydediyor(kayıt sayısı döner)
            sqlConnection.Close();
            MessageBox.Show("Şehir Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From Tbl_City Where CityName=@cityName", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text); 
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void FrmCity_Load(object sender, EventArgs e)
        {

        }
    }
}
