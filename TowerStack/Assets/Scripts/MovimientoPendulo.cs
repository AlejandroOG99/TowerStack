using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPendulo : MonoBehaviour
{
	HingeJoint2D joint;
	JointAngleLimits2D limites;
	JointMotor2D motor;
	public float motorSpeed;
	public float initialAngle;
	public float variacionDelAngulo;
	public GameObject[] planta;
	GameObject[] pisosColocados = new GameObject[300]; 
	GameObject currentFloor;
	GameObject plantaActual;
	public Transform ultimoEslabon;
	float cooldown;
	bool puedoCambiar;
	int contador = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		joint = GetComponent<HingeJoint2D>();
		motor.motorSpeed = motorSpeed;
		limites.max =  initialAngle / 2;
		limites.min = -initialAngle / 2;
		motor.maxMotorTorque = 1000;
		plantaActual = Instantiate(planta[0], ultimoEslabon);
		plantaActual.transform.SetParent(ultimoEslabon);
		plantaActual.GetComponent<Caida>().amiTheFirst = true;
		plantaActual.transform.localPosition = Vector3.zero;
		plantaActual.transform.localEulerAngles = new Vector3(0, 0, -90);
		pisosColocados[contador] = plantaActual;
		Debug.Log(plantaActual);

	}

    // Update is called once per frame
    void Update()
    {
		pisosColocados[contador - 3].GetComponent<Rigidbody>().isKinematic = true;
		
		Mathf.Clamp(limites.min, -28, 0);
		Mathf.Clamp(limites.max, 0, 28);
		//Debug.Log(joint.jointAngle);
		if (joint.jointAngle <= joint.limits.min  || joint.jointAngle >= joint.limits.max)
		{
			if (puedoCambiar)
			{
				CambioDireccion();
			}
			
		}
		//if (Input.GetButtonDown("Fire1"))
		//{
		//	AumentarAngulo();
		//}
		joint.limits = limites;
		joint.motor = motor;

		if (Input.GetButtonDown("Fire1"))
		{
			plantaActual.GetComponent<Caida>().caer = true;
			StartCoroutine(instanciarNuevoObjeto());
			AumentarAngulo();

		}

		if (puedoCambiar == false)
		{
			contador++;
			if(contador > 10)
			{
				puedoCambiar = true;
				contador = 0;
			}
		}


	}
	void CambioDireccion()
	{
		puedoCambiar = false;
		motor.motorSpeed = -motor.motorSpeed;
	}

    
        void AumentarAngulo()
	{
		limites.min = limites.min - variacionDelAngulo;
        
        limites.max = limites.max + variacionDelAngulo;

        if(motor.motorSpeed < 0)
        {

            motor.motorSpeed -= 2;
        }
        else { motor.motorSpeed += 2;
        }

       

	}
	IEnumerator instanciarNuevoObjeto()
	{
		yield return new WaitForSeconds(1);
		switch (contador)
		{
			case 1:
				currentFloor = planta[1];
				break;
			case 6:
				currentFloor = planta[2];
				break;
			case 11:
				currentFloor = planta[3];
				break;
			case 16:
				currentFloor = planta[4];
				break;
			case 21:
				currentFloor = planta[5];
				break;
			
			
		}
		plantaActual = Instantiate(currentFloor, ultimoEslabon.transform);
		pisosColocados[contador] = plantaActual;
	}
	public void volverAloValoresIniciales()
	{
		motor.motorSpeed = motorSpeed;
		
	}
	
}
