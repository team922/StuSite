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
        /// <summary>
        /// 获取网页信息
        /// 采集本地信息（未考虑到发布到服务器的情况，暂未使用）
        /// </summary>
        /// <returns>网页部分信息</returns>
        public string GetHtml()
        {
            string url = "http://ip.chinaz.com/getip.aspx";//ip地址查询网站
            //string url = "http://www.ip138.com/ips138.asp";
            string html = "";
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                Stream stream = webRequest.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.Default);
                //string all = streamReader.ReadToEnd(); //读取网站的数据

                //int start = all.IndexOf("您的IP地址是：[");
                //int end = all.IndexOf("<br/><br/>", start);

                //streamReader.Close();
                //stream.Close();

                //html = all.Substring(start, end - start);
                html = streamReader.ReadToEnd();
            }
            catch
            {

            }
            return html;
        }

        //获取IP
        public string GetIP()
        {
            string html = GetHtml();
            string outputIP = "";

            //提取IP地址
            try
            {
                int start = html.IndexOf("{ip:'") + 5;
                int end = html.IndexOf("',", start);
                string IP = html.Substring(start, end - start);
                outputIP = IP;
            }
            catch
            {

            }
            return outputIP;
        }

        //获取省份
        public string GetAddress()
        {
            string html = GetHtml();
            string outputAddress = "";

            //提取地址信息
            try
            {
                int start = html.IndexOf("address:'") + 9;
                int end = html.IndexOf(" ");
                string address = html.Substring(start, end - start);
                outputAddress = address;
            }
            catch
            {

            }
            return outputAddress;
        }

        //获取运营商
        public string GetProvider()
        {
            string html = GetHtml();
            string outputProvider = "";

            //提取运营商信息
            try
            {
                int start = html.IndexOf(" ") + 1;
                int end = html.IndexOf("'}");

                string provider = html.Substring(start, end - start);
                outputProvider = provider;
            }
            catch
            {

            }
            return outputProvider;
        }
        
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
