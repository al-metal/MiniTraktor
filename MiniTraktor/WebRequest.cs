using Bike18;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace web
{

    class WebRequest
    {
        httpRequest httpReq = new httpRequest();
        public string getRequestEncod(string url)
        {
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Proxy = null;
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding(1251));
            String otv = ressr.ReadToEnd();
            res.Close();

            return otv;
        }

        public string getRequest(CookieContainer cookie, string url)
        {
            string otv = null;
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
            //req.Proxy = null;
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
            req.CookieContainer = cookie;

                res = (HttpWebResponse)req.GetResponse();
                StreamReader ressr = new StreamReader(res.GetResponseStream());
                otv = ressr.ReadToEnd();
                res.GetResponseStream().Close();
                req.GetResponse().Close();
                res.Close();

            


            return otv;
        }

        public string getRequest(string url)
        {
            string otv = null;
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Proxy = null;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader ressr = new StreamReader(res.GetResponseStream());
                otv = ressr.ReadToEnd();
                res.GetResponseStream().Close();
                req.GetResponse().Close();
                res.Close();
            }
            catch
            {
                otv = "err";
            }


            return otv;
        }

        public string PostRequest(CookieContainer cookie, string nethouseTovar)
        {
            string otv = null;
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(nethouseTovar);
            req.Proxy = null;
            req.KeepAlive = false;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream());
            otv = ressr.ReadToEnd();
            res.Close();
            ressr.Close();
            return otv;
        }

        public CookieContainer webCookie(string url)
        {
            CookieContainer cooc = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cooc;
            Stream stre = req.GetRequestStream();
            stre.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            return cooc;
        }

        public CookieContainer webCookievk()
        {
            CookieContainer cooc = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://oauth.vk.com/authorize?client_id=5464980&display=popup&redirect_uri=http://api.vk.com/blank.html&scope=market,photos&response_type=token&v=5.52");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cooc;
            Stream stre = req.GetRequestStream();
            stre.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            return cooc;
        }

        internal int price(double priceTovarRacerMotors, double discount)
        {
            priceTovarRacerMotors = priceTovarRacerMotors - (priceTovarRacerMotors * discount);
            priceTovarRacerMotors = Math.Round(priceTovarRacerMotors);
            int price = Convert.ToInt32(priceTovarRacerMotors);
            price = (price / 10) * 10;
            return price;
        }

        internal void loadAltTextImage(CookieContainer cookie, string idImg, string altText)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/images/savealt");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
            req.Method = "POST";
            req.Proxy = null;
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms = Encoding.GetEncoding("utf-8").GetBytes("id=" + idImg + "&alt=" + altText);
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res1 = (HttpWebResponse)req.GetResponse();
            StreamReader ressr1 = new StreamReader(res1.GetResponseStream());
            res1.Close();
            ressr1.Close();
        }

        internal void savePrice(CookieContainer cookie, string urlTovar, MatchCollection articl, double priceTrue, WebRequest webRequest)
        {
            string otv = webRequest.PostRequest(cookie, urlTovar);
            string productId = new Regex("(?<=<section class=\"comment\" id=\").*?(?=\">)").Match(otv).ToString();
            otv = webRequest.PostRequest(cookie, "http://bike18.nethouse.ru/api/catalog/getproduct?id=" + productId);
        }

        internal void DeleteImage(CookieContainer cookie, string productId, string objectId, string type, string name, string desc, string alt, string priority)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://bike18.nethouse.ru/api/images/delete");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms = Encoding.GetEncoding("utf-8").GetBytes("id=" + productId + "&objectId=" + objectId + "&type=" + type + "&name=" + name + "&desc=" + desc + "&alt=" + alt + "&isVisibleOnMain=0&priority=" + priority);
            req.ContentLength = ms.Length;
            Stream stre6 = req.GetRequestStream();
            stre6.Write(ms, 0, ms.Length);
            stre6.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream());
        }

        internal void saveProductAlsoBuy(CookieContainer cookie, List<string> getProduct, List<string> alsoBuy)
        {
            string alsoBuyStr = null;
            foreach (string element in alsoBuy)
            {
                alsoBuyStr += element;
            }
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://bike18.nethouse.ru/api/catalog/saveproduct");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms = System.Text.Encoding.GetEncoding("utf-8").GetBytes("id=" + getProduct[0] + "&slug=" + getProduct[1] + "&categoryId=" + getProduct[2] + "&productGroup=" + getProduct[3] + "&name=" + getProduct[4] + "&serial=" + getProduct[5] + "&serialByUser=" + getProduct[6] + "&desc=" + getProduct[7] + "&descFull=" + getProduct[8] + "&cost=" + getProduct[9] + "&discountCost=" + getProduct[10] + "&seoMetaDesc=" + getProduct[11] + "&seoMetaKeywords=" + getProduct[12] + "&seoTitle=" + getProduct[13] + "&haveDetail=" + getProduct[14] + "&canMakeOrder=" + getProduct[15] + "&balance=100&showOnMain=" + getProduct[16] + "&isVisible=1&hasSale=0&avatar[id]=" + getProduct[17] + "&avatar[objectId]=" + getProduct[18] + "&avatar[timestamp]=" + getProduct[19] + "&avatar[type]=" + getProduct[20] + "&avatar[name]=" + getProduct[21] + "&avatar[desc]=" + getProduct[22] + "&avatar[ext]=" + getProduct[23] + "&avatar[formats][raw]=" + getProduct[24] + "&avatar[formats][W215]=" + getProduct[25] + "&avatar[formats][150x120]=" + getProduct[26] + "&avatar[formats][104x82]=" + getProduct[27] + "&avatar[formatParams][raw][fileSize]=" + getProduct[28] + "&avatar[alt]=" + getProduct[29] + "&avatar[isVisibleOnMain]=" + getProduct[30] + "&avatar[priority]=" + getProduct[31] + "&avatar[url]=" + getProduct[32] + "&avatar[filters][crop][left]=" + getProduct[33] + "&avatar[filters][crop][top]=" + getProduct[34] + "&avatar[filters][crop][right]=" + getProduct[35] + "&avatar[filters][crop][bottom]=" + getProduct[36] + "&customDays=" + getProduct[37] + "&isCustom=" + getProduct[38] + alsoBuyStr + "&alsoBuyLabel=%D0%9F%D0%BE%D1%85%D0%BE%D0%B6%D0%B8%D0%B5%20%D1%82%D0%BE%D0%B2%D0%B0%D1%80%D1%8B%20%D0%B2%20%D0%BD%D0%B0%D1%88%D0%B5%D0%BC%20%D0%BC%D0%B0%D0%B3%D0%B0%D0%B7%D0%B8%D0%BD%D0%B5");
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res1 = (HttpWebResponse)req.GetResponse();
            StreamReader ressr1 = new StreamReader(res1.GetResponseStream());
        }

        internal void fileWriter(string v1, string v2)
        {

            StreamWriter writers = new StreamWriter(v1 + ".txt", true, Encoding.GetEncoding("windows-1251"));
            writers.WriteLine(v2 + "\n");
            writers.Close();
        }

        internal void fileWriterCSV(List<string> newProduct, string nameFile)
        {
            StreamWriter newProductcsv = new StreamWriter(nameFile + ".csv", true, Encoding.GetEncoding("windows-1251"));
            int count = newProduct.Count - 1;
            for (int i = 0; count > i; i++)
            {
                newProductcsv.Write(newProduct[i], Encoding.GetEncoding("windows-1251"));
                newProductcsv.Write(";");
            }
            newProductcsv.Write(newProduct[count], Encoding.GetEncoding("windows-1251"));
            newProductcsv.WriteLine();
            newProductcsv.Close();
        }

        internal void writerCSV(string v, string section1, string section2)
        {
            StreamWriter newProductcsv = new StreamWriter("fullProducts.csv", true, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(v, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.Write(section1, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.Write(section2, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.WriteLine();
            newProductcsv.Close();
        }

        internal void writerCSV(string v1, string v2, string section1, string section2)
        {
            StreamWriter newProductcsv = new StreamWriter("fullProducts.csv", true, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(v1, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.Write(v2, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.Write(section1, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.Write(section2, Encoding.GetEncoding("windows-1251"));
            newProductcsv.Write(";");
            newProductcsv.WriteLine();
            newProductcsv.Close();
        }

        internal void doubleTovar(string v, string dblProduct, int i, int section)
        {

        }

        internal List<string> listTovarImages(CookieContainer cookie, string url)
        {
            WebRequest webRequest = new WebRequest();
            List<string> listTovar = new List<string>();

            if (!url.Contains("nethouse"))
                url = url.Replace("http://bike18.ru/", "http://bike18.nethouse.ru/");

            string otv = httpReq.PostRequest(cookie, url);
            if (otv != null)
            {
                string productId = new Regex("(?<=<section class=\"comment\" id=\").*?(?=\">)").Match(otv).ToString();
                string article = new Regex("(?<=Артикул:)[\\w\\W]*(?=</div><div><div class)").Match(otv).Value.Trim();
                if (article.Length > 11)
                {
                    article = new Regex("(?<=Артикул:)[\\w\\W]*(?=</title>)").Match(otv).ToString().Trim();
                }
                string prodName = new Regex("(?<=<h1>).*(?=</h1>)").Match(otv).Value;
                string price = new Regex("(?<=<span class=\"product-price-data\" data-cost=\").*?(?=\">)").Match(otv).Value;
                string imgId = new Regex("(?<=<div id=\"avatar-).*(?=\")").Match(otv).Value;
                string desc = new Regex("(?<=<div class=\"user-inner\">).*?(?=</div>)").Match(otv).Value;
                string fulldesc = new Regex("(?<=<div id=\"product-full-desc\" data-ng-non-bindable class=\"user-inner\">).*?(?=</div>)").Match(otv).Value.Replace("&nbsp;&nbsp;", " ");
                string seometa = new Regex("(?<=<meta name=\"description\" content=\").*?(?=\" >)").Match(otv).Value;
                string keywords = new Regex("(?<=<meta name=\"keywords\" content=\").*?(?=\" >)").Match(otv).Value;
                string title = new Regex("(?<=<title>).*?(?=</title>)").Match(otv).Value;
                string visible = new Regex("(?<=,\"balance\":).*?(?=,\")").Match(otv).Value;
                string reklama = new Regex("(?<=<div class=\"text\">).*?(?=</div>)").Match(otv).ToString();
                if (reklama == "акция")
                {
                    reklama = "&markers[3]=1";
                }
                if (reklama == "новинка")
                {
                    reklama = "&markers[1]=1";
                }

                otv = webRequest.getRequest(cookie, "https://bike18.nethouse.ru/api/catalog/getproduct?id=" + productId);
                string slug = new Regex("(?<=\",\"slug\":\").*?(?=\")").Match(otv).ToString();
                string discountCoast = new Regex("(?<=discountCost\":\").*?(?=\")").Match(otv).Value;
                string serial = new Regex("(?<=serial\":\").*?(?=\")").Match(otv).Value;
                string categoryId = new Regex("(?<=\",\"categoryId\":\").*?(?=\")").Match(otv).Value;
                string productGroup = new Regex("(?<=\",\"productGroup\":).*?(?=,\")").Match(otv).Value;
                string havenDetail = new Regex("(?<=haveDetail\".).*?(?=,\")").Match(otv).Value;
                string canMakeOrder = new Regex("(?<=canMakeOrder\".).*?(?=,\")").Match(otv).Value;
                canMakeOrder = canMakeOrder.Replace("false", "0");
                canMakeOrder = canMakeOrder.Replace("true", "1");
                //String balance = new Regex("(?<=,\"balance\":).*?(?=,\")").Match(otv).ToString();
                string showOnMain = new Regex("(?<=showOnMain\".).*?(?=,\")").Match(otv).Value;
                string customDays = new Regex("(?<=,\"customDays\":\").*?(?=\")").Match(otv).Value;
                string isCustom = new Regex("(?<=\",\"isCustom\":).*?(?=,)").Match(otv).Value;

                otv = webRequest.getRequest(cookie, "https://bike18.nethouse.ru/api/catalog/productmedia?id=" + productId);
                string objektId = new Regex("(?<=\"objectId\":\").*?(?=\")").Match(otv).Value;

                List<string> imagesTovar = new List<string>();
                MatchCollection imagesStrTovat = new Regex("(?<={\"id\")[\\w\\W]*?(?=jpg\"})").Matches(otv);
                foreach(Match str in imagesStrTovat)
                {
                    if (str.ToString().Contains("type\":\"4\"") || str.ToString().Contains("type\":\"5\""))
                        imagesTovar.Add(str.ToString());
                }


                MatchCollection avatarId = new Regex("(?<=\"id\":\").*?(?=\")").Matches(otv);
                
                MatchCollection timestamp = new Regex("(?<=\"timestamp\":\").*?(?=\")").Matches(otv);
                MatchCollection type = new Regex("(?<=\"type\":\").*?(?=\")").Matches(otv);
                MatchCollection name = new Regex("(?<=\",\"name\":\").*?(?=\")").Matches(otv);
                MatchCollection descimg = new Regex("(?<=\"desc\":\").*?(?=\")").Matches(otv);
                MatchCollection ext = new Regex("(?<=\"ext\":\").*?(?=\")").Matches(otv);
                MatchCollection raw = new Regex("(?<=\"raw\":\").*?(?=\")").Matches(otv);
                MatchCollection W215 = new Regex("(?<=\"W215\":\").*?(?=\")").Matches(otv);
                MatchCollection srimg = new Regex("(?<=\"150x120\":\").*?(?=\")").Matches(otv);
                MatchCollection minimg = new Regex("(?<=\"104x82\":\").*?(?=\")").Matches(otv);
                MatchCollection filesize = new Regex("(?<=\"fileSize\":).*?(?=})").Matches(otv);
                MatchCollection alt = new Regex("(?<=\"alt\":\").*?(?=\")").Matches(otv);
                MatchCollection isvisibleonmain = new Regex("(?<=\"isVisibleOnMain\".).*?(?=,)").Matches(otv);
                string prioriti = new Regex("(?<=\"priority\":\").*?(?=\")").Match(otv).Value;
                MatchCollection avatarurl = new Regex("(?<=\"raw\":\").*?(?=\",\")").Matches(otv);
                string filtersleft = new Regex("(?<=\"left\":).*?(?=,)").Match(otv).Value;
                string filterstop = new Regex("(?<=\"top\":).*?(?=,)").Match(otv).Value;
                string filtersright = new Regex("(?<=\"right\":).*?(?=,)").Match(otv).Value;
                string filtersbottom = new Regex("(?<=\"bottom\":).*?(?=})").Match(otv).Value;

                listTovar.Add(productId);       //0
                listTovar.Add(slug);            //1
                listTovar.Add(categoryId);      //2
                listTovar.Add(productGroup);    //3
                listTovar.Add(prodName);        //4
                listTovar.Add(serial);          //5
                listTovar.Add(article);         //6
                listTovar.Add(desc);            //7
                listTovar.Add(fulldesc);        //8
                listTovar.Add(price);           //9
                listTovar.Add(discountCoast);   //10
                listTovar.Add(seometa);         //11
                listTovar.Add(keywords);        //12
                listTovar.Add(title);           //13
                listTovar.Add(havenDetail);     //14
                listTovar.Add(canMakeOrder);    //15 купить с сайта в 1 клик
                                                //listTovar.Add(balance);
                listTovar.Add(showOnMain);      //16
                listTovar.Add(avatarId[0].ToString());        //17
                listTovar.Add(objektId);        //18
                listTovar.Add(timestamp[0].ToString());       //19
                listTovar.Add(type[0].ToString());            //20
                listTovar.Add(name[0].ToString());            //21
                listTovar.Add(descimg[0].ToString());         //22
                listTovar.Add(ext[0].ToString());             //23
                listTovar.Add(raw[0].ToString());             //24
                listTovar.Add(W215[0].ToString());            //25
                listTovar.Add(srimg[0].ToString());           //26
                listTovar.Add(minimg[0].ToString());          //27
                listTovar.Add(filesize[0].ToString());        //28
                listTovar.Add(alt[0].ToString());             //29
                listTovar.Add(isvisibleonmain[0].ToString()); //30
                listTovar.Add(prioriti);        //31
                listTovar.Add(avatarurl[0].ToString());       //32
                listTovar.Add(filtersleft);     //33
                listTovar.Add(filterstop);      //34
                listTovar.Add(filtersright);    //35
                listTovar.Add(filtersbottom);   //36
                listTovar.Add(customDays);      //37
                listTovar.Add(isCustom);        //38
                listTovar.Add(reklama);         //39

                for(int i = 0; imagesTovar.Count > i; i++)
                {
                    listTovar.Add(avatarId[i].ToString());        //40 54 68 82 96 110 124 138 152 166 180 194 208
                    listTovar.Add(timestamp[i].ToString());       //41
                    listTovar.Add(type[i].ToString());            //42
                    listTovar.Add(name[i].ToString());            //43
                    listTovar.Add(descimg[i].ToString());         //44
                    listTovar.Add(ext[i].ToString());             //45
                    listTovar.Add(raw[i].ToString());             //46
                    listTovar.Add(W215[i].ToString());            //47
                    listTovar.Add(srimg[i].ToString());           //48
                    listTovar.Add(minimg[i].ToString());          //49
                    listTovar.Add(filesize[i].ToString());        //50
                    listTovar.Add(alt[i].ToString());             //51 65 79 93 107 121 135 149 163 177 191 205 219 233
                    listTovar.Add(isvisibleonmain[i].ToString()); //52
                    listTovar.Add(avatarurl[i].ToString());       //53
                }
                
            }
            return listTovar;
        }

        internal List<string> ReturnImagesId(CookieContainer cookie, string url)
        {
            List<string> imagesTovar = new List<string>();

            if (!url.Contains("nethouse"))
                url = url.Replace("http://bike18.ru/", "http://bike18.nethouse.ru/");

            string otv = httpReq.PostRequest(cookie, url);
            if (otv != null)
            {
                string productId = new Regex("(?<=<section class=\"comment\" id=\").*?(?=\">)").Match(otv).ToString();
                
                otv = httpReq.getRequest(cookie, "https://bike18.nethouse.ru/api/catalog/getproduct?id=" + productId);
                
                otv = httpReq.getRequest(cookie, "https://bike18.nethouse.ru/api/catalog/productmedia?id=" + productId);
                string objektId = new Regex("(?<=\"objectId\":\").*?(?=\")").Match(otv).Value;

                MatchCollection imagesStrTovat = new Regex("(?<=\"id\":\").*?(?=filters)").Matches(otv);
                if(imagesStrTovat.Count == 0)
                    imagesStrTovat = new Regex("(?<=\"id\":\").*?(?=})").Matches(otv);
                foreach (Match str in imagesStrTovat)
                {
                    if (str.ToString().Contains("type\":\"4\"") || str.ToString().Contains("type\":\"5\""))
                    {
                        string s = str.ToString();
                        string imageId = new Regex("(?<=:\").*?(?=\",\"objectId)").Match(str.ToString()).ToString();
                        if(imageId == "")
                            imageId = new Regex(".*?(?=\",\"objectId\":\")").Match(str.ToString()).ToString();
                        imagesTovar.Add(imageId);
                    }       
                }
            }
            return imagesTovar;
        }
    }
}
