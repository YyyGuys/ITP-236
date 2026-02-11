using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EID
{
    public class EidDelegate
    {
        public static int SumArray(int[] numbers)
        {
            int total = 0;
            foreach (var number in numbers)
            {
                total += number;
            }   
            return total;
        }
    }
}
