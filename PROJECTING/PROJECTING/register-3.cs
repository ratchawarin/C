using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace PROJECTING
{
    public partial class Form3 : Form
    {

        // ประกาศตัวแปรที่จำเป็นสำหรับเก็บข้อมูล
        private string name;
        private string lastname;
        private string userid;
        private string password;
        private int idcard;
        private string telephone;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        // ปุ่ม สมัคร
        private void button1_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลจาก TextBoxes
            string name = textBox1.Text;
            string lastname = textBox2.Text;
            string userid = textBox3.Text;
            string password = textBox4.Text;
            string idcard = textBox5.Text;
            string telephone = textBox6.Text;
            string numberhome = textBox7.Text;
            string dis1 = textBox8.Text;
            string dis2 = textBox9.Text;
            string pro = textBox10.Text;
            string pri = textBox11.Text;

            // ตรวจสอบว่าข้อมูลทุกช่องถูกกรอกหรือไม่
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname) ||
                string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(telephone) || string.IsNullOrWhiteSpace(numberhome) ||
                string.IsNullOrWhiteSpace(dis1) || string.IsNullOrWhiteSpace(dis2) ||
                string.IsNullOrWhiteSpace(pro) || string.IsNullOrWhiteSpace(pri)
                )
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ถูกทุกช่อง ");
                return;
            }
            // ตรวจสอบว่า pri เป็นเลขเท่านั้นและมีความยาวเท่ากับ 5 ตัวเท่านั้น
            if (!int.TryParse(pri, out int priValue) || pri.Length != 5)
            {
                MessageBox.Show("กรุณากรอกข้อมูล pri เป็นเลขเท่านั้นและความยาวต้องเท่ากับ 5 ตัวเท่านั้น");
                return;
            }
            // ตรวจสอบว่า pri เป็นเลขเท่านั้นและมีความยาวเท่ากับ 5 ตัวเท่านั้น
            if (!int.TryParse(pri, out int idcardValue) || idcard.Length != 13)
            {
                MessageBox.Show("กรุณากรอกข้อมูล เลขบัตรประชาชน เป็นเลขเท่านั้นและความยาวต้องเท่ากับ 13 ตัวเท่านั้น");
                return;
            }
            // ตรวจสอบว่า pri เป็นเลขเท่านั้นและมีความยาวเท่ากับ 5 ตัวเท่านั้น
            if (!int.TryParse(pri, out int telephoneValue) || telephone.Length != 10)
            {
                MessageBox.Show("กรุณากรอกข้อมูล เบอร์โทรศัพท์ เป็นเลขเท่านั้นและความยาวต้องเท่ากับ 10 ตัวเท่านั้น");
                return;
            }

            // สร้าง connection string
            string connectionString = "server=127.0.0.1;user=root;password=;database=information";

            // คำสั่ง SQL สำหรับตรวจสอบว่ามี userid นี้ในฐานข้อมูลแล้วหรือไม่
            string checkQuery = "SELECT COUNT(*) FROM register WHERE userid = @userid";

            // เชื่อมต่อกับฐานข้อมูลและทำการตรวจสอบ userid
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@userid", userid);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    // ถ้ามี userid ที่ซ้ำกัน
                    if (count > 0)
                    {
                        MessageBox.Show("มีผู้ใช้งานระบบด้วย User ID นี้แล้ว");
                        return;
                    }
                }
            }

            // คำสั่ง SQL สำหรับ INSERT เข้าไปในฐานข้อมูล
            string query = "INSERT INTO register (name, lastname, userid, password, idcard, telephone, numberhome, dis1, dis2, pro, pri) VALUES (@name, @lastname, @userid, @password, @idcard, @telephone, @numberhome, @dis1, @dis2, @pro, @pri)";

            // เชื่อมต่อกับฐานข้อมูลและทำการบันทึกข้อมูล
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@idcard", idcard);
                    command.Parameters.AddWithValue("@telephone", telephone);
                    command.Parameters.AddWithValue("@numberhome", numberhome);
                    command.Parameters.AddWithValue("@dis1", dis1);
                    command.Parameters.AddWithValue("@dis2", dis2);
                    command.Parameters.AddWithValue("@pro", pro);
                    command.Parameters.AddWithValue("@pri", pri);

                    command.ExecuteNonQuery();

                    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว");
                    // สร้าง instance 
                    Form11 form11 = new Form11();

                    // ซ่อน 
                    this.Hide();

                    // แสดง
                    form11.Show();
                }
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        // ดึงข้อมูลจาก TextBoxes
        //string name = textBox1.Text;
        //string lastname = textBox2.Text;
        //string userid = textBox3.Text;
        //string password = textBox4.Text;
        //string idcard = textBox5.Text;
        //string telephone = textBox6.Text;
        //string numberhome = textBox7.Text;
        //string dis1 = textBox8.Text;
       // string dis2 = textBox9.Text;
        //string pro = textBox10.Text;
        //string pri = textBox11.Text;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            name = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            lastname = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            userid = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            password = textBox4.Text;
        }

        //ตรวจสอบเลขบัตรประชาชน

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string input = textBox5.Text;

            // ตรวจสอบว่าทุกตัวอักษรใน input เป็นตัวเลขหรือไม่
            if (input.All(char.IsDigit))
            {
                // หากเป็นตัวเลข กำหนดค่าให้กับตัวแปร idcard
                if (int.TryParse(input, out int result))
                {
                    idcard = result;
                }

                // ตรวจสอบความยาวของข้อมูลใน TextBox
                if (input.Length > 13)
                {
                    // แจ้งเตือนเมื่อกรอกเกิน 13 หลัก
                    MessageBox.Show("ไม่ควรกรอกเลขบัตรประชาชนมากกว่า 13 หลัก");
                    // ลบตัวอักษรที่เกิน 13 หลักออกจาก TextBox
                    textBox5.Text = input.Substring(0, 13);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    textBox5.Select(textBox5.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                textBox5.Text = string.Concat(input.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                textBox5.Select(textBox5.Text.Length, 0);
            }



        }

        //ตรวจสอบเบอร์โทรศัพท์
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string input = textBox6.Text;

            // ตรวจสอบว่าทุกตัวอักษรใน input เป็นตัวเลขหรือไม่
            if (input.All(char.IsDigit))
            {
                // หากเป็นตัวเลข กำหนดค่าให้กับตัวแปร idcard
                if (int.TryParse(input, out int result))
                {
                    idcard = result;
                }

                // ตรวจสอบความยาวของข้อมูลใน TextBox
                if (input.Length > 10)
                {
                    // แจ้งเตือนเมื่อกรอกเกิน 10 หลัก
                    MessageBox.Show("ไม่ควรกรอกเบอร์ดโทรศัพท์มากกว่า 10 หลัก");
                    // ลบตัวอักษรที่เกิน 10 หลักออกจาก TextBox
                    textBox6.Text = input.Substring(0, 10);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    textBox6.Select(textBox5.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                textBox6.Text = string.Concat(input.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                textBox6.Select(textBox6.Text.Length, 0);
            }

        }

        //ไปหน้าหลัก
        private void button2_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form11 form11 = new Form11();

            // ซ่อน 
            this.Hide();

            // แสดง
            form11.Show();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        //ตรวจสอบรหัสไปรษณีย์
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            string pri = textBox11.Text;
            // ตรวจสอบว่าทุกตัวอักษรใน input เป็นตัวเลขหรือไม่
            if (pri.All(char.IsDigit))
            {
                // หากเป็นตัวเลข กำหนดค่าให้กับตัวแปร idcard
                if (int.TryParse(pri, out int result))
                {
                    idcard = result;
                }

                // ตรวจสอบความยาวของข้อมูลใน TextBox
                if (pri.Length > 5)
                {
                    // แจ้งเตือนเมื่อกรอกเกิน 10 หลัก
                    MessageBox.Show("ไม่ควรกรอกรหัสไปรษณีย์เกิน5หลัก");
                    // ลบตัวอักษรที่เกิน 10 หลักออกจาก TextBox
                    textBox11.Text = pri.Substring(0, 5);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    textBox11.Select(textBox5.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                textBox11.Text = string.Concat(pri.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                textBox11.Select(textBox11.Text.Length, 0);
            }
        }
    }

}
