using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletConfig : MonoBehaviour
{
    //Gun sets
    public float backspinDrag;

    //Bullet sets
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2);
    }

    void FixedUpdate()
    {
        Vector3 magnusDirection = Vector3.Cross(rb.velocity, transform.right).normalized;
        Vector3 magnusForce = Mathf.Sqrt(rb.velocity.magnitude) * magnusDirection * backspinDrag * Time.fixedDeltaTime;
        
        Debug.DrawRay(transform.position, magnusForce * 1000, Color.red, Mathf.Infinity);
        this.GetComponent<Rigidbody>().AddForce(magnusForce);
        //Debug.Log(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }
}
