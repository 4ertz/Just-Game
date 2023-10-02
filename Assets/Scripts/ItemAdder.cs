using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemAdder : MonoBehaviour
{
    [SerializeField] private item _itemToAdd;
    private Inventory _inventory;
    private SpriteRenderer _sprite;
    void Start()
    {
        _inventory = GameObject.Find("Player").GetComponent<Inventory>();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = _itemToAdd.Icon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _inventory.AddItem(_itemToAdd);
            GameObject.Destroy(gameObject);
        }
    }
}
