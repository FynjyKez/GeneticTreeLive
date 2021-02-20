using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfGeneticPlants.Genocode.GeneticAlgorithm;

namespace WorldOfGeneticPlants.Genocode
{
    /// <summary>
    /// Класс определяющий все геномы которые были созданы
    /// </summary>
    internal static class Genomes
    {
        /// <summary>
        /// Индекс скорости мутаций геномов
        /// </summary>
        static int MutationsIndex = Properties.BaseWorld.Default.Genocode_MutationsIndex;
        /// <summary>
        /// Счетчик реализованных геномов
        /// </summary>
        static uint CountGenocodes = 0;
        /// <summary>
        /// Список геномов
        /// </summary>
        static List<Genome> ListGenocodes = new List<Genome>();
        /// <summary>
        /// Реализация класса Genomes
        /// </summary>
        static Genomes()
        {
        }
        /// <summary>
        /// Возвращает геном по индексу из списка
        /// </summary>
        public static Genome GetGenocode(int Index)
        {
            return ListGenocodes[Index];
        }
        /// <summary>
        /// Вернуть индекс последнего генокода
        /// </summary>
        public static int GetLastGenocodeIndex()
        {
            return ListGenocodes.Count - 1;
        }
        /// <summary>
        /// Добавляет новый геном со случайным генокодом
        /// </summary>
        /// <returns></returns>
        public static int AddGenocode()
        {
            IncreaseCountGenocode();
            ListGenocodes.Add(new Genome());
            return ListGenocodes.Count - 1;
        }
        /// <summary>
        /// Добавляет новый геном, мутируя из родительского генома найденного по индексу
        /// </summary>
        /// <param name="ParentIndex"></param>
        public static int AddGenocode(int ParentIndex)
        {
            IncreaseCountGenocode();
            if (MutationsIndex == 1)
            {
                ListGenocodes.Add(new Genome(ListGenocodes[ParentIndex]));
                return ListGenocodes.Count - 1;
            }
            return ParentIndex;
        }

        /// <summary>
        /// Получить команду гена по индексам
        /// </summary>
        /// <param name="GenomeIndex"></param>
        /// <param name="ChromosomeIndex"></param>
        /// <param name="GeneIndex"></param>
        /// <returns></returns>
        public static int GetGeneCommand(int GenomeIndex, int ChromosomeIndex, int GeneIndex)
        {
            return ListGenocodes[GenomeIndex][ChromosomeIndex][GeneIndex].GetGeneCommand();
        }

        /// <summary>
        /// Увеличить счетчик колличества генетических кодов
        /// </summary>
        static private void IncreaseCountGenocode() => CountGenocodes++;

    }
}
