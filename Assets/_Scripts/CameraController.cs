using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;

    public GameObject target;

    public float smoothSpeed = 5f;

    public Vector3 offset;

    public float cameraZOffsetMin = -5.58f;
    public float cameraZOffsetMax = -8.25f;

    public Vector3 minCamPosition;
    public Vector3 maxCamPosition;

    private void Start()
    {
        this.transform.parent = null;
    }

    private void LateUpdate()
    {
        //if (player.GetComponent<pPlayerComponent>().isInConversation) return;

        Vector3 desiredPosition = target.transform.position + offset;

        Vector3 clampedPosition = new Vector3(Mathf.Clamp(desiredPosition.x, minCamPosition.x, maxCamPosition.x), Mathf.Clamp(desiredPosition.y, minCamPosition.y, maxCamPosition.y), Mathf.Clamp(desiredPosition.z, minCamPosition.z, maxCamPosition.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothPosition;
    }
}
