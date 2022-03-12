using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    public ScrollRect svLeft;
    public ScrollRect svRight;

    public Text txtName;
    public Image imgState;

    public Text txtRange;

    //儲存右邊要生成的按鈕
    private List<GameObject> itemList = new List<GameObject>();
    public override void Init()
    {
        //動態創建左側按鈕
        //獲取到服務器列表數據(左側按鈕)
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;

        //得到要循環創建多少左側按鈕
        int num = infoList.Count / 5 + 1;//向下取整 +1代表平均分成num的數量的按鈕(除5是五個一區間)

        //創建左側按鈕
        for (int i = 0; i < num; i++)
        {
            //動態創建預設體對象
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));
            item.transform.SetParent(svLeft.content, false);
            //初始化
            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            //判斷是否超過總數
            if (endIndex > infoList.Count)
            {
                endIndex = infoList.Count;
            }
            serverLeft.InitInfo(beginIndex, endIndex);
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //顯示自己時初始化上一次選擇的服務器 
        int id = LoginMgr.Instance.LoginData.frontSeverID;
        if (id <= 0)
        {
            txtName.text = "";
            imgState.gameObject.SetActive(false);
        }
        else
        {
            //根據上一次登入的服務器 獲取服務器訊息用於介面更新
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "區  " + info.name;

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
        //更新當前選擇
        UpdatePanel(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
    }
    /// <summary>
    /// 用於更新右側選擇區間
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex, int endIndex)
    {
        //更新服務器上方區間文字顯示
        txtRange.text = "服務器 " + beginIndex + "-" + endIndex;
        //刪除之前按鈕
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i]);
        }
        itemList.Clear();
        //創建新的按鈕
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //獲取服務器訊息
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i - 1];
            //動態創建預設體
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(svRight.content, false);
            //根據訊息更新按鈕數據
            ServerRightItem rightItem = serverItem.GetComponent<ServerRightItem>();
            rightItem.InitInfo(nowInfo);

            //創建成功後記錄到列表中
            itemList.Add(serverItem);
        }
    }
}
