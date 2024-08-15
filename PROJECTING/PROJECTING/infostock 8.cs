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
using MySql.Data.MySqlClient;

namespace PROJECTING
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
            LoadDataToDataGridView();
        }


        private void LoadDataToDataGridView()
        {
            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
            string query = "SELECT * FROM adminstock2";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    guna2DataGridView1.DataSource = table;
                    //Guna2DataGridView.AutoResizeRow
                    guna2DataGridView1.RowTemplate.Height = 100;

                    // แสดงรูปภาพใน DataGridView
                    DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                    imageColumn = (DataGridViewImageColumn)guna2DataGridView1.Columns["image"];
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    // กำหนดความสูงของรูปภาพในแต่ละเซลล์ให้เป็นขนาดเดียวกัน
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        row.Height = 100;
                    }
                }
            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // ปุ่มย้อนกลับไปหน้า system admin
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form8 form8 = new Form8();

            // ซ่อน 
            this.Hide();

            // แสดง
            form8.Show();
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

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        //ปุ่มอัพโหลดรูปภาพ
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // ตั้งค่าสำหรับการเลือกไฟล์รูปภาพ
            openFileDialog1.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // โหลดรูปภาพจากที่เลือกและนำมาแสดงใน PictureBox
                    guna2PictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }

        //ปุ่ม add ข้อมูล
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีข้อมูลที่จะเพิ่มหรือไม่
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||

                guna2PictureBox1.Image == null)
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบทุกช่อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // เตรียมข้อมูลที่จะเพิ่ม
            string id = guna2TextBox1.Text;
            string name = guna2TextBox2.Text;

            Image image = guna2PictureBox1.Image;

            // แปลงรูปภาพเป็น byte array
            byte[] imageBytes = ImageToByteArray(image);

            // สร้างคำสั่ง SQL เพื่อเพิ่มข้อมูล
            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
            string query = "INSERT INTO adminstock2 (ID,name,  image) VALUES (@id,@name, @image)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // เพิ่มพารามิเตอร์
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@image", imageBytes);

                    try
                    {
                        // เปิดการเชื่อมต่อและดำเนินการเพิ่มข้อมูล
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("เพิ่มข้อมูลเรียบร้อยแล้ว");

                            // เรียกเมธอดสำหรับโหลดข้อมูลใหม่และแสดงใน DataGridView
                            LoadDataToDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                    }
                }
            }
        }


        // ฟังก์ชันสำหรับแปลงรูปภาพเป็น byte array
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }

        //ปุ่ม ลบ ข้อมูล
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่ถูกเลือกหรือไม่
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("โปรดเลือกข้อมูลที่ต้องการลบ", "ข้อมูลไม่ถูกเลือก", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ขอสิทธิ์การลบข้อมูลจากผู้ใช้
            DialogResult result = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลที่เลือก?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // ลบแถวที่เลือกออกจากฐานข้อมูล
                foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
                {
                    // ดึงค่า ID ของแถวที่เลือก
                    string idToDelete = row.Cells["ID"].Value.ToString();

                    // สร้างคำสั่ง SQL เพื่อลบข้อมูล
                    string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
                    string query = "DELETE FROM adminstock2 WHERE ID = @id";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            // เพิ่มพารามิเตอร์
                            command.Parameters.AddWithValue("@id", idToDelete);

                            try
                            {
                                // เปิดการเชื่อมต่อและดำเนินการลบข้อมูล
                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว");
                                }
                                else
                                {
                                    MessageBox.Show("ไม่สามารถลบข้อมูลได้");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message);
                            }
                        }
                    }

                    // ลบแถวที่เลือกออกจาก DataGridView
                    guna2DataGridView1.Rows.Remove(row);
                }
            }
        }


        // ฟังก์ชันสำหรับแปลงข้อมูล byte array เป็นรูปภาพ (Image)
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(memoryStream);
                return returnImage;
            }
        }
        //ปุ่ม select ข้อมูล
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่ถูกเลือกใน DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // ดึงแถวที่ถูกเลือก
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];

                // แสดงข้อมูลจากแถวที่ถูกเลือกใน TextBoxes
                guna2TextBox1.Text = selectedRow.Cells["id"].Value.ToString();

                // แสดงข้อมูลจากแถวที่ถูกเลือกใน TextBoxes
                guna2TextBox2.Text = selectedRow.Cells["name"].Value.ToString();
                                // อ่านข้อมูลภาพจากเซลล์และแสดงใน PictureBox
                if (selectedRow.Cells["image"].Value != DBNull.Value)
                {
                    byte[] imageData = (byte[])selectedRow.Cells["image"].Value;
                    guna2PictureBox1.Image = ByteArrayToImage(imageData);
                }
                else
                {
                    guna2PictureBox1.Image = null;
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการดึงข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //ปุ่ม update ข้อมูล
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าข้อมูลถูกกรอกครบทุกช่องหรือไม่
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||

                guna2PictureBox1.Image == null)
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบทุกช่อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // เตรียมข้อมูลที่จะอัพเดท
            string id = guna2TextBox1.Text;
            string name = guna2TextBox2.Text;
            Image image = guna2PictureBox1.Image;
            byte[] imageBytes = ImageToByteArray(image);

            // สร้างคำสั่ง SQL เพื่ออัพเดทข้อมูล
            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
            string query = "UPDATE adminstock2 SET name=@name, image = @image WHERE ID = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // เพิ่มพารามิเตอร์
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@image", imageBytes);

                    try
                    {
                        // เปิดการเชื่อมต่อและดำเนินการอัพเดทข้อมูล
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("อัพเดทข้อมูลเรียบร้อยแล้ว");

                            // เรียกเมธอดสำหรับโหลดข้อมูลใหม่และแสดงใน DataGridView
                            LoadDataToDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดในการอัพเดทข้อมูล");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                    }
                }
            }
        }

        private void guna2TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        //TextBox ค้นหาข้อมูล 
        private void guna2TextBox3_TextChanged_1(object sender, EventArgs e)
        {
            string searchText = guna2TextBox3.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                // Suspend binding
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[guna2DataGridView1.DataSource];
                currencyManager.SuspendBinding();

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if ((row.Cells["Name"].Value != null && row.Cells["Name"].Value.ToString().Contains(searchText)) || (row.Cells["ID"].Value != null && row.Cells["ID"].Value.ToString().Contains(searchText)))
                        {
                            row.Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                }

                // Resume binding
                currencyManager.ResumeBinding();
            }
            else
            {
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    row.Visible = true;
                }
            }
        }
    }
}
