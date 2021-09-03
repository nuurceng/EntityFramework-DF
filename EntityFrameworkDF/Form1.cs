using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSınavEntities db = new DbSınavEntities();

        private void Form1_Load(object sender, EventArgs e)
        {
            var adsoyadlist = db.ogrenci.Select(x => new
            {
                x.id,
                adsoyad=x.ad+ " "+x.soyad
            }
                ).ToList();
            cbogrid.DataSource = adsoyadlist;
            cbogrid.ValueMember = "id";
            cbogrid.DisplayMember = "adsoyad";
            

            var d = db.dersler.ToList();
            cbdersid.DataSource = d;
            cbdersid.ValueMember = "dersid";
            cbdersid.DisplayMember = "dersad";
        
        }

        private void ogrencilistele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.ogrenci.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void derslist_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.dersler.ToList();
            dataGridView1.Columns[2].Visible = false;
        }

        private void notlist_Click(object sender, EventArgs e)
        {
            // dataGridView1.DataSource = db.notlar.ToList();
            var sorgu = from item in db.notlar select new { item.notid, item.ogrenci.ad, item.ogrenci.soyad, item.ogrenci.id, item.dersler.dersad, item.sinav1, item.sinav2, item.sinav3, item.ortalama, item.durum };
            dataGridView1.DataSource = sorgu.ToList();
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
        }
        private void kulüplistele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.kulupler.ToList();
        }

        private void kaydet_Click(object sender, EventArgs e)
        {    
            if(tbad.Text!="" & tbsoyad.Text!="")
            {
                ogrenci k = new ogrenci();
                k.ad = tbad.Text;
                k.soyad = tbsoyad.Text;
                db.ogrenci.Add(k);
                db.SaveChanges();
                MessageBox.Show("Öğrenci listeye eklenmiştir");
                dataGridView1.DataSource = db.ogrenci.ToList();
            }
            
            if(tbdersad.Text!="")
            {
                dersler d = new dersler();
                d.dersad = tbdersad.Text;
                db.dersler.Add(d);
                db.SaveChanges();
                MessageBox.Show("Ders listeye eklenmiştir");
                dataGridView1.DataSource = db.dersler.ToList();

            }
            if(tbkulupad.Text!="")
            {
                kulupler p = new kulupler();
                p.kulupad = tbkulupad.Text;
                db.kulupler.Add(p);
                db.SaveChanges();
                MessageBox.Show("Kulüpler listeye eklenmiştir");
                dataGridView1.DataSource = db.kulupler.ToList();
            }
            if(tbort.Text!="")
            {
                notlar n = new notlar();
                n.ogrid = (int)cbogrid.SelectedValue;
                n.ders = (int)cbdersid.SelectedValue;
                n.sinav1 =Convert.ToInt16( tbsinav1.Text);
                n.sinav2 = Convert.ToInt16(tbsinav2.Text);
                n.sinav3 = Convert.ToInt16(tbsinav3.Text);
                n.ortalama = Convert.ToDecimal(tbort.Text);
                n.durum =Convert.ToBoolean( tbdurum.Text);
                db.notlar.Add(n);
                db.SaveChanges();
                MessageBox.Show("Notlar listeye eklendi...");
                dataGridView1.DataSource = db.Notlistesi();
            }
          
        } 

        private void sil_Click(object sender, EventArgs e)
        {
            if(tbogrenciid.Text!="")
            {
                int id = Convert.ToInt32(tbogrenciid.Text);
                var x = db.ogrenci.Find(id);//id tut
                db.ogrenci.Remove(x);
                db.SaveChanges();
                MessageBox.Show("Öğrenci silindi");
                dataGridView1.DataSource = db.ogrenci.ToList();
            }

            if(tbdersid.Text!="")
            {
                int id1 = Convert.ToInt32(tbdersid.Text);
                var x1 = db.dersler.Find(id1);//id tut
                db.dersler.Remove(x1);
                db.SaveChanges();
                MessageBox.Show("Ders silindi");
                dataGridView1.DataSource = db.dersler.ToList();
            }
            

            if(tbkulupid.Text!="")
            {
                int id2 = Convert.ToInt32(tbkulupid.Text);
                var x2 = db.kulupler.Find(id2);//id tut
                db.kulupler.Remove(x2);
                db.SaveChanges();
                MessageBox.Show("Kulüp silindi");
                dataGridView1.DataSource = db.kulupler.ToList();
            }
            if (tbnotid.Text != "")
            {
                int id = Convert.ToInt32(tbnotid.Text);
                var n1 = db.notlar.Find(id);
                db.notlar.Remove(n1);
                db.SaveChanges();
                MessageBox.Show("Not bilgileri silindi...");
                dataGridView1.DataSource = db.Notlistesi();
            }

        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            if (tbogrenciid.Text!="")
            {
                int id = Convert.ToInt32(tbogrenciid.Text);
                var x = db.ogrenci.Find(id);
                x.ad = tbad.Text;
                x.soyad = tbsoyad.Text;
                //x.resim = tbresim.Text;
                db.SaveChanges();
                MessageBox.Show("Öğrenci bilgileri güncellendi");
                dataGridView1.DataSource = db.ogrenci.ToList();
            }
            if (tbdersid.Text != "")
            {
                int id = Convert.ToInt32(tbdersid.Text);
                var x = db.dersler.Find(id);
                x.dersad = tbdersad.Text;
                db.SaveChanges();
                MessageBox.Show("Ders bilgileri güncellendi");
                dataGridView1.DataSource = db.dersler.ToList();
            }
            if (tbkulupid.Text != "")
            {
                int id = Convert.ToInt32(tbkulupid.Text);
                var x = db.kulupler.Find(id);
                x.kulupad = tbkulupad.Text;
                db.SaveChanges();
                MessageBox.Show("Kulüp bilgileri güncellendi");
                dataGridView1.DataSource = db.kulupler.ToList();
            }
            if(tbnotid.Text!="")
            {
                int id = Convert.ToInt32(tbnotid.Text);
                var n = db.notlar.Find(id);
                n.ogrid = (int)cbogrid.SelectedValue;
                n.ders = (int)cbdersid.SelectedValue;
                n.sinav1 = Convert.ToInt16(tbsinav1.Text);
                n.sinav2 = Convert.ToInt16(tbsinav2.Text);
                n.sinav3 = Convert.ToInt16(tbsinav3.Text);
                n.ortalama = Convert.ToDecimal(tbort.Text);
                n.durum = Convert.ToBoolean(tbdurum.Text);
                db.SaveChanges();
                MessageBox.Show("Not bilgileri güncellendi...");
                dataGridView1.DataSource = db.Notlistesi();
            }
        }

        private void prosedür_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Notlistesi();
        }

        private void bul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.ogrenci.Where(x => x.ad == tbad.Text | x.soyad==tbsoyad.Text).ToList();

        }

        private void tbad_TextChanged(object sender, EventArgs e)
        {
            string aranan = tbad.Text;
            var degerler = from item in db.ogrenci where item.ad.Contains(aranan) select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btnlinqentity_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                List<ogrenci> liste1 = db.ogrenci.OrderBy(p => p.ad).ToList();
                dataGridView1.DataSource = liste1;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            if (radioButton2.Checked==true)
            {
                List<ogrenci> liste2 = db.ogrenci.OrderByDescending(p => p.ad).ToList();
                dataGridView1.DataSource = liste2;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            if(radioButton3.Checked==true)
            {
                List<ogrenci> liste3 = db.ogrenci.OrderBy(p => p.ad).Take(3).ToList();
                dataGridView1.DataSource = liste3;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            if(radioButton4.Checked==true)
            {
                List<ogrenci> liste4 = db.ogrenci.Where(p => p.id == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            if(radioButton5.Checked==true) 
            {
                List<ogrenci> liste5 = db.ogrenci.Where(p => p.ad.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            if (radioButton6.Checked == true)
            {
                List<ogrenci> liste6 = db.ogrenci.Where(p => p.ad.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            if(radioButton7.Checked==true)
            {
                bool deger = db.kulupler.Any();
                MessageBox.Show(deger.ToString(),"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            if(radioButton8.Checked==true)
            {
                int toplam = db.ogrenci.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if(radioButton9.Checked==true)
            {
               var toplam = db.notlar.Sum(p => p.sinav1);
               MessageBox.Show("Toplam Sınav1 Puanı:"+ toplam.ToString());
            }
            if (radioButton10.Checked == true)
            {
                var ortalama = db.notlar.Average(p => p.sinav1);
                MessageBox.Show("1.Sınavın ortalaması:" + ortalama.ToString());
            }
            if (radioButton11.Checked == true)
            {
                var enyuksek = db.notlar.Max(p => p.sinav1);
                MessageBox.Show("1.Sınavın en yüksek notu:" + enyuksek.ToString());
            }
            if (radioButton12.Checked == true)
            {
                var endusuk= db.notlar.Min(p => p.sinav1);
                MessageBox.Show("1.Sınavın en düşük notu:" + endusuk.ToString());
            }

        }

        private void btnjoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.notlar join d2 in db.ogrenci  on d1.ogrid equals d2.id join d3 in db.dersler on d1.ders equals d3.dersid  select new { Öğrenci = d2.ad + " " +d2.soyad, Ders=d3.dersad,/*Soyad=d2.soyad,*/ sınav1 = d1.sinav1, sınav2 = d1.sinav2,sınav3=d1.sinav3,ortalama=d1.ortalama };
            dataGridView1.DataSource = sorgu.ToList();
        }

        private void hesapla_Click(object sender, EventArgs e)
        {
            if(tbsinav1.Text!="" & tbsinav2.Text!="" & tbsinav3.Text!="")
            {
                int sayi1, sayi2, sayi3 ,ortalama;
                sayi1 = int.Parse(tbsinav1.Text);
                sayi2 = int.Parse(tbsinav2.Text);
                sayi3 = int.Parse(tbsinav3.Text);
                ortalama = sayi1 + sayi2 + sayi3 / 3;
                tbort.Text = ortalama.ToString();
                if(ortalama>100)
                {
                    tbdurum.Text = "True";
                }
                else
                {
                    tbdurum.Text = "False";
                }
            }   
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbnotid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cbogrid.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbdersid.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbsinav1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            tbsinav2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            tbsinav3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            tbort.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            tbdurum.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
    }
}
