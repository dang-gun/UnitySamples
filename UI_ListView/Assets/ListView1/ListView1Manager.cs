using System;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// ����Ʈ ��1 �޴���
/// </summary>
public class ListView1Manager : ItemListUiInterface
{
    /// <summary>
    /// �������� �����Ϳ� UI ����Ʈ
    /// </summary>
    public List<ListView1DataModel> ItemList = new List<ListView1DataModel>();

    public ListView1Controller ListView1Cont = null;

    public ListView1Manager(ListView1Controller contListView1)
    {
        this.ListView1Cont = contListView1;

    }

    public void Clear()
    {
        //������ ��ü �ı�
        foreach(ListView1DataModel item in this.ItemList)
        {
            GlobalStatic.MainCont.Instance_Destroy(item.ItemCont.gameObject);
        }

        //����Ʈ ����
        this.ItemList.Clear();
    }

    public void AddItem(ItemDataModel dataItem)
    {
        //������ ����
        ListView1DataModel newData = new ListView1DataModel();
        
        //������ ����
        newData.ItemData = dataItem;

        //UI ����
        GameObject goNew 
            = GlobalStatic.MainCont
                .Instance_New(ListView1Cont.ContentGo.transform);

        //��Ʈ�ѷ� ����
        newData.ItemCont = goNew.GetComponent<ItemObjectController>();



        //����Ʈ�� ����
        this.ItemList.Add(newData);
    }

    
}
