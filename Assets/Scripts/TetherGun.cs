using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TetherGun : MonoBehaviour {
	private List<GoMySon> toTether;
	private Transform cam;
	private Vector3 center;
	private bool noTether = false;
	[SerializeField] private GameObject tempPrefab;
	public GameObject tetherModePanel;
    //for sounds
    private AudioSource AudSource;
    [SerializeField] private AudioClip TimeFreezeClip;
    [SerializeField] private AudioClip TimeStartClip;
	[SerializeField] GameManager gameManager;

	void Awake() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		cam = Camera.main.transform;
        AudSource = GetComponent<AudioSource>();
    }

	void Update() {
		if (!gameManager.isPaused)
		{
			//Start the Tether sequence
			if (CrossPlatformInputManager.GetButtonDown("Fire1") && !noTether)
			{
				StopCoroutine("Tethering");
				StopCoroutine("KeepingTrack");
				//Play Freeze Sound
				AudSource.clip = TimeFreezeClip;
				AudSource.Play();
				StartCoroutine("Tethering");
			}
		}
	}

	//Tether Sequence
	IEnumerator Tethering() {
		//Setup
		//Chase Addition
		tetherModePanel.SetActive(true);
		noTether = true;
		Time.timeScale = 0;
		toTether = new List<GoMySon>();
		Debug.Log("Start Tether");
		yield return new WaitForEndOfFrame();

		while (true) {
			//Wait till Mouse click
			yield return new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("Fire1") || CrossPlatformInputManager.GetButtonDown("Fire2"));
			//Exit Tether Sequence (Right click mouse)
			if (CrossPlatformInputManager.GetButtonDown("Fire2"))
            {
                AudSource.clip = TimeStartClip;
                AudSource.Play();
                break;
            }
			
			//Left click (Adds an object to tether)
			else {
				Ray ray = new Ray(cam.position, cam.forward);
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(cam.position, cam.forward, out hit)) {
					Debug.Log(hit.collider.gameObject.name);
					GameObject gm = hit.collider.gameObject;
					if (gm.tag == "T")
						toTether.Add(gm.GetComponent<GoMySon>());
					else if (gm.tag == "KT")	//Adds a temporary object instead of using entire wall or ceiling
						toTether.Add(Instantiate(tempPrefab, hit.point, Quaternion.identity).GetComponent<GoMySon>());
				}
			}
			yield return new WaitForEndOfFrame();
		}

		//Start calculating the center of selected objects until they all hit each other
		if (toTether.Count > 1)
			StartCoroutine("KeepingTrack");
		//Closure of Tether Sequence
		noTether = false;
		Debug.Log("Stop Tether");
		//Chase Addition
		tetherModePanel.SetActive(false);
		Time.timeScale = 1;
	}

	IEnumerator KeepingTrack() {
		//Calculate Center of objects
		float x = 0, y = 0, z = 0;
		foreach (GoMySon o in toTether) {
			x += o.transform.position.x;
			y += o.transform.position.y;
			z += o.transform.position.z;
		}
		center = new Vector3(x / toTether.Count, y / toTether.Count, z / toTether.Count);

		//Create object at the center which moves and acts as a stop (In case you click two walls and a box)
		GameObject centerStop = Instantiate(tempPrefab, center, Quaternion.identity);

		//Send information to the objects
		for (int i = 0; i < toTether.Count; i++) {
			toTether[i].going = true;
			toTether[i].center = center;
			toTether[i].objs = toTether;
		}

		//Looping until there's no more T objects in the list
		bool hasObjects = true;
		while (hasObjects) {
			hasObjects = false;
			//Removes any T objects that aren't going anymore
			for (int i = 0; i < toTether.Count; i++) {
				if (toTether[i].going && toTether[i].tag == "T")
					hasObjects = true;
				else if (toTether[i].tag == "T") {
					toTether[i].objs = new List<GoMySon>();
					toTether.RemoveAt(i);
				}
			}
			if (!hasObjects)
				break;
			x = 0;
			y = 0;
			z = 0;
			//Recalculating the center of the remaining objects
			for (int i = 0; i < toTether.Count; i++) {
				x += toTether[i].transform.position.x;
				y += toTether[i].transform.position.y;
				z += toTether[i].transform.position.z;
			}
			center = new Vector3(x / toTether.Count, y / toTether.Count, z / toTether.Count);

			//Recentering center stop
			centerStop.transform.position = center;

			//Sending info to objects
			for (int i = 0; i < toTether.Count; i++) {
				toTether[i].center = center;
			}

			yield return new WaitForFixedUpdate();
		}
		//Getting rid of temporary objects
		Destroy(centerStop);
		Debug.Log("Done");
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("NT"))
			Destroy(o);
	}
}