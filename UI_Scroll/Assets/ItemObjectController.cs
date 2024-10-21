using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ItemObjectController : MonoBehaviour
{
    #region 외부에서 연결할 이벤트
    /// <summary>
    /// 아이템 클릭했을때 이벤트 전달을 위한 대리자
    /// </summary>
    public delegate void ItemClickDelegate(ItemObjectController contThis);
    /// <summary>
    /// 아이템을 클릭했을때 발생할 이벤트
    /// </summary>
    public ItemClickDelegate OnItemClick;
    /// <summary>
    /// 아이템을 클릭했을때 발생할 이벤트 호출
    /// </summary>
    private void OnItemClickCall()
    {
        if (null != OnItemClick)
        {
            this.OnItemClick(this);
        }
    }
    #endregion


    /// <summary>
    /// 전체 영역
    /// </summary>
    public Button Item;
    

    /// <summary>
    /// 제목
    /// </summary>
    public TextMeshProUGUI Title;
    
    /// <summary>
    /// 인포 이미지
    /// </summary>
    public Image InfoImage;
    /// <summary>
    /// 크레딧 이미지
    /// </summary>
    public Image CreditImage;

    

    void Awake()
    {
        //스크립트에서 개체 찾기

        //이 아이템의 메인 버튼
        this.Item = GetComponent<Button>();
        this.Item.onClick.AddListener(OnItemClickCall);

        //제목 텍스트
        this.Title 
            = this.transform.Find("BtnText")
                    .GetComponent<TextMeshProUGUI>();

        //인포 이미지
        this.InfoImage
            = this.transform.Find("InfoImage")
                    .GetComponent<Image>();
        //크레딧 이미지
        this.CreditImage
            = this.transform.Find("CreditImage")
                    .GetComponent<Image>();
    }

}
