using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

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
    }
}
