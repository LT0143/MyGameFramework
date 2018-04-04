using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using GameFramework.Localization;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu(("Game Framework/Base"))]
    public sealed class BaseComponent : GameFrameworkComponent
    {
        private const int DefaultDpi = 96; //default windows dpi 

        private string m_GameVersion = string.Empty;
        private int m_InteralApplictionVersion = 0;
        private float m_GameSpeedBeforePause = 1f;

        [SerializeField] private bool m_EditorResourceMode = true;


        [SerializeField] private Language m_EditorLanguage = Language.Unspecified;

        [SerializeField] private string m_LogHelperTypeName = "UnityGameFramework.Runtime.LogHelper";

        [SerializeField] private string m_ZipHelperTypeName = "UnityGameFramework.Runtime.ZipHelper";

        [SerializeField] private string m_JsonHelperTypeName = "UnityGameFramework.Runtime.JsonHelper";

        [SerializeField] private string m_ProfilerHelperTypeName = "UnityGameFramework.Runtime.ProfilerHelper";

        [SerializeField] private int m_FrameRate = 30;

        [SerializeField] private float m_GameSpeed = 1f;

        [SerializeField] private bool m_RunInBackground = true;

        [SerializeField] private bool m_NeverSleep = true;

        /// <summary>
        /// 获取或设置游戏版本号
        /// </summary>
        public string GameVersion
        {
            get { return m_GameVersion; }
            set { m_GameVersion = value; }
        }

        /// <summary>
        /// 获取或设置应用程序内部版本号
        /// </summary>
        public int InternalApplicatinVersion
        {
            get { return m_InteralApplictionVersion; }
            set { m_InteralApplictionVersion = value; }
        }

        /// <summary>
        /// 获取或设置是否使用编辑器资源模式（仅编辑器内有效）
        /// </summary>
        public bool EditorResourceMode
        {
            get { return m_EditorResourceMode; }
            set { m_EditorResourceMode = value; }
        }

        /// <summary>
        /// 获取或设置是否使用编辑器资源模式（仅编辑器内有效）
        /// </summary>
        public Language EditorLanguage
        {
            get { return m_EditorLanguage; }
            set { m_EditorLanguage = value; }
        }

        /// <summary>
        /// 获取或设置游戏帧率
        /// </summary>
        public int FrameRate
        {
            get { return m_FrameRate; }
            set { Application.targetFrameRate = m_FrameRate = value; }
        }

        /// <summary>
        /// 获取或设置游戏速度
        /// </summary>
        public float GameSpeed
        {
            get { return m_GameSpeed; }
            set { Time.timeScale = m_GameSpeed = (value >= 0 ? value : 0); }
        }

        /// <summary>
        /// 获取游戏是否暂停
        /// </summary>
        public bool IsGamePaused
        {
            get { return m_GameSpeed <= 0f; }
        }

        /// <summary>
        /// 获取是否正常的游戏速度
        /// </summary>
        public bool IsNormalGameSpeed
        {
            get { return m_GameSpeed == 1f; }
        }


        /// <summary>
        /// 获取或设置是否允许后台允许
        /// </summary>
        public bool RunInBackground
        {
            get { return m_RunInBackground; }
            set { Application.runInBackground = m_RunInBackground = value; }
        }

        /// <summary>
        /// 获取或设置是否禁止休眠
        /// </summary>
        public bool NeverSleep
        {
            get { return m_NeverSleep; }
            set
            {
                m_NeverSleep = value;
                Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            InitLogHelper();
            Log.Info("Game Framework version is {0}.Unity Game Framework version is {1}.", GameFrameworkEntry.Version,
                GameEntry.Version);

//#if UNITY_5_3_OR_NEWER || UNITY_5_3
            InitZipHelper();
            InitJsonHelper();
            InitProfilerHelper();
            Utility.Converter.ScreenDpi = Screen.dpi;
            Log.Info(" Screen.dpi : {0}.", Screen.dpi);

            if (Utility.Converter.ScreenDpi <= 0)
                Utility.Converter.ScreenDpi = DefaultDpi;
            m_EditorResourceMode &= Application.isEditor;

            if(m_EditorResourceMode)
                Log.Info("During this run ,Game Framework will use editor resoure files,which you should validate first.");

            Application.targetFrameRate = m_FrameRate;
            Time.timeScale = m_GameSpeed;

            Application.runInBackground = m_RunInBackground;
            Screen.sleepTimeout = m_NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

//#else
            Log.Error("Game Framework only applies with unity 5.3 and above,but current Unity version is{0}.",Application.unityVersion);
            GameEntry.Shutdown(ShutdownType.Quit);
            //#endif
//#if UNITY_5_6_OR_NEWER 
       //     Application.lowMemory += OnlowMemory;
//#endif
        }



        private void InitLogHelper()
        {
            if (string.IsNullOrEmpty(m_LogHelperTypeName))
            {
                return;
            }

            Type logHelperType = Utility.Assembly.GetType(m_LogHelperTypeName);
            if (logHelperType == null)
                throw new GameFrameworkException(string.Format("Can not find log helper type '{0}'.",
                    m_LogHelperTypeName));

            Log.ILogHelper logHelper = (Log.ILogHelper) Activator.CreateInstance(logHelperType);

            if (logHelper == null)
            {
                throw new GameFrameworkException(string.Format("Can not create log helper type '{0}'.",
                    m_LogHelperTypeName));
            }

            Log.SetLogHelper(logHelper);
        }

        private void InitZipHelper()
        {
            if (string.IsNullOrEmpty(m_ZipHelperTypeName))
            {
                return;
            }

            Type zipHelperType = Utility.Assembly.GetType(m_ZipHelperTypeName);
            if (zipHelperType == null)
                throw new GameFrameworkException(string.Format("Can not find log helper type '{0}'.",
                    m_ZipHelperTypeName));

            Utility.Zip.IZipHelper zipHelper = (Utility.Zip.IZipHelper) Activator.CreateInstance(zipHelperType);

            if (zipHelper == null)
            {
                throw new GameFrameworkException(string.Format("Can not create log helper type '{0}'.",
                    m_ZipHelperTypeName));
            }
            Utility.Zip.SetZipHelper(zipHelper);
        }

        private void InitJsonHelper()
        {
            if (string.IsNullOrEmpty(m_JsonHelperTypeName))
            {
                return;
            }

            Type jsonHelperType = Utility.Assembly.GetType(m_JsonHelperTypeName);
            if (jsonHelperType == null)
                throw new GameFrameworkException(string.Format("Can not find log helper type '{0}'.",
                    m_JsonHelperTypeName));

            Utility.Json.IJsonHelper jsonHelper = (Utility.Json.IJsonHelper) Activator.CreateInstance(jsonHelperType);

            if (jsonHelper == null)
            {
                throw new GameFrameworkException(string.Format("Can not create log helper type '{0}'.",
                    m_JsonHelperTypeName));
            }
            Utility.Json.SetJsonHelper(jsonHelper);
        }

        /// <summary>
        /// 初始化性能分析器
        /// </summary>
        private void InitProfilerHelper()
        {
            if (string.IsNullOrEmpty(m_ProfilerHelperTypeName))
            {
                return;
            }

            Type profilerHelperType = Utility.Assembly.GetType(m_ProfilerHelperTypeName);
            if (profilerHelperType == null)
                throw new GameFrameworkException(string.Format("Can not find log helper type '{0}'.",
                    m_ProfilerHelperTypeName));

            Utility.Profiler.IProfilerHelper profilerHelper =
                (Utility.Profiler.IProfilerHelper) Activator.CreateInstance(profilerHelperType);

            if (profilerHelper == null)
            {
                throw new GameFrameworkException(string.Format("Can not create log helper type '{0}'.",
                    m_ProfilerHelperTypeName));
            }
            Utility.Profiler.SetProfilerHelper(profilerHelper);
        }

        /// <summary>
        /// 内存不足时调用
        /// </summary>
        private void OnLowMemory()
        {
            Log.Info("Low memory reported");
            ObjectPoolComponent objectPoolComponent = GameEntry.GetComponent<ObjectPoolComponent>();
            if(objectPoolComponent !=null)
                objectPoolComponent.ReleaseAllUnused();

            
        }
    }
}
