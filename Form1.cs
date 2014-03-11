using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DrivingTest;

// ReSharper disable once CheckNamespace
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
        #region 属性与变量
        /// <summary>
        /// 考试题目所在目录和图片
        /// </summary>
        internal static readonly string QuestionDir = GetQuestion.QuestionDir;
        /// <summary>
        /// 默认的保存编码格式
        /// </summary>
        internal static Encoding GB2312 = Encoding.GetEncoding("GB2312");
        /// <summary>
        /// 考题里的分隔符，必须与抓取程序一致
        /// </summary>
        internal const string SPLIT_STR = GetQuestion.SPLIT_STR;
        /// <summary>
        /// C1驾照的题目总数,如果要包含大型车，请改成2540题，再去抓取题目
        /// </summary>
        internal const int QUESTION_NUM = GetQuestion.QUESTION_NUM;

        /// <summary>
        /// 模拟测验的总题目数
        /// </summary>
        internal const int EXAM_NUM = 100;

        /// <summary>
        /// 历史记录保存目录
        /// </summary>
        readonly string dirHistory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"history");
        /// <summary>
        /// 考试或随机答题时，用于随机取得要回答的题目序号
        /// </summary>
        static readonly Random rnd = new Random(Guid.NewGuid().GetHashCode());


        /// <summary>
        /// 当前正在回答的问题
        /// </summary>
        private readonly Statuses _status = new Statuses();

        static Regex regImgFile = new Regex(@"\[\[(.*?)\]\]", RegexOptions.Compiled);

        private string[] opnChar = { "", "A", "B", "C", "D" };
        private string[] rightChar = { "", "对", "错" };
        #endregion


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabAnswered == tabControl1.SelectedTab)
            //{
            //    // 选中 已答题目回顾 时，初始化

            //}
        }

        #region 考试窗体操作
        public Form1()
        {
            var tmp = Path.Combine(QuestionDir, "1.txt");
            if (!File.Exists(tmp))
            {
                MessageBox.Show("考题不存在，程序退出");
                Close();
                return;
            }
            InitializeComponent();
            txtEnd.Text = QUESTION_NUM.ToString();
            
            // 创建回答历史记录所在的目录
            if (!Directory.Exists(dirHistory))
                Directory.CreateDirectory(dirHistory);

            button1_Click(null, null);

            // 加载历史记录
            var logs = Directory.GetFiles(dirHistory, "*.log", SearchOption.TopDirectoryOnly);
            // 按文件名倒序加载
            foreach (var log in logs.OrderByDescending(a => a))
            {
                var item = GetListText(log);
                if (!string.IsNullOrEmpty(item))
                {
                    lstHis.Items.Add(item);
                }
            }
            if (lstHis.Items.Count > 0)
            {
                lstHis.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 重新开始答题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (sender == btnRandom) // 是否随机
            {
                _status.ExamType = ExamType.Random;
            }
            else if (sender == btnErr) // 是否复习错题
            {
                _status.IsReviewWrong = true;
                if (!_status.AllWrong.Any())
                {
                    MessageBox.Show("你太厉害了，本轮还没出错呢");
                    return;
                }
            }
            else if (sender == btnTest) // 是否测验
            {
                _status.ExamType = ExamType.Examing;
            }

            init();
            ShowNext();
        }


        private void btnLast_Click(object sender, EventArgs e)
        {
            ShowBefore();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_status.IsAnsing)
            {
                MessageBox.Show("你还没答题呢");
                return;
            }
            ShowNext();
        }


        #region 历史记录操作
        
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

            _status.IsAnsing = false;

            LoadHistory(filename, _status);

            var wrongCount = _status.WrongNum;
            var answerNum = _status.AnswerNum;
            var right = answerNum - wrongCount;
            labRight.Text = right.ToString();
            labErr.Text = wrongCount.ToString();
            labPer.Text = ((float)right * 100 / answerNum).ToString("N1") + "%";
            labLeft.Text = (_status.QuestionNum - answerNum).ToString();

            labTime.Text = "00:00";
            txt1AnswerDesc.Text = string.Empty;

            _status.SaveFile = filename;

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

        Regex regHis = new Regex(@"^(?:nor|tst|rnd).+\.log$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
            if (!regHis.IsMatch(logfilename))
                return null;
            logfilename = logfilename.Substring(0, logfilename.Length - 4);

            var ret = logfilename.Substring(3);
            if (logfilename.StartsWith("tst", StringComparison.OrdinalIgnoreCase))
            {
                ret = ret + "_模拟";
            }
            else if (logfilename.StartsWith("rnd", StringComparison.OrdinalIgnoreCase))
            {
                ret = ret + "_随机";
            }
            else// if (logfilename.StartsWith("nor", StringComparison.OrdinalIgnoreCase))
            {
                ret = ret + "_顺序";
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
            string ret;
            if (item.EndsWith("_模拟", StringComparison.Ordinal))
            {
                ret = "tst";
            }
            else if (item.EndsWith("_随机", StringComparison.Ordinal))
            {
                ret = "rnd";
            }
            else// if (item.EndsWith("_顺序", StringComparison.Ordinal))
            {
                ret = "nor";
            }
            ret += item.Substring(0, item.Length - 3);
            return Path.Combine(dirHistory, ret + ".log");
        }
        #endregion

        string GetIdx(int ans, bool isOption)
        {
            if (isOption)
                return opnChar[ans];
            return rightChar[ans];
        }

        /// <summary>
        /// 支持按钮的ABCD来选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_status.IsAnsing || e.Control || e.Alt || e.Shift)
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
                    if (_status.IsOption)
                    {
                        rad3.Checked = true;
                        e.Handled = true;
                    }
                    break;
                case Keys.D:
                    if (_status.IsOption)
                    {
                        rad4.Checked = true;
                        e.Handled = true;
                    }
                    break;
            }
        }


        /// <summary>
        /// 计时代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region 主要方法，判断对错，并保存当前回答
        /// <summary>
        /// 判断对错的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            if (!_status.IsAnsing)
            {
                return;
            }

            _status.IsAnsing = false;
            rad1.Enabled = false;
            rad2.Enabled = false;
            rad3.Enabled = false;
            rad4.Enabled = false;

            Question currentQuestion = _status.Ques;

            var obj = (RadioButton)sender;
            var myAnswer = int.Parse(obj.Name.Substring(3));
            var right = GetIdx(currentQuestion.ta, _status.IsOption);
            if (myAnswer == currentQuestion.ta)
            {
                // 答对
                labRight.Text = (int.Parse(labRight.Text) + 1).ToString();
            }
            else
            {
                // 答错
                labErr.Text = (int.Parse(labErr.Text) + 1).ToString();
                if (_status.ExamType != ExamType.Examing) // 测验不显示正确答案
                {
                    MessageBox.Show("你答错了，正确答案是：" + right);
                }
            }
            _status.Ques.Answer = myAnswer;

            if (_status.ExamType != ExamType.Examing) // 测验不显示说明
            {
                labPer.Text = (float.Parse(labRight.Text) * 100 / (_status.QuestionNum - int.Parse(labLeft.Text))).ToString("N1") + "%";
                txt1AnswerDesc.Text = currentQuestion.bestanswer;
            }

            // 复习错题不保存
            if (!_status.IsReviewWrong)
            {
                SaveHistory(_status);
            }

            // 测验时直接显示下一题
            if (_status.ExamType == ExamType.Examing || (chkAutoNext.Checked && _status.Ques.IsRight))
            {
                ShowNext();
            }
            btnNext.Focus();
        }
        #endregion

        #region 主要方法，开始时的初始化
        void init()
        {
            string saveFile = _status.SaveFile;
            // 开始新的一轮前，把当前轮的历史加入列表
            if (!string.IsNullOrEmpty(saveFile) && File.Exists(saveFile))
            {
                lstHis.Items.Insert(0, GetListText(saveFile));
            }

            _status.IsAnsing = false;

            // 初始化当前轮文件名（暂不保存到文件，开始回答后才保存）
            saveFile = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");
            if (_status.ExamType == ExamType.Examing)
            {
                saveFile = "tst" + saveFile;
            }
            else if (_status.ExamType == ExamType.Random)
            {
                saveFile = "rnd" + saveFile;
            }
            else //if (_examType == ExamType.Default)
            {
                saveFile = "nor" + saveFile;
            }
            saveFile = Path.Combine(dirHistory, saveFile + ".log");
            _status.SaveFile = saveFile;

            //清空题目数组并重新填充
            _status.AllQuestion.Clear();
            _status.CurrentIdx = -1;
            if (_status.IsReviewWrong)
            {
                _status.AllQuestion.AddRange(_status.AllWrong);
            }
            else if (_status.ExamType == ExamType.Examing)
            {
                #region 测验，从全部题目里随机抽取100题
                HashSet<int> arrExam = new HashSet<int>();
                for (var i = 0; i < EXAM_NUM; i++)
                {
                    var ret = rnd.Next(1, QUESTION_NUM + 1);
                    while (!arrExam.Add(ret))
                    {
                        ret = rnd.Next(1, QUESTION_NUM + 1);
                    }
                    Question ques = LoadQuestion(ret);
                    if (ques == null)
                    {
                        return;
                    }
                    _status.AllQuestion.Add(ques);
                }
                arrExam.Clear();// 快速回收
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
                if (!int.TryParse(txtEnd.Text, out end) || end > QUESTION_NUM)
                {
                    end = QUESTION_NUM;
                    txtEnd.Text = end.ToString();
                }
                if (_status.ExamType == ExamType.Random)
                {
                    #region 随机添加全部考题
                    
                    var all = new List<int>();
                    for (var i = begin; i <= end; i++)
                    {
                        all.Add(i);
                    }
                    while (all.Count > 0)
                    {
                        var ret = rnd.Next(0, all.Count);
                        ret = all[ret];
                        all.Remove(ret);
                        Question ques = LoadQuestion(ret);
                        if (ques == null)
                        {
                            return;
                        }
                        _status.AllQuestion.Add(ques);
                    }
                    #endregion
                }
                else
                {
                    #region 顺序添加全部考题
                    for (var i = begin; i <= end; i++)
                    {
                        Question ques = LoadQuestion(i);
                        if (ques == null)
                        {
                            return;
                        }
                        _status.AllQuestion.Add(ques);
                    }
                    #endregion
                }
            }

            labRight.Text = "0";
            labErr.Text = "0";
            labPer.Text = "0";
            labTime.Text = "00:00";
            labLeft.Text = _status.QuestionNum.ToString();
            txt1AnswerDesc.Text = string.Empty;
        }
        #endregion

        // 主要方法，并显示下一题
        void ShowNext()
        {
            _status.IsAnsing = false;
            pictureBox1.Visible = false;

            bool isRestore = false;// 是否从上一题恢复到考题
            Question question = null;
            if (_status.CurrentIdx > 0)
            {
                question = _status.Ques;
                int showId = GetShowId();
                if (showId != question.id && _status.PrevIdx != _status.CurrentIdx)
                {
                    isRestore = true;
                    _status.PrevIdx++;
                    ShowId(_status.PrevIdx);
                    showId = GetShowId();
                    if (showId != question.id)
                    {
                        return;
                    }
                    if (question.Answer > 0)
                    {
                        // 全部回答完毕了
                        return;
                    }
                }
            }

            if (_status.AllQuestion.Count <= _status.CurrentIdx + 1)
            {
                MessageBox.Show("已经答完全部题目，正确率：" + labPer.Text);
                return;
            }


            // 取得当前题目
            if (!isRestore)
            {
                _status.CurrentIdx++;
                question = _status.Ques;
                labLeft.Text = (int.Parse(labLeft.Text) - 1).ToString();
                labSer.Text = question.id + "、";
                var pic = question.imageurl;
                if (!string.IsNullOrEmpty(pic))
                {
                    ShowImg(pictureBox1, pic);
                }
                txtQuestion.Text = question.question;
                rad1.Text = question.a;
                rad2.Text = question.b;
            }
            _status.PrevIdx = _status.CurrentIdx;// 开始下一题时，点上一题要同时变化

            if (!question.IsOption)
            {
                rad3.Text = question.c;
                rad4.Text = question.d;
                rad3.Visible = true;
                rad4.Visible = true;
                _status.IsOption = true;
            }
            else
            {
                rad1.Text = "对";
                rad2.Text = "错";
                _status.IsOption = false;
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
            //rad1.CheckedChanged += rad_CheckedChanged;
            //rad2.CheckedChanged += rad_CheckedChanged;
            //rad3.CheckedChanged += rad_CheckedChanged;
            //rad4.CheckedChanged += rad_CheckedChanged;

            _status.IsAnsing = true;
        }

        // 显示前一题，主要用于查看历史
        void ShowBefore()
        {
            _status.PrevIdx--;
            if (_status.PrevIdx <= 0)
            {
                MessageBox.Show("已经是第一题了");
                return;
            }
            ShowId(_status.PrevIdx);
        }

        void ShowId(int idx)
        {
            _status.IsAnsing = false;
            pictureBox1.Visible = false;

            Question question = _status.AllQuestion[idx];
            labSer.Text = question.id + "、";
            var pic = question.imageurl;
            if (!string.IsNullOrEmpty(pic))
            {
                ShowImg(pictureBox1, pic);
            }
            txtQuestion.Text = question.question;
            rad1.Text = question.a;
            rad2.Text = question.b;
            if (!question.IsOption)
            {
                rad3.Text = question.c;
                rad4.Text = question.d;
                rad3.Visible = true;
                rad4.Visible = true;
            }
            else
            {
                rad1.Text = "对";
                rad2.Text = "错";
                rad3.Visible = false;
                rad4.Visible = false;
            }
            switch (question.Answer)
            {
                case 1:
                    rad1.Checked = true;
                    break;
                case 2:
                    rad2.Checked = true;
                    break;
                case 3:
                    rad3.Checked = true;
                    break;
                case 4:
                    rad4.Checked = true;
                    break;
            }

            rad1.Enabled = false;
            rad2.Enabled = false;
            rad3.Enabled = false;
            rad4.Enabled = false;
        }

        int GetShowId()
        {
            string str = labSer.Text;
            int showId = int.Parse(str.Substring(0, str.Length - 1));
            return showId;
        }

        private static void SaveHistory(Statuses status)
        {
            string saveFile = status.SaveFile;
            if (!File.Exists(saveFile))
            {
                // 文件不存在时，创建
                using (var sw = new StreamWriter(saveFile, false, GB2312))
                {
                    // 保持空行，兼容旧历史的 保存全部题目 行
                    sw.WriteLine();

                    // 保存答题方式
                    sw.WriteLine((int)status.ExamType);
                }
            }
            Question question = status.Ques;
            using (var sw = new StreamWriter(saveFile, true, GB2312))
            {
                // 格式为：题号,回答,是否正确
                sw.WriteLine("{0},{1},{2}",
                    question.id.ToString(), question.Answer.ToString(), question.IsRight.ToString());
            }
        }

        private static void LoadHistory(string saveFile, Statuses status)
        {
            List<Question> allQuestion = status.AllQuestion;

            //清空题目数组并重新填充
            allQuestion.Clear();
            status.CurrentIdx = -1;

            // 收集已回答的id列表
            HashSet<int> answeredIds = new HashSet<int>();

            #region 加载文件内容
            using (var sr = new StreamReader(saveFile, GB2312))
            {
                // 第一行是所有题目id列表，兼容代码，不使用这些id了
                string line = sr.ReadLine();

                // 第二行是考试类型
                line = sr.ReadLine();
                int typeInt;
                if (line == null || !int.TryParse(line, out typeInt))
                {
                    return;
                }
                status.ExamType = (ExamType)typeInt;
                
                // 第3行开始，是回答完毕的用户答案情况
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line == null)
                    {
                        return;
                    }

                    var content = line.Split(','); //题号,回答,是否正确
                    var quesId = int.Parse(content[0]);

                    if (answeredIds.Add(quesId))
                    {
                        var myAnswer = int.Parse(content[1]);
                        Question question = LoadQuestion(quesId);
                        if (question == null)
                        {
                            return;
                        }

                        question.Answer = myAnswer;
                        allQuestion.Add(question);
                    }
                }
            }
            #endregion

            status.CurrentIdx = allQuestion.Count - 1;
            status.PrevIdx = status.CurrentIdx;
            int totalNum;
            bool isRandom;
            if (status.ExamType == ExamType.Examing)
            {
                totalNum = EXAM_NUM;
                isRandom = true;
            }
            else if (status.ExamType == ExamType.Random)
            {
                totalNum = QUESTION_NUM;
                isRandom = true;
            }
            else
            {
                totalNum = QUESTION_NUM;
                isRandom = false;
            }

            int ret = 0;
            // 考题不足时，补充
            while (allQuestion.Count < totalNum)
            {
                if (isRandom)
                {
                    ret = rnd.Next(1, QUESTION_NUM + 1);
                    while (!answeredIds.Add(ret))
                    {
                        ret = rnd.Next(1, QUESTION_NUM + 1);
                    }
                }
                else
                {
                    ret++;
                    while (!answeredIds.Add(ret))
                    {
                        ret++;
                    }
                }
                Question ques = LoadQuestion(ret);
                if (ques == null)
                {
                    return;
                }
                allQuestion.Add(ques);
            }

            answeredIds.Clear();
        }

        /// <summary>
        /// 根据id，从文件加载问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static Question LoadQuestion(int id)
        {
            var file = Path.Combine(QuestionDir, id.ToString() + ".txt");
            if (!File.Exists(file))
            {
                MessageBox.Show("文件不存在：" + file);
                return null;
            }

            string txt;
            using (var sr = new StreamReader(file, GB2312))
            {
                txt = sr.ReadToEnd().Replace("\r\n", "");
            }
            // 拆分文本文件
            var arr = Regex.Split(txt, SPLIT_STR);
            var pic = string.Empty;
            var m = regImgFile.Match(arr[0]);
            if (m.Success)
            {
                pic = Path.Combine(QuestionDir, m.Result("$1"));
                arr[0] = arr[0].Replace(m.Value, "[参考图片]");
            }
            else if (!string.IsNullOrEmpty(arr[1]))
            {
                pic = Path.Combine(QuestionDir, arr[1].Trim(','));
            }
            var ret = new Question
            {
                id = id,
                imageurl = pic,
                question = arr[0],
                ta = int.Parse(arr[2]),
                bestanswer = arr[3],
                a = arr[4],
                b = arr[5]
            };
            if (arr.Length > 7)
            {
                ret.c = arr[6];
                ret.d = arr[7];
                ret.IsOption = false;
            }
            else
            {
                ret.IsOption = true;
            }
            return ret;
        }
        #endregion



        #region 图片处理方法
        /// <summary>
        /// 点击图片直接打开图片文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bool show = true;
            MouseEventArgs marg = e as MouseEventArgs;
            if (marg != null && marg.Button == MouseButtons.Right)
            {
                show = false;
            }
            if (show)
            {
                var url = _status.Ques.imageurl;

                if (!string.IsNullOrEmpty(url))
                    Process.Start(url);
            }
        }

        // 图片的最大宽高
        private const int _width = 480;
        private const int _height = 400;

        static void ShowImg(PictureBox pictureBox, string picPath)
        {
            using (Bitmap originBmp = new Bitmap(picPath))
            {
                //var originBmp = Image.FromFile(picPath);
                int width = originBmp.Width;
                int height = originBmp.Height;
                if (width > _width || height > _height)
                {
                    float div;
                    int difW = width - _width;
                    int difH = height - _height;
                    if (difW > difH)
                    {
                        div = (float)_width / width;
                    }
                    else
                    {
                        div = (float)_height / height;
                    }
                    width = (int)Math.Floor(width * div);
                    height = (int)Math.Floor(height * div);
                }
                pictureBox.Visible = true;
                pictureBox.Width = width;
                pictureBox.Height = height;

                // 不能dispose，会导致PictureBox崩溃
                Bitmap resizedBmp = new Bitmap(width, height);
                //using (Bitmap resizedBmp = new Bitmap(width, height))
                using (Graphics g = Graphics.FromImage(resizedBmp))
                {
                    //设置高质量插值法   
                    g.InterpolationMode = InterpolationMode.High;
                    //设置高质量,低速度呈现平滑程度   
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    //消除锯齿 
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawImage(originBmp, new Rectangle(0, 0, width, height),
                        new Rectangle(0, 0, originBmp.Width, originBmp.Height),
                        GraphicsUnit.Pixel);
                    //resizedBmp.Save(reSizePicPath, format);
                    pictureBox.Image = resizedBmp;
                }
            }
        }

        ///// <summary> 
        ///// 放大缩小图片尺寸 
        ///// </summary> 
        ///// <param name="picPath"></param> 
        ///// <param name="h"></param> 
        ///// <param name="w"></param> 
        ///// <param name="format"></param> 
        //static void PicReSize(string picPath, int h, int w, ImageFormat format)
        //{
        //    using(Bitmap resizedBmp = new Bitmap(w, h))
        //    using (Graphics g = Graphics.FromImage(resizedBmp))
        //    using (Bitmap originBmp = new Bitmap(picPath))
        //    {
        //        //设置高质量插值法   
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //        //设置高质量,低速度呈现平滑程度   
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        //消除锯齿 
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //        g.DrawImage(originBmp, new Rectangle(0, 0, w, h), new Rectangle(0, 0, originBmp.Width, originBmp.Height),
        //            GraphicsUnit.Pixel);
        //        //resizedBmp.Save(reSizePicPath, format);
        //    }
        //}
        #endregion

    }

    class Statuses
    {
        /// <summary>
        /// 实时保存回答结果的文件路径
        /// </summary>
        public string SaveFile { get; set; }

        /// <summary>
        /// 当前题目是选择题还是判断题
        /// </summary>
        public bool IsOption { get; set; }

        /// <summary>
        /// 当前是正在回答，还是回答完毕(比如答错了，看解释中)
        /// </summary>
        public bool IsAnsing;

        /// <summary>
        /// 初始化保存要回答的题目列表，回答完毕的题目 会从数组中删除
        /// </summary>
        public readonly List<Question> AllQuestion = new List<Question>();
        /// <summary>
        /// 当前回答到AllQuestion里第几个元素
        /// </summary>
        public int CurrentIdx { get; set; }
        /// <summary>
        /// 问题Id
        /// </summary>
        public Question Ques
        {
            get
            {
                return AllQuestion[CurrentIdx];
            }
        }

        /// <summary>
        /// 用于查看上一题的记录
        /// </summary>
        public int PrevIdx { get; set; }

        /// <summary>
        /// 所有问题总数（比如回答1~100,这个值就是100）
        /// </summary>
        public int QuestionNum
        {
            get
            {
                return AllQuestion.Count;
            }
        }

        public ExamType ExamType = ExamType.Default;
        public bool IsReviewWrong = false;    // 是否复习错误题目

        /// <summary>
        /// 返回所有有作答的问题列表
        /// </summary>
        public IEnumerable<Question> AllAnswer
        {
            get
            {
                var wrongData = from item in AllQuestion
                                // 有作答 并且 回答错误
                                where item.Answer > 0
                                select item;
                return wrongData;
            }
        }

        /// <summary>
        /// 返回所有有作答的问题总数
        /// </summary>
        public int AnswerNum
        {
            get
            {
                return AllAnswer.Count();
            }
        }

        /// <summary>
        /// 返回所有有作答且回答错误的问题列表
        /// </summary>
        public IEnumerable<Question> AllWrong
        {
            get
            {
                var wrongData = from item in AllQuestion
                                // 有作答 并且 回答错误
                                where item.Answer > 0 && !item.IsRight
                                select item;
                return wrongData;
            }
        }

        /// <summary>
        /// 返回所有有作答且回答错误的问题总数
        /// </summary>
        public int WrongNum
        {
            get
            {
                return AllWrong.Count();
            }
        }
    }
    
    //1为测验，2为随机答题，3为顺序答题
    enum ExamType
    {
        UnKnown = 0,

        /// <summary>
        /// 默认的顺序全部考试 
        /// </summary>
        Default = 3,

        /// <summary>
        /// 随机全部考试
        /// </summary>
        Random = 2,

        /// <summary>
        /// 模拟考试
        /// </summary>
        Examing = 1
    }
}
