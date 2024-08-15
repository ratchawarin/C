using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PROJECTING
{
    public partial class Form7 : Form
    {
        private string name;
        private string lastname;
        private string id;
        private string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
        // กำหนดตัวแปรเพื่อเก็บการเชื่อมต่อฐานข้อมูล
        private SqlConnection connection;

        public string Id
        {
            get { return id; }
        }
        // เพิ่มคอนสตรักเตอร์ที่รับพารามิเตอร์ชื่อและนามสกุล
        public Form7(string name, string lastname, string id)
        {



            InitializeComponent();
            connection = new SqlConnection("server=127.0.0.1;user=root;password=;database=stock;");


            this.name = name;
            this.lastname = lastname;
            this.id = id;
            // แสดงชื่อและนามสกุลบน Label
            label2.Text = "Name: " + name;
            label3.Text = "ID: " + id;
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        public static int pictureBoxIndex = 0; // ประกาศตัวแปรข้างนอกฟังก์ชัน

        private void Form7_Load(object sender, EventArgs e)
        {

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

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form2 form2 = new Form2();

            // ซ่อน 
            this.Hide();

            // แสดง
            form2.Show();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            // Hide Form7
            this.Hide();

            // Show Form12
            Form12 form12 = new Form12(this);
            if (form12.ShowDialog() == DialogResult.OK)
            {
                // Show Form7 again when Form12 closes
                this.Show();
            }
        } // ต้องเพิ่มวงเล็บปิดนี้

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            // Hide Form7
            this.Hide();

            // Show Form19
            Form19 form19 = new Form19(this);
            if (form19.ShowDialog() == DialogResult.OK)
            {
                // Show Form7 again when Form12 closes
                this.Show();
            }
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            // Hide Form7
            this.Hide();

            // Show Form19
            Form20 form20 = new Form20(this);
            if (form20.ShowDialog() == DialogResult.OK)
            {
                // Show Form7 again when Form12 closes
                this.Show();
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
