using MySql.Data.MySqlClient;
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

namespace PROJECTING
{
    public partial class Form9 : Form
    {
        
        public Form9()
        {
            InitializeComponent();
            LoadDataToDataGridView();
            
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
        private void guna2TextBox5_TextChanged_2(object sender, EventArgs e)
        {

        }
        

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        // ปุ่มเลือกรูปภาพ
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            openFileDialog.Title = "Select an Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // อ่านไฟล์ภาพจากที่ผู้ใช้เลือก
                    string selectedImagePath = openFileDialog.FileName;
                    // นำเข้าภาพไปยัง PictureBox
                    guna2PictureBox1.Image = Image.FromFile(selectedImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void ClearInputs()
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox4.Text = "";
            
            guna2PictureBox1.Image = null;
        }



        // ปุ่มเพิ่มข้อมูล
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าข้อมูลที่จำเป็นถูกกรอกครบหรือไม่
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox2.Text) || string.IsNullOrWhiteSpace(guna2TextBox5.Text) || string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||  guna2PictureBox1.Image == null  )
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ดึงข้อมูลจาก TextBoxes
                string id = guna2TextBox1.Text;
                string name = guna2TextBox2.Text;
                int count = int.Parse(guna2TextBox5.Text);
                int price = int.Parse(guna2TextBox4.Text);
                
                
                
                Image image = guna2PictureBox1.Image;
                byte[] imageData = ImageToByteArray(image);

                // เชื่อมต่อฐานข้อมูล MySQL
                string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // เตรียมคำสั่ง SQL สำหรับการเพิ่มข้อมูลลงในตาราง adminstock
                    string query = "INSERT INTO adminstock (id, name, count, price,  image) VALUES (@Id, @Name, @Count, @Price, @Image) ";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // เพิ่มพารามิเตอร์และกำหนดค่าของพารามิเตอร์
                        command.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;
                        command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = name;
                        command.Parameters.Add("@Count", MySqlDbType.Int32).Value = count;
                        command.Parameters.Add("@Price", MySqlDbType.Int32).Value = price;
                        
                        command.Parameters.Add("@Image", MySqlDbType.VarBinary).Value = imageData;
                        
                        

                        // ประมวลผลคำสั่ง SQL
                        command.ExecuteNonQuery();
                    }
                }
                // เมื่อเพิ่มข้อมูลเสร็จสิ้น ให้ลบค่าใน TextBoxes และรูปภาพออก
                ClearInputs();
                MessageBox.Show("เพิ่มข้อมูลเรียบร้อยแล้ว");

                // โหลดข้อมูลใหม่เข้าสู่ DataGridView
                LoadDataToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล: " + ex.Message);
            }
        }

        // textbox ค้นหาชื่อหรือไอดี
        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            string searchText = guna2TextBox6.Text.Trim();

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



       
        // ดึงข้อมูลจาก database
        private void LoadDataToDataGridView()
        {
            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
            string query = "SELECT * FROM adminstock";

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

                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

       

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }



        //ปุ่ม select ดึงข้อมูลมาแสดง
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่ถูกเลือกใน DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // ดึงแถวที่ถูกเลือก
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];

                // แสดงข้อมูลจากแถวที่ถูกเลือกใน TextBoxes
                guna2TextBox1.Text = selectedRow.Cells["id"].Value.ToString();
                guna2TextBox2.Text = selectedRow.Cells["name"].Value.ToString();
                guna2TextBox5.Text = selectedRow.Cells["count"].Value.ToString();
                guna2TextBox4.Text = selectedRow.Cells["price"].Value.ToString();
                
                
                

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

        //ปุ่มลบข้อมูล
        private void guna2Button5_Click(object sender, EventArgs e)
        {

            // ตรวจสอบว่ามีแถวที่ถูกเลือกใน DataGridView หรือไม่
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // แสดงกล่องข้อความยืนยันการลบ
                DialogResult result = MessageBox.Show("คุณแน่ใจหรือไม่ที่ต้องการลบข้อมูลนี้?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้ต้องการลบ
                if (result == DialogResult.Yes)
                {
                    // ดึง ID ของแถวที่เลือก
                    string idToDelete = guna2DataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

                    // เชื่อมต่อฐานข้อมูล MySQL
                    string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // เตรียมคำสั่ง SQL สำหรับการลบข้อมูลในตาราง adminstock โดยใช้ ID
                        string query = "DELETE FROM adminstock WHERE id = @Id";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            // เพิ่มพารามิเตอร์และกำหนดค่าของพารามิเตอร์ ID
                            command.Parameters.Add("@Id", MySqlDbType.VarChar).Value = idToDelete;

                            // ประมวลผลคำสั่ง SQL
                            int rowsAffected = command.ExecuteNonQuery();

                            // ถ้ามีแถวถูกลบ
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว");

                                // รีเฟรชข้อมูลใน DataGridView เพื่ออัปเดตการแสดงผล
                                LoadDataToDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถลบข้อมูลได้", "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการลบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        // ปุ่ม update ข้อมูล
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่ถูกเลือกใน DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // ดึงข้อมูลจาก TextBoxes
                string id = guna2TextBox1.Text;
                string name = guna2TextBox2.Text;
                int count;
                int price;
                if (!int.TryParse(guna2TextBox5.Text, out count) || !int.TryParse(guna2TextBox4.Text, out price))
                {
                    MessageBox.Show("กรุณากรอกข้อมูลจำนวนและราคาให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                
                
                Image image = guna2PictureBox1.Image;
                byte[] imageData = ImageToByteArray(image);

                // สร้างคำสั่ง SQL UPDATE กับเงื่อนไข WHERE
                string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
                string query = "UPDATE adminstock SET name = @Name, count = @Count, price = @Price,  image = @Image  WHERE id = @Id";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // เพิ่มพารามิเตอร์และกำหนดค่าของพารามิเตอร์
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Count", count);
                        command.Parameters.AddWithValue("@Price", price);
                        
                        command.Parameters.AddWithValue("@Image", imageData);
                        

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("อัปเดตข้อมูลเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataToDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถอัปเดตข้อมูลได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการอัปเดต", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


       

        private void Form9_Load(object sender, EventArgs e)
        {

        }
        // ปุ่มย้อนกลับไปหน้า systemadmin
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form8 form8 = new Form8();

            // ซ่อน 
            this.Hide();

            // แสดง
            form8.Show();
        }

       

        
    }


}
