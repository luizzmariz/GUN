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
    public bool hasAmmo;
    public GameObject ammo;
    public float ammoQnt = 0;
    public GameObject bullet;
    private GameObject bulletSpawned;

    //UI and world sets
    public TMP_Text bd;
    public TMP_Text bulletSpeed;    
    public TMP_Text magazineBullets;
    public TMP_Text ammoType;
    void Start()
    {
        muzzle = this.gameObject.transform.GetChild(3);
        onHand = false;
        hasAmmo = false;

        bd = GameObject.Find("BackspinDrag").GetComponent<TMP_Text>();
        bulletSpeed = GameObject.Find("BulletSpeed").GetComponent<TMP_Text>();
        magazineBullets = GameObject.Find("MagazineBullets").GetComponent<TMP_Text>();
        ammoType = GameObject.Find("AmmoType").GetComponent<TMP_Text>();
    }

    void Update()
    {
        
    }

    public void PickOrDrop()
    {
        if(!onHand)
        {
            onHand = true;
            bd.text = ""+backspinDrag;
            magazineBullets.text = "No bullets";
            ammoType.text = "";
            bulletSpeed.text = "";
        }
        else
        {
            onHand = false;
            bd.text = "";
            magazineBullets.text = "";
            ammoType.text = "";
            bulletSpeed.text = "";
        }
    }

    public void ReloadAmmo(GameObject ammunition)
    {
        hasAmmo = true;
        this.ammo = ammunition;
        this.ammoQnt = ammo.GetComponent<AmmoConfig>().ammoQnt;
        this.bullet = ammo.GetComponent<AmmoConfig>().bulletType;
        magazineBullets.text = ""+ammoQnt;
        ammoType.text = ammo.GetComponent<AmmoConfig>().bulletTyp;
    }

    public void ChangeHopup(float Input)
    {
        backspinDrag += Input * 0.0005f;
        bd.text = ""+backspinDrag;
    }

    public void FireBullet()
    {
        if(onHand && hasAmmo)
        {
            bulletSpawned = Instantiate(bullet, muzzle.position, muzzle.rotation);
            bulletSpawned.GetComponent<BulletConfig>().backspinDrag = this.backspinDrag;
            bulletSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * -1 * springForce);

            CheckAmmo();

            StartCoroutine(bulletCheck(bulletSpawned));
        }
    }

    public void CheckAmmo()
    {
        if(hasAmmo)
        {
            ammoQnt--;
            magazineBullets.text = ""+ammoQnt;
            if(ammoQnt == 0)
            {
                hasAmmo = false;
            }
        }
    }

    IEnumerator bulletCheck(GameObject bulletSpawned)
    {
        yield return new WaitForSeconds(0.015f);
        bulletSpeed.text = bulletSpawned.GetComponent<Rigidbody>().velocity.magnitude+" m/s";
    }
}
