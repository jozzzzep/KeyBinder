using System;
using System.Collections.Generic;
using UnityEngine;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/jozzzzep/KeyBinder
    internal static class Extensions
    {
        internal static void SafeInvoke(this Action e)
        {
            if (e != null)
                e();
        }

        internal static void SafeInvoke<T>(this Action<T> e, T args)
        {
            if (e != null)
                e(args);
        }

        internal static void SafeInvoke<T1, T2>(this Action<T1, T2> e, T1 arg1, T2 arg2)
        {
            if (e != null)
                e(arg1, arg2);
        }
        
        internal static T Initialize<T>(string objName)
            where T : Component
        {
            var objects = UnityEngine.Object.FindObjectsOfType<T>();
            if (objects == null || objects.Length == 0)
            {
                var obj = new GameObject(objName);
                var comp = obj.AddComponent<T>();
                return comp;
            }
            else if (objects.Length > 1)
                for (int i = 1; i < objects.Length; i++)
                    UnityEngine.Object.Destroy(objects[i].gameObject);
            return objects[0];
        }
    }
}
