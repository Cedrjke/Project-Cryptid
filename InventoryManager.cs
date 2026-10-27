using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI Panels")]
    public GameObject inventoryPanel; 
    public Transform itemsParentGrid;
    private bool isOpen = false;

    [Header("Audio")]
    public AudioSource audioSource;       
    public AudioClip bagOpenSound;        
    public AudioClip bagCloseSound;       

    [Header("Data")]
    public List<Item> collectedItems = new List<Item>();
    private HashSet<Item> collectedItemLookup = new HashSet<Item>();

    void Awake() { Instance = this; }

    void Start()
    {
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (audioSource != null)
        {
            if (isOpen && bagOpenSound != null) audioSource.PlayOneShot(bagOpenSound);
            else if (!isOpen && bagCloseSound != null) audioSource.PlayOneShot(bagCloseSound);
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null && !collectedItemLookup.Contains(newItem))
        {
            collectedItems.Add(newItem);
            collectedItemLookup.Add(newItem);
            RefreshInventoryUI();
        }
    }

    public bool HasItem(Item item)
    {
        return collectedItemLookup.Contains(item);
    }

    void RefreshInventoryUI()
{
    if (itemsParentGrid == null) return;

    foreach (Transform child in itemsParentGrid)
    {
        Destroy(child.gameObject);
    }

    foreach (Item item in collectedItems)
    {
        GameObject slotObj = new GameObject(item.itemName + "_Slot", typeof(RectTransform), typeof(Image));
        slotObj.transform.SetParent(itemsParentGrid, false);

        RectTransform rect = slotObj.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.sizeDelta = new Vector2(80, 80);

        Image img = slotObj.GetComponent<Image>();
        img.sprite = item.itemIcon;
        img.preserveAspect = true;
    }

    LayoutRebuilder.ForceRebuildLayoutImmediate(itemsParentGrid as RectTransform);
}
}