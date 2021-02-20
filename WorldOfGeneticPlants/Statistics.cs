using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfGeneticPlants.Genocode;
using WorldOfGeneticPlants.Genocode.GeneticAlgorithm;
using WorldOfGeneticPlants.EnumData;

namespace WorldOfGeneticPlants
{
    public class Statistics
    {






        /// <summary>
        /// 
        /// </summary>
        /// <param name="IndexGenome"></param>
        /// <returns></returns>
        public static string GetGenomeProgram(int IndexGenome)
        {
            string str = "";

            //
            IndexGenome = Genomes.GetLastGenocodeIndex();
            //

            foreach (Chromosome Chomo in Genomes.GetGenocode(IndexGenome))
            {
                str += "Chromo:\n";
                foreach (Gene Gen in Chomo)
                {
                    str += (TypeActionsOfPlants)Gen.GetGeneCommand() + "\n";
                }
            }
            return str;
        }
    }
}
