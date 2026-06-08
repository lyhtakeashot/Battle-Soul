using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Battle_Soul
{
    public class CardViewer : Form
    {
        // 构造函数：CardViewer
        // 创建一个模态窗体，展示传入的牌名列表（用于查看抽牌/弃牌堆内容）
        public CardViewer(List<string> cards)
        {
            this.Text = "牌堆查看";
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;
            var panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.Padding = new Padding(20);
            foreach (var c in cards)
            {
                var pb = new Panel();
                pb.Size = new Size(160, 220);
                pb.BorderStyle = BorderStyle.FixedSingle;
                var lbl = new Label();
                lbl.Text = c;
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                pb.Controls.Add(lbl);
                panel.Controls.Add(pb);
            }
            this.Controls.Add(panel);
        }
    }
}
