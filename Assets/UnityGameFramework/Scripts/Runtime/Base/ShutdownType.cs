using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 关闭游戏框架类型。
    /// </summary>
    public enum ShutdownType
    {
        /// <summary>
        /// 关闭游戏框架类型
        /// </summary>
        None = 0,

        /// <summary>
        /// 关闭游戏框架并重启游戏
        /// </summary>
        Restart,
        
        /// <summary>
        /// 关闭游戏框架并退出游戏
        /// </summary>
        Quit,
    }
}
