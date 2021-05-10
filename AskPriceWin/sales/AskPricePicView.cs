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
using MySql.Data.MySqlClient;

namespace AskPriceWin.sales
{
    public partial class AskPricePicView : Form
    {
        public string ID = string.Empty;
        public AskPricePicView()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void AskPricePicView_Load(object sender, EventArgs e)
        {
            common cmn = new common();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "select ID,pic from askpricepic where ID = "+ID;
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader sqlDr = cmd.ExecuteReader();
                    if (sqlDr.Read())
                    {
                        byte[] images = (byte[])sqlDr["pic"];
                        MemoryStream ms = new MemoryStream(images);
                        pictureBox1.Image = new Bitmap(ms);

                    }
                }
            }
        }
    }
}
