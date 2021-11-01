using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
     //  other.transform.parent.gameObject.GetComponent<Tile>().;

    }
}
