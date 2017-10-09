using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class SaveLoadScript : MonoBehaviour
{
    private int boundColor = 0;
    private int[,] boardColor = new int[100, 100]; 
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
    public static float[,] Saves = new float[155, 9];
    public static float[,,] matrix = new float[100, 100, 9];

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
            for (int  i = 1; i < 155; i++)
            {
                for (int j = 0; j < 9; j++)
                    Saves[i, j] = File.OpenText(playerDataPath).Read();
            }
        }
    }

    void dfsWire(int i, int j, int color)
    {
        Debug.Log(new Vector2(i, j));
        boardColor[i, j] = color;
        if (matrix[i, j, 2] == 8)
        {
            if ((matrix[i, j, 3] == 1 && matrix[i, j, 6] == 0) || (matrix[i, j, 3] == 0 && matrix[i, j, 6] == 1))
            {
                if (boardColor[i, j - 1] != color && (matrix[i, j - 1, 2] >= 8))
                {
                    dfsWire(i, j - 1, color);
                }
                if (boardColor[i, j + 1] != color && (matrix[i, j + 1, 2] >= 8))
                {
                    dfsWire(i, j + 1, color);
                }
            }
            else
            if ((matrix[i, j, 3] == 0.7071068 && matrix[i, j, 6] == 0.7071068) || (matrix[i, j, 3] == -0.7071068 && matrix[i, j, 6] == 0.7071068))
            {
                if (boardColor[i - 1, j] != color && (matrix[i - 1, j, 2] >= 8))
                {
                    dfsWire(i - 1, j, color);
                }
                if (boardColor[i + 1, j] != color && (matrix[i + 1, j, 2] >= 8))
                {
                    dfsWire(i + 1, j, color);
                }
            }
        }
    }
    void dfs(int i, int j)
    {
        Debug.Log(matrix[i,j,2]);
        boardColor[i, j] = boundColor;
        if (matrix[i,j,2] == 3)
        {
            if (matrix[i,j,3] == 1 && matrix[i, j, 6] == 0)
            {
                boundColor++;
                dfs(i, j + 1);
            }
            else
            if (matrix[i, j, 3] == -0.7071068 && matrix[i, j, 6] == 0.7071068)
            {
                boundColor++;
                dfs(i - 1, j);
            }
            else
            if (matrix[i, j, 3] == 0 && matrix[i, j, 6] == 1)
            {
                boundColor++;
                dfs(i, j - 1);
            }
            else
            if (matrix[i, j, 3] == 0.7071068 && matrix[i, j, 6] == 0.7071068)
            {
                boundColor++;
                dfs(i + 1, j);
            }
        }
        else
        if (matrix[i, j, 2] == 6 || matrix[i, j, 2] == 4)
        {
            boundColor++;
            if ((matrix[i, j, 3] == 1 && matrix[i, j, 6] == 0) || (matrix[i, j, 3] == 0 && matrix[i, j, 6] == 1))
            {
                if (boardColor[i, j - 1] == 0)
                {
                    dfs(i, j - 1);
                }
                else
                if (boardColor[i, j + 1] == 0)
                {
                    dfs(i, j + 1);
                }
            }
            else
            if ((matrix[i, j, 3] == 0.7071068 && matrix[i, j, 6] == 0.7071068) || (matrix[i, j, 3] == -0.7071068 && matrix[i, j, 6] == 0.7071068))
            {
                if (boardColor[i -1, j] == 0)
                {
                    dfs(i - 1, j);
                }
                else
                if (boardColor[i + 1, j] == 0)
                {
                    dfs(i + 1, j);
                }
            }
        }
        else
        if (matrix[i, j, 2] == 8)
        {
            dfsWire(i, j, boundColor);
        }

    }
 
    void Save()
    {
        StreamWriter dataWriter1 = new StreamWriter(playerDataPath);
        /*for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                //for (int j2 = 0; j2 < 9; j2++)
                    dataWriter.Write(matrix[i,j, 2] + " ");
                //dataWriter.Write("   ");
            }
            dataWriter.WriteLine();
        }*/
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 11; j++)
            { 
                dataWriter1.Write(boardColor[i,j] + " ");
            }
            dataWriter1.WriteLine();
        }

        dataWriter1.Flush();
        dataWriter1.Close();
        
    }

    void Load()
    {
        StreamReader dataReader = new StreamReader(playerDataPath);
        for (int i = 0; i < 155; i++)
        {
            for (int j = 0; j < 9; j++)
                Saves[i,j] = dataReader.Read();
        }
        dataReader.Close();
    }

    void ClearSaveFile()
    {
        for (int i = 1; i < 155; i++)
        {
            for (int j = 0; j < 9; j++)
                Saves[i, j] = 0;
        }
        Save();
    }

    void Update()
    {
        StreamWriter dataReader = new StreamWriter(playerDataPath);
        if (Input.GetKeyDown("s"))
        {
            int i = 1;
            ClearSaveFile();
            int xcounter = 0, ycounter = 0;
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    int spriteInt = -1;                   
                    RaycastHit2D hit = Physics2D.Raycast(new Vector3(x, y, -10f), Vector2.zero);
                    spriteInt = getIntOfImage(hit.transform.GetComponent<SpriteRenderer>().sprite);
                    Saves[i,0] = x;
                    Saves[i,1] = y;
                    Saves[i, 2] = spriteInt;
                    Saves[i, 3] = hit.transform.rotation.w;
                    Saves[i, 4] = hit.transform.rotation.x;
                    Saves[i, 5] = hit.transform.rotation.y;
                    Saves[i, 6] = hit.transform.rotation.z;
                    Saves[i, 7] = hit.collider.gameObject.GetComponent<BlockScript>().resist;
                    Saves[i,8] = hit.collider.gameObject.GetComponent<BlockScript>().eds;
                    if (ycounter > 10)
                    {
                        ycounter = 0;
                        xcounter++;
                    }
                    for (int j = 0; j < 9; j++)
                        matrix[xcounter, ycounter, j] = Saves[i, j];
                    dataReader.Write(matrix[xcounter, ycounter, 2] + " ");                                    
                    i++;
                    ycounter++;
                }
                dataReader.WriteLine();
            }
            dfs(6, 6);
            Save();
            Debug.Log("saved, блять, а теперь иди нахуй");
            dataReader.Flush();
            dataReader.Close();
            
        }



        if (Input.GetKeyDown("l"))
        {
            Load();
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    //data = GetFloat(i);

                    RaycastHit2D hit = Physics2D.Raycast(new Vector3(matrix[i, j, 0], matrix[i, j, 1], -10f), Vector2.zero);
                    if ((int)matrix[i, j, 2] != -1)
                    {
                        hit.transform.GetComponent<SpriteRenderer>().sprite = arraySprites[(int)matrix[i, j, 2]];
                    }
                    else
                        hit.transform.GetComponent<SpriteRenderer>().sprite = null;
                    hit.transform.rotation = new Quaternion(matrix[i, j, 4], matrix[i, j, 5], matrix[i, j, 6], matrix[i, j, 3]);
                    hit.collider.gameObject.GetComponent<BlockScript>().resist = (int)matrix[i, j, 7];
                    hit.collider.gameObject.GetComponent<BlockScript>().eds = (int)matrix[i, j, 8];

                }
            }
            Debug.Log("Загрузка произведена, мать твою за ногу");
        }
    }
}