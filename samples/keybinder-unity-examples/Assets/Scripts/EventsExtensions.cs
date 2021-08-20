using System;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/JosepeDev/KeyBinder
    internal static class EventsExtensions
    {
        public static void SafeInvoke(this Action e)
        {
            if (e != null)
                e();
        }

        public static void SafeInvoke<T>(this Action<T> e, T args)
        {
            if (e != null)
                e(args);
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> e, T1 arg1, T2 arg2)
        {
            if (e != null)
                e(arg1, arg2);
        }
    }
}
