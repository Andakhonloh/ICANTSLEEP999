using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera playerCamera;
    public float interactRange = 3f;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("PlayerInteract: ไม่พบ Camera Component บนตัวละคร!");
        }
    }

    void Update()
    {
        // === เพิ่มเข้ามาใหม่ (1) ===
        // วาดเส้นเลเซอร์ "สีเขียว" ในหน้าต่าง Scene (ซีน)
        // เพื่อให้เรา "เห็น" ว่าเลเซอร์ยิงไปทางไหน (ต้องเปิด Gizmos (กิซ-โม่) ในหน้าต่าง Game (เกม) ถึงจะเห็น)
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactRange, Color.green);
        // === จบส่วนเพิ่มใหม่ (1) ===

        if (Input.GetKeyDown(KeyCode.E))
        {
            // === เพิ่มเข้ามาใหม่ (2) ===
            // ตะโกนบอกเราว่า "กด E แล้วนะ!"
            Debug.Log("--- กดปุ่ม E แล้ว ---");
            // === จบส่วนเพิ่มใหม่ (2) ===

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange))
            {
                // === เพิ่มเข้ามาใหม่ (3) ===
                // ตะโกนบอกเราว่า "เลเซอร์ชนอะไรบางอย่าง"
                // hit.collider.name คือ "ชื่อ" ของวัตถุที่ชน
                Debug.Log("เลเซอร์ชน: " + hit.collider.name);
                // === จบส่วนเพิ่มใหม่ (3) ===

                if (hit.collider.TryGetComponent(out LightSwitch lightSwitch))
                {
                    Debug.Log("...เจอสคริปต์ LightSwitch! กำลังสั่ง Interact()...");
                    lightSwitch.Interact();
                }
                else
                {
                    // === เพิ่มเข้ามาใหม่ (4) ===
                    // ตะโกนบอกว่า "ชนแล้ว แต่... ไม่มีสคริปต์ LightSwitch"
                    Debug.Log("...แต่บน " + hit.collider.name + " ไม่มีสคริปต์ LightSwitch");
                    // === จบส่วนเพิ่มใหม่ (4) ===
                }
            }
            else
            {
                // === เพิ่มเข้ามาใหม่ (5) ===
                // ตะโกนบอกว่า "ยิงเลเซอร์แล้ว... แต่ไม่ชนอะไรเลย"
                Debug.Log("เลเซอร์ไม่ชนอะไรเลย (ยิงทะลุ/ระยะไม่ถึง)");
                // === จบส่วนเพิ่มใหม่ (5) ===
            }
        }
    }
}