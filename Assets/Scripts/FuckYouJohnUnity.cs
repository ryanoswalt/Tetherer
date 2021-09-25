using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class FuckYouJohnUnity : MonoBehaviour
{
    #region "Variables"
    public Rigidbody Rigid;
    public float MouseSensitivity = 2;
    public float MoveSpeed = .3f;
    public float JumpForce = 500;
    public Transform player;
    public Transform camera;
    bool isGrounded = true;
    [SerializeField] GameManager gameManager;
    #endregion
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (!gameManager.isPaused)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MoveSpeed = .5f;
            }
            player.Rotate(Vector3.up * Input.GetAxis("Mouse X") * MouseSensitivity);
            camera.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * -MouseSensitivity);
            //Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));
            Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
            if (Input.GetKeyDown("space") && isGrounded)
            {
                Rigid.AddForce(Vector3.up * JumpForce);
                isGrounded = false;
            }
            if (!isGrounded)
            {
                Rigid.AddForce(Vector3.up * -JumpForce / 100f);
            }
        }
        if(gameManager.isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Plane" || col.gameObject.name == "Plane (1)")
        {
            isGrounded = true;
        }
    }
}
