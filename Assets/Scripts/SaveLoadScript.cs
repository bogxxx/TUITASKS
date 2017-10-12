using UnityEngine;
using System;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class SaveLoadScript : MonoBehaviour
{
    double[,] a_matrix = new double[100,100];
    double[] b_vector = new double[100];
    int[] colorArray = new int[100];
    public static int countColor = 0;
    double[] result = new double[100];



    public struct ResistorInOut
    {        
        public int firstUzel;
        public int secondUzel;
        public float resistorNum;
    };
    public ResistorInOut[,] resistorsMatrixArr = new ResistorInOut[100,100]; 
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

    string playerDataPath = "", path = "";
    public static float[,] Saves = new float[155, 9];
    public static float[,,] matrix = new float[100, 100, 9];

    void dfsWire(int i, int j, int color)
    {
        //Debug.Log(matrix[i, j, 2]);
        if (matrix[i, j, 2] != -1)
            boardColor[i, j] = color;
        if (matrix[i, j, 2] == 8)
        {
            if ((matrix[i, j, 3] == 1 && matrix[i, j, 6] == 0) || (matrix[i, j, 3] == 0 && matrix[i, j, 6] == 1))
            {
                //Debug.Log(i + " " + j + " " + 1);
                if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
                {
                    dfsWire(i, j - 1, color);
                }
                if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
                {
                    dfsWire(i, j + 1, color);
                }
            }
            else
            if ((matrix[i, j, 3] == 0.7071068f && matrix[i, j, 6] == 0.7071068f) || (matrix[i, j, 3] == -0.7071068f && matrix[i, j, 6] == 0.7071068f))
            {
                //Debug.Log(i + " " + j + " " + 2);
                if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
                {
                    dfsWire(i - 1, j, color);
                }
                if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
                {
                    dfsWire(i + 1, j, color);
                }
            }
        }
        else
        if (matrix[i, j, 2] == 0)
        {
            if (matrix[i, j, 3] == 1f && matrix[i, j, 6] == 0f)
            {
                //Debug.Log(i + " " + j + " " + 1);
                if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
                {
                    dfsWire(i, j - 1, color);
                }
                if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
                {
                    dfsWire(i + 1, j, color);
                }
            }
            else
            if (matrix[i, j, 3] == -0.7071068f && matrix[i, j, 6] == 0.7071068f)
            {
                //Debug.Log(i + " " + j + " " + 2);
                if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
                {
                    dfsWire(i - 1, j, color);
                }
                //if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
                {
                    dfsWire(i, j - 1, color);
                }
            }
            else
            if (matrix[i, j, 3] == 0f && matrix[i, j, 6] == 1f)
            {
                //Debug.Log(i + " " + j + " " + 3);
                if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
                {
                    dfsWire(i, j + 1, color);
                }
                if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
                {
                    dfsWire(i - 1, j, color);
                }
            }
            else
            if (matrix[i, j, 3] == 0.7071068f && matrix[i, j, 6] == 0.7071068f)
            {
                //Debug.Log(i + " " + j + " " + 4);
                if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
                {
                    dfsWire(i + 1, j, color);
                }
                if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
                {
                    dfsWire(i, j + 1, color);
                }
            }
        }
        else
        if (matrix[i,j,2] == 10)
        {
            if (matrix[i, j, 3] == 1f && matrix[i, j, 6] == 0f)
            {
                //Debug.Log(i + " " + j + " " + 1);
                if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
                {
                    dfsWire(i, j - 1, color);
                }
                if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
                {
                    dfsWire(i, j + 1, color);
                }
                if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
                {
                    dfsWire(i + 1, j, color);
                }
            }
            else
            if (matrix[i, j, 3] == -0.7071068f && matrix[i, j, 6] == 0.7071068f)
            {
                //Debug.Log(i + " " + j + " " + 2);
                if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
                {
                    dfsWire(i - 1, j, color);
                }
                if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
                {
                    dfsWire(i + 1, j, color);
                }
                if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
                {
                    dfsWire(i, j - 1, color);
                }
            }
            else
            if (matrix[i, j, 3] == 0f && matrix[i, j, 6] == 1f)
            {
                //Debug.Log(i + " " + j + " " + 3);
                if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
                {
                    dfsWire(i, j + 1, color);
                }
                if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
                {
                    dfsWire(i, j - 1, color);
                }
                if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
                {
                    dfsWire(i - 1, j, color);
                }
            }
            else
            if (matrix[i, j, 3] == 0.7071068f && matrix[i, j, 6] == 0.7071068f)
            {
                //Debug.Log(i + " " + j + " " + 4);
                if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
                {
                    dfsWire(i + 1, j, color);
                }
                if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
                {
                    dfsWire(i - 1, j, color);
                }
                if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
                {
                    dfsWire(i, j + 1, color);
                }
            }
        }
        else
        if (matrix[i, j, 2] == 9)
        {
            if (boardColor[i + 1, j] != color && boardColor[i + 1, j] > -1)
            {
                dfsWire(i + 1, j, color);
            }
            if (boardColor[i - 1, j] != color && boardColor[i - 1, j] > -1)
            {
                dfsWire(i - 1, j, color);
            }
            if (boardColor[i, j + 1] != color && boardColor[i, j + 1] > -1)
            {
                dfsWire(i, j + 1, color);
            }
            if (boardColor[i, j - 1] != color && boardColor[i, j - 1] > -1)
            {
                dfsWire(i, j - 1, color);
            }
        }
        else if (matrix[i, j, 2] == 3 || matrix[i, j, 2] == 6 || matrix[i, j, 2] == 4) dfs(i, j);
    }
    void dfs(int i, int j)
    {
        if (matrix[i,j,2] == 3)
        {
            boardColor[i, j] = -3;
            if (matrix[i,j,3] == 1f && matrix[i, j, 6] == 0f)
            {
                boundColor++;
                dfs(i + 1, j);
            }
            else
            if (matrix[i, j, 3] == -0.7071068f && matrix[i, j, 6] == 0.7071068f)
            {
                boundColor++;
                dfs(i, j - 1);
            }
            else
            if (matrix[i, j, 3] == 0f && matrix[i, j, 6] == 1f)
            {
                boundColor++;
                dfs(i - 1, j);
            }
            else
            if (matrix[i, j, 3] == 0.7071068f && matrix[i, j, 6] == 0.7071068f)
            {
                boundColor++;
                dfs(i, j + 1);
            }
        }
        else
        if (matrix[i, j, 2] == 6 || matrix[i, j, 2] == 4)
        {
            if (matrix[i, j, 2] == 6) boardColor[i, j] = -6;
            if (matrix[i, j, 2] == 4) boardColor[i, j] = -4;
            boundColor++;
            if ((matrix[i, j, 3] == 1 && matrix[i, j, 6] == 0) || (matrix[i, j, 3] == 0 && matrix[i, j, 6] == 1))
            {
                if (boardColor[i - 1, j] == 0)
                {
                    dfs(i - 1, j);
                }
                else
                if (boardColor[i + 1, j] == 0)
                {
                    dfs(i + 1, j);
                }
            }
            else
            if ((matrix[i, j, 3] == 0.7071068f && matrix[i, j, 6] == 0.7071068f) || (matrix[i, j, 3] == -0.7071068f && matrix[i, j, 6] == 0.7071068f))
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
        }
        else
        if (matrix[i, j, 2] != -1)
        {
            dfsWire(i, j, boundColor);
        }

    }
 
    void Save()
    {
        StreamWriter dataWriter = new StreamWriter(playerDataPath);
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                for (int j2 = 0; j2 < 9; j2++)
                    dataWriter.Write(matrix[i, j, j2] + " ");
                dataWriter.WriteLine();
            }
        }
        /*for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                dataWriter.Write(boardColor[i, j] + " ");
            }
            dataWriter.WriteLine();
        }*/
        double allResist;
        int xPosEds = 6, yPosEds = 6;

        for (int currentcolor = 0; currentcolor < countColor; currentcolor++)
        {
            allResist = 0;
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (boardColor[i, j] < 0 && matrix[i,j,2] != 3)
                    {
                        if (resistorsMatrixArr[i, j].firstUzel == colorArray[currentcolor])
                        {
                            a_matrix[currentcolor, ColorInArray(resistorsMatrixArr[i, j].secondUzel)] += 1 / resistorsMatrixArr[i, j].resistorNum;
                            allResist+= 1 / resistorsMatrixArr[i, j].resistorNum;
                        }
                        if (resistorsMatrixArr[i, j].secondUzel == colorArray[currentcolor])
                        {
                            a_matrix[currentcolor, ColorInArray(resistorsMatrixArr[i, j].firstUzel)] += 1 / resistorsMatrixArr[i, j].resistorNum;
                            allResist += 1 / resistorsMatrixArr[i, j].resistorNum;
                        }
                    } else
                    if (matrix[i, j, 2] == 3)
                    {
                        xPosEds = i;
                        xPosEds = j;
                    }
                }
            }
            a_matrix[currentcolor, currentcolor] = -allResist;
        }
        for (int i = 0; i < countColor; i++)
        {
            a_matrix[ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].firstUzel), i] = 0;
            a_matrix[ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].secondUzel), i] = 0;
        }
        a_matrix[ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].firstUzel), ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].firstUzel)] = 1;
        a_matrix[ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].secondUzel), ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].secondUzel)] = 1;
        b_vector[ColorInArray(resistorsMatrixArr[xPosEds, yPosEds].secondUzel)] = resistorsMatrixArr[xPosEds, yPosEds].resistorNum;

        Gauss.LinearSystem U = new Gauss.LinearSystem(a_matrix, b_vector, 1);

        result = U.XVector;
        for (int i = 0; i < countColor; i++)
        {
            dataWriter.Write(result[i] + " ");
        }

        /*for (int i = 0; i < countColor; i++)
        {
            for (int j = 0; j < countColor; j++)
            {
                dataWriter.Write(a_matrix[i,j]+ " ");
            }
            dataWriter.WriteLine(b_vector[i]);
        }

                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        if (boardColor[i,j] < 0)
                        {
                            dataWriter.WriteLine(resistorsMatrixArr[i,j].firstUzel + " " + resistorsMatrixArr[i, j].secondUzel + " " + i + " " + j + " " + resistorsMatrixArr[i,j].resistorNum);
                        }
                    }            
                }*/

                dataWriter.Flush();
        dataWriter.Close();

    }

    int ColorInArray(int color)
    {
        for (int i = 0; i < countColor; i++)
        {
            if (colorArray[i] == color) return i;
        }
        return 50;
    }

    void Load()
    {
        StreamReader dataReader = new StreamReader(playerDataPath);
        string[] stringdata = new string[9];
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                stringdata = dataReader.ReadLine().Split(' ');
                Debug.Log(stringdata.Length);
                for (int a = 0; a < 9; a++)
                {
                    Debug.Log(stringdata[a]);
                    matrix[i,j,a] = float.Parse(stringdata[a]);
                }
               // Debug.Log(matrix[i, j, 0] + " " + matrix[i, j, 1] + " " + matrix[i, j, 2]);
            }
        }
       dataReader.Close();
    }


    public static bool doSaveWindow, doLoadWindow;
    private Rect windowRect0 = new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 3);

    void OnGUI()
    {
        if (doSaveWindow)
        {
            GUI.color = Color.cyan;
            windowRect0 = GUI.Window(1, windowRect0, DoMyWindow, "Введите имя файла для сохранения:");
        }

        if (doLoadWindow)
        {
            GUI.color = Color.cyan;
            windowRect0 = GUI.Window(0, windowRect0, DoMyWindow, "Введите имя файла для загрузки:");
        }
    }

    void DoMyWindow(int windowID)
    {
        playerDataPath = "";
        GUI.color = Color.cyan;
        path = GUI.TextField(new Rect(10, Screen.height / 10, (Screen.width / 3 - 20), Screen.height / 30), path);
        if (GUI.Button(new Rect(Screen.width / 9, Screen.height / 4 + 30, Screen.width / 9, Screen.width / 50), "Подтвердить"))
        {
            playerDataPath = "C:/" + path + ".db";


            GUI.color = Color.cyan;
            if (windowID == 0)
            {
                Load();
                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
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
            doLoadWindow = false;
            

            if (windowID == 1)
            {
                int i = 1;
                int xcounter = 0, ycounter = 0;
                for (int x = 0; x < 14; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        int spriteInt = -1;
                        RaycastHit2D hit = Physics2D.Raycast(new Vector3(x, y, -10f), Vector2.zero);
                        spriteInt = getIntOfImage(hit.transform.GetComponent<SpriteRenderer>().sprite);
                        Saves[i, 0] = x;
                        Saves[i, 1] = y;
                        Saves[i, 2] = spriteInt;
                        Saves[i, 3] = hit.transform.rotation.w;
                        Saves[i, 4] = hit.transform.rotation.x;
                        Saves[i, 5] = hit.transform.rotation.y;
                        Saves[i, 6] = hit.transform.rotation.z;
                        Saves[i, 7] = hit.collider.gameObject.GetComponent<BlockScript>().resist;
                        Saves[i, 8] = hit.collider.gameObject.GetComponent<BlockScript>().eds;
                        if (ycounter > 10)
                        {
                            ycounter = 0;
                            xcounter++;
                        }
                        for (int j = 0; j < 9; j++)
                            matrix[xcounter, ycounter, j] = Saves[i, j];
                        i++;
                        ycounter++;
                    }

                }
                for (int x = 0; x < 14; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        boardColor[x, y] = 0;
                    }
                }
                boundColor = 0;
                dfs(6, 6);
                
                for (int x = 0; x < 14; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        if (matrix[x,y,2] == 6 || matrix[x,y,2] == 3)
                        {
                            resistorsMatrixArr[x, y].resistorNum = matrix[x, y, 7];
                            if ((matrix[x, y, 3] == 1 && matrix[x, y, 6] == 0) || (matrix[x, y, 3] == 0 && matrix[x, y, 6] == 1))
                            {
                                if (boardColor[x - 1, y] > 0)
                                    if (resistorsMatrixArr[x, y].firstUzel == 0)
                                        resistorsMatrixArr[x, y].firstUzel = boardColor[x - 1, y];
                                    else
                                        if (resistorsMatrixArr[x, y].secondUzel == 0)
                                        resistorsMatrixArr[x, y].secondUzel = boardColor[x - 1, y];

                                if (boardColor[x + 1, y] > 0)
                                    if (resistorsMatrixArr[x, y].firstUzel == 0)
                                        resistorsMatrixArr[x, y].firstUzel = boardColor[x + 1, y];
                                    else
                                    if (resistorsMatrixArr[x, y].secondUzel == 0)
                                            resistorsMatrixArr[x, y].secondUzel = boardColor[x + 1, y];
                            }
                            else
                            {
                                if ((matrix[x, y, 3] == 1 && matrix[x, y, 6] == 0) || (matrix[x, y, 3] == 0 && matrix[x, y, 6] == 1))
                                {
                                    if (boardColor[x, y - 1] > 0)
                                        if (resistorsMatrixArr[x, y].firstUzel == 0)
                                            resistorsMatrixArr[x, y].firstUzel = boardColor[x, y - 1];
                                        else
                                        if (resistorsMatrixArr[x, y].secondUzel == 0)
                                            resistorsMatrixArr[x, y].secondUzel = boardColor[x, y - 1];

                                        if (boardColor[x, y + 1] > 0)
                                        if (resistorsMatrixArr[x, y].firstUzel == 0)
                                            resistorsMatrixArr[x, y].firstUzel = boardColor[x, y + 1];
                                        else
                                            if (resistorsMatrixArr[x, y].secondUzel == 0)
                                                resistorsMatrixArr[x, y].secondUzel = boardColor[x, y + 1];
                                }
                            }
                        }
                        if (matrix[x, y, 2] == 3)
                        {
                            resistorsMatrixArr[x, y].resistorNum = matrix[x, y, 8];
                        }                        
                    }
                }

                bool f;
                int length = 0;

                for (int x = 0; x < 14; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        f = true;
                        for (int k = 0; k < length; k++)
                        {
                            if (colorArray[k] == boardColor[x, y])
                            {
                                f = false;
                                break;
                            }
                        }
                        if (f && boardColor[x, y] != 0 && boardColor[x, y] != -6 && boardColor[x, y] != -3)
                        {
                            colorArray[length] = boardColor[x, y];
                            length++;
                            countColor++;
                        }
                    }
                }
                Save();
                Debug.Log(countColor);
                Debug.Log("saved, блять, а теперь иди нахуй");
                playerDataPath = "";
                countColor = 0;
                doSaveWindow = false;
            }
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            doSaveWindow = true;
        }

        if (Input.GetKeyDown("l"))
        {
            doLoadWindow = true;
        }
    }
}