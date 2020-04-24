using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromMetMah
{
    class GameState
    {
        private Level CurrentLevel;
        private List<Level> Levels;

        public GameState(List<Level> levels)
        {
            Levels = levels;
        }
    }
}
