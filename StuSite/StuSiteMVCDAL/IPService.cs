using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace StuSiteMVC.DAL
{
    public class IPService
    {
        public static string apikey = "dbf49ab660abe00f6e358e6e3a5554d4";

        // 获取用户IP地址
        public string GetIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            if (!string.IsNullOrEmpty(ip) && IsIP(ip))
            {
                return ip;
            }
            return "127.0.0.1";
        }

        // 检查IP地址格式
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        //根据IP地址定位地区
        public string GetAddress(string ip)
        {
            string url = "http://apis.baidu.com/apistore/iplookupservice/iplookup";

            string strURL = url + "?ip=" + ip;
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", apikey);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            string address = "";
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                address = strValue + StrDate + "\r\n";
            }
            return address;
        }

        //根据地区获取地区编码
        public string GetAreaid(string district)
        {
            string url = "http://apis.baidu.com/apistore/weatherservice/citylist";

            string strURL = url + "?cityname=" + district;
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", apikey);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            string areaid = "";
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                areaid += strValue;
                areaid += StrDate;
                areaid += "\r\n";
            }
            return areaid;
        }

        //根据地区编码获取天气信息
        public string GetWeather(string id)
        {
            string url = "http://apis.baidu.com/apistore/weatherservice/cityid";

            string strURL = url + "?cityid=" + id;
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", apikey);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            string weather = "";
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                weather += strValue;
                weather += StrDate;
                weather += "\r\n";
            }
            return weather;
        }

        //获取系统时间
        public DateTime GetDateTime()
        {
            //获取本地（服务器）时间
            DateTime dateTime = DateTime.Now;
            return dateTime;
            //获取网络（北京）时间
            //已知BUG：字符串乱码。
            //string url = "http://24timezones.com/zh_shi/beijing_shi_zhong.php";//ip地址查询网站
            //string html = "";
            //try
            //{
            //    WebRequest webRequest = WebRequest.Create(url);
            //    Stream stream = webRequest.GetResponse().GetResponseStream();
            //    StreamReader streamReader = new StreamReader(stream, Encoding.Default);
            //    string all = streamReader.ReadToEnd(); //读取网站的数据

            //    int start = all.IndexOf("<span id=\"currentTime\">") + 23;
            //    int end = all.IndexOf("</span>", start);

            //    streamReader.Close();
            //    stream.Close();

            //    html = all.Substring(start, end - start);
            //}
            //catch
            //{

            //}
            //return html;
        }

        //生成验证码
        public string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random random = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    random = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = random.Next(36);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        //生成图片
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 30);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                //1、画验证码干扰线【背景】
                for (int i = 0; i < 50; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Width);
                    int y2 = random.Next(image.Width);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                //2、画验证码干扰点【前景】
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //3、验证的字符串画到图片
                Font font = new Font("Arial", 15, (FontStyle.Bold));
                LinearGradientBrush burth = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Red, 1.2f, true);
                g.DrawString(validateCode, font, burth, 3, 2);
                //4、画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //5、保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //6、输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
