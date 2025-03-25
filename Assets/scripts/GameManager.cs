using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public RoleData RoleData;
    public WeaponData weaponData;
    public DifficulityData difficulityData;
    public int currentWave;

    public int weaponCount;

    private void Awake()
    {
        Instance = this;
        currentWave = 1;
        weaponCount = 6;

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
}
