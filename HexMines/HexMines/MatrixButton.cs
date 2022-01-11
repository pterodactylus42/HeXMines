using Xamarin.Forms;

namespace HexMines
{
    public class MatrixButton : Button
    {
        public int Index { get; set; }

        public MatrixButton(int i)
        {
            Index = i;
        }

    }
}
