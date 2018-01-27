using System;
using UnityEngine;

public class LogUtil
{
    static bool ENABLE_LOG = true;

    public static void Log(string tag, string content)
    {
        if (!ENABLE_LOG) return;
        Debug.Log(string.Intern("[" + tag + "]" + content));
    }

    public static void LogW(string tag, string content)
    {
        if (!ENABLE_LOG) return;
        Debug.LogWarning(string.Intern("[" + tag + "]" + content));
    }


    public static void LogE(string tag, string content)
    {
        if (!ENABLE_LOG) return;
        Debug.LogError(string.Intern("[" + tag + "]" + content));
    }
}

