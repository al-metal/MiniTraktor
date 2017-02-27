using Bike18;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using web;

namespace MiniTraktor
{
    public partial class Form1 : Form
    {
        WebRequest webRequest = new WebRequest();
        nethouse nethouse = new nethouse();

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists("files"))
            {
                Directory.CreateDirectory("files");
            }
            if (!Directory.Exists("pic"))
            {
                Directory.CreateDirectory("pic");
            }

            if (!File.Exists("files\\miniText.txt"))
            {
                File.Create("files\\miniText.txt");
            }

            if (!File.Exists("files\\fullText.txt"))
            {
                File.Create("files\\fullText.txt");
            }

            if (!File.Exists("files\\title.txt"))
            {
                File.Create("files\\title.txt");
            }

            if (!File.Exists("files\\description.txt"))
            {
                File.Create("files\\description.txt");
            }

            if (!File.Exists("files\\keywords.txt"))
            {
                File.Create("files\\keywords.txt");
            }
            StreamReader altText = new StreamReader("files\\miniText.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                rtbMiniText.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\fullText.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                rtbFullText.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\title.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                tbTitle.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\description.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                tbDescription.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\keywords.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                tbKeywords.AppendText(str + "\n");
            }
            altText.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbLogin.Text = Properties.Settings.Default.login;
            tbPassword.Text = Properties.Settings.Default.password;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string otv = null;
            #region Login and password
            Properties.Settings.Default.login = tbLogin.Text;
            Properties.Settings.Default.password = tbPassword.Text;
            Properties.Settings.Default.Save();
            #endregion

            otv = webRequest.getRequest("https://xn--80andaliilpdrd0d.xn--p1ai/shop/");
            MatchCollection categories = new Regex("(?<=<li class=\"product-category  col-md-4 col-sm-6\">)[\\w\\W]*?(?=<img)").Matches(otv);
            foreach(Match str in categories)
            {
                string urlCategories = str.ToString();
                urlCategories = urlCategories.Replace("\">", "").Replace("<a href=\"", "").Trim();

                otv = webRequest.getRequest(urlCategories);
                MatchCollection subCategories = new Regex("(?<=<li class=\"product-category  col-md-4 col-sm-6\">)[\\w\\W]*?(?=<img)").Matches(otv);
                foreach(Match subStr in subCategories)
                {
                    string urlSubCategories = subStr.ToString();
                    urlSubCategories = urlSubCategories.Replace("\">", "").Replace("<a href=\"", "").Trim();

                    GetArrayTovar(urlSubCategories);
                }
            }
        }

        private void GetArrayTovar(string urlSubCategories)
        {
            string otv = null;
            otv = webRequest.getRequest(urlSubCategories);
            MatchCollection tovars = new Regex("(?<=<a class=\"product-loop-title\" href=\")[\\w\\W]*?(?=\"><h3>)").Matches(otv);
            foreach(Match str in tovars)
            {
                string url = str.ToString();
                GetListTovar(url);
            }
        }

        private void GetListTovar(string url)
        {
            string otv = null;
            string name = null;
            string article = null;
            string price = null;
            string category = null;
            string miniText = null;
            string fullText = null;
            string title= null;
            string description = null;
            string keywords = null;
            otv = webRequest.getRequest(url);
            name = new Regex("(?<=product_title\">).*?(?=</h1>)").Match(otv).ToString();
            article = new Regex("(?<=itemprop=\"sku\">).*?(?=</span>)").Match(otv).ToString();
            price = new Regex("(?<=\"price\" content=\").*?(?=\" />)").Match(otv).ToString();
            category = ReturnCategoryTovar(otv);
            miniText = ReturnDescriptionText(otv);
            miniText = ReplaceNameTovar(name, miniText);
        }

        private string ReplaceNameTovar(string nameTovar, string text)
        {
            text = text.Replace("ТОВАР", nameTovar);
            return text;
        }

        private string ReturnDescriptionText(string otv)
        {
            string description = null;
            description = new Regex("(?<=class=\"description\">)[\\w\\W]*?(?=</div>)").Match(otv).ToString();
            MatchCollection urls = new Regex("<a.*?\">").Matches(description);
            foreach(Match str in urls)
            {
                string s = str.ToString();
                description = description.Replace(s, "").Replace("</a>", "").Trim();
            }
            description = nethouse.specChar(description);

            string templateMiniText = MinitextStr();
            string discounts = ReturnDiscountsText();
            description = description + "<br />" + templateMiniText + "<br />" + discounts;
            return description;
        }

        private string ReturnCategoryTovar(string otv)
        {
            string category = "Запчасти и расходники => Запчасти для сельхозтехники и навесного оборудования => ";
            string strCategory = new Regex("(?<=<div class=\"breadcrumbs\">)[\\w\\W]*?(?=</div>)").Match(otv).ToString();
            MatchCollection arrayCategory = new Regex("(?<=\">).*?(?=</a>)").Matches(strCategory);
            category += arrayCategory[2].ToString() + " => " + arrayCategory[3].ToString();
            return category;
        }
        
        private string MinitextStr()
        {
            string minitext = "";
            for (int z = 0; rtbMiniText.Lines.Length > z; z++)
            {
                if (rtbMiniText.Lines[z].ToString() == "")
                {
                    minitext += "<p><br /></p>";
                }
                else
                {
                    minitext += "<p>" + rtbMiniText.Lines[z].ToString() + "</p>";
                }
            }
            return minitext;
        }

        private string FulltextStr()
        {
            string fullText = "";
            for (int z = 0; rtbFullText.Lines.Length > z; z++)
            {
                if (rtbFullText.Lines[z].ToString() == "")
                {
                    fullText += "<p><br /></p>";
                }
                else
                {
                    fullText += "<p>" + rtbFullText.Lines[z].ToString() + "</p>";
                }
            }
            return fullText;
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            int count = 0;
            StreamWriter writers = new StreamWriter("files\\miniText.txt", false, Encoding.GetEncoding(1251));
            count = rtbMiniText.Lines.Length;
            for (int i = 0; rtbMiniText.Lines.Length > i; i++)
            {
                if (count - 1 == i)
                {
                    if (rtbMiniText.Lines[i] == "")
                        break;
                }
                writers.WriteLine(rtbMiniText.Lines[i].ToString());
            }
            writers.Close();

            writers = new StreamWriter("files\\fullText.txt", false, Encoding.GetEncoding(1251));
            count = rtbFullText.Lines.Length;
            for (int i = 0; count > i; i++)
            {
                if (count - 1 == i)
                {
                    if (rtbFullText.Lines[i] == "")
                        break;
                }
                writers.WriteLine(rtbFullText.Lines[i].ToString());
            }
            writers.Close();

            writers = new StreamWriter("files\\title.txt", false, Encoding.GetEncoding(1251));
            writers.WriteLine(tbTitle.Lines[0]);
            writers.Close();

            writers = new StreamWriter("files\\description.txt", false, Encoding.GetEncoding(1251));
            writers.WriteLine(tbDescription.Lines[0]);
            writers.Close();

            writers = new StreamWriter("files\\keywords.txt", false, Encoding.GetEncoding(1251));
            writers.WriteLine(tbKeywords.Lines[0]);
            writers.Close();

            MessageBox.Show("Сохранено");
        }

        private string ReturnDiscountsText()
        {
            string discount = "<p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> Сделай ТРОЙНОЙ удар по нашим ценам! </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 1. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Скидки за отзывы о товарах!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 2. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Друзьям скидки и подарки!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 3. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Нашли дешевле!? 110% разницы Ваши!</a></span></p>";
            return discount;
        }

    }
}
