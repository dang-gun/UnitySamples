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
    /// 이 콘트롤러와 연결된 데이터 개체(원본)
    /// </summary>
    private ItemDataModel ItemData_ori = null;
    /// <summary>
    /// 이 콘트롤러와 연결된 데이터 개체
    /// </summary>
    public ItemDataModel ItemData 
    { 
        get
        {
            return this.ItemData_ori;
        }
        set
        { 
            this.ItemData_ori = value;
        }
    }


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

}
