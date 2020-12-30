using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;


namespace ProjeVT
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection(@"server=localhost;port=5432;Database=VTproje;user ID=postgres;password=mert1997;");
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter("select * from public.kullanicilar where kullaniciadi='"+textBox1.Text+"' and sifre= '"+textBox2.Text+"'",conn);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (textBox1.Text=="" || textBox2.Text=="" )
            {
                MessageBox.Show("Kullanici Adi ve sifre giriniz!");
            }
            else if (dt.Rows[0][0].ToString()=="1" )
            {
                this.Hide();
                Form3 frm = new Form3();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("hata");
            }
        }
    }
}
