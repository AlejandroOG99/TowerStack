using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCubo : Object
{
    public static GameObject Clone(Vector3 pos)
    {

        GameObject cubeClone = GameObject.CreatePrimitive(PrimitiveType.Cube);

        MeshRenderer rend = cubeClone.GetComponent<MeshRenderer>();
        rend.material.color = new Color(255, 0, 0);
        cubeClone.AddComponent<Rigidbody>();
        cubeClone.GetComponent<Rigidbody>().isKinematic = true;
        cubeClone.AddComponent<BoxCollider>();
        cubeClone.name = "CuboClonado";
        cubeClone.gameObject.SetActive(true);
        cubeClone.transform.position = pos;
        cubeClone.transform.localScale = new Vector3(1,1, 1);

        return cubeClone;
    }
}
