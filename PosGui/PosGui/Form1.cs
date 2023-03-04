using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.DataAccess.Types;
using System.Drawing.Printing;
using System.IO;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form

    {
        public Form1()
        {
            InitializeComponent();
        }
        private void del ()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                cmd.CommandText = "delete from sold where customer_id='0'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception m) {  }
            conn.Close();
            del1();
        }
        private void del1()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                cmd.CommandText = "delete from rec_pro where customer_id='0'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception m) { }
            conn.Close();
        }
        private void d ()
    {
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 130;
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 350;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 130;
    }

        private void t()
        {
            float t1, t2;
            if (textBox5.Text=="")
            {
                textBox5.Text = "0";
            }
            if (textBox7.Text == "")
            {
                textBox7.Text = "0";
            }
            t1 = float.Parse(textBox5.Text);
            t2 = float.Parse(textBox7.Text);
            textBox8.Text = (t1 - t2).ToString("0.00");
        }
        

        OracleConnection conn = new OracleConnection("DATA SOURCE=localhost:1521/orcl;PASSWORD=education;USER ID=POS1");
      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string p;
            if (e.RowIndex >= 0)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    p = row.Cells["CODE"].Value.ToString();                
                    cmd.CommandText = "select to_char(buy_price),code from PRODUCT where code='" + p + "'";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    textBox3.Text = dr.GetString(0);
                    textBox1.Text = dr.GetString(1);
                  
                }
                catch { }
                conn.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {

                cmd.CommandText = "update sold set SELL_PRICE='"+textBox4.Text+"' where code='"+textBox1.Text+"'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                OracleDataAdapter sda1 = new OracleDataAdapter("select s.Code,product_name ,s.sell_price  from sold s,PRODUCT p where s.code=p.code and s.CUSTOMER_ID=0", conn);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                dataGridView1.DataSource = dt1;


                cmd.CommandText = "select to_char(sum(sell_price)) from sold where customer_id='0'";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                textBox5.Text = dr.GetString(0);

                t();
             
                textBox3.Clear();
                textBox4.Clear();
                textBox1.Clear();
            }
            catch (Exception m) {  }
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            del();
            
            this.dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            textBox1.Clear();
            textBox6.Clear();
            textBox9.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
          
            textBox7.Clear();
         
            textBox8.Clear();
       
          
            textBox10.Clear();
            this.ActiveControl = textBox1;
           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            del();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                OracleDataAdapter sda1 = new OracleDataAdapter("select s.Code,product_name ,s.sell_price  from sold s,PRODUCT p where s.code=p.code and s.CUSTOMER_ID=0", conn);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                dataGridView1.DataSource = dt1;
            }
            catch (Exception m) { }
            conn.Close();
            textBox1.Clear();
            textBox6.Clear();
            textBox9.Clear();
            textBox9.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            textBox7.Clear();
         
            textBox10.Clear();
            textBox8.Clear();
          
          
          
            textBox10.Clear();
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float totk=-1;

            try
            {
                totk = float.Parse(textBox8.Text);
            }
            catch { }
            if (totk >= 0)
            {

                button5.PerformClick();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                try
                {
                   
                    cmd.CommandText = "begin conf('"+textBox12.Text+"'); end;";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    textBox1.Clear();
                    textBox6.Clear();
                    textBox9.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();

                    textBox7.Clear();

                    textBox8.Clear();

                    OracleDataAdapter sda1 = new OracleDataAdapter("select s.Code,product_name ,s.sell_price  from sold s,PRODUCT p where s.code=p.code and s.CUSTOMER_ID=0", conn);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    dataGridView1.DataSource = dt1;


                    textBox10.Clear();
                 


                }
                catch (Exception m) { MessageBox.Show(m.Message); }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Please Sold More Product"); textBox8.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                cmd.CommandText = "insert into rec_pro values ('" + textBox6.Text + "','0',(select SELL_PRICE from sold where code='" + textBox6.Text + "'))";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();



                cmd.CommandText = "select to_char(sum(sell_price)) from rec_pro where customer_id='0'";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();

                textBox7.Text = dr.GetString(0);

                textBox6.Clear();
                t();
               


            }
            catch (Exception m) {  }
            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                cmd.CommandText = "insert into sold values ('" + textBox1.Text + "','0',(select SELL_PRICE from PRODUCT where code='" + textBox1.Text + "'),'" + textBox2.Text + "')";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                OracleDataAdapter sda1 = new OracleDataAdapter("select s.Code,product_name ,s.sell_price  from sold s,PRODUCT p where s.code=p.code and s.CUSTOMER_ID=0", conn);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                d();

                cmd.CommandText = "select to_char(sum(sell_price)) from sold where customer_id='0'";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();

                textBox5.Text = dr.GetString(0);
            
                textBox3.Clear();
                textBox4.Clear();
                textBox1.Clear();
                t();


            }
            catch (Exception m) { }
            conn.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float gm, rm;
                if (textBox8.Text == "")
                {
                    textBox8.Text = "0";
                }
                if (textBox9.Text == "")
                {
                    textBox9.Text = "0";
                }
                gm = float.Parse(textBox9.Text);
                rm = float.Parse(textBox8.Text);
                textBox10.Text = (gm - rm).ToString("0.00");
            }
            catch { }

        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            textBox9.SelectAll();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
               

                cmd.CommandText = "select rp() from dual";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();

                textBox11.Text = dr.GetString(0);
                textBox11.Text = textBox11.Text.Replace("[]", "\r\n");

                PrintDocument pd = new PrintDocument();
                PaperSize ps = new PaperSize("",314,130+((textBox11.Lines.Length)*20)+100);

                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                pd.PrintController = new  StandardPrintController();
                pd.DefaultPageSettings.Margins.Left = 0;
                pd.DefaultPageSettings.Margins.Right = 0;
                pd.DefaultPageSettings.Margins.Top = 0;
                pd.DefaultPageSettings.Margins.Bottom = 0;
            

                pd.DefaultPageSettings.PaperSize = ps;
                pd.Print();
             

            }
            catch (Exception m) { }
            conn.Close();

        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            int SPACE = 145;
           
            Graphics g = e.Graphics;



       


        
            Font fBody = new Font("Consolas", 20, FontStyle.Bold);

            Font fBody1 = new Font("Consolas", 9, FontStyle.Regular);
            Font fBody2 = new Font("Consolas", 15, FontStyle.Bold);
            Font fBody3 = new Font("Consolas", 10, FontStyle.Regular);
            Font fBody4 = new Font("Consolas", 12, FontStyle.Bold | FontStyle.Underline);
            Font rs = new Font("Consolas", 25, FontStyle.Bold);
            Font fTType = new Font("", 150, FontStyle.Bold);
            SolidBrush sb = new SolidBrush(Color.Black);
          
           g.DrawString("      CORAL", fBody, sb, 12, 10);
            g.DrawString("     Fashion House", fBody2, sb, 10, 33);
            g.DrawString("\r\n69/1,Shop-East II, Sandhani Plaza,\r\n  Shahid Rafique Road, Manikganj\r\n      01682438869;01953725262", fBody3, sb, 10, 40);
            g.DrawString("Money Receipt", fBody4, sb, 85, 100);

            g.DrawString(textBox11.Text, fBody1, sb,0, 130);
            g.DrawImage(pictureBox1.Image, 90, ((textBox11.Lines.Length)*16)+110);
           





        }
    }
}
