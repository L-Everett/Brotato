using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static GameManager Instance;
    public RoleData RoleData;
    public List<WeaponData> WeaponDatas = new List<WeaponData>();
    public DifficulityData difficulityData;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public object RandomSelect<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return null;
        }
        System.Random random = new System.Random();
        int index = random.Next(0, list.Count);
        return list[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
