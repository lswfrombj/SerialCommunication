namespace SerialCommunication
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBeginWork = new System.Windows.Forms.Button();
            this.picbx = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.cbxCOMPort = new System.Windows.Forms.ComboBox();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.btnCheckCOM = new System.Windows.Forms.Button();
            this.rbnHex = new System.Windows.Forms.RadioButton();
            this.rbnChar = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxRecvData = new System.Windows.Forms.TextBox();
            this.btnCleanRecData = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxRecvTnum = new System.Windows.Forms.TextBox();
            this.btnMovSheBei = new System.Windows.Forms.Button();
            this.btnSetPath = new System.Windows.Forms.Button();
            this.zgc = new ZedGraph.ZedGraphControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.clockPanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxTime = new System.Windows.Forms.TextBox();
            this.tbxWater = new System.Windows.Forms.TextBox();
            this.tbxState = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbxWarn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.OpenButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.SomeZhnTButton = new System.Windows.Forms.Button();
            this.BehindwardTButton = new System.Windows.Forms.Button();
            this.ForwardTButton = new System.Windows.Forms.Button();
            this.cbxTLLStep = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.ReapTButton = new System.Windows.Forms.Button();
            this.ClearTButton = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxQTGS = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbxDSGS = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbxXSGS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxMSGS = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxResultPos2 = new System.Windows.Forms.TextBox();
            this.tbxResult2 = new System.Windows.Forms.TextBox();
            this.picbxRes1 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbxResultPos1 = new System.Windows.Forms.TextBox();
            this.tbxResult1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxWorkTime = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cbxWorkPos = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cbxLineStep = new System.Windows.Forms.ComboBox();
            this.cbxLineWidth = new System.Windows.Forms.ComboBox();
            this.cbxLineNumber = new System.Windows.Forms.ComboBox();
            this.cbxWorkMode = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.sp = new System.IO.Ports.SerialPort(this.components);
            this.picbxLogo = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.picbxHead1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbx)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxRes1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxHead1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBeginWork);
            this.groupBox1.Controls.Add(this.picbx);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cbxDataBits);
            this.groupBox1.Controls.Add(this.cbxParity);
            this.groupBox1.Controls.Add(this.cbxStopBits);
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.cbxCOMPort);
            this.groupBox1.Controls.Add(this.btnOpenCom);
            this.groupBox1.Controls.Add(this.btnCheckCOM);
            this.groupBox1.Controls.Add(this.rbnHex);
            this.groupBox1.Controls.Add(this.rbnChar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(4, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(360, 146);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnBeginWork
            // 
            this.btnBeginWork.Location = new System.Drawing.Point(256, 107);
            this.btnBeginWork.Name = "btnBeginWork";
            this.btnBeginWork.Size = new System.Drawing.Size(79, 27);
            this.btnBeginWork.TabIndex = 16;
            this.btnBeginWork.Text = "启动接收";
            this.btnBeginWork.Click += new System.EventHandler(this.btnBeginWork_Click);
            // 
            // picbx
            // 
            this.picbx.Location = new System.Drawing.Point(17, 35);
            this.picbx.Name = "picbx";
            this.picbx.Size = new System.Drawing.Size(40, 40);
            this.picbx.TabIndex = 15;
            this.picbx.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(71, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 14);
            this.label16.TabIndex = 14;
            this.label16.Text = "波特率:";
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(131, 74);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(65, 22);
            this.cbxDataBits.TabIndex = 13;
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(276, 22);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(65, 22);
            this.cbxParity.TabIndex = 12;
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(276, 48);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(65, 22);
            this.cbxStopBits.TabIndex = 11;
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(131, 48);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(65, 22);
            this.cbxBaudRate.TabIndex = 10;
            // 
            // cbxCOMPort
            // 
            this.cbxCOMPort.FormattingEnabled = true;
            this.cbxCOMPort.Location = new System.Drawing.Point(131, 22);
            this.cbxCOMPort.Name = "cbxCOMPort";
            this.cbxCOMPort.Size = new System.Drawing.Size(65, 22);
            this.cbxCOMPort.TabIndex = 9;
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Location = new System.Drawing.Point(143, 107);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(79, 27);
            this.btnOpenCom.TabIndex = 8;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // btnCheckCOM
            // 
            this.btnCheckCOM.Location = new System.Drawing.Point(30, 107);
            this.btnCheckCOM.Name = "btnCheckCOM";
            this.btnCheckCOM.Size = new System.Drawing.Size(79, 27);
            this.btnCheckCOM.TabIndex = 7;
            this.btnCheckCOM.Text = "串口检测";
            this.btnCheckCOM.UseVisualStyleBackColor = true;
            this.btnCheckCOM.Click += new System.EventHandler(this.btnCheckCOM_Click);
            // 
            // rbnHex
            // 
            this.rbnHex.AutoSize = true;
            this.rbnHex.Location = new System.Drawing.Point(203, 75);
            this.rbnHex.Name = "rbnHex";
            this.rbnHex.Size = new System.Drawing.Size(74, 18);
            this.rbnHex.TabIndex = 6;
            this.rbnHex.Text = "HEX显示";
            this.rbnHex.UseVisualStyleBackColor = true;
            this.rbnHex.CheckedChanged += new System.EventHandler(this.rbnHex_CheckedChanged);
            // 
            // rbnChar
            // 
            this.rbnChar.AutoSize = true;
            this.rbnChar.Checked = true;
            this.rbnChar.Location = new System.Drawing.Point(281, 75);
            this.rbnChar.Margin = new System.Windows.Forms.Padding(1);
            this.rbnChar.Name = "rbnChar";
            this.rbnChar.Size = new System.Drawing.Size(81, 18);
            this.rbnChar.TabIndex = 5;
            this.rbnChar.TabStop = true;
            this.rbnChar.Text = "字符显示";
            this.rbnChar.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "数据位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "奇偶校验:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "停止位:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxRecvData);
            this.groupBox2.Controls.Add(this.btnCleanRecData);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.tbxRecvTnum);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(368, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 301);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据消息显示";
            // 
            // tbxRecvData
            // 
            this.tbxRecvData.AllowDrop = true;
            this.tbxRecvData.Location = new System.Drawing.Point(6, 22);
            this.tbxRecvData.Multiline = true;
            this.tbxRecvData.Name = "tbxRecvData";
            this.tbxRecvData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxRecvData.Size = new System.Drawing.Size(183, 204);
            this.tbxRecvData.TabIndex = 0;
            // 
            // btnCleanRecData
            // 
            this.btnCleanRecData.Location = new System.Drawing.Point(104, 264);
            this.btnCleanRecData.Name = "btnCleanRecData";
            this.btnCleanRecData.Size = new System.Drawing.Size(79, 27);
            this.btnCleanRecData.TabIndex = 5;
            this.btnCleanRecData.Text = "清空数据";
            this.btnCleanRecData.UseVisualStyleBackColor = true;
            this.btnCleanRecData.Click += new System.EventHandler(this.btnCleanRecData_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(13, 264);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(79, 27);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "发送数据";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxRecvTnum
            // 
            this.tbxRecvTnum.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tbxRecvTnum.Location = new System.Drawing.Point(5, 232);
            this.tbxRecvTnum.Multiline = true;
            this.tbxRecvTnum.Name = "tbxRecvTnum";
            this.tbxRecvTnum.ReadOnly = true;
            this.tbxRecvTnum.Size = new System.Drawing.Size(183, 22);
            this.tbxRecvTnum.TabIndex = 6;
            // 
            // btnMovSheBei
            // 
            this.btnMovSheBei.Location = new System.Drawing.Point(233, 109);
            this.btnMovSheBei.Name = "btnMovSheBei";
            this.btnMovSheBei.Size = new System.Drawing.Size(79, 27);
            this.btnMovSheBei.TabIndex = 8;
            this.btnMovSheBei.Text = "调整工位";
            this.btnMovSheBei.UseVisualStyleBackColor = true;
            this.btnMovSheBei.Click += new System.EventHandler(this.btnMovSheBei_Click);
            // 
            // btnSetPath
            // 
            this.btnSetPath.Location = new System.Drawing.Point(74, 109);
            this.btnSetPath.Name = "btnSetPath";
            this.btnSetPath.Size = new System.Drawing.Size(79, 27);
            this.btnSetPath.TabIndex = 7;
            this.btnSetPath.Text = "设置路径";
            this.btnSetPath.UseVisualStyleBackColor = true;
            this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
            // 
            // zgc
            // 
            this.zgc.Font = new System.Drawing.Font("宋体", 5F);
            this.zgc.Location = new System.Drawing.Point(9, 38);
            this.zgc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.zgc.Name = "zgc";
            this.zgc.ScrollGrace = 0D;
            this.zgc.ScrollMaxX = 0D;
            this.zgc.ScrollMaxY = 0D;
            this.zgc.ScrollMaxY2 = 0D;
            this.zgc.ScrollMinX = 0D;
            this.zgc.ScrollMinY = 0D;
            this.zgc.ScrollMinY2 = 0D;
            this.zgc.Size = new System.Drawing.Size(1098, 263);
            this.zgc.TabIndex = 9;
            this.zgc.Load += new System.EventHandler(this.zgc_Load);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // clockPanel
            // 
            this.clockPanel.Location = new System.Drawing.Point(934, 100);
            this.clockPanel.Name = "clockPanel";
            this.clockPanel.Size = new System.Drawing.Size(180, 180);
            this.clockPanel.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = "吊索号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(322, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 14);
            this.label10.TabIndex = 13;
            this.label10.Text = "当前帧";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(436, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 14;
            this.label11.Text = "报警位置";
            // 
            // tbxTime
            // 
            this.tbxTime.Location = new System.Drawing.Point(49, 37);
            this.tbxTime.Name = "tbxTime";
            this.tbxTime.ReadOnly = true;
            this.tbxTime.Size = new System.Drawing.Size(92, 23);
            this.tbxTime.TabIndex = 15;
            // 
            // tbxWater
            // 
            this.tbxWater.Location = new System.Drawing.Point(297, 37);
            this.tbxWater.Name = "tbxWater";
            this.tbxWater.ReadOnly = true;
            this.tbxWater.Size = new System.Drawing.Size(92, 23);
            this.tbxWater.TabIndex = 16;
            // 
            // tbxState
            // 
            this.tbxState.Location = new System.Drawing.Point(421, 37);
            this.tbxState.Name = "tbxState";
            this.tbxState.ReadOnly = true;
            this.tbxState.Size = new System.Drawing.Size(92, 23);
            this.tbxState.TabIndex = 17;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbxWarn);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.tbxWater);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.tbxState);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.tbxTime);
            this.groupBox5.Location = new System.Drawing.Point(571, 321);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(547, 74);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "工作信息显示";
            // 
            // tbxWarn
            // 
            this.tbxWarn.Location = new System.Drawing.Point(173, 37);
            this.tbxWarn.Name = "tbxWarn";
            this.tbxWarn.ReadOnly = true;
            this.tbxWarn.Size = new System.Drawing.Size(92, 23);
            this.tbxWarn.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 1;
            this.label6.Text = "检测通道 ";
            // 
            // OpenButton
            // 
            this.OpenButton.Font = new System.Drawing.Font("宋体", 10.5F);
            this.OpenButton.Location = new System.Drawing.Point(893, 14);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(85, 22);
            this.OpenButton.TabIndex = 20;
            this.OpenButton.Text = "导入数据";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.SomeZhnTButton);
            this.groupBox7.Controls.Add(this.BehindwardTButton);
            this.groupBox7.Controls.Add(this.ForwardTButton);
            this.groupBox7.Controls.Add(this.cbxTLLStep);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.ReapTButton);
            this.groupBox7.Controls.Add(this.ClearTButton);
            this.groupBox7.Controls.Add(this.checkBox2);
            this.groupBox7.Controls.Add(this.checkBox1);
            this.groupBox7.Controls.Add(this.zgc);
            this.groupBox7.Controls.Add(this.OpenButton);
            this.groupBox7.Location = new System.Drawing.Point(4, 401);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1114, 310);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "实时监测曲线";
            // 
            // SomeZhnTButton
            // 
            this.SomeZhnTButton.Font = new System.Drawing.Font("宋体", 10.5F);
            this.SomeZhnTButton.Location = new System.Drawing.Point(496, 14);
            this.SomeZhnTButton.Name = "SomeZhnTButton";
            this.SomeZhnTButton.Size = new System.Drawing.Size(85, 22);
            this.SomeZhnTButton.TabIndex = 25;
            this.SomeZhnTButton.Text = "指定帧浏览";
            this.SomeZhnTButton.UseVisualStyleBackColor = true;
            this.SomeZhnTButton.Click += new System.EventHandler(this.SomeZhnTButton_Click);
            // 
            // BehindwardTButton
            // 
            this.BehindwardTButton.Font = new System.Drawing.Font("宋体", 10.5F);
            this.BehindwardTButton.Location = new System.Drawing.Point(587, 14);
            this.BehindwardTButton.Name = "BehindwardTButton";
            this.BehindwardTButton.Size = new System.Drawing.Size(85, 22);
            this.BehindwardTButton.TabIndex = 24;
            this.BehindwardTButton.Text = "往后浏览";
            this.BehindwardTButton.UseVisualStyleBackColor = true;
            this.BehindwardTButton.Click += new System.EventHandler(this.BehindwardTButton_Click);
            // 
            // ForwardTButton
            // 
            this.ForwardTButton.Font = new System.Drawing.Font("宋体", 10.5F);
            this.ForwardTButton.Location = new System.Drawing.Point(406, 14);
            this.ForwardTButton.Name = "ForwardTButton";
            this.ForwardTButton.Size = new System.Drawing.Size(72, 22);
            this.ForwardTButton.TabIndex = 23;
            this.ForwardTButton.Text = "往前浏览";
            this.ForwardTButton.UseVisualStyleBackColor = true;
            this.ForwardTButton.Click += new System.EventHandler(this.ForwardTButton_Click);
            // 
            // cbxTLLStep
            // 
            this.cbxTLLStep.FormattingEnabled = true;
            this.cbxTLLStep.Items.AddRange(new object[] {
            "50",
            "500",
            "2500",
            "4000",
            "5000",
            "10000",
            "20000",
            "25000",
            "50000"});
            this.cbxTLLStep.Location = new System.Drawing.Point(308, 14);
            this.cbxTLLStep.Name = "cbxTLLStep";
            this.cbxTLLStep.Size = new System.Drawing.Size(86, 22);
            this.cbxTLLStep.TabIndex = 18;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(170, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(133, 14);
            this.label26.TabIndex = 17;
            this.label26.Text = "浏览步长/指定位置:";
            // 
            // ReapTButton
            // 
            this.ReapTButton.Font = new System.Drawing.Font("宋体", 10.5F);
            this.ReapTButton.Location = new System.Drawing.Point(691, 14);
            this.ReapTButton.Name = "ReapTButton";
            this.ReapTButton.Size = new System.Drawing.Size(85, 22);
            this.ReapTButton.TabIndex = 22;
            this.ReapTButton.Text = "恢复原状";
            this.ReapTButton.UseVisualStyleBackColor = true;
            this.ReapTButton.Click += new System.EventHandler(this.ReapTButton_Click);
            // 
            // ClearTButton
            // 
            this.ClearTButton.Font = new System.Drawing.Font("宋体", 10.5F);
            this.ClearTButton.Location = new System.Drawing.Point(792, 14);
            this.ClearTButton.Name = "ClearTButton";
            this.ClearTButton.Size = new System.Drawing.Size(85, 22);
            this.ClearTButton.TabIndex = 21;
            this.ClearTButton.Text = "清空曲线";
            this.ClearTButton.UseVisualStyleBackColor = true;
            this.ClearTButton.Click += new System.EventHandler(this.ClearTButton_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(38, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(61, 18);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "曲线A";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(104, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 18);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "曲线B";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.tbxQTGS);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.tbxDSGS);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.tbxXSGS);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.tbxMSGS);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.tbxResultPos2);
            this.groupBox6.Controls.Add(this.tbxResult2);
            this.groupBox6.Controls.Add(this.picbxRes1);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.tbxResultPos1);
            this.groupBox6.Controls.Add(this.tbxResult1);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(571, 94);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(357, 226);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "探伤检测结果";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(180, 187);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 14);
            this.label18.TabIndex = 33;
            this.label18.Text = "其它缺陷个数:";
            // 
            // tbxQTGS
            // 
            this.tbxQTGS.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxQTGS.Location = new System.Drawing.Point(284, 183);
            this.tbxQTGS.Multiline = true;
            this.tbxQTGS.Name = "tbxQTGS";
            this.tbxQTGS.ReadOnly = true;
            this.tbxQTGS.Size = new System.Drawing.Size(67, 23);
            this.tbxQTGS.TabIndex = 32;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(9, 187);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 14);
            this.label23.TabIndex = 31;
            this.label23.Text = "断丝缺陷个数:";
            // 
            // tbxDSGS
            // 
            this.tbxDSGS.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxDSGS.Location = new System.Drawing.Point(112, 183);
            this.tbxDSGS.Multiline = true;
            this.tbxDSGS.Name = "tbxDSGS";
            this.tbxDSGS.ReadOnly = true;
            this.tbxDSGS.Size = new System.Drawing.Size(59, 23);
            this.tbxDSGS.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(180, 158);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 14);
            this.label17.TabIndex = 29;
            this.label17.Text = "锈蚀缺陷个数:";
            // 
            // tbxXSGS
            // 
            this.tbxXSGS.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxXSGS.Location = new System.Drawing.Point(284, 154);
            this.tbxXSGS.Multiline = true;
            this.tbxXSGS.Name = "tbxXSGS";
            this.tbxXSGS.ReadOnly = true;
            this.tbxXSGS.Size = new System.Drawing.Size(67, 23);
            this.tbxXSGS.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 27;
            this.label2.Text = "磨损缺陷个数:";
            // 
            // tbxMSGS
            // 
            this.tbxMSGS.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxMSGS.Location = new System.Drawing.Point(112, 154);
            this.tbxMSGS.Multiline = true;
            this.tbxMSGS.Name = "tbxMSGS";
            this.tbxMSGS.ReadOnly = true;
            this.tbxMSGS.Size = new System.Drawing.Size(59, 23);
            this.tbxMSGS.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 111);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 14);
            this.label12.TabIndex = 25;
            this.label12.Text = "单位缺陷个数:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(73, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 14);
            this.label13.TabIndex = 24;
            this.label13.Text = "缺陷检出率:";
            // 
            // tbxResultPos2
            // 
            this.tbxResultPos2.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxResultPos2.Location = new System.Drawing.Point(180, 107);
            this.tbxResultPos2.Multiline = true;
            this.tbxResultPos2.Name = "tbxResultPos2";
            this.tbxResultPos2.ReadOnly = true;
            this.tbxResultPos2.Size = new System.Drawing.Size(141, 23);
            this.tbxResultPos2.TabIndex = 19;
            // 
            // tbxResult2
            // 
            this.tbxResult2.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxResult2.Location = new System.Drawing.Point(180, 82);
            this.tbxResult2.Multiline = true;
            this.tbxResult2.Name = "tbxResult2";
            this.tbxResult2.ReadOnly = true;
            this.tbxResult2.Size = new System.Drawing.Size(141, 23);
            this.tbxResult2.TabIndex = 18;
            // 
            // picbxRes1
            // 
            this.picbxRes1.Location = new System.Drawing.Point(18, 56);
            this.picbxRes1.Name = "picbxRes1";
            this.picbxRes1.Size = new System.Drawing.Size(40, 40);
            this.picbxRes1.TabIndex = 16;
            this.picbxRes1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(73, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 14);
            this.label15.TabIndex = 15;
            this.label15.Text = "缺陷报警总量:";
            // 
            // tbxResultPos1
            // 
            this.tbxResultPos1.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxResultPos1.Location = new System.Drawing.Point(180, 56);
            this.tbxResultPos1.Multiline = true;
            this.tbxResultPos1.Name = "tbxResultPos1";
            this.tbxResultPos1.ReadOnly = true;
            this.tbxResultPos1.Size = new System.Drawing.Size(141, 23);
            this.tbxResultPos1.TabIndex = 14;
            // 
            // tbxResult1
            // 
            this.tbxResult1.Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxResult1.Location = new System.Drawing.Point(180, 30);
            this.tbxResult1.Multiline = true;
            this.tbxResult1.Name = "tbxResult1";
            this.tbxResult1.ReadOnly = true;
            this.tbxResult1.Size = new System.Drawing.Size(141, 23);
            this.tbxResult1.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(73, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 14);
            this.label14.TabIndex = 13;
            this.label14.Text = "吊索检测总量:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnMovSheBei);
            this.groupBox4.Controls.Add(this.cbxWorkTime);
            this.groupBox4.Controls.Add(this.btnSetPath);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.cbxWorkPos);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.cbxLineStep);
            this.groupBox4.Controls.Add(this.cbxLineWidth);
            this.groupBox4.Controls.Add(this.cbxLineNumber);
            this.groupBox4.Controls.Add(this.cbxWorkMode);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(4, 246);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(360, 149);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "系统设置";
            // 
            // cbxWorkTime
            // 
            this.cbxWorkTime.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cbxWorkTime.FormattingEnabled = true;
            this.cbxWorkTime.Items.AddRange(new object[] {
            "30",
            "60",
            "90",
            "120",
            "150",
            "180",
            "210",
            "240",
            "270",
            "300",
            "330",
            "360",
            "390",
            "420",
            "450",
            "480",
            "510",
            "540",
            "570",
            "600",
            "630",
            "660",
            "690",
            "720",
            "750",
            "780",
            "810",
            "840",
            "870",
            "900",
            "930",
            "960",
            "990",
            "1020",
            "1050",
            "1080"});
            this.cbxWorkTime.Location = new System.Drawing.Point(264, 72);
            this.cbxWorkTime.Name = "cbxWorkTime";
            this.cbxWorkTime.Size = new System.Drawing.Size(76, 22);
            this.cbxWorkTime.TabIndex = 18;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(188, 75);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(70, 14);
            this.label25.TabIndex = 17;
            this.label25.Text = "工作时间:";
            // 
            // cbxWorkPos
            // 
            this.cbxWorkPos.FormattingEnabled = true;
            this.cbxWorkPos.Items.AddRange(new object[] {
            "-10",
            "-5",
            "-1",
            "1",
            "5",
            "10",
            "-40",
            "-30",
            "-20",
            "20",
            "30",
            "40",
            "-70",
            "-60",
            "-50",
            "50",
            "60",
            "70",
            "-100",
            "-90",
            "-80",
            "80",
            "90",
            "100",
            "-130",
            "-120",
            "-110",
            "110",
            "120",
            "130",
            "-140",
            "140"});
            this.cbxWorkPos.Location = new System.Drawing.Point(102, 72);
            this.cbxWorkPos.Name = "cbxWorkPos";
            this.cbxWorkPos.Size = new System.Drawing.Size(76, 22);
            this.cbxWorkPos.TabIndex = 16;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(22, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 14);
            this.label22.TabIndex = 15;
            this.label22.Text = "调整距离:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(188, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 14);
            this.label19.TabIndex = 14;
            this.label19.Text = "吊索数量:";
            // 
            // cbxLineStep
            // 
            this.cbxLineStep.FormattingEnabled = true;
            this.cbxLineStep.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80"});
            this.cbxLineStep.Location = new System.Drawing.Point(264, 47);
            this.cbxLineStep.Name = "cbxLineStep";
            this.cbxLineStep.Size = new System.Drawing.Size(76, 22);
            this.cbxLineStep.TabIndex = 13;
            // 
            // cbxLineWidth
            // 
            this.cbxLineWidth.FormattingEnabled = true;
            this.cbxLineWidth.Items.AddRange(new object[] {
            "8",
            "10",
            "12",
            "16",
            "20",
            "24",
            "28",
            "32",
            "36",
            "40",
            "44",
            "48",
            "52",
            "56",
            "60",
            "64",
            "68",
            "72",
            "76",
            "80",
            "84",
            "88",
            "92",
            "96",
            "100",
            "120",
            "140",
            "160",
            "180",
            "200"});
            this.cbxLineWidth.Location = new System.Drawing.Point(102, 47);
            this.cbxLineWidth.Name = "cbxLineWidth";
            this.cbxLineWidth.Size = new System.Drawing.Size(76, 22);
            this.cbxLineWidth.TabIndex = 12;
            // 
            // cbxLineNumber
            // 
            this.cbxLineNumber.FormattingEnabled = true;
            this.cbxLineNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbxLineNumber.Location = new System.Drawing.Point(264, 22);
            this.cbxLineNumber.Name = "cbxLineNumber";
            this.cbxLineNumber.Size = new System.Drawing.Size(76, 22);
            this.cbxLineNumber.TabIndex = 10;
            // 
            // cbxWorkMode
            // 
            this.cbxWorkMode.FormattingEnabled = true;
            this.cbxWorkMode.Items.AddRange(new object[] {
            "安装调试",
            "检测运行",
            "缺陷定位"});
            this.cbxWorkMode.Location = new System.Drawing.Point(102, 22);
            this.cbxWorkMode.Name = "cbxWorkMode";
            this.cbxWorkMode.Size = new System.Drawing.Size(76, 22);
            this.cbxWorkMode.TabIndex = 9;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(188, 50);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 14);
            this.label20.TabIndex = 4;
            this.label20.Text = "吊索间隔:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(22, 50);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 14);
            this.label21.TabIndex = 3;
            this.label21.Text = "吊索直径:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(22, 25);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 14);
            this.label24.TabIndex = 0;
            this.label24.Text = "工作模式:";
            // 
            // picbxLogo
            // 
            this.picbxLogo.Location = new System.Drawing.Point(35, 8);
            this.picbxLogo.Name = "picbxLogo";
            this.picbxLogo.Size = new System.Drawing.Size(108, 68);
            this.picbxLogo.TabIndex = 16;
            this.picbxLogo.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(382, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(350, 31);
            this.label7.TabIndex = 23;
            this.label7.Text = "吊索探伤系统数据采集显控平台";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label8.Location = new System.Drawing.Point(883, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 14);
            this.label8.TabIndex = 24;
            this.label8.Text = "ESID 502 V2.0@HERAL.11.2024";
            // 
            // picbxHead1
            // 
            this.picbxHead1.Location = new System.Drawing.Point(4, 82);
            this.picbxHead1.Name = "picbxHead1";
            this.picbxHead1.Size = new System.Drawing.Size(1114, 10);
            this.picbxHead1.TabIndex = 26;
            this.picbxHead1.TabStop = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnCheckCOM;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1124, 715);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.picbxHead1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.picbxLogo);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.clockPanel);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.Text = "ESID 502 V2.0@HERAL.11.2024";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbx)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxRes1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxHead1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
//        private System.Windows.Forms.GroupBox groupBox3;
//        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbnHex;
        private System.Windows.Forms.RadioButton rbnChar;
        private System.Windows.Forms.Button btnOpenCom;
        private System.Windows.Forms.Button btnCheckCOM;
//        private System.Windows.Forms.Button btnCleanData;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.ComboBox cbxCOMPort;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.ComboBox cbxDataBits;
//        private System.Windows.Forms.TextBox tbxSendData;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox picbx;
        private ZedGraph.ZedGraphControl zgc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel clockPanel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxTime;
        private System.Windows.Forms.TextBox tbxWater;
        private System.Windows.Forms.TextBox tbxState;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxWarn;
//        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.IO.Ports.SerialPort sp;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnCleanRecData;
        private System.Windows.Forms.TextBox tbxRecvTnum;
//        private System.Windows.Forms.TextBox tbxTxnum;
        private System.Windows.Forms.TextBox tbxRecvData;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbxResultPos1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox picbxRes1;
        private System.Windows.Forms.TextBox tbxResultPos2;
        private System.Windows.Forms.PictureBox picbxLogo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox picbxHead1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbxLineStep;
        private System.Windows.Forms.ComboBox cbxLineWidth;
        private System.Windows.Forms.ComboBox cbxLineNumber;
        private System.Windows.Forms.ComboBox cbxWorkMode;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
//        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbxWorkTime;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbxWorkPos;
        private System.Windows.Forms.Label label22;
//        private System.Windows.Forms.RadioButton rbnHexs;
//        private System.Windows.Forms.RadioButton rbnChars;
        private System.Windows.Forms.Button ClearTButton;
        private System.Windows.Forms.Button ReapTButton;
        private System.Windows.Forms.Button btnBeginWork;
        private System.Windows.Forms.Button ForwardTButton;
        private System.Windows.Forms.ComboBox cbxTLLStep;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button SomeZhnTButton;
        private System.Windows.Forms.Button BehindwardTButton;
        private System.Windows.Forms.TextBox tbxResult2;
        private System.Windows.Forms.TextBox tbxResult1;
        private System.Windows.Forms.Button btnSetPath;
        private System.Windows.Forms.Button btnMovSheBei;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxMSGS;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbxQTGS;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbxDSGS;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxXSGS;
    }
}

