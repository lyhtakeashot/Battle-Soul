# Battle Soul

一款基于 Windows Forms 的卡牌战斗游戏，仿杀戮尖塔式（Slay the Spire）玩法。

## 环境要求

- .NET 10.0 SDK 或更高版本
- Windows 操作系统

## 运行方式

1. 确保已安装 .NET 10.0 SDK
2. 进入项目目录，运行以下命令：

```bash
dotnet run
```

## 项目结构与文件说明

| 文件 | 功能说明 |
|------|----------|
| `Battle Soul.csproj` | 项目配置文件，定义目标框架为 .NET 10.0 Windows Forms |
| `Battle Soul.slnx` | Visual Studio 解决方案文件 |
| `Form1.cs` | 主游戏窗体，包含核心游戏逻辑：卡牌系统、战斗系统、敌方 AI、UI 渲染等 |
| `Form1.Designer.cs` | 主窗体的设计器代码，定义界面布局和控件 |
| `Form1.resx` | 主窗体的资源文件 |
| `Program.cs` | 程序入口，启动主窗体 |
| `Desktop.cs` | 桌面背景绘制类，负责绘制游戏背景图片 |
| `CardViewer.cs` | 卡牌查看器对话框，用于展示抽牌堆或弃牌堆中的卡牌列表 |
| `CardViewer.resx` | 卡牌查看器的资源文件 |
| `DiscardSelector.cs` | 弃牌选择器对话框，当手牌超过上限时让玩家选择弃置哪些牌 |
| `DiscardSelector.resx` | 弃牌选择器的资源文件 |
| `PauseDialog.cs` | 暂停对话框，提供暂停、重新开始、退出游戏功能，支持无尽模式切换 |
| `PauseDialog.resx` | 暂停对话框的资源文件 |
| `BroadcastForm.cs` | 彩蛋广播窗口，在五指拳心剑第五层击杀敌人时显示诗句彩蛋 |
| `BroadcastForm.resx` | 广播窗口的资源文件 |

## 主要功能实现原理

### 1. 卡牌系统

**核心数据结构**：
- `drawPile`（抽牌堆）：存储待抽取的卡牌，使用 `List<CardType>` 实现
- `hand`（手牌）：玩家当前持有的卡牌，最多 5 张（无中生有可突破上限）
- `discardPile`（弃牌堆）：已打出的卡牌存放处，抽牌堆为空时洗入抽牌堆

**卡牌类型枚举**：定义了 10 种卡牌类型，通过 `CardType` 枚举区分

**抽牌机制**：
- 初始抽取 5 张手牌（随机抽取但保持抽牌堆顺序）
- 每回合开始抽取 3 张
- 抽牌堆为空时自动将弃牌堆洗牌并入抽牌堆

### 2. 战斗系统

**伤害结算流程**：
```
伤害 → 护盾抵挡 → 格挡减伤（30%）→ 剩余伤害扣血 → 反击反弹
```

**状态系统实现**：
- **护盾**：独立于生命值的防御值，优先承受伤害
- **DOT（持续伤害）**：每回合结算时造成 4 点/层伤害，持续 3 回合
- **格挡**：本回合有效，减少 30% 受到的伤害
- **反击**：本回合有效，反弹敌人伤害的一半

**回合结算逻辑**（`ResolveEndOfRound` 方法）：
1. 结算双方 DOT 伤害
2. 清除本回合临时状态（格挡、反击）
3. 重置能量为 4 点
4. 切换到玩家回合并抽牌

### 3. 敌方 AI

**血量分段策略**：
- **高血量（>70%）**：80% 普通攻击，15% 2倍攻击，5% 4倍攻击
- **中血量（30%~70%）**：50% 使用 DOT，50% 使用护盾
- **低血量（<30%）**：50% 4倍攻击（绝望反扑），50% 使用护盾保命

**随机决策机制**：使用 `Random.NextDouble()` 生成随机数，根据概率阈值选择行动

### 4. 无尽模式

**核心特性**：
- 敌人生命值提升至 500
- 敌人被击杀后自动重置生命继续战斗
- 溢出伤害累积到敌人下一条命

**实现方式**：通过 `isEndlessMode` 标志控制，在 `CheckGameOver` 方法中处理击杀后的重置逻辑

### 5. 五指拳心剑彩蛋

**触发条件**：在非无尽模式下，使用五指拳心剑达到第 5 层时击杀敌人

**实现流程**：
```
第五层五指拳心剑造成击杀 → 弹出 BroadcastForm → 显示诗句彩蛋 → 暂停游戏直到关闭窗口
```

**诗句内容**：
```
历经五十四次劫，劫云仍旧漫遮天。
胸中魂光压众生，拳里剑气纵北原。
时来时去四百载，无死何能生新颜?
弃此残躯换清风，卷席苍穹复光年!
```

## 游戏玩法和规则介绍

### 游戏目标

玩家通过打出手牌对敌人造成伤害，将敌人的生命值降为 0 即可获胜。玩家初始生命值为 100，敌人初始生命值为 300（无尽模式下为 500）。

### 能量系统

- 每回合开始时，玩家获得 4 点能量
- 出牌需要消耗能量（部分卡牌免费）
- 合理分配能量是获胜的关键

### 卡牌系统

游戏包含以下 10 种卡牌：

| 卡牌名称 | 费用 | 效果 |
|----------|------|------|
| 普通攻击 | 0 | 造成基础攻击力的伤害 |
| 持续伤害 | 2 | 造成即时伤害并叠加 1 层 DOT（持续 3 回合） |
| 五指拳心剑 | 2 | 叠层攻击，伤害为 6×3^(n-1)，最多 5 层 |
| 小幅格挡 | 1 | 本回合减伤 30%（每回合限用一次） |
| 净化驱散 | 3 | 清除自身 DOT |
| 凝盾护体 | 0 | 获得 15 点护盾（持续） |
| 无中生有 | 0 | 抽取两张牌（可破手牌上限） |
| 天元宝莲 | 0 | 回复 2 点能量（可临时超过上限） |
| 摧枯拉朽 | 1 | 本回合普通攻击 +3，叠层攻击改為 3^n（每回合限用一次） |
| 反击架势 | 2 | 本回合反弹敌人伤害的一半（每回合限用一次） |

### 状态系统

- **护盾**：优先于生命值抵挡伤害，不会自动清除
- **DOT（持续伤害）**：每回合造成 4 点/层的伤害，可被护盾抵挡，3 回合后消失
- **格挡**：本回合减伤 30%，回合结束时清除
- **反击**：本回合反弹敌人伤害的一半，回合结束时清除

### 敌方 AI

敌人根据自身血量不同会采取不同策略：

- **高血量（>70%）**：以普通攻击为主，偶尔使用强化攻击（2 倍或 4 倍伤害）
- **中血量（30%~70%）**：使用 DOT 侵蚀或护盾
- **低血量（<30%）**：高概率使用 4 倍伤害的绝望反扑，或优先叠护盾保命

### 无尽模式

在暂停菜单中可勾选无尽模式：
- 敌人初始生命值变为 500
- 击杀敌人后，敌人会重置生命值继续战斗
- 溢出伤害会累积到敌人下一条命

## 操作说明

### 鼠标操作

- **点击手牌**：打出对应卡牌
- **点击抽牌堆/弃牌堆图标**：查看对应牌堆中的卡牌
- **结束回合按钮**：结束当前回合，进入敌人行动阶段

### 游戏流程

1. 游戏开始，玩家先手，抽取 5 张手牌
2. 玩家回合：可自由出牌，消耗能量
3. 点击结束回合：选择弃置多余手牌（超过 5 张），然后进入敌人行动
4. 敌人行动：根据 AI 逻辑出牌
5. 回合结算：DOT 伤害生效，能量重置，回到玩家回合
6. 重复步骤 2-5，直至分出胜负

## 灵感与参考

本项目的制作灵感和设计元素来自以下作品和创作者：

### 主要玩法参考

- **杀戮尖塔（Slay the Spire）** — 游戏的核心玩法框架：回合制卡牌战斗、能量系统、抽牌堆/弃牌堆/手牌的牌组循环、DOT 持续伤害、护盾格挡等状态系统，均参考自这款经典 Roguelike 卡牌游戏。

### 角色、敌人与背景设定参考

- **蛊真人** — 游戏中的角色设定、敌人形象、世界观背景以及卡牌命名灵感均来源于此。具体相关的卡牌包括：
  - **五指拳心剑**：一记威力强大的仙道杀招，在游戏中以叠层攻击的形式呈现，伤害随叠层次数递增。
  - **天元宝莲**：原著中一系列能够产出源石和仙源石的蛊虫，在游戏中化身为回复能量的卡牌。

### 卡牌设计参考

- **龙族（Dragon Raja）** — 卡牌"无中生有"的英文译名灵感来源于龙族。
- **三国杀** — 游戏中两张核心卡牌的功能原型均参考自三国杀：
  - **无中生有**：卡牌名称和"抽取手牌"的核心功能原型。
  - **摧枯拉朽**：功能原型参考自三国杀中的"酒"，用于本回合增伤。

### 配音彩蛋参考

- **木成老师** — 五指拳心剑在第五层触发击杀时的彩蛋广播诗句，灵感来源于木成老师的配音风格，为游戏增添了一丝热血的仪式感。

## 许可证

本项目基于 MIT 许可证开源，欢迎参考学习。

MIT License

Copyright (c) 2025

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
