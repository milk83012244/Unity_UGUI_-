using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    public Button btnSelf;

    public Text txtInfo;

    //選區的區間
    private int beginIndex;
    private int endIndex;
    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //通知選服面板改變右側內容
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdatePanel(beginIndex, endIndex);
        });
    }
    /// <summary>
    /// 初始化左側按鈕內容
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void InitInfo(int beginIndex, int endIndex) 
    {
        //記錄當前按鈕的區間值
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;
        //改變顯示內容
        txtInfo.text = beginIndex + " - " + endIndex + "區";
    }
}
