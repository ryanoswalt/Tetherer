using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoMySon : MonoBehaviour {
	public Vector3 center;
	public List<GoMySon> objs;
	private Rigidbody rb;
	public bool going;
	private bool firstFrame;
    private AudioSource AudSource;
    [SerializeField] private AudioClip BonkClip;
	[SerializeField] private GameManager gameManager;
	private bool canEarnPoints = false;

    void Awake() {
		gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
		going = false;
		firstFrame = true;
		rb = GetComponent<Rigidbody>();
        AudSource = GetComponent<AudioSource>();
		StartCoroutine(PlzWait());
	}

	void FixedUpdate() {
		if (going) {
				Vector3 dir = center - transform.position;
				dir.Normalize();
			if (firstFrame) {
                rb.useGravity = false;
				rb.AddForce(dir * 1000, ForceMode.Force);
				firstFrame = false;
			} else {
				rb.AddForce(dir * 75, ForceMode.Force);
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
        foreach (GoMySon o in objs)
			if (other.gameObject == o.gameObject || other.tag == "NT") {
				going = false;
                rb.useGravity = true;
				firstFrame = true;
			}
	}

    private void OnCollisionEnter(Collision collision)
    {
		if(canEarnPoints)
		{
			gameManager.AddPoints();
			AudSource.clip = BonkClip;
			AudSource.Play();
		}
        //play bonk sound
        
    }
	IEnumerator PlzWait()
	{
		yield return new WaitForSeconds(3f);
		canEarnPoints = true;
	}
}
