using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("wmfweo");
            SceneManager.LoadScene((int)ESceneEnum.LakeTown);
        }
       //LoadNewScene();
    }


    void LoadNewScene()
    {

    }

}
