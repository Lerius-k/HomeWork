using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallOpening.Models
{
    public class OpeningInfo //анимическая модель, без поведения, содержит только свойсва
    {
        //свойства проема 
        public string Name { get; set; }
        public double Distance { get; set; }
        public bool IsCorrect {  get; set; }
    }
}
