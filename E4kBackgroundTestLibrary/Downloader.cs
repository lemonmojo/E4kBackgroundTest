using System;
using System.Net;

namespace E4kBackgroundTestLibrary
{
    public class Downloader
    {
        public static ObjCEvent WillStartDownload = new ObjCEvent(nameof(WillStartDownload));
        public static ObjCEvent DidFinishDownload = new ObjCEvent(nameof(DidFinishDownload));

        public string URL { get; set; }

        public Downloader()
        {
            URL = string.Empty;
        }

        public Downloader(string url)
        {
            URL = url;
        }

        public void Download()
        {
            string url = URL;

            DownloaderWillStartEventArgs willStartEventArgs = new DownloaderWillStartEventArgs(url: url);

            WillStartDownload.Invoke(this, willStartEventArgs);

            bool cancel = willStartEventArgs.cancel;

            if (cancel) {
                DidFinishDownload.Invoke(this, new DownloaderDidFinishEventArgs(
                    url: url,
                    cancelled: true,
                    errorMessage: null,
                    result: null
                ));

                return;
            }

            WebClient wc = new WebClient();

            wc.DownloadStringCompleted += (sender, e) => {
                DidFinishDownload.Invoke(this, new DownloaderDidFinishEventArgs(
                    url: url,
                    cancelled: e.Cancelled,
                    errorMessage: e.Error != null ? e.Error.Message : null,
                    result: e.Error == null && e.Result != null ? e.Result : null
                ));
            };

            try {
				wc.DownloadStringAsync(new Uri(url));
            } catch (Exception ex) {
                DidFinishDownload.Invoke(this, new DownloaderDidFinishEventArgs(
                    url: url,
                    cancelled: false,
                    errorMessage: ex.Message,
                    result: null
                ));
            }
        }
    }
}
