using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    AudioSource audioSource;

    bool isTransitioning = false;  // 충돌이 발생하면 아무것도 안되고, 못하게 하는 변수 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) { return; }

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
                isTransitioning = true;
                break;
        }
        
        
    }

    void StartCrashSequence()
    {   
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash, 0.3f);
        GetComponent<Movement>().enabled = false;  // 게임오버 시 플레이어의 이동 제어권을 뺏기 위해 movement 비활성화 
        // todo add particle effect upon crash

        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartClearSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        // todo add particle effect upon success
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
