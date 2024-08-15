using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROJECTING
{
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form1 form1 = new Form1();

            // ซ่อน 
            this.Hide();

            // แสดง
            form1.Show();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form5 form5 = new Form5();

            // ซ่อน 
            this.Hide();

            // แสดง
            form5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form6 form6 = new Form6();

            // ซ่อน 
            this.Hide();

            // แสดง
            form6.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string userid = textBox1.Text;
            string password = textBox2.Text;

            // สร้าง connection string
            string connectionString = "server=127.0.0.1;user=root;password=;database=information";

            // คำสั่ง SQL สำหรับตรวจสอบ user id และ password
            string query = "SELECT COUNT(*) FROM register WHERE userid = @userid AND password = @password";

            // เชื่อมต่อกับฐานข้อมูลและทำการตรวจสอบ userid และ password
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("เข้าสู่ระบบสำเร็จ");
                        // ดึงชื่อและนามสกุลจากฐานข้อมูล
                        string name = "";
                        string lastname = "";
                        string id = "";
                        string queryUserInfo = "SELECT name, lastname,userid FROM register WHERE userid = @userid";
                        using (MySqlCommand commandUserInfo = new MySqlCommand(queryUserInfo, connection))
                        {
                            commandUserInfo.Parameters.AddWithValue("@userid", userid);
                            using (MySqlDataReader reader = commandUserInfo.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    name = reader.GetString("name");
                                    lastname = reader.GetString("lastname");
                                    id = reader.GetString("userid");
                                }
                            }
                        }
                       
                        // เปิด Form7 และส่งชื่อและนามสกุลไปยังคอนสตรักเตอร์
                        Form7 form7 = new Form7(name, lastname,id);
                        form7.Show();
                        this.Hide();

                    }
                   
                    else
                    {
                        MessageBox.Show("User ID หรือ Password ไม่ถูกต้อง");
                    }
                }
            }



        }

        


        private string userid;
        private string password;
        private bool showPassword;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
            if (!showPassword)
            {
                textBox2.PasswordChar = '*'; // ซ่อนรหัสผ่าน
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            showPassword = guna2CheckBox1.Checked;
            if (showPassword)
            {
                textBox2.PasswordChar = '\0'; // แสดงรหัสผ่าน
            }
            else
            {
                textBox2.PasswordChar = '*'; // ซ่อนรหัสผ่าน
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form3 form3 = new Form3();

            // ซ่อน 
            this.Hide();

            // แสดง
            form3.Show();
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label4.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("HH:mm:ss");
            timer1.Start();
        }
    }
}
