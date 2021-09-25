using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private bool progressToStageTwo = false;
    [SerializeField] private GameObject destrutableWall;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Rigidbody playerRigidbody;
    public Slider pointsSlider;
    public Text pointsTxt;
    public bool isPaused = false;
    void Start()
    {
        //playerRigidbody.AddForce(Vector3.up * 10000f, ForceMode.Force);
    }
    public void AddPoints()
    {
        points += 100;
        if(points >= 50000)
        {
            progressToStageTwo = true;
            destrutableWall.GetComponent<Rigidbody>().isKinematic = false;
            destrutableWall.GetComponent<BoxCollider>().isTrigger = true;
            pointsTxt.enabled = false;
            pointsSlider.gameObject.SetActive(false);
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
        pointsTxt.text = ("" + points + " out of 50000");
        pointsSlider.value = points;
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
        Debug.Log("REEEEEE");
        SceneManager.LoadScene("Main Menu");
    }
}
