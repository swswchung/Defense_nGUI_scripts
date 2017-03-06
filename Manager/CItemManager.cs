using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CItemManager : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, int> _gunDic = new Dictionary<string, int>();
    public Dictionary<string, string> _gunInfo = new Dictionary<string, string>();
    public Dictionary<string, int> _gunPrice = new Dictionary<string, int>();
    

	// Use this for initialization
	void Awake ()
    {
        _gunDic.Add("Pistol", 5);
        _gunInfo.Add("Pistol", "데미지 +5");
        _gunPrice.Add("Pistol", 500);

        _gunDic.Add("AssaultRifle", 10);
        _gunInfo.Add("AssaultRifle", "데미지 +10");
        _gunPrice.Add("AssaultRifle", 1000);

    }
}