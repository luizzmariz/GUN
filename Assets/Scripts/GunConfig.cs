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
    public bool onHand;

    //Bullet sets
    public GameObject bullet;
    private GameObject bulletSpawned;

    //UI and world sets
    public TMP_Text bd;
    public TMP_Text bulletSpeed;    
    void Start()
    {
        muzzle = this.gameObject.transform.GetChild(3);
        onHand = false;

        bd = GameObject.Find("BackspinDrag").GetComponent<TMP_Text>();
        bulletSpeed = GameObject.Find("BulletSpeed").GetComponent<TMP_Text>();
    }

    void Update()
    {
        
    }

    public void PickOrDrop()
    {
        bd.text = ""+backspinDrag;
        onHand = !onHand;
    }

    public void ChangeHopup(float Input)
    {
        backspinDrag += Input * 0.0005f;
        bd.text = ""+backspinDrag;
    }

    public void FireBullet()
    {
        if(onHand)
        {
            bulletSpawned = Instantiate(bullet, muzzle.position, muzzle.rotation);
            bulletSpawned.GetComponent<BulletConfig>().backspinDrag = this.backspinDrag;
            bulletSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * -1 * springForce);

            StartCoroutine(bulletCheck(bulletSpawned));
        }
    }

    IEnumerator bulletCheck(GameObject bulletSpawned)
    {
        yield return new WaitForSeconds(0.015f);
        bulletSpeed.text = bulletSpawned.GetComponent<Rigidbody>().velocity.magnitude+" m/s";
    }
}
