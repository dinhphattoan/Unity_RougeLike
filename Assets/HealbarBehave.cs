using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealbarBehave : MonoBehaviour
{
    public Slider Slider;
    public Color High;
    public Color Low;
    public Vector3 Offset;

    void Start()
    {
    }

    public void SetHealth(float health, float maxHealth)
    {
        Slider.maxValue = maxHealth;
        Slider.value = health;
        Slider.gameObject.SetActive(health < maxHealth);

        // Debug log to verify the health values and Slider status
        Debug.Log($"SetHealth called: health={health}, maxHealth={maxHealth}, Slider.activeSelf={Slider.gameObject.activeSelf}");

        // Get the fill image component directly from the Slider
        Image fillImage = Slider.GetComponentsInChildren<Image>()[1]; // Assuming Fill image is the second child
        if (fillImage != null)
        {
            fillImage.color = Color.Lerp(Low, High, health / maxHealth);
            Debug.Log($"Interpolated Color: {fillImage.color}");
        }
        else        
        {
            Debug.LogError("No Image component found in the Slider's fillRect.");
        }
    }

    void Update()
    {
        Vector3 worldPosition = transform.parent.position + Offset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        Slider.transform.position = screenPosition;
    }
}
