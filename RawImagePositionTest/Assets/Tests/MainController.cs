using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    /// Raw Image 컨트롤러
    /// </summary>
    public RawImageController RawImageCont { get; set; }
    public RectTransform RawImageRect
    { 
        get
        {
            return this.RawImageCont.rectTransform;
        }
    }

    /// <summary>
    /// 복사할 프리팹
    /// </summary>
    public GameObject TestPrefab1;
    /// <summary>
    /// 복사할 프리팹
    /// </summary>
    public GameObject TestPrefab2;

    /// <summary>
    /// 미니맵 줌
    /// </summary>
    //public float Zoom_Now { get; set; } = 0f;
    public float Zoom_Now { get; set; } = 7.0f;


    /// <summary>
    /// UI 해상도
    /// </summary>
    public Vector2 UI_Resolution { get; set; }
    /// <summary>
    /// 미니맵의 화면상 시작 위치
    /// </summary>
    Vector2 MiniMap_Offset { get; set; }
    /// <summary>
    /// 미니맵의 크기
    /// </summary>
    public Vector2 MiniMap_Size { get; set; }


    public GameObject AddGroup { get; set; }

    void Awake()
    {
        this.Camera_Main = Camera.main;
        

        //미니맵 카메라 찾기
        this.Camera_MiniMap 
            = GameObject.Find("Camera_MiniMap")
                        .GetComponent<Camera>();

        //로이미지 클릭 컨트롤러 찾기
        this.RawImageCont
            = GameObject.Find("RawImage")
                        .GetComponent<RawImageController>();
        this.RawImageCont.OnRawImageClick += RawImageCont_OnRawImageClick;

        RectTransform ui = GameObject.Find("UI").GetComponent<RectTransform>();
        RectTransform miniMap = GameObject.Find("MiniMap").GetComponent<RectTransform>();

        //UI 해상도 저장
        this.UI_Resolution = new Vector2(ui.rect.width, ui.rect.height);
        //미니맵 크기 저장
        this.MiniMap_Size = new Vector2(miniMap.rect.width, miniMap.rect.height);

        //오프셋도 좌하단이 0,0이 되도록 보정한다.
        //anchoredPosition.y는 좌하단이 마이너스이므로 부호보정을 하면 안된다.
        this.MiniMap_Offset = new Vector2(miniMap.anchoredPosition.x
                                        , (miniMap.anchoredPosition.y + this.UI_Resolution.y));
        Debug.Log("MiniMap_Offset " + MiniMap_Offset);


        this.AddGroup = GameObject.Find("AddGroup");
    }

    /// <summary>
    /// 로이미지 클릭 이벤트
    /// </summary>
    /// <param name="clickPosition"></param>
    private void RawImageCont_OnRawImageClick(PointerEventData eventData)
    {
        Debug.Log("클릭위치 " + eventData.position);



        //클릭한 위치 그대로 수동 보정
        Vector3 vecTemp
            = new Vector2(eventData.position.x - this.MiniMap_Offset.x
                            , this.MiniMap_Size.y + (eventData.position.y - this.MiniMap_Offset.y));
        Vector3 worldPosition = this.gameObject.transform.TransformPoint(vecTemp);
        Instantiate(TestPrefab1, worldPosition, Quaternion.identity, this.AddGroup.transform);
        Debug.Log("오브젝트1 " + vecTemp + ", " + worldPosition);



        // 미니맵의 RectTransform을 통해 클릭 위치를 가져옵니다.
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(RawImageRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        //// 미니맵의 클릭 위치를 월드 좌표로 변환합니다.
        //float worldX = (localPoint.x / RawImageRect.rect.width) * (Camera_MiniMap.orthographicSize * 2) - (Camera_MiniMap.orthographicSize);
        //float worldY = (localPoint.y / RawImageRect.rect.height) * (Camera_MiniMap.orthographicSize * 2) - (Camera_MiniMap.orthographicSize);

        //// 카메라의 Z축에 따라 확대/축소를 반영한 최종 월드 좌표를 계산합니다.
        //Vector3 spawnPosition = new Vector3(worldX + Camera_MiniMap.transform.position.x, worldY + Camera_MiniMap.transform.position.y, 0);

        //// 오브젝트를 생성합니다.
        //Instantiate(TestPrefab2, spawnPosition, Quaternion.identity, this.AddGroup.transform);
        //Debug.Log("오브젝트2 " + spawnPosition);

        // 클릭된 위치를 계산
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    this.RawImageCont.rectTransform,
        //    eventData.position,
        //    eventData.pressEventCamera,
        //    out Vector2 localPoint);

        //// Raw Image의 크기를 고려하여 월드 좌표로 변환
        ////Vector3 worldPosition = Camera_Main.ViewportToWorldPoint(new Vector3(
        ////    (localPoint.x / this.RawImageCont.rectTransform.rect.width) * 2 - 1,
        ////    (localPoint.y / this.RawImageCont.rectTransform.rect.height) * 2 - 1,
        ////    Camera_Main.nearClipPlane));

        //// Raw Image의 크기를 고려하여 월드 좌표로 변환
        //Vector3 miniMapPosition = Camera_MiniMap.ViewportToWorldPoint(new Vector3(
        //    (localPoint.x / this.RawImageCont.rectTransform.rect.width),
        //    (localPoint.y / this.RawImageCont.rectTransform.rect.height),
        //    Camera_MiniMap.nearClipPlane));

        //// 메인 카메라의 비율을 고려하여 변환
        //Vector3 mainCameraPosition = Camera_Main.ViewportToWorldPoint(new Vector3(
        //    miniMapPosition.x / Screen.width,
        //    miniMapPosition.y / Screen.height,
        //    Camera_Main.nearClipPlane));

        //Instantiate(this.TestPrefab, new Vector3(mainCameraPosition.x, mainCameraPosition.y, mainCameraPosition.z), Quaternion.identity);


        //Debug.Log("클릭된 좌표2: " + clickPosition);
        //Vector2 vector2 = clickPosition - this.MiniMap_Offset;
        //Debug.Log("클릭된 좌표3: " + vector2);
        //// 화면 좌표를 월드 좌표로 변환
        ////Vector2 mousePosition = Camera.main.ScreenToWorldPoint(clickPosition);
        //// 프리팹 생성
        //Instantiate(this.TestPrefab, vector2, Quaternion.identity);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Delete))
        {
            // 현재 게임 오브젝트의 모든 자식 오브젝트를 삭제
            foreach (Transform child in this.AddGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
        else if (Input.GetKey(KeyCode.KeypadPlus))
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

        // 마우스 왼쪽 버튼 클릭 확인
        if (Input.GetMouseButtonDown(0))
        {
            // 화면 좌표를 월드 좌표로 변환
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 프리팹 생성
            Instantiate(this.TestPrefab2, mousePosition, Quaternion.identity);
            Debug.Log("클릭된 좌표1: " + mousePosition + ", " + Input.mousePosition);
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
