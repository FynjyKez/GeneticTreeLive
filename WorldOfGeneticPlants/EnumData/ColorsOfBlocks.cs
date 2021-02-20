using System.Collections.Generic;
using System.Drawing;

namespace WorldOfGeneticPlants.EnumData
{
    public static class ColorsOfBlocks
    {
        public static Dictionary<TypeBlocks, Color> ValueDictionary = new Dictionary<TypeBlocks, Color>();

        static ColorsOfBlocks()
        {
            ValueDictionary.Add(TypeBlocks.Void, Color.Black);
            ValueDictionary.Add(TypeBlocks.Air, Color.Blue);
            ValueDictionary.Add(TypeBlocks.Earth, Color.Brown);
            ValueDictionary.Add(TypeBlocks.Plant, Color.Green);
            ValueDictionary.Add(TypeBlocks.Seed, Color.Gray);
        }
    }
}