using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Battle_Soul
{
    public class BroadcastForm : Form
    {
        // 基于总时长的平滑滚动文字播报
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private Label lbl = new Label();
        private int startX;
        private int totalDistance;
        private readonly int totalMilliseconds;
        private Stopwatch sw = new Stopwatch();

        // 构造函数：text 要显示的文字，durationMs 总播放毫秒（默认 5000ms）
        public BroadcastForm(string text, int durationMs = 5000)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.Opacity = 0.9;
            this.Size = new Size(800, 120);

            lbl.ForeColor = Color.Yellow;
            lbl.Font = new Font("宋体", 12F, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Text = text;
            this.Controls.Add(lbl);

            totalMilliseconds = Math.Max(100, durationMs);

            this.Load += BroadcastForm_Load;
            t.Interval = 20;
            t.Tick += T_Tick;
            // 支持按键跳过（如 空格 / Esc ）
            this.KeyPreview = true;
            this.KeyDown += BroadcastForm_KeyDown;
        }

        private void BroadcastForm_Load(object? sender, EventArgs e)
        {
            startX = this.Width;
            lbl.Location = new Point(startX, (this.Height - lbl.Height) / 2);
            totalDistance = startX + lbl.Width;
            sw.Restart();
            t.Start();
        }

        private void T_Tick(object? sender, EventArgs e)
        {
            long elapsed = sw.ElapsedMilliseconds;
            if (elapsed >= totalMilliseconds)
            {
                t.Stop();
                sw.Stop();
                this.Close();
                return;
            }
            double progress = elapsed / (double)totalMilliseconds;
            int x = startX - (int)Math.Round(totalDistance * progress);
            lbl.Location = new Point(x, lbl.Location.Y);
        }

        // 按键处理：按 空格 或 Esc 跳过播放
        private void BroadcastForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                try
                {
                    t.Stop();
                }
                catch { }
                try { sw.Stop(); } catch { }
                // 直接关闭对话框，调用方（FormMain）会在 finally 中停止音频
                this.Close();
            }
        }
    }
}
