using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    public List<int> _itemsToSave;
    public List<int> _itemsAmountToSave;

    public List<int> _itemLoadResault;
    public List<int> _itemAmountLoadResault;

    public List<float> _player—oordinatesToSave;
    public List<float> _player—oordinatesLoadResault;

    public int _equipedGunIndexToSave;
    public int _equipedGundIndexToLoad;



    private void Awake()
    {
        LoadGame();
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/Save.dat");
        SaveData data = new SaveData();
        data._savedEquipedGunIndex = _equipedGunIndexToSave;
        for (int i = 0; i < _itemsToSave.Count; i++)
        {
            data._savedItems.Add(_itemsToSave[i]);
            data._savedItemsAmount.Add(_itemsAmountToSave[i]);
        }
        for (int j = 0; j < _player—oordinatesToSave.Count; j++)
        {
            data._savedPlayer—oordinates.Add(_player—oordinatesToSave[j]);
        }
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");

    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/Save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/Save.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _itemLoadResault.Clear();
            _itemAmountLoadResault.Clear();
            _equipedGundIndexToLoad = data._savedEquipedGunIndex;
            for (int i = 0; i < data._savedItems.Count; i++)
            {
                _itemLoadResault.Add(data._savedItems[i]);
                _itemAmountLoadResault.Add(data._savedItemsAmount[i]);
            }
            for (int j = 0; j < 2; j++)
            {
              _player—oordinatesLoadResault.Add(data._savedPlayer—oordinates[j]);
            }

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void ClearData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/Save.dat");
        SaveData data = new SaveData();
        data.Clear();
        data.Clear();
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data was cleared!");
    }
}
[Serializable]
class SaveData
{
    public List<int> _savedItems  = new List<int>();
    public List<int> _savedItemsAmount = new List<int>();
    public List<float> _savedPlayer—oordinates = new List<float>();
    public int _savedEquipedGunIndex;


    public void Clear()
    {
        _savedItems.Clear();
        _savedItemsAmount.Clear();
    }

}
