using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    public GameObject TouchControlHolder;
    bool ShowControls;
    public GameObject myPlayer;
    public VariableJoystick variableJoystick;


    bool isForward = false;
    bool isBackward = false;
    bool isLeft = false;
    bool isRight = false;

    void Start()
    {
        //Fin all PhotonViews
        //StartCoroutine(WaitToSetupPlayer());
    }

    //IEnumerator WaitToSetupPlayer()
    //{
    //    //yield on a new YieldInstruction that waits for 1 second.
    //    yield return new WaitForSeconds(2);
    //    SetupName();
    //}

    void SetupName()
    {
        //GameObject[] listPlayers = GameObject.FindGameObjectsWithTag("Player");
        //foreach (GameObject player in listPlayers)
        //{
        //    PhotonView pView = player.GetComponent<PhotonView>();
        //    if (pView != null)
        //    {
        //        if (pView.IsMine)
        //        {
        //            myPlayer = player;
        //        }
        //    }
        //}
    }
    public  Vector2 GenerateInput()
    {
        Vector2 vector = new Vector2
        {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };
        return vector;

    }

    void Update()
    {
        HandleMovement();
    }

    //void HandleMovement()
    //{
    //    if (myPlayer != null && Player2.Instance != null && variableJoystick != null)
    //    {
    //        Player2.Instance.v = variableJoystick.Vertical;
    //        Debug.Log("h :" + Player2.Instance.v);
    //        Player2.Instance.h = variableJoystick.Horizontal;
    //        transform.Translate(new Vector3(Player2.Instance.h, Player2.Instance.v, 0) * Player2.Instance.speed * Time.deltaTime);
    //    }

    //}
    void HandleMovement()
    {
        if (myPlayer != null && myPlayer.GetComponent<TouchInput>() != null && variableJoystick != null)
        {
            myPlayer.GetComponent<TouchInput>().valY = variableJoystick.Vertical;
            myPlayer.GetComponent<TouchInput>().valX = variableJoystick.Horizontal;
            transform.Translate(new Vector3(myPlayer.GetComponent<Player2>().h, myPlayer.GetComponent<Player2>().v, 0) * myPlayer.GetComponent<Player2>().speed * Time.deltaTime);
        }

    }


    public void MoveForward()
    {
        isForward = true;
    }
    public void MoveBackward()
    {
        isBackward = true;
    }
    public void MoveLeft()
    {
        isLeft = true;
    }
    public void MoveRight()
    {
        isRight = true;
    }

    public void ResetData()
    {
        isForward = false;
        isBackward = false;
        isLeft = false;
        isRight = false;
    }

    public void ShowTouchControls()
    {
        if (!ShowControls)
        {
            TouchControlHolder.SetActive(true);
            ShowControls = true;
        }
        else
        {
            TouchControlHolder.SetActive(false);
            ShowControls = false;
        }
    }
}
