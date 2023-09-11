using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunConfig : MonoBehaviour
{
    //Gun sets
    private Transform muzzle;
    public float backspinDrag = 0.02f;
    public float springForce = 1.2f;

    //Bullet sets
    public GameObject bullet;
    private GameObject bulletSpawned;

    //UI sets
    public TMP_Text bd;
    public TMP_Text bulletSpeed;
    
    void Start()
    {
        muzzle = this.gameObject.transform.GetChild(3);
        bd.text = ""+backspinDrag;

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            backspinDrag += Input.mouseScrollDelta.y * 0.0005f;
            bd.text = ""+backspinDrag;
        }
    }

    void FireBullet()
    {
        //bulletSpawned = Instantiate(bullet, muzzle.position, muzzle.rotation, muzzle);
        bulletSpawned = Instantiate(bullet, muzzle.position, muzzle.rotation);
        bulletSpawned.GetComponent<BulletConfig>().backspinDrag = this.backspinDrag;
        bulletSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * -1 * springForce);

        StartCoroutine(bulletCheck(bulletSpawned));
    }

    IEnumerator bulletCheck(GameObject bulletSpawned)
    {
        yield return new WaitForSeconds(0.015f);
        bulletSpeed.text = bulletSpawned.GetComponent<Rigidbody>().velocity.magnitude+" m/s";
    }
}
