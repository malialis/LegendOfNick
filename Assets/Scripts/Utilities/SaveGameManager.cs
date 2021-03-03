using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveGameManager : MonoBehaviour
{
    public SaveData activeSave;

    public static SaveGameManager instance;

    public bool hasLoaded;

    private void Awake()
    {
        instance = this;
        Load();
    }

    private void Update()
    {
        
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);

        serializer.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("Saves are Saved");
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);

            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Saves are Loaded");

            hasLoaded = true;
        }

        Debug.Log("Did not load");
        
    }

    public void DeleteSaveDate()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");

            Debug.Log("Saves are Deleted");
        }
    }





[System.Serializable]
public class SaveData
{
        public string saveName;

        public Vector3 respawnPosition;

        public bool doorOpen;
        public bool chestsOpen;

        public int money;
        public int hearts;
        public int magic;



}



    /*

    //[SerializeField]
    //private SaveGameObject saveGame;

    public static SaveGameManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    private void Awake()
    {
        if(gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        LoadScriptables();
        Debug.Log("Saves are Loaded");
    }

    private void OnDisable()
    {
        SaveScriptables();
        Debug.Log("Saves are Saved");
    }

    #region Saving and Loading

    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
            Debug.Log("Saves are Saved");
        }

    }
    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
                Debug.Log("Saves are Loaded");
            }
        }

    }

    #endregion

    public void ResetScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
                Debug.Log("Saves Are Cleared yo");
            }
        }
    }

    */


}
