using System;
using System.Drawing;
using System.Windows.Forms;

namespace Battle_Soul
{
    public class PauseDialog : Form
    {
        // 暂停对话框包含三个按钮：暂停、重新开始、退出
        // BtnPause: 触发暂停游戏（将主逻辑置为暂停状态）
        // BtnRestart: 重新开始当前战斗（会重置游戏状态并关闭对话框）
        // BtnExit: 退出当前战斗并关闭主窗口（返回主界面或结束程序）
        public Button BtnPause { get; private set; }
        public Button BtnRestart { get; private set; }
        public Button BtnExit { get; private set; }
        public CheckBox ChkEndless { get; private set; }

        // 构造函数：初始化对话框并创建控件
        public PauseDialog()
        {
            InitializeComponent();
        }

        // InitializeComponent
        // 初始化对话框的控件和布局
        private void InitializeComponent()
        {
            // 对话框样式为固定对话框，居中显示，禁止最大化最小化
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(360, 120);
            this.Text = "暂停";
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 创建三个按钮实例并设置文字
            BtnPause = new Button();
            BtnRestart = new Button();
            BtnExit = new Button();

            BtnPause.Text = "暂停";
            BtnRestart.Text = "重新开始";
            BtnExit.Text = "退出";

            ChkEndless = new CheckBox();
            ChkEndless.Text = "无尽模式";
            ChkEndless.AutoSize = true;
            // place checkbox at top-left area
            ChkEndless.Location = new Point(12, 12);

            // 统一按钮尺寸
            BtnPause.Size = new Size(100, 36);
            BtnRestart.Size = new Size(100, 36);
            BtnExit.Size = new Size(100, 36);

            // 将三个按钮在对话框中水平均匀分布
            int spacing = (this.ClientSize.Width - (BtnPause.Width * 3)) / 4;
            BtnPause.Location = new Point(spacing, (this.ClientSize.Height - BtnPause.Height) / 2);
            BtnRestart.Location = new Point(spacing * 2 + BtnPause.Width, (this.ClientSize.Height - BtnRestart.Height) / 2);
            BtnExit.Location = new Point(spacing * 3 + BtnPause.Width * 2, (this.ClientSize.Height - BtnExit.Height) / 2);

            // 将按钮加入对话框控件集合
            this.Controls.Add(ChkEndless);
            this.Controls.Add(BtnPause);
            this.Controls.Add(BtnRestart);
            this.Controls.Add(BtnExit);
        }
    }
}
