using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Time.timeScale = 1;
		GameManager.finPartida = false;
		GUIscript.paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void IniciarPartida()
	{
		SceneManager.LoadScene(1);
	}
	public void IrMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void CerrarJuego()
	{
		Application.Quit();
	}
}
