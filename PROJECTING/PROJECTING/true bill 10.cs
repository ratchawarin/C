using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using QRCoder;
using Saladpuk.PromptPay.Contracts;
using Saladpuk.PromptPay.Facades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace PROJECTING
{

    

    public partial class Form18 : Form
    {
        //pdf background
        public class PDFBackgroundHelper : PdfPageEventHelper
        {
            private iTextSharp.text.Image _backgroundImage;

            public PDFBackgroundHelper(iTextSharp.text.Image backgroundImage)
            {
                _backgroundImage = backgroundImage;
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte content = writer.DirectContentUnder;
                _backgroundImage.SetAbsolutePosition(0, 0);
                content.AddImage(_backgroundImage);
            }
        }

        private string connectionString = "server=127.0.0.1;user=root;password=;database=information;";
        private string connectionString2 = "server=127.0.0.1;user=root;password=;database=stock;";
        public void SetDataToDataGridView2(DataTable data)
        {
            guna2DataGridView1.DataSource = data;
            CalculateTotal();
            CalculateTotal2();
        }


        public Form18()
        {
            InitializeComponent();
            
        }

        private void UpdateDataToDataGridView()
        {
            // สร้าง DataTable เพื่อเก็บข้อมูลใหม่จากฐานข้อมูล
            DataTable dt = new DataTable();

            // ตั้งค่าคำสั่ง SQL เพื่อเลือกข้อมูลจากฐานข้อมูล
            string sql = "SELECT * FROM oderuser";

            // ใช้ SqlDataAdapter เพื่อดึงข้อมูลจากฐานข้อมูล
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connectionString2))
            {
                // เติมข้อมูลใน DataTable
                adapter.Fill(dt);
            }

            // ตั้งค่า DataSource ของ DataGridView เป็น DataTable ที่มีข้อมูลใหม่
            guna2DataGridView1.DataSource = dt;

            // กำหนดคอลัมน์ที่ไม่ต้องการให้แสดง
            guna2DataGridView1.Columns["id"].Visible = false;

            // ล้างข้อมูลใน TextBoxes หากไม่มีข้อมูลใน DataGridView
            if (guna2DataGridView1.Rows.Count == 0)
            {
                guna2TextBox1.Text = "";
                guna2TextBox3.Text = "0.00";
                guna2TextBox4.Text = "0.00";
            }
        }
        private void CalculateTotal2()
        {

            
        }

        private void CalculateTotal()
        {
            // รีเซ็ตค่า total เพื่อคำนวณใหม่ทุกครั้ง
            decimal total = 0;

            // วนลูปผ่านแต่ละแถวใน DataGridView1 เพื่อรวมค่า total
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                // ตรวจสอบว่าคอลัมน์ "total" มีค่าที่สามารถแปลงเป็น decimal ได้หรือไม่
                if (decimal.TryParse(row.Cells["total"].Value.ToString(), out decimal rowTotal))
                {
                    // เพิ่มค่า total ของแถวนี้เข้าไปในค่า total ทั้งหมด
                    total += rowTotal;
                }
            }

            // แสดงผลรวมที่คำนวณได้ใน TextBox1 ของ Form18
            guna2TextBox1.Text = total.ToString("#,##0.00");

            // คำนวณยอดรวมหลังลดราคา
            decimal totalWithDiscount = total;
            decimal discount = 0;

            // หากมีการลดราคา 5% เมื่อ checkbox ถูกติ๊ก
            if (checkBox1.Checked)
            {
                discount = total * 0.05m;
                totalWithDiscount -= discount;
            }

            // แสดงยอด discount ใน label5
            label5.Text = discount.ToString("#,##0.00");

            // แสดงผลรวมรวมส่วนลดใน TextBox4
            guna2TextBox4.Text = totalWithDiscount.ToString("#,##0.00");

        }
        private void Form18_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label2.Text = DateTime.Now.ToString("HH:mm:ss");

            // Load data into DataGridView
            LoadDataToDataGridView();

            // Calculate total
            CalculateTotal();

            // Generate QR code
            GenerateQRCode();

            // Hook into TextChanged event
            guna2TextBox4.TextChanged += guna2TextBox4_TextChanged;

        }

        private void YourForm_Load(object sender, EventArgs e)
        {
            // เรียกเมท็อด LoadDataToDataGridView() เพื่อโหลดข้อมูลเมื่อ Form โหลด
            LoadDataToDataGridView();
        }

        private void LoadDataToDataGridView()
        {
            try
            {
                // เชื่อมต่อกับฐานข้อมูล
                using (MySqlConnection connection = new MySqlConnection(connectionString2))
                {
                    connection.Open();

                    // เตรียมคำสั่ง SQL เพื่อดึงข้อมูลจากฐานข้อมูล
                    string query = "SELECT idname,name, price, count, total FROM oderuser";

                    // สร้างคำสั่ง SQL
                    MySqlCommand command = new MySqlCommand(query, connection);

                    // สร้าง DataAdapter และ DataSet
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                    System.Data.DataTable dataTable = new System.Data.DataTable();

                    // เตรียม DataAdapter เพื่อเก็บข้อมูลใน DataSet
                    dataAdapter.Fill(dataTable);

                    // กำหนดให้ DataGridView ใช้ DataTable เป็นแหล่งข้อมูล
                    guna2DataGridView1.DataSource = dataTable;

                    // ปิดการเชื่อมต่อ
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateQRCode()
        {
            // รับค่า totalWithVat จาก TextBox4 ที่แสดงผลรวมทั้งหมดพร้อม VAT
            if (decimal.TryParse(guna2TextBox4.Text, out decimal totalWithVat))
            {
                // แปลงค่า totalWithVat เป็น double เพื่อนำไปใช้ในการสร้างรหัส QR
                double qr_price = Convert.ToDouble(totalWithVat);

                // สร้างรหัส QR โดยใช้ PromptPayFacade
                IPromptPayBuilder builder = PPay.DynamicQR;
                string qr = PPay.DynamicQR.MobileNumber("0643061478").Amount(qr_price).CreateCreditTransferQrCode();

                // สร้าง QR Code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);

                // แสดงรูปภาพใน PictureBox 
                guna2PictureBox1.Image = qrCodeImage;
            }
            else
            {
                MessageBox.Show("Invalid total amount for QR code generation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString2))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        string idname = row.Cells["idname"].Value.ToString();
                        string name = row.Cells["name"].Value.ToString();
                        decimal price = Convert.ToDecimal(row.Cells["price"].Value);
                        int count = Convert.ToInt32(row.Cells["count"].Value);
                        decimal total = Convert.ToDecimal(row.Cells["total"].Value);
                        string date = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
                        string time = label2.Text;
                        string pay;
                        string iduser = guna2TextBox6.Text;
                        string nameuser = label3.Text;

                        // Check that only one of the checkboxes is selected
                        if (guna2CheckBox1.Checked)
                        {
                            pay = guna2CheckBox1.Text; // Set 'pay' to the name of Checkbox 1 if it is checked
                        }
                        else if (guna2CheckBox2.Checked)
                        {
                            pay = guna2CheckBox2.Text; // Set 'pay' to the name of Checkbox 2 if it is checked
                        }
                        else
                        {
                            MessageBox.Show("Please select one checkbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Cancel the operation if no checkbox is selected
                        }

                        byte[] imageBytes = null;
                        // Check if an image was selected and stored in the PictureBox's Tag property
                        if (guna2PictureBox2.Tag != null)
                        {
                            // Read the selected file into a byte array
                            imageBytes = File.ReadAllBytes(guna2PictureBox2.Tag.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Please select an image file using the upload button.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Insert data into history1 table
                        string query = "INSERT INTO history1 (iduser, idname, nameuser, name, price, count, total, pay, date, time, image) " +
                                       "VALUES (@iduser, @idname, @nameuser, @name, @price, @count, @total, @pay, @date, @time, @image)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@iduser", iduser);
                        command.Parameters.AddWithValue("@idname", idname);
                        command.Parameters.AddWithValue("@nameuser", nameuser);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@count", count);
                        command.Parameters.AddWithValue("@total", total);
                        command.Parameters.AddWithValue("@pay", pay);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@time", time);
                        command.Parameters.AddWithValue("@image", imageBytes);
                        command.ExecuteNonQuery();
                    }

                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show("ยินยัน\nคุณต้องการดำเนินการต่อหรือไม่?", "Success", buttons, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // สร้าง SaveFileDialog เพื่อให้ผู้ใช้เลือกตำแหน่งที่จะบันทึกไฟล์ PDF
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                            saveFileDialog.Title = "Save PDF";

                            // แสดง SaveFileDialog และตรวจสอบว่าผู้ใช้เลือกตำแหน่งที่จะบันทึกไฟล์หรือไม่
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // เมื่อผู้ใช้เลือกตำแหน่งที่จะบันทึกไฟล์ ให้นำชื่อไฟล์ที่เลือกมาใช้ในการสร้าง PDF
                                string fileName = saveFileDialog.FileName;

                                // เรียกใช้เมธอด ExportDataGridToPDF เพื่อสร้างไฟล์ PDF ด้วยชื่อไฟล์ที่เลือก
                                ExportDataGridToPDF(fileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // โค้ดที่คุณต้องการให้ทำเมื่อผู้ใช้คลิก "ไม่" ที่ message box
                    }

                    // Clear the image from the picture box
                    guna2PictureBox2.Image = null;
                    guna2PictureBox2.Tag = null;

                    // Delete all data from the oderuser table
                    string deleteQuery = "DELETE FROM oderuser";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.ExecuteNonQuery();
                    UpdateDataToDataGridView();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data into history1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
                        Form16 form16 = new Form16();

                        // ซ่อน 
                        this.Hide();

                        // แสดง
                        form16.Show();
                  
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ExportDataGridToPDF(string fileName)
        {
            try
            {
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                string backgroundImagePath = @"C:\Users\RCWR\Downloads\re (3).png"; // Path to your background image

                // Load the background image
                iTextSharp.text.Image backgroundImage = iTextSharp.text.Image.GetInstance(backgroundImagePath);
                backgroundImage.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);

                // Set the custom event handler for the background image
                PDFBackgroundHelper backgroundHelper = new PDFBackgroundHelper(backgroundImage);
                writer.PageEvent = backgroundHelper;

                document.Open();

                // Add Label 3 and Date above the table
                Paragraph label3AndDate = new Paragraph();
                // Add some spacing before the title
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));

                label3AndDate.Add("User: " + label3.Text + "\n");
                label3AndDate.Add("Date: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                document.Add(label3AndDate);


                // Add some spacing before the title
                
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));

                // Create a table from the DataGridView data
                PdfPTable table = new PdfPTable(guna2DataGridView1.Columns.Count);
                for (int i = 0; i < guna2DataGridView1.Columns.Count; i++)
                {
                    table.AddCell(new Phrase(guna2DataGridView1.Columns[i].HeaderText));

                }

                for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < guna2DataGridView1.Columns.Count; j++)
                    {
                        if (guna2DataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            string cellValue = guna2DataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (cellValue.Length > 0 && char.IsLetter(cellValue[0])) // เช็คว่าตัวแรกเป็นตัวอักษรหรือไม่
                            {
                                table.AddCell(new Phrase(cellValue)); // ไม่มีการเพิ่มเครื่องหมายคอมมา
                            }
                            else
                            {
                                if (int.TryParse(cellValue, out int numericValue))
                                {
                                    table.AddCell(new Phrase(numericValue.ToString("N0")));
                                }
                                else
                                {
                                    table.AddCell(new Phrase(cellValue));
                                }
                            }
                        }
                        else
                        {
                            table.AddCell(new Phrase(""));
                        }
                    }
                }
                document.Add(table);

                // Add TextBox 1, 2, และ 3 ลงใน PDF
                Paragraph textBoxValues = new Paragraph();
                document.Add(new Paragraph(" "));


                textBoxValues.Add("\n\nPrice: " + guna2TextBox1.Text + " Bath ");
                textBoxValues.Add("\n\nVAT 7%: " + guna2TextBox3.Text + " Bath ");
                textBoxValues.Add("\n\nDiscount 5%: " + label5.Text + " Bath ");
                textBoxValues.Add("\n\nTOTAL: " + guna2TextBox4.Text + " Bath ");


                textBoxValues.Add("\n\n"); // Add some line breaks for spacing
                if (guna2CheckBox1.Checked)
                {
                    textBoxValues.Add("Pay: " + guna2CheckBox1.Text);
                }
                else if (guna2CheckBox2.Checked)
                {
                    textBoxValues.Add("Pay: " + guna2CheckBox2.Text);
                }
                document.Add(textBoxValues);

                document.Close();

                MessageBox.Show("Data exported to PDF successfully!", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data to PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
            
        }

        private void CalculateVAT()
        {
            if (decimal.TryParse(guna2TextBox1.Text, out decimal price))
            {
                decimal vat = price * 0.07m; // คำนวณ VAT 7%

                // แสดงผลลัพธ์ใน TextBox3
                guna2TextBox3.Text = vat.ToString("#,##0.00"); // หรือ guna2TextBox3.Text = vat.ToString("0.00");
            }
            else
            {
                guna2TextBox3.Text = ""; // ถ้ามีข้อผิดพลาดในการแปลงค่า ให้ล้างข้อมูลใน TextBox3
            }
        }





        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("HH:mm:ss");
            timer1.Start();
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2CheckBox2.Checked = false;
            }
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox2.Checked)
            {
                guna2CheckBox1.Checked = false;
            }
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            CalculateVAT();
            GenerateQRCode();
        }

        private void guna2DateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            
        }

        

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            string userIdToSearch = guna2TextBox6.Text;

            // สร้างการเชื่อมต่อกับฐานข้อมูล
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // เตรียมคำสั่ง SQL
                    string query = "SELECT name FROM register WHERE userid = @userId";

                    // สร้างคำสั่ง SQL
                    MySqlCommand command = new MySqlCommand(query, connection);

                    // เพิ่มพารามิเตอร์
                    command.Parameters.AddWithValue("@userId", userIdToSearch);

                    // ดึงชื่อจากฐานข้อมูล
                    object result = command.ExecuteScalar();

                    if (result != null) // ถ้าพบชื่อ
                    {
                        string name = result.ToString();
                        label3.Text = " " + name;
                        checkBox1.Checked = true; // Set the checkbox to checked
                    }
                    else
                    {
                        label3.Text = "unknown";
                        checkBox1.Checked = false; // Ensure the checkbox is not checked
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter for file types, this example is for images
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                guna2PictureBox2.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);

                // Store the file path for later use
                guna2PictureBox2.Tag = openFileDialog.FileName;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // เรียกเมธอด CalculateTotal() เมื่อมีการเปลี่ยนแปลงใน checkBox1
            CalculateTotal();   
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // สร้าง instance 
            Form16 form16 = new Form16();

            // ซ่อน 
            this.Hide();

            // แสดง
            form16.Show();
        }
    }
}
