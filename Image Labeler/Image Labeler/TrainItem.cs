using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Image_Labeler
{
    class TrainItem
    {
        public bool positive;
        public bool take;
        public Rectangle rect;
        public String imgDir;

        public TrainItem()
        {
            positive = take = false;
            rect = new Rectangle();
            imgDir = "No Dir";
        }
        public TrainItem(bool positive, bool take, Rectangle rect, String imgDir)
        {
            this.positive = positive;
            this.take = take;
            this.rect = rect;
            this.imgDir = imgDir;
        }
    }
}
