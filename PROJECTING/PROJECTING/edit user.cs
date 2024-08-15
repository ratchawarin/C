using Guna.UI2.WinForms;
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
    public partial class Form20 : Form
    {
        // ประกาศตัวแปรที่จำเป็นสำหรับเก็บข้อมูล
        private string name;
        private string lastname;
        private string userid;
        private string password;
        private int idcard;
        private string telephone;
        private Form7 form7;
        public Form20(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "server=127.0.0.1;user=root;password=;database=information;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // คำสั่ง SQL เพื่อดึงข้อมูลจากฐานข้อมูลโดยคำนึงถึง iduser จาก Form7
                string query = "SELECT * FROM register WHERE userid = @userid";

                MySqlCommand command = new MySqlCommand(query, connection);
                // ใช้คุณสมบัติ Id จาก Form7
                command.Parameters.AddWithValue("@userid", form7.Id);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // แสดงข้อมูลใน TextBox ตามที่กำหนด
                    guna2TextBox1.Text = reader["name"].ToString();
                    guna2TextBox2.Text = reader["lastname"].ToString();
                    guna2TextBox3.Text = reader["userid"].ToString();
                    guna2TextBox4.Text = reader["password"].ToString();
                    guna2TextBox5.Text = reader["idcard"].ToString();
                    guna2TextBox6.Text = reader["telephone"].ToString();
                    guna2TextBox7.Text = reader["numberhome"].ToString();
                    guna2TextBox8.Text = reader["dis1"].ToString();
                    guna2TextBox9.Text = reader["dis2"].ToString();
                    guna2TextBox10.Text = reader["pro"].ToString();
                    guna2TextBox11.Text = reader["pri"].ToString();
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลสำหรับ ID ที่ระบุ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form20_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            string input = guna2TextBox5.Text;

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
                    guna2TextBox5.Text = input.Substring(0, 13);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    guna2TextBox5.Select(guna2TextBox5.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                guna2TextBox5.Text = string.Concat(input.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                guna2TextBox5.Select(guna2TextBox5.Text.Length, 0);
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            string input = guna2TextBox6.Text;

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
                    guna2TextBox6.Text = input.Substring(0, 10);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    guna2TextBox6.Select(guna2TextBox6.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                guna2TextBox6.Text = string.Concat(input.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                guna2TextBox6.Select(guna2TextBox6.Text.Length, 0);
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox11_TextChanged(object sender, EventArgs e)
        {
            string pri = guna2TextBox11.Text;
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
                    guna2TextBox11.Text = pri.Substring(0, 5);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    guna2TextBox11.Select(guna2TextBox11.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                guna2TextBox11.Text = string.Concat(pri.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                guna2TextBox11.Select(guna2TextBox11.Text.Length, 0);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Close Form12 and send a DialogResult back to Form7
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลจาก TextBoxes
            string name = guna2TextBox1.Text;
            string lastname = guna2TextBox2.Text;
            string userid = guna2TextBox3.Text;
            string password = guna2TextBox4.Text;
            string idcard = guna2TextBox5.Text;
            string telephone = guna2TextBox6.Text;
            string numberhome = guna2TextBox7.Text;
            string dis1 = guna2TextBox8.Text;
            string dis2 = guna2TextBox9.Text;
            string pro = guna2TextBox10.Text;
            string pri = guna2TextBox11.Text;

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
            string connectionString = "server=127.0.0.1;user=root;password=;database=information;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // คำสั่ง SQL เพื่ออัปเดตข้อมูล
                string query = "UPDATE register SET name = @name, lastname = @lastname, password = @password, idcard = @idcard, telephone = @telephone, numberhome = @numberhome, dis1 = @dis1, dis2 = @dis2, pro = @pro, pri = @pri WHERE userid = @userid";

                MySqlCommand command = new MySqlCommand(query, connection);
                // ใช้ค่าที่ป้อนใน TextBox และ Id จาก Form7 เพื่ออัปเดตข้อมูลในฐานข้อมูล
                command.Parameters.AddWithValue("@name", guna2TextBox1.Text);
                command.Parameters.AddWithValue("@lastname", guna2TextBox2.Text);
                command.Parameters.AddWithValue("@password", guna2TextBox4.Text);
                command.Parameters.AddWithValue("@idcard", guna2TextBox5.Text);
                command.Parameters.AddWithValue("@telephone", guna2TextBox6.Text);
                command.Parameters.AddWithValue("@numberhome", guna2TextBox7.Text);
                command.Parameters.AddWithValue("@dis1", guna2TextBox8.Text);
                command.Parameters.AddWithValue("@dis2", guna2TextBox9.Text);
                command.Parameters.AddWithValue("@pro", guna2TextBox10.Text);
                command.Parameters.AddWithValue("@pri", guna2TextBox11.Text);
                command.Parameters.AddWithValue("@userid", form7.Id); // ใช้ค่า Id จาก Form7

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("อัปเดตข้อมูลเรียบร้อยแล้ว");
                }
                else
                {
                    MessageBox.Show("ไม่สามารถอัปเดตข้อมูลได้");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
