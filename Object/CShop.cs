using UnityEngine;
using System.Collections;

public class CShop : MonoBehaviour {

    CPlayerManager _playerManager;
    CInventory _inventory;
    CItemManager _itemManager;
    public UILabel _logLabel;

    void Awake()
    {
        _playerManager = GameObject.Find("Manager").GetComponent<CPlayerManager>();
        _itemManager = GameObject.Find("Manager").GetComponent<CItemManager>();
        _inventory = GameObject.Find("SoldierUI").GetComponent<CInventory>();
    }

	public void OnClickButton(string itemName)
    {
        int price = _itemManager._gunPrice[itemName];
        if(price <= _playerManager.GetGold())
        {
            _playerManager.GoldDown(price);
            _inventory.AddItem(itemName);
        }
        else
        {
            StartCoroutine("LogMessageCoroutine");
        }
    }

    IEnumerator LogMessageCoroutine()
    {
        _logLabel.text = "돈이 부족합니다.";
        yield return new WaitForSeconds(3.0f);
        _logLabel.text = "";
    }
}
