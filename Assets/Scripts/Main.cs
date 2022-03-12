using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //測試
        //TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
        //tipPanel.ChangeInfo("測試");

        UIManager.Instance.ShowPanel<LoginBKPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();

        print(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
