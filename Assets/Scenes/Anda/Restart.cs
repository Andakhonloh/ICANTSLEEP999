using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [Header("ชื่อ Scene สำหรับเริ่มใหม่")]
    public string sceneToRestart = "SampleScene"; // ตั้งชื่อฉากหลักของอันดาได้เลย

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneToRestart);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();

        // ใช้ตอนทดสอบใน Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
