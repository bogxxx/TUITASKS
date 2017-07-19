using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public class ElShema //класс, в котором хранится массив, содержащий в себе электрическую схему
    {
        public int[,] Array = new int[50, 30]; //вот он)
    }



    public class Shema : ElShema //в этом классе я планиркю хранить связи между элементами, он наследует элсхему
    {
        int KolStolb = 50, KolStroch = 30;
        public int[,,] ShemaBool = null; //создаем второй массив(трехмерный) в котором будут хранится связи. Третья переменная означает направление: Up = 0, Right = 1, Down = 2, Left = 3.
        public Shema(int KolStolb, int KolStroch)//конструкт
        {
            this.KolStroch = KolStroch;
            this.KolStolb = KolStolb;
            ShemaBool = new int[KolStroch, KolStolb, 3];
        }

        public void ZapolnenieShemaBool() // в зависимости от номера провода заполняется второй массив
        {
            int i, j;
            for (i = 0; i < this.KolStolb; i++)
            {
                for (j = 0; j < this.KolStroch; j++)
                {
                    switch (Array[i, j])
                    {
                        case 0: break;
                        case 1:
                            ShemaBool[i, j, 1] = 1;
                            ShemaBool[i, j, 2] = 1;
                            break;
                        case 2:
                            ShemaBool[i, j, 2] = 1;
                            ShemaBool[i, j, 3] = 1;
                            break;
                        case 3:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 1] = 1;
                            break;
                        case 4:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 3] = 1;
                            break;
                        case 5:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 1] = 1;
                            ShemaBool[i, j, 2] = 1;
                            break;
                        case 6:
                            ShemaBool[i, j, 3] = 1;
                            ShemaBool[i, j, 1] = 1;
                            ShemaBool[i, j, 2] = 1;
                            break;
                        case 7:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 1] = 1;
                            ShemaBool[i, j, 3] = 1;
                            break;
                        case 8:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 3] = 1;
                            ShemaBool[i, j, 2] = 1;
                            break;
                        case 9:
                            ShemaBool[i, j, 3] = 1;
                            ShemaBool[i, j, 1] = 1;
                            break;
                        case 10:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 2] = 1;
                            break;
                        case 11:
                            ShemaBool[i, j, 0] = 1;
                            ShemaBool[i, j, 1] = 1;
                            ShemaBool[i, j, 2] = 1;
                            ShemaBool[i, j, 3] = 1;
                            break;
                    default:
                            ShemaBool[i, j, 0] = -1;
                            ShemaBool[i, j, 1] = -1;
                            ShemaBool[i, j, 2] = -1;
                            ShemaBool[i, j, 3] = -1;
                            break;
                    }
                }
            }
            Console.WriteLine(Proverka());
        }


        public Boolean Proverka() //проходимся по новосозданному массиву и проверяем, чтобы связи соответствовали друг другу
        {
            int i, j;
            Boolean f = true;
            for (i = 0; i < this.KolStolb; i++)
            {
                for (j = 0; j < this.KolStroch; j++)
                {
                    if (ShemaBool[i, j, 1] == 1 && ShemaBool[i + 1, j, 1] == 0) //вправо
                    {
                        f=false;
                    }
                    if (ShemaBool[i, j, 2] == 1 && ShemaBool[i, j + 1, 1] == 0) //вниз
                    {
                        f=false;
                    }
                }
            }
            return f;
        }

    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Shema elshema = new Shema(1,1);
            elshema.Array[0, 0] = 1;
            elshema.Array[0, 1] = 2;
            elshema.Array[1, 0] = 3;
            elshema.Array[1, 1] = 4;
            elshema.ZapolnenieShemaBool();



        }
    }
}
