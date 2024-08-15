using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECTING
{
    public partial class Form17 : Form
    {
        private string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";

        public Form17()
        {
            InitializeComponent();
            guna2DataGridView1.CellClick += guna2DataGridView1_CellContentClick;
        }

        private void Form17_Load(object sender, EventArgs e)
        {
            DisplayData();
            DisplayDataAndCalculateTotal();
            guna2DataGridView1.Columns["image"].Visible = false; // Hide the image column in the DataGridView
            UpdateLabel2WithTotalRowCount();
        }

        private void UpdateLabel2WithTotalRowCount()
        {
            string query = "SELECT SUM(count) FROM history1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    object result = command.ExecuteScalar();
                    int totalCount = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    label2.Text = $"จำนวนสินที่ขายได้: {totalCount} ชิ้น";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisplayDataAndCalculateTotal()
        {
            string query = "SELECT * FROM history1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "History");

                    guna2DataGridView1.DataSource = dataSet.Tables["History"];

                    double total = 0;
                    foreach (DataRow row in dataSet.Tables["History"].Rows)
                    {
                        total += Convert.ToDouble(row["total"]);
                    }

                    label1.Text = $"Total: {total:#,0} บาท ";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisplayData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM history1";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "History");

                    guna2DataGridView1.DataSource = dataSet.Tables["History"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //ปุ่มย้นอกลับไปหน้า system admin
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            this.Hide();
            form8.Show();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                if (row.Cells["image"].Value != DBNull.Value)
                {
                    byte[] imageData = (byte[])row.Cells["image"].Value;

                    using (var ms = new System.IO.MemoryStream(imageData))
                    {
                        guna2PictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    guna2PictureBox1.Image = null;
                }
            }
        }

        //วันที่

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                MessageBox.Show("Start date cannot be greater than end date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2DateTimePicker2.Value = guna2DateTimePicker1.Value;
            }
        }

        //วันที่
        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (guna2DateTimePicker2.Value < guna2DateTimePicker1.Value)
            {
                MessageBox.Show("End date cannot be less than start date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2DateTimePicker1.Value = guna2DateTimePicker2.Value;
            }
        }

        //ปุ่มค้นหา
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = guna2DateTimePicker1.Value.Date;
            DateTime endDate = guna2DateTimePicker2.Value.Date;

            string query = $"SELECT * FROM history1 WHERE date BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "History");
                    guna2DataGridView1.DataSource = dataSet.Tables["History"];

                    double total = 0;
                    foreach (DataRow row in dataSet.Tables["History"].Rows)
                    {
                        total += Convert.ToDouble(row["total"]);
                    }

                    label1.Text = $"Total: {total:#,0} บาท";

                    // Calculate the sum of the "count" column
                    int totalCount = 0;
                    foreach (DataRow row in dataSet.Tables["History"].Rows)
                    {
                        totalCount += Convert.ToInt32(row["count"]);
                    }
                    label2.Text = $"จำนวนสินที่ขายได้: {totalCount} ชิ้น";

                    guna2PictureBox1.Image = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        //ค้นหาและอ้างอิงราคาและจำนวนชิ้นในวันที่เลือก
        private void SearchData(string searchText)
        {
            DateTime defaultDate1 = guna2DateTimePicker1.MinDate;
            DateTime defaultDate2 = guna2DateTimePicker2.MinDate;

            bool isDateRangeSelected = guna2DateTimePicker1.Value != defaultDate1 || guna2DateTimePicker2.Value != defaultDate2;

            string query;
            if (isDateRangeSelected)
            {
                DateTime startDate = guna2DateTimePicker1.Value.Date;
                DateTime endDate = guna2DateTimePicker2.Value.Date;
                query = $"SELECT * FROM history1 WHERE (idname LIKE '%{searchText}%' OR name LIKE '%{searchText}%' OR nameuser LIKE '%{searchText}%') AND date BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'";
            }
            else
            {
                query = $"SELECT * FROM history1 WHERE idname LIKE '%{searchText}%' OR name LIKE '%{searchText}%' OR nameuser LIKE '%{searchText}%'";
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "History");
                    guna2DataGridView1.DataSource = dataSet.Tables["History"];

                    double total = 0;
                    foreach (DataRow row in dataSet.Tables["History"].Rows)
                    {
                        total += Convert.ToDouble(row["total"]);
                    }

                    label1.Text = $"Total: {total:#,0} บาท";

                    // Calculate the sum of the "count" column
                    int totalCount = 0;
                    foreach (DataRow row in dataSet.Tables["History"].Rows)
                    {
                        totalCount += Convert.ToInt32(row["count"]);
                    }
                    label2.Text = $"จำนวนสินที่ขายได้: {totalCount} ชิ้น";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchData(guna2TextBox1.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
