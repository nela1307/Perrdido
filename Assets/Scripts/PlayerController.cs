using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private float mvmtSpeed;
    [SerializeField]
    private float mouseSensitivity;

    private Rigidbody rb;
    private float verticalRotation;
    private GameController gameController;
    private void Awake()
    {
        //instanciar el game controller y rigid body
        GameObject goGameController = GameObject.FindGameObjectWithTag("GameController");
        gameController = goGameController.GetComponent<GameController>();

        rb = GetComponent<Rigidbody>();
        //poner el cursor invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();
    }


    void PlayerRotation()
    {
        if (!gameController.youLost())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //eje Y
            transform.Rotate(Vector3.up * mouseX);

            //eje X
            verticalRotation -= mouseY;
            //limita el movimiento para mirar hacia arriba y abajo sin dar una vuelta completa
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            playerCamera.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }

    }

    void MovePlayer() 
    {
        if (rb == null || gameController.youLost()) return;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //vector de movimiento con las entradas del usuario
        Vector3 mvmt = new Vector3 (h,0f, v);
        //enlazar el movimiento a la camara
        mvmt=Camera.main.transform.TransformDirection(mvmt);
        
        //velocidad del rigidbody dependiendo de cuanto se mueva
        rb.velocity = mvmt * mvmtSpeed;

    }

}
