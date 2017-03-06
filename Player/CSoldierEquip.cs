using UnityEngine;
using System.Collections;

public class CSoldierEquip : MonoBehaviour {

    public CSoldierStat _stat;
    public CItemManager _itemManager;
    public bool _isEquip = false;
    public string _itemName;

    void Start()
    {
        _stat = GetComponent<CSoldierStat>();
        _itemManager = GameObject.Find("Manager").GetComponent<CItemManager>();
    }

    public void EquipGun(string item)
    {
        _itemName = item;
        _stat._damagelimit += _itemManager._gunDic[item];
        _stat._damage += _itemManager._gunDic[item];
        _isEquip = true;
    }

    public void ReleaseGun()
    {
        _stat._damagelimit -= _itemManager._gunDic[_itemName];
        _stat._damage -= _itemManager._gunDic[_itemName];
        _itemName = null;
        _isEquip = false;
    }

    //if(_isEquip) CSoldierEquip._itemName;=
} 