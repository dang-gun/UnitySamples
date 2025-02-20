using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 랩패널 스크롤 컨트롤러
/// </summary>
public class WrapScrollController : MonoBehaviour
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
        //this.ContentGo = this.transform.Find("Viewport/Content/GridGroup").gameObject;
        this.ContentGo = this.transform.Find("Viewport/Content").gameObject;
    }


}
