using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAOpen : MonoBehaviour
{
    bool isOpen = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {

        if ((isOpen == false) && Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<Animator>().SetBool("DoorOpen", true);
            isOpen = true;
            
        }
        else if ((isOpen == true) && Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<Animator>().SetBool("DoorOpen", false);
            isOpen = false;
        }
    }
}