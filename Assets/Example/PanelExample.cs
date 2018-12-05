using UnityEngine;
using MiniFramework;
public class PanelExample : UIPanel {

	// Use this for initialization
	public void Start () {
        //UIManager.Instance.CloseUI(name).OpenUI(name);
	}
    public override void Open(params object[] paramList)
    {
        gameObject.SetActive(true);
        UIManager.Instance.IdleQueue = false;
        transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-1000, 00, 0);
        //transform.GetComponent<RectTransform>().DOAnchorPosX(0, 1f).OnComplete(() =>
        //{
        //    UIManager.Instance.IdleQueue = true;
        //});
        SetLayerToTop();
    }
    public override void Close(params object[] paramList)
    {
        UIManager.Instance.IdleQueue = false;
        transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(000, 00, 0);
        //transform.GetComponent<RectTransform>().DOAnchorPosX(-1000f, 1f).SetEase(Ease.Linear).OnComplete(() => 
        //{
        //    gameObject.SetActive(false);
        //    UIManager.Instance.IdleQueue = true;
        //});
        SetLayerToButtom();
    }
}
