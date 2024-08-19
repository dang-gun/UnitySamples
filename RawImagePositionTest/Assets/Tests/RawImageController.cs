using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RawImageController : MonoBehaviour, IPointerClickHandler
{
    #region 외부에서 연결할 이벤트
    /// <summary>
    /// Raw Image를 클릭했을 때 좌표를 전달하는 대리자
    /// </summary>
    /// <param name="clickPosition"></param>
    public delegate void RawImageClickDelegate(PointerEventData eventData);

    /// <summary>
    /// Raw Image를 클릭하면 발생하는 이벤트
    /// </summary>
    public event RawImageClickDelegate OnRawImageClick;
    /// <summary>
    /// Raw Image를 클릭하면 발생하는 이벤트 호출
    /// </summary>
    private void OnRawImageClickCall(PointerEventData eventData)
    {
        if (this.OnRawImageClick != null)
        {
            this.OnRawImageClick(eventData);
        }
    }
    #endregion

    public RectTransform rectTransform { get; private set; }

    private void Awake()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 위치의 좌표를 가져옵니다.
        // 클릭된 위치를 계산
        //Vector2 clickPosition = eventData.position;
        //Debug.Log("클릭된 좌표: " + clickPosition);
        //this.OnRawImageClickCall(eventData.position);
        //this.OnRawImageClickCall(localPoint);
        this.OnRawImageClickCall(eventData);
    }
}
