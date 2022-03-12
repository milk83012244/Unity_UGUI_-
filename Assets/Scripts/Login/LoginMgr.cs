using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance = new LoginMgr();

    public static LoginMgr Instance
    {
        get
        {
            return instance;
        }
    }
    //登入數據
    private LoginData loginData;
    //給外部獲取的唯讀屬性
    public LoginData LoginData
    {
        get
        {
            return loginData;
        }
    }

    //註冊數據
    private RegisterData registerData;
    public RegisterData RegisterData
    {
        get
        {
            return registerData;
        }
    }

    //服務器數據
    private List<ServerInfo> serverData;

    public List<ServerInfo> ServerData 
    {
        get
        {
            return serverData;
        }
    }

    private LoginMgr()
    {
        //通過Json管理器讀取對應數據
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");

        //通過Json管理器讀取讀取註冊數據
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");

        //通過Json管理器讀取讀取服務器數據
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region 登入數據
    public void SaveLoginData()
    {
        //通過Json管理器儲存對應數據
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
    //註冊成功後清除登入面板上的登入數據
    public void ClearLoginData()
    {
        loginData.frontSeverID = 0;
        loginData.autoLogin = false;
        loginData.rememberPw = false;
    }
    #endregion

    #region 註冊數據
    //存儲註冊數據
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "registerData");
    }
    /// <summary>
    /// 註冊方法
    /// </summary>
    /// <param name="userName">帳號</param>
    /// <param name="passWord">密碼</param>
    /// <returns></returns>
    public bool RegisterUser(string userName,string passWord)
    {
        //判斷用戶名是否存在
        if (registerData.registerInfo.ContainsKey(userName))
        {
            return false;
        }
        //註冊新帳號密碼
        registerData.registerInfo.Add(userName, passWord);
        //儲存 註冊成功
        SaveRegisterData();
        return true;
    }
    /// <summary>
    /// 驗證帳號密碼是否合法
    /// </summary>
    /// <param name="userName">帳號</param>
    /// <param name="passWord">密碼</param>
    /// <returns></returns>
    public bool CheckInfo(string userName,string passWord)
    {
        //判斷用戶名是否存在
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //判斷密碼是否正確
            if (registerData.registerInfo[userName] == passWord)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
