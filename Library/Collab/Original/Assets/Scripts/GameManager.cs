using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private bool progressToStageTwo = false;
    [SerializeField] private GameObject destrutableWall;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Rigidbody playerRigidbody;
    public bool isPaused = false;
    void Start()
    {
        //playerRigidbody.AddForce(Vector3.up * 10000f, ForceMode.Force);
    }
    public void AddPoints()
    {
        points += 500;
        if(points >= 50000)
        {
            progressToStageTwo = true;
            destrutableWall.GetComponent<Rigidbody>().isKinematic = false;
            destrutableWall.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void LoadMainMenu()
    {
        Debug.Log("FUCK");
        SceneManager.LoadScene(0);
    }
}
