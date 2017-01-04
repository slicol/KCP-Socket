////////////////////////////////////////////////////////////////////
//                            _ooOoo_                             //
//                           o8888888o                            //
//                           88" . "88                            //
//                           (| ^_^ |)                            //
//                           O\  =  /O                            //
//                        ____/`---'\____                         //
//                      .'  \\|     |//  `.                       //
//                     /  \\|||  :  |||//  \                      //
//                    /  _||||| -:- |||||-  \                     //
//                    |   | \\\  -  /// |   |                     //
//                    | \_|  ''\---/''  |   |                     //
//                    \  .-\__  `-`  ___/-. /                     //
//                  ___`. .'  /--.--\  `. . ___                   //
//                ."" '<  `.___\_<|>_/___.'  >'"".                //
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |               //
//              \  \ `-.   \_ __\ /__ _/   .-` /  /               //
//        ========`-.____`-.___\_____/___.-`____.-'========       //
//                             `=---='                            //
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^      //
//            佛祖保佑       无BUG        不修改                   //
////////////////////////////////////////////////////////////////////
/*
 * 描述：
 * 作者：slicol
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = System.Object;

namespace SGF
{
    public static class DebugerExtension
    {
        [Conditional("EnableLog")]
        public static void Log(this object obj, object message)
        {
            if (!Debuger.EnableLog)
            {
                return;
            }

            Debuger.Log(GetLogTag(obj), (string)message);
        }


        public static void LogError(this object obj, object message)
        {
            Debuger.LogError(GetLogTag(obj), (string)message);
        }


        public static void LogWarning(this object obj, object message)
        {
            Debuger.LogWarning(GetLogTag(obj), (string)message);
        }

        //----------------------------------------------------------------------
        [Conditional("EnableLog")]
        public static void Log(this object obj, string format, params object[] args)
        {
            if (!Debuger.EnableLog)
            {
                return;
            }

            string message = string.Format(format, args);
            Debuger.Log(GetLogTag(obj), message);
        }


        public static void LogError(this object obj, string format, params object[] args)
        {
            string message = string.Format(format, args);
            Debuger.LogError(GetLogTag(obj), message);
        }


        public static void LogWarning(this object obj, string format, params object[] args)
        {
            string message = string.Format(format, args);
            Debuger.LogWarning(GetLogTag(obj), message);
        }

        //----------------------------------------------------------------------
        private static string GetLogTag(object obj)
        {
            FieldInfo fi = obj.GetType().GetField("LOG_TAG");
            if (fi != null)
            {
                return (string) fi.GetValue(obj);
            }

            return obj.GetType().Name;
        }

    }
}
