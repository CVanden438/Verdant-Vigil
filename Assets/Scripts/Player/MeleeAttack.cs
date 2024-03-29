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
    private Vector3 rotation;

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
                    if (weaponData.meleeType == MeleeType.slash)
                    {
                        SlashAttack();
                        swing.SwingAnimation(weaponData);
                    }
                    else if (weaponData.meleeType == MeleeType.stab)
                    {
                        StabAttack();
                        swing.StabAnimation(weaponData);
                    }
                    _lastFireTime = Time.time;
                }
            }
        }
    }

    private void SlashAttack()
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
                    .GetComponent<DebuffController>()
                    .ApplyDebuff(weaponData.meleeDebuff, weaponData.meleeDebuffDuration);
            }
            if (weaponData.meleeDOT)
            {
                enemy
                    .GetComponent<DebuffController>()
                    .ApplyDOT(
                        weaponData.meleeDebuff,
                        weaponData.meleeDebuffDuration,
                        weaponData.meleeDOTDuration
                    );
            }
        }
    }

    private void StabAttack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - weaponTransform.position).normalized;

        RaycastHit2D[] hitEnemies = Physics2D.RaycastAll(
            weaponTransform.position,
            direction,
            weaponData.meleeRange,
            attackableLayer
        );
        foreach (RaycastHit2D enemy in hitEnemies)
        {
            var healthController = enemy.transform.GetComponent<HealthController>();
            healthController.TakeDamage(weaponData.meleeDamage);
            if (weaponData.meleeDebuff)
            {
                enemy.transform
                    .GetComponent<DebuffController>()
                    .ApplyDebuff(weaponData.meleeDebuff, weaponData.meleeDebuffDuration);
            }
            if (weaponData.meleeDebuff)
            {
                enemy.transform
                    .GetComponent<DebuffController>()
                    .ApplyDOT(
                        weaponData.meleeDebuff,
                        weaponData.meleeDebuffDuration,
                        weaponData.meleeDebuffDuration
                    );
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

        rotation = mousePos - weaponRotation.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        weaponRotation.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
