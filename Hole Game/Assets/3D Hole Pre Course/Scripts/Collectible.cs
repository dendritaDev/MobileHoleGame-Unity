using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float size;

    private void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0; //para que siempre este activo y nunca entre en modo sleep
    }

    public float GetSize()
    {
        return size;
    }
}
