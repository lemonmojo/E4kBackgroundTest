﻿using System;

using Foundation;

namespace E4kBackgroundTestLibrary
{
	public interface IDownloaderWillStartEventArgs
	{
		string url { get; set; }
		bool cancel { get; set; }
	}

	[Register]
	public class DownloaderWillStartEventArgs : NSObject, IDownloaderWillStartEventArgs
	{
		[Export("url")]
		public string url { get; set; }

		[Export("cancel")]
		public bool cancel { get; set; }
	}

	public interface IDownloaderDidFinishEventArgs
	{
		string url { get; set; }
		bool cancelled { get; set; }
		string errorMessage { get; set; }
		string result { get; set; }
	}

	[Register]
	public class DownloaderDidFinishEventArgs : NSObject, IDownloaderDidFinishEventArgs
	{
		[Export("url")]
		public string url { get; set; }

		[Export("cancelled")]
		public bool cancelled { get; set; }

		[Export("errorMessage")]
		public string errorMessage { get; set; }

		[Export("result")]
		public string result { get; set; }
	}
}