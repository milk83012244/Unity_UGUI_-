using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnSure;

    public Text txtInfo;

    public override void Init()
    {
        //初始化按鈕事件監聽
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    /// <summary>
    /// 給外部改變提示內容
    /// </summary>
    /// <param name="info">提示內容</param>
    public void ChangeInfo(string info)
    {
        txtInfo.text = info;
    }
}
