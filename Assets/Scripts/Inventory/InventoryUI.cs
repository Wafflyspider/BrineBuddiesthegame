using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList; 
    [SerializeField] ItemSlotUI itemSlotUI; 

    private Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
        if (inventory == null)
        {
            Debug.LogError("Inventory not found! Ensure the PlayerController has an Inventory component.");
        }
    }

    private void Start()
    {
        if (inventory != null)
        {
            UpdateItemList();
        }
    }

    void UpdateItemList()
    {
        
        foreach (Transform child in itemList.transform) 
        {
            Destroy(child.gameObject);
        }

        
        foreach (var itemSlot in inventory.Slots)
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform); 
            slotUIObj.SetData(itemSlot); 
        }
    }

    public void HandleUpdate()
    {
        
        Debug.Log("Updating Inventory UI...");
    }
}