﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CEnemyManager : MonoBehaviour {
    
    [SerializeField]
    List<GameObject> _enemyList;

    public GameObject _enemyPrefab;
    public Transform[] _genPos;

    CPlayerManager _playerManager;
    CGameManager _gameManager;
    List<GameObject> _soldierList;

    void Awake()
    {
        _playerManager = GetComponent<CPlayerManager>();
        _gameManager = GetComponent<CGameManager>();
    }

    public void EnemyGen(int enemyCount)
    {
        int randomNum = 0;
        int damage = 0;
        if(5 < enemyCount)
        {
            damage = enemyCount / 5;
        }

        _soldierList = _playerManager.GetSoldier();

        for(int i = 0 ; i < enemyCount ; i++)
        {
            randomNum = Random.Range(0, _genPos.Length);
            //적을 지정된 위치에 생성하고 자식으로 만듬 이미 자식이 있을경우 다른 위치에서 생성
            if (_genPos[randomNum].childCount == 0)
            {
                GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity) as GameObject;
                enemy.SendMessage("SetDamage", damage);
                _enemyList.Add(enemy);
                enemy.transform.SetParent(_genPos[randomNum]);
                enemy.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                //생성된 오브젝트에 플레이어의 유닛들의 위치정보를 넘김
                enemy.SendMessage("PlayerUnitPos",_soldierList);

            }
            else
            {
                i--;
            }
        }
        StartCoroutine("CheckCoroutine");
    }

    public List<GameObject> GetEnemy()
    {
        return _enemyList;
    }

    public void DeleteList(GameObject unit)
    {
        _enemyList.Remove(unit);
        _playerManager.GoldUp(200);
    }

    IEnumerator CheckCoroutine()
    {
        while(_enemyList.Count != 0)
        {
            yield return null;
        }
        _gameManager.StageClear();
    }
}
