using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Battle_Soul
{
    public class DiscardSelector : Form
    {
        private List<CardSelection> selections = new List<CardSelection>();
        public List<int> SelectedIndexes { get; private set; } = new List<int>();

        // 构造函数：DiscardSelector
        // 创建一个模态窗体，允许玩家在手牌过多时选择要保留的卡牌（其余弃置）
        public DiscardSelector(List<string> cardNames, int keepCount)
        {
            this.Text = "选择要保留的手牌（将弃置其余）";
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            var panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.Padding = new Padding(20);

            for (int i = 0; i < cardNames.Count; i++)
            {
                var p = new Panel();
                p.Size = new Size(220, 120);
                p.BorderStyle = BorderStyle.FixedSingle;
                var lbl = new Label();
                lbl.Text = cardNames[i];
                lbl.Dock = DockStyle.Top;
                lbl.Height = 60;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                var cb = new CheckBox();
                cb.Text = "保留";
                cb.Dock = DockStyle.Bottom;
                cb.Checked = true; // 默认保留
                p.Controls.Add(lbl);
                p.Controls.Add(cb);
                panel.Controls.Add(p);
                selections.Add(new CardSelection { Index = i, Check = cb });
            }

            var btnPanel = new Panel();
            btnPanel.Dock = DockStyle.Bottom;
            btnPanel.Height = 60;
            var btnOk = new Button();
            btnOk.Text = "确认";
            btnOk.Dock = DockStyle.Right;
            btnOk.Width = 120;
            btnOk.Click += (s, e) => {
                // 收集被勾选为保留的索引；若不足 keepCount 则自动补足
                var keep = selections.Where(x => x.Check.Checked).Select(x => x.Index).ToList();
                if (keep.Count > keepCount)
                {
                    MessageBox.Show($"请只保留 {keepCount} 张手牌");
                    return;
                }
                if (keep.Count < keepCount)
                {
                    // 从未勾选项中依次补充，直到达到 keepCount
                    foreach (var sel in selections.Where(x => !x.Check.Checked))
                    {
                        keep.Add(sel.Index);
                        if (keep.Count == keepCount) break;
                    }
                }
                // 要弃置的索引为不在 keep 列表中的索引
                SelectedIndexes = Enumerable.Range(0, cardNames.Count).Where(i => !keep.Contains(i)).ToList();
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            btnPanel.Controls.Add(btnOk);
            this.Controls.Add(panel);
            this.Controls.Add(btnPanel);
        }

        private class CardSelection
        {
            public int Index;
            public CheckBox Check;
        }
    }
}
