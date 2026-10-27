using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Item Configuration")]
    public Item itemToPickup;          
    public LocationNode roomLocation;  

    void Start()
    {
        CheckVisibility();
    }

    public void CheckVisibility()
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemToPickup))
        {
            gameObject.SetActive(false);
            return;
        }

        if (GameManager.Instance != null && GameManager.Instance.currentLocation == roomLocation)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnItemClicked()
    {
        if (itemToPickup != null && InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemToPickup);
            gameObject.SetActive(false);
        }
    }
}