using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using DG.Tweening;
using System;

public class PanelExample : UIPanel {

	// Use this for initialization
	public void Start () {
        //UIManager.Instance.CloseUI(name).OpenUI(name);
	}
    public override void Open()
    {
        State = UIPanelState.Playing;
        gameObject.SetActive(true);
        transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-1000, 00, 0);
        transform.GetComponent<RectTransform>().DOAnchorPosX(0, 1f).OnComplete(() => {
            Close();
        } );
        SetLayerToTop();
    }
    public override void Close()
    {
        State = UIPanelState.Playing;
        gameObject.SetActive(true);
        transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(000, 00, 0);
        transform.GetComponent<RectTransform>().DOAnchorPosX(-1000f, 1f).OnComplete(() => 
        {
            gameObject.SetActive(false);
            State = UIPanelState.Close;
        });
        SetLayerToButtom();
    }
}
