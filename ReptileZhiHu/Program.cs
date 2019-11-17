using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace ReptileZhiHu
{
    class Program
    {
        public static string downLoadPath = Environment.CurrentDirectory + "\\img";
        static ConcurrentQueue<string> urlQueue = new ConcurrentQueue<string>();
        static ConcurrentQueue<int> errorQueue = new ConcurrentQueue<int>();
        readonly static object _locker = new object();
        static EventWaitHandle _wh = new AutoResetEvent(false);
        static Thread _worker;

        static void Main(string[] args)
        {
            if (!Directory.Exists(downLoadPath))
            {
                Directory.CreateDirectory(downLoadPath);
            }
            var offsetIds = new List<int> { 0, 20, 40, 60, 80, 100, 120, 140, 160, 180 };
            //创建工作进程
            _worker = new Thread(() => DownLoadPic());
            _worker.Start();
            foreach (var offsetId in offsetIds)
            {
                ReptilePicture(offsetId);
            }

            if (errorQueue.Count > 0)
            {
                int errorOffest = 0;
                bool isSuccess = errorQueue.TryDequeue(out errorOffest);
                if (isSuccess)
                {
                    Console.WriteLine($"异常offest:{errorOffest}");
                }

            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }

        public static void DownLoadPic()
        {
            while (true)
            {
                string url = string.Empty;
                lock (_locker)
                {
                    if (urlQueue.Count > 0)
                    {
                        urlQueue.TryDequeue(out url);
                        if (string.IsNullOrWhiteSpace(url)) return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(url))
                {
                    var client = new RestClient(url);
                    var request = new RestRequest(string.Empty, Method.GET);
                    byte[] bytes = client.DownloadData(request);
                    File.WriteAllBytes(Program.downLoadPath + "\\" + DateTime.Now.Ticks + ".jpg", bytes);
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    Console.WriteLine($"下载成功==>{DateTime.Now}");
                }
                else
                {
                    _wh.WaitOne();
                }
            }
        }
        public static void ReptilePicture(int offset)
        {
            try
            {
                string url = $"https://www.zhihu.com/api/v4/questions/34243513/answers?include=data[*].is_normal,admin_closed_comment,reward_info,is_collapsed,annotation_action,annotation_detail,collapse_reason,is_sticky,collapsed_by,suggest_edit,comment_count,can_comment,content,editable_content,voteup_count,reshipment_settings,comment_permission,created_time,updated_time,review_info,relevant_info,question,excerpt,relationship.is_authorized,is_author,voting,is_thanked,is_nothelp,is_labeled,is_recognized,paid_info,paid_info_content;data[*].mark_infos[*].url;data[*].author.follower_count,badge[*].topics&offset=${offset}&limit=20&sort_by=default&platform=desktop";
                var client = new RestClient(url);
                client.AddDefaultHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Safari/537.36");
                var request = new RestRequest(string.Empty, Method.GET);
                var res = client.Execute(request);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var entity = JsonConvert.DeserializeObject<ZhJsonEntity>(res.Content);
                    if (entity != null)
                    {
                        var contentArray = entity.data;
                        if (contentArray.Length > 0)
                        {
                            for (var i = 0; i < contentArray.Length; i++)
                            {
                                string content = contentArray[i].content;
                                if (!string.IsNullOrWhiteSpace(content))
                                {
                                    string rule = "img\\s*src=\"https://pic[0-9]{1,}\\.zhimg\\.com/[0-9]{1,}/.+?jpg";
                                    var match = Regex.Matches(content, rule);
                                    foreach (Match item in match)
                                    {
                                        string urlDownload = item.Groups[0].Value;
                                        if (!string.IsNullOrWhiteSpace(urlDownload))
                                        {
                                            urlQueue.Enqueue(urlDownload.Substring(9));

                                        }
                                    }
                                }
                            }
                            Console.WriteLine($"已采集完:{offset}===>{DateTime.Now}");
                            _wh.Set();  // 给工作线程发信号
                        }
                    }
                }
                else
                {
                    errorQueue.Enqueue(offset);
                }
            }
            catch (Exception ex)
            {
                errorQueue.Enqueue(offset);
            }

        }
    }
}
