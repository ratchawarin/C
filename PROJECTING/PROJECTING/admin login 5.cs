using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROJECTING
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        // กำหนดรหัสผ่าน
        private const string AdminPassword = "1234";
        private const string AdminId = "admin";

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }
        private string password;
        private bool showPassword;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
            if (!showPassword)
            {
                textBox2.PasswordChar = '*'; // ซ่อนรหัสผ่าน
            }

        }
        // ปุ่มย้อนกลับไปหน้าหลัก
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form11 form11 = new Form11();

            // ซ่อน 
            this.Hide();

            // แสดง
            form11.Show();
        }


        // สร้างปุ่มไปหน้า system admin
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string enteredId = textBox1.Text; // รหัส id ที่ผู้ใช้กรอกเข้ามา
            string enteredPassword = textBox2.Text; // รหัสผ่านที่ผู้ใช้กรอกเข้ามา

            if (enteredId == AdminId && enteredPassword == AdminPassword)
            {
                MessageBox.Show("เข้าสู่ระบบสำเร็จ"); // กรณี id และรหัสผ่านถูกต้อง
                Form8 form8 = new Form8(); // สร้าง instance ของฟอร์มที่ต้องการไป
                form8.Show(); // แสดงฟอร์มใหม่
                this.Hide(); // ซ่อนฟอร์มปัจจุบัน

            }
            else
            {
                MessageBox.Show("รหัสหรือรหัสผ่านไม่ถูกต้อง"); // กรณี id หรือรหัสผ่านไม่ถูกต้อง
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

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
