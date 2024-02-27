using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Animator gunAnimator;
    public Transform gunTransform;
    public GameObject muzzleFlashPrefab;
    public Vector3 muzzleFlashOffset = Vector3.forward; // Adjust the offset relative to gun's transform
    public float recoilDuration = 0.1f;
    public float fireRate = 0.2f;
    public float range = 100f;
    public Camera fpsCam;

    private Vector3 originalGunPosition;
    private bool canShoot = true;
    private GameObject currentMuzzleFlash;

    void Start()
    {
        originalGunPosition = gunTransform.localPosition;
    }

    void Update()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }

        gunAnimator.SetTrigger("Fire");
        StartCoroutine(RecoilCoroutine());

        // Instantiate the muzzle flash prefab and parent it to the gun
        currentMuzzleFlash = Instantiate(muzzleFlashPrefab, gunTransform.TransformPoint(muzzleFlashOffset), Quaternion.identity);
        currentMuzzleFlash.transform.parent = gunTransform;

        canShoot = false;
        Invoke("ResetFireRate", fireRate);
    }

    IEnumerator RecoilCoroutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < recoilDuration)
        {
            float recoilProgress = elapsedTime / recoilDuration;
            Vector3 recoilOffset = Vector3.Lerp(Vector3.zero, -gunTransform.forward * 0.1f, recoilProgress);
            gunTransform.localPosition = originalGunPosition + recoilOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gunTransform.localPosition = originalGunPosition;
    }

    void ResetFireRate()
    {
        canShoot = true;

        // Destroy the current muzzle flash
        if (currentMuzzleFlash != null)
        {
            Destroy(currentMuzzleFlash);
        }
    }
}
