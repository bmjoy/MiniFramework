using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniFramework
{
    public class UIManagerComponent : MonoSingleton<UIManagerComponent>
    {
        public string UIDownloadPath = Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui";
        public string UIResourecePath = "UI";
        
        private readonly Dictionary<string, GameObject> UIPanelDict = new Dictionary<string, GameObject>();
        private readonly Queue<QueueObject> PanelQueue = new Queue<QueueObject>();

        public Canvas Canvas;
        public Camera UICamera;
        public EventSystem EventSystem;
        public bool IdleQueue;
        enum OperationType
        {
            Close,
            Open,
        }
        class QueueObject
        {
            public GameObject UPanel;
            public OperationType Type;
        }
        public void Start()
        {
            GetCanvas();
            GetCamera();
            GetEventSystem();
            GetDefaultUI();

            ResLoader.Instance.LoadAllAssetBundle(UIDownloadPath, LoadUIFromAssetBundle);
        }

        private void Update()
        {
            CheckReadyPanels();
        }
        /// <summary>
        /// 打开UI(队列)
        /// </summary>
        /// <param name="panelName"></param>
        public void OpenUIByQueue(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                GameObject up = UIPanelDict[panelName];
                QueueObject qo = new QueueObject();
                qo.UPanel = up;
                qo.Type = OperationType.Open;
                PanelQueue.Enqueue(qo);
            }
        }
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="panelName"></param>
        public void OpenUI(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                GameObject up = UIPanelDict[panelName];
                up.SetActive(true);
            }
        }
        /// <summary>
        /// 关闭UI(队列)
        /// </summary>
        /// <param name="panelName"></param>
        public void CloseUIByQueue(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                GameObject up = UIPanelDict[panelName];
                QueueObject qo = new QueueObject();
                qo.UPanel = up;
                qo.Type = OperationType.Close;
                PanelQueue.Enqueue(qo);
            }
        }
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="panelName"></param>
        public void CloseUI(string panelName)
        {
            if (UIPanelDict.ContainsKey(panelName))
            {
                GameObject up = UIPanelDict[panelName];
                up.SetActive(false);
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
                GameObject panel = UIPanelDict[panelName];
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
                GameObject panel = UIPanelDict[panelName];
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
        void LoadUIFromAssetBundle(Object[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                Object ui = Instantiate(objs[i], Canvas.transform);
                ui.name = objs[i].name;
                //UIPanel panel = ui.GetComponent<UIPanel>();
                //if (panel != null && !UIPanelDict.ContainsKey(panel.name))
                //{
                //    UIPanelDict.Add(panel.name, panel);
                //}
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
        void GetDefaultUI()
        {
            for (int i = 0; i < Canvas.transform.childCount; i++)
            {
                GameObject panel = Canvas.transform.GetChild(i).gameObject;
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
                        //qo.UPanel.Close(qo.paramList);
                        break;
                    case OperationType.Open:
                        //qo.UPanel.Open(qo.paramList);
                        break;
                }
            }
        }
    }
}
