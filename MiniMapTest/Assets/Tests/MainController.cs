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
    /// RawImage ī�޶�
    /// </summary>
    public Camera Camera_RawImage { get; set; }
    /// <summary>
    /// RawImage ī�޶�
    /// </summary>
    public Camera Camera_RenderTexture { get; set; }
    /// <summary>
    /// Viewport ī�޶�
    /// </summary>
    public Camera Camera_Viewport { get; set; }


    /// <summary>
    /// �̴ϸ� ��
    /// </summary>
    public float Zoom_Now { get; set; } = 0f;

    private void Awake()
    {
        this.Camera_Main = Camera.main;


        //RawImage ī�޶� ã��
        this.Camera_RawImage
            = GameObject.Find("Camera_RawImage")
                        .GetComponent<Camera>();

        //RenderTexture ī�޶� ã��
        this.Camera_RenderTexture
            = GameObject.Find("Camera_RenderTexture")
                        .GetComponent<Camera>();

        //RenderTexture ī�޶� ã��
        this.Camera_Viewport
            = GameObject.Find("Camera_Viewport")
                        .GetComponent<Camera>();
    }

    // Update is called once per frame
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
        Vector3 vectorMain = this.Camera_Main.transform.position
                            + new Vector3(0, 0, this.Zoom_Now);

        //�̴ϸ� ī�޶��� ��ġ�� ����ī�޶�� ��ġ��Ų��.
        this.Camera_RawImage.transform.position = vectorMain;
        this.Camera_RenderTexture.transform.position = vectorMain;
        this.Camera_Viewport.transform.position = vectorMain;

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
