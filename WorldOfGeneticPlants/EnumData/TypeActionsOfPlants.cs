using System;
namespace WorldOfGeneticPlants.EnumData
{
    public enum TypeActionsOfPlants
    {
        //Функциональные гены
        Free,
        GrowUp,
        GrowLeft,
        GrowRight,
        GrowDown,
        /*BeSeed,
        DissipateEnergy,*/
        //Условные гены
        /*IfEnergy25,
        IfEnergy50,
        IfEnergy75,
        IfEnergy100*/
    }

    public static class ActionsOfPlants
    {
        public static int LengthActionsOfPlants = Enum.GetNames(typeof(TypeActionsOfPlants)).Length;
    }
}