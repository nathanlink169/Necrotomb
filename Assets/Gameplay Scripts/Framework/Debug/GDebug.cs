using UnityEngine;
using System.Collections;

namespace GameFramework
{
    public static class GDebug
    {   
        //
        // Static Methods
        public static void Log(object message)
        {
            #if DEBUG_LOG_ENABLED
            Debug.Log(message);
            #endif
        }
        
        public static void Log(object message, Object context)
        {
            #if DEBUG_LOG_ENABLED
            Debug.Log(message, context);
            #endif
        }
        /*
        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertion(object message)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogAssertion(message);
            #endif
        }

        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertion(object message, Object context)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogAssertion(message, context);
            #endif
        }

        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertionFormat(Object context, string format, params object[] args)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogAssertionFormat(context, format, args);
            #endif
        }

        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertionFormat(string format, params object[] args)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogAssertionFormat(format, args);
            #endif
        }
        */
        public static void LogError(object message)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogError(message);
            #endif
        }
        
        public static void LogError(object message, Object context)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogError(message, context);
            #endif
        }
        
        public static void LogErrorFormat(string format, params object[] args)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogErrorFormat(format, args);
            #endif
        }
        
        public static void LogErrorFormat(Object context, string format, params object[] args)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogErrorFormat(context, format, args);
            #endif
        }
        
        public static void LogException(System.Exception exception) 
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogException(exception);
            #endif
        }
        
        public static void LogException(System.Exception exception, Object context) 
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogException(exception, context);
            #endif
        }
        
        public static void LogFormat(string format, params object[] args) 
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogFormat(format, args);
            #endif
        }
        
        public static void LogFormat(Object context, string format, params object[] args) 
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogFormat(context, format, args);
            #endif
        }

        public static void LogWarning(object message) 
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogWarning(message);
            #endif
        }
        
        public static void LogWarning(object message, Object context) 
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogWarning(message, context);
            #endif
        }
        
        public static void LogWarningFormat(string format, params object[] args)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogWarningFormat(format, args);
            #endif
        }
        
        public static void LogWarningFormat(Object context, string format, params object[] args)
        {
            #if DEBUG_LOG_ENABLED
            Debug.LogWarningFormat(context, format, args);
            #endif
        }
    }
}
