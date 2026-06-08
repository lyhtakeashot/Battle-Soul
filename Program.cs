namespace Battle_Soul
{
    internal static class Program
    {
        // Main
        // 应用程序入口方法
        [STAThread]
        static void Main()
        {
            // 应用配置初始化（例如高 DPI 或默认字体）
            ApplicationConfiguration.Initialize();
            // 运行主窗体
            Application.Run(new BattleSoul());
        }
    }
}