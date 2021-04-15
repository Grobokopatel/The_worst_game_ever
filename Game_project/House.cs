using System;
using System.Collections.Generic;
using System.Text;

namespace GameModel
{
    class House : IInteractable
    {
        public House(double position)
        {
            Position = position;
        }

        public double Position 
        { 
            get;
        }

        public void Interact(Player player)
        {
            //Меняет SubWorld игрока
            throw new NotImplementedException();
        }
    }
}
