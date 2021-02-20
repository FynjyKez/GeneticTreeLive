using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfGeneticPlants.Genocode.GeneticAlgorithm.TypeGenes
{
    /// <summary>
    /// Класс одиночного условного гена
    /// </summary>
    internal class ConditionalGene : Gene
    {
        /// <summary>
        /// Условие гена
        /// </summary>
        int condition { get; }
        /// <summary>
        /// Параметр условия
        /// </summary>
        int param { get; }
        int conditionOperator { get;}
        /// <summary>
        /// Смещение условного гена
        /// </summary>
        int offset { get; }
        const int DefaultOffset = 1;

        //Необходимо пересмотреть условия задания
        /// <summary>
        /// Случайный условный ген
        /// </summary>
        public ConditionalGene() 
        {
            condition = MainWorld.GlobalRand.Next(2);
            param = MainWorld.GlobalRand.Next(2);
            conditionOperator = MainWorld.GlobalRand.Next(2);
            offset = MainWorld.GlobalRand.Next(2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Сondition">Условие гена</param>
        /// <param name="Offset">Смещение гена</param>
        public ConditionalGene(int Condition, int Param, int ConditionOperator, int Offset)
        {
            condition = Condition;
            param = Param;
            conditionOperator = ConditionOperator;
            offset = Offset;
        }
        /// <summary>
        /// Получить команду гена
        /// </summary>
        /// <returns></returns>
        public int GetGeneCommand()
        {
            return condition;
        }

        /// <summary>
        /// Получить параметр условия
        /// </summary>
        /// <returns></returns>
        public int GetСonditionParam()
        {
            return param;
        }

        public int GetOffset()
        {
            return offset;
        }

        /// <summary>
        /// Получить смещение на следующую команду
        /// </summary>
        /// <returns></returns>
        public int GetOffsetOnNextComand(int[] Param = null)
        {
            if (conditionOperator == -1)
            {
                return param < Param[0] ? offset : DefaultOffset;
            }
            else if(conditionOperator == 0)
            {
                return param == Param[0] ? offset : DefaultOffset;
            }
            else if(conditionOperator == 1)
            {
                return param > Param[0] ? offset : DefaultOffset;
            }
            return DefaultOffset;
        }


        /// <summary>
        /// Сохранить ген в стоковой форме
        /// </summary>
        /// <returns></returns>
        public string SaveGene()
        {
            string str = "[if]" + GetGeneCommand() +"[offset]" + GetOffset() + ",";
            return str;
        }
    }
}
