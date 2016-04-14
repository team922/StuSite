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
        /// </summary>
        /// <returns>网页部分信息</returns>
        public string GetHtml()
        {
            string url = "http://www.ip138.com/ips138.asp";//ip地址查询网站
            string html = "";
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                Stream stream = webRequest.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.Default);
                string all = streamReader.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("您的IP地址是：[");
                int end = all.IndexOf("<br/><br/>", start);

                streamReader.Close();
                stream.Close();

                html = all.Substring(start, end - start);
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
            int start = html.IndexOf("您的IP地址是：[") + 9;
            int end = html.IndexOf("]", start);
            string IP = html.Substring(start, end - start);
            if (IP != "")
            {
                outputIP = IP;
            }
            return outputIP;
        }

        //获取省份
        public string GetAddress()
        {
            string html = GetHtml();
            string outputAddress = "";

            //提取地址信息
            int start = html.IndexOf("来自：") + 3;
            int end = html.Length - 3;
            string address = html.Substring(start, end - start);
            if (address != "")
            {
                outputAddress = address;
            }
            return outputAddress;
        }

        //获取运营商
        public string GetProvider()
        {
            string html = GetHtml();
            string outputProvider = "";

            //提取运营商信息
            int start = html.Length - 2;
            string provider = html.Substring(html.Length - 2, 2);
            if (provider != "")
            {
                outputProvider = provider;
            }
            return outputProvider;
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
