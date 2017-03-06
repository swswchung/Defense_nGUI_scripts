using UnityEngine;

using System.Collections;

public class CSoldierPanel : MonoBehaviour {

    private const int UPGRADEGOLD = 50;
    
    //Soldier
    public GameObject _unit;

    ///nGUI/////////////////////
    public UISlider _hpBar;
    public UISlider _atkBar;
    public UISlider _dfBar;

    public UILabel _hpLabel;
    public UILabel _atkLabel;
    public UILabel _dfLabel;
    public UILabel _maxHpLabel;
    public UILabel _maxAtkLabel;
    public UILabel _maxDfLabel;
    public UILabel _requireGoldLabel;
    public UILabel _logLabel;
    

    public UIButton _destroyButton;
    UIPanel _soldierUI;
    public GameObject _itemInfoPanel;
    ///////////////////////////

    //scripts////////////////////////////
    public CPlayerManager _playerManager;
    public CTileManager _tileManager;
    CSoldierStat _stat;
    ////////////////////////////////////

    void Awake()
    {
        _soldierUI = GetComponent<UIPanel>();
    }

    void OnEnable()
    {
        _soldierUI.alpha = 0f;
        StartCoroutine("EnablePanel");
    }

    IEnumerator EnablePanel()
    {
        float a = 0.0f;
        
        while (a != 1)
        {
            a += 0.05f;
            _soldierUI.alpha = a;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    void GetUnit(GameObject unit)
    {
        _stat = unit.GetComponent<CSoldierStat>();
        _unit = unit;
        UpdateData();
        GetComponent<CEquipPanel>().GetUnit(_unit);
        if (_stat._maxHp == _stat._hp)
        {
            _requireGoldLabel.text = "0";
        }
        else
        {
            _requireGoldLabel.text = ((_stat._maxHp - _stat._hp) * 2).ToString();
        }

        if (_unit.name.Equals("Player"))
        {
            _destroyButton.gameObject.SetActive(false);
        }
        else
        {
            _destroyButton.gameObject.SetActive(true);
        }
        _logLabel.text = "";
    }

    public GameObject SetUnit()
    {
        return _unit;
    }

    void UpdateData()
    {
        _hpLabel.text = _stat._maxHp.ToString();
        _atkLabel.text = _stat._damage.ToString();
        _dfLabel.text = _stat._defense.ToString();
        _maxHpLabel.text = _stat._hplimit.ToString();
        _maxAtkLabel.text = _stat._damagelimit.ToString();
        _maxDfLabel.text = _stat._defenselimit.ToString();

        float damagePer = 100.0f / _stat._damagelimit;
        float defensePer = 100.0f / _stat._defenselimit;
        float hpPer = 100.0f / _stat._hplimit;

        _hpBar.value = _stat._maxHp * (0.01f * hpPer);
        _atkBar.value = _stat._damage * (0.01f * damagePer);
        _dfBar.value = _stat._defense * (0.01f * defensePer);
    }

    public void UpgradeHPButton()
    {
        if (_stat._maxHp < _stat._hplimit)
        {
            if (UPGRADEGOLD <= _playerManager.GetGold())
            {
                _stat.MaxHpUp();
                _playerManager.GoldDown(50);
                _logLabel.text = "";
            }
            else
            {
                _logLabel.text = "돈이 부족합니다";
            }
        }
        else
        {
            _logLabel.text = "HP가 최대치에 도달했습니다.";
        }
        UpdateData();
    }

    public void UpgradeAPButton()
    {
        if (_stat._damage < _stat._damagelimit)
        {
            if (50 <= _playerManager.GetGold())
            {
                _stat.APUp();
                _playerManager.GoldDown(50);
                _logLabel.text = "";
            }
            else
            {
                _logLabel.text = "돈이 부족합니다.";
            }
        }
        else
        {
            _logLabel.text = "공격력이 최대치에 도달했습니다.";
        }
        UpdateData();
    }

    public void UpgradeDPButton()
    {
        if (_stat._defense < _stat._defenselimit)
        {
            if (50 <= _playerManager.GetGold())
            {
                _stat.DPUp();
                _playerManager.GoldDown(50);
                _logLabel.text = "";
            }
            else
            {
                _logLabel.text = "돈이 부족합니다.";
            }
        }
        else
        {
            _logLabel.text = "방어력이 최대치에 도달했습니다.";
        }
        UpdateData();
    }

    public void RecoveryHPButton()
    {
        if (_stat._hp != _stat._maxHp)
        {
            if (int.Parse(_requireGoldLabel.text) <= _playerManager.GetGold())
            {
                _stat.HPRecovery();
                _playerManager.GoldDown(int.Parse(_requireGoldLabel.text));
                _requireGoldLabel.text = "0";
                _logLabel.text = "";
            }
            else
            {
                _logLabel.text = "돈이 부족합니다";
            }
        }
        else
        {
            _logLabel.text = "이미 최대체력입니다.";
        }
        UpdateData();
    }

    public void OnDestroyOKButtonClick()
    {
        // 해당 타일의 유닛 제거
        //GameManager오브젝트에는 CPlayerManager와 CEnemyManager 스크립트 둘다
        //DeleteList라는 함수를 가지고있어서 SendMessage함수 사용불가.
        _playerManager.GetComponent<CPlayerManager>().DeleteList(_unit);
        _tileManager.DeleteTileUnit(_unit, true);
        Destroy(_unit);
    }

    public void OnExitButtonClick()
    {
        Camera.main.SendMessage("DestroyTarget");
        
    }

    void OnDisable()
    {
        _itemInfoPanel.SetActive(false);
    }
}
