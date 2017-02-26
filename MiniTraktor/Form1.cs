using Bike18;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
