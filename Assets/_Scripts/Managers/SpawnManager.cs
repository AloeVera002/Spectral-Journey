using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   public Transform player;
    public Transform spawnPoint; // Assign the spawn point in the Inspector

    private void Start()
    {
        if (PlayerPrefs.HasKey("SpawnX"))
        {
            Vector3 newPosition = new Vector3(
                PlayerPrefs.GetFloat("SpawnX"),
                PlayerPrefs.GetFloat("SpawnY"),
                PlayerPrefs.GetFloat("SpawnZ")
            );

            player.position = newPosition;
        }
        else
        {
            player.position = spawnPoint.position;
        }
    }
}
