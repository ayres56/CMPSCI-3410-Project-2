//Author: Josiah Ayres
//Date: 10/6/2024
//Player Movement / Camera Controller Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public Text winText;
    public Text countDownText;
    public Button restartButton;


    private Rigidbody rb;
    private int count;
    public float timer;

    void Start()
    {
        // Limit framerate to 60fps.
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();
        restartButton.gameObject.SetActive(false);

        //Reset text
        timer = 60f;
        winText.text = "";
        countDownText.text = "";
    }

    private void FixedUpdate()
    {
        //Calculate Movement
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal != 0f)
        {
                transform.Rotate(0, moveHorizontal * rotSpeed, 0);
        }
        transform.position += transform.forward * moveVertical * moveSpeed;

        //Win condition
        if (count >= 12)
        {
            winCond();
        }
        else if (timer <= 0.0f)
        {
            failCond();
        }
        else
        {
            //Increment Timer
            timer -= Time.deltaTime;
            int seconds = (int)timer % 60;
            countDownText.text = "Timer: " + seconds.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("MiniGame");
    }

    private void winCond()
    {
        winText.text = "You Win!";
        restartButton.gameObject.SetActive(true);
    }

    private void failCond()
    {
        winText.text = "Game Over!";
        restartButton.gameObject.SetActive(true);
    }

}
