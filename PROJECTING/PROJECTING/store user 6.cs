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

namespace PROJECTING
{
    public partial class Form12 : Form
    {

        
        private Form7 form7;
        
        public Form12(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;

            
        }
        // จำนวนราคา
        private void label1_Click(object sender, EventArgs e)
        {

        }

        //โหลด id 00 ไปแสดงใน combobox

        private void Form12_Load(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";

            // Create connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Query to select items with ID ending with '00'
                string query = "SELECT ID, name FROM adminstock WHERE ID LIKE '%00'";

                // Create a command using the query and connection
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Create a data adapter to fill a DataTable
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {

                        // Create a DataTable to store the results
                        DataTable table = new DataTable();

                        // Fill the DataTable with the results of the query
                        adapter.Fill(table);
                        // Insert an empty row at the beginning of the DataTable
                        DataRow newRow = table.NewRow();
                        newRow["name"] = "please choose";

                        table.Rows.InsertAt(newRow, 0);

                        // Set the DisplayMember and ValueMember for guna2ComboBox1
                        guna2ComboBox1.DisplayMember = "name";
                        guna2ComboBox1.ValueMember = "ID";

                        // Set the DataSource for guna2ComboBox1
                        guna2ComboBox1.DataSource = table;

                        
                    }
                }
            }



        }
        //ปุ่มย้อนกลับไปหน้า system admin
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form8 form8 = new Form8();

            // ซ่อน 
            this.Hide();

            // แสดง
            form8.Show();
        }

        //เลือกข้อมูลมาแสดงใน combobox
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedValue != null)
            {
                string selectedID = guna2ComboBox1.SelectedValue.ToString();

                string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ID, name FROM adminstock WHERE ID LIKE @selectedID AND NOT ID LIKE '%00'";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    if (selectedID.Length >= 1)
                    {
                        command.Parameters.AddWithValue("@selectedID", selectedID.Substring(0, 1) + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);


                            guna2ComboBox2.DisplayMember = "name";
                            guna2ComboBox2.ValueMember = "ID";
                            guna2ComboBox2.DataSource = table;
                        }
                    }
                    else
                    {
                        label1.Text = "";
                        label2.Text = "";
                        // ลบข้อมูลออกจาก combobox 2 โดยการกำหนด DataSource เป็น null
                        guna2ComboBox2.DataSource = null;
                        guna2PictureBox1.Image = null; // Clear the image
                        
                    }
                }
            }

            

        }


        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        //เลือก id ที่ตรงกันกับ combobox 1
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedValue != null)
            {
                
                string selectedID = guna2ComboBox2.SelectedValue.ToString();

                string connectionString = "server=127.0.0.1;user=root;password=;database=stock;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to select price and image from adminstock table
                    string query = "SELECT price,count, image FROM adminstock WHERE ID = @selectedID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@selectedID", selectedID);

                    // Execute query1 to get image from adminstock table
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // ดึงราคาจากฐานข้อมูล
                            int price = Convert.ToInt32(reader["price"]);
                            // จัดรูปแบบราคาโดยเพิ่มเครื่องหมายคอมมาในจำนวนที่เกิน 1,000
                            string formattedPrice = price >= 1000 ? string.Format("{0:N0}", price) : price.ToString();
                            // แสดงราคาใน label1
                            label1.Text = "Price " + formattedPrice + " ฿";
                            label2.Text = "Count " + reader["count"].ToString();
                            // ดึงข้อมูลรูปภาพ
                            byte[] imageData = (byte[])reader["image"];
                            if (imageData != null && imageData.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    // แสดงรูปภาพใน guna2PictureBox1
                                    guna2PictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                guna2PictureBox1.Image = null;
                            }
                        }
                        else
                        {
                            // Clear label1 and guna2PictureBox1 if no data found for the selected ID
                            label1.Text = "ราคา: N/A";
                            label2.Text = "คงเหลือ: N/A";
                            guna2PictureBox1.Image = null;
                            
                        }
                    }

                    // Query to select image from adminstock2 table
                    string query2 = "SELECT image FROM adminstock2 WHERE ID = @selectedID";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@selectedID", selectedID);

                    // Execute query2 to get image from adminstock2 table
                    using (MySqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            byte[] imageData2 = (byte[])reader2["image"];
                            if (imageData2 != null && imageData2.Length > 0)
                            {
                                using (MemoryStream ms2 = new MemoryStream(imageData2))
                                {
                                    guna2PictureBox2.Image = Image.FromStream(ms2);
                                }
                            }
                            else
                            {
                                guna2PictureBox2.Image = null;
                            }
                        }
                        else
                        {
                            // Clear the image in guna2PictureBox2 if no image found in adminstock2 table
                            guna2PictureBox2.Image = null;
                        }
                    }
                }

            }
            else
            {
                // Clear the image in guna2PictureBox2 if no item is selected in guna2ComboBox2
                guna2PictureBox2.Image = null;
            }


            


        }

        

        
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            
        }



        // จำนวนสต็อก
        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        // ปุ่มไปหน้า bill one
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // สร้าง instance ของ Form16 โดยส่ง DataTable ที่ต้องการไปยัง constructor
            Form16 form16 = new Form16();

            // ซ่อน Form8
            this.Hide();

            // แสดง Form16
            form16.Show();
        }
    }
}
