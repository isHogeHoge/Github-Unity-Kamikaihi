using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GeekBtnController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject cdPlayerBtn;
    [SerializeField] GameObject geek;
    [SerializeField] GameObject geekInTopRightRoom;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite geekItemSpr;

    // 自身をクリックした時、アイテムとして取得できるようにする
    public void OnPointerClick(PointerEventData eventData)
    {
        geek.SetActive(false);
        geekInTopRightRoom.SetActive(false);
        itemManager.GetComponent<ItemManager>().ClickItemBtn(geekItemSpr);

        // CDPlayerをクリック可能に
        cdPlayerBtn.GetComponent<Image>().enabled = true;
    }
}
