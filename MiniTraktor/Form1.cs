using NehouseLibrary;
using xNet.Net;
using RacerMotors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
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
        string boldOpenCSV = "<span style=\"\"font-weight: bold; font-weight: bold;\"\">";
        string boldOpenSite = "<span style=\"font-weight: bold; font-weight: bold;\">";
        string boldOpen = "";
        string boldClose = "</span>";
        int countEdit = 0;
        string otv;

        bool chekedSEO = false;
        bool chekedMiniText = false;
        bool chekedFullText = false;

        string minitextTemplate;
        string fullTextTemplate;
        string keywordsTextTemplate;
        string titleTextTemplate;
        string descriptionTextTemplate;

        string discount = "";

        Dictionary<string, string> simbols = new Dictionary<string, string>();
        Dictionary<string, string> ampersands = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();

            #region ampersand
            ampersands.Add("&bull;", "·");
            ampersands.Add("&#32;", " ");
            ampersands.Add("&#33;", "!");
            ampersands.Add("&#34;", "\"");
            ampersands.Add("&quot;", "\"");
            ampersands.Add("&#35;", "#");
            ampersands.Add("&#36;", "$");
            ampersands.Add("&#37;", "%");
            ampersands.Add("&#38;", "&");
            ampersands.Add("&amp;", "&");
            ampersands.Add("&#39;", "'");
            ampersands.Add("&#40", "(");
            ampersands.Add("&#41;", ")");
            ampersands.Add("&#42;", "*");
            ampersands.Add("&#43;", "+");
            ampersands.Add("&#44;", ",");
            ampersands.Add("&#45;", "-");
            ampersands.Add("&#46;", ".");
            ampersands.Add("&#47;", "/");
            ampersands.Add("&#48;", "0");
            ampersands.Add("&#49;", "1");
            ampersands.Add("&#50;", "2");
            ampersands.Add("&#51;", "3");
            ampersands.Add("&#52;", "4");
            ampersands.Add("&#53;", "5");
            ampersands.Add("&#54;", "6");
            ampersands.Add("&#55;", "7");
            ampersands.Add("&#56;", "8");
            ampersands.Add("&#57;", "9");
            ampersands.Add("&#58;", ":");
            ampersands.Add("&#59;", ";");
            ampersands.Add("&#60;", "<");
            ampersands.Add("&lt;", "<");
            ampersands.Add("&#61;", "=");
            ampersands.Add("&#62;", ">");
            ampersands.Add("&gt;", ">");
            ampersands.Add("&#63;", "?");
            ampersands.Add("&#64;", "@");
            ampersands.Add("&#65;", "A");
            ampersands.Add("&#66;", "B");
            ampersands.Add("&#67;", "C");
            ampersands.Add("&#68;", "D");
            ampersands.Add("&#69;", "E");
            ampersands.Add("&#70;", "F");
            ampersands.Add("&#71;", "G");
            ampersands.Add("&#72;", "H");
            ampersands.Add("&#73;", "I");
            ampersands.Add("&#74;", "J");
            ampersands.Add("&#75;", "K");
            ampersands.Add("&#76;", "L");
            ampersands.Add("&#77;", "-");
            ampersands.Add("&#78;", "N");
            ampersands.Add("&#79;", "O");
            ampersands.Add("&#80;", "P");
            ampersands.Add("&#81;", "Q");
            ampersands.Add("&#82;", "R");
            ampersands.Add("&#83;", "S");
            ampersands.Add("&#84;", "T");
            ampersands.Add("&#85;", "U");
            ampersands.Add("&#86;", "V");
            ampersands.Add("&#87;", "W");
            ampersands.Add("&#88;", "X");
            ampersands.Add("&#89;", "Y");
            ampersands.Add("&#90;", "Z");
            ampersands.Add("&#91;", "[");
            ampersands.Add("&#92;", "\\");
            ampersands.Add("&#93;", "]");
            ampersands.Add("&#94;", "^");
            ampersands.Add("&#95;", "_");
            ampersands.Add("&#96;", "`");
            ampersands.Add("&#97;", "a");
            ampersands.Add("&#98;", "b");
            ampersands.Add("&#99;", "c");
            ampersands.Add("&#100;", "d");
            ampersands.Add("&#101;", "e");
            ampersands.Add("&#102;", "f");
            ampersands.Add("&#103;", "g");
            ampersands.Add("&#104;", "-");
            ampersands.Add("&#105;", "i");
            ampersands.Add("&#106;", "j");
            ampersands.Add("&#107;", "k");
            ampersands.Add("&#108;", "l");
            ampersands.Add("&#109;", "m");
            ampersands.Add("&#110;", "n");
            ampersands.Add("&#111;", "o");
            ampersands.Add("&#112;", "p");
            ampersands.Add("&#113;", "q");
            ampersands.Add("&#114;", "r");
            ampersands.Add("&#115;", "s");
            ampersands.Add("&#116;", "t");
            ampersands.Add("&#117;", "u");
            ampersands.Add("&#118;", "v");
            ampersands.Add("&#119;", "w");
            ampersands.Add("&#120;", "x");
            ampersands.Add("&#121;", "y");
            ampersands.Add("&#122;", "z");
            ampersands.Add("&#123;", "{");
            ampersands.Add("&#124;", "|");
            ampersands.Add("&#125;", "}");
            ampersands.Add("&#126;", "~");
            ampersands.Add("&#161;", "¡");
            ampersands.Add("&iexcl;", "¡");
            ampersands.Add("&#160;", " ");
            ampersands.Add("&nbsp;", " ");
            ampersands.Add("&#162;", "¢");
            ampersands.Add("&cent;", "¢");
            ampersands.Add("&#163;", "£");
            ampersands.Add("&pound;", "£");
            ampersands.Add("&#164;", "¤");
            ampersands.Add("&curren;", "¤");
            ampersands.Add("&#165;", "¥");
            ampersands.Add("&yen;", "¥");
            ampersands.Add("&#166;", "¦");
            ampersands.Add("&brvbar;", "¦");
            ampersands.Add("&#167;", "§");
            ampersands.Add("&sect;", "§");
            ampersands.Add("&#168;", "¨");
            ampersands.Add("&uml;", "¨");
            ampersands.Add("&#169;", "©");
            ampersands.Add("&copy;", "©");
            ampersands.Add("&#170;", "ª");
            ampersands.Add("&ordf;", "ª");
            ampersands.Add("&#171;", "«");
            ampersands.Add("&laquo;", "«");
            ampersands.Add("&#172;", "¬");
            ampersands.Add("&not;", "¬");
            ampersands.Add("&#173;", "­ ");
            ampersands.Add("&shy;", " ");
            ampersands.Add("&#174;", "®");
            ampersands.Add("&reg;", "®");
            ampersands.Add("&#175;", "¯");
            ampersands.Add("&macr;", "¯");
            ampersands.Add("&#176;", "°");
            ampersands.Add("&deg;", "°");
            ampersands.Add("&#177;", "±");
            ampersands.Add("&plusmn;", "±");
            ampersands.Add("&#178;", "²");
            ampersands.Add("&sup2;", "²");
            ampersands.Add("&#179;", "³");
            ampersands.Add("&sup3;", "³");
            ampersands.Add("&#180;", "´");
            ampersands.Add("&acute;", "´");
            ampersands.Add("&#181;", "µ");
            ampersands.Add("&micro;", "µ");
            ampersands.Add("&#182;", "¶");
            ampersands.Add("&para;", "¶");
            ampersands.Add("&#183;", "·");
            ampersands.Add("&middot;", "·");
            ampersands.Add("&#184;", "¸");
            ampersands.Add("&cedil;", "¸");
            ampersands.Add("&#185;", "¹");
            ampersands.Add("&sup1;", "¹");
            ampersands.Add("&#186;", "º");
            ampersands.Add("&ordm;", "º");
            ampersands.Add("&#187;", "»");
            ampersands.Add("&raquo;", "»");
            ampersands.Add("&#188;", "¼");
            ampersands.Add("&frac14;", "¼");
            ampersands.Add("&#189;", "½");
            ampersands.Add("&frac12;", "½");
            ampersands.Add("&#190;", "¾");
            ampersands.Add("&frac34;", "¾");
            ampersands.Add("&#191;", "¿");
            ampersands.Add("&iquest;", "¿");
            ampersands.Add("&#192;", "À");
            ampersands.Add("&Agrave;", "À");
            ampersands.Add("&#193;", "Á");
            ampersands.Add("&Aacute;", "Á");
            ampersands.Add("&#194;", "Â");
            ampersands.Add("&Acirc;", "Â");
            ampersands.Add("&#195;", "Ã");
            ampersands.Add("&Atilde;", "Ã");
            ampersands.Add("&#196;", "Ä");
            ampersands.Add("&Auml;", "Ä");
            ampersands.Add("&#197;", "Å");
            ampersands.Add("&Aring;", "Å");
            ampersands.Add("&#198;", "Æ");
            ampersands.Add("&AElig;", "Æ");
            ampersands.Add("&#199;", "Ç");
            ampersands.Add("&Ccedil;", "Ç");
            ampersands.Add("&#200;", "È");
            ampersands.Add("&Egrave;", "È");
            ampersands.Add("&#201;", "É");
            ampersands.Add("&Eacute;", "É");
            ampersands.Add("&#202;", "Ê");
            ampersands.Add("&Ecirc;", "Ê");
            ampersands.Add("&#203;", "Ë");
            ampersands.Add("&Euml;", "Ë");
            ampersands.Add("&#204;", "Ì");
            ampersands.Add("&Igrave;", "Ì");
            ampersands.Add("&#205;", "Í");
            ampersands.Add("&Iacute;", "Í");
            ampersands.Add("&#206;", "Î");
            ampersands.Add("&Icirc;", "Î");
            ampersands.Add("&#207;", "Ï");
            ampersands.Add("&Iuml;", "Ï");
            ampersands.Add("&#208;", "Ð");
            ampersands.Add("&ETH;", "Ð");
            ampersands.Add("&#209;", "Ñ");
            ampersands.Add("&Ntilde;", "Ñ");
            ampersands.Add("&#210;", "Ò");
            ampersands.Add("&Ograve;", "Ò");
            ampersands.Add("&#211;", "Ó");
            ampersands.Add("&Oacute;", "Ó");
            ampersands.Add("&#212;", "Ô");
            ampersands.Add("&Ocirc;", "Ô");
            ampersands.Add("&#213;", "Õ");
            ampersands.Add("&Otilde;", "Õ");
            ampersands.Add("&#214;", "Ö");
            ampersands.Add("&Ouml;", "Ö");
            ampersands.Add("&#215;", "×");
            ampersands.Add("&times;", "×");
            ampersands.Add("&#216;", "Ø");
            ampersands.Add("&Oslash;", "Ø");
            ampersands.Add("&#217;", "Ù");
            ampersands.Add("&Ugrave;", "Ù");
            ampersands.Add("&#218;", "Ú");
            ampersands.Add("&Uacute;", "Ú");
            ampersands.Add("&#219;", "Û");
            ampersands.Add("&Ucirc;", "Û");
            ampersands.Add("&#220;", "Ü");
            ampersands.Add("&Uuml;", "Ü");
            ampersands.Add("&#221;", "Ý");
            ampersands.Add("&Yacute;", "Ý");
            ampersands.Add("&#222;", "Þ");
            ampersands.Add("&THORN;", "Þ");
            ampersands.Add("&#223;", "ß");
            ampersands.Add("&szlig;", "ß");
            ampersands.Add("&#224;", "à");
            ampersands.Add("&agrave;", "à");
            ampersands.Add("&#225;", "á");
            ampersands.Add("&aacute;", "á");
            ampersands.Add("&#226;", "â");
            ampersands.Add("&acirc;", "â");
            ampersands.Add("&#227;", "ã");
            ampersands.Add("&atilde;", "ã");
            ampersands.Add("&#228;", "ä");
            ampersands.Add("&auml;", "ä");
            ampersands.Add("&#229;", "å");
            ampersands.Add("&aring;", "å");
            ampersands.Add("&#230;", "æ");
            ampersands.Add("&aelig;", "æ");
            ampersands.Add("&#231;", "ç");
            ampersands.Add("&ccedil;", "ç");
            ampersands.Add("&#232;", "è");
            ampersands.Add("&egrave;", "è");
            ampersands.Add("&#233;", "é");
            ampersands.Add("&eacute;", "é");
            ampersands.Add("&#234;", "ê");
            ampersands.Add("&ecirc;", "ê");
            ampersands.Add("&#235;", "ë");
            ampersands.Add("&euml;", "ë");
            ampersands.Add("&#236;", "ì");
            ampersands.Add("&igrave;", "ì");
            ampersands.Add("&#237;", "í");
            ampersands.Add("&iacute;", "í");
            ampersands.Add("&#238;", "î");
            ampersands.Add("&icirc;", "î");
            ampersands.Add("&#239;", "ï");
            ampersands.Add("&iuml;", "ï");
            ampersands.Add("&#240;", "ð");
            ampersands.Add("&eth;", "ð");
            ampersands.Add("&#241;", "ñ");
            ampersands.Add("&ntilde;", "ñ");
            ampersands.Add("&#242;", "ò");
            ampersands.Add("&ograve;", "ò");
            ampersands.Add("&#243;", "ó");
            ampersands.Add("&oacute;", "ó");
            ampersands.Add("&#244;", "ô");
            ampersands.Add("&ocirc;", "ô");
            ampersands.Add("&#245;", "õ");
            ampersands.Add("&otilde;", "õ");
            ampersands.Add("&#246;", "ö");
            ampersands.Add("&ouml;", "ö");
            ampersands.Add("&#247;", "÷");
            ampersands.Add("&divide;", "÷");
            ampersands.Add("&#248;", "ø");
            ampersands.Add("&oslash;", "ø");
            ampersands.Add("&#249;", "ù");
            ampersands.Add("&ugrave;", "ù");
            ampersands.Add("&#250;", "ú");
            ampersands.Add("&uacute;", "ú");
            ampersands.Add("&#251;", "û");
            ampersands.Add("&ucirc;", "û");
            ampersands.Add("&#252;", "ü");
            ampersands.Add("&uuml;", "ü");
            ampersands.Add("&#253;", "ý");
            ampersands.Add("&yacute;", "ý");
            ampersands.Add("&#254;", "þ");
            ampersands.Add("&thorn;", "þ");
            ampersands.Add("&#255;", "ÿ");
            ampersands.Add("&yuml;", "ÿ");

            #endregion

            simbols.Add(" ", "_");
            simbols.Add("-", "_");
            simbols.Add("!", "_");
            simbols.Add("@", "_");
            simbols.Add("#", "_");
            simbols.Add("$", "_");
            simbols.Add("%", "_");
            simbols.Add("^", "_");
            simbols.Add("&", "_");
            simbols.Add("*", "_");
            simbols.Add("(", "_");
            simbols.Add(")", "_");
            simbols.Add("=", "_");
            simbols.Add("\"", "_");
            simbols.Add("№", "_");
            simbols.Add(";", "_");
            simbols.Add(":", "_");
            simbols.Add("?", "_");
            simbols.Add("+", "_");
            simbols.Add("{", "_");
            simbols.Add("}", "_");
            simbols.Add("[", "_");
            simbols.Add("]", "_");
            simbols.Add("|", "_");
            simbols.Add("/", "_");
            simbols.Add(",", "_");
            simbols.Add(".", "_");
            simbols.Add("  ", "_");
            simbols.Add("--", "_");
            simbols.Add("---", "_");
            simbols.Add("~", "_");
            simbols.Add("`", "_");
            simbols.Add("·", "_");
            simbols.Add("__", "_");
            simbols.Add("___", "_");

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

            CookieDictionary cookie = nethouse.CookieNethouse(tbLogin.Text, tbPassword.Text);

            if (cookie.Count != 4)
            {
                MessageBox.Show("Логин или пароль введены не верно!");
                return;
            }

            chekedSEO = cbSEO.Checked;
            chekedMiniText = cbMinitext.Checked;
            chekedFullText = cbFullText.Checked;

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

        private void UpdateTovar(CookieDictionary cookie)
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
                    if (searchTovarInBike == "")
                    {
                        discount = DiscountCSV();
                        boldOpen = boldOpenCSV;
                        WriteTovarInCSV(product);
                    }
                    else
                    {
                        discount = DiscountSite();
                        boldOpen = boldOpenSite;
                        bool edits = false;
                        string name = product[0];
                        string article = product[1];
                        string price = product[2];
                        string category = product[3];
                        string miniDescriptionText = product[4];
                        string slug = product[5];
                        string availability = product[6];

                        List<string> productB18 = nethouse.GetProductList(cookie, searchTovarInBike);

                        string priceB18 = productB18[9];

                        if (price != priceB18)
                        {
                            productB18[9] = price;
                            edits = true;
                        }

                        if (availability == "Нет в наличии")
                        {
                            productB18[43] = "0";
                            edits = true;
                        }

                        if (productB18[43] == "0" && availability != "Нет в наличии")
                        {
                            productB18[43] = "100";
                            edits = true;
                        }

                        if (productB18[42].Contains("&alsoBuy[0]=0") || productB18[42].Contains("&alsoBuy[0]=als"))
                        {
                            productB18[42] = nethouse.alsoBuyTovars(productB18);
                            edits = true;
                        }

                        if (chekedSEO)
                        {
                            string titleText = titleTextTemplate;
                            string descriptionText = descriptionTextTemplate;
                            string keywordsText = keywordsTextTemplate;

                            titleText = ReplaceSEO(titleText, name, article);
                            descriptionText = ReplaceSEO(descriptionText, name, article);
                            keywordsText = ReplaceSEO(keywordsText, name, article);

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

                            productB18[11] = descriptionText;
                            productB18[12] = keywordsText;
                            productB18[13] = titleText;
                            edits = true;
                        }

                        if (chekedMiniText)
                        {
                            string miniText = minitextTemplate;
                            miniText = ReplaceNameTovar(name, miniText);
                            miniText = miniDescriptionText + "<br >" + miniText;
                            miniText = ReplaceSEO(miniText, name, article);
                            productB18[7] = miniText;

                            edits = true;
                        }

                        if (chekedFullText)
                        {
                            string fullText = fullTextTemplate;
                            fullText = ReplaceNameTovar(name, fullText);
                            fullText = ReplaceSEO(fullText, name, article);
                            productB18[8] = fullText;

                            edits = true;
                        }

                        productB18 = ReplaceAmpersChar(productB18);

                        if (edits)
                        {
                            nethouse.SaveTovar(cookie, productB18);
                            countEdit++;
                        }
                    }
                }
            }

            System.Threading.Thread.Sleep(10000);
            string[] naSite1 = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
            if (naSite1.Length > 1)
                nethouse.UploadCSVNethouse(cookie, "naSite.csv", tbLogin.Text, tbPassword.Text);

            MessageBox.Show("Обновлено товаров: " + countEdit);

            ControlsFormEnabledTrue();
        }

        private List<string> ReplaceAmpersChar(List<string> tovarList)
        {
            tovarList[7] = ampersChar(tovarList[7]);
            tovarList[8] = ampersChar(tovarList[8]);
            tovarList[11] = ampersChar(tovarList[11]);
            tovarList[12] = ampersChar(tovarList[12]);
            tovarList[13] = ampersChar(tovarList[13]);
            return tovarList;
        }

        private string ampersChar(string text)
        {
            if (text == null)
                return text = "";
            foreach (KeyValuePair<string, string> pair in ampersands)
            {
                text = text.Replace(pair.Key, pair.Value);
            }
            return text;
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
            text = text.Replace("СКИДКА", discount).Replace("НАЗВАНИЕ", nameTovarRacerMotors).Replace("АРТИКУЛ", article);
            return text;
        }

        private void GetArrayTovar(string urlSubCategories, CookieDictionary cookie)
        {
            string otv = null;
            otv = webRequest.getRequest(urlSubCategories);
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
                if (searchTovarInBike == null)
                    WriteTovarInCSV(product);
                else
                {
                    string price = product[2];

                    List<string> productB18 = nethouse.GetProductList(cookie, searchTovarInBike);
                    string priceB18 = productB18[9];

                    if (price != priceB18)
                    {
                        productB18[9] = price;
                        nethouse.SaveTovar(cookie, productB18);
                        countEdit++;
                    }
                }
            }
        }

        private List<string> GetListTovar(string url, CookieDictionary cookie)
        {
            List<string> tovar = new List<string>();
            string otv = null;
            string name = null;
            string article = null;
            string price = null;
            string category = null;
            string miniText = null;
            string slug = null;
            string availability = null;

            otv = webRequest.getRequest(url);
            if (otv == "err")
                return tovar = null;

            name = new Regex("(?<=product_title\">).*?(?=</h1>)").Match(otv).ToString();
            article = new Regex("(?<=itemprop=\"sku\">).*?(?=</span>)").Match(otv).ToString();
            price = new Regex("(?<=\"price\" content=\").*?(?=\" />)").Match(otv).ToString();
            availability = new Regex("(?<=<p class=\"stock out-of-stock\">).*(?=</p>)").Match(otv).ToString();

            if (name.Contains("&"))
                name = AmpersChar(name);

            slug = chpu.vozvr(name);

            if (article == "" || article == "--" || article == " " || article == "-" || article == "----")
                article = "ur_" + slug;
            else
                article = "ur_" + article;
            article = ReturnArticle(article);

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
            tovar.Add(availability);

            return tovar;
        }

        private string ReturnArticle(string article)
        {
            foreach (KeyValuePair<string, string> pair in simbols)
            {
                article = article.Replace(pair.Key, pair.Value);
            }

            bool b = false;
            do
            {
                b = false;
                int lastIndex = article.LastIndexOf('_');
                if (lastIndex == article.Length - 1)
                    b = true;
                if (b)
                    article = article.Remove(article.Length - 1);

            } while (b);
            return article;
        }

        private void UpdateTovar(string searchTovarInBike, string price, CookieDictionary cookie)
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
            text = text.Replace("НАЗВАНИЕ", nameTovar);
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

        private string DiscountCSV()
        {
            string discount = "<p style=\"\"text-align: right;\"\"><span style=\"\"font-weight: bold; font-weight: bold;\"\"> 1. <a href=\"\"https://bike18.ru/oplata-dostavka\"\">Выгодные условия доставки по всей России!</a></span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font-weight: bold; font-weight: bold;\"\"> 2. <a href=\"\"https://bike18.ru/stock\"\">Нашли дешевле!? 110% разницы Ваши!</a></span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font-weight: bold; font-weight: bold;\"\"> 3. <a href=\"\"https://bike18.ru/service\"\">Также обращайтесь в наш сервис центр в Ижевске!</a></span></p>";
            return discount;
        }

        private string DiscountSite()
        {
            string discount = "<p style=\"text-align: right;\"><span style=\"font-weight: bold; font-weight: bold;\"> 1. <a href=\"https://bike18.ru/oplata-dostavka\">Выгодные условия доставки по всей России!</a></span></p><p style=\"text-align: right;\"><span style=\"font-weight: bold; font-weight: bold;\"> 2. <a href=\"https://bike18.ru/stock\">Нашли дешевле!? 110% разницы Ваши!</a></span></p><p style=\"text-align: right;\"><span style=\"font-weight: bold; font-weight: bold;\"> 3. <a href=\"https://bike18.ru/service\">Также обращайтесь в наш сервис центр в Ижевске!</a></span></p>";
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

        private void cbOther_CheckedChanged_1(object sender, EventArgs e)
        {
            if (gpOther.Visible)
                gpOther.Visible = false;
            else
                gpOther.Visible = true;
        }
    }
}
