using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Text.RegularExpressions;
using Guna.UI2.WinForms;
using System.IO;
using Guna.UI.WinForms;
using test611.Properties;

namespace test611
{
    public partial class Form1 : Form
    {
        string GelenVeri = Properties.Settings.Default.uname;



        private bool isDragging = false;
        private Point dragOffset;
        public Form1()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.MouseDown += guna2GradientPanel3_MouseDown;
            this.MouseMove += guna2GradientPanel3_MouseMove;
            this.MouseUp += guna2GradientPanel3_MouseUp_1;
     
            this.Resize += MainForm_Resize;
            previousSize = this.Size;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
         
                // Formu tekrar göster
                this.Size = previousSize;
                this.Show();
                isMinimized = false;
            
        }

        private void refresh()
        {
            
        }


        int count = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
           guna2GradientPanel2.Visible = false;
            guna2CircleProgressBar1.Visible = true;
            timer1.Start();
           
            //   listBox1.ScrollAlwaysVisible = true;

            guna2VScrollBar1.VerticalScroll.Value = guna2VScrollBar1.VerticalScroll.Maximum;
            guna2GradientPanel2.VerticalScroll.Value = guna2GradientPanel2.VerticalScroll.Maximum;
            bakiye();
            bas();
            gel();
            //   listBox1.ForeColor = Color.Orange;

        }

        int satırsag = 0, satırsol = 0;

        void gidenmsg(string message)
        {
            var bubıl = new linkapp.ChatItems.Incomming();
            guna2GradientPanel2.Controls.Add(bubıl);
           
            bubıl.BringToFront();
            bubıl.Dock = DockStyle.Top;
            bubıl.Message = message;
        }
        void Gelenmsg(string message)
        {
            var bubıl = new linkapp.ChatItems.Outgoing();
            guna2GradientPanel2.Controls.Add(bubıl);
            bubıl.BringToFront();
            bubıl.Dock = DockStyle.Top;

            bubıl.Message = message;

        }
        int son20 = 0;

        string[] mesajlar;
        private async void gel()
        {
            string dosyaAdi = "Ordek_sesi.wav";
            string dosyaYolu = Path.Combine(Environment.CurrentDirectory, dosyaAdi);
            int gelenmsg = 0;
            SoundPlayer soundPlayer = new SoundPlayer(dosyaYolu);
            int satirsay = 0;
            string duck = "";
            using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
            {
                await connection.OpenAsync();

                while (true)
                {
                    if (count == satirsay)
                    { gelenmsg = 1; }
                    using (SqlCommand command = new SqlCommand("SELECT  message,mdate,uname FROM messages ORDER BY mdate " + duck + " ", connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                duck = "desc";

                                if (reader.GetString(2) == GelenVeri)
                                {

                                    while (count > satirsay)
                                    {
                                        if (msgGonderim == false)
                                        {
                                            gidenmsg(reader.GetString(2) + ":" + reader.GetString(0));
                                        }

                                      
                                        guna2GradientPanel2.VerticalScroll.Value = guna2GradientPanel2.VerticalScroll.Maximum;
                                        // guna2VScrollBar1.VerticalScroll.Value = guna2VScrollBar1.VerticalScroll.Maximum;
                                        satirsay++;
                                        break;

                                    }
                                }
                                else
                                {
                                    while (count > satirsay)
                                    {



                                        //   MessageBox.Show("");
                                        if (gelenmsg == 1 && guna2ToggleSwitch1.Checked == false)
                                        {
                                            soundPlayer.Play();
                                        }
                                       
                                        Gelenmsg(reader.GetString(2) + ":" + reader.GetString(0));
                                       

                                        guna2GradientPanel2.VerticalScroll.Value = guna2GradientPanel2.VerticalScroll.Maximum;
                                        //-  guna2VScrollBar1.VerticalScroll.Value = guna2VScrollBar1.VerticalScroll.Maximum;
                                        satirsay++;

                                        break;
                                    }
                                }



                            }
                        }
                    }

                    // 1 saniye bekleme
                    await Task.Delay(400);
                }
            }
        }

        private async void bakiye()
        {
            using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
            {
                await connection.OpenAsync();

                while (true)
                {
                    using (SqlCommand command = new SqlCommand("SELECT  bakiye FROM hesap where uid=(select id from users where uname=@uname) ", connection))
                    {
                        command.Parameters.AddWithValue("@uname", GelenVeri);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                label3.Text = reader.GetDouble(0).ToString();


                            }
                        }
                    }

                    // 1 saniye bekleme
                    await Task.Delay(1000);
                }
            }

        }

        /*
         DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
if(dialogResult == DialogResult.Yes)
{
    //do something
}
else if (dialogResult == DialogResult.No)
{
    //do something else
}
         */

        private async void bas()
        {
            using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
            {
                await connection.OpenAsync();

                while (true)
                {
                    using (SqlCommand command = new SqlCommand("SELECT  count(*) FROM messages ", connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                count = reader.GetInt32(0);
                             

                            }
                        }
                    }

                    // 1 saniye bekleme
                    await Task.Delay(400);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


        }
        string uname = "";
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private async void button5_Click_1(object sender, EventArgs e)
        {
        }

        private async void refreshpazar()
        {
            listView1.Items.Clear();
            using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT  id,urun_Adi,fiyat FROM pazar ", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            ListViewItem item = new ListViewItem(reader.GetInt32(0).ToString());
                            item.SubItems.Add(reader.GetString(1));
                            string fiyat = Convert.ToString(reader.GetDouble(2));
                            item.SubItems.Add(fiyat);

                            listView1.Items.Add(item);
                            //   listView1.Items.Add(reader.GetInt32(0).ToString(),reader.GetString(1),reader.GetDecimal(2).ToString());
                            //  listView1.Columns.Add(reader.GetInt32(0).ToString());

                        }
                    }
                }

                // 1 saniye bekleme
                await Task.Delay(10000);


            }
        }



        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void guna2Button6_Click(object sender, EventArgs e)
        {


            if (!Regex.IsMatch(guna2TextBox5.Text, "^[0-9]*$") || String.IsNullOrEmpty(guna2TextBox5.Text))
            {
                MessageBox.Show("Sadece ürün id'sini giri.");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Emin misin?", "Transfer", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool urunyok = false;
                    double ub = 0, pb = 0;
                    using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
                    {
                        string hata = "";
                        await connection.OpenAsync();
                        SqlCommand walletchk = new SqlCommand("select bakiye as walbak from hesap where uid=(select id from users where uname='" + GelenVeri + "');", connection);
                        SqlDataReader red = walletchk.ExecuteReader();
                        while (red.Read())
                        {
                            ub = Convert.ToDouble(red.GetDouble(0));

                            connection.Close();
                            red.Close();
                            break;

                        }
                        connection.Close();
                        await Task.Delay(500);
                        await connection.OpenAsync();
                        SqlCommand walletchk2 = new SqlCommand("select fiyat as price from pazar where id=" + guna2TextBox5.Text + ";", connection);
                        SqlDataReader red2 = walletchk2.ExecuteReader();
                        urunyok = true;

                        while (red2.Read())
                        {


                            pb = Convert.ToDouble(red2.GetDouble(0));


                            urunyok = false;

                            break;

                        }


                        connection.Close();
                        red2.Close();

                        await Task.Delay(250);


                        if (urunyok == false)
                        {
                            if (ub < pb)
                            {
                                MessageBox.Show("yetersiz bakiye");
                            }
                            else
                            {
                                //

                                connection.Open();
                                SqlCommand satinal = new SqlCommand("declare @walletkontrol int=0; declare @fiyat float =(select fiyat from pazar where id=@urunid);declare @satici int=(select satici from pazar where id=@urunid); declare @aliciid int=(select id from users where uname=@loginuser); declare @kont int =0;  if (select bakiye from hesap where uid=@aliciid)>=@fiyat begin set @walletkontrol=1; select @walletkontrol as Walletchk;  update hesap set bakiye+=@fiyat where uid =@satici; update hesap set bakiye -=@fiyat where uid=@aliciid; delete pazar where id=@urunid; end;", connection);
                                satinal.Parameters.AddWithValue("@urunid", guna2TextBox5.Text);
                                satinal.Parameters.AddWithValue("@loginuser", GelenVeri);
                                satinal.ExecuteNonQuery();


                                connection.Close();
                                MessageBox.Show("İşlem gerçekleşti");
                            }
                        }
                        else { MessageBox.Show("Böyle bir ürün yok."); }



                    }
                    refreshpazar();
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }

        private async void guna2Button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
            {
                await connection.OpenAsync();

                while (true)
                {
                    using (SqlCommand command = new SqlCommand("SELECT  id,urun_Adi,fiyat FROM pazar ", connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                ListViewItem item = new ListViewItem(reader.GetInt32(0).ToString());
                                item.SubItems.Add(reader.GetString(1));
                                string fiyat = Convert.ToString(reader.GetDouble(2));
                                item.SubItems.Add(fiyat);

                                listView1.Items.Add(item);
                                //   listView1.Items.Add(reader.GetInt32(0).ToString(),reader.GetString(1),reader.GetDecimal(2).ToString());
                                //  listView1.Columns.Add(reader.GetInt32(0).ToString());

                            }
                        }
                    }

                    // 1 saniye bekleme
                    await Task.Delay(10000);
                    break;
                }
            }
        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {



        }
        bool msgGonderim = false;
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(guna2TextBox3.Text))
            {
                using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
                {
                    msgGonderim = true;
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into messages values(@uname,@msg,getdate())", connection);
                    cmd.Parameters.AddWithValue("@uname", GelenVeri);
                    cmd.Parameters.AddWithValue("@msg", guna2TextBox3.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    connection.Close();
                    string message = guna2TextBox3.Text.ToString();
                    gidenmsg(message);
                    guna2TextBox3.Clear();

                    guna2GradientPanel2.VerticalScroll.Value = guna2GradientPanel2.VerticalScroll.Maximum;
                    son20 = 0;
                }
            }
        }

        private async void guna2TextBox3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                using (SqlConnection connection = new SqlConnection("Server=31.186.11.154;Database=sec5alerekaccom_vris;User Id = sec5alerekaccom_vrisadmin; Password=Emirgok5328*;"))
                {
                    await connection.OpenAsync();

                    if (!string.IsNullOrEmpty(GelenVeri))
                    {
                        msgGonderim = true;

                        SqlCommand cmd = new SqlCommand("insert into messages values(@uname,@msg,getdate())", connection);
                        cmd.Parameters.AddWithValue("@uname", GelenVeri);
                        cmd.Parameters.AddWithValue("@msg", guna2TextBox3.Text);
                        SqlDataReader reader = cmd.ExecuteReader();
                        string message = guna2TextBox3.Text.ToString();
                        gidenmsg(message);
                        guna2TextBox3.Clear();

                        guna2GradientPanel2.VerticalScroll.Value = guna2GradientPanel2.VerticalScroll.Maximum;
                        son20 = 0;
                    }
                    await Task.Delay(100);

                }
            }
        }



        private void guna2GradientPanel3_MouseUp_1(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void guna2GradientPanel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - dragOffset.X;
                newLocation.Y += e.Y - dragOffset.Y;
                this.Location = newLocation;
            }
        }

        private void guna2GradientPanel3_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragOffset = new Point(e.X, e.Y);
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            
        }


        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }
        private bool isMinimized = false;
        private Size previousSize;
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            AltTabHelper.SendAltTab();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2CircleProgressBar1.Value += 10; // İlerleme miktarını ayarlayabilirsiniz

            if (guna2CircleProgressBar1.Value >= guna2CircleProgressBar1.Maximum)
            {
                // İlerleme tamamlandığında Timer'ı durdur
                guna2GradientPanel2.Visible = true;
                guna2CircleProgressBar1.Visible = false;
           
                timer1.Stop();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Emin misin?", "Transfer", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}

