using UnityEngine;
using UnityEngine.Rendering;

public class TestPlayerManager : MonoBehaviour
{
    #region Singelton
    public static TestPlayerManager instance;

    void Awake(){
        instance = this;
    }

    #endregion

    public GameObject player;

}
