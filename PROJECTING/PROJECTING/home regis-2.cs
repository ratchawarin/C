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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        //ไปหน้าสมัคร
        private void button6_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form3 form3 = new Form3();

            // ซ่อน 
            this.Hide();

            // แสดง
            form3.Show();
        }

        //ไปหน้าเริ่มต้น
        private void button1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form1 form1 = new Form1();

            // ซ่อน 
            this.Hide();

            // แสดง
            form1.Show();
        }
        
        //ไปหน้า about
        private void button3_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form5 form5 = new Form5();

            // ซ่อน 
            this.Hide();

            // แสดง
            form5.Show();
        }

        //ไปหน้า login admin
        private void button4_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form6 form6 = new Form6();

            // ซ่อน 
            this.Hide();

            // แสดง
            form6.Show();
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }
    }
}
