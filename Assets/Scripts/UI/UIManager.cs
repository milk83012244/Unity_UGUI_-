using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }
    //存儲面板的容器
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    //得到Canvas對象
    private Transform canvasTrans;

    private UIManager()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        //過場景不刪除
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }

    //顯示面板 使用泛型限制比返回值簡單
    public T ShowPanel<T>() where T:BasePanel
    {
        //根據泛型類型得到面板名 保證泛型T的類型和面板名一致方便使用
        string panelName = typeof(T).Name;
        //已經顯示面板就不用創建直接返回
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }

        //顯示面板 動態創建面板預設體放在父對象裡
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);

        //得到對應的面板腳本
        T panel = panelObj.GetComponent<T>();
        //把面板腳本存到字典中 之後方便獲取
        panelDic.Add(panelName, panel);

        panel.ShowMe();

        return panel;
    }
    //隱藏面板
    //bool=保留淡入淡出效果 false直接刪除面板
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        //根據泛型類型得到面板名 保證泛型T的類型和面板名一致方便使用
        string panelName = typeof(T).Name;
        //判斷顯示面板中有沒有該面板
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(() =>
                {
                    //刪除面板後也要從字典移除
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    //獲得面板
    public T GetPanel<T>() where T : BasePanel
    {
        //根據泛型類型得到面板名 保證泛型T的類型和面板名一致方便使用
        string panelName = typeof(T).Name;
        //判斷顯示面板中有沒有該面板
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
}
