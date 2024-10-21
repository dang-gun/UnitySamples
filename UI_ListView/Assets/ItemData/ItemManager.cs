using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 데이터(ItemDataModel)만 관리하는 비지니스 모델
/// </summary>
public class ItemManager
{

    /// <summary>
    /// 관리중인 아이템 리스트
    /// </summary>
    private List<ItemDataModel> ItemList
        = new List<ItemDataModel>();

    /// <summary>
    /// 생성시 사용할 카운터
    /// </summary>
    private int AddCount = 0;


    /// <summary>
    /// 전체 리스트를 비운다.
    /// </summary>
    public void ItemList_ClearAll()
    {
        //리스트를 비운다.
        this.ItemList.Clear();
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




        //리스트에 추가 ****
        this.ItemList.Add(newItem);
    }

}
