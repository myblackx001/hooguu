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


namespace AskPriceWin
{
    public partial class login : Form
    {
        string strcon = "server = 101.132.77.197; " +
                "user id = wg; password = Miss123; " +
                "database=shukong;port=3306;Charset=utf8";
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(strcon))
            {
                conn.Open();
                string sql = "select account from user where power = 2";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader myread = comm.ExecuteReader();
                    while (myread.Read())
                    {
                        string account = myread["account"].ToString();
                        comboBox1.Items.Add(account);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = comboBox1.Text;
            string passwd = textBox1.Text;
            using (MySqlConnection conn = new MySqlConnection(strcon)) 
            {
                conn.Open();
                string sql = "select * from user where account = '"+
                                    username+"' and password = '"+passwd+"'";
                using (MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    MySqlDataReader myread = comm.ExecuteReader();
                    if (myread.Read())
                    {
                        string depart = myread["depart"].ToString();
                        string classify = myread["classify"].ToString();
                        string company = myread["company"].ToString();
                        mainWindow mw = new mainWindow();
                        mw.username = username;
                        mw.depart = depart;
                        mw.classify = classify;
                        mw.company = company;
                        mw.Show();
                        this.Hide();
                        
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！");
                    }
                }

            }
        }
    }
}
