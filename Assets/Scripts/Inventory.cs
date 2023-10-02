using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action<item> OnItemAdded;
    public Action<int> OnItemRemoved;
    public Action<int> OnStackChanched;

    public List<item> _inventoryItems = new List<item>();
    public List<int> _inventoryItemsCount = new List<int>();

    [SerializeField] private int _maxItemsCount = 10;
    [SerializeField] private int _maxItemsInStack = 5;
    [SerializeField] private item[] _items;
    private SaveSerial _saver;
    void Start()
    {
        _saver = GameObject.Find("Main Camera").GetComponent<SaveSerial>();
        for (int i = 0; i < _saver._itemLoadResault.Count; i++)
        {
            AddItem(_items[_saver._itemLoadResault[i]]);
            _inventoryItemsCount[i] = _saver._itemAmountLoadResault[i];
        }
        gameObject.transform.position = new Vector2(_saver._player—oordinatesLoadResault[0], _saver._player—oordinatesLoadResault[1]);
        Redraw();
    }
    public void AddItem(item item)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {        
            if (_inventoryItems[i] == item && _inventoryItemsCount[i] < _maxItemsInStack && _maxItemsCount > _inventoryItems.Count)
            {
                _inventoryItemsCount[i]++; 
                OnStackChanched?.Invoke(i);
                return;
            }
        }
        if (_maxItemsCount > _inventoryItems.Count)
        {
            _inventoryItems.Add(item);
            _inventoryItemsCount.Add(item.Count);
            OnItemAdded?.Invoke(item);
        }
    }
    public void RemoveItem(int index)
    {
        _inventoryItems.RemoveAt(index);
        _inventoryItemsCount.RemoveAt(index);
        OnItemRemoved?.Invoke(index);
    }
    public void SaveGame()
    {
        _saver._player—oordinatesToSave.Add(gameObject.transform.position.x);
        _saver._player—oordinatesToSave.Add(gameObject.transform.position.y);
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            _saver._itemsToSave.Add(_inventoryItems[i].index);
            _saver._itemsAmountToSave.Add(_inventoryItemsCount[i]);
            _saver.SaveGame();
        }
    }
    private void Redraw()
    {
        for (int i = 0; i < _inventoryItems.Count; i++) { OnStackChanched?.Invoke(i); }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
