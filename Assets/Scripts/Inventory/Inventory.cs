using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> slots = new List<ItemSlot>(); // List of item slots

    public List<ItemSlot> Slots => slots; // Public getter for slots

    // Static method to get the Inventory instance from the PlayerController
    public static Inventory GetInventory()
    {
        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            return playerController.GetComponent<Inventory>();
        }

        Debug.LogError("PlayerController or Inventory component not found!");
        return null;
    }
}

// Represents an individual item slot
[System.Serializable] // Allows it to be serialized and shown in the Inspector
public class ItemSlot
{
    [SerializeField] private ItemBase item; // The item in this slot
    [SerializeField] private int count;     // The quantity of the item

    public ItemBase Item => item; // Public getter for the item
    public int Count => count;    // Public getter for the count

    // Constructor for creating a new ItemSlot
    public ItemSlot(ItemBase item, int count)
    {
        this.item = item;
        this.count = count;
    }

    // Method to increase item count
    public void AddCount(int amount)
    {
        count += amount;
    }

    // Method to decrease item count
    public void RemoveCount(int amount)
    {
        count = Mathf.Max(0, count - amount); // Ensures count never goes below zero
    }
}