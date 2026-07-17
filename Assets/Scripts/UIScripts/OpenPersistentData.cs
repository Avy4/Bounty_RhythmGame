using System.IO;
using UnityEngine;

public class OpenPersistentData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Open()
    {
        if (Directory.Exists(Application.persistentDataPath))
        {
            Application.OpenURL("file://" + Application.persistentDataPath);
        }
    }
}
