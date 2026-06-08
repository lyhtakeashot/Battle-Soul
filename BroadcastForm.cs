using System;
using System.Drawing;
using System.Windows.Forms;

namespace Battle_Soul
{
    public class BroadcastForm : Form
    {
        // BroadcastForm
        // 用于在屏幕中央显示滚动文本（如诗句、公告），播放完成自动关闭
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private Label lbl = new Label();
        private int x; // 标签当前横坐标

        // 构造函数：BroadcastForm
        // 参数 text 为要滚动显示的文本内容
        public BroadcastForm(string text)
        {
            // 无边框窗体，位于屏幕中央，使用半透明背景以突出文字
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.Opacity = 0.9;
            this.Size = new Size(800, 120);

            // 标签样式：黄色粗体文本
            lbl.ForeColor = Color.Yellow;
            lbl.Font = new Font("宋体", 12F, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Text = text;
            this.Controls.Add(lbl);

            // 绑定窗体加载事件与计时器回调
            this.Load += BroadcastForm_Load;
            t.Interval = 20; // 每 20ms 更新一次标签位置实现平滑滚动
            t.Tick += T_Tick;
        }

        // BroadcastForm_Load
        // 窗体加载时将标签放在右侧起始位置并启动计时器
        private void BroadcastForm_Load(object? sender, EventArgs e)
        {
            x = this.Width;
            lbl.Location = new Point(x, (this.Height - lbl.Height) / 2);
            t.Start();
        }

        // T_Tick
        // 计时器回调：移动标签；当文字完全滚出左侧时停止计时并关闭窗体
        private void T_Tick(object? sender, EventArgs e)
        {
            x -= 2;
            lbl.Location = new Point(x, lbl.Location.Y);
            if (x + lbl.Width < 0)
            {
                t.Stop();
                this.Close();
            }
        }
    }
}
