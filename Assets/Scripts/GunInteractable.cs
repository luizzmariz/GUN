using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    private GameObject gunHolder;
    private GameObject itens;
    // Start is called before the first frame update
    void Start()
    {
        gunHolder = GameObject.Find("Gun Holder");
        itens = GameObject.Find("Itens");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        if(gunHolder.transform.childCount == 0)
        {
            /*
            Destroy(gameObject);
            Instantiate(gameObject, gunHolder.transform.position, gameObject.transform.rotation, gunHolder.transform);
            */
            gameObject.transform.SetParent(gunHolder.transform);
            gunHolder.transform.GetChild(0).localPosition = new Vector3(0,0,0);
            gunHolder.transform.GetChild(0).localRotation = new Quaternion(0,1,0,0);
        }
        else
        {
            /*
            Instantiate(gunHolder.transform.GetChild(0), gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            Destroy(gunHolder.transform.GetChild(0).gameObject);
            Instantiate(gameObject, gunHolder.transform.position, gameObject.transform.rotation, gunHolder.transform);
            */
            gunHolder.transform.GetChild(0).position = gameObject.transform.position;
            gunHolder.transform.GetChild(0).SetParent(itens.transform);
            gameObject.transform.SetParent(gunHolder.transform);
            gunHolder.transform.GetChild(0).localPosition = new Vector3(0,0,0);
            gunHolder.transform.GetChild(0).localRotation = new Quaternion(0,1,0,0);
        }
        Debug.Log("Interacted with " + gameObject.name);
    }
}
