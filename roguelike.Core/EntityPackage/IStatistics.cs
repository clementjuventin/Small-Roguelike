using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    interface IStatistics
    {
        int HealthPoints { get; set; }
        int Damages { get; set; }
        int Vitality { get; set; }
        int Dexterity { get; set; }
        int Level { get; set; }
        int Armor { get; set; }

        float CriticalChange { get; set; }
    }
}
