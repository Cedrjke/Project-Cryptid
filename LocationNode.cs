using UnityEngine;

[CreateAssetMenu(fileName = "NewLocationNode", menuName = "Navigation/LocationNode")]
public class LocationNode : ScriptableObject
{
    public string locationName;
    public Sprite backgroundSprite;

    [Header("Audio")]
    public AudioClip roomAmbientSound;

    [Header("Navigation Routes")]
    public LocationNode upRoute;
    public LocationNode downRoute;
    public LocationNode leftRoute;
    public LocationNode rightRoute;
    public LocationNode closetRoute;

    [Header("Room Items Container")]
    public GameObject roomItemContainer;
}