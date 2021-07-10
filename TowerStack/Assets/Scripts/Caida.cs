using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caida : MonoBehaviour
{
	public bool caer;
	Rigidbody rb;
	GameManager gm;
	public bool amiTheFirst;
	public LayerMask queEsSuelo;

	public float inicioRayo;
	public float longitudRayo;
	
	bool cayo;

	bool haSubido;
	bool puntos;
	public GameObject golpe, perfectParticles, perfectImage;

	public float anchuraRayo;
	bool perfect = true;
	public bool rayo1;
	public bool rayo2;
	public Transform particlePosition, particles2Position;
	AudioSource audio;
	public AudioClip golpeAudio;
	// Start is called before the first frame update
	void Start()
    {

		audio = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
		gm = FindObjectOfType<GameManager>();
		rb.useGravity = false;
		rb.isKinematic = true;
		amiTheFirst = false;
		anchuraRayo = 1.09f;
		longitudRayo = 1.5f;
	}

    // Update is called once per frame
    void Update()
    {
		
		if (caer)
		{
			transform.parent = null;
			rb.useGravity = true;
			rb.isKinematic = false;
			
			
			
		}
		else
		{
			
		}
		
		if (cayo && !amiTheFirst)
		{
			//RaycastHit hit;
			//Ray ray1 = new Ray();
			//Ray ray2 = new Ray();
			//ray1.origin = transform.position - transform.right * anchuraRayo;
			//ray1.direction = transform.up * -1;

			//ray2.origin = transform.position + transform.right * anchuraRayo;
			//ray.direction = transform.up * -1;
			Debug.DrawRay(transform.position - transform.right * anchuraRayo, -transform.up * longitudRayo, Color.red);
			//if(Physics.Raycast(transform.position - transform.right* anchuraRayo, Vector3.down * longitudRayo,longitudRayo))
			//{
			//	GameManager.puntos++;
			//}
			//else
			//{
			//	Debug.Log("Has perdido");
			//}
			Debug.DrawRay(transform.position + transform.right * anchuraRayo , -transform.up * longitudRayo, Color.red);
			
			if (Physics.Raycast(transform.position + transform.right * anchuraRayo , -transform.up * longitudRayo, longitudRayo, queEsSuelo))
			{
				rayo1 = true;
			}
			else
			{
				rayo1 = false;
			}
			if (Physics.Raycast(transform.position - transform.right * anchuraRayo, -transform.up * longitudRayo, longitudRayo, queEsSuelo))
			{
				rayo2 = true;
			}
			else
			{
				rayo2 = false;
			}
			if(rayo1 && rayo2)
			{
				if (perfect)
				{
					instanciarParticulasPerfect();
					GameManager.contadorPerfect++;
					perfect = false;
					Debug.Log("Perfect");
				}
				
				
			}
			else
			{
				GameManager.contadorPerfect = 0;
			}
			if (cayo)
			{
				if (!rayo1 && !rayo2)
				{
					GameManager.finPartida = true;
					rb.isKinematic = false;
					FindObjectOfType<GUIscript>().GameOver();
					Debug.Log("PERDISTE");
				}
				//rb.isKinematic = true;
			}
			if(rayo1 || rayo2)
			{
				if (!puntos)
				{
					instanciarParticulas();
					GameManager.puntos += 100;
					puntos = true;
					Debug.Log("PUNTOOOO");
				}
				
			}
		}
		
	}
	void OnCollisionEnter(Collision other)
	{
		//rb.isKinematic = true;
		caer = false;
		if(rb != null)
		{
			rb.velocity = Vector3.zero;
		}
		
		if (!amiTheFirst)
		{
			if (!haSubido)
			{
				if(gm != null)
				{
					gm.subir();
				}
				
				haSubido = true;
			}
			
		}
		
		cayo = true;
	}
	public void instanciarParticulas()
	{
		Instantiate(golpe, particles2Position);
		audio.PlayOneShot(golpeAudio);
	}
	public void instanciarParticulasPerfect()
	{
		Instantiate(perfectParticles, particlePosition);
		GameObject.FindGameObjectWithTag("perfect").GetComponent<Animator>().SetTrigger("perfect");
	}



}
