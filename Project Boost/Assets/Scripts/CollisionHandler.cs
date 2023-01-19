using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;  // 충돌이 발생하면 아무것도 안되고, 못하게 하는 변수 
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() 
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        { 
            collisionDisabled = !collisionDisabled;  // toggle collision 
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }

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
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;  

        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartClearSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
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
