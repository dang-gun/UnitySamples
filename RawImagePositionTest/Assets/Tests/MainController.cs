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
    /// 미니맵 카메라
    /// </summary>
    public Camera Camera_MiniMap { get; set; }

    /// <summary>
    /// 미니맵 줌
    /// </summary>
    public float Zoom_Now { get; set; } = -20f;

    void Awake()
    {
        this.Camera_Main = Camera.main;

        //미니맵 카메라 찾기
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
        //미니맵 카메라의 위치를 메인카메라와 일치시킨다.
        this.Camera_MiniMap.transform.position
            = this.Camera_Main.transform.position
                + new Vector3(0, 0, this.Zoom_Now);
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
