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
    /// ���� UI
    /// </summary>
    public Text txtNormal;
    /// <summary>
    /// �񵿱� UI
    /// </summary>
    public Text txtAsync;

    /// <summary>
    /// �񵿱� ���� ������ UI
    /// </summary>
    public Text txtAsyncMain;

    /// <summary>
    /// UnityMainThreadDispatcher �̿�
    /// </summary>
    public Text txtUnityMainThreadDispatcher;


    /// <summary>
    /// ���ۿ� ��ư
    /// </summary>
    public Button btnGo;

    /// <summary>
    /// �׼��� ��Ƶ� ť
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
        //ť�� �׼��� �׿������� ���� ��Ų��.
        while (m_queueAction.Count > 0)
        {
            m_queueAction.Dequeue().Invoke();
        }
    }

    public void GoCall()
    {
        //�Ϲ����� �Ҵ�
        this.txtNormal.text = "txtNormal change! 101";


        //���� ������ �ƴ� ������ ���� ������
        Thread thread = new Thread(()=> 
        {
            //ť�� �׼��� �ִ´�.
            m_queueAction.Enqueue(() => 
            {
                this.txtAsyncMain.text = "txtAsyncMain change! 103";
            });

            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                this.txtUnityMainThreadDispatcher.text 
                    = "txtUnityMainThreadDispatcher change! 104";
            });

            //���� �����ƴ϶�� ����
            this.txtAsync.text = "txtAsync change! 102";
        });
        thread.Start();
    }

}
