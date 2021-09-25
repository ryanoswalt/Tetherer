using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBreakController : MonoBehaviour
{
    public GameObject child1;
    public GameObject child2;
    private Rigidbody thisRb;
    private Rigidbody rb1;
    private Rigidbody rb2;
    public List<Collider> childCols;

    // Start is called before the first frame update
    void Start()
    {
        if (child1 == null || child2 == null)
        {
            Debug.Log("No children in script");
        }
        //gets children rigidbody and set it to not move
        rb1 = child1.GetComponent<Rigidbody>();
        rb2 = child2.GetComponent<Rigidbody>();
        thisRb = GetComponent<Rigidbody>();

        rb1.useGravity = false;
        rb1.isKinematic = true;

        rb2.useGravity = false;
        rb2.isKinematic = true;

        childCols.AddRange(child1.GetComponents<Collider>());
        childCols.AddRange(child2.GetComponents<Collider>());

        foreach (Collider col in childCols)
        {
            col.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.velocity.x >= 2 || collision.rigidbody.velocity.y >= 2 || collision.rigidbody.velocity.z >= 2 || rb1.velocity.x >= 2 || rb1.velocity.y >= 2 || rb1.velocity.z >= 2 || rb2.velocity.x >= 2 || rb2.velocity.y >= 2 || rb2.velocity.z >= 2)
        {
            this.gameObject.gameObject.GetComponent<Collider>().enabled = false;
            thisRb.isKinematic = true;

            rb1.useGravity = true;
            rb1.isKinematic = false;

            rb2.useGravity = true;
            rb2.isKinematic = false;

            foreach (Collider col in childCols)
            {
                col.enabled = true;
            }
        }
    }
}
