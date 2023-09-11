using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interact with objects in space")]
    [SerializeField] Camera cam;
    [SerializeField] float dist = 5f;
    [SerializeField] LayerMask mask;
    public TMP_Text iText;

    [Header("Interact with the gun")]

    public bool gunInHand;
    [SerializeField] GameObject gunHolder;

    

    // Start is called before the first frame update
    void Start()
    {
        gunInHand = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, dist, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                iText.text = "Press F to interact with " + hitInfo.collider.GetComponent<Interactable>().promptMessage;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    hitInfo.collider.GetComponent<Interactable>().BaseInteract();
                }
            }
        } 
        else 
        {
            iText.text = "";
        }

        if(gunHolder.transform.childCount != 0 && gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>() != null)
        {
            gunInHand = true;
        }
        else
        {
            gunInHand = false;
        }

        //fire gun

        if(Input.GetButtonDown("Fire1") && gunInHand)
        {
            gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>().FireBullet();
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            gunHolder.transform.GetChild(0).gameObject.GetComponent<GunConfig>().ChangeHopup(Input.mouseScrollDelta.y);
        }
    }
}
