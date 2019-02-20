using UnityEngine;
using UnityEngine.UI;
using MiniFramework;
using System.Collections.Generic;
using System;

public class DownloadExample : MonoBehaviour
{
    public Button BtStart;
    public Button BtPause;
    public GameObject GOTask;
    public List<Task> Tasks;
    private Transform parent;
    public Text Speed;
    public Slider Process;
    // Use this for initialization
    void Start()
    {
        parent = GOTask.transform.parent;

        for (int i = 0; i < Tasks.Count; i++)
        {
            GameObject obj = Instantiate(GOTask, parent);
            obj.transform.SetAsFirstSibling();
            obj.SetActive(true);
            obj.GetComponentInChildren<InputField>().text = Tasks[i].Url;
            Tasks[i].Process = obj.GetComponentInChildren<Slider>();
            Tasks[i].Start = obj.GetComponentInChildren<Button>();
            Tasks[i].Pause = obj.GetComponentsInChildren<Button>()[1];
        }

        BtStart.onClick.AddListener(() =>
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                Tasks[i].Name = DownloaderManager.Instance.AddTask(Tasks[i].Url);
                DownloaderManager.Instance.GetTask(Tasks[i].Name).DownloadSuccessed += DownloadSuccess;

            }
            DownloaderManager.Instance.Download();
        });

        BtPause.onClick.AddListener(() => {
            DownloaderManager.Instance.Pause();
        });
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < DownloaderManager.Instance.GetTaskCount(); i++)
        {
            Tasks[i].Process.value = DownloaderManager.Instance.GetTask(Tasks[i].Name).GetDownloadProcess();
        }
        Speed.text = DownloaderManager.Instance.GetDownloaderSpeed();
        Process.value = DownloaderManager.Instance.GetDownloadProcess();
    }
    void DownloadFailed(string name)
    {        
    }
    void DownloadSuccess(string name)
    {
    }
}
[Serializable]
public class Task
{
    public string Name;
    public string Url;
    public Slider Process;
    public Button Start;
    public Button Pause;
}
