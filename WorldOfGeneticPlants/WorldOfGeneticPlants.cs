using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorldOfGeneticPlants.EnumData;
using WorldOfGeneticPlants.SubstanceData;

namespace WorldOfGeneticPlants
{
    /// <summary>
    /// Мировой класс.
    /// </summary>
    public static class MainWorld
    {
        /// <summary>
        /// Глобальная переменная для получения рандомизированных параметров
        /// </summary>
        internal static Random GlobalRand = new Random();

        /// <summary>
        /// Дополнительный поток
        /// </summary>
        //private static Thread threadMain = new Thread(MainWorld.GoOneWorldTick);

        public static readonly int WorldHeight = Properties.BaseWorld.Default.WorldHeight;
        public static readonly int WorldWidth = Properties.BaseWorld.Default.WorldWidth;

        /// <summary>
        /// Сколько блоков вверх влияют на полачаемую энергию
        /// </summary>
        private static readonly int PropertySunEnergyTakeLevels = Properties.BaseWorld.Default.World_SunEnergyTakeLevels;
        /// <summary>
        /// Модификатор получаемой энергии за уровни по вертикали
        /// </summary>
        private static readonly int PropertySunEnergyModifierForLevel = Properties.BaseWorld.Default.World_SunEnergyModifierForLevel;
        /// <summary>
        /// Базовое получение энегрии блоком
        /// </summary>
        private static readonly int PropertySunEnergyBase = Properties.BaseWorld.Default.World_SunEnergyBase;
        /// <summary>
        /// Графитационная константа действующая на блоки
        /// </summary>
        public static readonly int PropertyGravitation = Properties.BaseWorld.Default.World_Gravitation;

        /// <summary>
        /// Карта типов блоков
        /// </summary>
        private static TypeBlocks[,] MapTypeBlocks;
        /// <summary>
        /// Карта заблокированных для изменений блоков
        /// </summary>
        private static bool[,] MapBlockedBlocks;
        /// <summary>
        /// Список объектов существующих в симуляции
        /// </summary>
        private static List<SubstanceObject> BlocksSubstance = new List<SubstanceObject>();
        /// <summary>
        /// Инициализация класса MainWorld
        /// </summary>
        static MainWorld()
        {
            MapTypeBlocks = new TypeBlocks[WorldWidth, WorldHeight];
            MapBlockedBlocks = new bool[WorldWidth, WorldHeight];
            InitBlockedPointMap(true);

            NewAir();
            NewPlants();
        }

        /// <summary>
        /// Создание среды в симуляции
        /// </summary>
        private static void NewAir()
        {
            for (int i = 0; i < WorldWidth; i++)
            {
                for (int j = 0; j < WorldHeight; j++)
                {
                    MapTypeBlocks[i, j] = TypeBlocks.Air;
                }
            }
            BlocksSubstance.Add(new Air());
        }
        /// <summary>
        /// Создание растений в симуляции
        /// </summary>
        private static void NewPlants()
        {
            BlocksSubstance.Add(new Plants());
            Plants.AddPlant(WorldWidth / 2, 0);
            Plants.DieLastPlantInWorld += Plants_DieLastPlantInWorld;
        }

        #region тестовые функции по эвентам
        /// <summary>
        /// Функция создающее новое растение в стандартных координатах при смерти всех растений
        /// </summary>
        private static void Plants_DieLastPlantInWorld()
        {
            Plants.AddPlant(WorldWidth / 2, 0, Genocode.Genomes.GetLastGenocodeIndex());
        }
        #endregion


        #region Тест для функций потока

        /// <summary>
        /// Запустить поток обработки мира
        /// </summary>
        public static void StartThread()
        {
            //threadMain.Start();
        }
        /// <summary>
        /// Преостановить поток обработки мира
        /// </summary>
        public static void AbortThread()
        {
            //threadMain.Abort();
        }
        #endregion


        /// <summary>
        /// Функция запускающая тригеры объектов симуляции за один тик
        /// </summary>
        public static void GoOneWorldTick()
        {
            InitBlockedPointMap(false);

            foreach (SubstanceObject OneWorldSubstance in BlocksSubstance)
            {
                OneWorldSubstance.Physics();
            }
            foreach (SubstanceObject OneWorldSubstance in BlocksSubstance)
            {
                OneWorldSubstance.Activate();
            }  
        }
        




        /// <summary>
        /// Получить тип блока на карте
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static TypeBlocks GetTypeBlockMap(int X, int Y)
        {
            if (X >= 0 && X < WorldWidth && Y >= 0 && Y < WorldHeight)
            {
                return MapTypeBlocks[X, Y];
            }
            return TypeBlocks.Blocked;
        }
        /// <summary>
        /// Установка и изменение типа блока на карте с блокировкой точки изменения
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="TypeBlocks"></param>
        public static void SetPointWorldMap(int X, int Y, TypeBlocks TypeBlocks)
        {
            if (X >= 0 && X < WorldWidth && Y >= 0 && Y < WorldHeight)
            {
                MapTypeBlocks[X, Y] = TypeBlocks;
                TruePointChangeMap(X, Y);
            }
        }
        /// <summary>
        /// Установка и изменение типа блока на карте с блокировкой точки изменения
        /// </summary>
        /// <param name="point"></param>
        /// <param name="TypeBlocks"></param>
        public static void SetPointWorldMap(Point point, TypeBlocks TypeBlocks)
        {
            if (point.X >= 0 && point.X < WorldWidth && point.Y >= 0 && point.Y < WorldHeight)
            {
                MapTypeBlocks[point.X, point.Y] = TypeBlocks;
                TruePointChangeMap(point.X, point.Y);
            }
        }
        /// <summary>
        /// Установка массива блоков на карте
        /// </summary>
        /// <param name="points"></param>
        /// <param name="TypeBlocks"></param>
        public static void SetPointsWorldMap(Point[] points, TypeBlocks TypeBlocks)
        {
            foreach(Point OnePoint in points)
            {
                SetPointWorldMap(OnePoint, TypeBlocks);
            }
        }
        /// <summary>
        /// Перемещение блока на карте с проверкой
        /// </summary>
        /// <returns><code>true</code> если блок был пустым и успешно перемещён <code>false</code> блок не был перемещён</returns>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        public static bool MovePointWorldMap(int X, int Y, int newX, int newY)
        {
            TypeBlocks TypeBlock = GetTypeBlockMap(newX, newY);
            if (TypeBlock == TypeBlocks.Void || TypeBlock == TypeBlocks.Air)
            {
                TypeBlocks MovedBlock = GetTypeBlockMap(X, Y);
                SetPointWorldMap(X, Y, TypeBlock);
                SetPointWorldMap(newX, newY, MovedBlock);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Функция возвращающая точку карды изменений
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static bool GetPointChangeMap(int X, int Y)
        {
            if(X >= 0 && X < WorldWidth && Y >= 0 && Y < WorldHeight)
            {
                return MapBlockedBlocks[X, Y];
            }
            return false;
        }
        /// <summary>
        /// Закрепление точки карты изменений
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public static void TruePointChangeMap(int X, int Y)
        {
            MapBlockedBlocks[X, Y] = true;
        }
        /// <summary>
        /// Функция вычисляющая энегрию получаемую точкой на карте
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static int SunEnergyForPointMap(int X, int Y)
        {
            int energy = 0;
            TypeBlocks typeBlock;
            for (int _y = Y + 1; _y < Y + PropertySunEnergyTakeLevels + 1; _y++) {
                typeBlock = GetTypeBlockMap(X, _y);
                if(typeBlock == TypeBlocks.Air || typeBlock == TypeBlocks.Void)
                {
                    energy += (int)(_y / PropertySunEnergyModifierForLevel) + PropertySunEnergyBase;
                }
            }
            return energy;
        }
        /// <summary>
        /// Изменение блокирования точек для всей карты симуляции
        /// </summary>
        /// <param name="value">при <code>true</code> вся карта заблокированна, при <code>false</code> вся карта разблокирована для изменений</param>
        private static void InitBlockedPointMap(bool value)
        {
            for (int i = 0; i < WorldWidth; i++)
            {
                for (int j = 0; j < WorldHeight; j++)
                {
                    MapBlockedBlocks[i, j] = value;
                }
            }
        }
    }
}
