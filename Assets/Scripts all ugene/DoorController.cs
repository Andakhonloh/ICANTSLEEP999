using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // ตัวแปรเก็บสถานะ (false = ปิด, true = เปิด)
    private bool isOpen = false;

    // องศาที่เราจะให้ประตูเปิดไป (เช่น 90 องศา)
    public float openAngle = 90f;
    // องศาตอนปิด (ปกติคือ 0)
    public float closeAngle = 0f;
    // ความเร็วในการเปิด/ปิด
    public float smoothSpeed = 2f;

    // ตัวแปรเก็บ "เป้าหมาย" การหมุน
    private Quaternion targetRotation;

    void Start()
    {
        // เริ่มต้น ให้ตั้งเป้าหมายไปที่ "ปิด"
        targetRotation = Quaternion.Euler(0, closeAngle, 0);
    }

    void Update()
    {
        // "ค่อยๆ" หมุน (Slerp) ไปหา "เป้าหมาย" ตลอดเวลา
        // (Slerp คือการหมุนแบบสมูท (Smooth))
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothSpeed * Time.deltaTime);
    }

    // ฟังก์ชันนี้จะถูกเรียกโดย "ผู้เล่น" (PlayerInteract)
    public void Interact()
    {
        // สลับสถานะ (จาก ปิด เป็น เปิด, จาก เปิด เป็น ปิด)
        isOpen = !isOpen;

        // ตั้ง "เป้าหมาย" การหมุนใหม่
        if (isOpen)
        {
            // ถ้า "เปิด" ให้ตั้งเป้าหมายไปที่ "openAngle" (90 องศา)
            targetRotation = Quaternion.Euler(0, openAngle, 0);
        }
        else
        {
            // ถ้า "ปิด" ให้ตั้งเป้าหมายกลับไปที่ "closeAngle" (0 องศา)
            targetRotation = Quaternion.Euler(0, closeAngle, 0);
        }
    }
}