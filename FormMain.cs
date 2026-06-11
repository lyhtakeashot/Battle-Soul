namespace Battle_Soul
{
    // 主游戏窗体：包含游戏状态、卡牌系统、回合流程、敌方 AI、UI 刷新等逻辑
    // 文件内主要功能模块（中文注释）：
    // - 玩家/敌人属性定义与初始化
    // - 抽牌堆/手牌/弃牌堆管理
    // - 各类卡牌效果实现（DoXxx 方法）
    // - 回合控制（玩家出牌、结束回合、敌人行动、回合结算）
    // - UI 渲染与全屏、暂停等交互支持
    public partial class BattleSoul : Form
    {
        #region 私有字段
        //桌面类对象
        private Desktop _desktop;
        // 卡牌提示
        private ToolTip cardToolTip;
        #endregion
        //===================== 玩家角色基础属性 =======================
        //血量
        private int playerMaxHp;//玩家最大生命值
        private int playerCurHp;//玩家当前生命值
        //攻击力
        private int playerAtk;//玩家攻击力
        //护盾
        private int playerShied;//玩家当前护盾值
        //充能
        private int playerEnergy;//玩家当前充能点数
        //叠层攻击（五指拳心剑）
        private int playerStack;//玩家叠层次数（伤害6*3^（n-1）递增）
        //DOT持续伤害
        private int playerDotCount;//玩家DOT层数
        private int playerDotRound;//玩家DOT剩余持续回合
        // 标记：本回合刚被施加的 DOT（用于避免在施加当回合立即结算）
        private bool playerDotJustApplied = false; // 敌人对玩家施加的 DOT（player 受到的 DOT）
        private bool enemyDotJustApplied = false;  // 玩家对敌人施加的 DOT（enemy 受到的 DOT）
        //临时buff状态（仅限当前回合有效）
        private bool playerIsBlock;//玩家是否格挡姿势
        private bool playerIsCounter;//玩家是否反击姿势
        // ===================== 敌方AI角色基础属性 =====================
        private int enemyMaxHp;
        private int enemyCurHp;
        private int enemyAtk;
        private int enemyDef;
        private int enemyShield;
        private int enemyEnergy;
        private int enemyStack;
        private int enemyDotCount;
        private int enemyDotRound;
        private bool enemyIsBlock;
        private bool enemyIsCounter;
        //===================== 全局变量 =======================
        private bool isplayerTurn;//是否玩家回合
        private bool isGameOver;//是否游戏结束
        private bool isEndlessMode = false; // 无尽模式标志
        private bool playerFirst;//随机先手结果
        private Random rand = new Random();
        private bool isPaused;
        // 玩家在本局对敌人造成的总伤害
        private int playerTotalDamageDealt = 0;
        // 无尽模式下的溢出伤害，会计入敌人的下一条命
        private int enemyOverflowDamage = 0;
        // 每层DOT每回合伤害（改为每层 4 点）
        private const int DotPerLayer = 4;
        // 基础盾值
        private const int ShieldValue = 15;
        // 可配置的角色图片路径（普通模式 / 无尽模式）
        private string playerImageNormalPath = null;
        private string playerImageEndlessPath = null;
        private string enemyImageNormalPath = null;
        private string enemyImageEndlessPath = null;
        // 构造函数: BattleSoul
        public BattleSoul()
        {
            InitializeComponent();
            _desktop = new Desktop("Image\\背景图.png");
            // 清除 PictureBox.Image 避免与自绘冲突，绑定 Paint 并置底重绘
            pictureBoxDesktop.Image = null;
            pictureBoxDesktop.Paint -= pictureBoxDesktop_Paint;
            pictureBoxDesktop.Paint += pictureBoxDesktop_Paint;
            pictureBoxDesktop.SendToBack();
            pictureBoxDesktop.Invalidate();
            // 调整日志字体大小以提升可读性
            logTextBox.Font = new Font(logTextBox.Font.FontFamily, 8F, logTextBox.Font.Style);
            // 默认玩家先手
            playerFirst = true;
            isplayerTurn = true;//开局自动生效

            // 设置默认角色图片路径（常规 / 无尽）
            SetCharacterImagePaths(
                "Image\\方源第一次宿命大战.jpg",
                "Image\\方源第二次宿命大战.jpg",
                "Image\\龙公.png",
                "Image\\龙公三气归来.png"
            );

            // 初始化属性
            InitStats();

            // 初始日志与随机先手提示
            // 设计要求：日志初始仅一行，随后追加随机先手信息
            logTextBox.Text = "战斗即将开始";
            AppendLog("玩家先手");

            // 绑定按钮事件 (卡牌通过手牌按钮触发)
            btnEndTurn.Click += BtnEndTurn_Click;

            // 全屏切换快捷键绑定
            this.KeyPreview = true;
            this.KeyDown += BattleSoul_KeyDown;

            // 启用双缓冲以减少闪烁（适用于窗体和面板）
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            handPanel.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.SetValue(handPanel, true);
            logTextBox.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.SetValue(logTextBox, true);

            // 保存原始布局数据以便全屏时等比缩放和退出还原
            SaveOriginalLayout();

            // 绑定暂停按钮（在设计器中创建）
            btnPause.Click += BtnPause_Click;

            // 初始化卡牌提示工具
            cardToolTip = new ToolTip()
            {
                AutoPopDelay = 10000,
                InitialDelay = 300,
                ReshowDelay = 200,
                ShowAlways = true
            };
            RefreshUI();
            this.Resize += BattleSoul_Resize;
        }
        // 方法: BattleSoul_Load
        // 窗体加载事件（初始化后回调）
        private void BattleSoul_Load(object sender, EventArgs e)
        {
            //设置WindowsMediaPlayer组件不可见
            //添加该组件的方法:工具箱空白处->鼠标右键->选择项->COM组件->Windows Media Player
            axWindowsMediaPlayerBGM.Visible = false;
            axWindowsMediaPlayereasteregg.Visible = false;
            //为WindowsMediaPlayer组件设置音乐文件并启动播放
            axWindowsMediaPlayerBGM.URL = "Sound\\bgm.mp3";
            axWindowsMediaPlayerBGM.settings.autoStart = true;
            axWindowsMediaPlayerBGM.settings.setMode("loop", true);
        }
        // 方法: BattleSoul_Resize
        // 窗体尺寸变化处理（用于触发自定义全屏逻辑）
        private void BattleSoul_Resize(object? sender, EventArgs e)
        {
            // if user clicks maximize (window state becomes Maximized), enter our fullscreen mode
            if (this.WindowState == FormWindowState.Maximized && !isFullscreen)
            {
                ToggleFullscreen();
            }
            else if (this.WindowState == FormWindowState.Normal && isFullscreen)
            {
                ToggleFullscreen();
            }
        }
        // 方法: pictureBoxDesktop_Paint
        // PictureBox 的 Paint 事件处理，委托给 Desktop 进行自绘
        private void pictureBoxDesktop_Paint(object? sender, PaintEventArgs e)
        {
            _desktop.Draw(e.Graphics);
        }

        // 方法: BtnPause_Click
        // 暂停按钮点击处理
        private void BtnPause_Click(object? sender, EventArgs e)
        {
            var dlg = new PauseDialog();
            // 暂停按钮：冻结逻辑（将 isPaused 设为 true，同时保持弹窗不关闭）
            dlg.BtnPause.Click += (s, ea) =>
            {
                isPaused = true;
                AppendLog("游戏已暂停。");
            };
            // 重启按钮：重置游戏状态并关闭弹窗
            dlg.BtnRestart.Click += (s, ea) =>
            {
                isPaused = false;
                AppendLog("重新开始游戏。");
                // 根据复选框设置无尽模式
                isEndlessMode = dlg.ChkEndless.Checked;
                InitStats();
                // 如果是无尽模式，将敌方生命值设置为 500
                if (isEndlessMode)
                {
                    enemyMaxHp = 500;
                    enemyCurHp = enemyMaxHp;
                }
                RefreshUI();
                dlg.DialogResult = DialogResult.OK;
                dlg.Close();
            };
            // 退出按钮：关闭主窗口（退出程序 / 返回主界面）
            dlg.BtnExit.Click += (s, ea) =>
            {
                isPaused = false;
                dlg.DialogResult = DialogResult.Cancel;
                dlg.Close();
                this.Close();
            };

            // 打开暂停弹窗时，同步显示当前无尽模式的状态
            dlg.ChkEndless.Checked = isEndlessMode;

            // 以模态方式显示对话框，阻塞主窗体交互
            dlg.ShowDialog(this);
            // 对话框关闭时，若暂停标记为开启状态，则保持暂停状态，直至用户重新开始或退出程序
        }

        // 全屏缩放支持
        private bool isFullscreen = false;
        private Rectangle originalBounds;
        private Dictionary<Control, Rectangle> originalControlBounds = new Dictionary<Control, Rectangle>();
        private Dictionary<Control, float> originalControlFontSizes = new Dictionary<Control, float>();

        // 方法: SaveOriginalLayout
        // 保存窗体及其子控件的原始边界与字体尺寸，用于全屏还原
        private void SaveOriginalLayout()
        {
            originalBounds = this.Bounds;
            originalControlBounds.Clear();
            originalControlFontSizes.Clear();
            foreach (Control c in this.Controls)
            {
                originalControlBounds[c] = c.Bounds;
                if (c.Font != null) originalControlFontSizes[c] = c.Font.Size;
            }
        }

        // 方法: ApplyScale
        // 根据当前窗体尺寸按等比比例缩放所有控件，实现黑边居中显示
        private void ApplyScale(float scale)
        {
            this.SuspendLayout();
            // 计算居中显示区域以保留黑边
            Size baseSize = new Size(800, 600);
            var target = this.Bounds.Size;
            float ratio = Math.Min((float)target.Width / baseSize.Width, (float)target.Height / baseSize.Height);
            // 应用于控件
            foreach (var kv in originalControlBounds)
            {
                var c = kv.Key;
                var orig = kv.Value;
                int nx = (int)(orig.X * ratio + (target.Width - baseSize.Width * ratio) / 2);
                int ny = (int)(orig.Y * ratio + (target.Height - baseSize.Height * ratio) / 2);
                int nw = (int)(orig.Width * ratio);
                int nh = (int)(orig.Height * ratio);
                c.SetBounds(nx, ny, nw, nh);
                if (originalControlFontSizes.ContainsKey(c) && c.Font != null)
                {
                    float origSize = originalControlFontSizes[c];
                    c.Font = new Font(c.Font.FontFamily, Math.Max(6f, origSize * ratio), c.Font.Style);
                }
                // 确保 PictureBox 使用 SizeMode.Zoom 以保持长宽比
                if (c is PictureBox pb) pb.SizeMode = PictureBoxSizeMode.Zoom;
            }
            this.ResumeLayout();
        }

        // 方法: RestoreLayout
        // 恢复保存的原始控件边界与字体尺寸
        private void RestoreLayout()
        {
            this.SuspendLayout();
            foreach (var kv in originalControlBounds)
            {
                var c = kv.Key;
                var orig = kv.Value;
                c.SetBounds(orig.X, orig.Y, orig.Width, orig.Height);
                if (originalControlFontSizes.ContainsKey(c) && c.Font != null)
                {
                    float origSize = originalControlFontSizes[c];
                    c.Font = new Font(c.Font.FontFamily, origSize, c.Font.Style);
                }
            }
            this.ResumeLayout();
        }

        // 方法: BattleSoul_KeyDown
        // 键盘按键处理（用于监听 F11 / Escape 切换全屏）
        private void BattleSoul_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
            }
            else if (e.KeyCode == Keys.Escape && isFullscreen)
            {
                ToggleFullscreen();
            }
        }

        // 方法: ToggleFullscreen
        // 切换自定义全屏模式（移除边框并全屏显示 / 恢复原始窗口）
        private void ToggleFullscreen()
        {
            if (!isFullscreen)
            {
                // 进入全屏
                SaveOriginalLayout();
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal; // ensure bounds can be set
                this.Bounds = Screen.FromControl(this).Bounds;
                isFullscreen = true;
                ApplyScale(1f);
            }
            else
            {
                // 退出全屏，恢复原始布局
                isFullscreen = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.WindowState = FormWindowState.Normal;
                this.Bounds = originalBounds;
                RestoreLayout();
            }
        }

        // 抽卡系统
        private enum CardType
        {
            NormalAtk,
            DotAtk,
            StackAtk,
            Block,
            ClearDot,
            Shield,
            SomethingForNothing,
            SkyLotus,
            CrushingPower,
            Counter
        }

        private List<CardType> drawPile = new List<CardType>();
        private List<CardType> discardPile = new List<CardType>();
        private List<CardType> hand = new List<CardType>();
        private const int HandMax = 5;
        private bool pendingStartDraw = false;
        private bool isFirstTurn = true;
        private bool playerBlockPlayedThisTurn = false;
        private bool playerCrushingPlayedThisTurn = false;
        private bool playerCrushingActive = false;
        private bool playerCounterPlayedThisTurn = false;
        private int enemyDamageDealtThisTurn = 0;

        // 方法: BuildInitialDeck
        // 构建初始卡组并抽取初始手牌
        private void BuildInitialDeck()
        {
            drawPile.Clear();
            discardPile.Clear();
            hand.Clear();
            // 明确定义每种卡牌的数量，便于修改
            var counts = new Dictionary<CardType, int>
            {
                { CardType.NormalAtk, 5 },//普通攻击
                { CardType.DotAtk, 3 },//持续伤害
                { CardType.StackAtk, 2 },//五指拳心剑
                { CardType.Block, 2 },//小幅格挡
                { CardType.ClearDot, 1 },//净化驱散
                { CardType.Shield, 5 },//凝盾护体
                { CardType.SomethingForNothing, 2 },//无中生有
                { CardType.SkyLotus, 2 },//天元宝莲
                { CardType.CrushingPower, 2 },//摧枯拉朽
                { CardType.Counter, 2 }//反击架势
            };
            // 按固定顺序加入抽牌堆（不洗牌）
            var order = new CardType[] {
                CardType.NormalAtk,
                CardType.DotAtk,
                CardType.StackAtk,
                CardType.Block,
                CardType.ClearDot,
                CardType.Shield,
                CardType.SomethingForNothing,
                CardType.SkyLotus,
                CardType.CrushingPower,
                CardType.Counter
            };
            foreach (var t in order)
            {
                int copies = counts.ContainsKey(t) ? counts[t] : 0;
                for (int i = 0; i < copies; i++) drawPile.Add(t);
            }
            // 初始手牌五张（从 drawPile 中随机抽取，但保留 drawPile 中剩余的物理顺序）
            DrawRandomInitialHand(5);
            UpdateDeckUI();
        }

        // DrawRandomInitialHand
        // 随机抽取初始手牌（从抽牌堆中按随机索引抽取，但保留抽牌堆剩余顺序）
        private void DrawRandomInitialHand(int count)
        {
            var indices = Enumerable.Range(0, drawPile.Count).ToList();
            // 随机打乱索引列表并取前 count 个
            for (int i = indices.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                var tmp = indices[i]; indices[i] = indices[j]; indices[j] = tmp;
            }
            var pick = indices.Take(count).OrderBy(i => i).ToList();
            // 提取选定项，务必从末尾开始移除，保证索引有效
            for (int k = pick.Count - 1; k >= 0; k--)
            {
                int idx = pick[k];
                hand.Add(drawPile[idx]);
                drawPile.RemoveAt(idx);
            }
            RenderHand();
        }

        private void Shuffle(List<CardType> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        // 从抽牌堆抽 count 张到手牌，若抽牌堆不足则把弃牌堆洗回抽牌堆
        // 方法: DrawToHand
        // 从抽牌堆抽取指定数量的牌到手牌，必要时洗入弃牌堆
        private void DrawToHand(int count, bool allowOverdraw = false)
        {
            for (int i = 0; i < count; i++)
            {
                if (!allowOverdraw && hand.Count >= HandMax) break;
                if (drawPile.Count == 0)
                {
                    // 洗回弃牌堆
                    if (discardPile.Count > 0)
                    {
                        drawPile.AddRange(discardPile);
                        discardPile.Clear();
                        Shuffle(drawPile);
                    }
                    else break;
                }
                var card = drawPile[0];
                drawPile.RemoveAt(0);
                hand.Add(card);
            }
            RenderHand();
            UpdateDeckUI();
        }

        // 方法: RenderHand
        // 渲染手牌区控件（将手牌生成为按钮或图片容器）
        private void RenderHand()
        {
            // 确保 FlowLayoutPanel 在一行显示所有牌并启用滚动，这样在面板较窄时仍可水平浏览
            handPanel.FlowDirection = FlowDirection.LeftToRight;
            handPanel.WrapContents = false;
            handPanel.AutoScroll = true;
            handPanel.Controls.Clear();
            for (int i = 0; i < hand.Count; i++)
            {
                var c = hand[i];
                var btn = new Button();
                // 采用更大的按钮，以完整显示卡片名称
                btn.Size = new Size(140, 40);
                btn.Font = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.AutoSize = false;
                btn.Tag = i;
                int cost = CardCost(c);
                btn.Text = cost > 0 ? $"{CardName(c)} ({cost})" : $"{CardName(c)} (0)";
                btn.Click += CardButton_Click;
                // 设置提示文本
                if (cardToolTip != null)
                {
                    cardToolTip.SetToolTip(btn, CardDescription(c));
                }
                // 禁用不可出牌（非玩家回合、游戏结束或能量不足），并处理单次卡牌约束
                bool enabled = isplayerTurn && !isGameOver && playerEnergy >= cost;
                if (c == CardType.Block && playerBlockPlayedThisTurn) enabled = false;
                if (c == CardType.CrushingPower && playerCrushingPlayedThisTurn) enabled = false;
                // 叠层卡在非无尽模式达到 5 层后不可再使用；无尽模式允许继续出牌（但伤害按第五次计算）
                if (c == CardType.StackAtk && !isEndlessMode && playerStack >= 5) enabled = false;
                if (c == CardType.Counter && playerCounterPlayedThisTurn) enabled = false;
                btn.Enabled = enabled;
                handPanel.Controls.Add(btn);
            }
        }
        // 返回卡牌说明文本，用于 ToolTip 显示
        private string CardDescription(CardType c)
        {
            return c switch
            {
                CardType.NormalAtk => "普通攻击：造成基础攻击力的伤害，费用 0。",
                CardType.DotAtk => "持续伤害：造成即时伤害并叠加 1 层 DOT（持续3回合），费用 2。",
                CardType.StackAtk => "五指拳心剑：叠层攻击，随使用次数递增伤害，费用 2（最多5层）。",
                CardType.Block => "小幅格挡：本回合减伤 30%，费用 1（每回合限用一次）。",
                CardType.ClearDot => "净化驱散：清除自身 DOT，费用 3。",
                CardType.Shield => "凝盾护体：获得一段护盾（持续），费用 0。",
                CardType.SomethingForNothing => "无中生有：抽取两张牌（可破手牌上限），费用 0。",
                CardType.SkyLotus => "天元宝莲：回复 2 点能量（可临时超过上限），费用 0。",
                CardType.CrushingPower => "摧枯拉朽：本回合提高伤害效果，费用 1（每回合限用一次）。",
                CardType.Counter => "反击架势：本回合反弹敌方伤害的一半，费用 2（每回合限用一次）。",
                _ => "未知卡牌",
            };
        }
        

        // 方法: CardName
        // 返回卡牌的显示名称（中文）
        private string CardName(CardType c)
        {
            return c switch
            {
                CardType.NormalAtk => "普通攻击",
                CardType.DotAtk => "持续伤害",
                CardType.StackAtk => "五指拳心剑",
                CardType.Block => "小幅格挡",
                CardType.ClearDot => "净化驱散",
                CardType.Shield => "凝盾护体",
                CardType.SomethingForNothing => "无中生有",
                CardType.SkyLotus => "天元宝莲",
                CardType.CrushingPower => "摧枯拉朽",
                CardType.Counter => "反击架势",
                _ => "未知",
            };
        }

        // 方法: UpdateDeckUI
        // 更新抽牌与弃牌计数的 UI 显示
        private void UpdateDeckUI()
        {
            lblDrawCount.Text = drawPile.Count.ToString();
            lblDiscardCount.Text = discardPile.Count.ToString();
        }

        // 方法: PicDiscard_Click
        // 点击查看弃牌堆事件处理
        private void PicDiscard_Click(object? sender, EventArgs e)
        {
            var names = new List<string>();
            foreach (var c in discardPile) names.Add($"{CardName(c)} ({CardCost(c)})");
            var viewer = new CardViewer(names);
            viewer.ShowDialog(this);
        }

        // 方法: PicDraw_Click
        // 点击查看抽牌堆事件处理
        private void PicDraw_Click(object? sender, EventArgs e)
        {
            var names = new List<string>();
            foreach (var c in drawPile) names.Add($"{CardName(c)} ({CardCost(c)})");
            var viewer = new CardViewer(names);
            viewer.ShowDialog(this);
        }

        // 方法: CardButton_Click
        // 手牌按钮点击事件处理（打出手牌）
        private void CardButton_Click(object? sender, EventArgs e)
        {
            if (isPaused) { AppendLog("当前处于暂停状态，无法操作。"); return; }
            if (sender is Button btn && int.TryParse(btn.Tag.ToString(), out int idx))
            {
                if (idx < 0 || idx >= hand.Count) return;
                var card = hand[idx];
                // 打出牌的费用判断
                int cost = CardCost(card);
                if (playerEnergy < cost) { AppendLog("能量不足，无法打出该卡。"); return; }
                // 执行卡牌效果
                // 额外校验：若为叠层卡且已达到5层则不能出（无尽模式允许继续出牌）
                if (card == CardType.StackAtk && !isEndlessMode && playerStack >= 5)
                {
                    AppendLog("叠层已达最大次数，无法打出叠层卡。");
                    return;
                }
                // 额外校验：反击一回合只能使用一张
                if (card == CardType.Counter && playerCounterPlayedThisTurn)
                {
                    AppendLog("本回合已使用过反击，无法再次使用。\n");
                    return;
                }
                PlayCard(card);
                // 打出的牌进弃牌堆
                discardPile.Add(card);
                hand.RemoveAt(idx);
                RenderHand();
                UpdateDeckUI();
                RefreshUI();
                CheckGameOver();
            }
        }

        // CardCost
        // 返回指定卡牌的能量消耗
        // 方法: CardCost
        // 获取指定卡牌的能量消耗
        private int CardCost(CardType c)
        {
            return c switch
            {
                CardType.NormalAtk => 0,
                CardType.Block => 1,
                // 注：治疗类逻辑并非卡牌实现，保留成本映射以备需要
                CardType.ClearDot => 3,
                CardType.DotAtk => 2,
                CardType.StackAtk => 2,
                CardType.Shield => 0,
                CardType.Counter => 2,
                CardType.SomethingForNothing => 0,
                CardType.SkyLotus => 0,
                CardType.CrushingPower => 1,
                _ => 1,
            };
        }

        // PlayCard
        // 执行指定卡牌的效果并扣除能量
        // 方法: PlayCard
        // 执行卡牌效果并扣除能量
        private void PlayCard(CardType c)
        {
            int cost = CardCost(c);
            playerEnergy -= cost;
            switch (c)
            {
                case CardType.NormalAtk:
                    DoNormalAtk();
                    break;
                case CardType.DotAtk:
                    DoDotAtk();
                    break;
                case CardType.StackAtk:
                    DoStackAtk();
                    break;
                case CardType.Block:
                    DoBlock();
                    break;
                case CardType.ClearDot:
                    DoClearDot();
                    break;
                case CardType.Counter:
                    DoCounter();
                    break;
                case CardType.SomethingForNothing:
                    DoSomethingForNothing();
                    break;
                case CardType.SkyLotus:
                    DoSkyLotus();
                    break;
                case CardType.CrushingPower:
                    DoCrushingPower();
                    break;
                case CardType.Shield:
                    DoShield();
                    break;
            }
        }

        // --- 抽离不消耗能量的技能实现 （DoXxx） ---
        // 方法: DoNormalAtk
        // 执行普通攻击的效果
        private void DoNormalAtk()
        {
            int baseDmg = playerAtk;
            int dmg = baseDmg + (playerCrushingActive ? 3 : 0);
            DealDamageToEnemy(dmg, "玩家 普通攻击");
            AppendLog("玩家 使用 普通攻击。");
            RefreshUI();
            CheckGameOver();
        }

        // 方法: DoDotAtk
        // 执行持续伤害卡的效果（立即造成部分伤害并叠加 DOT）
        private void DoDotAtk()
        {
            // 即时伤害固定为 4 点
            int instant = 4;
            DealDamageToEnemy(instant, "玩家 持续伤害(即时伤害)");
            enemyDotCount += 1;
            enemyDotRound = 3;
            // 标记为本回合刚被施加，周期性伤害从下一个回合开始结算
            enemyDotJustApplied = true;
            AppendLog("玩家 对敌人施加了 持续伤害 层数 +1 (持续3回合)。");
            RefreshUI();
            CheckGameOver();
        }

        // 方法: DoStackAtk
        // 执行叠层攻击（五指拳心剑）的效果并处理叠层耗尽逻辑，PS：彩蛋也写在这里了
        private void DoStackAtk()
        {
            // 新伤害模型：基础伤害为6点，每次打出伤害乘以3
            int currentUses = playerStack; // 已使用次数
            // 计算原始伤害（按当前使用次数），但在无尽模式下第5次及以后只按第五次的伤害计算
            long dmgLong = 6L * (long)Math.Pow(3, Math.Min(currentUses, 4));
            int dmg = (int)Math.Min(dmgLong, int.MaxValue);
            // 在造成伤害前记录敌方生命值以便检测是否被此次攻击击杀
            int beforeHp = enemyCurHp;
            // 叠层计数：非无尽模式限制为最多5层，达到后消耗殆尽；无尽模式允许继续出牌但不增加更高伤害
            if (!isEndlessMode)
            {
                playerStack = Math.Min(playerStack + 1, 5);
            }
            else
            {
                // 在无尽模式下仍然增加计数以展示，但不超出 5 表示已经达到第五次样式
                playerStack = Math.Min(playerStack + 1, int.MaxValue);
            }
            // 造成伤害（若已超过第五次，dmg 已按第五次伤害计算）
            DealDamageToEnemy(dmg, "玩家 使用 五指拳心剑");
            AppendLog($"玩家 使用 五指拳心剑，造成 {dmg} 点伤害，叠层增加到 {playerStack}。\n");
            // 若达到最大叠层次数并非无尽模式，消耗殆尽
            if (!isEndlessMode && playerStack >= 5)
            {
                ExhaustStackCards();
                // 当第五层五指拳心剑造成的伤害直接击杀敌方时，触发广播窗显示诗句（仅在此次攻击前敌人存活且被此次攻击击杀时触发）
                if (beforeHp > 0 && enemyCurHp <= 0)
                {
                    try
                    {
                        var poem = "历经五十四次劫，劫云仍旧漫遮天。胸中魂光压众生，拳里剑气纵北原。时来时去四百载，无死何能生新颜?弃此残躯换清风，卷席苍穹复光年!";
                        // 由于彩蛋音频时长 21 秒，设置诗词滚动时长为 21000ms（可在 20000-22000ms 之间微调）
                        var bf = new BroadcastForm(poem, durationMs: 21000);
                        bf.TopMost = true;
                        // 彩蛋播放期间暂停游戏逻辑，并播放彩蛋音乐
                        try
                        {
                            isPaused = true;
                            axWindowsMediaPlayerBGM.Ctlcontrols.pause();
                            axWindowsMediaPlayereasteregg.URL = "Sound\\游戏彩蛋.mp3";
                            axWindowsMediaPlayereasteregg.Ctlcontrols.play();
                            bf.ShowDialog(this);
                        }
                        finally
                        {
                            // 停止音乐并恢复游戏
                            try { axWindowsMediaPlayereasteregg.Ctlcontrols.stop(); } catch { }
                            isPaused = false;
                            axWindowsMediaPlayerBGM.Ctlcontrols.play();
                        }
                    }
                    catch
                    {
                        // 若广播窗创建失败，不影响游戏流程
                        isPaused = false;
                    }
                }
            }
            RefreshUI();
            CheckGameOver();
        }

        // 方法: ExhaustStackCards
        // 从所有牌堆中移除所有叠层卡（当达到上限时消耗殆尽）
        private void ExhaustStackCards()
        {
            // 从 drawPile、discardPile 和 hand 中移除所有叠层卡
            int removed = 0;
            removed += drawPile.RemoveAll(c => c == CardType.StackAtk);
            removed += discardPile.RemoveAll(c => c == CardType.StackAtk);
            // 从手牌中也移除，并将这些牌不进入弃牌堆（彻底消失）
            for (int i = hand.Count - 1; i >= 0; i--)
            {
                if (hand[i] == CardType.StackAtk)
                {
                    hand.RemoveAt(i);
                    removed++;
                }
            }
            if (removed > 0)
            {
                AppendLog("五指拳心剑已消耗殆尽");
            }
            RenderHand();
            UpdateDeckUI();
        }

        // 方法: DoBlock
        // 执行小幅格挡（设置本回合减伤标志）
        private void DoBlock()
        {
            if (playerBlockPlayedThisTurn)
            {
                AppendLog("本回合已使用过小幅格挡，无法再次使用。");
                return;
            }
            playerIsBlock = true;
            playerBlockPlayedThisTurn = true;
            AppendLog("玩家 使用 小幅格挡（本回合减伤30%）。");
            RefreshUI();
        }

        // 方法: DoHeal
        // 执行治疗效果（生命调息）
        private void DoHeal()
        {
            int heal = 12;
            playerCurHp = Math.Min(playerMaxHp, playerCurHp + heal);
            AppendLog($"玩家 使用 生命调息，回复 {heal} 点生命。当前生命 {playerCurHp}/{playerMaxHp}。\n");
            RefreshUI();
        }

        // 方法: DoClearDot
        // 执行净化驱散（清除自身 DOT）
        private void DoClearDot()
        {
            playerDotCount = 0;
            playerDotRound = 0;
            AppendLog("玩家 使用 净化驱散，清除了自身 DOT。\n");
            RefreshUI();
        }

        // 方法: DoShield
        // 执行凝盾护体（增加护盾）
        private void DoShield()
        {
            playerShied += ShieldValue;
            AppendLog($"玩家 使用 凝盾护体，获得 {ShieldValue} 点护盾。\n");
            RefreshUI();
        }

        // 方法: DoSomethingForNothing
        // 执行无中生有（从抽牌堆抽取两张牌）
        private void DoSomethingForNothing()
        {
            // 从抽牌堆抽取两张牌（遵循 drawPile 物理顺序抽取）
            DrawToHand(2, allowOverdraw: true);
            AppendLog("玩家 使用 无中生有，从抽牌堆抽取两张牌。");
            RefreshUI();
        }

        // 方法: DoSkyLotus
        // 执行天元宝莲（回复两格能量，可临时超过上限）
        private void DoSkyLotus()
        {
            // 天元宝莲允许临时超过默认上限（例如从4到6）
            playerEnergy += 2;
            AppendLog("玩家 使用 天元宝莲，回复两格能量（可临时超过4点）。");
            RefreshUI();
        }

        // 方法: DoCrushingPower
        // 执行摧枯拉朽（本回合提升普通和叠层攻击）
        private void DoCrushingPower()
        {
            if (playerCrushingPlayedThisTurn)
            {
                AppendLog("本回合已使用过摧枯拉朽，无法再次使用。");
                return;
            }
            playerCrushingPlayedThisTurn = true;
            playerCrushingActive = true;
            AppendLog("玩家 使用 摧枯拉朽：本回合普通攻击 +3，叠层攻击改为 3^n（仅本回合有效）。");
            RefreshUI();
        }

        // 方法: DoCounter
        // 执行反击架势（本回合反弹部分伤害）
        private void DoCounter()
        {
            if (playerCounterPlayedThisTurn)
            {
                AppendLog("本回合已使用过反击，无法再次使用。\n");
                return;
            }
            playerIsCounter = true;
            playerCounterPlayedThisTurn = true;
            AppendLog("玩家 使用 反击架势：本回合将反弹敌人本回合造成伤害的一半。\n");
            RefreshUI();
        }


        // 方法: playerNameLabel_Click
        // 玩家名称标签点击事件（占位）
        private void playerNameLabel_Click(object sender, EventArgs e)
        {

        }

        

        // 方法: InitStats
        // 初始化玩家与敌人的基础属性并构建牌组
        private void InitStats()
        {
            // 玩家初始属性
            playerMaxHp = 100;
            playerCurHp = 100;
            playerAtk = 8;
            playerShied = 0;
            playerEnergy = 4;
            playerStack = 0;
            playerDotCount = 0;
            playerDotRound = 0;
            playerIsBlock = false;
            playerIsCounter = false;

            // 敌人初始属性
            enemyMaxHp = isEndlessMode ? 500 : 300;
            enemyCurHp = enemyMaxHp;
            enemyAtk = 8;
            enemyDef = 0;
            enemyShield = 0;
            enemyEnergy = 4;
            enemyStack = 0;
            enemyDotCount = 0;
            enemyDotRound = 0;
            enemyIsBlock = false;
            enemyIsCounter = false;

            isGameOver = false;
            playerTotalDamageDealt = 0;
            enemyOverflowDamage = 0;
            // 构建卡组
            BuildInitialDeck();
            // 初始化并根据模式更新角色图片
            UpdateCharacterImages();
        }

        // 方法：设置角色图片路径（由调用者指定资源路径或文件路径）
        public void SetCharacterImagePaths(string playerNormal, string playerEndless, string enemyNormal, string enemyEndless)
        {
            playerImageNormalPath = playerNormal;
            playerImageEndlessPath = playerEndless;
            enemyImageNormalPath = enemyNormal;
            enemyImageEndlessPath = enemyEndless;
            UpdateCharacterImages();
        }

        // 方法：根据当前模式更新 PictureBox 的图片，支持 1:1 图片自动缩放到 PictureBox（SizeMode = Zoom）
        private void UpdateCharacterImages()
        {
            try
            {
                // 先释放现有图片避免文件锁定
                if (picPlayer.Image != null)
                {
                    var old = picPlayer.Image;
                    picPlayer.Image = null;
                    old.Dispose();
                }
                if (picEnemy.Image != null)
                {
                    var old = picEnemy.Image;
                    picEnemy.Image = null;
                    old.Dispose();
                }

                string pPath = isEndlessMode ? playerImageEndlessPath ?? playerImageNormalPath : playerImageNormalPath;
                string ePath = isEndlessMode ? enemyImageEndlessPath ?? enemyImageNormalPath : enemyImageNormalPath;

                if (!string.IsNullOrEmpty(pPath) && System.IO.File.Exists(pPath))
                {
                    // 使用从文件加载并克隆的方式，避免文件句柄锁定
                    using (var img = Image.FromFile(pPath))
                    {
                        picPlayer.Image = new Bitmap(img);
                    }
                }
                else
                {
                    picPlayer.Image = null;
                }

                if (!string.IsNullOrEmpty(ePath) && System.IO.File.Exists(ePath))
                {
                    using (var img = Image.FromFile(ePath))
                    {
                        picEnemy.Image = new Bitmap(img);
                    }
                }
                else
                {
                    picEnemy.Image = null;
                }
            }
            catch
            {
                // 忽略图片加载错误，保持为空
                picPlayer.Image = null;
                picEnemy.Image = null;
            }
        }

        // 日志输出（追加并滚动）
        // 方法: AppendLog
        // 向日志框追加一行文本并保持滚动底部
        private void AppendLog(string text)
        {
            if (string.IsNullOrEmpty(logTextBox.Text))
            {
                logTextBox.Text = text;
            }
            else
            {
                logTextBox.AppendText(Environment.NewLine + text);
            }
        }

        // 刷新界面文本和按钮状态
        // 方法: RefreshUI
        // 刷新界面上的数值文本与按钮状态
        private void RefreshUI()
        {
            labelplayerHp.Text = $"生命值：{playerCurHp}/{playerMaxHp}";
            labelplayerShield.Text = $"护盾值：{playerShied}";
            labelplayerEnergy.Text = $"当前充能：{playerEnergy}";
            labelplayerStack.Text = $"叠层次数：{playerStack}";
            labelplayerDot.Text = $"DOT 层数：{playerDotCount}";

            labelenemyHp.Text = $"生命值：{enemyCurHp}/{enemyMaxHp}";
            labelenemyShied.Text = $"护盾值：{enemyShield}";
            labelenemyEnergy.Text = $"当前充能：{enemyEnergy}";
            labelenemyStack.Text = $"叠层次数：{enemyStack}";
            labelenemyDot.Text = $"DOT 层数：{enemyDotCount}";

            // 按钮启用逻辑（仅玩家回合且游戏未结束）
            bool canAct = isplayerTurn && !isGameOver;
            // 手牌按钮与能量检查由 RenderHand 控制，EndTurn 始终可用在相应回合
            btnEndTurn.Enabled = canAct || (!isplayerTurn && !isGameOver);
        }

        // 伤害结算（对敌人）
        // 方法: DealDamageToEnemy
        // 对敌人造成伤害并处理护盾、格挡与反击
        private void DealDamageToEnemy(int dmg, string source)
        {
            if (dmg <= 0) return;
            int remaining = dmg;
            // 护盾先抵挡
            if (enemyShield > 0)
            {
                int used = Math.Min(enemyShield, remaining);
                enemyShield -= used;
                remaining -= used;
                AppendLog($"敌人护盾抵挡了 {used} 点伤害。");
            }
            // 格挡减伤 30%
            if (enemyIsBlock && remaining > 0)
            {
                int reduced = (int)Math.Ceiling(remaining * 0.3);
                remaining -= reduced;
                AppendLog($"敌人格挡，减免 {reduced} 点伤害。");
            }
            if (remaining > 0)
            {
                int actual = Math.Min(remaining, enemyCurHp);
                enemyCurHp -= remaining;
                if (enemyCurHp < 0)
                {
                    int overflow = -enemyCurHp;
                    enemyCurHp = 0;
                    if (isEndlessMode && overflow > 0)
                    {
                        // 溢出伤害记入下一条命
                        enemyOverflowDamage += overflow;
                        AppendLog($"{source} 对敌人造成 {remaining} 点伤害（含溢出 {overflow} 点，将计入敌人下一条命）。");
                    }
                    else
                    {
                        AppendLog($"{source} 对敌人造成 {remaining} 点伤害。");
                    }
                }
                else
                {
                    AppendLog($"{source} 对敌人造成 {remaining} 点伤害。");
                }
                // 记录玩家造成的总伤害（按实际减少量统计）
                playerTotalDamageDealt += actual;
            }
            // 反击姿态：反弹部分伤害
            if (enemyIsCounter && dmg > 0)
            {
                int reflect = Math.Max(1, dmg / 2);
                playerCurHp -= reflect;
                if (playerCurHp < 0) playerCurHp = 0;
                AppendLog($"敌人反击，对玩家反弹 {reflect} 点伤害。");
            }
        }

        // 伤害结算（对玩家）
        // 方法: DealDamageToPlayer
        // 对玩家造成伤害并处理护盾、格挡与反击
        private void DealDamageToPlayer(int dmg, string source)
        {
            if (dmg <= 0) return;
            int remaining = dmg;
            if (playerShied > 0)
            {
                int used = Math.Min(playerShied, remaining);
                playerShied -= used;
                remaining -= used;
                AppendLog($"玩家护盾抵挡了 {used} 点伤害。");
            }
            if (playerIsBlock && remaining > 0)
            {
                int reduced = (int)Math.Ceiling(remaining * 0.3);
                remaining -= reduced;
                AppendLog($"玩家格挡，减免 {reduced} 点伤害。");
            }
            if (remaining > 0)
            {
                playerCurHp -= remaining;
                AppendLog($"{source} 对玩家造成 {remaining} 点伤害。");
                // 记录敌方本回合对玩家造成的实际伤害（用于反击统计）
                enemyDamageDealtThisTurn += remaining;
                // 若玩家处于反击姿态，则立即按本次实际造成伤害的一半反弹给敌人
                if (playerIsCounter)
                {
                    int reflect = Math.Max(1, remaining / 2);
                    int actual = Math.Min(reflect, enemyCurHp);
                    enemyCurHp -= reflect;
                    if (enemyCurHp < 0) enemyCurHp = 0;
                    AppendLog($"玩家 反击，反弹 {reflect} 点伤害给敌人。");
                    playerTotalDamageDealt += actual;
                }
            }
        }

        // 方法: CheckGameOver
        // 检查并处理战斗结束的各种情况（胜负平）
        private void CheckGameOver()
        {
            if (isGameOver) return;
            if (playerCurHp <= 0 && enemyCurHp <= 0)
            {
                isGameOver = true;
                MessageBox.Show("平局！");
                AppendLog("战斗结束：平局");
                AppendLog($"玩家 对敌人造成的总伤害：{playerTotalDamageDealt}");
            }
            else if (playerCurHp <= 0)
            {
                isGameOver = true;
                MessageBox.Show("你输了！");
                AppendLog("战斗结束：玩家失败");
                AppendLog($"玩家 对敌人造成的总伤害：{playerTotalDamageDealt}");
            }
            else if (enemyCurHp <= 0)
            {
                // 敌人被击杀
                if (isEndlessMode)
                {
                    AppendLog("敌人 被击杀（无尽模式），将重置生命至 500 并继续战斗。");
                    enemyMaxHp = 500;
                    // 将溢出伤害应用到新生命（可能再次触发多次击杀）
                    int carry = enemyOverflowDamage;
                    enemyOverflowDamage = 0;
                    enemyDotCount = 0;
                    enemyDotRound = 0;
                    enemyIsBlock = false;
                    enemyIsCounter = false;
                    enemyCurHp = enemyMaxHp;
                    if (carry > 0)
                    {
                        AppendLog($"应用溢出伤害 {carry} 到新生命。开始处理溢出...");
                        while (carry > 0)
                        {
                            int applied = Math.Min(carry, enemyCurHp);
                            enemyCurHp -= applied;
                            playerTotalDamageDealt += applied;
                            carry -= applied;
                            AppendLog($"溢出伤害对当前生命造成 {applied} 点伤害，敌人生命 {enemyCurHp}/{enemyMaxHp}。");
                            if (enemyCurHp == 0 && carry > 0)
                            {
                                AppendLog("溢出伤害再次击杀敌人，重置生命并继续应用剩余溢出伤害。");
                                enemyCurHp = enemyMaxHp;
                                continue;
                            }
                            else break;
                        }
                        // 若仍有剩余 carry，保留为下一条命的初始溢出
                        enemyOverflowDamage = carry;
                    }
                    RefreshUI();
                    return;
                }
                isGameOver = true;
                MessageBox.Show("你赢了！");
                AppendLog("战斗结束：玩家胜利");
                AppendLog($"玩家 对敌人造成的总伤害：{playerTotalDamageDealt}");
            }
            if (isGameOver)
            {
                RefreshUI();
            }
        }

        // 玩家技能实现
        // 方法: BtnNormalAtk_Click
        // 玩家点击普通攻击按钮的处理
        private void BtnNormalAtk_Click(object? sender, EventArgs e)
        {
            if (!isplayerTurn || isGameOver) return;
            if (playerEnergy < 1) return;
            playerEnergy -= 1;
            DoNormalAtk();
        }

        // 方法: BtnDotAtk_Click
        // 玩家点击持续伤害按钮的处理
        private void BtnDotAtk_Click(object? sender, EventArgs e)
        {
            if (!isplayerTurn || isGameOver) return;
            if (playerEnergy < 2) return;
            playerEnergy -= 2;
            DoDotAtk();
        }

        // 方法: BtnStackAtk_Click
        // 玩家点击叠层攻击按钮的处理
        private void BtnStackAtk_Click(object? sender, EventArgs e)
        {
            if (!isplayerTurn || isGameOver) return;
            if (playerEnergy < 2) return;
            playerEnergy -= 2;
            DoStackAtk();
        }

        // 方法: BtnBlock_Click
        // 玩家点击小幅格挡按钮的处理
        private void BtnBlock_Click(object? sender, EventArgs e)
        {
            if (!isplayerTurn || isGameOver) return;
            if (playerEnergy < 1) return;
            playerEnergy -= 1;
            DoBlock();
        }

        // 治疗按钮处理已移除（不作为卡牌实现）

        // 方法: BtnClearDot_Click
        // 玩家点击净化驱散按钮的处理
        private void BtnClearDot_Click(object? sender, EventArgs e)
        {
            if (!isplayerTurn || isGameOver) return;
            if (playerEnergy < 2) return;
            playerEnergy -= 2;
            DoClearDot();
        }

        // 方法: BtnShield_Click
        // 玩家点击凝盾护体按钮的处理
        private void BtnShield_Click(object? sender, EventArgs e)
        {
            if (!isplayerTurn || isGameOver) return;
            if (playerEnergy < 2) return;
            playerEnergy -= 2;
            DoShield();
        }

        // 方法: BtnEndTurn_Click
        // 玩家结束回合按钮的处理，包含弃牌选择、敌方行动与回合结算
        private void BtnEndTurn_Click(object? sender, EventArgs e)
        {
            if (isPaused) { AppendLog("当前处于暂停状态，无法结束回合。"); return; }
            if (isGameOver) return;
            if (!isplayerTurn) return;
            AppendLog("玩家 结束回合，敌方行动中...");
            // 在结束回合时，如果手牌数大于上限，由玩家选择弃牌
            if (hand.Count > HandMax)
            {
                AppendLog($"手牌超过上限（{HandMax}），请选择要弃置的卡直到保留 {HandMax} 张。");
                var names = hand.Select(c => CardName(c)).ToList();
                var selector = new DiscardSelector(names, HandMax);
                if (selector.ShowDialog(this) == DialogResult.OK)
                {
                    var toDiscardIdx = selector.SelectedIndexes.OrderByDescending(x => x).ToList();
                    foreach (var idx in toDiscardIdx)
                    {
                        discardPile.Add(hand[idx]);
                        hand.RemoveAt(idx);
                    }
                    AppendLog($"玩家 弃置了 {toDiscardIdx.Count} 张手牌。");
                }
                else
                {
                    while (hand.Count > HandMax)
                    {
                        var c = hand[hand.Count - 1];
                        discardPile.Add(c);
                        hand.RemoveAt(hand.Count - 1);
                    }
                }
                RenderHand();
                UpdateDeckUI();
            }

            isplayerTurn = false;
            RefreshUI();
            // 玩家结束回合后，先结算 敌人 之前施加在玩家身上的 DOT（若有且非本回合刚施加）
            ApplyPlayerDotTickIfDue();
            CheckGameOver();
            if (isGameOver) return;
            // 敌方行动
            EnemyAction();
            // 敌人行动结束后，结算 玩家 之前施加在敌人身上的 DOT（若有且非本回合刚施加）
            ApplyEnemyDotTickIfDue();
            // 回合结算
            ResolveEndOfRound();
            RefreshUI();
            CheckGameOver();
        }

        // 敌方AI（按血量分段，单张牌行为，无能量机制）
        private void EnemyAction()
        {
            if (isPaused) { AppendLog("敌方行动被暂停。"); return; }
            if (isGameOver) return;
            double hpRatio = enemyMaxHp > 0 ? (double)enemyCurHp / enemyMaxHp : 0.0;

            double r = rand.NextDouble();

            if (hpRatio > 0.7)
            {
                // 高血量：80% 普通，15% 2倍，5% 4倍
                if (r < 0.80)
                {
                    int dmg = enemyAtk;
                    DealDamageToPlayer(dmg, "敌人 普通攻击");
                    AppendLog("敌人 使用 普通攻击。");
                    return;
                }
                else if (r < 0.95)
                {
                    int dmg = enemyAtk * 2;
                    DealDamageToPlayer(dmg, "敌人 强化普通攻击（2x）");
                    AppendLog("敌人 使用 强化普通攻击（2倍伤害）。");
                    return;
                }
                else
                {
                    int dmg = enemyAtk * 4;
                    DealDamageToPlayer(dmg, "敌人 爆发普通攻击（4x）");
                    AppendLog("敌人 使用 爆发普通攻击（4倍伤害）！");
                    return;
                }
            }
            else if (hpRatio > 0.3)
            {
                // 中血：50% DOT（即时4并叠层），50% 护盾（护盾量为 ShieldValue * 2）
                if (r < 0.5)
                {
                    int instant = 4;
                    DealDamageToPlayer(instant, "敌人 DOT侵蚀(即时伤害)");
                    playerDotCount += 1;
                    playerDotRound = 3;
                    // 标记为本回合刚被施加，周期性伤害从下一个回合开始结算
                    playerDotJustApplied = true;
                    AppendLog("敌人 使用 DOT 侵蚀。");
                    return;
                }
                else
                {
                    enemyShield += ShieldValue * 2;
                    AppendLog($"敌人 使用 凝盾护体，获得 {ShieldValue * 2} 点护盾。当前护盾 {enemyShield}。");
                    return;
                }
            }
            else
            {
                // 低血：50% 4x 攻击（临死反扑），50% 护盾保命（ShieldValue * 2）
                if (r < 0.5)
                {
                    int dmg = enemyAtk * 4;
                    DealDamageToPlayer(dmg, "敌人 绝望反扑（4x 普通攻击）");
                    AppendLog("敌人 使用 绝望反扑（4倍伤害）！");
                    return;
                }
                else
                {
                    enemyShield += ShieldValue * 2;
                    AppendLog($"敌人 使用 凝盾护体（临死尝试），获得 {ShieldValue * 2} 点护盾。当前护盾 {enemyShield}。");
                    return;
                }
            }
        }

        // 回合结束统一结算：DOT、清除临时buff、重置充能、切换回合
        private void ResolveEndOfRound()
        {
            // 注意：DOT 的周期性结算已在玩家结束回合与敌人行动后分别处理，故此处不重复结算。

            // 清空临时 buff（格挡、反击），护盾为持续值不在此处清空
            playerIsBlock = false;
            playerIsCounter = false;
            enemyIsBlock = false;
            enemyIsCounter = false;

            // 重置充能
            playerEnergy = 4;
            enemyEnergy = 4;

            // 切换为玩家回合
            isplayerTurn = true;
            // 重置本回合小幅格挡使用标志
            playerBlockPlayedThisTurn = false;
            // 重置摧枯拉朽相关状态
            playerCrushingActive = false;
            playerCrushingPlayedThisTurn = false;
            // 重置反击使用标志与敌方本回合造成伤害统计
            playerCounterPlayedThisTurn = false;
            enemyDamageDealtThisTurn = 0;
            // 在回合开始时抽牌（每回合抽3张），第一回合已在初始构建时抽过5张
            if (isFirstTurn)
            {
                isFirstTurn = false;
            }
            else
            {
                DrawToHand(3, allowOverdraw: true);
            }
            AppendLog("新回合开始：玩家回合。已进行抽牌。");
        }

        // 在玩家结束回合后调用：结算敌人之前施加在玩家身上的 DOT（如果到期且不是本回合刚施加）
        private void ApplyPlayerDotTickIfDue()
        {
            if (playerDotCount <= 0) return;
            if (playerDotJustApplied)
            {
                // 本回合刚被施加，跳过首次结算
                playerDotJustApplied = false;
                AppendLog("玩家 刚被施加 DOT，本回合延后开始结算（从下回合生效）。");
                return;
            }
            int totalDmg = playerDotCount * DotPerLayer;
            int absorbed = Math.Min(playerShied, totalDmg);
            if (absorbed > 0)
            {
                playerShied -= absorbed;
                AppendLog($"玩家 护盾抵挡了 {absorbed} 点 DOT 伤害。\n");
            }
            int remaining = totalDmg - absorbed;
            if (remaining > 0)
            {
                playerCurHp -= remaining;
                if (playerCurHp < 0) playerCurHp = 0;
                AppendLog($"玩家 受到 DOT {totalDmg} 点伤害（{playerDotCount} 层），护盾吸收 {absorbed} 点，生命扣除 {remaining} 点。");
            }
            else
            {
                AppendLog($"玩家 受到 DOT {totalDmg} 点伤害，被护盾完全抵挡（吸收 {absorbed} 点）。");
            }
            playerDotRound -= 1;
            if (playerDotRound <= 0)
            {
                playerDotCount = 0;
                playerDotRound = 0;
                AppendLog("玩家 的 DOT 效果消失。");
            }
        }

        // 在敌人行动并结束后调用：结算玩家之前施加在敌人身上的 DOT（如果到期且不是本回合刚施加）
        private void ApplyEnemyDotTickIfDue()
        {
            if (enemyDotCount <= 0) return;
            if (enemyDotJustApplied)
            {
                // 本回合刚被施加，跳过首次结算
                enemyDotJustApplied = false;
                AppendLog("敌人 刚被施加 DOT，本回合延后开始结算（从下回合生效）。");
                return;
            }
            int totalDmg = enemyDotCount * DotPerLayer;
            int absorbed = Math.Min(enemyShield, totalDmg);
            if (absorbed > 0)
            {
                enemyShield -= absorbed;
                AppendLog($"敌人 护盾抵挡了 {absorbed} 点 DOT 伤害。\n");
            }
            int remaining = totalDmg - absorbed;
            if (remaining > 0)
            {
                int actual = Math.Min(remaining, enemyCurHp);
                enemyCurHp -= remaining;
                if (enemyCurHp < 0) enemyCurHp = 0;
                AppendLog($"敌人 受到 DOT {totalDmg} 点伤害（{enemyDotCount} 层），护盾吸收 {absorbed} 点，生命扣除 {remaining} 点。");
                playerTotalDamageDealt += actual;
            }
            else
            {
                AppendLog($"敌人 受到 DOT {totalDmg} 点伤害，被护盾完全抵挡（吸收 {absorbed} 点）。");
            }
            enemyDotRound -= 1;
            if (enemyDotRound <= 0)
            {
                enemyDotCount = 0;
                enemyDotRound = 0;
                AppendLog("敌人 的 DOT 效果消失。");
            }
        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
