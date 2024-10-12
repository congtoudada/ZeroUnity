/****************************************************
  文件：UIWindow.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月30日 10:27:38
  功能：
*****************************************************/
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ZeroEngine
{
    public abstract class UIWindow : UIBase
    {
        #region Propreties

        //窗口准备就绪委托，参数传递的是自身
        private System.Action<UIWindow> _prepareCallback;

        private bool _bIsCreate = false;

        private GameObject _panel; //窗口实例化资源对象

        private Canvas _canvas; //所属Canvas

        private Canvas[] _childCanvas;

        private GraphicRaycaster _raycaster;

        private GraphicRaycaster[] _childRaycaster;

        public override UIType Type => UIType.Window;
        
        /// <summary>
        /// 窗口位置组件。
        /// </summary>
        public override Transform transform => _panel.transform;
        
        /// <summary>
        /// 窗口矩阵位置组件。
        /// </summary>
        public override RectTransform rectTransform => _panel.transform as RectTransform;
        
        /// <summary>
        /// 窗口的实例资源对象。
        /// </summary>
        public override GameObject gameObject => _panel;
        
        /// <summary>
        /// 窗口名称。
        /// </summary>
        public string WindowName { private set; get; }
        
        /// <summary>
        /// 窗口层级。
        /// </summary>
        public int WindowLayer { private set; get; }
        
        /// <summary>
        /// 资源定位地址。
        /// </summary>
        public string AssetName { private set; get; }
        
        /// <summary>
        /// 是否为全屏窗口。
        /// </summary>
        public virtual bool FullScreen { private set; get; } = false;
        
        /// <summary>
        /// 是内部资源无需AB加载。
        /// </summary>
        public bool FromResources { private set; get; }
        
        /// <summary>
        /// 隐藏窗口到关闭的时间。（相当于隐藏后缓存该窗口多久）
        /// </summary>
        public int HideTimeToClose { get; set; }
        
        /// <summary>
        /// 用于隐藏计时
        /// </summary>
        public int HideTimerId { get; set; }
        
        /// <summary>
        /// 是否加载完毕。
        /// </summary>
        internal bool bIsLoadDone = false;
        
        /// <summary>
        /// 自定义数据。（如果userDatas存在多个，则返回首个）
        /// </summary>
        public System.Object UserData
        {
            get
            {
                if (userDatas != null && userDatas.Length >= 1)
                {
                    return userDatas[0];
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// 自定义数据集。
        /// </summary>
        public System.Object[] UserDatas => userDatas;
        
        /// <summary>
        /// 窗口深度值 (sortingOrder)。
        /// </summary>
        public int Depth
        {
            get
            {
                if (_canvas != null)
                {
                    return _canvas.sortingOrder;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (_canvas != null)
                {
                    if (_canvas.sortingOrder == value)
                    {
                        return;
                    }

                    // 设置父类
                    _canvas.sortingOrder = value;

                    // 设置子类（表现层）
                    int depth = value;
                    for (int i = 0; i < _childCanvas.Length; i++)
                    {
                        var canvas = _childCanvas[i];
                        if (canvas != _canvas)
                        {
                            depth += 5; //注意递增值
                            canvas.sortingOrder = depth;
                        }
                    }

                    // 虚函数
                    if (_bIsCreate)
                    {
                        OnSortDepth(value);
                    }
                }
            }
        }
        
        /// <summary>
        /// 窗口可见性
        /// </summary>
        public bool Visible
        {
            get
            {
                if (_canvas != null)
                {
                    return _canvas.gameObject.layer == UIModule.WINDOW_SHOW_LAYER;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (_canvas != null)
                {
                    int setLayer = value ? UIModule.WINDOW_SHOW_LAYER : UIModule.WINDOW_HIDE_LAYER;
                    if (_canvas.gameObject.layer == setLayer)
                        return;

                    // 显示设置（表现层）
                    _canvas.gameObject.layer = setLayer;
                    for (int i = 0; i < _childCanvas.Length; i++)
                    {
                        _childCanvas[i].gameObject.layer = setLayer;
                    }

                    // 交互设置
                    Interactable = value;

                    // 虚函数
                    if (_bIsCreate)
                    {
                        OnVisibleChanged(value);
                    }
                }
            }
        }
        
        /// <summary>
        /// 窗口交互性
        /// </summary>
        private bool Interactable
        {
            get
            {
                if (_raycaster != null)
                {
                    return _raycaster.enabled;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (_raycaster != null)
                {
                    _raycaster.enabled = value;
                    for (int i = 0; i < _childRaycaster.Length; i++)
                    {
                        _childRaycaster[i].enabled = value;
                    }
                }
            }
        }
        
        #endregion
        
        #region 生命周期函数
    
        /// <summary>
        /// 向子类继续传递
        /// </summary>
        /// <param name="depth"></param>
        protected override void OnSortDepth(int depth)
        {
            //向Widgets传递信号（仅影响逻辑层）
            for (int i = 0; i < ListChild.Count; i++)
            {
                ListChild[i].CallSortDepth(depth);
            }
        }

        protected override void OnVisibleChanged(bool visible)
        {
            //向Widgets传递信号（仅影响逻辑层）
            for (int i = 0; i < ListChild.Count; i++)
            {
                ListChild[i].Visible = visible;
            }
        }

        #endregion
        
        /// <summary>
        /// 窗口初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="layer"></param>
        /// <param name="fullScreen"></param>
        /// <param name="assetName"></param>
        /// <param name="fromResources"></param>
        /// <param name="hideTimeToClose"></param>
        public void Init(string name, int layer, bool fullScreen, string assetName, bool fromResources, int hideTimeToClose)
        {
            WindowName = name;
            WindowLayer = layer;
            FullScreen = fullScreen;
            AssetName = assetName;
            FromResources = fromResources;
            HideTimeToClose = hideTimeToClose;
        }

        /// <summary>
        /// 窗口再次被Push到UI堆栈时调用
        /// </summary>
        /// <param name="prepareCallback"></param>
        /// <param name="userDatas"></param>
        internal void PushInvoke(System.Action<UIWindow> prepareCallback, System.Object[] userDatas)
        {
            base.userDatas = userDatas;
            if (IsPrepare)
            {
                prepareCallback?.Invoke(this);
            }
            else
            {
                _prepareCallback = prepareCallback;
            }
            CancelHideToCloseTimer();
        }
        
        /// <summary>
        /// 异步加载窗口
        /// </summary>
        /// <param name="location"></param>
        /// <param name="prepareCallback"></param>
        /// <param name="isAsync"></param>
        /// <param name="userDatas"></param>
        internal async UniTaskVoid InternalLoad(string location, Action<UIWindow> prepareCallback, bool isAsync, System.Object[] userDatas)
        {
            _prepareCallback = prepareCallback;
            this.userDatas = userDatas;
            if (!FromResources)
            {
                //从资源系统加载
                if (isAsync)
                {
                    var uiInstance = await GameModule.Resource.LoadGameObjectAsync(location, parent: GameModule.UI.UIRoot);
                    Handle_Completed(uiInstance);
                }
                else
                {
                    var uiInstance = GameModule.Resource.LoadGameObject(location, parent: GameModule.UI.UIRoot);
                    Handle_Completed(uiInstance);
                }
            }
            else
            {
                //从Resources加载
                GameObject panel = Object.Instantiate(Resources.Load<GameObject>(location), GameModule.UI.UIRoot);
                Handle_Completed(panel);
            }
        }
        
        /// <summary>
        /// 窗口创建
        /// </summary>
        internal void InternalCreate()
        {
            if (_bIsCreate == false)
            {
                _bIsCreate = true;
                OnScriptGenerator();
                OnCreate();
            }
        }
        
        /// <summary>
        /// 窗口刷新
        /// </summary>
        internal void InternalRefresh()
        {
            OnRefresh();
        }
        
        /// <summary>
        /// 窗口强制刷新
        /// </summary>
        public void ForceRefresh()
        {
            OnRefresh();
        }
        
        /// <summary>
        /// 内部更新
        /// </summary>
        /// <returns></returns>
        internal bool InternalUpdate()
        {
            if (!IsPrepare || !Visible)
            {
                return false;
            }

            List<UIWidget> listNextUpdateChild = null;
            if (ListChild != null && ListChild.Count > 0)
            {
                listNextUpdateChild = m_listUpdateChild;
                var updateListValid = m_updateListValid;
                List<UIWidget> listChild = null;
                if (!updateListValid)  //脏标记，存在update变化
                {
                    if (listNextUpdateChild == null)
                    {
                        listNextUpdateChild = new List<UIWidget>();
                        m_listUpdateChild = listNextUpdateChild;
                    }
                    else
                    {
                        listNextUpdateChild.Clear();
                    }

                    listChild = ListChild;
                }
                else
                {
                    listChild = listNextUpdateChild;
                }

                for (int i = 0; i < listChild.Count; i++)
                {
                    var uiWidget = listChild[i];

                    if (uiWidget == null)
                    {
                        continue;
                    }

                    ZeroProfiler.BeginSample(uiWidget.name);
                    var needValid = uiWidget.InternalUpdate();
                    ZeroProfiler.EndSample();

                    if (!updateListValid && needValid)
                    {
                        listNextUpdateChild.Add(uiWidget);
                    }
                }

                if (!updateListValid)
                {
                    m_updateListValid = true;  //取消脏标记
                }
            }

            ZeroProfiler.BeginSample("OnUpdate");

            bool needUpdate = false;
            //如果子控件都不需要更新就判断自身是否需要更新，如果子控件需要更新，自身一定更新
            if (listNextUpdateChild == null || listNextUpdateChild.Count <= 0)
            {
                HasOverrideUpdate = true;
                OnUpdate(); //记录当前组件是否需要Update
                needUpdate = HasOverrideUpdate;
            }
            else
            {
                OnUpdate();
                needUpdate = true;
            }

            ZeroProfiler.EndSample();

            return needUpdate;
        }
        
        /// <summary>
        /// 内部销毁
        /// </summary>
        internal void InternalDestroy()
        {
            _bIsCreate = false;

            RemoveAllUIEvent();

            for (int i = 0; i < ListChild.Count; i++)
            {
                var uiChild = ListChild[i];
                uiChild.CallDestroy();
                uiChild.OnDestroyWidget();
            }

            // 注销回调函数
            _prepareCallback = null;

            OnDestroy();

            // 销毁面板对象
            if (_panel != null)
            {
                UnityEngine.Object.Destroy(_panel);
                _panel = null;
            }
            CancelHideToCloseTimer();
        }
        
        /// <summary>
        /// 处理资源加载完成回调。
        /// </summary>
        /// <param name="panel">面板资源实例。</param>
        private void Handle_Completed(GameObject panel)
        {
            if (panel == null)
            {
                return;
            }

            bIsLoadDone = true;
            
            panel.name = GetType().Name;
            _panel = panel;
            _panel.transform.localPosition = Vector3.zero;

            // 获取组件
            _canvas = _panel.GetComponent<Canvas>();
            if (_canvas == null)
            {
                throw new Exception($"Not found {nameof(Canvas)} in panel {WindowName}");
            }

            _canvas.overrideSorting = true;
            _canvas.sortingOrder = 0;
            _canvas.sortingLayerName = "Default";

            // 获取组件
            _raycaster = _panel.GetComponent<GraphicRaycaster>();
            _childCanvas = _panel.GetComponentsInChildren<Canvas>(true);
            _childRaycaster = _panel.GetComponentsInChildren<GraphicRaycaster>(true);

            // 通知UI管理器
            IsPrepare = true;
            _prepareCallback?.Invoke(this);
        }
        
        protected virtual void Close()
        {
            GameModule.UI.CloseUI(this.GetType());
        }
        
        internal void CancelHideToCloseTimer()
        {
            if (HideTimerId > 0)
            {
                GameModule.Timer.RemoveTimer(HideTimerId);
                HideTimerId = 0;
            }
        }
    }
}