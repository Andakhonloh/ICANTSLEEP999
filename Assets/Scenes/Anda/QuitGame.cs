using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("ออกจากเกม..."); // แสดงใน Console ตอนทดสอบใน Editor

        // ใช้เวลารันใน Build จริง (เช่น .exe)
        Application.Quit();

        // ถ้าเล่นใน Unity Editor ให้หยุดการเล่นด้วย (เพื่อทดสอบ)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
