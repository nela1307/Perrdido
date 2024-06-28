using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{



    private void OnTriggerStay(Collider other)
    {
        GameObject dog= null;

        if (other.gameObject.CompareTag("Dog1"))
        {
            dog = GameObject.FindGameObjectWithTag("Dog1");
        }
        if (other.gameObject.CompareTag("Dog2"))
        {
            dog = GameObject.FindGameObjectWithTag("Dog2");
        }

        if (other.gameObject.CompareTag("Dog3"))
        {
            dog = GameObject.FindGameObjectWithTag("Dog3");
        }

        
        if (Input.GetAxis("PickUp") >0 && dog!= null)
        {

            dog.GetComponent<Vanish>().vanishing();

        }
    }
}
