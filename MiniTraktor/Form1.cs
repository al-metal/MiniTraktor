using Bike18;
using RacerMotors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using web;
using Формирование_ЧПУ;

namespace MiniTraktor
{
    public partial class Form1 : Form
    {
        web.WebRequest webRequest = new web.WebRequest();
        nethouse nethouse = new nethouse();
        FileEdit files = new FileEdit();
        WebClient webClient = new WebClient();
        CHPU chpu = new CHPU();

        Thread forms;

        List<string> newProduct = new List<string>();
        string boldOpen = "<span style=\"\"font-weight: bold; font-weight: bold; \"\">";
        string boldClose = "</span>";
        int countEdit = 0;
        string otv;

        string minitextTemplate;
        string fullTextTemplate;
        string keywordsTextTemplate;
        string titleTextTemplate;
        string descriptionTextTemplate;

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
            #region Login and password
            Properties.Settings.Default.login = tbLogin.Text;
            Properties.Settings.Default.password = tbPassword.Text;
            Properties.Settings.Default.Save();
            #endregion

            CookieContainer cookie = nethouse.CookieNethouse(tbLogin.Text, tbPassword.Text);

            if (cookie.Count != 4)
            {
                MessageBox.Show("Логин или пароль введены не верно!");
                return;
            }

            minitextTemplate = MinitextStr();
            fullTextTemplate = FulltextStr();
            keywordsTextTemplate = tbKeywords.Lines[0].ToString();
            titleTextTemplate = tbTitle.Lines[0].ToString();
            descriptionTextTemplate = tbDescription.Lines[0].ToString();

            File.Delete("naSite.csv");
            newList();

            Thread tabl = new Thread(() => UpdateTovar(cookie));
            forms = tabl;
            forms.IsBackground = true;
            forms.Start();
        }

        private void UpdateTovar(CookieContainer cookie)
        {
            ControlsFormEnabledFalse();

            otv = webRequest.getRequest("https://xn--80andaliilpdrd0d.xn--p1ai/shop/");
            MatchCollection categories = new Regex("(?<=<li class=\"product-category  col-md-4 col-sm-6\">)[\\w\\W]*?(?=<img)").Matches(otv);
            foreach (Match str in categories)
            {
                string urlCategories = str.ToString();
                if (urlCategories.Contains("katalogi-bumazhnaya-produktsiya") || urlCategories.Contains("reklamnaya-produktsiya") || urlCategories.Contains("dvigateli-dizelnyie2"))
                    continue;

                urlCategories = urlCategories.Replace("\">", "").Replace("<a href=\"", "").Trim();

                otv = webRequest.getRequest(urlCategories);
                MatchCollection subCategories = new Regex("(?<=<li class=\"product-category  col-md-4 col-sm-6\">)[\\w\\W]*?(?=<img)").Matches(otv);
                foreach (Match subStr in subCategories)
                {
                    string urlSubCategories = subStr.ToString();
                    urlSubCategories = urlSubCategories.Replace("\">", "").Replace("<a href=\"", "").Trim();

                    GetArrayTovar(urlSubCategories, cookie);
                }

                MatchCollection tovars = new Regex("(?<=<a class=\"product-loop-title\" href=\")[\\w\\W]*?(?=\"><h3>)").Matches(otv);
                foreach (Match tovar in tovars)
                {
                    List<string> product = new List<string>();
                    string url = tovar.ToString();
                    product = GetListTovar(url, cookie);

                    if (product == null)
                        continue;

                    string nameProduct = product[0];
                    string articl = product[1];
                    string searchTovarInBike = nethouse.searchTovar(nameProduct, articl);
                    if(searchTovarInBike == null)
                        WriteTovarInCSV(product);
                    else
                    {
                        string price = product[2];

                        List<string> productB18 = nethouse.GetProductList(cookie, searchTovarInBike);
                        string priceB18 = productB18[9];

                        if(price != priceB18)
                        {
                            productB18[9] = price;
                            nethouse.SaveTovar(cookie, productB18);
                            countEdit++;
                        }
                    }
                }
            }

            System.Threading.Thread.Sleep(10000);
            string[] naSite1 = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
            if (naSite1.Length > 1)
                nethouse.UploadCSVNethouse(cookie, "naSite.csv");

            MessageBox.Show("Обновлено товаров: " + countEdit);

            ControlsFormEnabledTrue();
        }

        private void WriteTovarInCSV(List<string> product)
        {
            string name = product[0];
            string article = product[1];
            string price = product[2];
            string category = product[3];
            string miniDescriptionText = product[4];
            string slug = product[5];

            string miniText = minitextTemplate;
            string fullText = fullTextTemplate;

            miniText = ReplaceNameTovar(name, miniText);
            fullText = ReplaceNameTovar(name, fullText);

            miniText = miniDescriptionText + "<br >" + miniText;

            string titleText = titleTextTemplate;
            string descriptionText = descriptionTextTemplate;
            string keywordsText = keywordsTextTemplate;

            titleText = ReplaceSEO(titleText, name, article);
            descriptionText = ReplaceSEO(descriptionText, name, article);
            keywordsText = ReplaceSEO(keywordsText, name, article);

            if (name.Length > 255)
                name = name.Remove(255);
            if (article.Length > 128)
                article = article.Remove(128);

            if (titleText.Length > 255)
            {
                titleText = titleText.Remove(255);
                titleText = titleText.Remove(titleText.LastIndexOf(" "));
            }
            if (descriptionText.Length > 200)
            {
                descriptionText = descriptionText.Remove(200);
                descriptionText = descriptionText.Remove(descriptionText.LastIndexOf(" "));
            }
            if (keywordsText.Length > 100)
            {
                keywordsText = keywordsText.Remove(100);
                keywordsText = keywordsText.Remove(keywordsText.LastIndexOf(" "));
            }
            
            newProduct = new List<string>();
            newProduct.Add(""); //id
            newProduct.Add("\"" + article.Trim() + "\""); //артикул
            newProduct.Add("\"" + name.Trim() + "\"");  //название
            newProduct.Add("\"" + price.Trim() + "\""); //стоимость
            newProduct.Add("\"" + "" + "\""); //со скидкой
            newProduct.Add("\"" + category.Trim() + "\""); //раздел товара
            newProduct.Add("\"" + "100" + "\""); //в наличии
            newProduct.Add("\"" + "0" + "\"");//поставка
            newProduct.Add("\"" + "1" + "\"");//срок поставки
            newProduct.Add("\"" + miniText.Trim() + "\"");//краткий текст
            newProduct.Add("\"" + fullText.Trim() + "\"");//полностью текст
            newProduct.Add("\"" + titleText.Trim() + "\""); //заголовок страницы
            newProduct.Add("\"" + descriptionText.Trim() + "\""); //описание
            newProduct.Add("\"" + keywordsText.Trim() + "\"");//ключевые слова
            newProduct.Add("\"" + slug.Trim() + "\""); //ЧПУ
            newProduct.Add(""); //с этим товаром покупают
            newProduct.Add("");   //рекламные метки
            newProduct.Add("\"" + "1" + "\"");  //показывать
            newProduct.Add("\"" + "0" + "\""); //удалить

            if (price != "0")
                files.fileWriterCSV(newProduct, "naSite");
        }

        private string ReplaceSEO(string text, string nameTovarRacerMotors, string article)
        {
            string discount = Discount();
            text = text.Replace("СКИДКА", discount).Replace("НАЗВАНИЕ", nameTovarRacerMotors).Replace("АРТИКУЛ", article);
            return text;
        }

        private void GetArrayTovar(string urlSubCategories, CookieContainer cookie)
        {
            string otv = null;
            otv = webRequest.getRequest(urlSubCategories);
            MatchCollection tovars = new Regex("(?<=<a class=\"product-loop-title\" href=\")[\\w\\W]*?(?=\"><h3>)").Matches(otv);
            foreach (Match str in tovars)
            {
                string url = str.ToString();
                GetListTovar(url, cookie);
            }
        }

        private List<string> GetListTovar(string url, CookieContainer cookie)
        {
            List<string> tovar = new List<string>();
            string otv = null;
            string name = null;
            string article = null;
            string price = null;
            string category = null;
            string miniText = null;
            string slug = null;

            otv = webRequest.getRequest(url);
            if (otv == "err")
                return tovar = null;

            name = new Regex("(?<=product_title\">).*?(?=</h1>)").Match(otv).ToString();
            article = new Regex("(?<=itemprop=\"sku\">).*?(?=</span>)").Match(otv).ToString();
            price = new Regex("(?<=\"price\" content=\").*?(?=\" />)").Match(otv).ToString();

            if (name.Contains("&"))
                name = AmpersChar(name);

            slug = chpu.vozvr(name);

            if (article == "" || article == "--" || article == " " || article == "-" || article == "----")
                article = "ur_" + slug;
            else
                article = "ur_" + article;
            article = article.Replace("-", "_");

            price = ReturnPrice(price);
            ImagesDownload(otv, article);
            category = ReturnCategoryTovar(otv);

            miniText = ReturnDescriptionText(otv);
            miniText = AmpersChar(miniText);

            tovar.Add(name);
            tovar.Add(article);
            tovar.Add(price);
            tovar.Add(category);
            tovar.Add(miniText);
            tovar.Add(slug);

            return tovar;
        }

        private void UpdateTovar(string searchTovarInBike, string price, CookieContainer cookie)
        {
            List<string> tovarBike = nethouse.GetProductList(cookie, searchTovarInBike);
            string priceBike = tovarBike[9].ToString();
            if (price != priceBike)
            {
                tovarBike[9] = price;
                nethouse.SaveTovar(cookie, tovarBike);
                countEdit++;
            }
        }

        private string AmpersChar(string text)
        {
            text = text.Replace("&#8211;", "-").Replace("&#8220;", "«").Replace("&#8221;", "»").Replace("&#8212;", "-").Replace("&#8243;", "″").Replace("&#215;", "-").Replace("~", "");
            return text;
        }

        private void ImagesDownload(string otv, string article)
        {
            MatchCollection images = new Regex("(?<=data-zoom-image=\").*?(?=\" title=\")").Matches(otv);
            int i = 0;
            foreach (Match str in images)
            {
                string urlImage = str.ToString();
                if (!File.Exists("pic\\" + article + "-" + i + ".jpg"))
                {
                    try
                    {
                        webClient.DownloadFile(urlImage, "pic\\" + article + "-" + i + ".jpg");
                    }
                    catch
                    {

                    }
                }
                i++;
            }
        }

        private string ReturnPrice(string price)
        {
            double dblPrice = Convert.ToDouble(price);
            dblPrice = dblPrice - (dblPrice * 0.02);
            dblPrice = Math.Round(dblPrice);
            int intPrice = Convert.ToInt32(dblPrice);
            intPrice = (intPrice / 10) * 10;
            price = intPrice.ToString();
            return price;
        }

        private string ReplaceNameTovar(string nameTovar, string text)
        {
            nameTovar = boldOpen + nameTovar + boldClose;
            text = text.Replace("ТОВАР", nameTovar);
            return text;
        }

        private string ReturnDescriptionText(string otv)
        {
            string description = null;
            description = new Regex("(?<=class=\"description\">)[\\w\\W]*?(?=</div>)").Match(otv).ToString();
            MatchCollection urls = new Regex("<a.*?\">").Matches(description);
            foreach (Match str in urls)
            {
                string s = str.ToString();
                description = description.Replace(s, "").Replace("</a>", "").Trim();
            }
            description = specChar(description);

            return description;
        }

        private string ReturnCategoryTovar(string otv)
        {
            string category = "Запчасти и расходники => Запчасти для сельхозтехники и навесного оборудования => ";
            string strCategory = new Regex("(?<=<div class=\"breadcrumbs\">)[\\w\\W]*?(?=</div>)").Match(otv).ToString();
            MatchCollection arrayCategory = new Regex("(?<=\">).*?(?=</a>)").Matches(strCategory);
            if (arrayCategory.Count == 3)
            {
                category += arrayCategory[2].ToString();
            }
            else
            {
                category += arrayCategory[2].ToString() + " => " + arrayCategory[3].ToString();
            }
            string skobki = new Regex("\\(.*\\)").Match(category).ToString();
            if (category.Contains("("))
                category = category.Replace(skobki, "");
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

        private string Discount()
        {
            string discount = "<p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> Сделай ТРОЙНОЙ удар по нашим ценам! </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 1. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Скидки за отзывы о товарах!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 2. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Друзьям скидки и подарки!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 3. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Нашли дешевле!? 110% разницы Ваши!</a></span></p>";
            return discount;
        }

        private List<string> newList()
        {
            List<string> newProduct = new List<string>();
            newProduct.Add("id");                                                                               //id
            newProduct.Add("Артикул *");                                                 //артикул
            newProduct.Add("Название товара *");                                          //название
            newProduct.Add("Стоимость товара *");                                    //стоимость
            newProduct.Add("Стоимость со скидкой");                                       //со скидкой
            newProduct.Add("Раздел товара *");                                         //раздел товара
            newProduct.Add("Товар в наличии *");                                                    //в наличии
            newProduct.Add("Поставка под заказ *");                                                 //поставка
            newProduct.Add("Срок поставки (дни) *");                                           //срок поставки
            newProduct.Add("Краткий текст");                                 //краткий текст
            newProduct.Add("Текст полностью");                                          //полностью текст
            newProduct.Add("Заголовок страницы (title)");                               //заголовок страницы
            newProduct.Add("Описание страницы (description)");                                 //описание
            newProduct.Add("Ключевые слова страницы (keywords)");                                 //ключевые слова
            newProduct.Add("ЧПУ страницы (slug)");                                   //ЧПУ
            newProduct.Add("С этим товаром покупают");                              //с этим товаром покупают
            newProduct.Add("Рекламные метки");
            newProduct.Add("Показывать на сайте *");                                           //показывать
            newProduct.Add("Удалить *");                                    //удалить
            files.fileWriterCSV(newProduct, "naSite");
            return newProduct;
        }

        public string specChar(string text)
        {
            text = text.Replace("&quot;", "\"").Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&laquo;", "«").Replace("&raquo;", "»").Replace("&ndash;", "-").Replace("&mdash;", "-").Replace("&lsquo;", "‘").Replace("&rsquo;", "’").Replace("&sbquo;", "‚").Replace("&ldquo;", "\"").Replace("&rdquo;", "”").Replace("&bdquo;", "„").Replace("&#43;", "+").Replace("&#40;", "(").Replace("&nbsp;", " ").Replace("&#41;", ")").Replace("&amp;quot;", "").Replace("&#039;", "'").Replace("&amp;gt;", ">").Replace("&#43;", "+").Replace("&#40;", "(").Replace("&nbsp;", " ").Replace("&#41;", ")").Replace("&#39;", "'").Replace("&quot;", "\"").Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&laquo;", "«").Replace("&raquo;", "»").Replace("&ndash;", "-").Replace("&mdash;", "-").Replace("&lsquo;", "‘").Replace("&rsquo;", "’").Replace("&sbquo;", "‚").Replace("&ldquo;", "\"").Replace("&rdquo;", "”").Replace("&bdquo;", "„").Replace("&#43;", "+").Replace("&#40;", "(").Replace("&nbsp;", " ").Replace("&#41;", ")").Replace("&amp;quot;", "").Replace("&#039;", "'").Replace("&amp;gt;", ">").Replace("&#39;", "'").Replace("&Oslash;", "Ø").Replace("&#8211;", "-");

            return text;
        }

        private void ControlsFormEnabledTrue()
        {
            btnStart.Invoke(new Action(() => btnStart.Enabled = true));
            btnSaveTemplate.Invoke(new Action(() => btnSaveTemplate.Enabled = true));
            rtbFullText.Invoke(new Action(() => rtbFullText.Enabled = true));
            rtbMiniText.Invoke(new Action(() => rtbMiniText.Enabled = true));
            tbDescription.Invoke(new Action(() => tbDescription.Enabled = true));
            tbKeywords.Invoke(new Action(() => tbKeywords.Enabled = true));
            tbLogin.Invoke(new Action(() => tbLogin.Enabled = true));
            tbPassword.Invoke(new Action(() => tbPassword.Enabled = true));
            tbTitle.Invoke(new Action(() => tbTitle.Enabled = true));
        }

        private void ControlsFormEnabledFalse()
        {
            btnStart.Invoke(new Action(() => btnStart.Enabled = false));
            btnSaveTemplate.Invoke(new Action(() => btnSaveTemplate.Enabled = false));
            rtbFullText.Invoke(new Action(() => rtbFullText.Enabled = false));
            rtbMiniText.Invoke(new Action(() => rtbMiniText.Enabled = false));
            tbDescription.Invoke(new Action(() => tbDescription.Enabled = false));
            tbKeywords.Invoke(new Action(() => tbKeywords.Enabled = false));
            tbLogin.Invoke(new Action(() => tbLogin.Enabled = false));
            tbPassword.Invoke(new Action(() => tbPassword.Enabled = false));
            tbTitle.Invoke(new Action(() => tbTitle.Enabled = false));
        }

    }
}
