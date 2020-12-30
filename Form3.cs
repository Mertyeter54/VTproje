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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection(@"server=localhost;port=5432;Database=VTproje;user ID=postgres;password=mert1997;");
        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from urun";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategori",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "kategoriadi";
            comboBox1.ValueMember = "kategoriid";
            comboBox1.DataSource = dt;
            baglanti.Close();

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urun (urunid,urunad,stok,alis,satis,gorsel,kategoriid) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7) ",baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(numericUpDown1.Value.ToString()));
            komut.Parameters.AddWithValue("@p4", double.Parse( TxtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p5", double.Parse( TxtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p6", TxtGorsel.Text);
            komut.Parameters.AddWithValue("@p7", int.Parse(comboBox1.SelectedValue.ToString())); ;
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün kaydı yapıldı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("DELETE From urun where urunid=@p1",baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);


        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update urun set urunad=@p1,stok=@p2,alis=@p3,satis=@p5 where urunid=@p4", baglanti);
            komut3.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            komut3.Parameters.AddWithValue("@p3", double.Parse(TxtAlisFiyat.Text));
            komut3.Parameters.AddWithValue("@p4", int.Parse(TxtID.Text));
            komut3.Parameters.AddWithValue("@p5",double.Parse(TxtSatisFiyat.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Ürün güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.ShowDialog();


            
        }
    }
}
