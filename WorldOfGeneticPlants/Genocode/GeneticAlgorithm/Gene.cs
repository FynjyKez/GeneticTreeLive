using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfGeneticPlants.Genocode.GeneticAlgorithm
{
    interface Gene
    {
        int GetGeneCommand();
        int GetOffsetOnNextComand(int[] param);
        string SaveGene();
    }
}
