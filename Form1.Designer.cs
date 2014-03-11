namespace Beinet.cn.DrivingTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gpAnswer = new System.Windows.Forms.GroupBox();
            this.rad4 = new System.Windows.Forms.RadioButton();
            this.rad3 = new System.Windows.Forms.RadioButton();
            this.rad2 = new System.Windows.Forms.RadioButton();
            this.rad1 = new System.Windows.Forms.RadioButton();
            this.labSer = new System.Windows.Forms.Label();
            this.labRight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labErr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt1AnswerDesc = new System.Windows.Forms.TextBox();
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOrder = new System.Windows.Forms.Button();
            this.labPer = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAutoNext = new System.Windows.Forms.CheckBox();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnErr = new System.Windows.Forms.Button();
            this.labLeft = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBegin = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.lstHis = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnHisLoad = new System.Windows.Forms.Button();
            this.btnHisDel = new System.Windows.Forms.Button();
            this.btnHisClear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabExam = new System.Windows.Forms.TabPage();
            this.btnLast = new System.Windows.Forms.Button();
            this.tabAnswered = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn2Last = new System.Windows.Forms.Button();
            this.btn2Next = new System.Windows.Forms.Button();
            this.txt2Question = new System.Windows.Forms.TextBox();
            this.txt2AnswerDesc = new System.Windows.Forms.TextBox();
            this.lab2Id = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad2a4 = new System.Windows.Forms.RadioButton();
            this.rad2a3 = new System.Windows.Forms.RadioButton();
            this.rad2a2 = new System.Windows.Forms.RadioButton();
            this.rad2a1 = new System.Windows.Forms.RadioButton();
            this.gpAnswer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabExam.SuspendLayout();
            this.tabAnswered.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpAnswer
            // 
            this.gpAnswer.Controls.Add(this.rad4);
            this.gpAnswer.Controls.Add(this.rad3);
            this.gpAnswer.Controls.Add(this.rad2);
            this.gpAnswer.Controls.Add(this.rad1);
            this.gpAnswer.Location = new System.Drawing.Point(5, 52);
            this.gpAnswer.Name = "gpAnswer";
            this.gpAnswer.Size = new System.Drawing.Size(380, 156);
            this.gpAnswer.TabIndex = 27;
            this.gpAnswer.TabStop = false;
            this.gpAnswer.Text = "选项";
            // 
            // rad4
            // 
            this.rad4.AutoSize = true;
            this.rad4.Location = new System.Drawing.Point(6, 123);
            this.rad4.Name = "rad4";
            this.rad4.Size = new System.Drawing.Size(95, 16);
            this.rad4.TabIndex = 14;
            this.rad4.Text = "radioButton1";
            this.rad4.UseVisualStyleBackColor = true;
            this.rad4.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // rad3
            // 
            this.rad3.AutoSize = true;
            this.rad3.Location = new System.Drawing.Point(6, 90);
            this.rad3.Name = "rad3";
            this.rad3.Size = new System.Drawing.Size(95, 16);
            this.rad3.TabIndex = 13;
            this.rad3.Text = "radioButton1";
            this.rad3.UseVisualStyleBackColor = true;
            this.rad3.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // rad2
            // 
            this.rad2.AutoSize = true;
            this.rad2.Location = new System.Drawing.Point(6, 57);
            this.rad2.Name = "rad2";
            this.rad2.Size = new System.Drawing.Size(95, 16);
            this.rad2.TabIndex = 12;
            this.rad2.Text = "radioButton1";
            this.rad2.UseVisualStyleBackColor = true;
            this.rad2.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // rad1
            // 
            this.rad1.AutoSize = true;
            this.rad1.Location = new System.Drawing.Point(6, 24);
            this.rad1.Name = "rad1";
            this.rad1.Size = new System.Drawing.Size(95, 16);
            this.rad1.TabIndex = 11;
            this.rad1.Text = "radioButton1";
            this.rad1.UseVisualStyleBackColor = true;
            this.rad1.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // labSer
            // 
            this.labSer.AutoSize = true;
            this.labSer.Location = new System.Drawing.Point(7, 8);
            this.labSer.Name = "labSer";
            this.labSer.Size = new System.Drawing.Size(29, 12);
            this.labSer.TabIndex = 26;
            this.labSer.Text = "725.";
            // 
            // labRight
            // 
            this.labRight.AutoSize = true;
            this.labRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRight.ForeColor = System.Drawing.Color.Red;
            this.labRight.Location = new System.Drawing.Point(36, 217);
            this.labRight.Name = "labRight";
            this.labRight.Size = new System.Drawing.Size(26, 12);
            this.labRight.TabIndex = 25;
            this.labRight.Text = "725";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "正确：";
            // 
            // labErr
            // 
            this.labErr.AutoSize = true;
            this.labErr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labErr.ForeColor = System.Drawing.Color.Red;
            this.labErr.Location = new System.Drawing.Point(37, 244);
            this.labErr.Name = "labErr";
            this.labErr.Size = new System.Drawing.Size(26, 12);
            this.labErr.TabIndex = 24;
            this.labErr.Text = "725";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "错误：";
            // 
            // txt1AnswerDesc
            // 
            this.txt1AnswerDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt1AnswerDesc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt1AnswerDesc.Location = new System.Drawing.Point(8, 378);
            this.txt1AnswerDesc.Multiline = true;
            this.txt1AnswerDesc.Name = "txt1AnswerDesc";
            this.txt1AnswerDesc.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt1AnswerDesc.Size = new System.Drawing.Size(384, 87);
            this.txt1AnswerDesc.TabIndex = 10;
            this.txt1AnswerDesc.Text = "答案说明";
            // 
            // btnRandom
            // 
            this.btnRandom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRandom.Location = new System.Drawing.Point(159, 287);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(122, 32);
            this.btnRandom.TabIndex = 9;
            this.btnRandom.Text = "重新开始随机答题";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.Location = new System.Drawing.Point(286, 217);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(99, 42);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "下一题";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(396, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(486, 404);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(19, 287);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(119, 32);
            this.btnOrder.TabIndex = 8;
            this.btnOrder.Text = "重新开始顺序答题";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.button1_Click);
            // 
            // labPer
            // 
            this.labPer.AutoSize = true;
            this.labPer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPer.ForeColor = System.Drawing.Color.Red;
            this.labPer.Location = new System.Drawing.Point(47, 270);
            this.labPer.Name = "labPer";
            this.labPer.Size = new System.Drawing.Size(47, 12);
            this.labPer.TabIndex = 22;
            this.labPer.Text = "100.0%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "正确率：";
            // 
            // chkAutoNext
            // 
            this.chkAutoNext.AutoSize = true;
            this.chkAutoNext.Checked = true;
            this.chkAutoNext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoNext.Location = new System.Drawing.Point(144, 216);
            this.chkAutoNext.Name = "chkAutoNext";
            this.chkAutoNext.Size = new System.Drawing.Size(120, 16);
            this.chkAutoNext.TabIndex = 8;
            this.chkAutoNext.TabStop = false;
            this.chkAutoNext.Text = "正确时自动下一题";
            this.chkAutoNext.UseVisualStyleBackColor = true;
            // 
            // txtQuestion
            // 
            this.txtQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuestion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtQuestion.Location = new System.Drawing.Point(37, 9);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtQuestion.Size = new System.Drawing.Size(847, 46);
            this.txtQuestion.TabIndex = 13;
            this.txtQuestion.TabStop = false;
            this.txtQuestion.Text = "显示问题";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(103, 217);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(35, 12);
            this.labTime.TabIndex = 6;
            this.labTime.Text = "00:00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "计时：";
            // 
            // btnErr
            // 
            this.btnErr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnErr.Location = new System.Drawing.Point(287, 353);
            this.btnErr.Name = "btnErr";
            this.btnErr.Size = new System.Drawing.Size(99, 23);
            this.btnErr.TabIndex = 6;
            this.btnErr.Text = "复习本轮错题";
            this.btnErr.UseVisualStyleBackColor = true;
            this.btnErr.Click += new System.EventHandler(this.button1_Click);
            // 
            // labLeft
            // 
            this.labLeft.AutoSize = true;
            this.labLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLeft.ForeColor = System.Drawing.Color.Red;
            this.labLeft.Location = new System.Drawing.Point(165, 270);
            this.labLeft.Name = "labLeft";
            this.labLeft.Size = new System.Drawing.Size(26, 12);
            this.labLeft.TabIndex = 7;
            this.labLeft.Text = "725";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 270);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "剩余题数：";
            // 
            // txtBegin
            // 
            this.txtBegin.Location = new System.Drawing.Point(133, 235);
            this.txtBegin.Name = "txtBegin";
            this.txtBegin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBegin.Size = new System.Drawing.Size(35, 21);
            this.txtBegin.TabIndex = 5;
            this.txtBegin.Text = "1";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(192, 235);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEnd.Size = new System.Drawing.Size(42, 21);
            this.txtEnd.TabIndex = 4;
            this.txtEnd.Text = "725";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "答题区间：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(174, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "到";
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.ForeColor = System.Drawing.Color.Firebrick;
            this.btnTest.Location = new System.Drawing.Point(287, 323);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(99, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "考试测验";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstHis
            // 
            this.lstHis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstHis.FormattingEnabled = true;
            this.lstHis.Location = new System.Drawing.Point(62, 323);
            this.lstHis.Name = "lstHis";
            this.lstHis.Size = new System.Drawing.Size(219, 20);
            this.lstHis.TabIndex = 0;
            this.lstHis.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 327);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "答题历史：";
            // 
            // btnHisLoad
            // 
            this.btnHisLoad.Location = new System.Drawing.Point(17, 352);
            this.btnHisLoad.Name = "btnHisLoad";
            this.btnHisLoad.Size = new System.Drawing.Size(69, 23);
            this.btnHisLoad.TabIndex = 12;
            this.btnHisLoad.TabStop = false;
            this.btnHisLoad.Text = "加载历史";
            this.btnHisLoad.UseVisualStyleBackColor = true;
            this.btnHisLoad.Click += new System.EventHandler(this.btnHisLoad_Click);
            // 
            // btnHisDel
            // 
            this.btnHisDel.Location = new System.Drawing.Point(95, 352);
            this.btnHisDel.Name = "btnHisDel";
            this.btnHisDel.Size = new System.Drawing.Size(73, 23);
            this.btnHisDel.TabIndex = 11;
            this.btnHisDel.TabStop = false;
            this.btnHisDel.Text = "删除历史";
            this.btnHisDel.UseVisualStyleBackColor = true;
            this.btnHisDel.Click += new System.EventHandler(this.btnHisDel_Click);
            // 
            // btnHisClear
            // 
            this.btnHisClear.Location = new System.Drawing.Point(176, 352);
            this.btnHisClear.Name = "btnHisClear";
            this.btnHisClear.Size = new System.Drawing.Size(85, 23);
            this.btnHisClear.TabIndex = 10;
            this.btnHisClear.TabStop = false;
            this.btnHisClear.Text = "清空全部历史";
            this.btnHisClear.UseVisualStyleBackColor = true;
            this.btnHisClear.Click += new System.EventHandler(this.btnHisClear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabExam);
            this.tabControl1.Controls.Add(this.tabAnswered);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(896, 494);
            this.tabControl1.TabIndex = 28;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabExam
            // 
            this.tabExam.Controls.Add(this.txtQuestion);
            this.tabExam.Controls.Add(this.gpAnswer);
            this.tabExam.Controls.Add(this.pictureBox1);
            this.tabExam.Controls.Add(this.labTime);
            this.tabExam.Controls.Add(this.labLeft);
            this.tabExam.Controls.Add(this.labPer);
            this.tabExam.Controls.Add(this.labErr);
            this.tabExam.Controls.Add(this.labRight);
            this.tabExam.Controls.Add(this.lstHis);
            this.tabExam.Controls.Add(this.txtEnd);
            this.tabExam.Controls.Add(this.txtBegin);
            this.tabExam.Controls.Add(this.chkAutoNext);
            this.tabExam.Controls.Add(this.btnHisClear);
            this.tabExam.Controls.Add(this.btnHisDel);
            this.tabExam.Controls.Add(this.btnHisLoad);
            this.tabExam.Controls.Add(this.btnLast);
            this.tabExam.Controls.Add(this.btnNext);
            this.tabExam.Controls.Add(this.btnErr);
            this.tabExam.Controls.Add(this.btnOrder);
            this.tabExam.Controls.Add(this.btnTest);
            this.tabExam.Controls.Add(this.btnRandom);
            this.tabExam.Controls.Add(this.txt1AnswerDesc);
            this.tabExam.Controls.Add(this.label7);
            this.tabExam.Controls.Add(this.label6);
            this.tabExam.Controls.Add(this.label5);
            this.tabExam.Controls.Add(this.label8);
            this.tabExam.Controls.Add(this.label4);
            this.tabExam.Controls.Add(this.label3);
            this.tabExam.Controls.Add(this.label1);
            this.tabExam.Controls.Add(this.labSer);
            this.tabExam.Controls.Add(this.label9);
            this.tabExam.Location = new System.Drawing.Point(4, 22);
            this.tabExam.Name = "tabExam";
            this.tabExam.Padding = new System.Windows.Forms.Padding(3);
            this.tabExam.Size = new System.Drawing.Size(888, 468);
            this.tabExam.TabIndex = 0;
            this.tabExam.Text = "考试";
            this.tabExam.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(286, 270);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(99, 37);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一题";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // tabAnswered
            // 
            this.tabAnswered.Controls.Add(this.pictureBox2);
            this.tabAnswered.Controls.Add(this.btn2Last);
            this.tabAnswered.Controls.Add(this.btn2Next);
            this.tabAnswered.Controls.Add(this.txt2Question);
            this.tabAnswered.Controls.Add(this.txt2AnswerDesc);
            this.tabAnswered.Controls.Add(this.lab2Id);
            this.tabAnswered.Controls.Add(this.groupBox1);
            this.tabAnswered.Location = new System.Drawing.Point(4, 22);
            this.tabAnswered.Name = "tabAnswered";
            this.tabAnswered.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnswered.Size = new System.Drawing.Size(888, 468);
            this.tabAnswered.TabIndex = 1;
            this.tabAnswered.Text = "已答题目回顾";
            this.tabAnswered.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(398, 59);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(486, 404);
            this.pictureBox2.TabIndex = 39;
            this.pictureBox2.TabStop = false;
            // 
            // btn2Last
            // 
            this.btn2Last.Location = new System.Drawing.Point(18, 219);
            this.btn2Last.Name = "btn2Last";
            this.btn2Last.Size = new System.Drawing.Size(119, 42);
            this.btn2Last.TabIndex = 29;
            this.btn2Last.Text = "前一题";
            this.btn2Last.UseVisualStyleBackColor = true;
            // 
            // btn2Next
            // 
            this.btn2Next.Location = new System.Drawing.Point(267, 220);
            this.btn2Next.Name = "btn2Next";
            this.btn2Next.Size = new System.Drawing.Size(119, 42);
            this.btn2Next.TabIndex = 29;
            this.btn2Next.Text = "后一题";
            this.btn2Next.UseVisualStyleBackColor = true;
            // 
            // txt2Question
            // 
            this.txt2Question.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt2Question.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt2Question.Location = new System.Drawing.Point(37, 7);
            this.txt2Question.Multiline = true;
            this.txt2Question.Name = "txt2Question";
            this.txt2Question.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt2Question.Size = new System.Drawing.Size(847, 46);
            this.txt2Question.TabIndex = 44;
            this.txt2Question.TabStop = false;
            this.txt2Question.Text = "显示问题";
            // 
            // txt2AnswerDesc
            // 
            this.txt2AnswerDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt2AnswerDesc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt2AnswerDesc.Location = new System.Drawing.Point(8, 268);
            this.txt2AnswerDesc.Multiline = true;
            this.txt2AnswerDesc.Name = "txt2AnswerDesc";
            this.txt2AnswerDesc.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt2AnswerDesc.Size = new System.Drawing.Size(384, 195);
            this.txt2AnswerDesc.TabIndex = 41;
            this.txt2AnswerDesc.Text = "答案说明";
            // 
            // lab2Id
            // 
            this.lab2Id.AutoSize = true;
            this.lab2Id.Location = new System.Drawing.Point(7, 6);
            this.lab2Id.Name = "lab2Id";
            this.lab2Id.Size = new System.Drawing.Size(29, 12);
            this.lab2Id.TabIndex = 56;
            this.lab2Id.Text = "725.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rad2a4);
            this.groupBox1.Controls.Add(this.rad2a3);
            this.groupBox1.Controls.Add(this.rad2a2);
            this.groupBox1.Controls.Add(this.rad2a1);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 156);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "答案选项";
            // 
            // rad2a4
            // 
            this.rad2a4.AutoSize = true;
            this.rad2a4.Enabled = false;
            this.rad2a4.Location = new System.Drawing.Point(6, 123);
            this.rad2a4.Name = "rad2a4";
            this.rad2a4.Size = new System.Drawing.Size(95, 16);
            this.rad2a4.TabIndex = 14;
            this.rad2a4.Text = "radioButton1";
            this.rad2a4.UseVisualStyleBackColor = true;
            // 
            // rad2a3
            // 
            this.rad2a3.AutoSize = true;
            this.rad2a3.Enabled = false;
            this.rad2a3.Location = new System.Drawing.Point(6, 90);
            this.rad2a3.Name = "rad2a3";
            this.rad2a3.Size = new System.Drawing.Size(95, 16);
            this.rad2a3.TabIndex = 13;
            this.rad2a3.Text = "radioButton1";
            this.rad2a3.UseVisualStyleBackColor = true;
            // 
            // rad2a2
            // 
            this.rad2a2.AutoSize = true;
            this.rad2a2.Enabled = false;
            this.rad2a2.Location = new System.Drawing.Point(6, 57);
            this.rad2a2.Name = "rad2a2";
            this.rad2a2.Size = new System.Drawing.Size(95, 16);
            this.rad2a2.TabIndex = 12;
            this.rad2a2.Text = "radioButton1";
            this.rad2a2.UseVisualStyleBackColor = true;
            // 
            // rad2a1
            // 
            this.rad2a1.AutoSize = true;
            this.rad2a1.Enabled = false;
            this.rad2a1.Location = new System.Drawing.Point(6, 24);
            this.rad2a1.Name = "rad2a1";
            this.rad2a1.Size = new System.Drawing.Size(95, 16);
            this.rad2a1.TabIndex = 11;
            this.rad2a1.Text = "radioButton1";
            this.rad2a1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 494);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "驾考科目一考题";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.gpAnswer.ResumeLayout(false);
            this.gpAnswer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabExam.ResumeLayout(false);
            this.tabExam.PerformLayout();
            this.tabAnswered.ResumeLayout(false);
            this.tabAnswered.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpAnswer;
        private System.Windows.Forms.Label labSer;
        private System.Windows.Forms.Label labRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labErr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt1AnswerDesc;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.RadioButton rad4;
        private System.Windows.Forms.RadioButton rad3;
        private System.Windows.Forms.RadioButton rad2;
        private System.Windows.Forms.RadioButton rad1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Label labPer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAutoNext;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnErr;
        private System.Windows.Forms.Label labLeft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBegin;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox lstHis;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnHisLoad;
        private System.Windows.Forms.Button btnHisDel;
        private System.Windows.Forms.Button btnHisClear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabExam;
        private System.Windows.Forms.TabPage tabAnswered;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btn2Next;
        private System.Windows.Forms.TextBox txt2Question;
        private System.Windows.Forms.TextBox txt2AnswerDesc;
        private System.Windows.Forms.Label lab2Id;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rad2a4;
        private System.Windows.Forms.RadioButton rad2a3;
        private System.Windows.Forms.RadioButton rad2a2;
        private System.Windows.Forms.RadioButton rad2a1;
        private System.Windows.Forms.Button btn2Last;
        private System.Windows.Forms.Button btnLast;

    }
}

