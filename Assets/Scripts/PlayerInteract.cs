using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float dist = 5f;
    [SerializeField] LayerMask mask;

    public TMP_Text iText;

    // Start is called before the first frame update
    void Start()
    {
        
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
                iText.text = "Press F to interact with " + hitInfo.collider.gameObject.name;
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
    }
}
