using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CInventory : MonoBehaviour {

    public GameObject _slot;   //인벤에 그려질 아이템창 프리팹
    public GameObject _inven;  //슬롯의 부모
    public List<GameObject> _slotList;   //슬롯관리용 리스트

    public UIScrollView _scrollView;
    public UIGrid _grid;

	// Use this for initialization
	void Start ()
    {
	    for(int i = 0 ; i < 16 ; i++)
        {
            GameObject itemslot = NGUITools.AddChild(_grid.gameObject, _slot);
            itemslot.GetComponent<CItemSlot>().SetItem("Empty");
            itemslot.SendMessage("SetNumber", i);
            _grid.Reposition();
            _scrollView.ResetPosition();
            _slotList.Add(itemslot);
        }

        /*
        for (int i = 0 ; i < _slotList.Count ; i++)
        {
            _slotList[i].transform.SetParent(_inven.transform, false);
        }
        */
        AddItem("Pistol");
        AddItem("AssaultRifle");
    }

    public void AddItem(string item)
    {
        for(int i = 0 ; i < _slotList.Count ; i++)
        {
            UISprite slot = _slotList[i].transform.FindChild("ItemIcon").GetComponent<UISprite>();
            if(!slot.isActiveAndEnabled)
            {
                _slotList[i].GetComponent<CItemSlot>().SetItem(item);
                slot.enabled = true;
                slot.spriteName = item;
                break;
            }
        }

        /*for(int i = 0 ; i < _slotList.Count ; i++)
        {
            Debug.Log(_slotList[i].transform.FindChild("ItemIcon").GetComponent<UISprite>().isActiveAndEnabled);
            if(!_slotList[i].transform.FindChild("ItemIcon").GetComponent<UISprite>().isActiveAndEnabled)
            {
                Debug.Log("Hi");
                _slotList[i].transform.FindChild("ItemIcon").GetComponent<UISprite>().enabled = true;
                _slotList[i].transform.FindChild("ItemIcon").GetComponent<UISprite>().spriteName = item;
                break;
            }
        }*/
    }

    public void DeleteItem(int number)
    {
        _slotList[number].SendMessage("SetItem", "Empty");
    }

    public void SwapItem(int number, string itemName)
    {
        _slotList[number].SendMessage("SetItem", itemName);
    }
}
