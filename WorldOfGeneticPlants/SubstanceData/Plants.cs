using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorldOfGeneticPlants.SubstanceData.PlantData;

namespace WorldOfGeneticPlants.SubstanceData
{
    /// <summary>
    /// Класс определяющий растения
    /// </summary>
    internal class Plants : SubstanceObject
    {
        private static List<Plant> StackListPlant = new List<Plant>();
        private static uint CountPlants = 0;
        private static List<Plant> ListPlants = new List<Plant>();


        public Plants()
        {

        }

        /// <summary>
        /// Добавить одно растение по координатам
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public static Plant AddPlant(int X, int Y)
        {
            CountPlants++;
            Plant plant = new Plant(X, Y);
            ListPlants.Add(plant);
            plant.PlantDieNow += OnePlant_PlantDieNow;
            return plant;
        }

        /// <summary>
        /// Добавить растение по координатам от родительских генов
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="indexParents">Генокод родителя</param>
        public static Plant AddPlant(int X, int Y, int IndexParents)
        {
            CountPlants++;
            Plant plant = new Plant(X, Y, IndexParents);
            ListPlants.Add(plant);
            plant.PlantDieNow += OnePlant_PlantDieNow;
            return plant;
        }

        /// <summary>
        /// Функция активации
        /// </summary>
        public void Activate()
        {
            //Активация для каждого растения
            foreach (Plant OnePlant in ListPlants)
            {
                OnePlant.Activate();
            }

            //Удаление умерших растений по окончанию максимального срока жизни
           if (StackListPlant.Count > 0)
            {
                ListPlants.RemoveAll(Plant.EndLivelongCount);
                StackListPlant.Clear();
            }

            if(ListPlants.Count == 0)
            {
                DieLastPlantInWorld();
            }
            //ListPlants.FindAll(Plant.MustDiePlant).ForEach(Plant.DestroyAllBlocksPlant);
            //ListPlants.RemoveAll(Plant.EndLivelongCount);
        }

        /// <summary>
        /// Добавляет растение в списк удаления(по причине смерти)
        /// </summary>
        /// <returns></returns>
        private static void OnePlant_PlantDieNow(Plant plant)
        {
            CountPlants--;
            StackListPlant.Add(plant);
        }


        /// <summary>
        /// Уничтожить растение в списке
        /// </summary>
        /// <param name="DestroyPlant"></param>
        public void DestroyPlant(Plant DestroyPlant)
        {
            ListPlants.Remove(DestroyPlant);
            if (ListPlants.Count == 0)
            {
                DieLastPlantInWorld();
            }
        }

        /// <summary>
        /// Функция обработки физики на растения
        /// </summary>
        public void Physics()
        {
            foreach (Plant OnePlant in ListPlants)
            {
                OnePlant.Physics();
            }
        }


        public delegate void DieLastPlant();
        public static event DieLastPlant DieLastPlantInWorld;

    }
}
