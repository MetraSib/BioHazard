using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public int numbersOfMotherCell;
    public List<Vector3> position = new List<Vector3>();
    public List<string> motherName = new List<string>();
}

[Serializable]
public class LoadManager : MonoBehaviour
{
    //public List<GameObject> sphere;
    //Data enemy = new Data();

    public List<GameObject> prefabs;

    public Transform parentForMotherCells;

    private void Awake()
    {
        LoadData(StaticSaveData.levelData);
    }

    public void LoadData(string levelData)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(levelData, typeof(TextAsset));
        string tileFile = txtAsset.text;
        Data returnData = JsonUtility.FromJson<Data>(tileFile);
        for (int i = 0; i < returnData.numbersOfMotherCell; i++)
        {
            for (int j = 0; j < prefabs.Count; j++)
            {
                if (returnData.motherName[i].Equals(prefabs[j].name))
                {
                    Instantiate(prefabs[j], returnData.position[i], Quaternion.identity, parentForMotherCells);
                }
            }
        }
    }

    /* Идея сохранения новых уровней в том, чтобы записывать новый текстовый файл и делать загрузку из него,
     * например метод public void SaveData() можно реализовать в старте, заблаговременно расставив все материнские клетки по местам
     */

    //public void SaveData()
    //{
    //    for (int i = 0; i < sphere.Count; i++)
    //    {
    //        enemy.motherName.Add(sphere[i].name);
    //        enemy.position.Add(sphere[i].transform.position);
    //    }
    //    string json = JsonUtility.ToJson(enemy, true);
    //    File.WriteAllText("E:/" + "Level_4.txt", json);
    //}
}




