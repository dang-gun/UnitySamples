using System;

/// <summary>
/// 아이템 리스트 UI 인터페이스
/// </summary>
public interface ItemListUiInterface
{
    /// <summary>
    /// 리스트 비우기
    /// </summary>
    public void Clear();

    /// <summary>
    /// 전달된 아이템 추가하기
    /// </summary>
    /// <param name="dataItem"></param>
    public void AddItem(ItemDataModel dataItem);
}


