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
        if(gunHolder.transform.childCount != 0 && gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>() != null)
        {
            gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>().ReloadAmmo(gameObject);
        }
        else
        {
            Debug.Log("You must hold a gun first");
        }
    }
}
