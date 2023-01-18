using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        //tag는 상수 
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                Debug.Log("Congrats, you finished!");
                break;
            case "Fuel":
                Debug.Log("You picked up Fuel");
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // 현재 활동하고 있는 씬의 index (build setting에 있는) 반환 
        SceneManager.LoadScene(currentSceneIndex);   
    }
}
