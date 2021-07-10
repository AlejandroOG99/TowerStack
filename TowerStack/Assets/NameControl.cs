using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameControl : MonoBehaviour
{
    public GameObject panel;

    public string nombre;

    public TMP_InputField infi;

    public float puntos;

    private static NameControl _instance;

    public static NameControl Instance
    {
        get { return _instance; }

    }

    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
           // DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Guardar.Instance.LoadGame();
    }

    public void ok() {

        panel.SetActive(false);
        nombre = infi.text;
        HacerDB.Instance.InsertScore(nombre, puntos);

    
    
    }
}
