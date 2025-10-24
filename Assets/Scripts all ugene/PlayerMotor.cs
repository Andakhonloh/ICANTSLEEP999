using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// คำสั่งนี้ "บังคับ" ให้ GameObject (วัตถุในเกม) ที่แปะสคริปต์นี้
// "ต้องมี" ส่วนประกอบ CharacterController ด้วย (ป้องกันการลืมใส่)
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    // ตัวแปรสำหรับอ้างอิงถึง "ร่างกาย" (แคปซูลล่องหน)
    private CharacterController controller;

    // ความเร็วในการเดิน
    public float speed = 12f;

    // แรงโน้มถ่วง (Gravity)
    public float gravity = -9.81f;

    // ตัวแปรเก็บค่าความเร็วในการตก (แนวดิ่ง)
    private Vector3 velocity;

    // ตัวแปรสำหรับเช็กว่า "เท้า" แตะพื้นอยู่หรือไม่
    private bool isGrounded;

    // เราต้องสร้าง "จุดเช็กพื้น" (Ground Check)
    // 1. ตัวอ้างอิงตำแหน่งของ "เท้า"
    public Transform groundCheck;
    // 2. รัศมีของวงกลมที่จะใช้เช็ก
    public float groundDistance = 0.4f;
    // 3. LayerMask (เลเยอร์มาสก์) เพื่อบอกว่า "อะไรคือพื้น"
    public LayerMask groundMask;


    // ฟังก์ชัน Start จะทำงาน "ครั้งเดียว" เมื่อเกมเริ่ม
    void Start()
    {
        // ค้นหาส่วนประกอบ CharacterController ที่แปะอยู่บน "ตัวเดียวกัน"
        // แล้วเก็บไว้ในตัวแปร controller เพื่อเรียกใช้งาน
        controller = GetComponent<CharacterController>();
    }

    // ฟังก์ชัน Update จะทำงาน "ทุกเฟรม"
    void Update()
    {
        // --- ส่วนของแรงโน้มถ่วง (Gravity) ---

        // 1. เช็กว่าเท้าแตะพื้นหรือไม่
        // Physics.CheckSphere จะสร้าง "ลูกบอลล่องหน" ที่ตำแหน่ง groundCheck
        // เพื่อเช็กว่ามัน "ชน" กับ Layer (เลเยอร์) ที่ชื่อ groundMask หรือไม่
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // 2. ถ้าเท้าแตะพื้น และกำลังตก (velocity.y < 0)
        // ให้รีเซ็ตค่าการตก (velocity.y) ให้เป็นค่าลบน้อยๆ (เช่น -2f)
        // เพื่อให้ตัวละคร "ติด" พื้น ไม่ลอยเล็กน้อย
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // --- ส่วนของการเคลื่อนที่ (Movement) ---

        // 1. รับค่าการกดปุ่มจากคีย์บอร์ด
        // Input.GetAxis("Horizontal") คือปุ่ม A (ซ้าย) และ D (ขวา) (ค่า -1 ถึง 1)
        // Input.GetAxis("Vertical") คือปุ่ม W (หน้า) และ S (หลัง) (ค่า -1 ถึง 1)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 2. คำนวณ "ทิศทาง" ที่จะเดิน
        // transform.right คือ "แกนขวา" ของผู้เล่น
        // transform.forward คือ "แกนหน้า" ของผู้เล่น
        // (เราใช้แกนของผู้เล่น ไม่ใช่แกนของโลก เพื่อให้ "W" คือเดินไปข้างหน้าที่เราหันอยู่เสมอ)
        Vector3 move = transform.right * x + transform.forward * z;

        // 3. สั่งให้ Character Controller "เคลื่อนที่"
        // เราใช้ .Move() แทนการเปลี่ยน Position (ตำแหน่ง) ตรงๆ
        // เพราะ .Move() จะจัดการเรื่อง "การชน" กำแพงให้เราอัตโนมัติ
        controller.Move(move * speed * Time.deltaTime);

        // --- ส่วนของการตก (Applying Gravity) ---

        // 4. เพิ่มแรงโน้มถ่วง (gravity) เข้าไปในความเร็วการตก (velocity)
        // (ต้องคูณ Time.deltaTime สองครั้ง เพราะแรงโน้มถ่วงคือความเร่ง)
        velocity.y += gravity * Time.deltaTime;

        // 5. สั่งให้ Character Controller "เคลื่อนที่" ลงตามแรงโน้มถ่วง
        controller.Move(velocity * Time.deltaTime);
    }
}