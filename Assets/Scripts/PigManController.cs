using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class PigManController : MonoBehaviour
{
    private Animator anim;
    private bool isActivated;
    public GameObject Player;
    private Rigidbody pRb;
    public float speed;
    public float hitPower;
    private int BombCounter = 0;
    private float timer;
    private bool comingAtYou = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timer = 0;
        isActivated = false;
        pRb = Player.GetComponent<Rigidbody>();
        anim.SetBool("IsOn", false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(Player.transform.position, this.transform.position) <= 80 && isActivated == false)
        {
            isActivated = true;
        }
        if (isActivated && Vector3.Distance(Player.transform.position, this.transform.position) > 20f && timer > 1)
        {
            Debug.Log(Vector3.Distance(Player.transform.position, this.transform.position));
            //make anim
            anim.SetBool("CanAttack", false);
            anim.SetBool("IsHit", false);
            anim.SetBool("IsOn", true);
            //move at player
            transform.LookAt(Player.transform);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            comingAtYou = true;
        }
        else if (isActivated && Vector3.Distance(Player.transform.position, this.transform.position) <= 20f)
        {
            anim.SetBool("IsOn", false);
            anim.SetBool("CanAttack", true);
            Vector3 temp = (Player.transform.position - transform.position);
            if (comingAtYou)
            {
                pRb.AddForce(temp * hitPower, ForceMode.VelocityChange);
            }
            comingAtYou = false;
            Debug.Log("Hit");
        }
    }
    private void OnTriggerEnter(Collider col)
    {
       
        if (col.gameObject.name == "bomb 1" || col.gameObject.name == "bomb 1(Clone)")
        {
            anim.SetBool("IsHit", true);
            timer = 0;
            Debug.Log("PigHit");
            BombCounter++;
            if (BombCounter >= 5)
            {
                Destroy(this.gameObject, 1f);
            }
        }
        
    }
   }
