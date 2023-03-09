using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Bullet prefabToUse;
    PlayerBullet playerPrefabToUse;
    public Transform firePoint;
    public float fireForce = 20f;
    
    public void Fire (int stamina)
    {
        Bullet[] bulletPrefabs = Resources.LoadAll<Bullet>("Bullets");
        if (stamina > 5)
        {
            int randomNumber = Random.Range(0, bulletPrefabs.Length);
            prefabToUse = bulletPrefabs[randomNumber];
            Bullet bullet = Instantiate(prefabToUse, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }


    }

    public void PlayerFire(int stamina)
    {
        PlayerBullet[] bulletPrefabs = Resources.LoadAll<PlayerBullet>("Bullets");
        if (stamina > 5)
        {
            playerPrefabToUse = bulletPrefabs[0];
            PlayerBullet bullet = Instantiate(playerPrefabToUse, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }


    }

}
