using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectCell_HMI.UserControls
{
    public partial class fenliqi1 : UserControl
    {
        public Timer delayTimer;
        public Dictionary<Control, PointF> controlRelativePositions = new Dictionary<Control, PointF>();

        public fenliqi1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(0xEF, 0xEF, 0xEF);
            this.Resize += new EventHandler(Beng_Resize);
            delayTimer = new Timer();
            delayTimer.Interval = 100;
            delayTimer.Tick += DelayTimer_Tick;
            delayTimer.Start();
        }

        public void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();
            delayTimer.Dispose();

            Beng_Resize(this, EventArgs.Empty);
        }

        public void Beng_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // 获取图片的实际大小
                Size imageSize = pictureBox1.Image.Size;

                // 计算图片在 PictureBox 中的实际显示区域
                Rectangle imageRect = GetImageRectangle(pictureBox1, imageSize);

                // 如果比例未记录，先记录比例
                if (controlRelativePositions.Count == 0)
                {
                    RecordControlRelativePositions(imageRect);
                }

                // 根据记录的比例调整控件位置
                foreach (Control control in this.Controls)
                {
                    if (control != pictureBox1) // 排除 pictureBox1
                    {
                        UpdateControlPosition(control, imageRect);
                    }
                }
            }
        }

        // 记录控件相对于图片的比例
        public void RecordControlRelativePositions(Rectangle imageRect)
        {
            foreach (Control control in this.Controls)
            {
                if (control != pictureBox1) // 排除 pictureBox1
                {
                    controlRelativePositions[control] = GetRelativePosition(control, imageRect);
                }
            }
        }

        // 获取控件相对于图片的比例
        public PointF GetRelativePosition(Control control, Rectangle imageRect)
        {
            float relativeX = (float)(control.Left - imageRect.Left) / imageRect.Width;
            float relativeY = (float)(control.Top - imageRect.Top) / imageRect.Height;
            return new PointF(relativeX, relativeY);
        }

        // 根据记录的比例调整控件位置
        public void UpdateControlPosition(Control control, Rectangle imageRect)
        {
            if (controlRelativePositions.TryGetValue(control, out PointF relativePosition))
            {
                control.Left = imageRect.Left + (int)(relativePosition.X * imageRect.Width);
                control.Top = imageRect.Top + (int)(relativePosition.Y * imageRect.Height);
            }
        }

        // 计算图片在 PictureBox 中的实际显示区域
        public Rectangle GetImageRectangle(PictureBox pictureBox, Size imageSize)
        {
            Rectangle rect = new Rectangle();

            if (pictureBox.SizeMode == PictureBoxSizeMode.Normal || pictureBox.SizeMode == PictureBoxSizeMode.AutoSize)
            {
                // 图片按原始大小显示
                rect.Size = imageSize;
                rect.Location = pictureBox.Location;
            }
            else if (pictureBox.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                // 图片拉伸填充整个 PictureBox
                rect.Size = pictureBox.Size;
                rect.Location = pictureBox.Location;
            }
            else if (pictureBox.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                // 图片居中显示
                rect.Size = imageSize;
                rect.Location = new Point(
                    pictureBox.Left + (pictureBox.Width - imageSize.Width) / 2,
                    pictureBox.Top + (pictureBox.Height - imageSize.Height) / 2
                );
            }
            else if (pictureBox.SizeMode == PictureBoxSizeMode.Zoom)
            {
                // 图片按比例缩放显示
                float ratioX = (float)pictureBox.Width / imageSize.Width;
                float ratioY = (float)pictureBox.Height / imageSize.Height;
                float ratio = Math.Min(ratioX, ratioY);

                rect.Size = new Size((int)(imageSize.Width * ratio), (int)(imageSize.Height * ratio));
                rect.Location = new Point(
                    pictureBox.Left + (pictureBox.Width - rect.Width) / 2,
                    pictureBox.Top + (pictureBox.Height - rect.Height) / 2
                );
            }

            return rect;
        }
    }
}
