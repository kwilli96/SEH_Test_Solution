using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SEH_Test_Solution
{
    public class ImageContainer
    {
        private bool isChecked;
        private BitmapImage image;
        private byte[] imageMap;

        public bool IsChecked { get => isChecked; set => isChecked = value; }
        public BitmapImage Image { get => image; set => image = value; }
        public byte[] ImageMap { get => imageMap; set => imageMap = value; }

        public ImageContainer()
        {
            IsChecked = false;
        }
    }
}
