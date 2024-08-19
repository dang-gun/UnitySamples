using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public Camera Camera_Main { get; set; }
    /// <summary>
    /// �̴ϸ� ī�޶�
    /// </summary>
    public Camera Camera_MiniMap { get; set; }

    /// <summary>
    /// �̴ϸ� ��
    /// </summary>
    public float Zoom_Now { get; set; } = -20f;

    void Awake()
    {
        this.Camera_Main = Camera.main;

        //�̴ϸ� ī�޶� ã��
        this.Camera_MiniMap 
            = GameObject.Find("Camera_MiniMap")
                        .GetComponent<Camera>();
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            this.Zoom_Plus();
        }
        else if (Input.GetKey(KeyCode.KeypadMinus))
        {
            this.Zoom_Minus();
        }
        else if (Input.GetKey(KeyCode.A)
        || Input.GetKey(KeyCode.LeftArrow))
        {//����
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(-0.01f, 0);
        }
        else if (Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.RightArrow))
        {//������
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(0.01f, 0);
        }
        else if (Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.UpArrow))
        {//����
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(0, 0.01f);
        }
        else if (Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.DownArrow))
        {//�Ʒ���
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(0, -0.01f);
        }
    }

    void LateUpdate()
    {
        //�̴ϸ� ī�޶��� ��ġ�� ����ī�޶�� ��ġ��Ų��.
        this.Camera_MiniMap.transform.position
            = this.Camera_Main.transform.position
                + new Vector3(0, 0, this.Zoom_Now);
        //Debug.Log("MinimapCamera : " + transform.position);
    }

    /// <summary>
    /// �̴ϸ� �� 
    /// </summary>
    public void Zoom_Plus()
    {
        this.Zoom_Now = this.Zoom_Now + 0.01f;
    }
    /// <summary>
    /// �̴ϸ� �� 
    /// </summary>
    public void Zoom_Minus()
    {
        this.Zoom_Now = this.Zoom_Now - 0.01f;
    }
}
