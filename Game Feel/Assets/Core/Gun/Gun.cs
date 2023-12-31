using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Gun : MonoBehaviour
{
    public static event Action  OnShoot;

    
    private Animator myAnim;
    private CinemachineImpulseSource impulseSource;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject muzzleFalsh;

    [SerializeField] private float gunCd;
    private float lastFire;

    private Vector2 mousePos;

    private   ObjectPooler objectPooler;

    private readonly int Fire_Has = Animator.StringToHash("Gun_Fire");

    private Coroutine muzzleFlashRoutine;
    PlayerController player;
    private void OnEnable()
    {
        OnShoot += ShootProjectile;
        OnShoot += ResrtLastFire;
        OnShoot += FireAnimation;
        OnShoot += MuzzleFlash;
        OnShoot += ScreenShake;
    }
    private void OnDisable()
    {
        OnShoot -= ShootProjectile;
        OnShoot -= ResrtLastFire;
        OnShoot -= FireAnimation;
        OnShoot -= MuzzleFlash;
        OnShoot -= ScreenShake;
    }

    private void Start()
    {
        player = PlayerController.Instance;
        objectPooler = ObjectPooler.instance;
        impulseSource =GetComponent<CinemachineImpulseSource>();
        myAnim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Shoot();
        GunRotation();
    }

    private void GunRotation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localDirection = player.transform.InverseTransformPoint(mousePos);     // Calculate the direction from the object to the mouse in local space
        float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && lastFire  <= Time.time)
        {
            OnShoot?.Invoke();
        }
    }

    private void ResrtLastFire() => lastFire = Time.time + gunCd;
    private void FireAnimation() => myAnim.Play(Fire_Has, 0, 0);
    private void ScreenShake() => impulseSource.GenerateImpulse();

    private void ShootProjectile()
    {
        GameObject newBullet = objectPooler.GetObjectFormPool(_bulletPrefab, _bulletSpawnPoint.position);
        if(newBullet != null)
            newBullet?.GetComponent<Bullet>().SetUp(player.transform.position, mousePos); ;

    }
    private void MuzzleFlash()
    {
        if(muzzleFlashRoutine!=null)
            StopCoroutine(muzzleFlashRoutine);

        muzzleFlashRoutine = StartCoroutine(MuzzleFlashDelay());
    }
    IEnumerator MuzzleFlashDelay()
    {
        muzzleFalsh.SetActive(true);
        yield return new WaitForSeconds(.08f);
        muzzleFalsh.SetActive(false);

    }
}
