using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private Transform weaponTransform;

    [SerializeField]
    private GameObject weaponRotation;
    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;
    private Vector3 mousePos;
    private Camera mainCam;
    private WeaponSO weaponData;

    [SerializeField]
    private LayerMask attackableLayer;

    [SerializeField]
    private MeleeSwing swing;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        ItemSO item = InventoryManager.instance.GetSelectedItem(false);
        if (item != null && item.itemType == ItemType.Weapon)
        {
            weaponData = (WeaponSO)item;
            if (weaponData.weaponType == WeaponType.range)
            {
                return;
            }
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
                if (timeSinceLastFire >= weaponData.meleeCooldown)
                {
                    PerformAttack();
                    _lastFireTime = Time.time;
                    swing.SwingWeapon();
                }
            }
        }
    }

    private void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            weaponTransform.position,
            weaponData.meleeRange,
            attackableLayer
        );
        foreach (Collider2D enemy in hitEnemies)
        {
            var healthController = enemy.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(weaponData.meleeDamage);
            if (weaponData.meleeDebuff)
            {
                enemy
                    .GetComponent<BuffDebuffController>()
                    .ApplyDebuff(weaponData.meleeDebuff, weaponData.meleeDebuffDuration);
            }
        }
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

        Vector3 rotation = mousePos - weaponRotation.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        weaponRotation.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
