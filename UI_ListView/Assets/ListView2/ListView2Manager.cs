using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



/// <summary>
/// 리스트 뷰2 메니저
/// </summary>
public class ListView2Manager : ItemListUiInterface
{
    /// <summary>
    /// 관리중인 데이터와 UI 리스트
    /// </summary>
    public List<ListView1DataModel> ItemList = new List<ListView1DataModel>();

    public ListView2Controller ListView2Cont = null;

    public ListView2Manager(ListView2Controller contListView2)
    {
        this.ListView2Cont = contListView2;

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
                .Instance_New(ListView2Cont.ContentGo.transform);

        //컨트롤러 저장
        newData.ItemCont = goNew.GetComponent<ItemObjectController>();
        newData.ItemCont.DataSetting(newData.ItemData);
        newData.ItemCont.OnItemClick 
            += (dataItem) => 
            {
                //임시로 클릭하면 지우도록 구현한다.
                this.RemoveItem(dataItem);
            };



        //리스트에 저장
        this.ItemList.Add(newData);
    }

    public void RemoveItem(ItemDataModel dataItem)
    {
        //데이터 개체 검색
        ListView1DataModel findData
            = this.ItemList
                .Where(w => w.ItemData == dataItem)
                .FirstOrDefault();

        if (null != findData) 
        {//검색된 데이터가 있다.

            this.RemoveItem(findData);
        }
    }

    /// <summary>
    /// 전달된 뷰데이터를 지운다.
    /// </summary>
    /// <param name="viewdataItem"></param>
    public void RemoveItem(ListView1DataModel viewdataItem)
    {
        GlobalStatic.MainCont.Instance_Destroy(viewdataItem.ItemCont.gameObject);
        this.ItemList.Remove(viewdataItem);
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

    

    
}
