using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCorrectPos : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))
        {
            other.transform.parent = transform.GetChild(0);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
