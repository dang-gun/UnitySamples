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
    /// RawImage 카메라
    /// </summary>
    public Camera Camera_RawImage { get; set; }
    /// <summary>
    /// RawImage 카메라
    /// </summary>
    public Camera Camera_RenderTexture { get; set; }
    /// <summary>
    /// Viewport 카메라
    /// </summary>
    public Camera Camera_Viewport { get; set; }


    /// <summary>
    /// 미니맵 줌
    /// </summary>
    public float Zoom_Now { get; set; } = 0f;

    private void Awake()
    {
        this.Camera_Main = Camera.main;


        //RawImage 카메라 찾기
        this.Camera_RawImage
            = GameObject.Find("Camera_RawImage")
                        .GetComponent<Camera>();

        //RenderTexture 카메라 찾기
        this.Camera_RenderTexture
            = GameObject.Find("Camera_RenderTexture")
                        .GetComponent<Camera>();

        //RenderTexture 카메라 찾기
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
        {//왼쪽
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(-0.01f, 0);
        }
        else if (Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.RightArrow))
        {//오른쪽
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(0.01f, 0);
        }
        else if (Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.UpArrow))
        {//위쪽
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(0, 0.01f);
        }
        else if (Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.DownArrow))
        {//아래쪽
            this.Camera_Main.transform.position
                = this.Camera_Main.transform.position + new Vector3(0, -0.01f);
        }
    }

    void LateUpdate()
    {
        Vector3 vectorMain = this.Camera_Main.transform.position
                            + new Vector3(0, 0, this.Zoom_Now);

        //미니맵 카메라의 위치를 메인카메라와 일치시킨다.
        this.Camera_RawImage.transform.position = vectorMain;
        this.Camera_RenderTexture.transform.position = vectorMain;
        this.Camera_Viewport.transform.position = vectorMain;

        //Debug.Log("MinimapCamera : " + transform.position);
    }

    /// <summary>
    /// 미니맵 줌 
    /// </summary>
    public void Zoom_Plus()
    {
        this.Zoom_Now = this.Zoom_Now + 0.01f;
    }
    /// <summary>
    /// 미니맵 줌 
    /// </summary>
    public void Zoom_Minus()
    {
        this.Zoom_Now = this.Zoom_Now - 0.01f;
    }
}
