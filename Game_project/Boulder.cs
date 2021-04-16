using System;
using System.Collections.Generic;
using System.Text;

namespace GameModel
{
    public class Boulder : IInteractable
    {
        public Boulder(double position)
        {
            Position = position;
        }

        public double Position
        {
            get;
        }

        public void Interact(Player player)
        {
            if (player.HasTool(Tools.Pickaxe))
            {
                //PlayAnimation(); ?
                player.AddDeltaResources(Resources.Stone, 1);
            }
            else
            {

            }
        }
    }
}
