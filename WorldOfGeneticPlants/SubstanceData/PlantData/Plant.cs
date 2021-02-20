using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfGeneticPlants.EnumData;
using WorldOfGeneticPlants.Genocode;
//using WorldOfGeneticPlants.GeneticAlgorithm;

namespace WorldOfGeneticPlants.SubstanceData.PlantData
{
    /// <summary>
    /// Класс описывающий один экземпляр растения
    /// </summary>
    public class Plant
    {
        /// <summary>
        /// Максимальный срок жизни растения в итерациях 
        /// </summary>
        private static readonly uint PropertyEndLivelong = Properties.BaseWorld.Default.Plant_Lifelong;

        /// <summary>
        /// Список для создания очереди добавления новых блоков растения
        /// </summary>
        private static List<BlockPlant> StackBlocksPlant = new List<BlockPlant>();

        private List<BlockPlant> ListBlocksPlant = new List<BlockPlant>();
        /// <summary>
        /// Номер генокода
        /// </summary>
        private int IndexGenocode { get; set; }

        /// <summary>
        /// Оставшийся срок жизни растения
        /// </summary>
        private uint Livelong;

        public Plant(int X, int Y)
        {
            Livelong = PropertyEndLivelong;
            IndexGenocode = Genomes.AddGenocode();
            AddNewBlockPlant(X, Y);
            AddListNewBlocksPlant();
        }

        public Plant(int X, int Y, int ParentsIndex)
        {
            //IndexGenocode = ParentsIndex;
            Livelong = PropertyEndLivelong;
            IndexGenocode = Genomes.AddGenocode(ParentsIndex);
            AddNewBlockPlant(X, Y);
            AddListNewBlocksPlant();
        }

        /// <summary>
        /// Функция активации для растения
        /// </summary>
        public void Activate()
        {
            LostLivelongCount();
            if(Livelong == 0)
            {
                DestroyPlant(this);
            }
            GeneticProgram();

            //Проверка на смерть и уничтожение блоков
            StackBlocksPlant = ListBlocksPlant.FindAll(BlockPlant.DeathByEnergyStarvation);
            StackBlocksPlant.ForEach(BlockPlant.DieBlockInWorldMap);
            ListBlocksPlant.RemoveAll(BlockPlant.DeathByEnergyStarvation);
            StackBlocksPlant.Clear();

        }

        /// <summary>
        /// Функция обработки физики для растения
        /// </summary>
        public void Physics()
        {
            foreach (BlockPlant OneBlockPlant in ListBlocksPlant)
            {
                OneBlockPlant.CanFall();
            }
            foreach (BlockPlant OneBlockPlant in ListBlocksPlant)
            {
                OneBlockPlant.Fall();
            }
        }

        #region Активируемые функции растения на каждом тике

        /// <summary>
        /// Уменьшить счетчик максимальной жизни
        /// </summary>
        private void LostLivelongCount()
        {
            Livelong--;
        }

        /// <summary>
        /// Запустить генетическую программу
        /// </summary>
        private void GeneticProgram()
        {
            foreach (BlockPlant OneBlockPlant in ListBlocksPlant)
            {
                OneBlockPlant.Activate();
                ///rand = MainWorld.GlobalRand.Next(ActionsOfPlants.LengthActionsOfPlants);    
                switch (Genomes.GetGeneCommand(IndexGenocode, 0, OneBlockPlant.GoActiveGene()))
                {
                    case (int)TypeActionsOfPlants.GrowUp:
                        AddNewBlockPlant(OneBlockPlant.X, OneBlockPlant.Y + 1, OneBlockPlant);
                        break;
                    case (int)TypeActionsOfPlants.GrowLeft:
                        AddNewBlockPlant(OneBlockPlant.X - 1, OneBlockPlant.Y, OneBlockPlant);
                        break;
                    case (int)TypeActionsOfPlants.GrowRight:
                        AddNewBlockPlant(OneBlockPlant.X + 1, OneBlockPlant.Y, OneBlockPlant);
                        break;
                    case (int)TypeActionsOfPlants.GrowDown:
                        AddNewBlockPlant(OneBlockPlant.X, OneBlockPlant.Y - 1, OneBlockPlant);
                        break;
                    default:
                        break;
                }
                OneBlockPlant.NextActiveGene();
            }

            if (StackBlocksPlant.Count > 0)
            {
                AddListNewBlocksPlant();
            }
        }

        #endregion


        /// <summary>
        /// Добавить безусловно новый блок растнеия
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void AddNewBlockPlant(int X, int Y)
        {
            TypeBlocks GetBlock = MainWorld.GetTypeBlockMap(X, Y);
            if (GetBlock != TypeBlocks.Blocked)
            {
                StackBlocksPlant.Add(new BlockPlant(X, Y));
                MainWorld.SetPointWorldMap(X, Y, TypeBlocks.Plant);
            }
        }
        /// <summary>
        /// Добавить новый блок растения по условию роста
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="OneBlockPlant"></param>
        private void AddNewBlockPlant(int X, int Y, BlockPlant OneBlockPlant)
        {
            if (OneBlockPlant.CanEnergyToGrowth)
            {
                TypeBlocks GetBlock = MainWorld.GetTypeBlockMap(X, Y);
                if (GetBlock != TypeBlocks.Blocked && (GetBlock == TypeBlocks.Void || GetBlock == TypeBlocks.Air))
                {
                    OneBlockPlant.SpendEnergyToGrowth();
                    StackBlocksPlant.Add(new BlockPlant(X, Y));
                    MainWorld.SetPointWorldMap(X, Y, TypeBlocks.Plant);
                }
            }
        }
        /// <summary>
        /// Инициализировать все новые блоки растения
        /// </summary>
        private void AddListNewBlocksPlant()
        {
            this.ListBlocksPlant.AddRange(StackBlocksPlant);
            StackBlocksPlant.Clear();
        }

        /// <summary>
        /// Предикат смерти растения по отмиранию всех клеток
        /// </summary>
        /// <returns></returns>
        public static bool MustDiePlant(Plant plant)
        {
            return plant.ListBlocksPlant.Count <= 0;
        }
        /// <summary>
        /// Предикат смерти по концу сетчика жизни
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        public static bool EndLivelongCount(Plant plant)
        {
            return plant.Livelong <= 0;
        }


        public delegate void PlantDie(Plant plant);
        public event PlantDie PlantDieNow;

        /// <summary>
        /// Уничтожить растение
        /// </summary>
        /// <param name="plant"></param>
        public static void DestroyPlant(Plant plant)
        {
            DestroyAllBlocksPlant(plant);
            plant.ListBlocksPlant.Clear();
            plant.PlantDieNow(plant);
        }

        /// <summary>
        /// Уничтожение всех блоков растения на глобальной карте 
        /// </summary>
        /// <param name="plant"></param>
        private static void DestroyAllBlocksPlant(Plant plant)
        {
            plant.ListBlocksPlant.ForEach(BlockPlant.DieBlockInWorldMap);
        }


    }
}
