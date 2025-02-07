using UnityEngine;

public class PebbleController : MonoBehaviour
{
    public Vector3 aimPos;
    public float pebbleSpeed = 500f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * pebbleSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
