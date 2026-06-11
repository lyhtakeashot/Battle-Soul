namespace Battle_Soul
{
    partial class BattleSoul
    {
        // 设计器使用的组件容器
        private System.ComponentModel.IContainer components = null;

        // Dispose
        // 释放资源（由窗体在关闭时调用）
        // 参数 disposing: 为 true 时释放托管资源和非托管资源
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 窗体设计器生成的代码

        // InitializeComponent
        // 窗体控件初始化方法（由 Designer 生成），请勿随意修改控件声明顺序
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleSoul));
            labelplayerName = new Label();
            labelplayerHp = new Label();
            labelplayerShield = new Label();
            labelplayerEnergy = new Label();
            labelplayerStack = new Label();
            labelplayerDot = new Label();
            labelenemyName = new Label();
            labelenemyHp = new Label();
            labelenemyShied = new Label();
            labelenemyEnergy = new Label();
            labelenemyStack = new Label();
            labelenemyDot = new Label();
            picPlayer = new PictureBox();
            picEnemy = new PictureBox();
            logTitle = new Label();
            logTextBox = new TextBox();
            btnEndTurn = new Button();
            handPanel = new FlowLayoutPanel();
            lblDiscardTitle = new Label();
            lblDiscardCount = new Label();
            picDiscard = new PictureBox();
            lblDrawTitle = new Label();
            lblDrawCount = new Label();
            picDraw = new PictureBox();
            btnPause = new Button();
            pictureBoxDesktop = new PictureBox();
            axWindowsMediaPlayerBGM = new AxWMPLib.AxWindowsMediaPlayer();
            axWindowsMediaPlayereasteregg = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)picPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEnemy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDiscard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDraw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDesktop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayerBGM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayereasteregg).BeginInit();
            SuspendLayout();
            // 
            // labelplayerName
            // 
            labelplayerName.AutoSize = true;
            labelplayerName.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelplayerName.Location = new Point(20, 20);
            labelplayerName.Margin = new Padding(5, 0, 5, 0);
            labelplayerName.Name = "labelplayerName";
            labelplayerName.Size = new Size(60, 24);
            labelplayerName.TabIndex = 0;
            labelplayerName.Text = "玩家";
            labelplayerName.Click += playerNameLabel_Click;
            // 
            // labelplayerHp
            // 
            labelplayerHp.AutoSize = true;
            labelplayerHp.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerHp.ForeColor = Color.Red;
            labelplayerHp.Location = new Point(20, 50);
            labelplayerHp.Margin = new Padding(5, 0, 5, 0);
            labelplayerHp.Name = "labelplayerHp";
            labelplayerHp.Size = new Size(159, 20);
            labelplayerHp.TabIndex = 1;
            labelplayerHp.Text = "生命值：100/100";
            // 
            // labelplayerShield
            // 
            labelplayerShield.AutoSize = true;
            labelplayerShield.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerShield.ForeColor = Color.Gray;
            labelplayerShield.Location = new Point(20, 75);
            labelplayerShield.Margin = new Padding(5, 0, 5, 0);
            labelplayerShield.Name = "labelplayerShield";
            labelplayerShield.Size = new Size(99, 20);
            labelplayerShield.TabIndex = 1;
            labelplayerShield.Text = "护盾值：0";
            // 
            // labelplayerEnergy
            // 
            labelplayerEnergy.AutoSize = true;
            labelplayerEnergy.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerEnergy.ForeColor = Color.Blue;
            labelplayerEnergy.Location = new Point(20, 100);
            labelplayerEnergy.Margin = new Padding(5, 0, 5, 0);
            labelplayerEnergy.Name = "labelplayerEnergy";
            labelplayerEnergy.Size = new Size(119, 20);
            labelplayerEnergy.TabIndex = 1;
            labelplayerEnergy.Text = "当前充能：4";
            // 
            // labelplayerStack
            // 
            labelplayerStack.AutoSize = true;
            labelplayerStack.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerStack.Location = new Point(20, 125);
            labelplayerStack.Margin = new Padding(5, 0, 5, 0);
            labelplayerStack.Name = "labelplayerStack";
            labelplayerStack.Size = new Size(119, 20);
            labelplayerStack.TabIndex = 1;
            labelplayerStack.Text = "叠层次数：0";
            // 
            // labelplayerDot
            // 
            labelplayerDot.AutoSize = true;
            labelplayerDot.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerDot.ForeColor = Color.FromArgb(0, 192, 0);
            labelplayerDot.Location = new Point(20, 150);
            labelplayerDot.Margin = new Padding(5, 0, 5, 0);
            labelplayerDot.Name = "labelplayerDot";
            labelplayerDot.Size = new Size(109, 20);
            labelplayerDot.TabIndex = 1;
            labelplayerDot.Text = "Dot层数：0";
            // 
            // labelenemyName
            // 
            labelenemyName.AutoSize = true;
            labelenemyName.BackColor = Color.Transparent;
            labelenemyName.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelenemyName.Location = new Point(600, 20);
            labelenemyName.Margin = new Padding(5, 0, 5, 0);
            labelenemyName.Name = "labelenemyName";
            labelenemyName.Size = new Size(60, 24);
            labelenemyName.TabIndex = 0;
            labelenemyName.Text = "敌人";
            labelenemyName.Click += playerNameLabel_Click;
            // 
            // labelenemyHp
            // 
            labelenemyHp.AutoSize = true;
            labelenemyHp.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyHp.ForeColor = Color.Red;
            labelenemyHp.Location = new Point(600, 50);
            labelenemyHp.Margin = new Padding(5, 0, 5, 0);
            labelenemyHp.Name = "labelenemyHp";
            labelenemyHp.Size = new Size(159, 20);
            labelenemyHp.TabIndex = 1;
            labelenemyHp.Text = "生命值：100/100";
            // 
            // labelenemyShied
            // 
            labelenemyShied.AutoSize = true;
            labelenemyShied.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyShied.ForeColor = Color.Gray;
            labelenemyShied.Location = new Point(600, 78);
            labelenemyShied.Margin = new Padding(5, 0, 5, 0);
            labelenemyShied.Name = "labelenemyShied";
            labelenemyShied.Size = new Size(99, 20);
            labelenemyShied.TabIndex = 1;
            labelenemyShied.Text = "护盾值：0";
            // 
            // labelenemyEnergy
            // 
            labelenemyEnergy.AutoSize = true;
            labelenemyEnergy.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyEnergy.ForeColor = Color.Blue;
            labelenemyEnergy.Location = new Point(600, 100);
            labelenemyEnergy.Margin = new Padding(5, 0, 5, 0);
            labelenemyEnergy.Name = "labelenemyEnergy";
            labelenemyEnergy.Size = new Size(119, 20);
            labelenemyEnergy.TabIndex = 1;
            labelenemyEnergy.Text = "当前充能：4";
            // 
            // labelenemyStack
            // 
            labelenemyStack.AutoSize = true;
            labelenemyStack.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyStack.Location = new Point(600, 125);
            labelenemyStack.Margin = new Padding(5, 0, 5, 0);
            labelenemyStack.Name = "labelenemyStack";
            labelenemyStack.Size = new Size(119, 20);
            labelenemyStack.TabIndex = 1;
            labelenemyStack.Text = "叠层次数：0";
            // 
            // labelenemyDot
            // 
            labelenemyDot.AutoSize = true;
            labelenemyDot.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyDot.ForeColor = Color.FromArgb(0, 192, 0);
            labelenemyDot.Location = new Point(600, 150);
            labelenemyDot.Margin = new Padding(5, 0, 5, 0);
            labelenemyDot.Name = "labelenemyDot";
            labelenemyDot.Size = new Size(109, 20);
            labelenemyDot.TabIndex = 1;
            labelenemyDot.Text = "Dot层数：0";
            // 
            // picPlayer
            // 
            picPlayer.BorderStyle = BorderStyle.FixedSingle;
            picPlayer.Location = new Point(20, 180);
            picPlayer.Name = "picPlayer";
            picPlayer.Size = new Size(140, 140);
            picPlayer.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayer.TabIndex = 2;
            picPlayer.TabStop = false;
            // 
            // picEnemy
            // 
            picEnemy.BorderStyle = BorderStyle.FixedSingle;
            picEnemy.Location = new Point(600, 180);
            picEnemy.Name = "picEnemy";
            picEnemy.Size = new Size(140, 140);
            picEnemy.SizeMode = PictureBoxSizeMode.Zoom;
            picEnemy.TabIndex = 2;
            picEnemy.TabStop = false;
            // 
            // logTitle
            // 
            logTitle.AutoSize = true;
            logTitle.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            logTitle.Location = new Point(220, 20);
            logTitle.Margin = new Padding(5, 0, 5, 0);
            logTitle.Name = "logTitle";
            logTitle.Size = new Size(110, 24);
            logTitle.TabIndex = 2;
            logTitle.Text = "战斗日志";
            // 
            // logTextBox
            // 
            logTextBox.BorderStyle = BorderStyle.FixedSingle;
            logTextBox.Font = new Font("宋体", 8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            logTextBox.Location = new Point(220, 50);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(360, 260);
            logTextBox.TabIndex = 3;
            logTextBox.Text = "战斗即将开始";
            logTextBox.WordWrap = false;
            logTextBox.TextChanged += logTextBox_TextChanged;
            // 
            // btnEndTurn
            // 
            btnEndTurn.BackColor = Color.DarkGreen;
            btnEndTurn.Font = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEndTurn.ForeColor = Color.White;
            btnEndTurn.Location = new Point(220, 513);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(110, 31);
            btnEndTurn.TabIndex = 4;
            btnEndTurn.Text = "结束回合";
            btnEndTurn.UseVisualStyleBackColor = false;
            // 
            // handPanel
            // 
            handPanel.AutoScroll = true;
            handPanel.Location = new Point(220, 380);
            handPanel.Name = "handPanel";
            handPanel.Size = new Size(360, 60);
            handPanel.TabIndex = 5;
            // 
            // lblDiscardTitle
            // 
            lblDiscardTitle.AutoSize = true;
            lblDiscardTitle.Location = new Point(20, 416);
            lblDiscardTitle.Name = "lblDiscardTitle";
            lblDiscardTitle.Size = new Size(64, 24);
            lblDiscardTitle.TabIndex = 5;
            lblDiscardTitle.Text = "弃牌堆";
            // 
            // lblDiscardCount
            // 
            lblDiscardCount.AutoSize = true;
            lblDiscardCount.Location = new Point(20, 520);
            lblDiscardCount.Name = "lblDiscardCount";
            lblDiscardCount.Size = new Size(21, 24);
            lblDiscardCount.TabIndex = 7;
            lblDiscardCount.Text = "0";
            // 
            // picDiscard
            // 
            picDiscard.BorderStyle = BorderStyle.FixedSingle;
            picDiscard.Location = new Point(19, 443);
            picDiscard.Name = "picDiscard";
            picDiscard.Size = new Size(110, 70);
            picDiscard.SizeMode = PictureBoxSizeMode.CenterImage;
            picDiscard.TabIndex = 6;
            picDiscard.TabStop = false;
            picDiscard.Click += PicDiscard_Click;
            // 
            // lblDrawTitle
            // 
            lblDrawTitle.AutoSize = true;
            lblDrawTitle.Location = new Point(660, 416);
            lblDrawTitle.Name = "lblDrawTitle";
            lblDrawTitle.Size = new Size(64, 24);
            lblDrawTitle.TabIndex = 8;
            lblDrawTitle.Text = "抽牌堆";
            // 
            // lblDrawCount
            // 
            lblDrawCount.AutoSize = true;
            lblDrawCount.Location = new Point(660, 525);
            lblDrawCount.Name = "lblDrawCount";
            lblDrawCount.Size = new Size(21, 24);
            lblDrawCount.TabIndex = 9;
            lblDrawCount.Text = "0";
            // 
            // picDraw
            // 
            picDraw.BorderStyle = BorderStyle.FixedSingle;
            picDraw.Location = new Point(660, 452);
            picDraw.Name = "picDraw";
            picDraw.Size = new Size(110, 70);
            picDraw.SizeMode = PictureBoxSizeMode.CenterImage;
            picDraw.TabIndex = 6;
            picDraw.TabStop = false;
            picDraw.Click += PicDraw_Click;
            // 
            // btnPause
            // 
            btnPause.BackColor = Color.DarkRed;
            btnPause.Font = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnPause.ForeColor = Color.White;
            btnPause.Location = new Point(340, 513);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(110, 31);
            btnPause.TabIndex = 4;
            btnPause.Text = "暂停";
            btnPause.UseVisualStyleBackColor = false;
            // 
            // pictureBoxDesktop
            // 
            pictureBoxDesktop.Dock = DockStyle.Fill;
            pictureBoxDesktop.Location = new Point(0, 0);
            pictureBoxDesktop.Name = "pictureBoxDesktop";
            pictureBoxDesktop.Size = new Size(778, 544);
            pictureBoxDesktop.TabIndex = 10;
            pictureBoxDesktop.TabStop = false;
            pictureBoxDesktop.Paint += pictureBoxDesktop_Paint;
            // 
            // axWindowsMediaPlayerBGM
            // 
            axWindowsMediaPlayerBGM.Enabled = true;
            axWindowsMediaPlayerBGM.Location = new Point(504, 7);
            axWindowsMediaPlayerBGM.Name = "axWindowsMediaPlayerBGM";
            axWindowsMediaPlayerBGM.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayerBGM.OcxState");
            axWindowsMediaPlayerBGM.Size = new Size(75, 23);
            axWindowsMediaPlayerBGM.TabIndex = 11;
            // 
            // axWindowsMediaPlayereasteregg
            // 
            axWindowsMediaPlayereasteregg.Enabled = true;
            axWindowsMediaPlayereasteregg.Location = new Point(504, 21);
            axWindowsMediaPlayereasteregg.Name = "axWindowsMediaPlayereasteregg";
            axWindowsMediaPlayereasteregg.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayereasteregg.OcxState");
            axWindowsMediaPlayereasteregg.Size = new Size(75, 23);
            axWindowsMediaPlayereasteregg.TabIndex = 11;
            // 
            // BattleSoul
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 544);
            Controls.Add(axWindowsMediaPlayereasteregg);
            Controls.Add(axWindowsMediaPlayerBGM);
            Controls.Add(btnEndTurn);
            Controls.Add(btnPause);
            Controls.Add(picEnemy);
            Controls.Add(picPlayer);
            Controls.Add(lblDiscardTitle);
            Controls.Add(picDiscard);
            Controls.Add(lblDiscardCount);
            Controls.Add(lblDrawTitle);
            Controls.Add(picDraw);
            Controls.Add(lblDrawCount);
            Controls.Add(handPanel);
            Controls.Add(logTextBox);
            Controls.Add(logTitle);
            Controls.Add(labelenemyDot);
            Controls.Add(labelplayerDot);
            Controls.Add(labelenemyStack);
            Controls.Add(labelplayerStack);
            Controls.Add(labelenemyEnergy);
            Controls.Add(labelplayerEnergy);
            Controls.Add(labelenemyShied);
            Controls.Add(labelplayerShield);
            Controls.Add(labelenemyHp);
            Controls.Add(labelplayerHp);
            Controls.Add(labelenemyName);
            Controls.Add(labelplayerName);
            Controls.Add(pictureBoxDesktop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            MaximumSize = new Size(800, 600);
            MinimumSize = new Size(800, 600);
            Name = "BattleSoul";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Battle Soul 回合制战斗";
            Load += BattleSoul_Load;
            ((System.ComponentModel.ISupportInitialize)picPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEnemy).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDiscard).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDraw).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDesktop).EndInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayerBGM).EndInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayereasteregg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelplayerName;
        private Label labelplayerHp;
        private Label labelplayerShield;
        private Label labelplayerEnergy;
        private Label labelplayerStack;
        private Label labelplayerDot;
        private Label labelenemyName;
        private Label labelenemyHp;
        private Label labelenemyShied;
        private Label labelenemyEnergy;
        private Label labelenemyStack;
        private Label labelenemyDot;
        private Label logTitle;
        private TextBox logTextBox;
            // 技能按钮已移除，改为卡牌手牌展示
        private Button btnEndTurn;
        private Button btnPause;
        private PictureBox picPlayer;
        private PictureBox picEnemy;
        private FlowLayoutPanel handPanel;
        private Label lblDiscardTitle;
        private Label lblDiscardCount;
        private PictureBox picDiscard;
        private Label lblDrawTitle;
        private Label lblDrawCount;
        private PictureBox picDraw;
        private PictureBox pictureBoxDesktop;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayerBGM;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayereasteregg;
    }
}
