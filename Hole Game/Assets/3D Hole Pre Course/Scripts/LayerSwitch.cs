using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LayerSwitch : MonoBehaviour
{

    [Header(" Settings ")]
    [SerializeField] private string enterLayer;
    [SerializeField] private string exitLayer;


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = LayerMask.NameToLayer(enterLayer);
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = LayerMask.NameToLayer(exitLayer);
    }

    
}
