using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public float startDelay;
    public float interval;
    public float elapsedTime;

    public AudioClip shootSound;

    public bool startDelayElapsed;


    public Transform shootpoint;
    public GameObject ProjectilePrefab;

    [Header("Настройки анимации")]
    public Transform GunBarrel;
    public Vector2 LeftLocalPosition;
    public Vector2 RightLocalPosition;
    public float GoBackDuration = 0.1f;
    public float DelayBetweenReturn = 0.05f;
    public float ReturnDuration = 1;

    private void Start()
    {
        elapsedTime = interval;
    }

    void Update()
    {
        if (!startDelayElapsed)
        {
            startDelay -= Time.deltaTime;
            if (startDelay <= 0)
                startDelayElapsed = true;
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= interval)
        {
            Shoot();
            elapsedTime = 0;
        }


    }

    void Shoot()
    {
        Projectile projectile = Instantiate(ProjectilePrefab, shootpoint.position, Quaternion.identity).GetComponent<Projectile>();

        projectile.Initialize(1, shootpoint.right, 10, 3);
        SoudManager.PlaySfx(shootSound);
        StartCoroutine(ShootAnim());



    }

    IEnumerator ShootAnim()
    {
        float distance = Vector2.Distance(RightLocalPosition, LeftLocalPosition);

        //отдача
        while((Vector2)GunBarrel.localPosition != RightLocalPosition)
        {
            GunBarrel.localPosition = Vector2.MoveTowards(GunBarrel.localPosition, RightLocalPosition, distance * Time.deltaTime/ GoBackDuration);
            yield return null;
        }

        yield return new WaitForSeconds(DelayBetweenReturn);


        //возврат
        while ((Vector2)GunBarrel.localPosition != LeftLocalPosition)
        {
            GunBarrel.localPosition = Vector2.MoveTowards(GunBarrel.localPosition, LeftLocalPosition, distance * Time.deltaTime / ReturnDuration);
            yield return null;
        }
    }


}
