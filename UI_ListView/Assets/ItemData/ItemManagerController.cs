using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 관리를 위한 매니저
/// </summary>
/// <remarks>
/// 아이템 리스트를 관리하는 것이 아니라 아이템의 생성/삭제와 같은 관리를 한다.
/// </remarks>
public class ItemManagerController : MonoBehaviour
{

    #region UI 연결용 개체

    /// <summary>
    /// 리스트 전체 비우기 버튼
    /// </summary>
    public Button ClearAllBtn;

    /// <summary>
    /// 리스트 끝에 아이템 추가하는 버튼
    /// </summary>
    public Button AddBtn;
    #endregion

    /// <summary>
    /// 생성시 사용할 카운터
    /// </summary>
    private int AddCount = 0;

    void Awake()
    {
        

        //UI 연결 **********

        this.ClearAllBtn = this.transform.Find("ClearAllBtn")
                                .GetComponent<Button>();
        this.ClearAllBtn.onClick.AddListener(() => { this.ItemList_ClearAll(); });

        this.AddBtn = this.transform.Find("AddBtn")
                            .GetComponent<Button>();
        this.AddBtn.onClick.AddListener(() => { this.ItemList_AddNew(); });

    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// 전체 리스트를 비운다.
    /// </summary>
    public void ItemList_ClearAll()
    {
        GlobalStatic.MainCont.ListView_Clear();
    }

    /// <summary>
    /// 리스트의 끝에 아이템을 하나 생성하여 넣는다.
    /// </summary>
    public void ItemList_AddNew()
    {
        //프리팹을 복사하여 아이템을 생성한다. ****
        ItemDataModel newItem = new ItemDataModel();


        //아이템 정보 수정 ****
        newItem.Title
            = string.Format("Add{0} : {1}"
                            , ++AddCount
                            , UnityEngine.Random.Range(1, 100));

        newItem.CreditColor
            = new Color(UnityEngine.Random.Range(0, 1f)
                        , UnityEngine.Random.Range(0, 1f)
                        , UnityEngine.Random.Range(0, 1f));




        //UI에 추가
        GlobalStatic.MainCont.ListView_AddItem(newItem);
    }
}
