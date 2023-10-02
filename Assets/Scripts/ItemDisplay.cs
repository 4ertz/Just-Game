using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Font _font;
    [SerializeField] private RectTransform _itemsPanel;
    [SerializeField] private Sprite _deleteButtonSprite;
    [SerializeField] private Transform[] _pointToMoveInventory = new Transform[2]; //1 - open point | 0 - closed point
    private Inventory _inventory;
    private List<GameObject> _icons = new List<GameObject>();
    private List<int> _indexes = new List<int>();
    private bool _open = false;



    void Start() 
    {
        _inventory = GameObject.Find("Player").GetComponent<Inventory>();
        DrawStartItems();
        _inventory.OnItemAdded += OnItemAdded;
        _inventory.OnStackChanched += EditIcon;
        transform.parent.position = _pointToMoveInventory[0].position;
        OpenOrCloseInventory();
    }
    private void OnDisable() { _inventory.OnItemAdded -= OnItemAdded; _inventory.OnStackChanched -= EditIcon; }

    public void OpenOrCloseInventory()
    {
        if (_open == false) { transform.parent.DOMove(_pointToMoveInventory[0].position, 1); _open = true; }
        else { transform.parent.DOMove(_pointToMoveInventory[1].position, 1); _open = false; }
    }
    private void OnItemAdded(item item)
    {
        var icon = new GameObject("Icon");
        int MyIndex = _icons.Count;
        _indexes.Add(MyIndex);
        _icons.Add(icon);
        icon.transform.SetParent(gameObject.transform);
        icon.AddComponent<Image>().sprite = item.Icon;
        var _iconButton = icon.AddComponent<Button>();

        var text = new GameObject("text");
        text.transform.SetParent(icon.transform);
        if (item.Count <= 1) { text.AddComponent<Text>().text = null; }
        else { text.AddComponent<Text>().text = item.Count.ToString(); }
        text.GetComponent<Text>().color = Color.black;
        text.transform.position = new Vector2(15, -12);
        text.GetComponent<Text>().font = _font;
        text.GetComponent<Text>().fontSize = 20;

        var deleteButton = new GameObject("Delete Button");
        deleteButton.transform.SetParent(icon.transform);
        deleteButton.AddComponent<Image>().sprite = _deleteButtonSprite;
        deleteButton.AddComponent<Button>();
        deleteButton.transform.position = new Vector2(22, 22);
        deleteButton.transform.localScale = new Vector2(0.5f, 0.5f);
        deleteButton.SetActive(false);

        bool isDeleteButtonVisible = false;
        _iconButton.onClick.AddListener(() => {
            if (isDeleteButtonVisible == false) { deleteButton.SetActive(true); isDeleteButtonVisible = true; }
            else { deleteButton.SetActive(false); isDeleteButtonVisible = false; }
        });
        deleteButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Index: " + _indexes.IndexOf(MyIndex));
            _inventory.RemoveItem(_indexes.IndexOf(MyIndex));
            RemoveIcon(_indexes.IndexOf(MyIndex));
            _indexes.RemoveAt(_indexes.IndexOf(MyIndex));
        });
    }
    private void DrawStartItems()
    {
        for (int i = 0; i < _inventory._inventoryItems.Count; i++)
        {
            var item = _inventory._inventoryItems[i];
            OnItemAdded(item);
        }
    }

    private void EditIcon(int index)
    {
        if (_inventory._inventoryItemsCount[index] <= 1) { _icons[index].GetComponentInChildren<Text>().text = null; }
        else { _icons[index].GetComponentInChildren<Text>().text = _inventory._inventoryItemsCount[index].ToString(); }
    }
    private void RemoveIcon(int index)
    {
        GameObject.Destroy(_icons[index]);  
        _icons.RemoveAt(index);
    }

}
