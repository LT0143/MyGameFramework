using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
#if UNITY_EDITOR
using  UnityEditor;
#endif

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 游戏入口
    /// </summary>
   public static class GameEntry
   {
       private const string UnityGameFrameworkVersion = "3.1.0";

       private static readonly LinkedList<GameFrameworkComponent> s_GameFrameworkComponents =
           new LinkedList<GameFrameworkComponent>(); 

        /// <summary>
        /// 游戏框架所在的场景编号
        /// </summary>
       internal const int GameFrameworkSceneID = 0;

        /// <summary>
        /// 获取unity游戏框架版本号
        /// </summary>
        public static string Version
        {
            get { return UnityGameFrameworkVersion; }
        }

        public static T GetComponent<T>() where T : GameFrameworkComponent
        {
            return (T)GetComponent(typeof(T));
        }

        public static GameFrameworkComponent GetComponent(Type type)
        {
            LinkedListNode<GameFrameworkComponent> current = s_GameFrameworkComponents.First;
            while (current !=null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return null;
        }

        public static void Shutdown(ShutdownType shutdownType)
        {
            Log.Info("Shutdown Game Framework ({0})...",shutdownType.ToString());
            BaseComponent baseComponent = GetComponent<BaseComponent>();
            if (baseComponent != null)
            {
                //baseComponent.
            }
        }
   }
}
