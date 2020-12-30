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

namespace ProjeVT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection(@"server=localhost;port=5432;Database=VTproje;user ID=postgres;password=mert1997;");
        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kategori";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (TxtKategorid.Text == "" || TxtAd.Text == "")
            {
                MessageBox.Show("Hata","HATA");
            }
            else
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategori (kategoriid,kategoriadi) values (@p1,@p2)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(TxtKategorid.Text));
                komut1.Parameters.AddWithValue("@p2", TxtAd.Text);
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kategori ekleme islemi yapıldı.");
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtKategorid.Text=="")
            {
                MessageBox.Show("Hata", "HATA");
            }
            else
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("DELETE From kategori where kategoriid=@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(TxtKategorid.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kategori silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update kategori set kategoriadi=@p2 where kategoriid=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", int.Parse(TxtKategorid.Text));
            komut3.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut3.ExecuteNonQuery();
            MessageBox.Show("Ürün güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();

        }
    }
}
