using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] gameObjects;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine());

    }


    IEnumerator Coroutine()
    {
        while (true) { 

        if (index >= gameObjects.Length) {
            index = 0;
        }

        gameObjects[index].SetActive(true);
        gameObjects[index].transform.position = new Vector3(Random.Range(-3, 3), 12, 1);
        gameObjects[index].GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(2);

            StartCoroutine(SetActiveFalse(gameObjects[index]));

            index++;

        }
    }

    IEnumerator SetActiveFalse(GameObject go)
    {

        yield return new WaitForSeconds(20);
        go.SetActive(false);

    }

}
