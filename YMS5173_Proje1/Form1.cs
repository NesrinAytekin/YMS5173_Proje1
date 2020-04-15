using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YMS5173_Proje1.Model;

namespace YMS5173_Proje1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Categories.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Birim fiyatı 18'den büyük olan ürünlerin ID, Adını, Birim fyatını, stok durumunu listeleyiniz

            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice > 18).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock

            }).ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //EmployeeID' si  5 ten büyük olan çalışanların Adını, Soyadını ve ünvanını getiriniz
            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID > 5).Select(x => new
            {
                x.EmployeeID,
                x.FirstName,
                x.LastName,
                x.Title

            }).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Stok miktarı sıfır olan ürünlerin ıd, adı, fiyatını, stok miktarını getiriniz

            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock == 0).Select(x => new

            {
                x.ProductID,
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock
            }).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 1952 ve 1960 yılları arasında doğan çalışanların adı, soyadı, dogum yılı, ünvan bilgilerini listeleyiniz.

            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) > 1952 && SqlFunctions.DatePart("Year", x.BirthDate) < 1960).Select(x => new
            {
                İsim=x.FirstName,
                Soyisim=x.LastName,
                Yaş=SqlFunctions.DatePart("Year", x.BirthDate),
                Unvan=x.Title
            }).ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Ünvanı (TitleOfCourtesy) Mr yada Dr olanları listeleyiniz
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy.Contains("Mr.") || x.TitleOfCourtesy.Contains("Dr.")).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Ürünlerin birim fiyatları 18, 19 veya 25 olanları listeleyin.

            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice == 18 || x.UnitPrice == 19 || x.UnitPrice == 25).ToList();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Soru: Çalışanlar tablosundan ID'si 2 ile 8 arasında olanları isimlerine göre artan sırada sıralayınız

            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID >= 2 && x.EmployeeID <= 8).OrderBy(x=> x.FirstName).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Birim en yüksek olan 5 ürünü getirin.

            dataGridView1.DataSource = db.Products.OrderByDescending(x => x.UnitPrice).Take(5).ToList();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Adı Michael olanları listeleyiniz.

            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.Contains("Michael")).ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Adının baş harfi "A" ile başlayanları listeleyiniz.
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.StartsWith("A")).ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Adının son harfi "N" olnaları listeleyiniz
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.EndsWith("N")).ToList();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //İsmi içerisinde "E" harfi geçenleri listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.Contains("E")).ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //İlk harfi "A" yada "L" ile başlayanları listeleyiniz
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.StartsWith("A") || x.FirstName.StartsWith("L")).ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Stok Miktarını hesaplayınız.

            decimal? Stok = db.Products.Sum(x => x.UnitsInStock);
            MessageBox.Show("Toplam Stok Miktarı: " + Stok);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //Çalışanların yaşlarının toplamınu bulunuz.

            int? YaslarınToplamı = db.Employees.Sum(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now));
            MessageBox.Show("Personel Yaşları Toplamı: " + YaslarınToplamı);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            //Kaç tana çalışanım var

            int? PersonelSayısı = db.Employees.Count();
            MessageBox.Show("Personel Sayısı: " + PersonelSayısı);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //Çalışanlarım ne kadar sipariş almışlar bulunuz 

            dataGridView1.DataSource = db.Orders.Join(db.Employees,
                o => o.EmployeeID,
                k => k.EmployeeID,
                (o,k)=>new 
                {
                o.EmployeeID,
                o.OrderID,
                k.FirstName,
                k.LastName,
                
                }).ToList();

        }
        private void button19_Click(object sender, EventArgs e)
        {
            //Her siparişteki toplam ürün sayısı 100'den fazla olan siparişleri  listeleyiniz
            dataGridView1.DataSource = db.Order_Details.Where(x => x.Quantity > 100).ToList();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //Çalışanların yaşlarının ortalaması bulunuz.

            double? Ortalama = db.Employees.Average(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now));
            MessageBox.Show("Çalışan Yaş Ortalaması:" + Ortalama);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //Bedeli 35'ten az olan ürünleri kategorilerine göre gruplayınız

            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice < 35).OrderBy(x => x.CategoryID).Select(x=>new 
            {

            x.ProductID,
            x.ProductName,
            x.CategoryID,
            x.UnitPrice
            
            }).ToList();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //ProductName 'in içinde "A" olan ve stok miktarı 5 ile 50 arasında olan ürünleri kategorilerine göre azalan sırada sıralayınız.

            dataGridView1.DataSource = db.Products.Where(x => x.ProductName.Contains("A") && x.UnitsInStock < 50 && x.UnitsInStock > 5).OrderByDescending(x => x.CategoryID).ToList();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //Hangi tedarikçiden hangi ürün sağlanıyor listeleyiniz.

            dataGridView1.DataSource = db.Products.Join(
           db.Suppliers,
           p => p.SupplierID,
           s => s.SupplierID,
           (p, s) => new
           {
            p.ProductID,
            p.ProductName,
            s.SupplierID,
            s.CompanyName

           }).ToList();

        }

        private void button24_Click(object sender, EventArgs e)
        {
            //Product tablosundan ProductID, ProductName, CategoryID
            //Categories tablosundan CategoryName, Description getiriniz

            dataGridView1.DataSource = db.Products.Join(
                db.Categories,
                p => p.CategoryID,
                c => c.CategoryID,
                (p, c) => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.CategoryID,
                    c.CategoryName,
                    c.Description
                }).ToList();

        }

        private void button25_Click(object sender, EventArgs e)
        {

            //Hangi sipariş hangi çalışan tarafından  yapılmış.

            dataGridView1.DataSource = db.Orders.Join(db.Employees,
                o => o.EmployeeID,
                k => k.EmployeeID,
                (o, k) => new
                {
                    o.OrderID,
                    o.OrderDate,
                    o.CustomerID,
                    k.EmployeeID,
                    k.FirstName,
                    k.LastName

                }).ToList();

        }

        private void button26_Click(object sender, EventArgs e)
        {
            //Kategorilerime göre toplam stok miktarım
            dataGridView1.DataSource = db.Categories.Join(db.Products,
                c => c.CategoryID,
                p => p.CategoryID,
                (c, p) => new { c, p }).GroupBy(x => x.c.CategoryName).Select(y => new
                {
                    Name = y.Key,
                    Count = y.Sum(z => z.p.UnitsInStock)
                }).ToList();
        }


    }
}
