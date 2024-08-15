using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient; // Import MySqlClient namespace
namespace PROJECTING
{
    public partial class Form16 : Form
    {


        public void ClearGunaDataGridView2()
        {

        }

        // Connection string to connect to the database
        private string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
        private string connectionStringForDatabase2 = "server=127.0.0.1;user=root;password=;database=information;";

        public Form16()
        {
            InitializeComponent();
            DisplayData();
            CalculateTotal(); // คำนวณและแสดงผลรวมทันทีเมื่อโปรแกรมเริ่มต้น
            LoadDataToDataGridView();
        }
        //ดึงข้อมูลมาจาก database มาแสดงใน datagrid 2 orderuser
        private void LoadDataToDataGridView()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM oderuser";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // ลบคอลัมน์ "ID" ออกจาก DataTable
                    table.Columns.Remove("ID");
                    guna2DataGridView2.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //ดึงข้อมูลมาจาก database มาแสดงใน datagrid 1
        private void DisplayData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT id, name, count, price, image FROM adminstock WHERE id NOT REGEXP '^[a-zA-Z]+00$'";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        guna2DataGridView1.DataSource = dataTable;
                        guna2DataGridView1.RowTemplate.Height = 50;

                        // แสดงรูปภาพใน DataGridView
                        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                        imageColumn = (DataGridViewImageColumn)guna2DataGridView1.Columns["image"];
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchByName(guna2TextBox1.Text);
        }

        //ค้นหาข้อมูลมาจาก database มาแสดงใน datagrid 1 orderuser
        private void SearchByName(string searchValue)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT id, name, count, price, image FROM adminstock WHERE name LIKE @searchValue AND id NOT REGEXP '^[a-zA-Z]+00$'";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        guna2DataGridView1.DataSource = dataTable;
                        guna2DataGridView1.RowTemplate.Height = 50;

                        // แสดงรูปภาพใน DataGridView
                        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                        imageColumn = (DataGridViewImageColumn)guna2DataGridView1.Columns["image"];
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }



        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        //ปุ่ม select เลือกเพื่อดึงแสดงข้อมูลออกไปแสดง
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลจาก DataGridView ที่เลือกอยู่
            DataGridViewRow selectedRow = guna2DataGridView1.CurrentRow;

            if (selectedRow != null)
            {
                // กำหนดข้อมูล id ไปยัง TextBox2
                guna2TextBox2.Text = selectedRow.Cells["id"].Value.ToString();


                // กำหนดข้อมูล name ไปยัง TextBox4
                guna2TextBox4.Text = selectedRow.Cells["name"].Value.ToString();



                // กำหนดข้อมูล price ไปยัง TextBox3
                decimal price = Convert.ToDecimal(selectedRow.Cells["price"].Value);
                guna2TextBox3.Text = price.ToString("#,##0");

                // กำหนดข้อมูลรูปภาพไปยัง PictureBox1
                byte[] imgData = (byte[])selectedRow.Cells["image"].Value;
                using (MemoryStream ms = new MemoryStream(imgData))
                {
                    guna2PictureBox1.Image = Image.FromStream(ms);
                }

                // แสดงราคาปัจจุบันใน TextBox5
                guna2TextBox5.Text = price.ToString("#,##0");
            }
        }
        //ปุ่มเพิ่มจำนวนสินค้า
        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // ดึงข้อมูลจาก DataGridView ที่เลือกอยู่
            DataGridViewRow selectedRow = guna2DataGridView1.CurrentRow;

            if (selectedRow == null)
            {
                MessageBox.Show("กรุณาเลือกรายการก่อนที่จะเพิ่มจำนวน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ดึงค่าจำนวนที่ถูกเลือกจาก Guna2NumericUpDown
            int quantity = (int)guna2NumericUpDown1.Value;

            // ค่า count ใน DataGridView
            int countInStock = (int)selectedRow.Cells["count"].Value;

            // ถ้าจำนวนที่เลือกมากกว่าจำนวนที่มีอยู่ในคอลัมน์ "count"
            if (quantity > countInStock)
            {
                MessageBox.Show("จำนวนที่เลือกมากกว่าจำนวนที่มีอยู่ในสต็อก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2NumericUpDown1.Value = countInStock; // เซ็ตค่าให้เท่ากับจำนวนในสต็อก
                return;
            }


            // คำนวณราคารวม
            decimal price = Convert.ToDecimal(selectedRow.Cells["price"].Value);
            decimal totalPrice = price * quantity;

            // แสดงผลลัพธ์ใน TextBox5
            guna2TextBox5.Text = totalPrice.ToString("#,##0");
        }


        //เพิ่มข้อมูลใน database
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลจาก DataGridView ที่เลือกอยู่
            DataGridViewRow selectedRow = guna2DataGridView1.CurrentRow;

            if (selectedRow != null)
            {
                // ดึงข้อมูลที่ต้องการจากแถวที่เลือก
                string idname = selectedRow.Cells["id"].Value.ToString();
                string name = selectedRow.Cells["name"].Value.ToString();
                decimal price = Convert.ToDecimal(selectedRow.Cells["price"].Value);
                int count = (int)guna2NumericUpDown1.Value;
                decimal total = Convert.ToDecimal(guna2TextBox5.Text);

                // ยืนยันการเพิ่มข้อมูล
                DialogResult result = MessageBox.Show("ต้องการเพิ่มข้อมูลการสั่งซื้อใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // เชื่อมต่อกับฐานข้อมูล
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            // เตรียมคำสั่ง SQL เพื่อเพิ่มข้อมูล
                            string query = "INSERT INTO oderuser (idname, name, price, count, total) VALUES (@Idname,@Name, @Price, @Count, @Total)";

                            // สร้างคำสั่ง SQL
                            MySqlCommand command = new MySqlCommand(query, connection);

                            // เพิ่มพารามิเตอร์
                            command.Parameters.AddWithValue("@Idname", idname);
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Price", price);
                            command.Parameters.AddWithValue("@Count", count);
                            command.Parameters.AddWithValue("@Total", total);

                            // ประมวลผลคำสั่ง SQL
                            int rowsAffected = command.ExecuteNonQuery();

                            // ตรวจสอบว่ามีการเพิ่มข้อมูลหรือไม่
                            if (rowsAffected > 0)
                            {
                                // แสดงข้อความเมื่อเพิ่มข้อมูลสำเร็จ
                                MessageBox.Show("เพิ่มข้อมูลการสั่งซื้อเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // อัปเดตค่า count ใน DataGridView 1
                                int newCount = (int)selectedRow.Cells["count"].Value - count;
                                selectedRow.Cells["count"].Value = newCount;

                                // อัปเดตค่า count ในฐานข้อมูล adminstock
                                UpdateCountInAdminStock(idname, newCount);

                                // โหลดข้อมูลใหม่ลงใน DataGridView 2
                                LoadDataToDataGridView();
                            }
                            else
                            {
                                // แสดงข้อความเมื่อไม่สามารถเพิ่มข้อมูลได้
                                MessageBox.Show("ไม่สามารถเพิ่มข้อมูลการสั่งซื้อได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // หากผู้ใช้กด No จะไม่ทำอะไรเพิ่มเติม
            }
        }

        // เมท็อดสำหรับอัปเดตค่า count ในฐานข้อมูล adminstock

        private void UpdateCountInAdminStock(string productId, int newCount)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE adminstock SET count = @newCount WHERE id = @productId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newCount", newCount);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form16_Load(object sender, EventArgs e)
        {

        }

        
        //ปุ่มลบ
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่เลือกอยู่หรือไม่
            if (guna2DataGridView2.SelectedRows.Count > 0)
            {
                // แสดง MessageBox เป็น Yes/No เพื่อให้ผู้ใช้ยืนยันการลบและอัปเดตข้อมูล
                DialogResult result = MessageBox.Show("คุณต้องการลบใช่หรือไม่?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ตรวจสอบผลลัพธ์ที่ผู้ใช้เลือก
                if (result == DialogResult.Yes)
                {
                    // ดึงข้อมูลจากแถวที่เลือก
                    DataGridViewRow selectedRow = guna2DataGridView2.SelectedRows[0];
                    string idname = selectedRow.Cells["idname"].Value.ToString();
                    int countToRemove = (int)selectedRow.Cells["count"].Value;

                    try
                    {
                        // เชื่อมต่อกับฐานข้อมูล
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            // เตรียมคำสั่ง SQL เพื่อลบข้อมูล
                            string deleteQuery = "DELETE FROM oderuser WHERE idname = @Idname";

                            // สร้างคำสั่ง SQL
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);

                            // เพิ่มพารามิเตอร์
                            deleteCommand.Parameters.AddWithValue("@Idname", idname);

                            // ประมวลผลคำสั่ง SQL
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            // ตรวจสอบว่ามีการลบข้อมูลหรือไม่
                            if (rowsAffected > 0)
                            {
                                // อัปเดตค่า count ใน DataGridView 1
                                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                                {
                                    if (row.Cells["id"].Value.ToString() == idname)
                                    {
                                        int currentCount = (int)row.Cells["count"].Value;
                                        row.Cells["count"].Value = currentCount + countToRemove;
                                        // อัปเดตค่า count ในฐานข้อมูล adminstock
                                        UpdateCountInAdminStock(idname, currentCount + countToRemove);
                                        // แสดงข้อความเมื่อลบและอัปเดตข้อมูลสำเร็จ
                                        MessageBox.Show("ข้อมูลถูกลบและค่า count ในฐานข้อมูล adminstock ได้ถูกอัปเดตสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                }

                                // โหลดข้อมูลใหม่ลงใน DataGridView 2
                                LoadDataToDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถลบข้อมูลได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // ถ้าผู้ใช้เลือก "No" หรือปิด MessageBox ไม่ต้องดำเนินการใดๆ
                }
            }
            else
            {
                MessageBox.Show("โปรดเลือกข้อมูลที่ต้องการลบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void CalculateTotal()
        {



        }

        
        // ปุ่มย้อนกลับไป system admin
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form8 form8 = new Form8();

            // ซ่อน 
            this.Hide();

            // แสดง
            form8.Show();
        }

        // ปุ่มไปหน้า true bill
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form18 form18 = new Form18();

            // ซ่อน 
            this.Hide();

            // แสดง
            form18.Show();
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }


     }
        
}
