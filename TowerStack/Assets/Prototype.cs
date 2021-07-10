using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype : MonoBehaviour
{
    public float timeRespawn;

    private float raX;
    //private float randomY;

    private void Start()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        raX = Random.Range(-3, 3);
        GameObject cubeLife = CrearCubo.Clone(new Vector3(raX, 2, 1));
        

        cubeLife.transform.SetParent(gameObject.transform);

        yield return new WaitForSeconds(timeRespawn);

     
        Destroy(cubeLife, 2);
        StartCoroutine(Respawn());
    }
}
