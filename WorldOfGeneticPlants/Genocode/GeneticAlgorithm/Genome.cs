using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfGeneticPlants.Genocode.GeneticAlgorithm.TypeGenes;

namespace WorldOfGeneticPlants.Genocode.GeneticAlgorithm
{
    /// <summary>
    /// Класс определяющий один геном
    /// </summary>
    internal class Genome : List<Chromosome>
    {


        /// <summary>
        /// 
        /// </summary>
        protected static int CountGeneRang;
        /// <summary>
        /// Личный ID генетической цепочки
        /// </summary>
        protected uint ID;
        /// <summary>
        /// ID цепочки являющейся родителем
        /// </summary>
        protected uint ID_parents;
        /// <summary>
        /// ID цепочек являющихся детьми
        /// </summary>
        protected uint[] ID_children;

        private static readonly Dictionary<int, string> GeneComand = new Dictionary<int, string>
         {
             {0, "nothing"},//Ничего
             {1, "grov_DownX_DownY" },
             {2, "grov_DownX_Y" },
             {3, "grov_DownXUpY" },
             {4, "grov_X_UpY" },
             {5, "grov_UpX_UpY" },
             {6, "grov_UpX_Y" },
             {7, "grov_UpX_DownY" },
             {8, "grov_X_DownY" },
             {9, "stepUp" },
             {10, "stepDown" },
         };

        /// <summary>
        /// Максимальное количество хромосом в геноме
        /// </summary>
        private static int MaxChromosome = Properties.BaseWorld.Default.Genocode_MaxChromosome;

        /// <summary>
        /// Инициализация статичных параметров строения генокода
        /// </summary>
        /// <param name="CountGeneRang">Колличество команд возможных в генокоде</param>
        /// <param name="MaxGenes">Максимальное колличество генов</param>
        /*public static void InitializationStatic(int CountGeneRang, int MaxGenes)
        {
            //Genecode.CountGeneRang = CountGeneRang;
            //Genecode.MaxGenes = MaxGenes;
            Gene.InitializationStatic(CountGeneRang, MaxGenes);
        }*/

        /// <summary>
        /// Создаёт геном и заполняет список из случайных генов
        /// </summary>
        public Genome()
        {
            for (int i = 0; i < MaxChromosome; i++)
            {
                AddChromosome();
            }
        }

        /// <summary>
        /// Копирует генокод в текущий геном из предложенного
        /// </summary>
        /// <param name="ThisGenocode">Генокод</param>
        public Genome(Genome ThisGenocode)
        {
            for (int i = 0; i < ThisGenocode.Count; i++)
            {
                Add(new Chromosome(ThisGenocode[i]));
            }
        }

        /// <summary>
        /// Добавляет хромосому в геном
        /// </summary>
        private void AddChromosome()
        {
            Add(new Chromosome());
        }


        /// <summary>
        /// Добавить ген со случайными параметрами
        /// </summary>
        /*private void AddGene()
        {
            Add(new FunctionalGene());
        }*/


        /// <summary>
        /// Клонировать генокод
        /// </summary>
        /// <returns></returns>
        public Genome Clone()
        {
            return new Genome(this);
        }

        /// <summary>
        /// Активация гена
        /// </summary>
        /// <param name="Index">Индекс активируемого гена</param>
        /// <returns></returns>
        /*public int ActivateGene(int Index)
        {
            //this[Index].ActivateGeneCount();
            //return this[Index].Comand;
        }*/

        /*public int NextGeneIndex(int Index)
        {
            //return this[Index].NextGene;
        }*/

        public void ChangeRandomGene()
        {
            /*Random rand = new Random();
            int RandGene = rand.Next(0, MaxGenes);
            //int PieceGene = rand.Next(1, 2);
            //if (PieceGene == 1)
            //{
            this[RandGene].Comand = rand.Next(0, CountGeneRang);
            //}
            //else if (PieceGene == 2)
            //{
            this[RandGene].NextGene = rand.Next(0, MaxGenes);*/
            //}
        }


        public string GetStringGenes()
        {
            string str = "";
            /*char spase = ' ';
            int count = 0;
            foreach (FunctionalGene valueGene in this)
            {
                str += count + "{" + Convert.ToString(valueGene.NextGene).PadRight(2, spase) + "}" +
                    ". [" + Convert.ToString(GeneComand[valueGene.Comand]).PadRight(17, spase) + "]" +
                    " A:" + valueGene.CountActivate + "\n";
                count++;
            }*/
            return str;
        }


        public string GetWorkIndex()
        {
            string str = "";
            /*int[] Indexs = new int[MaxGenes];
            int ActiveIndex = 0;
            int ReGen = -1;

            int i;
            for (i = 0; i < MaxGenes; i++)
            {
                Indexs[i] = -1;
            }
            for (i = 0; i < MaxGenes; i++)
            {
                Indexs[i] = ActiveIndex;
                ActiveIndex = this[ActiveIndex].NextGene;
                ReGen = Array.IndexOf(Indexs, ActiveIndex);
                if (ReGen >= 0)
                    break;
            }



            for (i = 0; i < Indexs.Length; i++)
            {
                if (i == ReGen)
                {
                    str += "[_";
                }
                if (Indexs[i] == -1 && ReGen >= 0)
                {
                    str += "]";
                    break;
                }
                str += Indexs[i] + "_";
            }*/
            return str;
        }


        /// <summary>
        /// Сохранить геном
        /// </summary>
        /// <returns></returns>
        public string SaveGenome()
        {
            return "SaveGenome";
        }

        /// <summary>
        /// Загрузить геном
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool LoadGenome(string str)
        {
            bool FallLoad = false;

            if (FallLoad)
            {
                return true;
            }
            return false;
        }

    }
}
