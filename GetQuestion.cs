using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Beinet.cn.DrivingTest;

namespace DrivingTest
{
    public partial class GetQuestion : Form
    {
        // 抓取考试题目的代码

        readonly string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"jk\");//@"e:\jk\{0}";
        private const string split = "======================";
        private CookieContainer _cookie;

        // 2013年以前的url地址
        //private const string _starturl = "http://wz.jxedt.com/test/201004abcd/201004-sxtest.asp?type=c";
        //private const string _quesurl =
        //    "http://wz.jxedt.com/test/201004abcd/Ask.asp?myTotal=725&ltype=c&myID={0}&myOrder={0}";
        private const string _starturl = "http://wz.jxedt.com/test/2014abcd/sxlx.asp?type=c";
        private const string _quesurl =
            "http://wz.jxedt.com/test/2014abcd/ajax.asp?r=0.555714223572155&index={0}";


        Encoding enc = Encoding.GetEncoding("GB2312");
        private const int quesNum = Form1.quesNum;

        public GetQuestion()
        {
            InitializeComponent();

            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            bool isok;
            // 得到Cookie
            GetPage(_starturl, out isok, ref _cookie, encoding: enc);
            for (int i = 1; i <= quesNum; i++)
            {
                var url = string.Format(_quesurl, i);
                var html = GetPage(url, out isok, ref _cookie, encoding: enc);
                if (!isok)
                {
                    textBox1.Text = html;
                    return;
                }
                //var jsonfile = fileName + i.ToString() + ".txt";
                //using (var sw = new StreamWriter(jsonfile, false, enc))
                //{
                //    sw.Write(html);
                //}
                //continue;
                Question question;
                try
                {
                    question = JsonToObject<Question>(html);
                }
                catch (Exception exp)
                {
                    textBox1.Text = html + "\r\n\r\n" + exp;
                    return;
                }

                var quesimg = string.Empty;             // 问题图片列表

                if (!string.IsNullOrEmpty(question.imageurl))//question.imageurl
                {
                    // 大图
                    //http://ww3.sinaimg.cn/mw600/5eb4d75agw1e2918wh62bj.jpg
                    // 小图
                    //http://ww3.sinaimg.cn/mw240/5eb4d75agw1e2918wh62bj.jpg

                    string bigUrl = "http://ww3.sinaimg.cn/mw600/" + question.sinaimg;
                    // 问题包括图片时，下载
                    var tmp = DownImgs(bigUrl, i);//(question.imageurl);
                    quesimg += tmp;
                }

                string rightAnswer = question.ta;       // 答案序号
                if (rightAnswer.Length > 1)
                {
                    // 用于断点，判断有没有多选题用
                }

                List<string> answerOpn = new List<string>();
                // 答案选项
                answerOpn.Add(question.a);
                answerOpn.Add(question.b);
                if (!string.IsNullOrEmpty(question.c))
                    answerOpn.Add(question.c);
                if (!string.IsNullOrEmpty(question.d))
                    answerOpn.Add(question.d);

                var rightAnswerDesc = question.bestanswer;   // 答案简要说明

                var file = fileName + i.ToString() + ".txt";
                using (var sw = new StreamWriter(file, false, enc))
                {
                    sw.WriteLine(question.question);            // 问题
                    sw.WriteLine(split);
                    sw.WriteLine(quesimg);          // 问题图片列表，以逗号分隔
                    sw.WriteLine(split);
                    sw.WriteLine(rightAnswer);      // 答案序号
                    sw.WriteLine(split);
                    sw.WriteLine(rightAnswerDesc);  // 答案简要说明

                    foreach (var opn in answerOpn)
                    {
                        sw.WriteLine(split);
                        sw.WriteLine(opn);  // 答案选项
                    }
                }
            }
            textBox1.Text = "全部完成";
        }

        string DownImgs(string imeurl, int id)
        {
            string ret = string.Empty;
            var imgname = id.ToString() + imeurl.Substring(imeurl.LastIndexOf('.'));
            var imgfile = fileName + imgname;
            if (!File.Exists(imgfile))
                DownloadFile(imeurl, imgfile, ref _cookie);
            ret += imgname + ",";
            return ret;
        }


        /// <summary>
        /// 抓取页面
        /// </summary>
        /// <param name="url">要抓取的网址</param>
        /// <param name="isok">抓取结果，成功还是失败</param>
        /// <param name="cookieContainer">要使用的cookie</param>
        /// <param name="param">参数</param>
        /// <param name="HttpMethod">GET POST</param>
        /// <param name="encoding">编码格式，默认UTF-8</param>
        /// <param name="showHeader">返回内容是否包括头信息</param>
        /// <param name="userName">网页登录名</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        static string GetPage(string url, out bool isok, ref CookieContainer cookieContainer, 
            string param = null, string HttpMethod = null, 
            Encoding encoding = null,
            bool showHeader = false, string userName = null, string password = null)
        {
            isok = true;
            if (encoding == null)
                encoding = Encoding.UTF8;

            bool isGet = (string.IsNullOrEmpty(HttpMethod) ||
                          HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase));

            #region Get方式，且有参数时，把参数拼接到Url后面
            if (isGet && !string.IsNullOrEmpty(param))
            {
                // 删除网址后面的#号
                if (url.IndexOf('#') >= 0)
                    url = url.Substring(0, url.IndexOf('#'));

                if (url.IndexOf('?') < 0)
                {
                    url += "?" + param;
                }
                else
                {
                    url += "&" + param;
                }
            }
            #endregion

            // 访问Https网站时，加上特殊处理，用于处理证书有问题的网站
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password))
                request.Credentials = new NetworkCredential(userName, password);

            #region 加Cookie
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            //            request.CookieContainer.SetCookies(new Uri(url), "aaa=bbb&ccc=ddd");// 必须一次全部加入Cookie
            #endregion

            //request.Referer = url;
            request.AllowAutoRedirect = false; //禁止自动转向，静态化目标页面发生转向是因为出了异常
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1;)";
            //request.KeepAlive = true;
            //request.Timeout = 100000;   // 设置超时时间，默认值为 100,000 毫秒（100 秒）。 
            //request.Accept = "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, * /*";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Accept = "* /*";
            request.Headers.Add("Accept-Language", "zh-cn");

            if (isGet)
            {
                request.Method = "GET";
                //request.ContentType = "text/html";
            }
            else
            {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                // 设置提交的数据
                if (!string.IsNullOrEmpty(param))
                {
                    request.ContentLength = param.Length;
                    Stream newStream = request.GetRequestStream();
                    // 把数据转换为字节数组
                    byte[] l_data = encoding.GetBytes(param);
                    newStream.Write(l_data, 0, l_data.Length);
                    newStream.Close();
                }
            }

            HttpWebResponse response;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (Exception exp)
            {
                isok = false;
                return "返回错误：" + exp;
            }
            if (response == null)
            {
                return "Response is null";
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream stream;
                using (stream = response.GetResponseStream())
                {
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                    {
                        stream = new GZipStream(stream, CompressionMode.Decompress);
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    {
                        stream = new DeflateStream(stream, CompressionMode.Decompress);
                    }
                    if (stream == null)
                    {
                        return "stream is null";
                    }
                    using (StreamReader sr = new StreamReader(stream, encoding))
                    {
                        string html = sr.ReadToEnd();
                        if (showHeader)
                            html = "请求头信息：\r\n" + request.Headers + "\r\n\r\n响应头信息：\r\n" + response.Headers + "\r\n" + html;
                        return html;
                    }
                }
            }
            return "远程服务器返回代码不为200," +
                   response.StatusCode + "," + response.StatusDescription;
        }
        /// <summary>
        /// 用于访问Https站点时，证书有问题，始终返回true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // Always accept
            //Console.WriteLine("accept" + certificate.GetName());
            return true; //总是接受
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="downloadUrl"></param>
        /// <param name="savePath"></param>
        /// <param name="cookieContainer"></param>
        void DownloadFile(string downloadUrl, string savePath, ref CookieContainer cookieContainer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(downloadUrl);
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookieContainer;
            byte[] buffer = new byte[1000000];
            using (var response = request.GetResponse())
            using (var stream = new FileStream(savePath, 
                FileMode.CreateNew, FileAccess.Write, FileShare.None))
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null)
                {
                    return;
                }
                long s = stream.Length, l = response.ContentLength;
                int read;
                while (s < l && (read = responseStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    stream.Write(buffer, 0, read);
                    stream.Flush();
                    s += read;
                    //var percent = (s*100/(decimal) l).ToString("N");
                    //OperationLabelMethod(labProcess, "下载进度：" + percent + "% " + "点这里取消");
                }
                responseStream.Close();
                //OperationLabelMethod(labProcess, null); // 隐藏进度
            }
        }


        ///// <summary>
        ///// 对象生成Json串
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //static string StringToJson<T>(T source)
        //{
        //    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        jsonSerializer.WriteObject(ms, source);
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(Encoding.UTF8.GetString(ms.ToArray()));

        //        return sb.ToString();
        //    }
        //}

        /// <summary>
        /// Json串生成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        static T JsonToObject<T>(string source)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(source)))
            {
                T obj = (T)jsonSerializer.ReadObject(ms);
                return obj;
            }
        }

        [DataContract]
        class Question
        {
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public int id { get; set; }
            /// <summary>
            /// 问题
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string question { get; set; }
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string a { get; set; }
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string b { get; set; }
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string c { get; set; }
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string d { get; set; }
            /// <summary>
            /// 正确答案
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string ta { get; set; }
            /// <summary>
            /// 题目用到的图片（可能是swf）
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string imageurl { get; set; }
            /// <summary>
            /// 答案解答
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string bestanswer { get; set; }
            /// <summary>
            /// 答案解答网页连接id，"http://tieba.jxedt.com/posts_" + bestanswerid + ".html"
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string bestanswerid { get; set; }
            /// <summary>
            /// 等于1表示判断题，等于2表示4选1，等于3表示4选多，多选题
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string Type { get; set; }
            /// <summary>
            /// 新浪图片
            /// </summary>
            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string sinaimg { get; set; }
// ReSharper restore UnusedMember.Local
// ReSharper restore UnusedAutoPropertyAccessor.Local
        }

    }
}
