using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameModel
{
    public class SubWorld
    {
        public static readonly double[] firstWorldBarriers
            = new double[] { 0, 500, 1500, 4000, 6000, 6200, 7300, 8500, 9500, 10000 };

        public List<double> Barriers { get; private set; }
        public List<IInteractable> Interactables { get; private set; }

        public SubWorld(double[] barrierCoords, List<IInteractable> interactables)
        {
            Barriers = new List<double>(barrierCoords.OrderBy(number => number).ToArray());
            Interactables = interactables;
        }

    }
}
