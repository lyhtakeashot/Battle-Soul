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
            ((System.ComponentModel.ISupportInitialize)picPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEnemy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDiscard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDraw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDesktop).BeginInit();
            SuspendLayout();
            // 控件: labelplayerName（玩家名称标签）
            labelplayerName.AutoSize = true;
            labelplayerName.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelplayerName.Location = new Point(20, 20);
            labelplayerName.Margin = new Padding(5, 0, 5, 0);
            labelplayerName.Name = "labelplayerName";
            labelplayerName.Size = new Size(60, 24);
            labelplayerName.TabIndex = 0;
            labelplayerName.Text = "玩家";
            labelplayerName.Click += playerNameLabel_Click;
            // 控件: labelplayerHp（玩家生命标签）
            labelplayerHp.AutoSize = true;
            labelplayerHp.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerHp.ForeColor = Color.Red;
            labelplayerHp.Location = new Point(20, 50);
            labelplayerHp.Margin = new Padding(5, 0, 5, 0);
            labelplayerHp.Name = "labelplayerHp";
            labelplayerHp.Size = new Size(159, 20);
            labelplayerHp.TabIndex = 1;
            labelplayerHp.Text = "生命值：100/100";
            // 控件: labelplayerShield（玩家护盾标签）
            labelplayerShield.AutoSize = true;
            labelplayerShield.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerShield.ForeColor = Color.Gray;
            labelplayerShield.Location = new Point(20, 75);
            labelplayerShield.Margin = new Padding(5, 0, 5, 0);
            labelplayerShield.Name = "labelplayerShield";
            labelplayerShield.Size = new Size(99, 20);
            labelplayerShield.TabIndex = 1;
            labelplayerShield.Text = "护盾值：0";
            // 控件: labelplayerEnergy（玩家充能标签）
            labelplayerEnergy.AutoSize = true;
            labelplayerEnergy.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerEnergy.ForeColor = Color.Blue;
            labelplayerEnergy.Location = new Point(20, 100);
            labelplayerEnergy.Margin = new Padding(5, 0, 5, 0);
            labelplayerEnergy.Name = "labelplayerEnergy";
            labelplayerEnergy.Size = new Size(119, 20);
            labelplayerEnergy.TabIndex = 1;
            labelplayerEnergy.Text = "当前充能：4";
            // 控件: labelplayerStack（玩家叠层次数标签）
            labelplayerStack.AutoSize = true;
            labelplayerStack.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerStack.Location = new Point(20, 125);
            labelplayerStack.Margin = new Padding(5, 0, 5, 0);
            labelplayerStack.Name = "labelplayerStack";
            labelplayerStack.Size = new Size(119, 20);
            labelplayerStack.TabIndex = 1;
            labelplayerStack.Text = "叠层次数：0";
            // 控件: labelplayerDot（玩家 DOT 层数标签）
            labelplayerDot.AutoSize = true;
            labelplayerDot.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelplayerDot.ForeColor = Color.FromArgb(0, 192, 0);
            labelplayerDot.Location = new Point(20, 150);
            labelplayerDot.Margin = new Padding(5, 0, 5, 0);
            labelplayerDot.Name = "labelplayerDot";
            labelplayerDot.Size = new Size(109, 20);
            labelplayerDot.TabIndex = 1;
            labelplayerDot.Text = "Dot层数：0";
            // 控件: labelenemyName（敌人名称标签）
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
            // 控件: labelenemyHp（敌人生命标签）
            labelenemyHp.AutoSize = true;
            labelenemyHp.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyHp.ForeColor = Color.Red;
            labelenemyHp.Location = new Point(600, 50);
            labelenemyHp.Margin = new Padding(5, 0, 5, 0);
            labelenemyHp.Name = "labelenemyHp";
            labelenemyHp.Size = new Size(159, 20);
            labelenemyHp.TabIndex = 1;
            labelenemyHp.Text = "生命值：100/100";
            // 控件: labelenemyShied（敌人护盾标签）
            labelenemyShied.AutoSize = true;
            labelenemyShied.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyShied.ForeColor = Color.Gray;
            labelenemyShied.Location = new Point(600, 78);
            labelenemyShied.Margin = new Padding(5, 0, 5, 0);
            labelenemyShied.Name = "labelenemyShied";
            labelenemyShied.Size = new Size(99, 20);
            labelenemyShied.TabIndex = 1;
            labelenemyShied.Text = "护盾值：0";
            // 控件: labelenemyEnergy（敌人充能标签）
            labelenemyEnergy.AutoSize = true;
            labelenemyEnergy.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyEnergy.ForeColor = Color.Blue;
            labelenemyEnergy.Location = new Point(600, 100);
            labelenemyEnergy.Margin = new Padding(5, 0, 5, 0);
            labelenemyEnergy.Name = "labelenemyEnergy";
            labelenemyEnergy.Size = new Size(119, 20);
            labelenemyEnergy.TabIndex = 1;
            labelenemyEnergy.Text = "当前充能：4";
            // 控件: labelenemyStack（敌人叠层次数标签）
            labelenemyStack.AutoSize = true;
            labelenemyStack.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyStack.Location = new Point(600, 125);
            labelenemyStack.Margin = new Padding(5, 0, 5, 0);
            labelenemyStack.Name = "labelenemyStack";
            labelenemyStack.Size = new Size(119, 20);
            labelenemyStack.TabIndex = 1;
            labelenemyStack.Text = "叠层次数：0";
            // 控件: labelenemyDot（敌人 DOT 层数标签）
            labelenemyDot.AutoSize = true;
            labelenemyDot.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelenemyDot.ForeColor = Color.FromArgb(0, 192, 0);
            labelenemyDot.Location = new Point(600, 150);
            labelenemyDot.Margin = new Padding(5, 0, 5, 0);
            labelenemyDot.Name = "labelenemyDot";
            labelenemyDot.Size = new Size(109, 20);
            labelenemyDot.TabIndex = 1;
            labelenemyDot.Text = "Dot层数：0";
            // 控件: picPlayer（玩家头像图片框）
            picPlayer.BorderStyle = BorderStyle.FixedSingle;
            picPlayer.Location = new Point(20, 180);
            picPlayer.Name = "picPlayer";
            picPlayer.Size = new Size(140, 140);
            picPlayer.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayer.TabIndex = 2;
            picPlayer.TabStop = false;
            // 控件: picEnemy（敌人头像图片框）
            picEnemy.BorderStyle = BorderStyle.FixedSingle;
            picEnemy.Location = new Point(600, 180);
            picEnemy.Name = "picEnemy";
            picEnemy.Size = new Size(140, 140);
            picEnemy.SizeMode = PictureBoxSizeMode.Zoom;
            picEnemy.TabIndex = 2;
            picEnemy.TabStop = false;
            // 控件: logTitle（日志标题）
            logTitle.AutoSize = true;
            logTitle.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            logTitle.Location = new Point(220, 20);
            logTitle.Margin = new Padding(5, 0, 5, 0);
            logTitle.Name = "logTitle";
            logTitle.Size = new Size(110, 24);
            logTitle.TabIndex = 2;
            logTitle.Text = "战斗日志";
            // 控件: logTextBox（战斗日志文本框）
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
            // 控件: btnEndTurn（结束回合按钮）
            btnEndTurn.BackColor = Color.DarkGreen;
            btnEndTurn.Font = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEndTurn.ForeColor = Color.White;
            btnEndTurn.Location = new Point(220, 513);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(110, 31);
            btnEndTurn.TabIndex = 4;
            btnEndTurn.Text = "结束回合";
            btnEndTurn.UseVisualStyleBackColor = false;
            // 控件: handPanel（手牌面板，显示玩家手牌按钮）
            handPanel.AutoScroll = true;
            handPanel.Location = new Point(220, 380);
            handPanel.Name = "handPanel";
            handPanel.Size = new Size(360, 60);
            handPanel.TabIndex = 5;
            // 控件: lblDiscardTitle（弃牌堆标题）
            lblDiscardTitle.AutoSize = true;
            lblDiscardTitle.Location = new Point(20, 416);
            lblDiscardTitle.Name = "lblDiscardTitle";
            lblDiscardTitle.Size = new Size(64, 24);
            lblDiscardTitle.TabIndex = 5;
            lblDiscardTitle.Text = "弃牌堆";
            // 控件: lblDiscardCount（弃牌堆计数标签）
            lblDiscardCount.AutoSize = true;
            lblDiscardCount.Location = new Point(20, 520);
            lblDiscardCount.Name = "lblDiscardCount";
            lblDiscardCount.Size = new Size(21, 24);
            lblDiscardCount.TabIndex = 7;
            lblDiscardCount.Text = "0";
            // 控件: picDiscard（弃牌堆图片框，点击查看）
            picDiscard.BorderStyle = BorderStyle.FixedSingle;
            picDiscard.Location = new Point(19, 443);
            picDiscard.Name = "picDiscard";
            picDiscard.Size = new Size(110, 70);
            picDiscard.SizeMode = PictureBoxSizeMode.CenterImage;
            picDiscard.TabIndex = 6;
            picDiscard.TabStop = false;
            picDiscard.Click += PicDiscard_Click;
            // 控件: lblDrawTitle（抽牌堆标题）
            lblDrawTitle.AutoSize = true;
            lblDrawTitle.Location = new Point(660, 416);
            lblDrawTitle.Name = "lblDrawTitle";
            lblDrawTitle.Size = new Size(64, 24);
            lblDrawTitle.TabIndex = 8;
            lblDrawTitle.Text = "抽牌堆";
            // 控件: lblDrawCount（抽牌堆计数标签）
            lblDrawCount.AutoSize = true;
            lblDrawCount.Location = new Point(660, 525);
            lblDrawCount.Name = "lblDrawCount";
            lblDrawCount.Size = new Size(21, 24);
            lblDrawCount.TabIndex = 9;
            lblDrawCount.Text = "0";
            // 控件: picDraw（抽牌堆图片框，点击查看）
            picDraw.BorderStyle = BorderStyle.FixedSingle;
            picDraw.Location = new Point(660, 452);
            picDraw.Name = "picDraw";
            picDraw.Size = new Size(110, 70);
            picDraw.SizeMode = PictureBoxSizeMode.CenterImage;
            picDraw.TabIndex = 6;
            picDraw.TabStop = false;
            picDraw.Click += PicDraw_Click;
            // 控件: btnPause（暂停按钮）
            btnPause.BackColor = Color.DarkRed;
            btnPause.Font = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnPause.ForeColor = Color.White;
            btnPause.Location = new Point(340, 513);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(110, 31);
            btnPause.TabIndex = 4;
            btnPause.Text = "暂停";
            btnPause.UseVisualStyleBackColor = false;
            // 控件: pictureBoxDesktop（背景图片容器）
            pictureBoxDesktop.Dock = DockStyle.Fill;
            pictureBoxDesktop.Location = new Point(0, 0);
            pictureBoxDesktop.Name = "pictureBoxDesktop";
            pictureBoxDesktop.Size = new Size(778, 544);
            pictureBoxDesktop.TabIndex = 10;
            pictureBoxDesktop.TabStop = false;
            pictureBoxDesktop.Paint += pictureBoxDesktop_Paint;
            // 窗体: BattleSoul（主窗体属性设置）
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 544);
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
    }
}
