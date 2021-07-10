using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodestroy : MonoBehaviour
{
	public float time;
    // Start is called before the first frame update
    void Start()
    {
		Invoke("destruir", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void destruir()
	{
		Destroy(this);
	}
}
