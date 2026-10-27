using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Display")]
    public Image environmentDisplay;
    public TMP_Text locationTitleText;
    public CanvasGroup titleCanvasGroup;

    [Header("The 4 Directional Buttons")]
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;

    [Header("Special Hotspots")]
    public GameObject closetButton;

    [Header("Audio")]
    public AudioSource ambientAudioSource;

    [Header("Starting Room")]
    public LocationNode currentLocation;

    private Coroutine fadeRoutine;
    private GameObject currentActiveItemContainer; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (currentLocation != null)
        {
            UpdateScene();
        }
    }

    public void ChangeLocation(LocationNode nextLocation)
    {
        if (nextLocation != null)
        {
            currentLocation = nextLocation;
            UpdateScene();
        }
    }

    void UpdateScene()
    {
        environmentDisplay.sprite = currentLocation.backgroundSprite;

        upButton.SetActive(currentLocation.upRoute != null);
        downButton.SetActive(currentLocation.downRoute != null);
        leftButton.SetActive(currentLocation.leftRoute != null);
        rightButton.SetActive(currentLocation.rightRoute != null);

        if (closetButton != null)
        {
            closetButton.SetActive(currentLocation.closetRoute != null);
        }

        if (currentActiveItemContainer != null)
        {
            currentActiveItemContainer.SetActive(false); 
        }

        if (currentLocation.roomItemContainer != null)
        {
            currentLocation.roomItemContainer.SetActive(true);
            currentActiveItemContainer = currentLocation.roomItemContainer;
        }

        if (ambientAudioSource != null)
        {
            if (currentLocation.roomAmbientSound != null)
            {
                if (ambientAudioSource.clip != currentLocation.roomAmbientSound)
                {
                    ambientAudioSource.clip = currentLocation.roomAmbientSound;
                    ambientAudioSource.Play();
                }
            }
            else
            {
                ambientAudioSource.Stop();
            }
        }

        if (locationTitleText != null && titleCanvasGroup != null)
        {
            locationTitleText.text = currentLocation.locationName;

            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            fadeRoutine = StartCoroutine(FadeTitleRoutine());
        }
        PickupItem[] allItems = FindObjectsOfType<PickupItem>(true);
        foreach (PickupItem item in allItems)
        {
            item.CheckVisibility();
        }
    }

    public void OnUpClicked() { if (currentLocation.upRoute != null) ChangeLocation(currentLocation.upRoute); }
    public void OnDownClicked() { if (currentLocation.downRoute != null) ChangeLocation(currentLocation.downRoute); }
    public void OnLeftClicked() { if (currentLocation.leftRoute != null) ChangeLocation(currentLocation.leftRoute); }
    public void OnRightClicked() { if (currentLocation.rightRoute != null) ChangeLocation(currentLocation.rightRoute); }
    public void OnClosetClicked() { if (currentLocation.closetRoute != null) ChangeLocation(currentLocation.closetRoute); }

    IEnumerator FadeTitleRoutine()
    {
        float elapsedTime = 0f;
        float fadeDuration = 0.5f;
        titleCanvasGroup.alpha = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            titleCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        titleCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(1.5f);

        elapsedTime = 0f;
        float outDuration = 0.8f;

        while (elapsedTime < outDuration)
        {
            elapsedTime += Time.deltaTime;
            titleCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / outDuration));
            yield return null;
        }
        titleCanvasGroup.alpha = 0f;
    }
}