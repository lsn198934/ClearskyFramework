using System;
using UnityEngine;
using System.Collections.Generic;

public class LogUtil
{

#if UNITY_EDITOR
    static bool ENABLE_LOG = true;
    static bool ENABLE_MORE_LOG = true;
    static bool ENABLE_SCREEN_LOG = false;
#else
#if DEBUG_BUILD
    static bool ENABLE_LOG = true;
    static bool ENABLE_MORE_LOG = true;
    static bool ENABLE_SCREEN_LOG = true;
#else 
    static bool ENABLE_LOG = false;
    static bool ENABLE_MORE_LOG = false; 
    static bool ENABLE_SCREEN_LOG = false;
#endif
#endif



    public class LogInfo
    {
        public long Timer;
        public string tag;
        public string body;
        public LogType type;

        public override string ToString()
        {

            if (tag != null)
            {
                return string.Intern(string.Format("[{0}]{1}", tag, body));
            }
            else
            {
                return string.Intern(body);
            }
        }
    }

    static List<LogInfo> logs;
    static object locker;

    //initialize
    static LogUtil()
    {
        logs = new List<LogInfo>();
        locker = new object();
    }

    #region LOG FUNCTIONS
    public static void Log(object content)
    {
        Log(null, content);
    }

    public static void Log(string tag ,object content)
    {
        if (!ENABLE_LOG) return;

        if (ENABLE_MORE_LOG)
            Debug.Log(logInternal(content, tag, LogType.Log));
        else
            Debug.Log(content.ToString());

    }

    public static void LogWarning(object content)
    {
        LogWarning(null, content);
    }

    public static void LogWarning(string tag, object content)
    {
        if (!ENABLE_LOG) return;

        if (ENABLE_MORE_LOG)
            Debug.LogWarning(logInternal(content, tag, LogType.Warning));
        else
            Debug.LogWarning(content.ToString());
    }


    public static void LogError(object content)
    {
        LogError(null, content);
    }
    public static void LogError(string tag, object content)
    {
        if (!ENABLE_LOG) return;

        if (ENABLE_MORE_LOG)
            Debug.LogError(logInternal(content, tag, LogType.Error));
        else
            Debug.LogError(content.ToString());
    }



    private static LogInfo logInternal(object content, string tag = null, LogType type = LogType.Log)
    {
        lock (locker) 
        { 
            LogInfo log = new LogInfo()
            {
                Timer = System.DateTime.Now.Ticks,
                tag = tag,
                type = type
            };


            if (content == null)
            {
                log.body = String.Intern("null");
            }
            else if (content is Exception)
            {
                log.body = StackTraceUtility.ExtractStringFromException(content as Exception);
            }
            else
            {
                log.body = content.ToString();
            }
        
            logs.Add(log);

            return log;
        }
    }

    #endregion


    #region SCREEN LOG



    #endregion
}

