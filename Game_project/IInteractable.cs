using System;
using System.Collections.Generic;
using System.Text;

namespace GameModel
{
    public interface IInteractable
    {
        public void Interact(Player player);
        public double Position
        {
            get;
        }
    }
}
