using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDisplay
{
    public static class Constants
    {
        public const int SQUARE_GRID_LENGTH = 160;
        public const int SMALL_CARD_LENGTH = 140;
        public const int BIG_CARD_LENGTH = SQUARE_GRID_LENGTH + SMALL_CARD_LENGTH;
        public const int EDGE = 20;
        public const int MAX_ROW = 4;
        public const int MAX_COLOMN = 5;
        public const int INSIDE_HEIGHT = MAX_ROW * SQUARE_GRID_LENGTH + 4 * EDGE;
        public const int INSIDE_WIDTH = MAX_COLOMN * SQUARE_GRID_LENGTH + 4 * EDGE;
    }
}
