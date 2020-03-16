using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeight : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 oldUp;
    public float heightFactor = 0.001f;
    public Transform grandPiano;
    public bool flipper;
    void Start()
    {
        oldUp = transform.up;
        flipper = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deltaRotation = Quaternion.FromToRotation(oldUp, transform.up);
        Debug.Log(deltaRotation);
        if (deltaRotation.x > 0)
        {
            grandPiano.position += new Vector3(0, heightFactor, 0);
        }
        else if (deltaRotation.x < 0)
        {
            grandPiano.position -= new Vector3(0, heightFactor, 0);
        }
        oldUp = transform.up;


    }
}
