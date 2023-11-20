using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private PlayerSize playerSize;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Collectible collectible))
        {
            playerSize.CollectibleColledcted(collectible.GetSize());
            Destroy(other.gameObject);
        }
    }
}
