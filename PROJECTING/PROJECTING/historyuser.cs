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
    public partial class Form19 : Form
    {
        private DataTable dataTable; // เพิ่มการประกาศตัวแปร dataTable ในระดับคลาส
        private Form7 form7;
        public Form19(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
            guna2DataGridView1.CellClick += guna2DataGridView1_CellContentClick;
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
                    guna2PictureBox1.Image = null; // Clear the picture box if there's no image
                }
            }
        }

        private void Form19_Load(object sender, EventArgs e)
        {

            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = "SELECT * FROM history1 WHERE iduser = @iduser";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@iduser", form7.Id);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                dataTable = new DataTable(); // สร้าง DataTable ใหม่


                adapter.Fill(dataTable);

                guna2DataGridView1.DataSource = dataTable;
                guna2DataGridView1.Columns["id"].Visible = false;
                guna2DataGridView1.Columns["image"].Visible = false; // Hide the image column in the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            double total = 0;

            if (dataTable != null) // ตรวจสอบว่า dataTable ไม่เป็น null ก่อนที่จะใช้งาน
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    total += Convert.ToDouble(row["total"]);
                }

                double vat = total * 0.07;

                label1.Text = "Total: " + total.ToString("N2") +" ฿ "   ;
                label2.Text = "VAT (7%): " + vat.ToString("N2")+" ฿ " ;

                double totalWithVAT = total + vat;
                label3.Text = "Total with VAT: " + totalWithVAT.ToString("N2")+ " ฿ " ;
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Close Form12 and send a DialogResult back to Form7
            this.DialogResult = DialogResult.OK;
            this.Close();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่าวันที่ที่ guna2DateTimePicker1 มากกว่าวันที่ที่ guna2DateTimePicker2
            if (guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                // แสดง MessageBox เพื่อแจ้งเตือน
                MessageBox.Show("Start date cannot be greater than end date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ถ้าเป็นเช่นนั้นให้กำหนดวันที่ของ guna2DateTimePicker2 เป็นวันเดียวกับ guna2DateTimePicker1
                guna2DateTimePicker2.Value = guna2DateTimePicker1.Value;
            }
        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่าวันที่ที่ guna2DateTimePicker2 น้อยกว่าวันที่ที่ guna2DateTimePicker1
            if (guna2DateTimePicker2.Value < guna2DateTimePicker1.Value)
            {
                // แสดง MessageBox เพื่อแจ้งเตือน
                MessageBox.Show("End date cannot be less than start date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ถ้าเป็นเช่นนั้นให้กำหนดวันที่ของ guna2DateTimePicker1 เป็นวันเดียวกับ guna2DateTimePicker2
                guna2DateTimePicker1.Value = guna2DateTimePicker2.Value;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";

            // เก็บค่าวันที่จาก DateTimePicker
            DateTime startDate = guna2DateTimePicker1.Value.Date;
            DateTime endDate = guna2DateTimePicker2.Value.Date;

            // สร้างคำสั่ง SQL SELECT โดยกรองด้วยเงื่อนไขของวันที่และ ID ของผู้ใช้
            string query = $"SELECT * FROM history1 WHERE iduser = @iduser AND date BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}'";

            // สร้างการเชื่อมต่อกับฐานข้อมูล
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // สร้าง Adapter และ DataSet
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // กำหนดค่าพารามิเตอร์ @iduser
                    adapter.SelectCommand.Parameters.AddWithValue("@iduser", form7.Id);

                    // นำข้อมูลจากฐานข้อมูลมาเติมใน DataSet
                    adapter.Fill(dataSet, "History");

                    // กำหนด DataSource ของ DataGridView
                    guna2DataGridView1.DataSource = dataSet.Tables["History"];

                    // คำนวณผลรวมของคอลัมน์ "total"
                    double total = 0;
                    foreach (DataRow row in dataSet.Tables["History"].Rows)
                    {
                        total += Convert.ToDouble(row["total"]);
                    }

                    // คำนวณ VAT (7%)
                    double vat = total * 0.07;

                    // แสดงผลลัพธ์ใน Label1 โดยใช้รูปแบบการแสดงที่มีการใส่ ,
                    label1.Text = $"Total: {total:#,0}  ฿";

                    // แสดงผลลัพธ์ใน Label2 โดยใช้รูปแบบการแสดงที่มีการใส่ ,
                    label2.Text = $"VAT (7%): {vat:#,0}  ฿";

                    // คำนวณและแสดงผลรวมของ Label1 และ Label2 ใน Label3
                    double totalWithVAT = total + vat;
                    label3.Text = $"Total with VAT: {totalWithVAT:#,0}  ฿";

                    // Clear the image in the PictureBox
                    guna2PictureBox1.Image = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
