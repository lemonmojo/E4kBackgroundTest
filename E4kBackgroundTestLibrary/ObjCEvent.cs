using System;
using System.Runtime.InteropServices;

using Foundation;

namespace E4kBackgroundTestLibrary
{
    public static class ObjCMain
    {
        public static void Main() { }
    }

    public class ObjCEvent
    {
        public const string KEY_EVENTARGS = "eventArgs";

        public string Name { get; private set; }

        public ObjCEvent(string name)
        {
            Name = nameof(E4kBackgroundTestLibrary) + "_" + nameof(ObjCEvent) + "_" + name;
        }

        internal static long HandleOfObject(object obj)
		{
			GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Weak);
			IntPtr pointer = GCHandle.ToIntPtr(handle);
			long pointerLong = pointer.ToInt64();

            return pointerLong;
		}

        internal static object ObjectFromHandle(long handlePointerLong)
        {
			if (handlePointerLong == 0) {
				return null;
			}

			IntPtr handlePointer = (IntPtr)handlePointerLong;

			if (handlePointer == IntPtr.Zero) {
				return null;
			}

			GCHandle handle = GCHandle.FromIntPtr(handlePointer);

			object target = handle.Target;

			if (target == null) {
				return null;
			}

            return target;
        }

        internal void Invoke(object sender, NSObject eventArgs = null)
        {
            long senderHandle = 0;

            if (sender != null) {
                senderHandle = HandleOfObject(sender);
            }

            NSNumber senderHandleNumber = NSNumber.FromInt64(senderHandle);

            NSDictionary eventArgsDict = null;

            if (eventArgs != null) {
                eventArgsDict = NSDictionary.FromObjectAndKey(eventArgs, (NSString)KEY_EVENTARGS);
            }

            NSNotificationCenter.DefaultCenter.PostNotificationName(Name, senderHandleNumber, eventArgsDict);
        }
    }

	[Register]
	public class NativeObjCEvent : NSObject
	{
		[Export("senderHandleFromNSNotification:")]
		public static long SenderHandleFromNSNotification(NSNotification aNotification)
		{
			NSNumber number = aNotification.Object as NSNumber;

			if (number == null) {
				return 0;
			}

			return number.Int64Value;
		}

		[Export("eventArgsFromNSNotification:")]
		public static NSObject EventArgsFromNSNotification(NSNotification aNotification)
		{
			NSDictionary userInfo = aNotification.UserInfo;

			if (userInfo == null) {
				return null;
			}

			if (!userInfo.ContainsKey((NSString)ObjCEvent.KEY_EVENTARGS)) {
				return null;
			}

			NSObject eventArgs = userInfo[(NSString)ObjCEvent.KEY_EVENTARGS];

			return eventArgs;
		}
	}
}