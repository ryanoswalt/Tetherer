using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private AudioSource AudSource;
    [SerializeField] private AudioClip BoomClip;
    [SerializeField] private List<MeshRenderer> tempList;
    
    

    private void Start()
    {
        AudSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.CompareTag("T") || col.gameObject.CompareTag("KT"))
        {
            AudSource.clip = BoomClip;
            AudSource.Play();
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponentsInChildren<MeshRenderer>(tempList);
            foreach (MeshRenderer i in tempList)
            {
                i.enabled = false;
            }
            Destroy(this.gameObject, 1f);
        }
        
    }

}
