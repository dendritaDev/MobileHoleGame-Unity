using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Settings ")]
    [SerializeField] private float screenPositionFollowThreshold;
    [SerializeField] private float moveSpeed;
    private Vector3 clickedScreenPosition;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTimer.onTimerOver += DisableMovement;

        GameManager.onStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        PlayerTimer.onTimerOver -= DisableMovement;

        GameManager.onStateChanged -= GameStateChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
            ManageControl();
    }

    private void ManageControl()
    {
        //Joysticklike controller
        if(Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            Vector3 difference = Input.mousePosition - clickedScreenPosition;

            Vector3 direction = difference.normalized;

            float maxScreenDistance = screenPositionFollowThreshold * Screen.height;

            if(difference.magnitude > maxScreenDistance )
            {
                clickedScreenPosition = Input.mousePosition - direction * maxScreenDistance;
                difference = Input.mousePosition - clickedScreenPosition;
            }

            difference /= Screen.width;

            //para que se mueva en el plano x z, no en y.
            difference.z = difference.y;
            difference.y = 0;

            Vector3 targetPosition = transform.position + difference * moveSpeed * Time.deltaTime;
            transform.position = targetPosition;


            //multiplicamos por la altura de la pantalla y por la anchura para estandarizar la cantidad del movimiento
            //independientemente de la resolución de la pantalla donde se juege
        }
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                break;
            case GameState.GAME:
                EnableMovement();
                break;
            case GameState.LEVELCOMPLETE:
                break;
            case GameState.GAMEOVER:
                break;
        }
    }

    private void DisableMovement()
    {
        canMove= false;
    }
}
