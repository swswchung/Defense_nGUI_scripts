using UnityEngine;
using System.Collections;

public class CItemInfoPanel : MonoBehaviour {

    public UISprite _icon;
    public UILabel _infoLabel;
    
    CSoldierPanel _panel;
    CItemManager _itemManager;
    CPlayerManager _playerManager;
    CInventory _inventory;
    [SerializeField] GameObject _unit;
    public GameObject _itemInfoPanel;

    int _slotNumber;

    // Use this for initialization
    void Start()
    {
        _inventory = GetComponent<CInventory>();
        _panel = GetComponent<CSoldierPanel>();
        _itemManager = GameObject.Find("Manager").GetComponent<CItemManager>();
        _playerManager = GameObject.Find("Manager").GetComponent<CPlayerManager>();
        _itemInfoPanel.SetActive(false);
    }

    void SetSlotNumber(int number)
    {
        _slotNumber = number;
    }

    void SetItemInfo(string itemName)
    {
        _icon.spriteName = itemName;
        _infoLabel.text = _itemManager._gunInfo[itemName];
    }

    public void OnEquipButtonClick()
    {
        _unit = _panel.SetUnit();
        if (!_unit.GetComponent<CSoldierEquip>()._isEquip)//장착하고 인벤토리에 없애기
        {
            _unit.GetComponent<CSoldierEquip>().EquipGun(_icon.spriteName);
            _inventory.DeleteItem(_slotNumber);
        }
        else//교체
        {
            string swapItemName = _unit.GetComponent<CSoldierEquip>()._itemName;
            _unit.GetComponent<CSoldierEquip>().ReleaseGun();
            _unit.GetComponent<CSoldierEquip>().EquipGun(_icon.spriteName);
            _inventory.SwapItem(_slotNumber, swapItemName);
        }
        _itemInfoPanel.SetActive(false);
        SendMessage("UpdateData");
        SendMessage("SetIcon", _icon.spriteName);
    }

    public void OnSellButtonClick(string itemName)
    {
        _playerManager.GoldUp(_itemManager._gunPrice[itemName] / 2);
        _inventory.DeleteItem(_slotNumber);
        _itemInfoPanel.SetActive(false);
    }
}
