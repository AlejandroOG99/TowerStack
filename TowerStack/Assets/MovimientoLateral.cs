using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoLateral : MonoBehaviour
{
	public Transform start, finish;
	public float velocity = 2f;
	Rigidbody rbInstanciador;
	public GameObject instanciador;
	bool direccion;
	GameObject[] pisosColocados = new GameObject[300];
	int lvl = 3;
	public int contador;
	public GameObject[] planta;
	GameObject currentFloor;
	GameObject plantaActual;
	public bool menuPrincipal;
	

	
	bool canThrow = true;
	// Start is called before the first frame update
	void Start()
    {
		
		currentFloor = planta[1];
		rbInstanciador = instanciador.GetComponent<Rigidbody>();
		if (menuPrincipal) return;
		plantaActual = Instantiate(planta[1], instanciador.transform);
		plantaActual.transform.SetParent(instanciador.transform);
		plantaActual.GetComponent<Caida>().amiTheFirst = true;
		plantaActual.transform.localPosition = Vector3.zero;
		pisosColocados[contador] = plantaActual;
		
		
	}

    // Update is called once per frame
    void Update()
    {
		Debug.Log("La variable finpartida es = " + GameManager.finPartida);
		if (direccion)
		{
			rbInstanciador.velocity = new Vector3(-velocity, 0, 0);
		}
		else
		{
			rbInstanciador.velocity = new Vector3(velocity, 0, 0);
		}
		if (instanciador.transform.position.x > finish.position.x)
		{
			direccion = true;
		}
		if (instanciador.transform.position.x < start.position.x)
		{
			direccion = false;
		}
		if (menuPrincipal) return;
		if (velocity < 2) velocity = 2;
		if(GameManager.finPartida == false)
		{
			if (contador >= 3)
			{
				if(contador - lvl >= 0)
				{
					pisosColocados[contador - lvl].GetComponent<Rigidbody>().isKinematic = true;
				}
				
			}

			
			if (!GUIscript.paused && canThrow)
			{
				if (Input.touchCount > 0)
				{
					foreach(Touch touch in Input.touches)
					{
						if(touch.phase == TouchPhase.Began)
						{
							if (Input.GetTouch(0).position.y < 2200)
							{
								contador++;
								plantaActual.GetComponent<Caida>().caer = true;
								StartCoroutine(instanciarNuevoObjeto());
								canThrow = false;
							}
						}
					}
					
					
				}
				if (Input.GetButtonDown("Jump"))
				{
					canThrow = false;
					contador++;
					plantaActual.GetComponent<Caida>().caer = true;
					StartCoroutine(instanciarNuevoObjeto());


				}
			}
			
		}
		
	}
	IEnumerator instanciarNuevoObjeto()
	{
		yield return new WaitForSeconds(1.7f);
		canThrow = true;
		switch (contador)
		{
			case 0:
				currentFloor = planta[1];
				break;
			case 5:
				currentFloor = planta[2];
				lvl = 6;
				velocity = velocity + 0.6f;
				break;
			case 10:
				velocity = velocity + 0.7f;
				lvl = 7;
				currentFloor = planta[3];
				break;
			case 15:
				lvl = 8;
				velocity = velocity + 0.7f;
				currentFloor = planta[4];
				break;
			case 20:
				lvl = 8;
				velocity = velocity + 0.7f;
				currentFloor = planta[4];
				break;
			case 25:
				lvl = 9;
				velocity = velocity + 0.7f;				
				break;
			case 30:
				lvl = 9;
				velocity = velocity + 0.7f;


				break;
			case 35:
				lvl = 10;
				velocity = velocity + 0.7f;


				break;
			case 40:
				lvl = 11;
				velocity = velocity + 0.7f;


				break;
			case 45:
				lvl = 11;
				velocity = velocity + 0.7f;


				break;
			case 50:
				lvl = 12;
				velocity = velocity + 0.7f;


				break;

		}
		plantaActual = Instantiate(currentFloor, instanciador.transform);
		plantaActual.transform.localPosition = Vector3.zero;
		pisosColocados[contador] = plantaActual;
	}
	
}
