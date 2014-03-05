using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DrivingTest;

namespace Beinet.cn.DrivingTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 启动题目抓取程序
            //Application.Run(new GetQuestion());

            // 启动考试程序
            Application.Run(new Form1());
        }
    }
}
