using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameFramework.Resource;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源组件
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Resource")]
    public sealed partial class ResourceComponent : GameFrameworkComponent
    {
        private IResourceManager m_ResourceManager = null;
        private EventComponent m_EventComponent = null;
        private bool m_EditorResourceMode = false; 
        private bool m_ForceUnloadUnusedAssets = false;  //强力卸载未使用的资源
        private bool m_PreorderUnloadUnusedAssets = false;  //预订卸载未使用的资源
        private bool m_PreformGCCollect = false;  //执行GC
        private AsyncOperation m_AsyncOpreation = null;
        private float m_LastOperationElapse = 0f; //最后操作流逝
        private ResourceHelperBase m_ResourceBase = null;

        [SerializeField]
        private ResourceMode m_ResourceMode = ResourceMode.Package;

        [SerializeField]
        private ReadWritePathType m_ = ReadWritePathType.Unspecified;



    }
}
