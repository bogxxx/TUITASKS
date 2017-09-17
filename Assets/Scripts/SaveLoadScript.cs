using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class SaveLoadScript : MonoBehaviour
{
    [SerializeField]
    public Sprite[] arraySprites;

    public int getIntOfImage(Sprite sprite1)
    {

        for (int i = 0; i < arraySprites.Length; i++)
        {
            if (arraySprites[i] == sprite1)
            {
                return i;
            }
        }
        return -1;
    }

    string playerDataPath;
    public static string[] Saves = new string[155];

    void Awake()
    {
        playerDataPath = Application.streamingAssetsPath + "/Saves/EditorData.db";
        if (!File.Exists(playerDataPath))
        {
            File.Create(playerDataPath);
            StreamWriter dataWriter = new StreamWriter(playerDataPath);
        }
        else
        {
            for (int  i = 1; i < Saves.Length; i++)
            {
                Saves[i] = File.OpenText(playerDataPath).ReadLine();
            }
        }
    }

 
    void Save()
    {
        StreamWriter dataWriter = new StreamWriter(playerDataPath);
        for (int i = 1; i < Saves.Length; i++)
        {
            dataWriter.WriteLine(Saves[i]);
        }
        dataWriter.Flush();
        dataWriter.Close();
    }

    void Load()
    {
        StreamReader dataReader = new StreamReader(playerDataPath);
        for (int i = 1; i < Saves.Length; i++)
        {
            Saves[i] = dataReader.ReadLine();
        }
        dataReader.Close();
    }

    public  static float[] GetFloat(int i)
    {
    if (Saves[i] != "")
        {
            string[] data = new string[7];
            float[] result = new float[7];
            data = Saves[i].Split(' ');
            for (int a = 0; a < data.Length; a++)
            {
                result[a] = float.Parse(data[a]);
            }
            return result;
        }
        else 
        {
            return null;
        }
    }


    void SetFloat(int i, float v)
    {
        Saves[i] += v.ToString()+ " ";
    }

    void ClearSaveFile()
    {
        for (int i = 1; i < Saves.Length; i++)
        {
            Saves[i] = "";
        }
        Save();
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            int i = 1;
            ClearSaveFile();
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    int spriteInt = -1;
                    RaycastHit2D hit = Physics2D.Raycast(new Vector3(x, y, -10f), Vector2.zero);
                    spriteInt = getIntOfImage(hit.transform.GetComponent<SpriteRenderer>().sprite);
                    Saves[i] = x.ToString() + " " + y.ToString() + " " + spriteInt.ToString() + " " + (hit.transform.rotation.w).ToString() + " " + (hit.transform.rotation.x).ToString() + " " + (hit.transform.rotation.y).ToString() + " " + (hit.transform.rotation.z).ToString();
                    i++;
                }
            }
            Save();
            Debug.Log("saved, блять, а теперь иди нахуй");
        }



        if (Input.GetKeyDown("l"))
        {
            Load();
            float[] data;
            for (int i = 1; i < Saves.Length; i++)
            {
                data = GetFloat(i);
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(data[0], data[1], -10f), Vector2.zero);
                if ((int)data[2] != -1)
                    hit.transform.GetComponent<SpriteRenderer>().sprite = arraySprites[(int)data[2]];
                else
                    hit.transform.GetComponent<SpriteRenderer>().sprite = null;
                hit.transform.rotation = new Quaternion(data[4], data[5], data[6], data[3]);
            }
            Debug.Log("Загрузка произведена, мать твою за ногу");
        }
    }
}