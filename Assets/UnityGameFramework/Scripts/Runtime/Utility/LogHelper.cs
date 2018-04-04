using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace UnityGameFramework.Runtime
{
   internal class LogHelper :Log.ILogHelper
    {
       public void Log(LogLevel level, object message)
       {
           switch (level)
           {
               case LogLevel.Debug:
                   Debug.Log(string.Format("<color=#888888>{0}</color>", message.ToString()));
                   break;
                case LogLevel.Info:
                    Debug.Log( message.ToString());
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(message.ToString());
                    break;
                case LogLevel.Error:
                    Debug.Log(message.ToString());
                    break;
                default:
                    throw new GameFrameworkException(message.ToString());
            }
       }
    }
}
