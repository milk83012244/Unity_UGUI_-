using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnStart;
    public Button btnChange;
    public Button btnBack;

    public Text serverName;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            //返回時取消自動登入
            if (LoginMgr.Instance.LoginData.autoLogin)
            {
                LoginMgr.Instance.LoginData.autoLogin = false;
            }

            UIManager.Instance.ShowPanel<LoginPanel>();
            UIManager.Instance.HidePanel<ServerPanel>();
        });
        btnStart.onClick.AddListener(() =>
        {
            //進入遊戲
            //過場景Canvas不會被移除 所以面板隱藏
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.HidePanel<LoginBKPanel>();
            //儲存數據
            LoginMgr.Instance.SaveLoginData();
            //切換場景
            SceneManager.LoadScene("GameScene");
        });
        btnChange.onClick.AddListener(() =>
        {
            //顯示服務器面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();

            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //顯示時更新當前選擇的服務器名字 通過紀錄的服務器ID來更新內容
        int id = LoginMgr.Instance.LoginData.frontSeverID;
        if (id <= 0)
        {
            serverName.text = "無";
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            serverName.text = info.id + "區 " + info.name;
        }

    }
}

