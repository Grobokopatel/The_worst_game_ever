using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    public enum Tools
    {
        Shovel = 0,
        Axe = 1,
        Pickaxe = 2,
        Bomb = 3,
        Saw = 4
    }

    public enum Resources
    {
        Wood = 0,
        Stone = 1,
        Fish = 2,
    }

    public enum Directions
    {
        Left = -1,
        Right = 1,
    }

    public class Player
    {
        public SubWorld currentSubWorld;

        private readonly HashSet<int> tools = new HashSet<int>();

        private readonly Dictionary<int, int> resources = new Dictionary<int, int>();

        private double currentPosition;
        public double CurrentPosition
        {
            get => currentPosition;
            private set
            {
                currentPosition = GetPositionAfterMovement(currentPosition, value);
            }
        }

        public Player(double position, SubWorld world)
        {
            currentSubWorld = world;
            currentPosition = position;
            #region
            /*
            var barriers = world.Barriers;
            for (var i = 0; i < barriers.Count; i += 2)
            {
                var firstEdge = barriers[i];
                var secondEdge = barriers[i + 1];
                if (firstEdge < position && position < secondEdge)
                    throw new InvalidOperationException();
            }*/
            #endregion
        }

        public void AddTool(Tools tool)
        {
            tools.Add((int)tool);
        }

        public bool HasTool(Tools tool)
        {
            return tools.Contains((int)tool);
        }

        public void ChangeAmountOfResource(Resources item, int deltaAmount)
        {
            var itemId = (int)item;
            if (resources.ContainsKey(itemId))
                resources[itemId] += deltaAmount;
            else
            {
                if (deltaAmount < 0)
                    throw new InvalidOperationException();
                resources[itemId] = deltaAmount;
            }
        }

        public int GetAmountOfResource(Resources item)
        {
            var itemId = (int)item;
            if (resources.ContainsKey(itemId))
                return resources[itemId];

            ChangeAmountOfResource(item, 0);
            return 0;
        }

        public double GetPositionAfterMovement(double currentPosition, double nextPosition)
        {
            var greaterCord = Math.Max(currentPosition, nextPosition);
            var lesserCord = Math.Min(currentPosition, nextPosition);

            return currentSubWorld.Barriers.Any(barrierCord => lesserCord <= barrierCord && barrierCord <= greaterCord)
                ? currentPosition : nextPosition;
        }

        public void Move(Directions direction)
        {
            CurrentPosition += (int)direction;
        }

        public void TeleportTo(double coord)
        {
            currentPosition = coord;
        }
    }
}
