using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfGeneticPlants.EnumData;
using WorldOfGeneticPlants.Genocode.GeneticAlgorithm;

namespace WorldOfGeneticPlants.SubstanceData.PlantData
{
    internal class BlockPlant
    {
        private static readonly int NewBlockEnergy = Properties.BaseWorld.Default.BlockPlant_EnergyNewBlockPlant;
        private static readonly int PropertyEnergyConsumptionForTick = Properties.BaseWorld.Default.BlockPlant_EnergyConsumptionForTick;
        //private static readonly int NewGrowthBlockEnergy = 100;
        private static readonly int MaxGrowthEnergy = Properties.BaseWorld.Default.BlockPlant_EnergyConsumptionForNewBlock;
        //private static readonly int MaxLevelEnergy = 100;

        public int X { get; set; }
        public int Y { get; set; }
        public int Energy { get; set; }
        public int ActiveGene { get; set; }
        private bool fall = false;


        public BlockPlant(int X, int Y)
        {
            Energy = NewBlockEnergy;
            ActiveGene = 0;
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Активация для обязательных расчетов
        /// </summary>
        public void Activate()
        {
            GetEnergyForTickBySun();
            LostEnergyForTick();
        }

        /// <summary>
        /// Вернуть индекс активного гена
        /// </summary>
        /// <returns></returns>
        public int GoActiveGene()
        {
            return ActiveGene;
        }

        /// <summary>
        /// Перейти к следующему гену ! необходимо реализовать условные гены
        /// </summary>
        /// <returns></returns>
        public int NextActiveGene()
        {
            ActiveGene = (ActiveGene + 1) % Chromosome.MaxGenes;
            return ActiveGene;
        }

        /// <summary>
        /// Проверка блока на возможность упасть
        /// </summary>
        /// <returns><c>true</c> если может упасть. <code>false</code> при невозможности упасть</returns>
        public bool CanFall()
        {
            TypeBlocks SideBlock;
            if(Y <= 0)
            {
                fall = false;
                return false;
            }
            
            SideBlock = MainWorld.GetTypeBlockMap(X - 1, Y);
            if(SideBlock == TypeBlocks.Plant || SideBlock == TypeBlocks.Seed || SideBlock == TypeBlocks.Earth)
            {
                return false;
            }
            SideBlock = MainWorld.GetTypeBlockMap(X + 1, Y);
            if (SideBlock == TypeBlocks.Plant || SideBlock == TypeBlocks.Seed || SideBlock == TypeBlocks.Earth)
            {
                return false;
            }
            SideBlock = MainWorld.GetTypeBlockMap(X, Y - 1);
            if (SideBlock == TypeBlocks.Plant || SideBlock == TypeBlocks.Seed || SideBlock == TypeBlocks.Earth)
            {
                return false;
            }
            /*SideBlock = MainWorld.GetTypeBlockMap(X, Y + 1);
            if (SideBlock == TypeBlocks.Plant || SideBlock == TypeBlocks.Seed || SideBlock == TypeBlocks.Earth)
            {
                return false;
            }*/

            fall = true;
            return true;
        }

        /// <summary>
        /// Функция падения (необходимо проверить будет баговать при гравитации >1)
        /// </summary>
        public void Fall()
        {
            int newY;
            if (Y > MainWorld.PropertyGravitation)
            {
                newY = Y - MainWorld.PropertyGravitation;
            }
            else
                newY = 0;
            if (fall && MainWorld.MovePointWorldMap(X, Y, X, newY))
            {
                Y = newY;
                fall = false;
            }
        }

        /// <summary>
        /// Проверки и изменение энергии блока
        /// </summary>
        /// 
        private void GetEnergyForTickBySun()
        {
            Energy += MainWorld.SunEnergyForPointMap(X,Y);
        }
        private void LostEnergyForTick()
        {
            Energy -= PropertyEnergyConsumptionForTick;
        }
        public bool CanEnergyToGrowth
        {
            get
            {
                if (this.Energy > MaxGrowthEnergy)
                {
                    return true;
                }
                return false;
            }
        }
        public void SpendEnergyToGrowth()
        {
            if (this.Energy > MaxGrowthEnergy)
            {
                this.Energy -= MaxGrowthEnergy;
            }
        }

        /// <summary>
        /// Предикат определения смерти блока
        /// </summary>
        /// <param name="blockPlant"></param>
        /// <returns></returns>
        public static bool DeathByEnergyStarvation(BlockPlant blockPlant)
        {
            return blockPlant.Energy <= 0;
        }
        /// <summary>
        /// Удалить блок растения с карты
        /// </summary>
        /// <param name="blockPlant"></param>
        public static void DieBlockInWorldMap(BlockPlant blockPlant)
        {
            MainWorld.SetPointWorldMap(blockPlant.X, blockPlant.Y, TypeBlocks.Void);
        }


        public void Physics()
        {
            
        }



        delegate void DestroyBlock();
        event DestroyBlock Destroy;

        private void ReEnergy(ref int[,] WorldObject)
        {
            /*int NewEnergy = this.Energy;
            int MaxShadowLevel = 3;
            NewEnergy -= 5;

            int i = 1;
            int Shadow = 0;
            int MaxY = WorldObject.GetLength(1);
            while (this.coordinateY + i < MaxY && Shadow < MaxShadowLevel && i < 10)
            {
                if (WorldObject[this.coordinateX, this.coordinateY + i] != 0)
                {
                    Shadow++;
                }
                i++;
            }
            if (this.coordinateY + i >= WorldObject.GetLength(1))
            {
                Shadow = MaxShadowLevel;
            }

            NewEnergy += Convert.ToInt32((4 * (this.coordinateY + 20)) * Math.Cos((Math.PI * 0.5 * Shadow) / MaxShadowLevel));*/

            /*if (NewEnergy < 0)
            {
                NewEnergy = 0;
            }
            else */
            /*if (NewEnergy > MaxLevelEnergy)
            {
                NewEnergy = MaxLevelEnergy;
            }

            this.Energy = NewEnergy;*/
        }

    }
}
