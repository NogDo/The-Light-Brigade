using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class CWeaponManager : MonoBehaviour
{
    #region public º¯¼ö
    public List<CWeapon> weaponList;
    #endregion

    void Awake()
    {
        weaponList = new List<CWeapon>();

        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath);

        foreach (FileInfo file in di.GetFiles("*.json"))
        {
            if (file.Name.Equals("Weapon.json"))
            {
                string json = File.ReadAllText(file.FullName);

                Debug.Log(json);
                weaponList = JsonConvert.DeserializeObject<List<CWeapon>>(json);
            }
        }
    }

    void OnApplicationQuit()
    {
        string path = $"{Application.streamingAssetsPath}/Weapon.json";
        string json = JsonConvert.SerializeObject(weaponList);

        File.WriteAllText(path, json);
    }
}