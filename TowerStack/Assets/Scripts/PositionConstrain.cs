using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionConstrain : MonoBehaviour
{
	public HingeJoint2D huesoHijo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		huesoHijo.anchor = transform.position;
    }
}
