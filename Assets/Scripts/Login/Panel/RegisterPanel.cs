using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnSure;
    public Button btnCancel;

    public InputField inputUN;
    public InputField inputPW;
    public override void Init()
    {
        btnCancel.onClick.AddListener(() =>
        {   
            //隱藏自己
            UIManager.Instance.HidePanel<RegisterPanel>();
            //顯示登入面板
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        btnSure.onClick.AddListener(() =>
        {
            //判斷輸入的帳號密碼是否合適
            if (inputPW.text.Length <= 6 || inputUN.text.Length <= 6)
            {
                //提示不合法
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("帳號密碼都必須大於6位");
                return;
            }
            //註冊帳號密碼
            if (LoginMgr.Instance.RegisterUser(inputUN.text,inputPW.text))
            {
                //清理登入面板上的登入數據
                LoginMgr.Instance.ClearLoginData();

                //註冊成功
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                //更新登入面板上的帳號密碼
                loginPanel.SetInfo(inputUN.text, inputPW.text);

                //隱藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();             
            }
            else
            {
                //提示
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用戶名已存在");

                inputUN.text = "";
                inputPW.text = "";
            }

        });
    }
}
