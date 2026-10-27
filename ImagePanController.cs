using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteriorPanController : MonoBehaviour
{
    private RectTransform rectTransform;

    [Header("Pan Limits")]
    public float maxPanX = 80f;
    public float maxPanY = 80f;
    public float smoothSpeed = 10f;

    private Vector2 currentOffset = Vector2.zero;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        float normalizedX = (mousePos.x / Screen.width) - 0.5f;
        float normalizedY = (mousePos.y / Screen.height) - 0.5f;

        float targetX = -normalizedX * 2f * maxPanX;
        float targetY = -normalizedY * 2f * maxPanY;

        Vector2 targetOffset = new Vector2(targetX, targetY);

        currentOffset = Vector2.Lerp(currentOffset, targetOffset, Time.deltaTime * smoothSpeed);

        rectTransform.anchoredPosition = currentOffset;
    }
}