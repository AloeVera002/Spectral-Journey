using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   #region Singelton

   public static PlayerManager instance;

   void Awake(){
    instance = this;
   }
   #endregion

   public GameObject player;
}
