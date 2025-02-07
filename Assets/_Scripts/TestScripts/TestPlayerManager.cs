using UnityEngine;
using UnityEngine.Rendering;

public class TestPlayerManager : MonoBehaviour
{
    public GameObject player;

    #region Singelton
    public static TestPlayerManager instance;

    void Awake(){
        instance = this;
    }

    #endregion


}
