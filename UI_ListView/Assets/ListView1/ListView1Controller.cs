using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class ListView1Controller : MonoBehaviour
{
    /// <summary>
    /// 관리중인 아이템 리스트
    /// </summary>
    private List<ItemDataModel> ItemList
        = new List<ItemDataModel>();

    /// <summary>
    /// 컨탠츠 영역
    /// </summary>
    public GameObject ContentGo = null;

    void Awake()
    {
        this.ContentGo = this.transform.Find("Viewport/Content").gameObject;
    }


}
