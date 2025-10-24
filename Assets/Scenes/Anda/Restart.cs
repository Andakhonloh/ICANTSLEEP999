using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    [Header("ชื่อ Scene ที่ต้องการเปลี่ยนไป")]
    public string sceneName; // ใส่ชื่อ Scene ได้ใน Inspector

    public void ChangeScene()
    {
        // ตรวจว่าชื่อ Scene ไม่ว่าง
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("กำลังเปลี่ยนไปที่ฉาก: " + sceneName);
        }
        else
        {
            Debug.LogWarning("⚠️ กรุณาใส่ชื่อ Scene ใน Inspector ก่อน!");
        }
    }
}