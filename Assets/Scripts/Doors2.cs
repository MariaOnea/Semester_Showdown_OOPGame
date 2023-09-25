using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors2 : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public string curPassword = "3957295";
    public string input;
    public bool doorOpen;
    public bool keypadScreen;
    public Transform doorHinge;

    public AudioSource doorSound;


    public bool inReach;




    void Start()
    {
        inReach = false;
        input = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            keypadScreen = false;
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            input = input + "0";
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            input = input + "1";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            input = input + "2";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            input = input + "3";
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            input = input + "4";
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            input = input + "5";
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            input = input + "6";
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            input = input + "7";
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            input = input + "8";
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            input = input + "9";
        }


        if (!(door.GetBool("Open")) && inReach && Input.GetButtonDown("Interact"))
        {
            if (input == curPassword)
            {
                doorOpen = true;
            }
        }

        if (door.GetBool("Open") && inReach && Input.GetButtonDown("Interact"))
        {
            doorOpen = false;

        }

        if (doorOpen == true)
        {
            DoorOpens();
        }

        else if (doorOpen == false)
        {
            DoorCloses();
        }



    }
    void DoorOpens()
    {
        Debug.Log("It Opens");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSound.Play();

    }

    void DoorCloses()
    {
        Debug.Log("It Closes");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
    }

    void OnGUI()
    {
        if (!doorOpen)
        {
            if (inReach)
            {
                GUI.Box(new Rect(0, 0, 400, 50), "'E' to open keypad");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    keypadScreen = true;
                }
            }

            if (keypadScreen)
            {
                GUI.Box(new Rect(0, 0, 640, 70), "");
                GUI.Box(new Rect(10, 10, 620, 50), input);
                GUI.Label(new Rect(10, 60, 200, 50), "'Esc' to exit");

                if (Input.GetKeyDown(KeyCode.Escape))
                {

                    keypadScreen = false;
                    input = "";
                }


            }
        }
    }

}
