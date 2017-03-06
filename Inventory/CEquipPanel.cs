using UnityEngine;
using System.Collections;

public class CEquipPanel : MonoBehaviour {

    public UISprite _icon;
    string _itemName;
    GameObject _unit;

    public void GetUnit(GameObject unit)//유닛의 정보를 받아와서 장착한 아이템이 있으면 장비창에 보여줌
    {
        _unit = unit;
        SetIcon(_unit.GetComponent<CSoldierEquip>()._itemName);
    }
    
    void SetIcon(string itemName)
    {
        _icon.spriteName = itemName;
        _itemName = itemName;
    }
}