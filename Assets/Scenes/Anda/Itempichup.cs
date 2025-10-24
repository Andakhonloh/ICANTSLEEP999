using UnityEngine;

public class Itempichup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 2f;
    public float staminaIncrease = 60f;

    [Header("References")]
    public GameObject interactUI;        // UI ตัว E ให้แสดงเมื่ออยู่ใกล้
    public GameObject cutsceneObject;    // ใส่ Cutscene Object ใน Inspector

    private Transform player;
    private bool isInRange = false;

    private static int itemsCollected = 0;
    public static int itemsToCollect = 3;

    private UIStamina staminaSystem;

    void Start()
    {
        // หา Player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.transform;

        // หา StaminaSystem (*** นี่คือบรรทัดที่แก้ไขแล้ว ***)
        staminaSystem = FindFirstObjectByType<UIStamina>();

        // เริ่มต้นซ่อน UI
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        isInRange = distance <= pickupRange;

        // แสดงหรือซ่อน UI ตัว E
        if (interactUI != null)
            interactUI.SetActive(isInRange);

        // ถ้าอยู่ในระยะและกด E
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        if (staminaSystem != null)
            staminaSystem.AddStamina(staminaIncrease);

        itemsCollected++;
        Debug.Log($"เก็บของแล้ว {itemsCollected}/{itemsToCollect}");

        Destroy(gameObject); // ทำให้ไอเท็มหายไป

        if (itemsCollected >= itemsToCollect)
        {
            PlayCutscene();
        }
    }

    void PlayCutscene()
    {
        if (cutsceneObject != null)
        {
            cutsceneObject.SetActive(true);
            Debug.Log("เล่น Cutscene แล้ว!");
        }
        else
        {
            Debug.LogWarning("ยังไม่ได้ใส่ Cutscene Object ใน Inspector");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}