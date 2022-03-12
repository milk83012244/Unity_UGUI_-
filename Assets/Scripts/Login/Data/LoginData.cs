using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 登入介面相關數據
/// </summary>
public class LoginData
{
    public string userName;
    public string passWord;

    public bool rememberPw;
    public bool autoLogin;

    //服務器相關
    public int frontSeverID = -1; //-1代表沒有選擇過服務器
}
