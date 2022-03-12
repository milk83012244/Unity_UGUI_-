using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{


    private CanvasGroup canvasGroup;
    //淡入淡出速度
    private float alphaSpeed = 10;
    //是否隱藏的標示
    private bool isShow;
    //自己淡出隱藏成功時要執行的委託函數
    private UnityAction hideMeCallBack;

    protected virtual void Awake()
    {
        //檢查是否有CanvasGroup組件
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup==null)
        {
            this.gameObject.AddComponent<CanvasGroup>();
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }
    /// <summary>
    /// 主要提供給子類用於初始化按鈕事件監聽等等內容
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 顯示自己
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;

    }
    /// <summary>
    /// 隱藏自己
    /// </summary>
    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        //淡出成功後傳入函數
        hideMeCallBack += callBack;
    }

    // Update is called once per frame
    void Update()
    {
        //淡入效果
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出效果
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha = 0;
                //執行委託函數
                hideMeCallBack?.Invoke();
            }
        }
    }
}
