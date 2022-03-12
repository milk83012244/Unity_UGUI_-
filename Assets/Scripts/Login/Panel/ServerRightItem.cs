using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    public Button btnSelf;

    public Image imgNew;
    public Image imgState;

    public Text txtName;

    //當前按鈕代表哪個服務器 之後使用其中的數據
    public ServerInfo nowServerInfo;
    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //記錄當前選擇的服務器ID
            LoginMgr.Instance.LoginData.frontSeverID = nowServerInfo.id;

            //隱藏選服面板
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            //顯示服務器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }
    /// <summary>
    /// 初始化方法 用於更新右側按鈕顯示相關
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ServerInfo info)
    {
        //記錄服務器數據
        nowServerInfo = info;

        //更新按鈕上的服務器名稱
        txtName.text = info.id + "區 " + info.name;
        //是否是新服
        imgNew.gameObject.SetActive(info.isNew);

        //服務器狀態
        imgState.gameObject.SetActive(true);
        //加載圖集
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
        switch (info.state)//代表服務器五種狀態
        {
            case 0://無狀態
                imgState.gameObject.SetActive(false);
                break;
            case 1://流暢
                imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://繁忙
                imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://火爆
                imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 4://維護
                imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
        }
    }


}
