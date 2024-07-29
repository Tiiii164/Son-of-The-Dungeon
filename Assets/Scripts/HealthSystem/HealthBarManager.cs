using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] private Gradient healthBarGradient;
    private Slider healthSlider;
    private Image fillImage; // Biến tham chiếu đến Image của Fill Area

    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthSlider = GetComponent<Slider>();
        fillImage = healthSlider.fillRect.GetComponent<Image>(); // Lấy component Image từ fillRect
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthText.text = playerHealth.currentHealth.ToString() + " HP / " + playerHealth.maxHealth.ToString() + " HP";
        float healthPercentage = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        healthSlider.value = healthPercentage;
        UpdateHealthBarColor(healthPercentage);
    }

    private void UpdateHealthBarColor(float healthPercentage)
    {
        // Cập nhật màu của Fill Area dựa trên gradient
        fillImage.color = healthBarGradient.Evaluate(healthPercentage);
    }
}
