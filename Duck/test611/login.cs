using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace test611
{


    public class AltTabHelper
    {
        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int VK_TAB = 0x09;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void SendAltTab()
        {
            keybd_event((byte)Keys.LMenu, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event((byte)Keys.Tab, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event((byte)Keys.Tab, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            keybd_event((byte)Keys.LMenu, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }
    }

    public partial class login : Form
    {

        private bool isDragging = false;
        private Point dragOffset;
        public login()
        {
            InitializeComponent();
            this.MouseDown += guna2GradientPanel4_MouseDown;
            this.MouseMove += guna2GradientPanel4_MouseMove;
            this.MouseUp += guna2GradientPanel4_MouseUp;
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            /*  using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
              {
                  await connection.OpenAsync();

                  while (true)
                  {
                      using (SqlCommand command = new SqlCommand("SELECT   FROM messages ", connection))
                      {
                          using (SqlDataReader reader = await command.ExecuteReaderAsync())
                          {
                              while (await reader.ReadAsync())
                              {




                              }
                          }
                      }

                      // 1 saniye bekleme
                      await Task.Delay(4000);
                  }
              }*/

            //mail gönderme şifremi unuttum

            string fromEmail = "your_email@example.com";
            string toEmail = guna2TextBox1.Text;
            string subject = "Test Email";
            string body = "This is a test email.";

            // Create a new MailMessage object
            MailMessage mail = new MailMessage(fromEmail, toEmail, subject, body);

            // Set the mail message properties (optional)
            mail.IsBodyHtml = false; // Set to true if the body contains HTML
            mail.Priority = MailPriority.Normal; // Set the priority of the mail message

            // Create a new SmtpClient object
            SmtpClient smtpClient = new SmtpClient("smtp.example.com", 587);

            // Provide credentials for the SMTP server if required
            smtpClient.Credentials = new NetworkCredential("username", "password");

            // Enable SSL encryption if required
            smtpClient.EnableSsl = true;

            try
            {
                // Send the email
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientPanel3_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {


        }

        private void login_MouseUp(object sender, MouseEventArgs e)
        {


        }

        private void login_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void guna2GradientPanel4_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void guna2GradientPanel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - dragOffset.X;
                newLocation.Y += e.Y - dragOffset.Y;
                this.Location = newLocation;
            }
        }

        private void guna2GradientPanel4_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragOffset = new Point(e.X, e.Y);
        }
        Thread th;
        private void opennewform(object obj)
        {
           
                Form1 form1 = new Form1();

            form1 = new Form1();
            form1.Size = new Size(1382, 677);

            Application.Run(form1);

        }
  

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  count(*) FROM users where uname=@unam and password=@pass ", connection))
                {
                    command.Parameters.AddWithValue("@unam", guna2TextBox5.Text);
                    command.Parameters.AddWithValue("@pass", guna2TextBox6.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int count = reader.GetInt32(0);
                            if (count == 0)
                            {
                                MessageBox.Show("Hatalı bir deneme");
                                break;
                            }
                            else
                            {

                                MessageBox.Show("Sohbete katıl");
                                /*  guna2Button3.Enabled = true;
                                  guna2Button6.Enabled = true;
                                  guna2Button5.Enabled = true;
                                  guna2TextBox3.Enabled = true;
                                  guna2TextBox5.Enabled = true;
                                  guna2TextBox1.Enabled = false;
                                  guna2Button1.Enabled = false;
                                  guna2Button2.Enabled = false;
                                  guna2TextBox2.Enabled = false;
                                  guna2TextBox2.Clear();*/
                                string veri = "Merhaba Form2!";


                                Properties.Settings.Default.uname = guna2TextBox5.Text;
                                Properties.Settings.Default.Save();



                                this.Close();
                                th = new Thread(opennewform);
                                th.SetApartmentState(ApartmentState.STA);
                                th.Start();
                                reader.Close();
                                connection.Close();

                                break;
                            }


                        }

                    }
                }
            }

            // 1 saniye bekleme

        }
        /*
         
         
          
         
         */
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = true;
            guna2GradientPanel3.Visible = false;
            guna2GradientPanel1.Visible = false;
        }
        private void guna2Button5_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel3.Visible = true;
            guna2GradientPanel1.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox4.Text) && !string.IsNullOrEmpty(guna2TextBox2.Text) && !string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id =sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
                {
                    connection.Open();


                    using (SqlCommand command = new SqlCommand("SELECT  count(*) FROM users where uname=@unam ", connection))
                    {
                        command.Parameters.AddWithValue("@unam", guna2TextBox3.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int count = reader.GetInt32(0);
                                if (count == 0)
                                {

                                    reader.Close();

                                    SqlCommand cmd = new SqlCommand("insert into users (uname,password) values(@uname,@pass) ; insert into hesap values((select id from users where uname=@uname),400)", connection);
                                    cmd.Parameters.AddWithValue("@uname", guna2TextBox3.Text);
                                    cmd.Parameters.AddWithValue("@pass", guna2TextBox2.Text);
                                    SqlDataReader reader1 = cmd.ExecuteReader();
                                    connection.Close();
                                    MessageBox.Show("hoş geldin");

                                    break;
                                }
                                else
                                {
                                    guna2TextBox3.Text = "";
                                    MessageBox.Show("Bu isim daha önce alınmış.");
                                    reader.Close();
                                    connection.Close();
                                    break;
                                }



                            }

                        }
                    }

                    // 1 saniye bekleme


                }
            }
            else
                MessageBox.Show("Boş geçilemez!");
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel3.Visible = true;
            guna2GradientPanel1.Visible = false;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel3.Visible = true;
            guna2GradientPanel1.Visible = false;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel3.Visible = false;
            guna2GradientPanel1.Visible = true;
        }
    }
}
