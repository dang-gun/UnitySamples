using System;
using UnityEngine;



/// <summary>
/// 랩패널 스크롤에 출력할 아이템 한개가 가지고 있는 데이터
/// </summary>
public class WrapScrollDataModel
{
    /// <summary>
    /// 표시에 사용된 데이터
    /// </summary>
    public ItemDataModel ItemData { get; set; }

    /// <summary>
    /// UI에 생성된 개체의 컨트롤러
    /// </summary>
    public ItemObjectController ItemCont { get; set; }

}