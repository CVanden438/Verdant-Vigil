using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform bulletTransform;

    [SerializeField]
    private float _timeBetweenShots;

    [SerializeField]
    private GameObject gunRotation;
    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;
    private Vector3 mousePos;
    private Camera mainCam;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        RotateGun();
        // if (_fireContinuously || _fireSingle)
        // {
        //     float timeSinceLastFire = Time.time - _lastFireTime;

        //     if (timeSinceLastFire >= _timeBetweenShots)
        //     {
        //         FireBullet();

        //         _lastFireTime = Time.time;
        //         _fireSingle = false;
        //     }
        // }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(
            _bulletPrefab,
            bulletTransform.position,
            transform.rotation
        );
    }

    // private void OnFire(InputValue inputValue)
    // {
    //     Debug.Log("FIRE");
    //     _fireContinuously = inputValue.isPressed;

    //     if (inputValue.isPressed)
    //     {
    //         _fireSingle = true;
    //     }
    // }

    void RotateGun()
    {
        //Rotates the Gun

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - gunRotation.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        gunRotation.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
