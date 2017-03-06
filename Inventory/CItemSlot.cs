using UnityEngine;
using System.Collections;

public class CItemSlot : MonoBehaviour {
    
    [SerializeField]
    public UISprite _icon; //아이템 아이콘 아이템이 해당 슬롯에 들어오면 엑티브 표시, 스프라이트를 해당 아이템으로 교체
    public GameObject _itemInfoPanel;
    public GameObject _soldierPanel;
    public string _item;

    int _number;    //슬롯 순번
    
    void Awake()
    {
        _itemInfoPanel = GameObject.Find("ItemInfoPanel");
        _soldierPanel = GameObject.Find("SoldierUI");
    }

    void SetNumber(int number)
    {
        _number = number;
    }

    public void SetItem(string itemName)
    {
        if (itemName.Equals("Empty"))
        {
            _icon.enabled = false;
            _item = itemName;
        }
        else
        {
            _icon.spriteName = itemName;
            _icon.enabled = true;
            _item = itemName;
        }
    }
	
	public void OnClick()
    {
        if (!_item.Equals("Empty"))
        {
            _itemInfoPanel.SetActive(true);
            _soldierPanel.SendMessage("SetItemInfo", _item);
            _soldierPanel.SendMessage("SetSlotNumber", _number);
        }
        else
        {
            _itemInfoPanel.SetActive(false);
        }
    }
}