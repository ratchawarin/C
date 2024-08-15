using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECTING
{
    public partial class Form8 : Form
    {
        

        public Form8()
        {
            InitializeComponent();
        }

        

        // ไปหน้า stock 
        private void button1_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(); // สร้าง instance ของฟอร์มที่ต้องการไป
            form9.Show(); // แสดงฟอร์มใหม่
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form6 form6 = new Form6();

            // ซ่อน 
            this.Hide();

            // แสดง
            form6.Show();
        }

        // ไปหน้า info stock 

        private void button2_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form13 form13 = new Form13();

            // ซ่อน 
            this.Hide();

            // แสดง
            form13.Show();
        }
        // ไปหน้า edit user 
        private void button3_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form14 form14 = new Form14();

            // ซ่อน 
            this.Hide();

            // แสดง
            form14.Show();
        }

        // ไปหน้า bill one 
        private void button4_Click(object sender, EventArgs e)
        {
            // สร้าง instance ของ Form16 โดยส่ง DataTable ที่ต้องการไปยัง constructor
            Form16 form16 = new Form16();

            // ซ่อน Form8
            this.Hide();

            // แสดง Form16
            form16.Show();
        }

        // ไปหน้า history 
        private void button5_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form17 form17 = new Form17();

            // ซ่อน 
            this.Hide();

            // แสดง
            form17.Show();
        }

        // ปุ่มไปหน้าร้านค้า
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            // สร้าง instance ของ Form7 ด้วยการส่งพารามิเตอร์ที่เหมาะสม
            Form7 form7 = new Form7("username", "password", "email");

            // สร้าง instance ของ Form12 ด้วยการส่ง instance ของ Form7 ที่เราสร้างไป
            Form12 form12 = new Form12(form7);

            // ซ่อน Form8
            this.Hide();

            // แสดง Form12
            form12.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
