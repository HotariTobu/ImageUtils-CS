using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivDiv
{
    internal static class SizeTypeExtension
    {
        public static double Ratio(this SizeType sizeType)
        {
            switch (sizeType)
            {
                case SizeType.A: return 1.41421356;
                case SizeType.B: return 1.41421356;
            }

            return 1.41421356;
        }
    }
}
