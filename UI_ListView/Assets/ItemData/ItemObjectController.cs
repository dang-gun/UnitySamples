using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Net.NetworkInformation;

public class ItemObjectController : MonoBehaviour
{
    #region 외부에서 연결할 이벤트
    /// <summary>
    /// 아이템 클릭했을때 이벤트 전달을 위한 대리자
    /// </summary>
    public delegate void ItemClickDelegate(ItemDataModel contThis);
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
            this.OnItemClick(this.ItemData);
        }
    }
    #endregion

    /// <summary>
    /// 이 콘트롤러와 연결된 데이터 개체
    /// </summary>
    public ItemDataModel ItemData { get; private set; }
    


    /// <summary>
    /// 전체 영역
    /// </summary>
    public Button Item;


    /// <summary>
    /// 제목
    /// </summary>
    public TextMeshProUGUI TitleText;
    
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
        this.TitleText 
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

    /// <summary>
    /// 데이터를 저장하고 UI에 적용한다.
    /// </summary>
    /// <remarks>
    /// 유니티에 라이프 사이클에 따라 'Awake'이전에 호출될 수 있다.<br />
    /// 'Awake'보다 빠르게 호출되는 경우가 생긴다면 'UI에 적용'을 'Awake'에서 하도록 지연시켜야 한다.
    /// </remarks>
    /// <param name="dataItem"></param>
    public void DataSetting(ItemDataModel dataItem)
    {
        this.ItemData = dataItem;

        //UI에 적용
        this.TitleText.text = this.ItemData.Title;
        this.CreditImage.color = this.ItemData.CreditColor;
    }
}
