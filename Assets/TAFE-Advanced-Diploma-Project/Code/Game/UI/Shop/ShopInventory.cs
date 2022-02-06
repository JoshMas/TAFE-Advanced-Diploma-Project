using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryContainer;
    private List<GameObject> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<GameObject>();
        foreach(Transform child in inventoryContainer ?? transform)
        {
            items.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveItem(GameObject _itemToRemove)
    {
        items.Remove(_itemToRemove);
    }
}
