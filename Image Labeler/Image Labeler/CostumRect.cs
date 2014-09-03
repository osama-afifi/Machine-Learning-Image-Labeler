namespace dotnetthoughts
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class CustomPictureBox : PictureBox
    {
        private Rectangle _rectangle;
        private Point _startingPoint;
        private Point _finishingPoint;
        private bool _isDrawing;


        public Rectangle getRect()
        {
            return _rectangle;
        }
        public Point startPoint()
        {
            return _startingPoint;
        }
        public Point finishPoint()
        {
            return _finishingPoint;
        }
        public CustomPictureBox()
        {
            Cursor = Cursors.Cross;
            _startingPoint = new Point();
            _finishingPoint = new Point();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isDrawing = true;
                _startingPoint = new Point(e.X, e.Y);
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && _isDrawing)
            {
                _finishingPoint = new Point(e.X, e.Y);
                if (e.X > _startingPoint.X)
                {
                    _rectangle = new Rectangle(
                        _startingPoint.X <= e.X ? _startingPoint.X : e.X,
                        _startingPoint.Y <= e.Y ? _startingPoint.Y : e.Y,
                        e.X - _startingPoint.X <= 0 ? _startingPoint.X - e.X : e.X - _startingPoint.X,
                        e.Y - _startingPoint.Y <= 0 ? _startingPoint.Y - e.Y : e.Y - _startingPoint.Y);
                }
                else
                {
                    _rectangle = new Rectangle(
                        e.X <= _startingPoint.X ? e.X : _startingPoint.X,
                        e.Y <= _startingPoint.Y ? e.Y : _startingPoint.Y,
                        _startingPoint.X - e.X <= 0 ? _startingPoint.X - e.X : _startingPoint.X - e.X,
                        _startingPoint.Y - e.Y <= 0 ? e.Y - _startingPoint.Y : _startingPoint.Y - e.Y);
                }
            }
            _isDrawing = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            using (Pen pen = new Pen(Color.Red, 2))
            {
                pen.DashStyle = DashStyle.Dash;
                pe.Graphics.DrawRectangle(pen, _rectangle);
            }
        }

        public void CropImage()
        {
            Bitmap bitmap = new Bitmap(_rectangle.Width, _rectangle.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(this.Image, 0, 0, _rectangle, GraphicsUnit.Pixel);
            }
            Image = bitmap;
            //Removing the rectangle after cropping.
            _rectangle = new Rectangle(0, 0, 0, 0);

        }
    }
}