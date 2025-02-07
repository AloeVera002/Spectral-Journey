using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Hold down LMB to show a bullet in front of the player
    // Release LMB to shoot the bullet
    // The bullet should be a prefab
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPlaceHolder;

    [SerializeField] private float bulletForce = 100f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletPlaceHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

        float rotation = Input.GetAxis("Horizontal");
        float translation = Input.GetAxis("Vertical");

        transform.Translate(0, 0, Time.deltaTime * translation * movementSpeed);
        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);
    }

    private void OnMouseDown()
    {
        bulletPlaceHolder.SetActive(true);
    }

    private void OnMouseUp()
    {
        bulletPlaceHolder.SetActive(false);
        ShootBullet();
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * bulletForce);
    }
}
