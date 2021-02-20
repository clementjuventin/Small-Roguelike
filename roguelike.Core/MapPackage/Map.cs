using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.MapPackage
{
    class Map
    {
        public ModelBuilder ModelBuilder { get; set; }

        public Map()
        {
            ModelBuilder = new ModelBuilder(40, 0.9999, 0.2);
        }
    }
}
