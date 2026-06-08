using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Battle_Soul
{
    /// Desktop 类（桌面背景）
    public class Desktop
    {
        /// 存放加载后的位图对象。
        private Bitmap _desktopBitmap;
        /// 构造器：通过文件路径加载位图。
        /// 注意：此处不做异常捕获或路径转换，调用方需保证路径正确。
        /// <param name="bitmapFile">位图文件路径，例如 "Image\\背景图.png"</param>
        public Desktop(string bitmapFile)
        {
            // 直接使用 Bitmap 构造函数加载图片文件
            _desktopBitmap = new Bitmap(bitmapFile);
        }
        public void Draw(Graphics g)
        {
            // 若未加载图片或 Graphics 为 null，直接返回，不抛异常（简洁处理）
            if (_desktopBitmap == null || g == null) return;

            // 使用较高质量的插值方式以获得更好的缩放效果
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 用 Graphics 的可见裁剪区作为目标绘制范围（防止越界）
            var clip = g.VisibleClipBounds;
            var dest = Rectangle.Round(clip);
            if (dest.Width <= 0 || dest.Height <= 0) return;

            // 计算等比适配后的目标矩形（居中）
            var fit = GetAspectFitRect(dest, _desktopBitmap.Size);

            // 绘制图片到计算得到的矩形区域
            g.DrawImage(_desktopBitmap, fit);
        }
        /// 计算等比适配（Aspect Fit）后的矩形：
        /// 给定外部矩形 outer（目标区域）和内部图片尺寸 inner（原图宽高），
        /// 返回一个在 outer 内部并保持 inner 长宽比的矩形，且在 outer 中居中对齐。
        /// - 先比较内外长宽比，决定以宽或高为约束进行缩放。
        /// - 计算缩放后的宽度和高度，然后计算居中时的偏移量（x,y）。
        private Rectangle GetAspectFitRect(Rectangle outer, Size inner)
        {
            // 若源尺寸异常，则直接返回 outer
            if (inner.Width == 0 || inner.Height == 0) return outer;

            // 计算内外的宽高比
            float ratioInner = (float)inner.Width / inner.Height;
            float ratioOuter = (float)outer.Width / outer.Height;

            int w, h;
            // 如果源图片更“宽”（宽高比更大），以外部宽度为约束，计算高度
            if (ratioInner > ratioOuter)
            {
                w = outer.Width;
                h = (int)(outer.Width / ratioInner);
            }
            else
            {
                // 否则以外部高度为约束，计算宽度
                h = outer.Height;
                w = (int)(outer.Height * ratioInner);
            }

            // 计算居中后的左上角坐标
            int x = outer.X + (outer.Width - w) / 2;
            int y = outer.Y + (outer.Height - h) / 2;

            return new Rectangle(x, y, w, h);
        }
    }
}
