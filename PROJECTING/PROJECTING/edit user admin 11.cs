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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
            LoadDataToDataGridView();
        }
        private void LoadDataToDataGridView()
        {
            string connectionString = "server=127.0.0.1;user=root;password=;database=information;";
            string query = "SELECT * FROM register";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    guna2DataGridView1.DataSource = table;
                    //Guna2DataGridView.AutoResizeRow
                    guna2DataGridView1.RowTemplate.Height = 100;


                }
            }
        }

        // เตรียมข้อมูลที่จะอัพเดต
        //string name = guna2TextBox1.Text;
        //string lastname = guna2TextBox2.Text;
        //string userid = guna2TextBox3.Text;
        //string password = guna2TextBox4.Text;
        //string idcard = guna2TextBox5.Text;
        //string telephone = guna2TextBox6.Text;
        //string numberhome = guna2TextBox7.Text;
        //string dis1 = guna2TextBox8.Text;
        //string dis2 = guna2TextBox9.Text;
        //string pro = guna2TextBox10.Text;
        // string pri = guna2TextBox11.Text;
        //string id = guna2TextBox13.Text;

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
            string idcard = guna2TextBox5.Text;
            // ตรวจสอบว่าทุกตัวอักษรใน input เป็นตัวเลขหรือไม่
            if (idcard.All(char.IsDigit))
            {
                // หากเป็นตัวเลข กำหนดค่าให้กับตัวแปร idcard
                if (int.TryParse(idcard, out int result))
                {
                    idcard = result.ToString();
                }

                // ตรวจสอบความยาวของข้อมูลใน TextBox
                if (idcard.Length > 13)
                {
                    // แจ้งเตือนเมื่อกรอกเกิน 13 หลัก
                    MessageBox.Show("ไม่ควรกรอกเลขบัตรประชาชนมากกว่าหรือน้อยกว่า 13 หลัก");
                    // ลบตัวอักษรที่เกิน 13 หลักออกจาก TextBox
                    guna2TextBox5.Text = idcard.Substring(0, 13);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    guna2TextBox5.Select(guna2TextBox5.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                guna2TextBox5.Text = string.Concat(idcard.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                guna2TextBox5.Select(guna2TextBox5.Text.Length, 0);
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            string telephone = guna2TextBox6.Text;
            // ตรวจสอบว่าทุกตัวอักษรใน input เป็นตัวเลขหรือไม่
            if (telephone.All(char.IsDigit))
            {
                // ตรวจสอบความยาวของข้อมูลใน TextBox
                if (telephone.Length > 10)
                {
                    // แจ้งเตือนเมื่อกรอกเกิน 10 หลัก
                    MessageBox.Show("ไม่ควรกรอกหมายเลขโทรศัพท์มากกว่า 10 หลัก");
                    // ลบตัวอักษรที่เกิน 10 หลักออกจาก TextBox
                    guna2TextBox6.Text = telephone.Substring(0, 10);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    guna2TextBox6.Select(guna2TextBox6.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                guna2TextBox6.Text = string.Concat(telephone.Where(char.IsDigit));

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
            string pro = guna2TextBox11.Text;
            // ตรวจสอบว่าทุกตัวอักษรใน input เป็นตัวเลขหรือไม่
            if (pro.All(char.IsDigit))
            {
                // ตรวจสอบความยาวของข้อมูลใน TextBox
                if (pro.Length > 5)
                {
                    // แจ้งเตือนเมื่อกรอกเกิน 5 หลัก
                    MessageBox.Show("ไม่ควรกรอกรหัสไปรษณีย์มากกว่า 5 หลัก");
                    // ลบตัวอักษรที่เกิน 5 หลักออกจาก TextBox
                    guna2TextBox11.Text = pro.Substring(0, 5);
                    // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                    guna2TextBox11.Select(guna2TextBox11.Text.Length, 0);
                }
            }
            else
            {
                // หากไม่เป็นตัวเลข แสดงข้อความเตือน
                MessageBox.Show("กรุณากรอกเฉพาะตัวเลข");

                // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจาก TextBox
                guna2TextBox11.Text = string.Concat(pro.Where(char.IsDigit));

                // ตั้งตำแหน่ง Cursor ที่ต่ำสุดที่ยังเหลือใน TextBox
                guna2TextBox11.Select(guna2TextBox11.Text.Length, 0);
            }
        }

        

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form14_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่ถูกเลือกหรือไม่
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                guna2TextBox13.ReadOnly = true; // ทำให้ไม่สามารถแก้ไข TextBox 13 ได้เมื่อมีการเลือกข้อมูล
            }
            else
            {
                guna2TextBox13.ReadOnly = false; // ทำให้สามารถแก้ไข TextBox 13 ได้เมื่อไม่มีการเลือกข้อมูล
            }
        }

        private void guna2TextBox12_TextChanged(object sender, EventArgs e)
        {
            string searchText = guna2TextBox12.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                // Suspend binding
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[guna2DataGridView1.DataSource];
                currencyManager.SuspendBinding();

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["telephone"].Value != null && row.Cells["telephone"].Value.ToString().Contains(searchText))
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่ถูกเลือกใน Guna2DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // ดึงแถวที่ถูกเลือก
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];

                // นำข้อมูลจากแถวที่ถูกเลือกมาใส่ใน Guna2TextBoxes
                guna2TextBox13.Text = selectedRow.Cells["ID"].Value.ToString();
                guna2TextBox1.Text = selectedRow.Cells["name"].Value.ToString();
                guna2TextBox2.Text = selectedRow.Cells["lastname"].Value.ToString();
                guna2TextBox3.Text = selectedRow.Cells["userid"].Value.ToString();
                guna2TextBox4.Text = selectedRow.Cells["password"].Value.ToString();
                guna2TextBox5.Text = selectedRow.Cells["idcard"].Value.ToString();
                guna2TextBox6.Text = selectedRow.Cells["telephone"].Value.ToString();
                guna2TextBox7.Text = selectedRow.Cells["numberhome"].Value.ToString();
                guna2TextBox8.Text = selectedRow.Cells["dis1"].Value.ToString();
                guna2TextBox9.Text = selectedRow.Cells["dis2"].Value.ToString();
                guna2TextBox10.Text = selectedRow.Cells["pro"].Value.ToString();
                guna2TextBox11.Text = selectedRow.Cells["pri"].Value.ToString();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการดึงข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //ปุ่มลบข้อมูล
        private void guna2Button3_Click(object sender, EventArgs e)
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
                    // ดึงค่า ID หรือ Primary Key ของแถวที่เลือก
                    string idToDelete = row.Cells["ID"].Value.ToString();

                    // สร้างคำสั่ง SQL เพื่อลบข้อมูล
                    string connectionString = "server=127.0.0.1;user=root;password=;database=information;";
                    string query = "DELETE FROM register WHERE ID = @id";

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

                    // ลบแถวที่เลือกออกจาก Guna2DataGridView
                    guna2DataGridView1.Rows.Remove(row);
                }
            }
        }

        // ปุ่มอัพเดท
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีข้อมูลทุกช่องถูกกรอกหรือไม่
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox3.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox5.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox6.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox7.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox8.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox9.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox10.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox11.Text)) // เพิ่มเงื่อนไขตรวจสอบ TextBox ที่ 11
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบทุกช่อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ตรวจสอบเลข pri ว่ามีความยาวไม่ถึง 5 ตัวหรือไม่
            if (guna2TextBox11.Text.Length < 5)
            {
                MessageBox.Show("เลขไปรษณีย์ต้องมี 5 ตัว", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // เตรียมข้อมูลที่จะอัพเดต
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
            string id = guna2TextBox13.Text;

            // เชื่อมต่อฐานข้อมูล MySQL
            string connectionString = "server=127.0.0.1;user=root;password=;database=information;";
            string query = "UPDATE register SET Name = @name, LastName = @lastname, UserID = @userid, Password = @password, IDCard = @idcard, Telephone = @telephone, NumberHome = @numberhome, Dis1 = @dis1, Dis2 = @dis2, Pro = @pro, Pri = @pri WHERE ID = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // เพิ่มพารามิเตอร์
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
                    command.Parameters.AddWithValue("@id", id); // ใส่ ID ของแถวที่ต้องการอัพเดตที่นี่

                    try
                    {
                        // เปิดการเชื่อมต่อและดำเนินการอัพเดตข้อมูล
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("อัพเดตข้อมูลเรียบร้อยแล้ว");
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่จะอัพเดต");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการอัพเดตข้อมูล: " + ex.Message);
                    }
                }
            }
            // โหลดข้อมูลใหม่ลงใน DataGridView
            LoadDataToDataGridView();
        }


        private void guna2TextBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
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
