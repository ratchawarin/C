﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //ปุ่มไปหน้าหลัก
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form11 form11 = new Form11();

            // ซ่อน 
            this.Hide();

            // แสดง
            form11.Show();
        }
    }
}