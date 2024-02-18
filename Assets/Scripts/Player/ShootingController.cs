using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private Transform attackPosition;

    [SerializeField]
    private Transform meleeRotation;

    [SerializeField]
    private Transform rangeRotation;

    [SerializeField]
    private Transform rangeWeapon;

    [SerializeField]
    private GameObject weaponRotation;
    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;
    private Vector3 mousePos;
    private Camera mainCam;
    private WeaponSO weaponData;
    private AudioSource sound;

    [SerializeField]
    private RangeAnimation shoot;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sound = GetComponent<AudioSource>();
        sound.volume = 0.01f;
    }

    void Update()
    {
        ItemSO item = InventoryManager.instance.GetSelectedItem(false);
        if (item != null && item.itemType == ItemType.Weapon)
        {
            weaponData = (WeaponSO)item;
            if (weaponData.weaponType == WeaponType.melee)
            {
                return;
            }
            RotateGun();
            if (Input.GetKey(KeyCode.Mouse0))
            {
                float timeSinceLastFire = Time.time - _lastFireTime;
                if (timeSinceLastFire >= weaponData.rangeCooldown)
                {
                    FireBullet();
                    _lastFireTime = Time.time;
                    shoot.ShootWeapon(weaponData);
                }
            }
        }
    }

    private void FireBullet()
    {
        sound.Play();
        GameObject bullet = Instantiate(
            weaponData.projectilePrefab,
            attackPosition.position,
            transform.rotation
        );
        bullet.GetComponent<BulletController>().weaponData = weaponData;
        bullet.GetComponent<BulletController>().OnCollision += CollisionBehaviour;
    }

    void RotateGun()
    {
        //Rotates the Gun

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - weaponRotation.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        weaponRotation.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // meleeRotation.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // rangeRotation.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // if (
        //     rangeRotation.transform.eulerAngles.z > 90
        //     && rangeRotation.transform.eulerAngles.z < 270
        // )
        // {
        //     rangeWeapon.GetComponent<SpriteRenderer>().flipY = true;
        // }
        // else
        // {
        //     rangeWeapon.GetComponent<SpriteRenderer>().flipY = false;
        // }
    }

    void CollisionBehaviour(Collider2D collision)
    {
        if (weaponData.rangeDebuff)
        {
            collision
                .GetComponent<DebuffController>()
                .ApplyDebuff(weaponData.rangeDebuff, weaponData.rangeDebuffDuration);
        }
        if (weaponData.rangeDOT)
        {
            collision
                .GetComponent<DebuffController>()
                .ApplyDOT(
                    weaponData.rangeDOT,
                    weaponData.rangeDOTDuration,
                    weaponData.rangeDOTDamage
                );
        }
    }
}
