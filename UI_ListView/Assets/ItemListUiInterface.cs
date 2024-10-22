using System;

/// <summary>
/// 아이템 리스트 UI 인터페이스
/// </summary>
public interface ItemListUiInterface
{
    /// <summary>
    /// 전달된 아이템을 리스트에 추가하고 UI를 표시한다.
    /// </summary>
    /// <param name="dataItem"></param>
    public void AddItem(ItemDataModel dataItem);

    /// <summary>
    /// 전달된 아이템 데이터를 찾아 UI와 리스트에서 지운다.
    /// </summary>
    /// <param name="dataItem"></param>
    public void RemoveItem(ItemDataModel dataItem);

    /// <summary>
    /// 리스트 비우기
    /// </summary>
    public void Clear();

    
}


