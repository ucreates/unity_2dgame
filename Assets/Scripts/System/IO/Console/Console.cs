using System.Diagnostics;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace Core.IO
{
    public class Console
    {
        public static void Info([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string methodName = "", params object[] values)
        {
            Write("<color=green>[Info]</color>", file, line, methodName, values);
        }

        public static void Warning([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string methodName = "", params object[] values)
        {
            Write("<color=yellow>[Warning]</color>", file, line, methodName, values);
        }

        public static void Error([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string methodName = "", params object[] values)
        {
            Write("<color=red>[Error]</color>", file, line, methodName, values);
        }

        private static void Write(string category, string file, int line, string methodName, params object[] values)
        {
            var frame = new StackFrame(2, true);
            var methodBase = frame.GetMethod();
            var className = methodBase?.ReflectedType?.FullName ?? "Unknown";
            var log = $"{category}\nclassName::{className}\nmethodName::{methodName}\nfile::{file} on {line}\nvalue::{string.Join(",", values)}\n";
            var ctg = category.ToLower();
            if (ctg.Contains("warning"))
                Debug.LogWarning(log);
            else if (ctg.Contains("error"))
                Debug.LogError(log);
            else
                Debug.Log(log);
        }
    }
}