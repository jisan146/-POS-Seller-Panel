using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.DataAccess.Types;

namespace WindowsFormsApplication6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.ActiveControl = textBox1;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear(); textBox2.PasswordChar = '*';
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
                textBox2.Clear(); textBox2.PasswordChar = '*';
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
           

        }
        OracleConnection conn = new OracleConnection("DATA SOURCE=localhost:1521/orcl;PASSWORD=education;USER ID=POS1");
      

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick(); e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {




                cmd.CommandText = "select 'a' from users WHERE user_id='" + textBox1.Text + "' AND password =DBMS_OBFUSCATION_TOOLKIT.md5 (input => UTL_RAW.cast_to_raw('" + textBox2.Text + "')) AND active='1'";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                s = dr.GetString(0);
                if (s=="a")
                {
                    Form1 f = new Form1();
                    this.Hide();
                    f.textBox12.Text = textBox1.Text;
                    f.Show();
                }




            }
            catch (Exception m) { MessageBox.Show("Login ID or Password Invalid"); }
            conn.Close();

        }
    }
}
