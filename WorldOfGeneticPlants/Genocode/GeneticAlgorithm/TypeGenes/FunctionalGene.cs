using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfGeneticPlants.Genocode.GeneticAlgorithm.TypeGenes
{
    /// <summary>
    /// Класс одиночного функционального гена
    /// </summary>
    internal class FunctionalGene : Gene
    {
        /// <summary>
        /// Команда гена
        /// </summary>
        int command { get; }
        /// <summary>
        /// Смещение гена
        /// </summary>
        const int offset = 1;
        /// <summary>
        /// Создание гена с случайной командой
        /// </summary>
        public FunctionalGene()
        {
            command = MainWorld.GlobalRand.Next(EnumData.ActionsOfPlants.LengthActionsOfPlants);
        }
        /// <summary>
        /// Создание гена с заданной командой
        /// </summary>
        /// <param name="Command"></param>
        public FunctionalGene(int Command)
        {
            command = Command;
        }
        /// <summary>
        /// Вернуть команду гена
        /// </summary>
        /// <returns></returns>
        public int GetGeneCommand()
        {
            return command;
        }
        /// <summary>
        /// Вернуть смещение гена
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public int GetOffsetOnNextComand(int[] _ = null)
        {
            return offset;
        }

        /// <summary>
        /// Сохранить ген в стоковой форме
        /// </summary>
        /// <returns></returns>
        public string SaveGene()
        {
            string str = GetGeneCommand() + ",";
            return str;
        }
    }
}
