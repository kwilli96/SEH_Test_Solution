using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SEH_Test_Solution
{
    static class ImageCollector
    {
        static List<string> urls;
        static int CurrentUrl;

        /// <summary>
        /// Sends SearchQuery to google images and returns the html response in string format
        /// </summary>
        /// <param name="SearchQuery"></param>
        /// <returns></returns>
        private static string GetHtmlCode(string SearchQuery)
        {
            string url = "https://www.google.com/search?q=" + SearchQuery + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }
        /// <summary>
        /// Grabs All Image URLs in the html argument
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static List<string> GetUrls(string html)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                ndx++;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
            }
            return urls;

        }
        /// <summary>
        /// Gets image from argument url and returns byte array
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static byte[] GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000000);

                    return bytes;
                }
            }
        }

        /// <summary>
        /// Sends query data to google images and retrieves supplied image urls
        /// </summary>
        /// <param name="SearchQuery"></param>
        public static void SendQuery(string SearchQuery)
        {
            string html = GetHtmlCode(SearchQuery);
            urls = GetUrls(html);
            CurrentUrl = 0;
        }

        /// <summary>
        /// Gets next image available from urls. maintains internal consistency
        /// </summary>
        /// <param name="imageContainer"></param>
        public static void SetNextImage(ImageContainer imageContainer)
        {
            imageContainer.ImageMap = GetImage(urls[CurrentUrl]);
            CurrentUrl++;

            using (MemoryStream ms = new MemoryStream(imageContainer.ImageMap))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                imageContainer.Image = bitmap;
            }
        }
    }
}
