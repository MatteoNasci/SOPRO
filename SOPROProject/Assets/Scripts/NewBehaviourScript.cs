using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOPRO;
public class NewBehaviourScript : MonoBehaviour
{
    public float Value;
    public BaseSOEvFloat Event;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Event.Raise(Value);
    }
}
