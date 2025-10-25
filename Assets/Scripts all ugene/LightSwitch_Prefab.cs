using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    // *** แก้ไขแล้ว: เราจะอ้างอิงถึง "ส่วนประกอบแสง" โดยตรง ***
    public Light lightComponent;

    // ตัวแปรเก็บสถานะ (false = ปิด, true = เปิด)
    private bool isLightOn = false;

    // ฟังก์ชันนี้จะถูกเรียกโดย "ผู้เล่น" (PlayerInteract)
    public void Interact()
    {
        // สลับสถานะ
        isLightOn = !isLightOn;

        // *** แก้ไขแล้ว: เราจะ "เปิด/ปิด" เฉพาะ "ส่วนประกอบ" (Component) นี้ ***
        // .enabled คือการติ๊กถูกที่ "ช่อง" ของ Component (ส่วนประกอบ) ครับ
        lightComponent.enabled = isLightOn;
    }
}