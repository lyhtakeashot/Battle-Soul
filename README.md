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
