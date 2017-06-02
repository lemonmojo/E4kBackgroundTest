using System;

using Foundation;

namespace E4kBackgroundTestLibrary
{
    public class ObjCEvent
    {
        public string Name { get; private set; }

        public ObjCEvent(string name)
        {
            Name = nameof(E4kBackgroundTestLibrary) + "_" + nameof(ObjCEvent) + "_" + name;
        }

        internal void Raise(NSObject obj = null)
        {
			NSNotificationCenter.DefaultCenter.PostNotificationName(Name, obj);
        }
    }
}