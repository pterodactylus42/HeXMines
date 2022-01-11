using System;
using System.Collections.Generic;
using System.Text;

namespace HexMines
{
    public class MineInfo
    {
        public MineInfo(bool isExplosive, MatrixButton mb)
        {
            IsExplosive = isExplosive;
            MineButton = mb;
        }

        public bool IsExplosive { private set; get; }

        public MatrixButton MineButton { private set; get; }
    }
}
