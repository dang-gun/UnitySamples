using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// https://github.com/PimDeWitte/UnityMainThreadDispatcher/blob/master/UnityMainThreadDispatcher.cs
/// </summary>
public class AsyncUiUpdateTest : MonoBehaviour
{

    /// <summary>
    /// 동기 UI
    /// </summary>
    public Text txtNormal;
    /// <summary>
    /// 비동기 UI
    /// </summary>
    public Text txtAsync;

    /// <summary>
    /// 비동기 매인 쓰래드 UI
    /// </summary>
    public Text txtAsyncMain;

    /// <summary>
    /// UnityMainThreadDispatcher 이용
    /// </summary>
    public Text txtUnityMainThreadDispatcher;


    /// <summary>
    /// 동작용 버튼
    /// </summary>
    public Button btnGo;

    /// <summary>
    /// 액션을 담아둘 큐
    /// </summary>
    private Queue<Action> m_queueAction = new Queue<Action>();


    // Start is called before the first frame update
    void Start()
    {
		this.txtNormal
            = GameObject.Find("txtNormal")
				.GetComponent<Text>();

        this.txtAsync
            = GameObject.Find("txtAsync")
                .GetComponent<Text>();

        this.txtAsyncMain
            = GameObject.Find("txtAsyncMain")
                .GetComponent<Text>();

        this.txtUnityMainThreadDispatcher
            = GameObject.Find("txtUnityMainThreadDispatcher")
                .GetComponent<Text>();

        this.btnGo
            = GameObject.Find("btnGo")
                .GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        //큐에 액션이 쌓여있으면 동작 시킨다.
        while (m_queueAction.Count > 0)
        {
            m_queueAction.Dequeue().Invoke();
        }
    }

    public void GoCall()
    {
        //일반적인 할당
        this.txtNormal.text = "txtNormal change! 101";


        //메인 쓰레드 아닌 에러를 위한 쓰레드
        Thread thread = new Thread(()=> 
        {
            //큐에 액션을 넣는다.
            m_queueAction.Enqueue(() => 
            {
                this.txtAsyncMain.text = "txtAsyncMain change! 103";
            });

            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                this.txtUnityMainThreadDispatcher.text 
                    = "txtUnityMainThreadDispatcher change! 104";
            });

            //메인 쓰래아니라고 에러
            this.txtAsync.text = "txtAsync change! 102";
        });
        thread.Start();
    }

}
