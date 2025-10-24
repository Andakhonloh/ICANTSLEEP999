using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // ความไวเมาส์ (Mouse Sensitivity)
    // public คือการทำให้เราไปปรับค่านี้ในหน้าต่าง Inspector ของ Unity ได้
    public float mouseSensitivity = 100f;

    // ตัวแปรสำหรับอ้างอิงถึง "ร่างกาย" ของผู้เล่น (ตัว Player หลัก)
    public Transform playerBody;

    // ตัวแปรภายในเพื่อเก็บค่าการหมุนแกน X (มองขึ้น-ลง)
    private float xRotation = 0f;

    // ฟังก์ชัน Start จะทำงาน "ครั้งเดียว" เมื่อเกมเริ่ม
    void Start()
    {
        // คำสั่งนี้จะซ่อนและล็อกเมาส์ไว้กลางจอตอนเริ่มเกม
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ฟังก์ชัน Update จะทำงาน "ทุกเฟรม" ตลอดเวลาที่เกมรันอยู่
    void Update()
    {
        // 1. รับค่าการขยับเมาส์
        // Input.GetAxis("Mouse X") คือการขยับเมาส์ซ้าย-ขวา
        // Input.GetAxis("Mouse Y") คือการขยับเมาส์ขึ้น-ลง
        // Time.deltaTime คือ "เวลา" ที่ใช้ใน 1 เฟรม (เพื่อให้ความเร็วคงที่ในคอมทุกเครื่อง)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 2. การมองขึ้น-ลง (หมุนแกน X)
        // เราจะเก็บค่าการหมุนไว้ในตัวแปร xRotation
        // ต้องลบค่า mouseY เพราะการขยับเมาส์ขึ้น (ค่าบวก) คือการมอง "ลง" (หมุนแกน X ลบ)
        xRotation -= mouseY;

        // 3. "จำกัด" การมองไม่ให้ตีลังกา
        // ใช้ Mathf.Clamp เพื่อ "หนีบ" ค่า xRotation ให้อยู่ระหว่าง -90 (มองฟ้าสุด) ถึง 90 (มองดินสุด)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 4. สั่งให้ "กล้อง" (ที่สคริปต์นี้แปะอยู่) หมุนขึ้น-ลง
        // Quaternion.Euler คือการกำหนดค่าการหมุนเป็นองศา
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 5. สั่งให้ "ร่างกาย" (playerBody) หมุนซ้าย-ขวา
        // เราใช้ Vector3.up (แกน Y) เพราะการหมุนซ้าย-ขวา คือการหมุนรอบแกน Y
        playerBody.Rotate(Vector3.up * mouseX);
    }
}