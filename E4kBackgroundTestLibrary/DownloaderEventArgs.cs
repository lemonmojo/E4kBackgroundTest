using System;

using Foundation;

using EventArgs = Foundation.NSObject;

namespace E4kBackgroundTestLibrary
{
    [Register]
    public class DownloaderInformation : NSObject
    {
        [Export(nameof(url))]
        public string url { get; private set; }

        internal DownloaderInformation(string url)
        {
            this.url = url;
        }
    }

	[Register]
    public class DownloaderWillStartEventArgs : EventArgs
	{
        [Export(nameof(information))]
        public DownloaderInformation information { get; private set; }

        [Export(nameof(cancel))]
		public bool cancel { get; set; }

        internal DownloaderWillStartEventArgs(string url)
        {
            this.information = new DownloaderInformation(url);
        }
	}

	[Register]
	public class DownloaderDidFinishEventArgs : EventArgs
	{
		[Export(nameof(information))]
		public DownloaderInformation information { get; private set; }

        [Export(nameof(cancelled))]
		public bool cancelled { get; private set; }

        [Export(nameof(errorMessage))]
		public string errorMessage { get; private set; }

        [Export(nameof(result))]
		public string result { get; private set; }

		internal DownloaderDidFinishEventArgs(string url, bool cancelled, string errorMessage, string result)
		{
			this.information = new DownloaderInformation(url);
            this.cancelled = cancelled;
            this.errorMessage = errorMessage;
            this.result = result;
		}
	}
}