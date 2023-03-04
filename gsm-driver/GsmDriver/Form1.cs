using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.IO.Ports;
using System.Threading;
using System.Net;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.DataAccess.Types;
using System.IO;
using System.Diagnostics;
namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
     
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);

            OracleConnection conn = new OracleConnection("DATA SOURCE=localhost:1521/orcl;PASSWORD=education;USER ID=pos1");

            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select data from app_information where sl=17";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox3.Text = dr.GetString(0);
          
            conn.Close();

        
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            OracleConnection conn = new OracleConnection("DATA SOURCE=localhost:1521/orcl;PASSWORD=education;USER ID=pos1");

            conn.Open();

            try
            {
                string p = "", sms = "", l = "", r = "", aa = "";
               
              /*  OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select to_char(phone),sms,rowid,to_char(ll) from sms where ll is not null and rownum=1";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                p = dr.GetString(0);
                sms = dr.GetString(1);
               r = dr.GetString(2);
               l = dr.GetString(3);*/
                  OracleCommand cmd = conn.CreateCommand();
                  cmd.CommandText = "begin send_sms_by_mob(:pf,:pf1,:pf2,:pf3); end;";

                OracleParameter pf = new OracleParameter("p_region_name", OracleDbType.Varchar2, 100, "", ParameterDirection.Output);
                OracleParameter pf1 = new OracleParameter("p_region_name", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output);
                OracleParameter pf2 = new OracleParameter("p_region_name", OracleDbType.Varchar2, 100, "", ParameterDirection.Output);
                OracleParameter pf3 = new OracleParameter("p_region_name", OracleDbType.Varchar2, 100, "", ParameterDirection.Output);

                cmd.Parameters.Add(pf);
                cmd.Parameters.Add(pf1);
                cmd.Parameters.Add(pf2);
                cmd.Parameters.Add(pf3);
                cmd.ExecuteNonQuery();
                   p =pf.Value.ToString();
                sms = pf1.Value.ToString();
               r = pf2.Value.ToString();
               l = pf3.Value.ToString();
            //   MessageBox.Show(pf.Value.ToString());
                if (pf.Value.ToString() != "0")
                {
                 //   MessageBox.Show(pf.Value.ToString());

                    WebClient w = new WebClient();
                    aa = w.DownloadString("http://smsgateway.me/api/v3/messages/send/?email=" + textBox3.Text + "&password=123456&device=" + l + "&number=" + p + "&message=" + sms);


                    OracleCommand cmd1 = conn.CreateCommand();
                    cmd.CommandText = "begin p_send_sms('" + r + "','" + aa + "'); end;";

                    cmd.ExecuteNonQuery();
                    conn.Close(); textBox4.Text = "";
                }
            }
            catch
            { textBox4.Text = "1"; }

            conn.Close();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection("DATA SOURCE=localhost:1521/orcl;PASSWORD=education;USER ID=pos1");
            conn.Open();
            try
            {
               


                string p = "", sms = "", l = "", r = "", aa = "";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select to_char(a) ,rowid from sms_id where a=(select a from sms_t where rownum=1)";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                p = dr.GetString(0);
                r = dr.GetString(1);


                WebClient w = new WebClient();
                aa = w.DownloadString("http://smsgateway.me/api/v3/messages/view/" + p + "/?email=" + textBox3.Text + "&password=123456");
                //  timer1.Enabled = false; MessageBox.Show(aa);

                OracleCommand cmd1 = conn.CreateCommand();
                cmd.CommandText = "begin p_check_sms('" + r + "','" + aa + "','" + p + "'); end;";

                cmd.ExecuteNonQuery();
                textBox5.Text = "";
            }
            catch { textBox5.Text = "2"; }
            conn.Close();
           
        }

      

        private void progressBar1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
