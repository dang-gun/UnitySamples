using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    /// <summary>
    /// 리스트뷰 리스트
    /// </summary>
    public List<ItemListUiInterface> ListViewList = new List<ItemListUiInterface>();

    public GameObject ItemPrefab;

    void Awake()
    {
        GlobalStatic.MainCont = this;

        GlobalStatic.ItemPrefab = this.ItemPrefab;


        //리스트뷰1 메니저 생성
        ListView1Manager ListView1Mgr
            = new ListView1Manager(
                GameObject.Find("Canvas").transform
                            .Find("ListView1")
                            .GetComponent<ListView1Controller>());
        ListViewList.Add(ListView1Mgr);

    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    #region 리스트 뷰 전체 명령 UI
    /// <summary>
    /// 전체 리스트 비우기
    /// </summary>
    public void ListView_Clear()
    {
        for (int i = 0; i < ListViewList.Count; ++i)
        {
            ItemListUiInterface item = ListViewList[i];
            item.Clear();
        }
    }

    /// <summary>
    /// 전체 리스트에 아이템 추가
    /// </summary>
    /// <param name="dataItem"></param>
    public void ListView_AddItem(ItemDataModel dataItem)
    {
        for (int i = 0; i < ListViewList.Count; ++i)
        {
            ItemListUiInterface item = ListViewList[i];
            item.AddItem(dataItem);
        }
    }
    #endregion

    /// <summary>
    /// 저장되있는 프리팹을 복사하여 지정된 부모에게 생성한다.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public GameObject Instance_New(Transform transform)
    {
        return this.Instance_New(GlobalStatic.ItemPrefab, transform);
    }
    /// <summary>
    /// 지정된 프리팹을 지정된 부모에게 생성한다.
    /// </summary>
    /// <param name="goPrefab"></param>
    /// <param name="transform"></param>
    /// <returns></returns>
    public GameObject Instance_New(GameObject goPrefab, Transform transform)
    {
        return Instantiate(goPrefab, transform);
    }
    /// <summary>
    /// 지정된 게임 오브젝트를 파괴한다.
    /// </summary>
    /// <param name="goTarget"></param>
    public void Instance_Destroy(GameObject goTarget)
    {
        Destroy(goTarget);
    }
}
