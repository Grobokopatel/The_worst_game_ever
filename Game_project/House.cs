using System;
using System.Collections.Generic;
using System.Text;

namespace GameModel
{
    public class House : IInteractable
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
            //Меняет currentSubWorld игрока
            throw new NotImplementedException();
        }
    }
}
