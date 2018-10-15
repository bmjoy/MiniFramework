using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniFramework
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public Canvas Canvas;
        public Camera UICamera;
        public EventSystem EventSystem;
        private string AssetBundlePath;
        private readonly Dictionary<string, UIPanel> UIPanelDict = new Dictionary<string, UIPanel>();
        private readonly Queue<QueueObject> PanelQueue = new Queue<QueueObject>();
        public bool IdleQueue;

        enum OperationType
        {
            Close,
            Open,
        }
        class QueueObject
        {
            public UIPanel UPanel;
            public OperationType Type;
            public object[] paramList;
        }
        public void Start()
        {
            IdleQueue = true;

            AssetBundlePath = Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui";
            GetCanvas();
            GetCamera();
            GetEventSystem();
            GetUI();

            ResLoader.Instance.LoadAssetBundle(this, AssetBundlePath, LoadUIFromAssetBundle);
        }

        private void Update()
        {
            CheckReadyPanels();
        }
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="panelName"></param>
        public void OpenUIByQueue(string panelName, params object[] paramList)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                UIPanel up = UIPanelDict[panelName];
                QueueObject qo = new QueueObject();
                qo.UPanel = up;
                qo.Type = OperationType.Open;
                qo.paramList = paramList;
                PanelQueue.Enqueue(qo);
            }
        }
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="panelName"></param>
        public void CloseUIByQueue(string panelName, params object[] paramList)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                UIPanel up = UIPanelDict[panelName];
                if(up.State == UIPanelState.Open)
                {
                    QueueObject qo = new QueueObject();
                    qo.UPanel = up;
                    qo.Type = OperationType.Close;
                    qo.paramList = paramList;
                    PanelQueue.Enqueue(qo);
                }             
            }
        }
        /// <summary>
        /// 打开所有UI
        /// </summary>
        public void OpenAll()
        {
            foreach (var item in UIPanelDict)
            {
                OpenUIByQueue(item.Key);
            }
        }
        /// <summary>
        /// 关闭所有UI
        /// </summary>
        public void CloseAll()
        {
            foreach (var item in UIPanelDict)
            {
                CloseUIByQueue(item.Key);
            }
        }

        /// <summary>
        /// 销毁UI
        /// </summary>
        /// <param name="panelName"></param>
        public void DestroyUI(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                Destroy(UIPanelDict[panelName].gameObject);
                UIPanelDict.Remove(panelName);
            }
        }
        /// <summary>
        /// 禁用面板交互
        /// </summary>
        /// <param name="panelName"></param>
        public void DisableRayCast(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                UIPanel panel = UIPanelDict[panelName];
                Image[] images = panel.transform.GetComponentsInChildren<Image>();
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].raycastTarget = false;
                }
                Text[] texts = panel.transform.GetComponentsInChildren<Text>();
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].raycastTarget = false;
                }
            }
        }
        /// <summary>
        /// 启用面板交互
        /// </summary>
        /// <param name="panelName"></param>
        public void EnableRayCast(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                UIPanel panel = UIPanelDict[panelName];
                Image[] images = panel.transform.GetComponentsInChildren<Image>();
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].raycastTarget = true;
                }
                Text[] texts = panel.transform.GetComponentsInChildren<Text>();
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].raycastTarget = true;
                }
            }
        }


        /// <summary>
        /// 从AssetBundle中加载UI
        /// </summary>
        /// <param name="ab"></param>
        void LoadUIFromAssetBundle(AssetBundle ab)
        {
            GameObject[] objects = ab.LoadAllAssets<GameObject>();
            for (int i = 0; i < objects.Length; i++)
            {
                GameObject ui = Instantiate(objects[i], Canvas.transform);
                ui.name = objects[i].name;
                UIPanel panel = ui.GetComponent<UIPanel>();
                if (panel != null && !UIPanelDict.ContainsKey(panel.name))
                {
                    UIPanelDict.Add(panel.name, panel);
                }
            }
        }
        /// <summary>
        /// 获取Canvas
        /// </summary>
        void GetCanvas()
        {
            Canvas = transform.GetComponentInChildren<Canvas>();
        }
        /// <summary>
        /// 获取UI摄像机
        /// </summary>
        void GetCamera()
        {
            UICamera = transform.GetComponentInChildren<Camera>();
        }
        /// <summary>
        /// 获取EventSystem
        /// </summary>
        void GetEventSystem()
        {
            EventSystem = transform.GetComponentInChildren<EventSystem>();
        }
        /// <summary>
        /// 获取根路径UI
        /// </summary>
        void GetUI()
        {
            for (int i = 0; i < Canvas.transform.childCount; i++)
            {
                UIPanel panel = Canvas.transform.GetChild(i).GetComponent<UIPanel>();
                if (panel != null && !UIPanelDict.ContainsKey(panel.name))
                {
                    UIPanelDict.Add(panel.name, panel);
                }
            }
        }
        /// <summary>
        /// 检查队列
        /// </summary>
        void CheckReadyPanels()
        {
            if (PanelQueue.Count > 0 && IdleQueue)
            {
                QueueObject qo = PanelQueue.Dequeue();
                switch (qo.Type)
                {
                    case OperationType.Close:
                        qo.UPanel.Close(qo.paramList);
                        break;
                    case OperationType.Open:
                        qo.UPanel.Open(qo.paramList);
                        break;
                }
            }
        }
    }
}
