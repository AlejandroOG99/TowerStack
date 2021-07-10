using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static int puntos;
	public GameObject cadena;
	Vector3 newPosition;
	bool subiendo;
	public static int contadorPerfect;
	public static bool finPartida;
	// Start is called before the first frame update
	void Start()
    {
		newPosition = cadena.transform.position;
		finPartida = false;
    }

    // Update is called once per frame
    void Update()
	{
		if (!finPartida)
		{
			if (contadorPerfect == 3)
			{
				FindObjectOfType<MovimientoLateral>().velocity -= 0.7f;
				contadorPerfect = 0;
			}
			if (subiendo)
			{
				cadena.transform.position = Vector3.Lerp(cadena.transform.position, newPosition, 0.1f);
				if (cadena.transform.position == newPosition)
				{
					subiendo = false;
				}
			}
		}
		else
		{
			Guardar.Instance.SaveGame(puntos);
			puntos = 0;
			SceneManager.LoadScene("Menu Principal 1");
			
		}
		
		
	}
	public void subir()
	{
		subiendo = true;
		Debug.Log("Sube");
		newPosition = cadena.transform.position + Vector3.up * 1.52f;
		
	}
}
