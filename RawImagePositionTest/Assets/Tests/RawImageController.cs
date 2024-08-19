using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RawImageController : MonoBehaviour, IPointerClickHandler
{
    #region �ܺο��� ������ �̺�Ʈ
    /// <summary>
    /// Raw Image�� Ŭ������ �� ��ǥ�� �����ϴ� �븮��
    /// </summary>
    /// <param name="clickPosition"></param>
    public delegate void RawImageClickDelegate(PointerEventData eventData);

    /// <summary>
    /// Raw Image�� Ŭ���ϸ� �߻��ϴ� �̺�Ʈ
    /// </summary>
    public event RawImageClickDelegate OnRawImageClick;
    /// <summary>
    /// Raw Image�� Ŭ���ϸ� �߻��ϴ� �̺�Ʈ ȣ��
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
        // Ŭ���� ��ġ�� ��ǥ�� �����ɴϴ�.
        // Ŭ���� ��ġ�� ���
        //Vector2 clickPosition = eventData.position;
        //Debug.Log("Ŭ���� ��ǥ: " + clickPosition);
        //this.OnRawImageClickCall(eventData.position);
        //this.OnRawImageClickCall(localPoint);
        this.OnRawImageClickCall(eventData);
    }
}
