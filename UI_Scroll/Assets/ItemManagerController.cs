using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class ItemManagerController : MonoBehaviour
{
    /// <summary>
    /// 아이템으로 사용할 프리팹
    /// </summary>
    public GameObject ItemPrefab;

    /// <summary>
    /// 관리중인 아이템 리스트
    /// </summary>
    private List<ItemObjectController> ItemList
        = new List<ItemObjectController>();

    #region UI 연결용 개체

    /// <summary>
    /// 아이템을 표시할 컨텐츠 영역
    /// </summary>
    public GameObject ListView_Content;


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
        if(null == this.ItemPrefab)
        {//프리팹을 지정하지 않았다.

            //직접 검색
            this.ItemPrefab
                //= Resources.Load<GameObject>("ItemObject");
                = AssetDatabase.LoadAssetAtPath<GameObject>("ItemObject");
        }
        


        //UI 연결 **********
        this.ListView_Content
            = GameObject.Find("List View")
                        .transform.Find("Viewport/Content")
                        .gameObject;

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
        //모든 자식을 찾아 파괴한다.
        foreach (Transform child in this.ListView_Content.transform)
        {
            Destroy(child.gameObject);
        }

        //리스트도 비운다.
        this.ItemList.Clear();
    }

    /// <summary>
    /// 리스트의 끝에 아이템을 하나 생성하여 넣는다.
    /// </summary>
    public void ItemList_AddNew()
    {
        //아이템을 생성한다. ****
        ItemObjectController newItem
            = Instantiate(this.ItemPrefab)
                .GetComponent<ItemObjectController>();

        //아이템 정보 수정 ****
        newItem.Title.text
            = string.Format("Add{0} : {1}"
                            , ++AddCount
                            , UnityEngine.Random.Range(1, 100));

        newItem.CreditImage.color
            = new Color(UnityEngine.Random.Range(0, 1f)
                        , UnityEngine.Random.Range(0, 1f)
                        , UnityEngine.Random.Range(0, 1f));




        //리스트에 추가 ****
        this.ItemList.Add(newItem);

        //UI에 추가
        newItem.gameObject.transform.SetParent(this.ListView_Content.transform);
    }
}
