using UnityEngine;

using System.Collections.Generic;
using System.Collections;

public class CTileManager : MonoBehaviour {
    
    public GameObject _tile;//타일 프리팹

    public GameObject _tiles;//타일들 저장용 오브젝트

    public List<GameObject> _tileList;

    public CGameManager _gameManager;

    void Awake()
    {
        CreateTile();
    }

    void Start()
    {
        DisableTile();
    }
    //  2 / 1.5 / 1 / 0.5 / 0 / 0.5 / 1 / 1.5 / 2 //
    void CreateTile()
    {
        for (int y = 9; 0 < y; y--)
        {
            for (int x = 0; x < 9; x++)
            {   //x * 타일크기 - 제일왼쪽위치 y도 비슷
                GameObject tile = Instantiate(_tile, new Vector3((x * 0.5f) - 2f, 0f, (y * 0.5f) - 2.5f), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
                _tileList.Add(tile);

                if(x == 4 && y == 5)//가운데 플레이어본체가 있는 자리에는 건설금지
                {
                    _tileList[_tileList.Count - 1].SendMessage("ChangeMaterial", 2);
                }
            }
        }

        for (int i = 0 ; i < _tileList.Count ;i++)
        {
            _tileList[i].transform.SetParent(_tiles.transform);
        }
    }

    void ViewTile()
    {
        Color color;
        for (int i = 0; i < _tileList.Count; i++)
        {
            //_tileList[i].SetActive(true);
            color = _tileList[i].GetComponent<CTile>()._renderer.material.color;
            _tileList[i].GetComponent<CTile>()._renderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
            
        }
    }

    void DisableTile()
    {
        Color color;
        for (int i = 0 ; i < _tileList.Count ; i++)
        {
            //_tileList[i].SetActive(false);
            color = _tileList[i].GetComponent<CTile>()._renderer.material.color;
            _tileList[i].GetComponent<CTile>()._renderer.material.color = new Color(color.r, color.g, color.b, 0.0f);
        }
    }

    public void DeleteTileUnit(GameObject unit, bool isClear)
    {
        for (int i = 0; i < _tileList.Count; i++)
        {
            if (_tileList[i].GetComponent<CTile>()._unit == unit)
            {
                _tileList[i].SendMessage("DeleteUnit");
                if (!isClear)
                {
                    DisableTile();
                }
            }
        }
    }
}
