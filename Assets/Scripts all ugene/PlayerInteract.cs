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
        // วาดเลเซอร์สีเขียว (สำหรับ Debug (ดีบัก))
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactRange, Color.green);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("--- กดปุ่ม E แล้ว ---");

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange))
            {
                Debug.Log("เลเซอร์ชน: " + hit.collider.name);

                // (เช็กที่ 1) เช็กสวิตช์ไฟ
                if (hit.collider.TryGetComponent(out LightSwitch lightSwitch))
                {
                    Debug.Log("...เจอสคริปต์ LightSwitch! กำลังสั่ง Interact()...");
                    lightSwitch.Interact();
                }

                // --- (อัปเกรดแล้ว!) ---
                // (เช็กที่ 2) "หรือถ้า" (else if) ลองหา "ประตู" บนตัวมันเอง
                else if (hit.collider.TryGetComponent(out DoorController doorOnSelf))
                {
                    Debug.Log("...เจอสคริปต์ DoorController! (บน " + hit.collider.name + ") กำลังสั่ง Interact()...");
                    doorOnSelf.Interact();
                }
                // (เช็กที่ 3) "หรือถ้า" (else if) ลองหา "ประตู" บน "ตัวแม่" (Parent)
                // (GetComponentInParent คือการ "ค้นหาในตัวแม่")
                else if (hit.collider.GetComponentInParent<DoorController>() != null)
                {
                    // เราต้องเรียก GetComponentInParent() อีกครั้งเพื่อสั่งการ
                    Debug.Log("...เจอสคริปต์ DoorController! (บน Parent) กำลังสั่ง Interact()...");
                    hit.collider.GetComponentInParent<DoorController>().Interact();
                }
                // --- (จบส่วนอัปเกรด) ---

                else
                {
                    Debug.Log("...แต่บน " + hit.collider.name + " หรือ Parent ของมัน ไม่มีสคริปต์ที่ Interact (ปฏิสัมพันธ์) ได้");
                }
            }
            else
            {
                Debug.Log("เลเซอร์ไม่ชนอะไรเลย (ยิงทะลุ/ระยะไม่ถึง)");
            }
        }
    }
}