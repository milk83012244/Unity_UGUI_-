using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnRegister;
    public Button btnSure;

    public InputField inputUN;
    public InputField inputPW;

    public Toggle togPW;
    public Toggle togAuto;
    public override void Init()
    {
        btnRegister.onClick.AddListener(() =>
        {
            //顯示註冊面板
            UIManager.Instance.ShowPanel<RegisterPanel>();

            //隱藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        });

        btnSure.onClick.AddListener(() =>
        {
            //點擊後驗證帳號密碼是否正確

            //判斷輸入的帳號密碼是否合適
            if (inputPW.text.Length <= 6 || inputUN.text.Length <= 6)
            {
                //提示不合法
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("帳號密碼都必須大於6位");
                return;
            }
            //驗證用戶名和密碼是否通過
            if (LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                //登入成功

                //記錄數據
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.passWord = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPw = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();

                //根據服務器訊息來判斷顯示哪個面板
                if (LoginMgr.Instance.LoginData.frontSeverID <= 0)
                {
                    //如果從來沒有選過服務器ID<=0時 直接打開選服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //有選過就打開服務器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //隱藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("帳號或密碼錯誤");
            }
        });

        togPW.onValueChanged.AddListener((isOn) =>
        {
            //記住密碼取消選中 自動登入也取消
            if (!isOn)
            {
                togAuto.isOn = false;
            }
        });

        togAuto.onValueChanged.AddListener((isOn) =>
        {
            //自動登入選中時 記住密碼也要被選中
            if (isOn)
            {
                togPW.isOn = true;
            }
        });
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //顯示自己時根據Data數據更新面板上的內容

        //得到數據
        LoginData loginData = LoginMgr.Instance.LoginData;

        //依照勾選選項初始化面板顯示
        togPW.isOn = loginData.rememberPw;
        togAuto.isOn = loginData.autoLogin;

        //更新默認帳號密碼
        inputUN.text = loginData.userName;
        if (togPW.isOn)
        {
            inputPW.text = loginData.passWord;
        }
        if (togAuto.isOn)
        {
            //自動驗證帳號密碼相關
            if (LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                //根據服務器訊息來判斷顯示哪個面板
                if (LoginMgr.Instance.LoginData.frontSeverID <= 0)
                {
                    //如果從來沒有選過服務器ID<=0時 直接打開選服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //有選過就打開服務器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //隱藏自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("帳號密碼錯誤");
            }
        }
    }
    /// <summary>
    /// 給外部設置帳號密碼輸入框
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    public void SetInfo(string userName, string passWord)
    {
        inputUN.text = userName;
        inputPW.text = passWord;
    }
}
