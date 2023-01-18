using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    
    void OnCollisionEnter(Collision other) 
    {
        //tag는 상수 
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                StartClearSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;  // 게임오버 시 플레이어의 이동 제어권을 뺏기 위해 movement 비활성화 
        GetComponent<AudioSource>().Stop();   // movement 비활성화로 spacebar를 누르지 않을 때가 실행이 안돼서..? 직접 멈춤
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartClearSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   
        SceneManager.LoadScene(currentSceneIndex);   
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;    
        int nextSceneIndex = currentSceneIndex + 1; 
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
