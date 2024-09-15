using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 0.1f);
    }
}
