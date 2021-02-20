using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfGeneticPlants.Genocode.GeneticAlgorithm.TypeGenes;

namespace WorldOfGeneticPlants.Genocode.GeneticAlgorithm
{
    internal class Chromosome : List<Gene>
    {

        /// <summary>
        /// Максимальное колличество генов в геноме
        /// </summary>
        public static int MaxGenes = Properties.BaseWorld.Default.Genocode_MaxGenes;
        /// <summary>
        /// Частота появления условныз генов в обратной пропорции 1/FCG
        /// </summary>
        public static int FrequencyConditionalGene = Properties.BaseWorld.Default.Genocode_FrequencyConditionalGene;
        /// <summary>
        /// Поцент мутаций что может возникнуть в генах хромасомы при клонировании
        /// </summary>
        public static int PercentageMutations = Properties.BaseWorld.Default.Genocode_PercentageMutations;

        /// <summary>
        /// Конструктор хромосомы со случайными генами
        /// </summary>
        public Chromosome()
        {
            for (int i = 0; i < /*MainWorld.GlobalRand.Next(*/MaxGenes/*) + 1*/; i++)
            {
                AddGene();
            }
        }

        /// <summary>
        /// Конструктор хромосомы из строки
        /// </summary>
        /// <param name="str">Строка</param>
        public Chromosome(string str)
        {
            LoadChromosome(str);
        }

        /// <summary>
        /// Клонирование хромосомы с возможностью мутации генов
        /// </summary>
        /// <param name="clone"></param>
        public Chromosome(Chromosome clone)
        {
            for (int i = 0; i < clone.Count; i++)
            {
                if (BeMutated())
                {
                    AddGene();
                }
                else
                {
                    Add(clone[i]);
                }
            }
        }

        /// <summary>
        /// Добавить ген в хромосому
        /// </summary>
        void AddGene()
        {
            if (MainWorld.GlobalRand.Next(FrequencyConditionalGene) != 0)
            {
                Add(new FunctionalGene());
            }
            else
            {
                Add(new ConditionalGene());
            }
        }

        /// <summary>
        /// Мутации генов для расширения в будующем
        /// </summary>
        /// <returns></returns>
        Gene MutatedGene()
        {
            return new FunctionalGene();
        }

        /// <summary>
        /// Должен ли мутировать ген
        /// </summary>
        /// <returns></returns>
        bool BeMutated()
        {
            if (MainWorld.GlobalRand.Next(100) < PercentageMutations)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сохранение хромосомы в строку
        /// </summary>
        /// <returns></returns>
        protected string SaveChromosome()
        {
            string str = "";
            for (int i = 0; i < Count; i++)
            {
                str += this[i].SaveGene();
            }
            return str;
        }
        /// <summary>
        /// Загрузка Хромосомы из строки
        /// </summary>
        /// <param name="str"></param>
        protected void LoadChromosome(string str)
        {
            string[] SplitStr = str.Split(',');
            for (int i = 0; i < SplitStr.Length; i++)
            {
                if (SplitStr[i][0] == '[')
                {

                }
                else
                {

                }
            }
        }


    }
}
