using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName; // ใส่ชื่อ scene ถัดไปใน Inspector

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
