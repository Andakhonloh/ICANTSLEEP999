using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // ใช้เปลี่ยนฉากหรือรีสตาร์ทได้

public class UIStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float decreaseDuration = 300f; // 300 วิ
    public Slider staminaBar;
    public GameObject player;
    public string gameOverSceneName = "GameOver"; // กำหนดชื่อ Scene ตอนตายใน Inspector ได้

    private float decreaseRate;
    private bool isDead = false;

    void Start()
    {
        currentStamina = maxStamina;
        decreaseRate = maxStamina / decreaseDuration;
    }

    void Update()
    {
        if (isDead) return;

        if (currentStamina > 0)
        {
            currentStamina -= decreaseRate * Time.deltaTime;
            staminaBar.value = currentStamina / maxStamina;
        }
        else
        {
            Die();
        }
    }

    public void AddStamina(float amount)
    {
        if (isDead) return;

        currentStamina = Mathf.Min(currentStamina + amount, maxStamina);
        staminaBar.value = currentStamina / maxStamina;
    }

    void Die()
    {
        isDead = true;
        currentStamina = 0;
        staminaBar.value = 0;

        Debug.Log("Player Dead!");

        // 🔹 ถ้ามีระบบ Player
        if (player != null)
        {
            player.SetActive(false); // ปิดตัว Player
        }

        // 🔹 ถ้ามี Scene GameOver
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
