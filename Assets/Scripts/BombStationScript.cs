using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStationScript : MonoBehaviour
{
    [SerializeField] private Transform bombRotation;
    [SerializeField] private GameObject bomb;
    [SerializeField]  private GameObject thisBomb;
    private bool makingBomb = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thisBomb == null && makingBomb == false)
        {
            makingBomb = true;
            StartCoroutine(PlzWait());
        }
    }
    IEnumerator PlzWait()
    {
        yield return new WaitForSeconds(3f);
        thisBomb = Instantiate(bomb, bombRotation.position, bombRotation.rotation);
        makingBomb = false;
    }
}
