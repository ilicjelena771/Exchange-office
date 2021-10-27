using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//definisemo strukturu 
struct ValInfo
{ 
    public double RSDtoVal;
    public double ValToRSD;
    public string ImagePath;
   
   
    private double v;
    private double v1;
    private string v2;
    //konstruktor samo generise

    public ValInfo(double v, double v1, string v2) : this()
    {
        this.v = v;
        this.v1 = v1;
        this.v2 = v2;
    }
};


namespace Konverzija_valuta
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        //pravimo mapu
        Dictionary<string, ValInfo> valute = new Dictionary<string, ValInfo>()

            {
                { "AUD", new ValInfo{ RSDtoVal=0.01132, ValToRSD=88.3394,ImagePath="australia.png" } },
                { "ATS", new ValInfo{ RSDtoVal=0.1110, ValToRSD=9.0103, ImagePath="austria.png"} },
                { "BAM", new ValInfo{ RSDtoVal=0.01577, ValToRSD=63.3922, ImagePath="bosna.png"} },
                { "DKK", new ValInfo{ RSDtoVal=0.06003, ValToRSD=16.6583, ImagePath="danska.png"} },
                { "EUR", new ValInfo{ RSDtoVal=0.0081, ValToRSD=123.9844, ImagePath="eu.jpg"} },
                { "JPY", new ValInfo{ RSDtoVal=0.9644, ValToRSD=1.03691, ImagePath="japan.jpg"} },
                { "CAD", new ValInfo{ RSDtoVal=0.01157, ValToRSD=86.4424,ImagePath= "kanada.png"} },
                { "CNY", new ValInfo{ RSDtoVal=0.05981, ValToRSD=16.7203, ImagePath="kina.png"} },
                { "DEM", new ValInfo{ RSDtoVal=0.01577, ValToRSD=63.3922, ImagePath="nemacka.jpg"} },
                { "NOK", new ValInfo{ RSDtoVal=0.0739, ValToRSD=13.5322, ImagePath="norveska.png"} },
                { "RUB", new ValInfo{ RSDtoVal=0.49208, ValToRSD=2.0322, ImagePath="rusija.png"} },
                { "USD", new ValInfo{ RSDtoVal=0.0087, ValToRSD=115.3129,ImagePath= "sad.gif"} },
                { "CHF", new ValInfo{ RSDtoVal=0.0087, ValToRSD=115.6032, ImagePath="svajcarska.gif"} },
                { "SEK", new ValInfo{ RSDtoVal=0.07709, ValToRSD=12.9717, ImagePath="svedska.png"}},
                { "GBP", new ValInfo{ RSDtoVal=0.00697, ValToRSD=143.4175, ImagePath="britanija.jpg"} }

            };


        private void Form1_Load(object sender, EventArgs e)
        {

            //inicijalizujemo vrednosti gde ce da nam stoji prvi picturebox i odgovarajuce labele
            
            int odLeve = 40, odGore = 40;
            int vOdLeve = 65,  vOdGore= 95;
            int iOdLeve = 55, iOdGore = 110;

            //u ovoj petlji za svaku vrednost iz mape pravimo pictureboxove i labele
            foreach (KeyValuePair<string, ValInfo> item in valute)
            {
                PictureBox p1 = new PictureBox();
                p1.Location = new Point(odLeve, odGore);
                p1.Size = new Size(80, 50);
                p1.BackgroundImage = Image.FromFile(item.Value.ImagePath);
                p1.BackgroundImageLayout = ImageLayout.Stretch;
                this.Controls.Add(p1);

                Label l1 = new Label();
                l1.Location = new Point(vOdLeve, vOdGore);
                l1.Size = new Size(50, 15);
                l1.Text = item.Key;
                l1.ForeColor = Color.SeaGreen;
                this.Controls.Add(l1);

                Label ll1 = new Label();
                ll1.Location = new Point(iOdLeve, iOdGore);
                ll1.Size = new Size(50, 15);
                ll1.Text = item.Value.ValToRSD.ToString();
                ll1.ForeColor = Color.SeaGreen;
                this.Controls.Add(ll1);
                //svaku sledecu vrednost pomeramo u desno, tj. pomeramo od leve ivice za 115
                odLeve += 115;
                vOdLeve += 115;
                iOdLeve += 115;
                //u jednom redu treba da se nadje 5 valuta,
                //sto znaci ako nam je vrednost od leve ivice postala veca od 500 prelazimo u novi red
                if (odLeve > 500)
                {
                    odLeve = 40;
                    vOdLeve = 65;
                    iOdLeve = 55;
                    odGore += 100;
                    vOdGore += 100;
                    iOdGore += 100;
                }

           }

       }

        //radimo samo konverziju iz dinara u stranu valutu i obrnuto
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != "RSD")
            {
                comboBox2.Text = "RSD";
                comboBox2.Enabled = false;

            }
            else
            {
                comboBox2.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double novac;//ono sto konvertujemo
            double kurs;//koliki je kurs
            double rezultat;//sta dobijemo pri konverziji
            if (double.TryParse(textBox1.Text, out novac))
            {
                //ako menjamo iz strane valute u dinare
                if (comboBox1.SelectedItem != "RSD")
                {

                    kurs = valute[comboBox1.SelectedItem.ToString()].ValToRSD;//iz mape izvucemo kurs
                    rezultat = novac * kurs;
                    rez.Text = rezultat.ToString();

                }
                //ako menjamo novac iz dinara u stranu valutu
                else if (comboBox1.SelectedItem == "RSD")
                {
                    if (comboBox2.SelectedItem != "RSD")
                    {
                        kurs = valute[comboBox2.SelectedItem.ToString()].RSDtoVal;//iz mape izvucemo kurs
                        rezultat = novac * kurs;
                        rez.Text = rezultat.ToString();
                    }
                    else {
                        rez.Text = novac.ToString();//ako unesemo iz rsd u rsd ostaje ista vrednost
                    }
                }                
            }
            else
            {
                MessageBox.Show("Neispravan unos kolicine novca.","Greska");
            }
        }        
    }
}