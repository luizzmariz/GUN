using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunConfig : MonoBehaviour
{
    [Header("Gun Sets")]
    private Transform muzzle;
    public float backspinDrag = 0.02f;
    public float springForce = 1.2f;
    public bool onHand;
    public bool hasMultipleFR;
    //private float myTime = 0.0F;
    //public float fireDelta = 0.5F;
    //private float nextFire = 0.5F;

    [Header("Bullet Sets")]
    public bool hasAmmo;
    public GameObject ammo;
    public float ammoQnt = 0;
    public float maxAmmoQnt = 0;
    public GameObject bullet;
    private GameObject bulletSpawned;

    [Header("UI and World Sets")]
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
        //myTime = myTime + Time.deltaTime;
    }

    public void PickOrDrop()
    {
        if(!onHand)
        {
            onHand = true;
            bd.text = "Hop-Up: "+backspinDrag;
            bulletSpeed.text = "";
            if(hasAmmo)
            {
                magazineBullets.text = ammoQnt+" / "+maxAmmoQnt;
                ammoType.text = "Ammo type: "+ammo.GetComponent<AmmoConfig>().bulletTyp;
            }
            else
            {
                magazineBullets.text = "No bullets";
                ammoType.text = "";
            }
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
        this.maxAmmoQnt = ammo.GetComponent<AmmoConfig>().ammoQnt;
        this.bullet = ammo.GetComponent<AmmoConfig>().bulletType;
        magazineBullets.text = ammoQnt+" / "+maxAmmoQnt;
        ammoType.text = "Ammo type: "+ammo.GetComponent<AmmoConfig>().bulletTyp;
    }

    public void ChangeHopup(float Input)
    {
        backspinDrag += Input * 0.0005f;
        bd.text = "Hop-Up: "+backspinDrag;
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

            /*if(myTime > nextFire)
            {
                nextFire = myTime + fireDelta;
                nextFire = nextFire - myTime;
                myTime = 0.0F;
            }*/
            
        }
    }

    public void CheckAmmo()
    {
        if(hasAmmo)
        {
            ammoQnt--;
            magazineBullets.text = ammoQnt+" / "+maxAmmoQnt;
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
