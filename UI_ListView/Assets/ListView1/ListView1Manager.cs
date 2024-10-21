using System;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 리스트 뷰1 메니저
/// </summary>
public class ListView1Manager : ItemListUiInterface
{
    /// <summary>
    /// 관리중인 데이터와 UI 리스트
    /// </summary>
    public List<ListView1DataModel> ItemList = new List<ListView1DataModel>();

    public ListView1Controller ListView1Cont = null;

    public ListView1Manager(ListView1Controller contListView1)
    {
        this.ListView1Cont = contListView1;

    }

    public void Clear()
    {
        //생성된 개체 파괴
        foreach(ListView1DataModel item in this.ItemList)
        {
            GlobalStatic.MainCont.Instance_Destroy(item.ItemCont.gameObject);
        }

        //리스트 비우기
        this.ItemList.Clear();
    }

    public void AddItem(ItemDataModel dataItem)
    {
        //데이터 생성
        ListView1DataModel newData = new ListView1DataModel();
        
        //데이터 저장
        newData.ItemData = dataItem;

        //UI 생성
        GameObject goNew 
            = GlobalStatic.MainCont
                .Instance_New(ListView1Cont.ContentGo.transform);

        //컨트롤러 저장
        newData.ItemCont = goNew.GetComponent<ItemObjectController>();



        //리스트에 저장
        this.ItemList.Add(newData);
    }

    
}
