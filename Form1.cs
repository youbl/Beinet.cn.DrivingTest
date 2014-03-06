using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Beinet.cn.DrivingTest
{
    /*
    目录下的jk子目录，就是试题，请把这个目录拷贝到编译得到的exe所在目录下即可运行
     * 
    文本文件格式如下：
    0、问题(可能包含有[[图片]])
    分隔符
    1、问题图片列表，以逗号分隔
    分隔符
    2、答案序号
    分隔符
    3、答案简要说明
    分隔符
    4、答案选项1
    分隔符
    答案选项2
    分隔符
    答案选项3，对错题没有
    分隔符
    答案选项4，对错题没有
    */
    public partial class Form1 : Form
    {
        readonly string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"jk\{0}");//@"e:\jk\{0}";
        readonly string dirHistory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"history");
        private const string split = "======================";
        public const int quesNum = 973;// 725;    // 题目总数,含大型车的是2540题

        private int answeringNum;           // 保存要回答的题目数（比如回答1~100,这个值就是100）
        Encoding enc = Encoding.GetEncoding("GB2312");
        /// <summary>
        /// 是否随机回答
        /// </summary>
        private bool isRandom;
        /// <summary>
        /// 是否测验
        /// </summary>
        private bool isTest;
        /// <summary>
        /// 是否复习错误题目
        /// </summary>
        private bool isErr;
        private List<int> arrIdx = new List<int>(quesNum);// 保存要回答的题目列表，回答完毕的题目比数组中删除
        Random rnd = new Random(Guid.NewGuid().GetHashCode());// 用于随机取得要回答的题目序号
        List<int> arrErr = new List<int>(); // 统计做错的题目

        private string keyFile;         // 记录每轮答题的记录，用于保存回答历史用
        /// <summary>
        /// 当前回答的题目的索引
        /// </summary>
        private int currentIdx = 1;

        private string currentPic = string.Empty;
        /// <summary>
        /// 当前题目的正确答案
        /// </summary>
        private int currentAnswer;   
        /// <summary>
        /// 当前题目的解释
        /// </summary>
        private string currentAnsDesc;
        /// <summary>
        /// 当前题目是选择题还是判断题
        /// </summary>
        private bool currentIsOption;
        /// <summary>
        /// 当前是正在回答，还是回答完毕(比如答错了，看解释中)
        /// </summary>
        private bool isAnsing;
        Regex regImgFile = new Regex(@"\[\[(.*?)\]\]", RegexOptions.Compiled);

        #region 
        public Form1()
        {
            var tmp = string.Format(fileName, "1.txt");
            if (!File.Exists(tmp))
            {
                MessageBox.Show("考题不存在，程序退出");
                Close();
                return;
            }
            InitializeComponent();
            txtEnd.Text = quesNum.ToString();
            
            // 创建回答历史记录所在的目录
            if (!Directory.Exists(dirHistory))
                Directory.CreateDirectory(dirHistory);

            button1_Click(null, null);

            // 加载历史记录
            var logs = Directory.GetFiles(dirHistory, "*.log", SearchOption.TopDirectoryOnly);
            foreach (var log in logs)
            {
                var item = GetListText(log);
                if(!string.IsNullOrEmpty(item))
                    lstHis.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isRandom = (sender == btnRandom);   // 是否随机
            isErr = (sender == btnErr);     // 是否复习错题
            if (isErr && arrErr.Count<=0)
            {
                MessageBox.Show("你太厉害了，本轮还没出错呢");
                return;
            }
            isTest = (sender == btnTest);     // 是否测验

            init();
            ShowNext();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (isAnsing)
            {
                MessageBox.Show("你还没答题呢");
                return;
            }
            ShowNext();
        }

        private void btnHisLoad_Click(object sender, EventArgs e)
        {
            if (lstHis.SelectedIndex < 0)
            {
                MessageBox.Show("请选择历史记录");
                return;
            }

            var filename = GetListFileName(lstHis.Text);
            if(!File.Exists(filename))
            {
                lstHis.Items.RemoveAt(lstHis.SelectedIndex);
                MessageBox.Show("指定的历史记录不存在，可能已经被删除了");
                return;
            }

            isAnsing = false;
            isTest = false;
            isRandom = false;
            isErr = false;

            int type;
            using(var sr = new StreamReader(filename, enc))
            {
                string line = sr.ReadLine();
                if (line == null)
                    return;
                //清空题目数组并重新填充
                arrIdx.Clear();
                var allQues = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);// 所有题目
                foreach (var que in allQues)
                {
                    arrIdx.Add(int.Parse(que));
                }
                answeringNum = arrIdx.Count;
                if (answeringNum < quesNum)
                {
                    for (var i = answeringNum; i < quesNum; i++)
                    {
                        arrIdx.Add(i);
                    }
                }
                answeringNum = arrIdx.Count;

                // 设置类型
                line = sr.ReadLine();
                if (line == null)
                    return;
                type = int.Parse(line);// 回答类型，1为测验，2为随机答题，3为顺序答题
                if (type == 1) 
                    isTest = true;
                else if (type == 2) 
                    isRandom = true;

                // 收集回答错误的和正确率
                arrErr.Clear();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line == null)
                        return;
                    var content = line.Split(',');//题号,回答,是否正确
                    var errQues = int.Parse(content[0]);
                    if(content[2] != "True")
                    {
                        // 回答错误，加入错误数组
                        arrErr.Add(errQues);
                    }
                    arrIdx.Remove(errQues);
                }
            }

            var right = answeringNum - arrIdx.Count - arrErr.Count;
            labRight.Text = right.ToString();
            labErr.Text = arrErr.Count.ToString();
            labPer.Text = ((float) right*100/(answeringNum - arrIdx.Count)).ToString("N1") + "%";

            labTime.Text = "00:00";
            textBox1.Text = string.Empty;
            rad1.CheckedChanged -= rad_CheckedChanged;
            rad2.CheckedChanged -= rad_CheckedChanged;
            rad3.CheckedChanged -= rad_CheckedChanged;
            rad4.CheckedChanged -= rad_CheckedChanged;

            keyFile = filename;

            ShowNext();
        }

        private void btnHisDel_Click(object sender, EventArgs e)
        {
            if (lstHis.SelectedIndex < 0)
            {
                MessageBox.Show("请选择历史记录");
                return;
            }

            var filename = GetListFileName(lstHis.Text);
            if (!File.Exists(filename))
            {
                lstHis.Items.RemoveAt(lstHis.SelectedIndex);
                MessageBox.Show("指定的历史记录不存在，可能已经被删除了");
                return;
            }
            File.Delete(filename);
        }

        private void btnHisClear_Click(object sender, EventArgs e)
        {
            var logs = Directory.GetFiles(dirHistory, "*.log", SearchOption.TopDirectoryOnly);
            foreach (var log in logs)
            {
                File.Delete(log);
            }
        }

        private string[] opnChar = { "", "A", "B", "C", "D" };
        private string[] rightChar = { "", "对", "错" };
        string GetIdx(int ans, bool isOption)
        {
            if (isOption)
                return opnChar[ans];
            return rightChar[ans];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isAnsing || e.Control || e.Alt || e.Shift)
                return;
            switch (e.KeyCode)
            {
                case Keys.A:
                    rad1.Checked = true;
                    e.Handled = true;
                    break;
                case Keys.B:
                    rad2.Checked = true;
                    e.Handled = true;
                    break;
                case Keys.C:
                    if (currentIsOption)
                    {
                        rad3.Checked = true;
                        e.Handled = true;
                    }
                    break;
                case Keys.D:
                    if (currentIsOption)
                    {
                        rad4.Checked = true;
                        e.Handled = true;
                    }
                    break;
            }
        }

        int GetRnd(int max)
        {
            var ret = rnd.Next(1, max + 1);
            //while (arrOked.ContainsKey(ret))
            //{
            //    ret = rnd.Next(1, quesNum + 1);
            //}
            //arrOked.Add(ret, 0);
            return ret;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var arr = labTime.Text.Split(':');
            int hour, minute, second;
            if(arr.Length == 3)
            {
                hour = int.Parse(arr[0]);
                minute = int.Parse(arr[1]);
                second = int.Parse(arr[2]) + 1;
            }
            else
            {
                hour = 0;
                minute = int.Parse(arr[0]);
                second = int.Parse(arr[1]) + 1;
            }
            if (second > 59)
            {
                second = 0;
                minute++;
                if (minute > 59)
                {
                    minute = 0;
                    hour++;
                }
            }
            if (hour > 0)
                labTime.Text = string.Format("{0:00}:{1:00}", minute, second);
            else
                labTime.Text = string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
        }

        Regex regHis = new Regex(@"^(?:nor|tst|rnd)\d+\.log$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        /// <summary>
        /// 把文件名转换为下拉列表项文本
        /// </summary>
        /// <param name="logfilename"></param>
        /// <returns></returns>
        string GetListText(string logfilename)
        {
            logfilename = Path.GetFileName(logfilename);
            if (logfilename == null)
                return null;
            if(!regHis.IsMatch(logfilename))
                return null;
            logfilename = logfilename.Substring(0, logfilename.Length - 4);

            var ret = logfilename.Substring(3);
            if(logfilename.StartsWith("tst", StringComparison.OrdinalIgnoreCase))
            {
                ret = "测验考试" + ret;
            }
            else if (logfilename.StartsWith("rnd", StringComparison.OrdinalIgnoreCase))
            {
                ret = "随机练习" + ret;
            }
            else// if (logfilename.StartsWith("nor", StringComparison.OrdinalIgnoreCase))
            {
                ret = "顺序练习" + ret;
            }
            return ret;
        }

        /// <summary>
        /// 把下拉列表项文本转换为文件名
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string GetListFileName(string item)
        {
            var ret = item.Substring(4);
            if (item.StartsWith("测验考试", StringComparison.OrdinalIgnoreCase))
            {
                ret = "tst" + ret;
            }
            else if (item.StartsWith("随机练习", StringComparison.OrdinalIgnoreCase))
            {
                ret = "rnd" + ret;
            }
            else// if (item.StartsWith("顺序练习", StringComparison.OrdinalIgnoreCase))
            {
                ret = "nor" + ret;
            }
            return Path.Combine(dirHistory, ret + ".log");
        }

        #region 主要方法，判断对错，并保存当前回答
        /// <summary>
        /// 判断对错的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAnsing) return;

            isAnsing = false;
            rad1.Enabled = false;
            rad2.Enabled = false;
            rad3.Enabled = false;
            rad4.Enabled = false;
            rad1.CheckedChanged -= rad_CheckedChanged;
            rad2.CheckedChanged -= rad_CheckedChanged;
            rad3.CheckedChanged -= rad_CheckedChanged;
            rad4.CheckedChanged -= rad_CheckedChanged;

            textBox1.Text = string.Format("{0}", currentAnsDesc);

            var obj = (RadioButton)sender;
            var myans = int.Parse(obj.Name.Substring(3));
            var right = GetIdx(currentAnswer, currentIsOption);
            bool isright;
            if (myans == currentAnswer)
            {
                // 答对
                labRight.Text = (int.Parse(labRight.Text) + 1).ToString();
                isright = true;
            }
            else
            {
                // 答错
                labErr.Text = (int.Parse(labErr.Text) + 1).ToString();
                if (!isTest)// 测验不显示正确答案
                    MessageBox.Show("你答错了，正确答案是：" + right);
                arrErr.Add(currentIdx);
                isright = false;
            }
            if (!isTest)// 测验不显示说明
                labPer.Text = (float.Parse(labRight.Text) * 100 / (answeringNum - arrIdx.Count)).ToString("N1") + "%";

            // 复习错题不保存
            if (!isErr)
            {
                if (!File.Exists(keyFile))
                {
                    #region 文件不存在时，创建
                    using (var sw = new StreamWriter(keyFile, false, enc))
                    {
                        // 保存全部题目，因为currentIdx已经从数组中移除了，所以要单独保存
                        sw.Write(currentIdx + ",");
                        foreach (var i in arrIdx)
                        {
                            sw.Write(i + ",");
                        }
                        sw.WriteLine();

                        // 保存答题方式
                        if(isTest)
                            sw.WriteLine(1);
                        else if(isRandom)
                            sw.WriteLine(2);
                        else
                            sw.WriteLine(3);
                    }
                    #endregion
                }
                using (var sw = new StreamWriter(keyFile, true, enc))
                {
                    // 格式为：题号,回答,是否正确
                    sw.WriteLine("{0},{1},{2}", currentIdx, myans, isright);
                }
            }

            // 测验时直接显示下一题
            if (isTest || (chkAutoNext.Checked && isright))
                ShowNext();
            btnNext.Focus();
        }
        #endregion

        #region 主要方法，开始时的初始化
        void init()
        {
            // 开始新的一轮前，把当前轮的历史加入列表
            if(!string.IsNullOrEmpty(keyFile) && File.Exists(keyFile))
            {
                lstHis.Items.Insert(0, GetListText(keyFile));
            }

            isAnsing = false;

            // 初始化当前轮文件名（暂不保存到文件，开始回答后才保存）
            keyFile = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (isTest)
                keyFile = "tst" + keyFile;
            else if (isRandom)
                keyFile = "rnd" + keyFile;
            else
                keyFile = "nor" + keyFile;
            keyFile = Path.Combine(dirHistory, keyFile + ".log");

            //清空题目数组并重新填充
            arrIdx.Clear();
            if (isErr)
            {
                arrIdx.AddRange(arrErr);
            }
            else if (isTest)
            {
                #region 测验，从全部题目里随机抽取100题
                for (var i = 0; i < 100; i++)
                {
                    var ret = rnd.Next(1, quesNum + 1);
                    while (arrIdx.Contains(ret))
                    {
                        ret = rnd.Next(1, quesNum + 1);
                    }
                    arrIdx.Add(ret);
                }
                #endregion
            }
            else
            {
                int begin, end;
                if (!int.TryParse(txtBegin.Text, out begin))
                {
                    begin = 1;
                    txtBegin.Text = begin.ToString();
                }
                if (!int.TryParse(txtEnd.Text, out end) || end > quesNum)
                {
                    end = quesNum;
                    txtEnd.Text = end.ToString();
                }
                for (var i = begin - 1; i < end; i++)
                    arrIdx.Add(i + 1);
            }
            answeringNum = arrIdx.Count;
            arrErr.Clear();

            labRight.Text = "0";
            labErr.Text = "0";
            labPer.Text = "0";
            labTime.Text = "00:00";
            textBox1.Text = string.Empty;
            rad1.CheckedChanged -= rad_CheckedChanged;
            rad2.CheckedChanged -= rad_CheckedChanged;
            rad3.CheckedChanged -= rad_CheckedChanged;
            rad4.CheckedChanged -= rad_CheckedChanged;
        }
        #endregion

        #region 主要方法，并显示下一题
        void ShowNext()
        {
            if (arrIdx.Count <= 0)
            {
                MessageBox.Show("已经答完全部题目，正确率：" + labPer.Text);
                return;
            }
            pictureBox1.Visible = false;
            isAnsing = true;

            // 取得当前题目
            var tmpIdx = isRandom ? GetRnd(arrIdx.Count) : 1;
            currentIdx = arrIdx[tmpIdx - 1];
            arrIdx.RemoveAt(tmpIdx - 1);    // 移除正在回答的项，确保随机时回答的数目正确
            labLeft.Text = arrIdx.Count.ToString();
            var file = string.Format(fileName, currentIdx + ".txt");
            if (!File.Exists(file))
            {
                MessageBox.Show("文件不存在：" + file);
                return;
            }

            string txt;
            using (var sr = new StreamReader(file, enc))
            {
                txt = sr.ReadToEnd().Replace("\r\n", "");
            }
            // 拆分文本文件
            var arr = Regex.Split(txt, split);
            labSer.Text = currentIdx + "、";
            var pic = string.Empty;
            var m = regImgFile.Match(arr[0]);
            if (m.Success)
            {
                pic = string.Format(fileName, m.Result("$1"));
                arr[0] = arr[0].Replace(m.Value, "[参考图片]");
            }
            else if (!string.IsNullOrEmpty(arr[1]))
            {
                pic = string.Format(fileName, arr[1].Trim(','));
            }
            if (!string.IsNullOrEmpty(pic))
            {
                pictureBox1.Visible = true;
                var img = Image.FromFile(pic);
                currentPic = pic;
                pictureBox1.Image = img;
                pictureBox1.Width = img.Width;
                pictureBox1.Height = img.Height;
            }
            txtQuestion.Text = arr[0];
            currentAnswer = int.Parse(arr[2]);
            currentAnsDesc = arr[3];
            rad1.Text = arr[4];
            rad2.Text = arr[5];
            if (arr.Length > 7)
            {
                rad3.Text = arr[6];
                rad4.Text = arr[7];
                rad3.Visible = true;
                rad4.Visible = true;
                currentIsOption = true;
            }
            else
            {
                rad1.Text = "对";
                rad2.Text = "错";
                currentIsOption = false;
                rad3.Visible = false;
                rad4.Visible = false;
            }

            // 回答完毕会禁用，所以这里要启用
            rad1.Enabled = true;
            rad2.Enabled = true;
            rad3.Enabled = true;
            rad4.Enabled = true;

            // 这里修改Checked属性，会触发rad_CheckedChanged事件，所以在rad_CheckedChanged事件里要解除事件绑定
            rad1.Checked = false;
            rad2.Checked = false;
            rad3.Checked = false;
            rad4.Checked = false;
            rad1.CheckedChanged += rad_CheckedChanged;
            rad2.CheckedChanged += rad_CheckedChanged;
            rad3.CheckedChanged += rad_CheckedChanged;
            rad4.CheckedChanged += rad_CheckedChanged;
        }
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentPic))
                Process.Start(currentPic);
        }
        #endregion
    }
}
