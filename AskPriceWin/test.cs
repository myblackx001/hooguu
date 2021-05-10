using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;

namespace AskPriceWin
{
    public partial class test : Form
    {

        public test()
        {
            InitializeComponent();
        }

        private void CrystalReport41_InitReport(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void test_Load(object sender, EventArgs e)
        {
            common cmn = new common();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter("select * from v_sales where salesID = 'XS-2021-02-22-Pzkz'", conn);
                DataSet ds = new DataSet("ds");
                sda.Fill(ds,"test");
                CrystalReport2 rd = new CrystalReport2();

                rd.SetDataSource(ds.Tables["test"]);
                this.crystalReportViewer1.ReportSource = rd;
            }
        }
    }
}
