using UnityEngine;

public class Itempichup : MonoBehaviour
{
    public float pickupRange = 2f;
    public float staminaIncrease = 60f;
    private Transform player;
    private bool isInRange = false;

    private static int itemsCollected = 0;
    public static int itemsToCollect = 3;

    private UIStamina staminaSystem; // ✅ ใช้ชื่อคลาสที่ถูกต้อง
    public GameObject cutsceneObject;    // ใส่ Cutscene ที่นี่ (ตั้งค่าใน Inspector)

    void Start()
    {
        // หา Player ใน Scene (ต้องมี Tag = "Player")
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // หา StaminaSystem ใน Scene
        staminaSystem = FindObjectOfType<UIStamina>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        isInRange = distance <= pickupRange;

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        if (staminaSystem != null)
        {
            staminaSystem.AddStamina(staminaIncrease);
        }

        itemsCollected++;
        Debug.Log($"เก็บของแล้ว {itemsCollected}/{itemsToCollect}");

        Destroy(gameObject);

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
