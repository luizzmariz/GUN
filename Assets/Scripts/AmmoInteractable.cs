using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInteractable : Interactable
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
            gameObject.transform.SetParent(gunHolder.transform);
            gunHolder.transform.GetChild(0).localPosition = new Vector3(0,0,0);
            gunHolder.transform.GetChild(0).localRotation = new Quaternion(0,1,0,0);
        }
        else
        {
            if(gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>() != null)
            {
                gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>().PickOrDrop();
            }
            gunHolder.transform.GetChild(0).position = gameObject.transform.position;
            gunHolder.transform.GetChild(0).SetParent(itens.transform);
            gameObject.transform.SetParent(gunHolder.transform);
            gunHolder.transform.GetChild(0).localPosition = new Vector3(0,0,0);
            gunHolder.transform.GetChild(0).localRotation = new Quaternion(0,1,0,0);
        }
    }
}
