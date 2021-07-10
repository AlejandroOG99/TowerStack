using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIscript : MonoBehaviour
{
	public Text puntos;
	public GameObject panelPausa;
	public static bool paused;
	public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
			puntos.text = GameManager.puntos.ToString();
		
		
    }
	public void Pausar()
	{
		paused = true;
		Time.timeScale = 0;
		panelPausa.SetActive(true);
	}
	public void Reanudar()
	{
		panelPausa.SetActive(false);
		paused = false;
		Time.timeScale = 1;
	}
	public void VolverAlMenu()
	{
		Time.timeScale = 1;
		GameManager.finPartida = false;
		paused = false;
		GameManager.puntos = 0;
		SceneManager.LoadScene(0);
	}
	public void NewGame()
	{
		gameOver.SetActive(false);
		Time.timeScale = 1;
		Debug.Log("ME estoy ejecutando");
		GameManager.finPartida = false;
		paused = false;
		GameManager.puntos = 0;
		SceneManager.LoadScene(1);
	}
	public void GameOver()
	{
		gameOver.SetActive(true);
	}
}
