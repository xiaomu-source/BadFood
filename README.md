# **Bad Food**

> **一句话概括游戏主题**  
>
> "在像素世界中，随机制作美食迎战外星怪物的肉鸽塔防游戏。"

---

## **目录**
1. [游戏简介](#游戏简介)
2. [功能特性](#功能特性)
3. [安装与运行](#安装与运行)
4. [项目结构](#项目结构)
5. [项目规范](#项目规范)
6. [开发工具与依赖](#开发工具与依赖)
7. [贡献指南](#贡献指南)
8. [未来计划](#未来计划)
9. [许可证](#许可证)

---

## **游戏简介**
《Bad Food》是一款结合了**肉鸽元素（Roguelike）**、**抽卡机制**与**塔防玩法**的创新游戏。玩家需要通过随机抽卡制作美食，以满足口味刁钻的外星怪物的卡路里需求，同时迎接层层挑战，对抗强敌，最终力挽狂澜拯救世界！

**特色：**

- 仅支持 [单人模式]。
- 随机抽取卡牌制作美食，满足那些口味刁钻的怪物，将为你带来前所未有的挑战与乐趣！
- 肉鸽元素（Roguelike）、抽卡机制、塔防玩法、怪物的口味需求、不断进化的敌人、随机神器与天赋、增益系统、像素风美食世界。

---

## **功能特性**
- **核心玩法：**
  - 玩家通过抽卡制作美食，满足外星怪物的口味需求，同时利用塔防机制阻挡敌人的进攻。
  - 游戏融合了肉鸽元素，每局随机生成美食塔防、敌人、神器和天赋，带来独特的挑战。
  - 随着敌人进化和新敌人出现，玩家可以解锁随机神器和天赋，并通过合理搭配美食塔防、神器与天赋，制定策略应对愈加复杂的局势。
- **平台支持：**
  - steam
- **图形与音效：**
  - 支持高清分辨率、流畅的帧率。
  - 优质的背景音乐与音效设计。
- **扩展功能：**
  - 无

---

## **安装与运行**
### **克隆项目：**
```bash
git clone git@github.com:xiaomu-source/BadFood.git
```

### **运行步骤：**

1. 打开Unity Editor（推荐版本：**3.3.3-c1**）。
2. 打开项目文件夹。
3. 在 `Scenes/` 文件夹中，选择主场景文件（例如 `MainMenu.unity`）。
4. 点击 "Play" 按钮运行游戏。

### **构建游戏：**

1. 在Unity Editor中，导航到 `File > Build Settings`。
2. 选择目标平台（`PC` ）。
3. 点击 `Build` 进行游戏构建。

## **项目结构**

以下是项目文件夹的目录结构：

```bash
Assets 主目录
├── Editor 编辑器                   # 编辑器相关扩展或工具
├── Gizmos 标识                     # 自定义 Gizmos 图标
├── Plugins 插件                   # 第三方插件和依赖
├── Resources 资源                  # 动态加载资源
│   ├── Animations 动画              # 动画和动画控制器
│   ├── Audios 音频                  # 音效和背景音乐
│   ├── Fonts 字体                   # 字体资源
│   ├── Materials 材质               # 材质文件
│   ├── Models 模型                  # 3D 模型
│   ├── Prefabs 预制体               # 常用预制体
│   ├── SFXs 特效                   # 特效文件
│   ├── Sprites 精灵                 # 精灵资源
│   ├── Videos 视频                  # 视频文件
│   ├── Configs 配置                 # 配置文件（如 JSON 或 ScriptableObject）
│   ├── Maps 地图                   # 地图或场景配置文件
│   └── [具体业务文件夹]             # 为具体的资源逻辑独立管理
├── Scenes 场景                     # 场景资源
│   ├── Levels                     # 各级游戏关卡
│   └── Others                     # 主菜单、教程或其他场景
├── Scripts 脚本                    # 代码存放目录
│   ├── Commons 通用类              # 通用基础类和工具类
│   ├── Framework 框架类            # 项目框架和核心逻辑
│   │   ├── Core                     # 核心框架逻辑（例如单例、事件总线等）
│   │   ├── MVC                      # MVC 框架结构
│   │   │   ├── Controllers 控制器      # 控制层代码
│   │   │   ├── Models 数据模型         # 数据层代码
│   │   │   └── Views 视图层            # 视图层代码
│   │   └── Pool                     # 对象池管理器
│   ├── Application 应用逻辑        # 具体的业务代码
│   │   ├── Controllers              # 应用层的控制器逻辑
│   │   ├── Models                   # 应用层的数据模型
│   │   └── Views                    # 应用层的视图逻辑
│   ├── Gameplay 游戏相关逻辑       # 核心的游戏功能实现
│   │   └── Managers 管理类          # 全局管理器
│   ├── Shaders 自定义 Shader      # 自定义的 Shader 代码
│   └── Tools 工具类               # 辅助工具或脚本
├── Standard Assets 标准资源        # 官方标准资源
├── StreamingAssets 流文件资源      # 视频或配置等直接流化存取的文件
└── Sandbox 沙盒                   # 用于测试或实验用的目录
```

## **项目规范**

### **命名规范：**

- **文件夹命名**：首字母大写，采用 PascalCase，例如：`PlayerController`。
- **脚本命名**：遵循 PascalCase，例如：`EnemyAI.cs`。
- **变量命名**：驼峰命名法，例如：`playerHealth`。
- **预制体命名**：使用 `类别_名称` 格式，例如：`UI_MainMenu`。

### **代码规范：**

- 每个脚本的功能职责单一，尽量避免多功能混用。
- 使用注释描述关键代码逻辑和复杂算法。
- 避免硬编码，推荐将配置写入 ScriptableObjects 或 JSON 文件中。

### **场景规范：**

- 主场景文件应存放在 `Scenes/` 目录。
- 场景命名统一使用 `Scene_` 开头，例如：`Scene_MainMenu`。

### **资源管理：**

- 删除未使用的资源，避免项目冗余。
- UI资源和Prefab分开管理，每个界面对应唯一Prefab。

## **开发工具与依赖**

### **Unity版本：**

- **2022.3.47f1 LTS**

### **插件：**

- **TextMesh Pro** - 用于高级文本渲染。

### **外部工具：**

- [工具名称] - [用途]
  - 例如：Blender - 用于3D建模。
  - 例如：Audacity - 用于音效编辑。

## **贡献指南**

1. Fork 本项目并创建分支：

   ```bash
   git checkout -b feature.your-feature-name
   ```

2. 提交更改并推送到你的分支：

   ```bash
   git commit -m "增加了一个新功能"
   git push origin feature.your-feature-name
   ```

3. 提交Pull Request到主分支，描述你修改的内容。

### **Git分支与版本发布规范：**

- 基本原则：master为保护分支，不直接在master上进行代码修改和提交。
- 开发日常需求或者项目时，从master分支上checkout一个feature分支或者bugfix分支进行bug修复，功能测试完毕并且项目发布上线后，`将feature分支合并到主干master，并且打Tag发布，最后删除开发分支`。

### **分支命名规范：**

- `feat.功能名`：新增 feature。
- `fix.修复名`：修复 bug。
- `docs.修改文件名_描述`：仅仅修改了文件名，比如README.md。
- `style.描述`：仅仅修改了空格、格式缩进、逗号等等，不改变代码逻辑。
- `refactor.功能名`：代码重构，没有加新功能或者修复 bug。
- `perf.优化名`：优化相关，比如提升性能、体验。
- `test.测试名`：测试用例，包括单元测试、集成测试等。
- `chore.改动点`：改变构建流程、或者增加依赖库、工具等。
- `revert.版本号`：回滚到上一个版本。

### **commit格式要求：**

~~~~bash
# 标题行：50个字符以内，描述主要变更内容
#
# 主体内容：更详细的说明文本，建议72个字符以内。 需要描述的信息包括:
#
# * 为什么这个变更是必须的? 它可能是用来修复一个bug，增加一个feature，提升性能、可靠性、稳定性等等
# * 他如何解决这个问题? 具体描述解决问题的步骤
# * 是否存在副作用、风险?
#
# 尾部：如果需要的化可以添加一个链接到issue地址或者其它文档，或者关闭某个issue。
~~~~

### **Tag命名规范：**

Tag 用于版本管理，明确标记特定的发布节点，遵循以下命名规范：

#### **1. 基本规则**

- **语义化版本号 (Semantic Versioning)**：

  ```bash
  v<主版本号>.<次版本号>.<修订号>
  ```

  - 主版本号（Major）：发生重大变更（如新增核心功能、不兼容更新等）。
  - 次版本号（Minor）：新增功能，但与旧版本兼容。
  - 修订号（Patch）：用于修复 bug 或做微小改动。

  示例：

  ```
  v1.0.0  
  v2.1.3  
  v3.2.0  
  ```

- **可选的额外信息**：
  添加描述版本状态或渠道的后缀，以明确版本意义：

  ```bash
  v<主版本号>.<次版本号>.<修订号>-<标记>
  ```

  示例：

  ```bash
  v1.0.0-alpha      # 内测版本  
  v1.0.0-beta       # 公测版本  
  v1.0.0-rc         # 发布候选版本  
  v1.0.0-stable     # 稳定版本  
  v1.1.0-hotfix     # 热修复版本  
  ```

#### **2. 特殊情况**

- **初始版本**：用 `v1.0.0` 标记第一个正式发布的版本。
- **重大版本更新**：版本变化较大时标记主版本号递增，例如 `v2.0.0`。
- **快速修复**：热修复版本可使用 `-hotfix.<编号>`，如 `v1.2.1-hotfix.1`。

#### **3. 使用建议**

- Tag 名称开头加 `v`，表示版本号，方便区分其他类型标签。
- 遵循语义化版本规则，尽可能清晰表达变更内容。
- 按发布节点打 Tag，并与变更记录（CHANGELOG.md）保持一致。

#### **示例 Tag 名称：**

- 正式版本：

  ```bash
  v1.0.0  
  v2.3.0  
  v2.3.5  
  ```

- 预发布版本：

  ```bash
  v1.0.0-alpha  
  v1.0.0-beta  
  v2.0.0-rc  
  ```

- 修复版本：

  ```bash
  v1.1.2-hotfix.1  
  ```

------

## **未来计划**

- 增加多人联机功能。

------

## **许可证**

 `LICENSE` 
**请阅读 `LICENSE` 文件了解详情。**

------

**感谢你耐心阅读完了这个文档！期待你的反馈与贡献！**

