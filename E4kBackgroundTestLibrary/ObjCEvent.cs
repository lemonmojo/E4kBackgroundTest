using System;

using Foundation;

namespace E4kBackgroundTestLibrary
{
    public static class ObjCMain
    {
        public static void Main() { }
    }

    public class ObjCEvent
    {
        public string Name { get; private set; }

        public ObjCEvent(string name)
        {
            Name = nameof(E4kBackgroundTestLibrary) + "_" + nameof(ObjCEvent) + "_" + name;
        }

        internal void Invoke(object sender, NSObject eventArgs = null)
        {
            NSNotificationCenter.DefaultCenter.PostNotificationName(Name, eventArgs);
        }
    }
}