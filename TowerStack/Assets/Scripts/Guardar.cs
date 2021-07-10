using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

public class Guardar : MonoBehaviour
{

    private static Guardar _instance;

    public static Guardar Instance
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

    // Start is called before the first frame update
    void Start()
    {
       
    }



    public class Guarda
    {
        public float score;
       

    }

    public Guarda gg;

    public void LoadGame()
    {
        string saveFIlePath = Application.persistentDataPath + "/Guardado.sav";

        byte[] Message; 

        Message = File.ReadAllBytes(saveFIlePath);

        string decryptedMessage = Decrypt(Message);
        gg = JsonConvert.DeserializeObject<Guarda>(decryptedMessage);

        NameControl.Instance.puntos = gg.score;
        Debug.Log("Cargado en " + saveFIlePath);

    }
    public void SaveGame(int puntos)
    {
        string saveFIlePath = Application.persistentDataPath + "/Guardado.sav";
        Guarda ggDa = new Guarda();

        ggDa.score = puntos;
  
        byte[] encryptedMessage = Encrypt(JsonConvert.SerializeObject(ggDa));
        File.WriteAllBytes(saveFIlePath, encryptedMessage);
        
        Debug.Log("Guardado en " + saveFIlePath);


    }

   



    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    byte[] _inicializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    byte[] Encrypt(string message)
    {
        AesManaged aes = new AesManaged();
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _inicializationVector);

        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        streamWriter.WriteLine(message);

        streamWriter.Close();
        cryptoStream.Close();
        memoryStream.Close();

        return memoryStream.ToArray();
    }

    string Decrypt(byte[] message)
    {
        AesManaged aes = new AesManaged();
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _inicializationVector);

        MemoryStream memoryStream = new MemoryStream(message);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        StreamReader streamReader = new StreamReader(cryptoStream);

        string decryptedMessage = streamReader.ReadToEnd();

        memoryStream.Close();
        cryptoStream.Close();
        streamReader.Close();

        return decryptedMessage;
    }



}

