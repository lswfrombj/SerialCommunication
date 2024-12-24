using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;
using ZedGraph;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using static System.Windows.Forms.AxHost;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.LinkLabel;
using tt;
using System.Text.RegularExpressions;
using System.Windows.Media.TextFormatting;
using System.Windows;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Media;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

namespace SerialCommunication
{
    //public delegate void ColorEventHandler(ListViewItem lvi);
    public partial class Form1 : Form
    {
        //private SerialPort sp = null; //声明一个串口类
        StringBuilder strBuilder = new StringBuilder(); //避免在事件处理方法中的反复创建
        StringBuilder strBuilder0 = new StringBuilder(); //避免在事件处理方法中的反复创建
                                                        //        StringBuilder strbuilders = new StringBuilder();
                                                        //ArrayList arryList = new ArrayList(); //将每次接收的（几个）数据存储在长度可变的ArrayList集合中
                                                        //        float[] data = new float[2]; //data[0]用于接收水位报警值，data[1]用于接收实时水位
        bool hasData = false; //是否更新实时曲线标志位
                              //        int dataNum = 0; //数据序号，即实时数据坐标点的X值
        bool isOpen = false; //打开串口标志位，默认关闭状态
        bool isSetProperty = false; //属性设置标志位
        bool isHex = true; //十六进制显示标志位
//        bool isHexs = true; //十六进制显示标志位
                            //        int num = 1; //ListView序号
//        int tickStart = 0; //Starting time in milliseconds
//        bool lastState = true; //true表示上次正常，false表示上次危险
//        bool currentState = true; //true表示当前正常，false表示当前危险
        Image imageDanger = Image.FromFile(@"Resources\danger.png"); //危险
        Image imageNormal = Image.FromFile(@"Resources\normal.png"); //正常
        Image imageGif = Image.FromFile(@"Resources\danger.gif"); //正常
        System.Drawing.Icon nfyChangeStatus = new System.Drawing.Icon(@"Resources\LOGOs.ico");
        //20240508
//        ushort[][] cdata_buf = new ushort[2][]
//        {
//                new ushort[22000],
//                new ushort[22000]
//        };
//        ushort[][] tdata_buf = new ushort[2][]
//        {
//                new ushort[22000],
//                new ushort[22000]
//        };

        ushort[] ydata_bufA = new ushort[6100000];
        ushort[] ydata_bufB = new ushort[6100000];

        ushort[] rec_data_bufa = new ushort[250];
        ushort[] rec_data_bufb = new ushort[250];

        byte cnt_flame =0;
//        int chk_add = 0;
        int totnum0 = 0;
        int totnum = 0;
        int pernum = 507;
        int pernums = 0;
        int rec_pernum = -1;
//        int rec_pernum1 = 0;
        int rec_totnum = 0;
        int trs_totnum = 0;
        int grp_num = 80;
//        int recflg = 0;
        int[] cnt_process = new int[2];
        int linenum = 0;
        int rutenum = 10;
//        int[] datflg = new int[2];
        double[] tongji1 = new double[5];
        double[] tongji2 = new double[5];
/*        double[][] tongjiTot = new double[8][]
        {
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80]
        };
        double[][] tongjiGrp = new double[8][]
        {
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80],
            new double[80]
        };*/
        double[][] tongjiYusA = new double[5][]
         {
            new double[24400],
            new double[24400],
            new double[24400],
            new double[24400],
            new double[24400]
         };

        double[][] tongjiYusB = new double[5][]
         {
            new double[24400],
            new double[24400],
            new double[24400],
            new double[24400],
            new double[24400]
         };

//        int rec_cnt = 0;
//        int rec_cnt_pre = 0;
//        int pro_cnt = 0;
        int numtb = 24800000;
        byte[] Trec_buf = new byte[24800000];
        int numtz = 8000;
//        int cir_cntA = 0;
//        int cir_cntB = 0;
//        int wrf_flg = 0;
        int tem_flg = 0;
//        int bak_num = 0;
//        int bak_cnt = 0;
        int zhna = 0;
        int zhnb = 0;
        //        int cir_cnt1 = 0;

        //public ConfigurationManager CM = new ConfigurationManager();
        private static string filePath=null;
        private static string WorkPath = null;

//        private static DataSet ds;
//        private static int i, j;
//        private static int rowCount;
//        private static int columnCount;
        //private static string strConn;

        //private PointPairList list = new PointPairList();
        //private PointPairList list_2 = new PointPairList();
        //RollingPointPairList list = new RollingPointPairList(1200); //设置1200个点，假设每50毫秒更新一次，刚好检测1分钟，一旦构造后将不能更改这个值
        private RollingPointPairList list = new RollingPointPairList(6100000);
        private RollingPointPairList list_2 = new RollingPointPairList(6100000);
        //public static event ColorEventHandler color;
        LineItem lineItem;//符号，无数据的空线条
        LineItem lineItem_2;
        double time = 0;
//        double timeA = 0;
//        double timeB = 0;
        byte[] buf = null; //创建一个临时数组存储当前来的串口字节数据
//        char[] bufc = null; //创建一个临时数组存储当前来的串口字节数据
        byte[] buf1 = new Byte[507]; //创建一个临时数组存储当前来的串口字节数据
        int usingbs = -1;
        //        int usingbz = 0;
//        long mtimest = 0;
//        long mtimeen = 0;
//        long mtimeus = 0;
        int stp_flg = 0;
//        double[] checkas = new double[24400];
//        double[] checkbs = new double[24400];

/*        double[][] checkad = new double[4][]
        {
            new double[24400],
            new double[24400],
            new double[24400],
            new double[24400]
        };

        double[][] checkbd = new double[4][]
        {
            new double[24400],
            new double[24400],
            new double[24400],
            new double[24400]
        };*/

//        int cornuma = 0;
//        int cornumb = 0;
//        int corstpa = 0;
//        int corstpb = 0;
        double[] minaz = new double[5];
        int[] minap = new int[5];
        double[] minbz = new double[5];
        int[] minbp = new int[5];

        //        int disp_flg = 0;
        //        int disp_tims = 0;
        //        int disp_time = 0;
        int begin = 0;
//        int first_rute = 0;
        FileStream fs1 = null;
        System.IO.StreamWriter fs2a = null;
        System.IO.StreamWriter fs3a = null;
        System.IO.StreamWriter fs4a = null;
        System.IO.StreamWriter fs2b = null;
        System.IO.StreamWriter fs3b = null;
        System.IO.StreamWriter fs4b = null;

        byte[] begin_send = new byte[6]
        {
            0xBA,
            0x65,
            0x00,
            0x52,
            0x08,
            0x79
        };

//        BA 65 01 A4 10 D4
//        BA 65 00 A4 10 D3

        byte[] end_send = new byte[6]
        {
            0xBA,
            0x65,
            0x01,
            0x52,
            0x08,
            0x7A
        };
//        int status = 0;
        int modelsta = 0;
        int modelstb = 0;
        int mgrpm = 0;
        int cacsta = 0;
        int cacstb = 0;
        int mstepa = 0;
        int mstepb = 0;
        int mstepas = 0;
        int mstepbs = 0;
        int mwid = 0;
        int millisteps = 0;
        int totaldis = 0;
        int linenumber = 0;
        int linethickness = 0;
        int linespace = 0;
        int worktime = 0;
        string mfileh = null;
        int data_input_status = 0;
//        double btimea = 0;
//        double btimeb = 0;
        int startz = 0;
        int endz = 0;
        int SensorCurPos = 0;
        int SensorMovedis = 0;
        int CheckAdd = 0;
        int WorkMode = 0;
        int mLinenum = 0;
        int mLinestep = 0;
        int di = 0;
//        System.Diagnostics.Stopwatch stopTime = new System.Diagnostics.Stopwatch();
        int once = 0;
//        int perlinecnt = 0;
        int timeperzhen = 0;
//        int lineloop = 0;
        int thdcnt = 0;
        int thdper = 0;
        double[][] thresholda = new double[10][]
        {
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4]
        };
        double[][] thresholdb = new double[10][]
        {
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4],
            new double[4]
        };
        double[] thresholdc = new double[3];
        double[] thresholdd = new double[3];

        float[] gzres = new float[30000];
        float[] mpeek = new float[10000];
        string[] gzstr = { "正常", "磨损", "锈蚀", "断丝", "抖动", "其它" };
        int peknum = 0;
//        bool hasDataA = false;
//        bool hasDataB = false;
        int mj = 0;
        int nein = 0;
        bool cflg = false;
        int disn = 1;
        int savefilenumber = 1;

        AutoSizeFormClass asc = new AutoSizeFormClass();

        public Form1()
        {
            InitializeComponent(); //窗口初始化，net自动生成

//            this.MaximumSize = this.Size; //获取窗体的大小
//            this.MinimumSize = this.Size;
            this.MaximizeBox = true; //获取或设置一个值，该值指示是否在窗体的标题栏中显示“最小化”按钮

            this.WindowState = FormWindowState.Maximized;
//            this.AutoSize = true;
            this.ShowInTaskbar = true;

            asc.controllInitializeSize(this);

            //            this.micon1.Icon = nfyChangeStatus;
            //            this.micon1.Visible = false;

            //                        Refresh();

            cnt_process[0] = 0;
            cnt_process[1] = 0;
            zhna = 0;
            zhnb = 0;
            peknum = 0;
            mpeek[0] = 0;
            mpeek[1] = 0;
            strBuilder.Clear();
            strBuilder0.Clear();
            tbxRecvData.Text = "";

            tongji1[0] = 0;
            tongji1[1] = 0;
            tongji1[2] = (float)10000000.0;
            tongji1[3] = 0;
            tongji1[4] = 0;

            tongji2[0] = 0;
            tongji2[1] = 0;
            tongji2[2] = (float)10000000.0;
            tongji2[3] = 0;
            tongji2[4] = 0;

            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);//必须手动添加事件处理程序
        }

        //在第一次显示窗体前发生
        private void Form1_Load(object sender, EventArgs e)
        {
            //时钟钟表显示
            ClockControl clock = new ClockControl();
            clockPanel.Controls.Add(clock);

//            this.MaximumSize = this.Size; //获取窗体的大小
//            this.MinimumSize = this.Size;
//            this.MaximizeBox = true; //获取或设置一个值，该值指示是否在窗体的标题栏中显示“最小化”按钮
            //最大支持到串口10，可根据自己的需求添加
            for (int i = 0; i < 10; i++)
            {
                cbxCOMPort.Items.Add("COM" + (i + 1).ToString());
            }
            cbxCOMPort.SelectedIndex = 0;
            //列出常见的波特率
            cbxBaudRate.Items.Add("1200");
            cbxBaudRate.Items.Add("2400");
            cbxBaudRate.Items.Add("4800");
            cbxBaudRate.Items.Add("9600");
            cbxBaudRate.Items.Add("19200");
            cbxBaudRate.Items.Add("38400");
            cbxBaudRate.Items.Add("43000");
            cbxBaudRate.Items.Add("56000");
            cbxBaudRate.Items.Add("57600");
            cbxBaudRate.Items.Add("115200");
            cbxBaudRate.Items.Add("128000");
            cbxBaudRate.Items.Add("230400");
            cbxBaudRate.Items.Add("256000");
            cbxBaudRate.Items.Add("460800");
            cbxBaudRate.SelectedIndex = 11;
            //列出停止位
            cbxStopBits.Items.Add("0");
            cbxStopBits.Items.Add("1");
            cbxStopBits.Items.Add("1.5");
            cbxStopBits.Items.Add("2");
            cbxStopBits.SelectedIndex = 1;
            //列出数据位
            cbxDataBits.Items.Add("8");
            cbxDataBits.Items.Add("7");
            cbxDataBits.Items.Add("6");
            cbxDataBits.Items.Add("5");
            cbxDataBits.SelectedIndex = 0;
            //列出奇偶校验位
            cbxParity.Items.Add("无");
            cbxParity.Items.Add("奇校验");
            cbxParity.Items.Add("偶校验");
            cbxParity.SelectedIndex = 0;
            //默认为Hex显示
            rbnHex.Checked = true;
            cbxTLLStep.SelectedIndex = 3;
            //            rbnHexs.Checked = true;

            //默认当前状态
            cbxWorkMode.SelectedIndex = 0;
            cbxLineNumber.SelectedIndex = 7;
            cbxLineStep.SelectedIndex = 0;
            cbxLineWidth.SelectedIndex = 0;
            cbxWorkPos.SelectedIndex = 1;
            cbxWorkTime.SelectedIndex = 6;

//            cbxLineNumber.Enabled = false;
//            cbxLineStep.Enabled = false;
//            cbxLineWidth.Enabled = false;

            //公司Logo
            picbxLogo.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
            //picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
            picbxLogo.Image = Image.FromFile(@"Resources\LOGOs.png"); //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

            //显示头部边界线
            picbxHead1.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
            //picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
            picbxHead1.Image = Image.FromFile(@"Resources\head8.png"); //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

//            System.Drawing.Icon nfyChangeStatus = new System.Drawing.Icon(@"Resources\LOGOs.ico");
//            this.Icon = nfyChangeStatus;
             this.Icon = nfyChangeStatus;
            
            //显示头部边界线
            //            picbxHead2.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
            //picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
            //            picbxHead2.Image = Image.FromFile(@"Resources\head6e.png"); //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

            //默认显示关闭状态
            picbx.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
            //picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
            picbx.Image = Image.FromFile(@"Resources\close.png"); //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

            //默认显示正常状态
            tbxResult1.Text = "0";
            tbxResultPos1.Text = "0";
            picbxRes1.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
            //picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
            picbxRes1.Image = imageNormal; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

            //显示竖边线
////            picbxVb.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
            //picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
////            picbxVb.Image = Image.FromFile(@"Resources\Vb.png"); //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

            //默认显示正常状态
            tbxResult2.Text = "0";
            tbxResultPos2.Text = "0";

            tbxMSGS.Text = "0";
            tbxXSGS.Text = "0";
            tbxDSGS.Text = "0";
            tbxQTGS.Text = "0";

////            picbxRes2.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
//picbx.Image = Image.FromFile(@"C:\Data\visual studio 2013\Projects\SerialCommunication\SerialCommunication\Properties\Resources\red.jpg");
////            picbxRes2.Image = imageNormal; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹

            //实时曲线初始化
            GraphPane graphPane = zgc.GraphPane; //获取引用
            graphPane.Title.Text = "实时监测曲线"; //设置标题
            graphPane.XAxis.Title.Text = "时间（ms）"; //设置X轴说明文字
            graphPane.YAxis.Title.Text = "采集数据"; //设置Y轴说明文字

            FontSpec myFont = new FontSpec("Arial", 16, System.Drawing.Color.Black, true, false, false);
            graphPane.Legend.FontSpec = myFont;
            graphPane.Legend.FontSpec.Border.IsVisible = false;

            graphPane.Legend.Border.IsVisible = false;

            //            Border mb = new Border(Color.Blue,-1);
            lineItem = graphPane.AddCurve("A通道", list, System.Drawing.Color.Blue, SymbolType.None); //开始增加的线是没有数据点的(也就是list为空)，增加一条名称:Voltage，颜色Color.Bule，无符号，无数据的空线条
            lineItem_2 = graphPane.AddCurve("B通道", list_2, System.Drawing.Color.Red, SymbolType.None); //开始增加的线是没有数据点的(也就是list为空)，增加一条名称:Voltage，颜色Color.Bule，无符号，无数据的空线条
            //            graphPane.Legend.Border = mb;
            timer1.Interval = 20; //设置timer控件的间隔为50ms，用于实时曲线
            timer1.Enabled = true; //timer可用
            timer1.Start(); //启动

            graphPane.Y2Axis.IsVisible = true;
            graphPane.Y2Axis.Scale.Align = AlignP.Inside;
            graphPane.Y2Axis.MajorTic.IsOpposite = false;
            graphPane.Y2Axis.MinorTic.IsOpposite = false;
/*            graphPane.YAxis.Scale.Min = 0; //X轴最小值0
            graphPane.YAxis.Scale.Max = 150; //X轴最大值60
            graphPane.YAxis.Scale.MinorStep = 3; //X轴小步长1，也就是小间隔
            graphPane.YAxis.Scale.MajorStep = 15; //X轴大步长5，也就是显示文字的大间隔
            graphPane.Y2Axis.Scale.Min = 0; //X轴最小值0
            graphPane.Y2Axis.Scale.Max = 150; //X轴最大值60
            graphPane.Y2Axis.Scale.MinorStep = 3; //X轴小步长1，也就是小间隔
            graphPane.Y2Axis.Scale.MajorStep = 15; //X轴大步长5，也就是显示文字的大间隔

            //graphPane.XAxis.Scale.Format = "dd  HH:mm:ss";   //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") 
            //graphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;
            graphPane.XAxis.Scale.Min = 0; //X轴最小值0
            graphPane.XAxis.Scale.Max = 4000; //X轴最大值60
            graphPane.XAxis.Scale.MinorStep = 40; //X轴小步长1，也就是小间隔
            graphPane.XAxis.Scale.MajorStep = 200; //X轴大步长5，也就是显示文字的大间隔
*/
            graphPane.XAxis.Scale.MaxAuto = true;
            graphPane.XAxis.Scale.MinAuto = true;
            graphPane.XAxis.Scale.MajorStepAuto = true;
            graphPane.XAxis.Scale.MinorStepAuto = true;

            graphPane.YAxis.Scale.MaxAuto = true;
            graphPane.YAxis.Scale.MinAuto = true;
            graphPane.YAxis.Scale.MajorStepAuto = true;
            graphPane.YAxis.Scale.MinorStepAuto = true;
            graphPane.Y2Axis.Scale.MaxAuto = true;
            graphPane.Y2Axis.Scale.MinAuto = true;
            graphPane.Y2Axis.Scale.MajorStepAuto = true;
            graphPane.Y2Axis.Scale.MinorStepAuto = true;
            zgc.PanModifierKeys = Keys.None;
            zgc.ZoomStepFraction = 0.1;

//                        zgc.IsShowHScrollBar = false;
            zgc.IsShowPointValues = true;
            zgc.IsZoomOnMouseCenter = true;
            zgc.AxisChange(); //改变轴的刻度
 //           tickStart = Environment.TickCount; //保存开始时间，即系统启动后经过的毫秒数
        }

        //检测哪些串口可用
        private void btnCheckCOM_Click(object sender, EventArgs e)
        {
            //bool comExistence = false; //有可用的串口标志位
            //cbxCOMPort.Items.Clear(); //清除当前串口号中所有串口名称

            string[] ArryPort = SerialPort.GetPortNames();
            cbxCOMPort.Items.Clear();
            for (int i = 0; i < ArryPort.Length; i++)
            {
                cbxCOMPort.Items.Add(ArryPort[i]);
            }
            if(ArryPort.Length > 0)
            {
                cbxCOMPort.SelectedIndex = 0;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("未发现串口设备，请检查是否连接！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //检查串口是否设置
        private bool CheckPortSetting()
        {
            if (cbxCOMPort.Text.Trim() == "")
            {
                return false;
            }
            if (cbxBaudRate.Text.Trim() == "")
            {
                return false;
            }
            if (cbxDataBits.Text.Trim() == "")
            {
                return false;
            }
            if (cbxParity.Text.Trim() == "")
            {
                return false;
            }
            if (cbxStopBits.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }

        //检查是否发送数据
        private bool CheckSendData()
        {
            if (tbxRecvData.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }

        //设置串口属性
        private void SetPortProperty()
        {
            sp = new SerialPort();
            sp.PortName = cbxCOMPort.Text.Trim(); //设置串口名
            sp.BaudRate = Convert.ToInt32(cbxBaudRate.Text.Trim()); //设置串口波特率
            float f = Convert.ToSingle(cbxStopBits.Text.Trim()); //设置停止位
            if (f == 0)
            {
                sp.StopBits = StopBits.None;
            }
            else if (f == 1.5)
            {
                sp.StopBits = StopBits.OnePointFive;
            }
            else if (f == 1)
            {
                sp.StopBits = StopBits.One;
            }
            else if (f == 2)
            {
                sp.StopBits = StopBits.Two;
            }
            else
            {
                sp.StopBits = StopBits.One;
            }
            sp.DataBits = Convert.ToInt16(cbxDataBits.Text.Trim()); //设置数据位
            string s = cbxParity.Text.Trim(); //设置奇偶校验位
            if (s.CompareTo("无") == 0)
            {
                sp.Parity = Parity.None;
            }
            if (s.CompareTo("奇校验") == 0)
            {
                sp.Parity = Parity.Odd;
            }
            if (s.CompareTo("偶校验") == 0)
            {
                sp.Parity = Parity.Even;
            }
            else
            {
                sp.Parity = Parity.None;
            }
//            sp.NewLine = "\n"; //用于解释 ReadLine( )和WriteLine( )方法调用结束的值
            sp.ReadTimeout = -1; //设置超时读取时间，无限长等待
//            sp.RtsEnable = true;//启用请求发送(RTS)信号
            sp.RtsEnable = true;//启用请求发送(RTS)信号
            sp.ReceivedBytesThreshold = 1;

//            sp.ReadBufferSize = 50700;
            //注册对串口接收数据的响应方法  ,串口接收数据的操作是在从线程中执行的
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //设置数据显示格式
            if (rbnHex.Checked)
            {
                isHex = true;
            }
            else
            {
                isHex = false;
            }
        }

        //发送串口数据
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] send_buf = null;
            string sends;

            if (isOpen) //串口标志位是否打开，写串口数据
            {
                try
                {
                    sends = tbxRecvData.Text.Replace(" ", "");

                    if (isHex)
                    {
                        if ((sends.Length % 2) != 0)
                        {
                            sends = sends.Insert(sends.Length - 1, "0");
                        }
                        send_buf = new byte[sends.Length / 2];

                        for (int i = 0; i < send_buf.Length; i++)
                        {
                            send_buf[i] = Convert.ToByte(sends.Substring(i * 2, 2), 16);
                        }

                        sp.Write(send_buf, 0, send_buf.Length);
//                        sp.Write(sends);
                        //BA 65 00 52 08 79
                    }
                    else
                    {
                        sp.WriteLine(sends);
                    }
                    trs_totnum = send_buf.Length;

                    //                    try
                    //                    {
                    //                        this.Invoke((EventHandler)(delegate
                    //                        {
                    tbxRecvTnum.Text = "Transmit: " + trs_totnum.ToString() + "Bytes" + "\r\n";
//                        }));
//                    }
//                    catch
//                    {
//                        MessageBox.Show("串口已关闭！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("发送数据时发生错误！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("串口未打开！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CheckSendData()) //检测要发送的数据
            {
                System.Windows.Forms.MessageBox.Show("请输入要发送的数据！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //打开按钮的事件响应
        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                if (!CheckPortSetting()) //检查串口设置
                {
                    System.Windows.Forms.MessageBox.Show("串口未设置！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!isSetProperty) //串口未设置则设置串口
                {
                    SetPortProperty();
                    isSetProperty = true; //串口设置完成后记置为true
                }
                try //打开串口
                {
                    picbx.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
                    picbx.Image = Image.FromFile(@"Resources\open.png"); //设置打开状态显示绿色
                    sp.Open(); //开串口
                    isOpen = true;
                    btnOpenCom.Text = "关闭串口"; //按钮显示文字转变
                    //串口打开后则相应的串口设置按钮便不可再用
                    cbxCOMPort.Enabled = false;
                    cbxBaudRate.Enabled = false;
                    cbxDataBits.Enabled = false;
                    cbxParity.Enabled = false;
                    cbxStopBits.Enabled = false;
                    rbnChar.Enabled = false;
                    rbnHex.Enabled = false;
                    btnCheckCOM.Enabled = false;

                    FileStream ls = new System.IO.FileStream("work.ini", FileMode.Open, FileAccess.Read, FileShare.None);
                    ls.Read(buf1, 0, 27);
                    mgrpm = (int)((ushort)(buf1[0] << 8) + (ushort)buf1[1]);
                    grp_num = mgrpm;
                    modelsta = (int)((ushort)(buf1[2] << 8) + (ushort)buf1[3]);
                    modelstb = (int)((ushort)(buf1[4] << 8) + (ushort)buf1[5]);
                    cacsta = (int)((ushort)(buf1[6] << 8) + (ushort)buf1[7]);
                    cacstb = (int)((ushort)(buf1[8] << 8) + (ushort)buf1[9]);
                    mstepa = (int)buf1[10];
                    mstepb = (int)buf1[11];
                    mstepas = (int)buf1[12];
                    mstepbs = (int)buf1[13];
                    mwid = (int)((ushort)(buf1[14] << 8) + (ushort)buf1[15]);
                    millisteps = (int)((ushort)(buf1[16] << 8) + (ushort)buf1[17]);
                    totaldis = (int)((ushort)(buf1[18] << 8) + (ushort)buf1[19]);
                    linenumber = (int)buf1[20];
                    linethickness = (int)buf1[21];
                    linespace = (int)buf1[22];
                    timeperzhen = (int)((ushort)(buf1[23] << 8) + (ushort)buf1[24]);
                    thdcnt = (int)buf1[25];
                    thdper = (int)buf1[26];
                    ls.Read(buf1, 0, thdcnt * (thdper + 1));
                    for(int i=0;i< thdcnt;i++)
                    {
                        for (int j = 0; j < (thdper + 1); j++)
                        {
                            thresholda[i][j] = (double)(buf1[i * (thdper + 1) + j]) / 100.0;
                        }
                    }
                    ls.Read(buf1, 0, thdcnt * (thdper + 1));
                    for (int i = 0; i < thdcnt; i++)
                    {
                        for (int j = 0; j < (thdper + 1); j++)
                        {
                            thresholdb[i][j] = (double)(buf1[i * (thdper + 1) + j]) / 100.0;
                        }
                    }
                    ls.Read(buf1, 0, 6);
                    thresholdc[0] = (double)buf1[0];
                    thresholdc[1] = (double)buf1[1];
                    thresholdc[2] = (double)buf1[2];
                    thresholdd[0] = (double)buf1[3];
                    thresholdd[1] = (double)buf1[4];
                    thresholdd[2] = (double)buf1[5];
                    ls.Close();

/*                    if (WorkMode == 0)
                    {
                        if (cbxWorkPos.Text.Trim() == "上")
                        {
                            status = 1;
                            SensorCurPos = 0;
                        }
                        if (cbxWorkPos.Text.Trim() == "中")
                        {
                            status = 0;
                            //                        SensorCurPos = totaldis / 2;

                        }
                        if (cbxWorkPos.Text.Trim() == "下")
                        {
                            status = -1;
                            SensorCurPos = totaldis;
                        }

                        if (status > 0)
                        {
                            SensorMovedis = linespace * millisteps / totaldis;
                            byte[] intBuff = BitConverter.GetBytes(SensorMovedis);
                            end_send[3] = intBuff[1];
                            end_send[4] = intBuff[0];
                            CheckAdd = end_send[0] + end_send[1] + end_send[2] + end_send[3] + end_send[4];
                            intBuff = BitConverter.GetBytes(CheckAdd);
                            end_send[5] = intBuff[0];
                            sp.Write(end_send, 0, end_send.Length);
                            SensorCurPos += linespace;
                        }
                        else if (status <= 0)
                        {
                            SensorMovedis = linespace * millisteps / totaldis;
                            byte[] intBuff = BitConverter.GetBytes(SensorMovedis);
                            begin_send[3] = intBuff[1];
                            begin_send[4] = intBuff[0];
                            CheckAdd = begin_send[0] + begin_send[1] + begin_send[2] + begin_send[3] + begin_send[4];
                            intBuff = BitConverter.GetBytes(CheckAdd);
                            begin_send[5] = intBuff[0];
                            sp.Write(begin_send, 0, begin_send.Length);
                            SensorCurPos -= linespace;
                        }
                    }
                    else
                    {
//                        perlinecnt = (int)((LineLength / GoSpeed) * 1000.0F / timeperzhen);
                        if(perlinecnt < mgrpm)
                        {
                            perlinecnt = mgrpm * 2;
                        }

                        SensorMovedis = linespace * millisteps / totaldis;
                        byte[] intBuff = BitConverter.GetBytes(millisteps);
                        begin_send[3] = intBuff[1];
                        begin_send[4] = intBuff[0];
                        CheckAdd = begin_send[0] + begin_send[1] + begin_send[2] + begin_send[3] + begin_send[4];
                        intBuff = BitConverter.GetBytes(CheckAdd);
                        begin_send[5] = intBuff[0];
                        sp.Write(begin_send, 0, begin_send.Length);
                        SensorCurPos = 0;

                        lineloop = 0;
                        once = 0;
                    }
*/
                    /*DateTime.Now.ToString();
                    DateTime.Now.Year.ToString(); 获取年份  // 2008
                    DateTime.Now.Month.ToString(); 获取月份   // 9
                    DateTime.Now.DayOfWeek.ToString(); 获取星期   // Thursday
                    DateTime.Now.DayOfYear.ToString(); 获取第几天   // 248
                    DateTime.Now.Hour.ToString(); 获取小时   // 20
                    DateTime.Now.Minute.ToString(); 获取分钟   // 31
                    DateTime.Now.Second.ToString(); 获取秒数   // 45

                    //n为一个数,可以数整数,也可以事小数
                    dt.AddYears(n).ToString();   //时间加n年
                    dt.AddDays(n).ToString();   //加n天
                    dt.AddHours(n).ToString();   //加n小时
                    dt.AddMonths(n).ToString();   //加n个月
                    dt.AddSeconds(n).ToString();   //加n秒
                    dt.AddMinutes(n).ToString();   //加n分*/
                }
                catch (Exception)
                {
                    //打开串口失败后，相应的标志位取消
                    isSetProperty = false;
                    isOpen = false;
                    System.Windows.Forms.MessageBox.Show("串口无效或已被占用！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try //已经打开串口
                {
                    sp.Close(); //关闭端口连接
                    isOpen = false;
                    isSetProperty = false;
                    btnOpenCom.Text = "打开串口"; //设置标签显示“打开串口”
                    picbx.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
                    picbx.Image = Image.FromFile(@"Resources\close.png"); //设置打开状态显示红色
                    //关闭串口后，串口设置选项便可以继续使用
                    cbxCOMPort.Enabled = true;
                    cbxBaudRate.Enabled = true;
                    cbxDataBits.Enabled = true;
                    cbxParity.Enabled = true;
                    cbxStopBits.Enabled = true;
                    rbnChar.Enabled = true;
                    rbnHex.Enabled = true;
                    btnCheckCOM.Enabled = true;
                }
                catch(Exception)//打开后关闭，重新打开
                {
                    //MessageBox.Show("关闭串口！", "需要请重新打开");
                    SetPortProperty();
                    isSetProperty = true; //串口设置完成后记置为true
                    btnOpenCom.Text = "打开串口"; //按钮显示文字转变
                    picbx.SizeMode = PictureBoxSizeMode.StretchImage; //设置图片在PictureBox控件中的显示方式
                    picbx.Image = Image.FromFile(@"Resources\open.png"); //设置打开状态显示绿色
                    //串口打开后则相应的串口设置按钮便不可再用
                    cbxCOMPort.Enabled = true;
                    cbxBaudRate.Enabled = true;
                    cbxDataBits.Enabled = true;
                    cbxParity.Enabled = true;
                    cbxStopBits.Enabled = true;
                    rbnChar.Enabled = true;
                    rbnHex.Enabled = true;
                    btnCheckCOM.Enabled = true;
                }
            }
        }

/*        int findIndex(StringBuilder str, byte b)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (b == str[i])
                {
                    return i;
                }
            }
            return -1;
        }*/

        //DataReceived事件，接收数据的响应方法
        //sender----引发事件的对象，e----传递给事件处理程序的数据
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (begin == 1)
            {
                rec_pernum = sp.BytesToRead;

                if ((rec_pernum > 0) && (tem_flg == 0))
                {
                    if ((cnt_process[0] == numtz) || (cnt_process[1] == numtz))
                    {
                        rec_totnum = 0;
                        usingbs = -1;
                        cnt_process[0] = 0;
                        cnt_process[1] = 0;
                        zhna = 0;
                        zhnb = 0;
                        peknum = 0;
                        mpeek[0] = 0;
                        mpeek[1] = 0;
                        totnum0 = 0;
                        cflg = false;
                        disn = 1;

                        strBuilder.Clear();
                        strBuilder0.Clear();
                        tbxRecvData.Text = "";

                        if (savefilenumber <= 1000)
                        {
                            mfileh = "B" + (linenum + 1).ToString("00") + "_" + tbxTime.Text + "_" + savefilenumber.ToString("0000") + "_";
                            mfileh += DateTime.Now.Year.ToString("0000");
                            mfileh += DateTime.Now.Month.ToString("00");
                            mfileh += DateTime.Now.Day.ToString("00");
                            mfileh += DateTime.Now.Hour.ToString("00");
                            mfileh += DateTime.Now.Minute.ToString("00");
                            mfileh += DateTime.Now.Second.ToString("00");
                            if (WorkPath != null)
                            {
                                mfileh = WorkPath + "\\" + mfileh;
                                StreamWriter tf = new System.IO.StreamWriter("Log.inf", false, System.Text.Encoding.GetEncoding("gb2312"));

                                tf.WriteLine(WorkPath + "\\");
                                tf.Close();
                            }
                            else
                            {
                                if (File.Exists("Log.inf"))
                                {
                                    StreamReader tr = new System.IO.StreamReader("Log.inf");
                                    WorkPath = tr.ReadLine();
                                    tr.Close();
                                    mfileh = WorkPath + mfileh;
                                }
                            }

                            fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                            fs1.Close();
                            //                        fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs2a.Close();
                            //                        fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs3a.Close();
                            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs4a.Close();
                            //                        fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs2b.Close();
                            //                        fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs3b.Close();
                            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs4b.Close();

                            savefilenumber++;
                        }
                        else
                        {
                            fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                            fs1.Close();
                            //                        fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs2a.Close();
                            //                        fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs3a.Close();
                            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs4a.Close();
                            //                        fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs2b.Close();
                            //                        fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            //                        fs3b.Close();
                            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs4b.Close();

                            savefilenumber++;
                        }

                        //                        cir_cntA++;
                        //                        cir_cntB++;
                    }

                    buf = new byte[rec_pernum];
                    sp.Read(buf, 0, rec_pernum); //读取缓存数据
                    if (rec_totnum == numtb)
                    {
                        rec_totnum = 0;
                    }
                    if (rec_pernum <= (numtb - rec_totnum))
                    {
                        Buffer.BlockCopy(buf, 0, Trec_buf, rec_totnum, rec_pernum);
                        rec_totnum += rec_pernum;
                    }
                    else
                    {
                        Buffer.BlockCopy(buf, 0, Trec_buf, rec_totnum, (numtb - rec_totnum));
                        rec_totnum = numtb;
                    }

                    if (usingbs == -1)
                    {
                        for (int i = 0; i < rec_totnum - 1; i++)
                        {
                            if (((Trec_buf[i] == 0xEB) && (Trec_buf[i + 1] == 0x90)) &&
                                ((Trec_buf[507 + i] == 0xEB) && (Trec_buf[507 + i + 1] == 0x90)) &&
                                ((Trec_buf[507 * 2 + i] == 0xEB) && (Trec_buf[507 * 2 + i + 1] == 0x90)))
                            {
                                if (((Trec_buf[i + 2] + 1) == (Trec_buf[507 + i + 2])) &&
                                    ((Trec_buf[507 + i + 2] + 1) == (Trec_buf[507 * 2 + i + 2])) &&
                                    (Trec_buf[i + 4] == 0x0A))
                                {
                                    usingbs = i;
                                    break;
                                }
                            }
                        }
                    }

                    if ((usingbs >= 0) && ((rec_totnum - usingbs - (cnt_process[0] + cnt_process[1]) * pernum) >= pernum))
                    {
                        Buffer.BlockCopy(Trec_buf, usingbs + (cnt_process[0] + cnt_process[1]) * pernum, buf1, 0, pernum);

                        if ((buf1[0] == 0xEB) && (buf1[1] == 0x90))
                        {
                            cnt_flame = buf1[2];
                            linenum = buf1[3];
                            rutenum = buf1[4];

                            if (rutenum == 10)
                            {
                                //                                hasDataA = true;

                                cnt_process[0]++;
                                //获取采集数据

                                int j = 0;
                                for (int i = 0; i < 2 * pernums; i += 2)
                                {
                                    rec_data_bufa[j] = (ushort)((buf1[i + 5] << 8) + buf1[i + 6]);
                                    j++;
                                }

                                AutoSaveFileByteSs();
                                PerCacinga((cnt_process[0] + cnt_process[1]), 1);

                                try
                                {
                                    this.Invoke((EventHandler)(delegate
                                    {
                                        FindPeekSsa();

                                        tbxRecvTnum.Text = rec_totnum.ToString() + "Bytes" + "\r\n";
                                        //                        tbxTxnum.Text = trs_totnum.ToString() + "Bytes" + "\r\n";

                                        tbxTime.Text = linenum.ToString();
                                        tbxWarn.Text = rutenum.ToString("X2");
                                        tbxWater.Text = cnt_process[0].ToString();
                                    }));
                                }
                                catch
                                {
                                    System.Windows.Forms.MessageBox.Show("串口已关闭！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (rutenum == 11)
                            {
                                //                                hasDataB = true;

                                cnt_process[1]++;
                                //获取采集数据

                                int j = 0;
                                for (int i = 0; i < 2 * pernums; i += 2)
                                {
                                    rec_data_bufb[j] = (ushort)((buf1[i + 5] << 8) + buf1[i + 6]);
                                    j++;
                                }

                                AutoSaveFileByteSs();
                                PerCacingb((cnt_process[0] + cnt_process[1]), 1);

                                try
                                {
                                    this.Invoke((EventHandler)(delegate
                                    {
                                        FindPeekSsb();

                                        tbxRecvTnum.Text = rec_totnum.ToString() + "Bytes" + "\r\n";
                                        //                        tbxTxnum.Text = trs_totnum.ToString() + "Bytes" + "\r\n";

                                        tbxTime.Text = linenum.ToString();
                                        tbxWarn.Text = rutenum.ToString("X2");
                                        tbxWater.Text = cnt_process[1].ToString();
                                    }));
                                }
                                catch
                                {
                                    System.Windows.Forms.MessageBox.Show("串口已关闭！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                //                                hasData = true;
                            }

                            if ((zhna == 1) && (zhnb == 1))
                            {
                                totnum = zhna;
                                totnum0 = totnum;
                                hasData = true;
                            }
                            else if((zhna > 1) && (zhnb > 1))
                            {
                                totnum = (zhna > zhnb ? zhnb : zhna);
                                if (totnum == (totnum0 + 1))
                                {
                                    totnum0 = totnum;
                                    hasData = true;
                                }
                            }
                        }
/*                        else
                        {
                            rec_totnum = 0;
                            usingbs = -1;
                            cnt_process[0] = 0;
                            cnt_process[1] = 0;
                            zhna = 0;
                            zhnb = 0;
                            peknum = 0;
                            mpeek[0] = 0;
                            mpeek[1] = 0;
                            totnum0 = 0;
                            cflg = false;
                            disn = 1;

//                            strBuilder.Clear();
//                            strBuilder0.Clear();
                        }*/
                    }
                }
            }
        }

    void Input_DataControl(int startz)
    {
            if ((rec_pernum > 0) && (tem_flg == 0))
            {
                if ((usingbs >= 0) && ((rec_totnum - usingbs - startz * pernum) >= pernum))
                {
                    Buffer.BlockCopy(Trec_buf, usingbs + startz * pernum, buf1, 0, pernum);

                    if ((buf1[0] == 0xEB) && (buf1[1] == 0x90))
                    {
                        cnt_flame = buf1[2];
                        linenum = buf1[3];
                        rutenum = buf1[4];

                        if (rutenum == 10)
                        {
//                            hasDataA = true;

                            cnt_process[0]++;
                            //获取采集数据

                            int j = 0;
                            for (int i = 0; i < 2 * pernums; i += 2)
                            {
                                rec_data_bufa[j] = (ushort)((buf1[i + 5] << 8) + buf1[i + 6]);
                                j++;
                            }

                            AutoSaveFileByteSs();
                            PerCacinga((cnt_process[0] + cnt_process[1]), 0);

//                            try
//                            {
//                                this.Invoke((EventHandler)(delegate
//                                {
                                    FindPeekSsa();

                                    tbxRecvTnum.Text = ((cnt_process[0] + cnt_process[1]) * pernum).ToString() + "Bytes" + "\r\n";
                                    //                        tbxTxnum.Text = trs_totnum.ToString() + "Bytes" + "\r\n";

                                    tbxTime.Text = linenum.ToString();
                                    tbxWarn.Text = rutenum.ToString("X2");
                                    tbxWater.Text = cnt_process[0].ToString();
//                                }));
//                            }
//                            catch
//                            {
//                                System.Windows.Forms.MessageBox.Show("串口已关闭！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            }
                            Refresh();
                        }
                        if (rutenum == 11)
                        {
//                            hasDataB = true;

                            cnt_process[1]++;
                            //获取采集数据

                            int j = 0;
                            for (int i = 0; i < 2 * pernums; i += 2)
                            {
                                rec_data_bufb[j] = (ushort)((buf1[i + 5] << 8) + buf1[i + 6]);
                                j++;
                            }

                            AutoSaveFileByteSs();
                            PerCacingb((cnt_process[0] + cnt_process[1]), 0);

//                            try
//                            {
//                                this.Invoke((EventHandler)(delegate
//                                {
                                    FindPeekSsb();

                                    tbxRecvTnum.Text = ((cnt_process[0] + cnt_process[1]) * pernum).ToString() + "Bytes" + "\r\n";
                                    //                        tbxTxnum.Text = trs_totnum.ToString() + "Bytes" + "\r\n";

                                    tbxTime.Text = linenum.ToString();
                                    tbxWarn.Text = rutenum.ToString("X2");
                                    tbxWater.Text = cnt_process[1].ToString();
//                                }));
//                            }
//                            catch
//                            {
//                                System.Windows.Forms.MessageBox.Show("串口已关闭！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            }

                            Refresh();
//                            hasData = true;
                        }

                        if ((zhna == 1) && (zhnb == 1))
                        {
                            totnum = zhna;
                            totnum0 = totnum;
                            hasData = true;
                        }
                        else if ((zhna > 1) && (zhnb > 1))
                        {
                            totnum = (zhna > zhnb ? zhnb : zhna);
                            if (totnum == (totnum0 + 1))
                            {
                                totnum0 = totnum;
                                hasData = true;
                            }
                        }
                    }
                }
            }
        }

        void PerCacinga(int zhn, int slg)
        {
/*            Buffer.BlockCopy(Trec_buf, usingbs + (zhn - 1) * pernum, buf1, 0, pernum);
            cnt_flame = buf1[2];
            linenum = buf1[3];
            rutenum = buf1[4];*/

            if ((rutenum == 10) && (cnt_process[0] > 0))
            {
                tongji1[0] = 0;
                tongji1[1] = 0;
                tongji1[2] = (float)10000000.0;
                tongji1[3] = 0;
                tongji1[4] = 0;

                int j = 0;
                for (int m = 0; m < 2 * pernums; m += 2)
                {
                    rec_data_bufa[j] = (ushort)((buf1[m + 5] << 8) + buf1[m + 6]);

                    tongji1[3] += (float)rec_data_bufa[j];
                    tongji1[4] += (float)rec_data_bufa[j] * (float)rec_data_bufa[j];
                    if (rec_data_bufa[j] > tongji1[1])
                    {
                        tongji1[1] = rec_data_bufa[j];
                    }
                    if (rec_data_bufa[j] < tongji1[2])
                    {
                        tongji1[2] = rec_data_bufa[j];
                    }
                    j++;
                }

                tongji1[0] = (float)pernums;
                tongji1[3] = tongji1[3] / tongji1[0];
                tongji1[4] = tongji1[4] / tongji1[0];
                tongji1[4] = Math.Sqrt(tongji1[4] - tongji1[3] * tongji1[3]);

                Buffer.BlockCopy(rec_data_bufa, 0, ydata_bufA, 2 * zhna * pernums, 2 * pernums);
                tongjiYusA[0][zhna] = tongji1[1];
                tongjiYusA[1][zhna] = tongji1[2];
                tongjiYusA[2][zhna] = tongji1[3];
                tongjiYusA[3][zhna] = tongji1[4];
                tongjiYusA[4][zhna] = (double)zhna * 0.05;

                if (slg == 0)
                {
                    AutoSaveFileShorta(zhn, cnt_flame, linenum, rutenum, rec_data_bufa);
                    AutoSaveFileAveStda(zhn, cnt_flame, linenum, rutenum, tongji1);
                }
                zhna++;
            }
        }

        void PerCacingb(int zhn, int slg)
        {
            /*            Buffer.BlockCopy(Trec_buf, usingbs + (zhn - 1) * pernum, buf1, 0, pernum);
                        cnt_flame = buf1[2];
                        linenum = buf1[3];
                        rutenum = buf1[4];*/

            if ((rutenum == 11) && (cnt_process[1] > 0))
            {
                tongji2[0] = 0;
                tongji2[1] = 0;
                tongji2[2] = (float)10000000.0;
                tongji2[3] = 0;
                tongji2[4] = 0;

                int j = 0;
                for (int m = 0; m < 2 * pernums; m += 2)
                {
                    rec_data_bufb[j] = (ushort)((buf1[m + 5] << 8) + buf1[m + 6]);

                    tongji2[3] += (float)rec_data_bufb[j];
                    tongji2[4] += (float)rec_data_bufb[j] * (float)rec_data_bufb[j];
                    if (rec_data_bufb[j] > tongji2[1])
                    {
                        tongji2[1] = rec_data_bufb[j];
                    }
                    if (rec_data_bufb[j] < tongji2[2])
                    {
                        tongji2[2] = rec_data_bufb[j];
                    }
                    j++;
                }

                tongji2[0] = (float)pernums;
                tongji2[3] = tongji2[3] / tongji2[0];
                tongji2[4] = tongji2[4] / tongji2[0];
                tongji2[4] = Math.Sqrt(tongji2[4] - tongji2[3] * tongji2[3]);

                Buffer.BlockCopy(rec_data_bufb, 0, ydata_bufB, 2 * zhnb * pernums, 2 * pernums);
                tongjiYusB[0][zhnb] = tongji2[1];
                tongjiYusB[1][zhnb] = tongji2[2];
                tongjiYusB[2][zhnb] = tongji2[3];
                tongjiYusB[3][zhnb] = tongji2[4];
                tongjiYusB[4][zhnb] = (double)zhnb * 0.05;

                if (slg == 0)
                {
                    AutoSaveFileShortb(zhn, cnt_flame, linenum, rutenum, rec_data_bufb);
                    AutoSaveFileAveStdb(zhn, cnt_flame, linenum, rutenum, tongji2);
                }
                zhnb++;
            }
        }

/*        void AutoSaveResult()
        {
            if ((usingbs >= 0) && (tem_flg == 1))
            {
                AutoSaveFileByte();

                if(cnt_process[0] > 0)
                {
                    AutoSaveFileShortHeada();
                    AutoSaveFileAveStdHeada();
                }
                if (cnt_process[1] > 0)
                {
                    AutoSaveFileShortHeadb();
                    AutoSaveFileAveStdHeadb();
                }

                for (int i = 0; i < (cnt_process[0] + cnt_process[1]); i++)
                {
                    Buffer.BlockCopy(Trec_buf, usingbs + i * pernum, buf1, 0, pernum);
                    cnt_flame = buf1[2];
                    linenum = buf1[3];
                    rutenum = buf1[4];

                    if ((rutenum == 10) && (cnt_process[0] > 0))
                    {
                        tongji1[0] = 0;
                        tongji1[1] = 0;
                        tongji1[2] = (float)10000000.0;
                        tongji1[3] = 0;
                        tongji1[4] = 0;

                        int j = 0;
                        for (int m = 0; m < 2 * pernums; m += 2)
                        {
                            rec_data_bufa[j] = (ushort)((buf1[m + 5] << 8) + buf1[m + 6]);

                            tongji1[3] += (float)rec_data_bufa[j];
                            tongji1[4] += (float)rec_data_bufa[j] * (float)rec_data_bufa[j];
                            if (rec_data_bufa[j] > tongji1[1])
                            {
                                tongji1[1] = rec_data_bufa[j];
                            }
                            if (rec_data_bufa[j] < tongji1[2])
                            {
                                tongji1[2] = rec_data_bufa[j];
                            }
                            j++;
                        }

                        tongji1[0] = (float)pernums;
                        tongji1[3] = tongji1[3] / tongji1[0];
                        tongji1[4] = tongji1[4] / tongji1[0];
                        tongji1[4] = Math.Sqrt(tongji1[4] - tongji1[3] * tongji1[3]);

                        Buffer.BlockCopy(rec_data_bufa, 0, ydata_bufA, 2 * zhna * pernums, 2 * pernums);
                        tongjiYusA[0][zhna] = tongji1[1];
                        tongjiYusA[1][zhna] = tongji1[2];
                        tongjiYusA[2][zhna] = tongji1[3];
                        tongjiYusA[3][zhna] = tongji1[4];

                        AutoSaveFileShorta((i + 1), cnt_flame, linenum, rutenum, rec_data_bufa);
                        AutoSaveFileAveStda((i + 1), cnt_flame, linenum, rutenum, tongji1);
                        zhna++;
                    }

                    if ((rutenum == 11) && (cnt_process[1] > 0))
                    {
                        tongji2[0] = 0;
                        tongji2[1] = 0;
                        tongji2[2] = (float)10000000.0;
                        tongji2[3] = 0;
                        tongji2[4] = 0;

                        int j = 0;
                        for (int m = 0; m < 2 * pernums; m += 2)
                        {
                            rec_data_bufb[j] = (ushort)((buf1[m + 5] << 8) + buf1[m + 6]);

                            tongji2[3] += (float)rec_data_bufb[j];
                            tongji2[4] += (float)rec_data_bufb[j] * (float)rec_data_bufb[j];
                            if (rec_data_bufb[j] > tongji2[1])
                            {
                                tongji2[1] = rec_data_bufb[j];
                            }
                            if (rec_data_bufb[j] < tongji2[2])
                            {
                                tongji2[2] = rec_data_bufb[j];
                            }
                            j++;
                        }

                        tongji2[0] = (float)pernums;
                        tongji2[3] = tongji2[3] / tongji2[0];
                        tongji2[4] = tongji2[4] / tongji2[0];
                        tongji2[4] = Math.Sqrt(tongji2[4] - tongji2[3] * tongji2[3]);

                        Buffer.BlockCopy(rec_data_bufb, 0, ydata_bufB, 2 * zhnb * pernums, 2 * pernums);
                        tongjiYusB[0][zhnb] = tongji2[1];
                        tongjiYusB[1][zhnb] = tongji2[2];
                        tongjiYusB[2][zhnb] = tongji2[3];
                        tongjiYusB[3][zhnb] = tongji2[4];

                        AutoSaveFileShortb((i + 1), cnt_flame, linenum, rutenum, rec_data_bufb);
                        AutoSaveFileAveStdb((i + 1), cnt_flame, linenum, rutenum, tongji2);
                        zhnb++;
                    }
                }

                double a1a = 0;
                double s1a = 0;
                double a2a = 0;
                double s2a = 0;
                double a3a = 0;
                double s3a = 0;
                double a4a = 0;
                double s4a = 0;
                double a5a = 0;
                double s5a = 0;
                double c1a = 0;
                double c2a = 0;
                double c3a = 0;
                double c4a = 0;
                double c5a = 0;

                double a1b = 0;
                double s1b = 0;
                double a2b = 0;
                double s2b = 0;
                double a3b = 0;
                double s3b = 0;
                double a4b = 0;
                double s4b = 0;
                double a5b = 0;
                double s5b = 0;
                double c1b = 0;
                double c2b = 0;
                double c3b = 0;
                double c4b = 0;
                double c5b = 0;

                if ((cnt_process[0] > (grp_num + 13)) || (cnt_process[1] > (grp_num + 13)))
                {
                    if (cnt_process[0] > (grp_num + 13) && (cnt_process[0] > modelsta))
                    {
                        Buffer.BlockCopy(ydata_bufA, modelsta, cdata_buf[0], 0, 2 * grp_num * pernums);
                        Buffer.BlockCopy(tongjiYusA[0], modelsta, tongjiGrp[0], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusA[1], modelsta, tongjiGrp[1], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusA[2], modelsta, tongjiGrp[2], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusA[3], modelsta, tongjiGrp[3], 0, 8 * grp_num);

                        mcorrs0(cdata_buf[0], grp_num * pernums, out a1a, out s1a);
                        mcorrd0(tongjiGrp[0], grp_num, out a2a, out s2a);
                        mcorrd0(tongjiGrp[1], grp_num, out a3a, out s3a);
                        mcorrd0(tongjiGrp[2], grp_num, out a4a, out s4a);
                        mcorrd0(tongjiGrp[3], grp_num, out a5a, out s5a);

                        minaz[0] = 10.0;
                        minaz[1] = 10.0;
                        minaz[2] = 10.0;
                        minaz[3] = 10.0;
                        minaz[4] = 10.0;

                        minap[0] = -1;
                        minap[1] = -1;
                        minap[2] = -1;
                        minap[3] = -1;
                        minap[4] = -1;

                        corstpa = mstepa;
                        cornuma = 0;
                        AutoSaveFileCorrHeada();
                    }

                    if (cnt_process[1] > (grp_num + 13) && (cnt_process[1] > modelstb))
                    {
                        Buffer.BlockCopy(ydata_bufB, modelstb, cdata_buf[1], 0, 2 * grp_num * pernums);
                        Buffer.BlockCopy(tongjiYusB[0], modelstb, tongjiGrp[4], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusB[1], modelstb, tongjiGrp[5], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusB[2], modelstb, tongjiGrp[6], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusB[3], modelstb, tongjiGrp[7], 0, 8 * grp_num);

                        mcorrs0(cdata_buf[1], grp_num * pernums, out a1b, out s1b);
                        mcorrd0(tongjiGrp[4], grp_num, out a2b, out s2b);
                        mcorrd0(tongjiGrp[5], grp_num, out a3b, out s3b);
                        mcorrd0(tongjiGrp[6], grp_num, out a4b, out s4b);
                        mcorrd0(tongjiGrp[7], grp_num, out a5b, out s5b);

                        minbz[0] = 10.0;
                        minbz[1] = 10.0;
                        minbz[2] = 10.0;
                        minbz[3] = 10.0;
                        minbz[4] = 10.0;

                        minbp[0] = -1;
                        minbp[1] = -1;
                        minbp[2] = -1;
                        minbp[3] = -1;
                        minbp[4] = -1;

                        corstpb = mstepb;
                        cornumb = 0;
                        AutoSaveFileCorrHeadb();
                    }

                    for (int k = modelsta + cacsta; k < (cnt_process[0] - grp_num); k += corstpa)
                    {
                        if (k < (cnt_process[0] - grp_num))
                        {
                            Buffer.BlockCopy(ydata_bufA, 2 * k * pernums, tdata_buf[0], 0, 2 * grp_num * pernums);
                            Buffer.BlockCopy(tongjiYusA[0], 8 * k, tongjiTot[0], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusA[1], 8 * k, tongjiTot[1], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusA[2], 8 * k, tongjiTot[2], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusA[3], 8 * k, tongjiTot[3], 0, 8 * grp_num);

                            c1a = mcorrs(cdata_buf[0], a1a, s1a, tdata_buf[0], grp_num * pernums);
                            if (c1a < minaz[0])
                            {
                                minaz[0] = c1a;
                                minap[0] = k;
                            }
                            checkas[cornuma] = c1a;

                            c2a = mcorrd(tongjiGrp[0], a2a, s2a, tongjiTot[0], grp_num);
                            if (c2a < minaz[1])
                            {
                                minaz[1] = c2a;
                                minap[1] = k;
                            }
                            checkad[0][cornuma] = c2a;
                            c3a = mcorrd(tongjiGrp[1], a3a, s3a, tongjiTot[1], grp_num);
                            if (c3a < minaz[2])
                            {
                                minaz[2] = c3a;
                                minap[2] = k;
                            }
                            checkad[1][cornuma] = c3a;
                            c4a = mcorrd(tongjiGrp[2], a4a, s4a, tongjiTot[2], grp_num);
                            if (c4a < minaz[3])
                            {
                                minaz[3] = c4a;
                                minap[3] = k;
                            }
                            checkad[2][cornuma] = c4a;
                            c5a = mcorrd(tongjiGrp[3], a5a, s5a, tongjiTot[3], grp_num);
                            if (c5a < minaz[4])
                            {
                                minaz[4] = c5a;
                                minap[4] = k;
                            }
                            checkad[3][cornuma] = c5a;

                            AutoSaveFileCorra(cornuma, linenum, 10);
                            cornuma++;
                        }
                    }
                    if (cnt_process[0] > (grp_num + 13))
                    {
                        AutoSaveFileCorrTaila();
                    }

                    for (int k = modelstb + cacstb; k < (cnt_process[1] - grp_num); k += corstpb)
                    {
                        if (k < (cnt_process[1] - grp_num))
                        {
                            Buffer.BlockCopy(ydata_bufB, 2 * k * pernums, tdata_buf[1], 0, 2 * grp_num * pernums);
                            Buffer.BlockCopy(tongjiYusB[0], 8 * k, tongjiTot[4], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusB[1], 8 * k, tongjiTot[5], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusB[2], 8 * k, tongjiTot[6], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusB[3], 8 * k, tongjiTot[7], 0, 8 * grp_num);

                            c1b = mcorrs(cdata_buf[1], a1b, s1b, tdata_buf[1], grp_num * pernums);
                            if (c1b < minbz[0])
                            {
                                minbz[0] = c1b;
                                minbp[0] = k;
                            }
                            checkbs[cornumb] = c1b;

                            c2b = mcorrd(tongjiGrp[4], a2b, s2b, tongjiTot[4], grp_num);
                            if (c2b < minbz[1])
                            {
                                minbz[1] = c2b;
                                minbp[1] = k;
                            }
                            checkbd[0][cornumb] = c2b;
                            c3b = mcorrd(tongjiGrp[5], a3b, s3b, tongjiTot[5], grp_num);
                            if (c3b < minbz[2])
                            {
                                minbz[2] = c3b;
                                minbp[2] = k;
                            }
                            checkbd[1][cornumb] = c3b;
                            c4b = mcorrd(tongjiGrp[6], a4b, s4b, tongjiTot[6], grp_num);
                            if (c4b < minbz[3])
                            {
                                minbz[3] = c4b;
                                minbp[3] = k;
                            }
                            checkbd[2][cornumb] = c4b;
                            c5b = mcorrd(tongjiGrp[7], a5b, s5b, tongjiTot[7], grp_num);
                            if (c5b < minbz[4])
                            {
                                minbz[4] = c5b;
                                minbp[4] = k;
                            }
                            checkbd[3][cornumb] = c5b;

                            AutoSaveFileCorrb(cornumb, linenum, 11);
                            cornumb++;
                        }
                    }

                    if (cnt_process[1] > (grp_num + 13))
                    {
                        AutoSaveFileCorrTailb();
                    }
                }

                System.Windows.Forms.MessageBox.Show("数据自动保存完成！", "完成通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
*/
/*        void AutoAnaylise()
        {
            int gznum = 0;
            int tem = 0;
            //int thdcnt = 0;
            //int thdper = 0;
            //float[][] threshold = new float[10][4];

            //float[] gzres = new float[30000];
            //string[] gzstr = { "正常", "磨损", "锈蚀", "断丝", "其它" };
            for (int i = 0; i < cornuma; i++)
            {
                if ((checkad[2][i] < threshold[0][0]) && (checkad[2][i] >= threshold[0][3]))
                {
                    if ((checkad[2][i] < threshold[0][0]) && (checkad[2][i] >= threshold[0][1]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 1;
                        gzres[gznum * 5 + 5] = 1;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    else if ((checkad[2][i] < threshold[0][1]) && (checkad[2][i] >= threshold[0][2]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 1;
                        gzres[gznum * 5 + 5] = 2;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    else
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 1;
                        gzres[gznum * 5 + 5] = 3;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    gznum++;
                }
                else if ((checkad[2][i] < threshold[1][0]) && (checkad[2][i] >= threshold[1][3]))
                {
                    if ((checkad[2][i] < threshold[1][0]) && (checkad[2][i] >= threshold[1][1]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 2;
                        gzres[gznum * 5 + 5] = 1;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    else if ((checkad[2][i] < threshold[1][1]) && (checkad[2][i] >= threshold[1][2]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 2;
                        gzres[gznum * 5 + 5] = 2;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);

                    }
                    else
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 2;
                        gzres[gznum * 5 + 5] = 3;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    gznum++;
                }
                else if ((checkad[2][i] < threshold[2][0]) && (checkad[2][i] >= threshold[2][3]))
                {
                    if ((checkad[2][i] < threshold[2][0]) && (checkad[2][i] >= threshold[2][1]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 3;
                        gzres[gznum * 5 + 5] = 1;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    else if ((checkad[2][i] < threshold[2][1]) && (checkad[2][i] >= threshold[2][2]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 3;
                        gzres[gznum * 5 + 5] = 2;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    else
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 10;
                        gzres[gznum * 5 + 4] = 3;
                        gzres[gznum * 5 + 5] = 3;
                        gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);
                    }
                    gznum++;
                }
                else if (checkad[2][i] < threshold[2][3])
                {
                    gzres[gznum * 5 + 2] = linenum;
                    gzres[gznum * 5 + 3] = 10;
                    gzres[gznum * 5 + 4] = 4;
                    gzres[gznum * 5 + 5] = 1;
                    gzres[gznum * 5 + 6] = (float)(modelsta + cacsta + i * corstpa);

                    gznum++;
                }
                else
                {
                    continue;
                }
            }

            tbxRecvData.Text = "";
            strBuilder.Clear();
            if (gznum > 0)
            {
                gzres[0] = gznum;
                for (int i = 0; i < (int)gzres[0]; i++)
                {
                    tbxResult1.Text = gzstr[(int)gzres[i * 5 + 4]];
                    picbxRes1.Image = imageGif; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
                    tbxResultPos1.Text = ((float)((int)gzres[i * 5 + 6]) * 0.05F).ToString("0.00") + "s";

                    strBuilder.Append("W" + (i + 1).ToString("000").PadRight(6, ' ') + ((int)gzres[i * 5 + 2]).ToString().PadRight(5, ' ') + ((int)gzres[i * 5 + 3]).ToString("X2").PadRight(5, ' ') + gzstr[(int)gzres[i * 5 + 4]].PadRight(6, ' ') + ((int)gzres[i * 5 + 5]).ToString().PadRight(5, ' ') + (((float)((int)gzres[i * 5 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + "\r\n");
                    }

                tbxRecvData.Text = strBuilder.ToString();
                AutoSaveFileAnalizea();
            }

            for (int i = 0; i < cornumb; i++)
            {
                if ((checkbd[2][i] < threshold[0][0]) && (checkbd[2][i] >= threshold[0][3]))
                {
                    if ((checkbd[2][i] < threshold[0][0]) && (checkbd[2][i] >= threshold[0][1]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 1;
                        gzres[gznum * 5 + 5] = 1;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    else if ((checkbd[2][i] < threshold[0][1]) && (checkbd[2][i] >= threshold[0][2]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 1;
                        gzres[gznum * 5 + 5] = 2;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    else
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 1;
                        gzres[gznum * 5 + 5] = 3;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    gznum++;
                }
                else if ((checkbd[2][i] < threshold[1][0]) && (checkbd[2][i] >= threshold[1][3]))
                {
                    if ((checkbd[2][i] < threshold[1][0]) && (checkbd[2][i] >= threshold[1][1]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 2;
                        gzres[gznum * 5 + 5] = 1;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    else if ((checkbd[2][i] < threshold[1][1]) && (checkbd[2][i] >= threshold[1][2]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 2;
                        gzres[gznum * 5 + 5] = 2;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);

                    }
                    else
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 2;
                        gzres[gznum * 5 + 5] = 3;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    gznum++;
                }
                else if ((checkbd[2][i] < threshold[2][0]) && (checkbd[2][i] >= threshold[2][3]))
                {
                    if ((checkbd[2][i] < threshold[2][0]) && (checkbd[2][i] >= threshold[2][1]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 3;
                        gzres[gznum * 5 + 5] = 1;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    else if ((checkbd[2][i] < threshold[2][1]) && (checkbd[2][i] >= threshold[2][2]))
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 3;
                        gzres[gznum * 5 + 5] = 2;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    else
                    {
                        gzres[gznum * 5 + 2] = linenum;
                        gzres[gznum * 5 + 3] = 11;
                        gzres[gznum * 5 + 4] = 3;
                        gzres[gznum * 5 + 5] = 3;
                        gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);
                    }
                    gznum++;
                }
                else if (checkbd[2][i] < threshold[2][3])
                {
                    gzres[gznum * 5 + 2] = linenum;
                    gzres[gznum * 5 + 3] = 11;
                    gzres[gznum * 5 + 4] = 4;
                    gzres[gznum * 5 + 5] = 1;
                    gzres[gznum * 5 + 6] = (float)(modelstb + cacstb + i * corstpb);

                    gznum++;
                }
                else
                {
                    continue;
                }
            }
            if (gznum > (int)gzres[0])
            {
                strBuilder.Clear();
                gzres[1] = (float)gznum - gzres[0];
                tem = (int)gzres[0] * 5;
                for (int i = 0; i < gzres[1]; i++)
                {
                    tbxResult2.Text = gzstr[(int)gzres[tem + i * 5 + 4]];
                    picbxRes2.Image = imageGif; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
                    tbxResultPos2.Text = ((float)((int)gzres[tem + i * 5 + 6]) * 0.05F).ToString("0.00") + "s";

                    strBuilder.Append("W" + (gzres[0] + i + 1).ToString("000").PadRight(6, ' ') + ((int)gzres[tem + i * 5 + 2]).ToString().PadRight(5, ' ') + ((int)gzres[tem + i * 5 + 3]).ToString("X2").PadRight(5, ' ') + gzstr[(int)gzres[tem + i * 5 + 4]].PadRight(6, ' ') + ((int)gzres[tem + i * 5 + 5]).ToString().PadRight(5, ' ') + (((float)((int)gzres[tem + i * 5 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + "\r\n");
                }
                tbxRecvData.Text += strBuilder.ToString();
                AutoSaveFileAnalizeb();
            }
        }*/
        
/*        void FindPeek(int st, int ed)
        {
            int mflg = 0;
//            int peknum = 0;
            double mave = 0;
            double mave1 = 0;
            int mtem = 0;
            double mave30 = 0;
            double mave31 = 0;
            double mave32 = 0;

            for (int i = st; i < ed; i++)
            {
                mflg = 0;
                mave = 0;
                mave30 = 0;
                mave31 = 0;
                mave32 = 0;
                mave1 = 0;
                for (int j = 1; j <= mstepas; j++)
                {
                    mave += (tongjiYusA[3][i - j] + tongjiYusA[3][i + j]) / 2.0;
                    mave30 += (tongjiYusA[0][i - j] + tongjiYusA[0][i + j]) / 2.0;
                    mave31 += (tongjiYusA[1][i - j] + tongjiYusA[1][i + j]) / 2.0;
                    mave32 += (tongjiYusA[2][i - j] + tongjiYusA[2][i + j]) / 2.0;

                    if ((tongjiYusA[3][i] > tongjiYusA[3][i - j]) && (tongjiYusA[3][i] > tongjiYusA[3][i + j]))
                    {
                        mave1 += (tongjiYusA[2][i] - tongjiYusA[2][i - j]) + (tongjiYusA[2][i] - tongjiYusA[2][i + j]);
                        mflg++;
                    }
                }
                mave /= (double)mstepas;
                mave30 /= (double)mstepas;
                mave31 /= (double)mstepas;
                mave32 /= (double)mstepas;

                if (mflg == mstepas)
                {
                    if ((tongjiYusA[0][i] > (2.1 * mave30)) &&
                        (tongjiYusA[2][i] > (1.6 * mave32)) &&
                        (tongjiYusA[3][i] > (5.0 * mave)))
                    {
                        if (mave1 > 0)
                        {
                            mpeek[peknum * 13 + 4] = 0;
                        }
                        else
                        {
                            mpeek[peknum * 13 + 4] = 1;
                        }
                        mpeek[peknum * 13 + 2] = linenum;
                        mpeek[peknum * 13 + 3] = 10;
                        mpeek[peknum * 13 + 5] = 3;
                        mpeek[peknum * 13 + 6] = i;
                        mpeek[peknum * 13 + 7] = (float)tongjiYusA[0][i];
                        mpeek[peknum * 13 + 8] = (float)mave30;
                        mpeek[peknum * 13 + 9] = (float)tongjiYusA[1][i];
                        mpeek[peknum * 13 + 10] = (float)mave31;
                        mpeek[peknum * 13 + 11] = (float)tongjiYusA[2][i];
                        mpeek[peknum * 13 + 12] = (float)mave32;
                        mpeek[peknum * 13 + 13] = (float)tongjiYusA[3][i];
                        mpeek[peknum * 13 + 14] = (float)mave;


                        peknum++;
                    }
                }
            }

            tbxRecvData.Text = "";
            strBuilder.Clear();
            if(peknum > 0)
            {
                mpeek[0] = peknum;
                for (int i = 0; i < (int)mpeek[0]; i++)
                {
                    tbxResult1.Text = gzstr[(int)mpeek[i * 13 + 5]];
                    picbxRes1.Image = imageGif; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
                    tbxResultPos1.Text = ((float)((int)mpeek[i * 13 + 6]) * 0.05F).ToString("0.00") + "s";

//                    strBuilder.Append("W" + (i + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[i * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[i * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[i * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[i * 13 + 5]].PadRight(6, ' ') +  (((float)((int)mpeek[i * 13 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + (mpeek[i * 13 + 7]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 8]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 9]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 10]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 11]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 12]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 13]).ToString("0.00").PadRight(10, ' ') + (mpeek[i * 13 + 14]).ToString("0.00").PadRight(10, ' ') + "\r\n");
                    strBuilder.Append("W" + (i + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[i * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[i * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[i * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[i * 13 + 5]].PadRight(6, ' ') +  (((float)((int)mpeek[i * 13 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + "\r\n");
                }

                tbxRecvData.Text = strBuilder.ToString();
                AutoSaveFileAnalizea();
            }


            for (int i = st; i < ed; i++)
            {
                mflg = 0;
                mave = 0;
                mave30 = 0;
                mave31 = 0;
                mave32 = 0;
                mave1 = 0;
                for (int j = 1; j <= mstepbs; j++)
                {
                    mave += (tongjiYusB[3][i - j] + tongjiYusB[3][i + j]) / 2.0;
                    mave30 += (tongjiYusB[0][i - j] + tongjiYusB[0][i + j]) / 2.0;
                    mave31 += (tongjiYusB[1][i - j] + tongjiYusB[1][i + j]) / 2.0;
                    mave32 += (tongjiYusB[2][i - j] + tongjiYusB[2][i + j]) / 2.0;

                    if ((tongjiYusB[3][i] > tongjiYusB[3][i - j]) && (tongjiYusB[3][i] > tongjiYusB[3][i + j]))
                    {
                        mave1 += (tongjiYusB[2][i] - tongjiYusB[2][i - j]) + (tongjiYusB[2][i] - tongjiYusB[2][i + j]);
                        mflg++;
                    }
                }
                mave /= (double)mstepbs;
                mave30 /= (double)mstepbs;
                mave31 /= (double)mstepbs;
                mave32 /= (double)mstepbs;

                if (mflg == mstepbs)
                {
                    if ((tongjiYusB[0][i] > (2.1 * mave30)) &&
                        (tongjiYusB[2][i] > (1.6 * mave32)) &&
                        (tongjiYusB[3][i] > (5.0 * mave)))
                    {
                        if (mave1 > 0)
                        {
                            mpeek[peknum * 13 + 4] = 0;
                        }
                        else
                        {
                            mpeek[peknum * 13 + 4] = 1;
                        }
                        mpeek[peknum * 13 + 2] = linenum;
                        mpeek[peknum * 13 + 3] = 11;
                        mpeek[peknum * 13 + 5] = 3;
                        mpeek[peknum * 13 + 6] = i;
                        mpeek[peknum * 13 + 7] = (float)tongjiYusB[0][i];
                        mpeek[peknum * 13 + 8] = (float)mave30;
                        mpeek[peknum * 13 + 9] = (float)tongjiYusB[1][i];
                        mpeek[peknum * 13 + 10] = (float)mave31;
                        mpeek[peknum * 13 + 11] = (float)tongjiYusB[2][i];
                        mpeek[peknum * 13 + 12] = (float)mave32;
                        mpeek[peknum * 13 + 13] = (float)tongjiYusB[3][i];
                        mpeek[peknum * 13 + 14] = (float)mave;

                        peknum++;
                    }
                }
            }

            if (peknum > (int)mpeek[0])
            {
                strBuilder.Clear();
                mpeek[1] = (float)peknum - mpeek[0];
                mtem = (int)mpeek[0] * 13;
                for (int i = 0; i < (int)mpeek[1]; i++)
                {
                    tbxResult2.Text = gzstr[(int)mpeek[mtem + i * 13 + 5]];
                    picbxRes2.Image = imageGif; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
                    tbxResultPos2.Text = ((float)((int)mpeek[mtem + i * 13 + 6]) * 0.05F).ToString("0.00") + "s";

//                    strBuilder.Append("W" + (i + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[mtem + i * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[mtem + i * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[mtem + i * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[mtem + i * 13 + 5]].PadRight(6, ' ') + (((float)((int)mpeek[mtem + i * 13 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 7]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 8]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 9]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 10]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 11]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 12]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 13]).ToString("0.00").PadRight(10, ' ') + (mpeek[mtem + i * 13 + 14]).ToString("0.00").PadRight(10, ' ') + "\r\n");
                    strBuilder.Append("W" + (i + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[mtem + i * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[mtem + i * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[mtem + i * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[mtem + i * 13 + 5]].PadRight(6, ' ') + (((float)((int)mpeek[mtem + i * 13 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + "\r\n");
                }

                tbxRecvData.Text += strBuilder.ToString();
                AutoSaveFileAnalizeb();
            }
        }*/
        void FindPeekSsa()
        {
            int mflg0 = 0;
            int mflg1 = 0;
            double mave = 0;
            double mave1 = 0;
            double mave30 = 0;
            double mave31 = 0;
            double mave32 = 0;
            int i = 0;

            if (zhna > 2 * mstepas)
            {
                i = zhna - mstepas - 1;
                mflg0 = 0;
                mflg1 = 0;
                mave = 0;
                mave30 = 0;
                mave31 = 0;
                mave32 = 0;
                mave1 = 0;
                for (int j = 1; j <= mstepas; j++)
                {
                    mave += (tongjiYusA[3][i - j] + tongjiYusA[3][i + j]) / 2.0;
                    mave30 += (tongjiYusA[0][i - j] + tongjiYusA[0][i + j]) / 2.0;
                    mave31 += (tongjiYusA[1][i - j] + tongjiYusA[1][i + j]) / 2.0;
                    mave32 += (tongjiYusA[2][i - j] + tongjiYusA[2][i + j]) / 2.0;

/*                    if ((tongjiYusA[3][i] > tongjiYusA[3][i - j]) && (tongjiYusA[3][i] > tongjiYusA[3][i + j]))
                    {
                        mave1 += (tongjiYusA[2][i] - tongjiYusA[2][i - j]) + (tongjiYusA[2][i] - tongjiYusA[2][i + j]);
                        mflg++;
                    }
*/
                    if ((tongjiYusA[2][i] > tongjiYusA[2][i - j]) && (tongjiYusA[2][i] > tongjiYusA[2][i + j]))
                    {
                        mave1 += (tongjiYusA[2][i] - tongjiYusA[2][i - j]) + (tongjiYusA[2][i] - tongjiYusA[2][i + j]);
                        mflg0++;
                    }
                    else if ((tongjiYusA[2][i] < tongjiYusA[2][i - j]) && (tongjiYusA[2][i] < tongjiYusA[2][i + j]))
                    {
                        mave1 += (tongjiYusA[2][i] - tongjiYusA[2][i - j]) + (tongjiYusA[2][i] - tongjiYusA[2][i + j]);
                        mflg1++;
                    }
                }
                mave /= (double)mstepas;
                mave30 /= (double)mstepas;
                mave31 /= (double)mstepas;
                mave32 /= (double)mstepas;

                if ((mflg0 == mstepas) || (mflg1 == mstepas))
                {
/*                    if ((tongjiYusA[0][i] > (2.1 * mave30)) &&
                        (tongjiYusA[2][i] > (1.6 * mave32)) &&
                        (tongjiYusA[3][i] > (5.0 * mave)))*/
////                    if ((tongjiYusA[0][i] > (1.2 * mave30)) || (tongjiYusA[0][i] < (0.8 * mave30)) ||
////                        (tongjiYusA[1][i] > (1.2 * mave31)) || (tongjiYusA[1][i] < (0.8 * mave31)) ||
////                        (tongjiYusA[2][i] > (1.2 * mave32)) || (tongjiYusA[2][i] < (0.8 * mave32)))
//                        (tongjiYusA[3][i] > (1.2 * mave)) || (tongjiYusA[3][i] < (0.8 * mave)))
                    if (tongjiYusA[3][i] > (thresholdc[0] * mave))
                    {
                        if (mave1 > 0)
                        {
                            mpeek[peknum * 13 + 4] = 0;
                        }
                        else
                        {
                            mpeek[peknum * 13 + 4] = 1;
                        }
                        mpeek[peknum * 13 + 2] = linenum;
                        mpeek[peknum * 13 + 3] = 10;
                        mpeek[peknum * 13 + 5] = 3;
                        mpeek[peknum * 13 + 6] = i;
                        mpeek[peknum * 13 + 7] = (float)tongjiYusA[0][i];
                        mpeek[peknum * 13 + 8] = (float)mave30;
                        mpeek[peknum * 13 + 9] = (float)tongjiYusA[1][i];
                        mpeek[peknum * 13 + 10] = (float)mave31;
                        mpeek[peknum * 13 + 11] = (float)tongjiYusA[2][i];
                        mpeek[peknum * 13 + 12] = (float)mave32;
                        mpeek[peknum * 13 + 13] = (float)tongjiYusA[3][i];
                        mpeek[peknum * 13 + 14] = (float)mave;

                        peknum++;
                    }
                }

                strBuilder0.Clear();
                if (peknum > ((int)mpeek[0] + (int)mpeek[1]))
                {
                    tbxResult1.Text = gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]];
                    picbxRes1.Image = imageGif; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
                    tbxResultPos1.Text = tongjiYusA[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s";
                    tbxState.Text = tongjiYusA[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s";

                    strBuilder0.Append("W" + (((int)mpeek[0] + (int)mpeek[1]) + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]].PadRight(6, ' ') + (tongjiYusA[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s").PadRight(10, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 7]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 8]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 9]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 10]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 11]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 12]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 13]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 14]).ToString("0.00").PadRight(12, ' '));
                    strBuilder.Insert(0, ("W" + (((int)mpeek[0] + (int)mpeek[1]) + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]].PadRight(6, ' ') + (tongjiYusA[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s").PadRight(10, ' ')
                                          + "\r\n"));
                    mpeek[0]++;

                    tbxRecvData.Text = strBuilder.ToString();
                    AutoSaveFileAnalizea();
                }
            }
        }

        void FindPeekSsb()
        {
            int mflg0 = 0;
            int mflg1 = 0;
            double mave = 0;
            double mave1 = 0;
            //            int mtem = 0;
            double mave30 = 0;
            double mave31 = 0;
            double mave32 = 0;
            int i = 0;

            if (zhnb > 2 * mstepbs)
            {
                i = zhnb - mstepbs - 1;
                mflg0 = 0;
                mflg1 = 0;
                mave = 0;
                mave30 = 0;
                mave31 = 0;
                mave32 = 0;
                mave1 = 0;
                for (int j = 1; j <= mstepbs; j++)
                {
                    mave += (tongjiYusB[3][i - j] + tongjiYusB[3][i + j]) / 2.0;
                    mave30 += (tongjiYusB[0][i - j] + tongjiYusB[0][i + j]) / 2.0;
                    mave31 += (tongjiYusB[1][i - j] + tongjiYusB[1][i + j]) / 2.0;
                    mave32 += (tongjiYusB[2][i - j] + tongjiYusB[2][i + j]) / 2.0;

                    if ((tongjiYusB[2][i] > tongjiYusB[2][i - j]) && (tongjiYusB[2][i] > tongjiYusB[2][i + j]))
                    {
                        mave1 += (tongjiYusB[2][i] - tongjiYusB[2][i - j]) + (tongjiYusB[2][i] - tongjiYusB[2][i + j]);
                        mflg0++;
                    }
                    else if ((tongjiYusB[2][i] < tongjiYusB[2][i - j]) && (tongjiYusB[2][i] < tongjiYusB[2][i + j]))
                    {
                        mave1 += (tongjiYusB[2][i] - tongjiYusB[2][i - j]) + (tongjiYusB[2][i] - tongjiYusB[2][i + j]);
                        mflg1++;
                    }
                }
                mave /= (double)mstepbs;
                mave30 /= (double)mstepbs;
                mave31 /= (double)mstepbs;
                mave32 /= (double)mstepbs;

                if ((mflg0 == mstepbs) || (mflg1 == mstepbs))
                {
//                    if ((tongjiYusB[0][i] > (2.1 * mave30)) &&
//                        (tongjiYusB[2][i] > (1.6 * mave32)) &&
//                        (tongjiYusB[3][i] > (5.0 * mave)))
////                    if ((tongjiYusB[0][i] > (1.2 * mave30)) || (tongjiYusB[0][i] < (0.8 * mave30)) ||
////                        (tongjiYusB[1][i] > (1.2 * mave31)) || (tongjiYusB[1][i] < (0.8 * mave31)) ||
////                        (tongjiYusB[2][i] > (1.2 * mave32)) || (tongjiYusB[2][i] < (0.8 * mave32)))
//                        (tongjiYusB[3][i] > (1.2 * mave)) || (tongjiYusB[3][i] < (0.8 * mave)))
                    if (tongjiYusB[3][i] > (thresholdd[0] * mave))
                    {
                        if (mave1 > 0)
                        {
                            mpeek[peknum * 13 + 4] = 0;
                        }
                        else
                        {
                            mpeek[peknum * 13 + 4] = 1;
                        }
                        mpeek[peknum * 13 + 2] = linenum;
                        mpeek[peknum * 13 + 3] = 11;
                        mpeek[peknum * 13 + 5] = 3;
                        mpeek[peknum * 13 + 6] = i;
                        mpeek[peknum * 13 + 7] = (float)tongjiYusB[0][i];
                        mpeek[peknum * 13 + 8] = (float)mave30;
                        mpeek[peknum * 13 + 9] = (float)tongjiYusB[1][i];
                        mpeek[peknum * 13 + 10] = (float)mave31;
                        mpeek[peknum * 13 + 11] = (float)tongjiYusB[2][i];
                        mpeek[peknum * 13 + 12] = (float)mave32;
                        mpeek[peknum * 13 + 13] = (float)tongjiYusB[3][i];
                        mpeek[peknum * 13 + 14] = (float)mave;
                        peknum++;
                    }
                }

                strBuilder0.Clear();
                if (peknum > ((int)mpeek[0] + (int)mpeek[1]))
                {
                    tbxResult2.Text = gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]];
////                    picbxRes2.Image = imageGif; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
                    tbxResultPos2.Text = tongjiYusB[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s";
                    tbxState.Text = tongjiYusB[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s";

//                    strBuilder.Append("W" + (((int)mpeek[0] + (int)mpeek[1]) + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]].PadRight(6, ' ') + (((float)((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]) * 0.05F).ToString("0.00") + "s").PadRight(10, ' ') + "\r\n");
                    strBuilder0.Append("W" + (((int)mpeek[0] + (int)mpeek[1]) + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]].PadRight(6, ' ') + (tongjiYusB[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s").PadRight(10, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 7]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 8]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 9]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 10]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 11]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 12]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 13]).ToString("0.00").PadRight(12, ' ')
                                          + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 14]).ToString("0.00").PadRight(12, ' '));
                    strBuilder.Insert(0, ("W" + (((int)mpeek[0] + (int)mpeek[1]) + 1).ToString("000").PadRight(6, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 2]).ToString().PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 3]).ToString("X2").PadRight(5, ' ') + ((int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 4]).ToString().PadRight(5, ' ') + gzstr[(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 5]].PadRight(6, ' ') + (tongjiYusB[4][(int)mpeek[((int)mpeek[0] + (int)mpeek[1]) * 13 + 6]].ToString("0.00") + "s").PadRight(10, ' ')
                                          + "\r\n"));

                    mpeek[1]++;
//                    tbxRecvData.Text.Insert(0, strBuilder.ToString());
                    tbxRecvData.Text = strBuilder.ToString();
                    AutoSaveFileAnalizeb();
                }
            }
        }

/*        void HoldSaveResult()
        {
            if (usingbs >= 0)
            {
                AutoSaveFileByte();

                if (cnt_process[0] > 0)
                {
                    AutoSaveFileShortHeada();
                    AutoSaveFileAveStdHeada();
                }
                if (cnt_process[1] > 0)
                {
                    AutoSaveFileShortHeadb();
                    AutoSaveFileAveStdHeadb();
                }

                for (int i = 2 * startz; i < (2 * startz + cnt_process[0] + cnt_process[1]); i++)
                {
                    Buffer.BlockCopy(Trec_buf, usingbs + i * pernum, buf1, 0, pernum);
                    cnt_flame = buf1[2];
                    linenum = buf1[3];
                    rutenum = buf1[4];

                    if ((rutenum == 10) && (cnt_process[0] > 0))
                    {
                        tongji1[0] = 0;
                        tongji1[1] = 0;
                        tongji1[2] = (float)10000000.0;
                        tongji1[3] = 0;
                        tongji1[4] = 0;

                        int j = 0;
                        for (int m = 0; m < 2 * pernums; m += 2)
                        {
                            rec_data_bufa[j] = (ushort)((buf1[m + 5] << 8) + buf1[m + 6]);

                            tongji1[3] += (float)rec_data_bufa[j];
                            tongji1[4] += (float)rec_data_bufa[j] * (float)rec_data_bufa[j];
                            if (rec_data_bufa[j] > tongji1[1])
                            {
                                tongji1[1] = rec_data_bufa[j];
                            }
                            if (rec_data_bufa[j] < tongji1[2])
                            {
                                tongji1[2] = rec_data_bufa[j];
                            }
                            j++;
                        }

                        tongji1[0] = (float)pernums;
                        tongji1[3] = tongji1[3] / tongji1[0];
                        tongji1[4] = tongji1[4] / tongji1[0];
                        tongji1[4] = Math.Sqrt(tongji1[4] - tongji1[3] * tongji1[3]);

                        Buffer.BlockCopy(rec_data_bufa, 0, ydata_bufA, 2 * zhna * pernums, 2 * pernums);
                        tongjiYusA[0][zhna] = tongji1[1];
                        tongjiYusA[1][zhna] = tongji1[2];
                        tongjiYusA[2][zhna] = tongji1[3];
                        tongjiYusA[3][zhna] = tongji1[4];

                        AutoSaveFileShorta((i + 1), cnt_flame, linenum, rutenum, rec_data_bufa);
                        AutoSaveFileAveStda((i + 1), cnt_flame, linenum, rutenum, tongji1);
                        zhna++;
                    }

                    if ((rutenum == 11) && (cnt_process[1] > 0))
                    {
                        tongji2[0] = 0;
                        tongji2[1] = 0;
                        tongji2[2] = (float)10000000.0;
                        tongji2[3] = 0;
                        tongji2[4] = 0;

                        int j = 0;
                        for (int m = 0; m < 2 * pernums; m += 2)
                        {
                            rec_data_bufb[j] = (ushort)((buf1[m + 5] << 8) + buf1[m + 6]);

                            tongji2[3] += (float)rec_data_bufb[j];
                            tongji2[4] += (float)rec_data_bufb[j] * (float)rec_data_bufb[j];
                            if (rec_data_bufb[j] > tongji2[1])
                            {
                                tongji2[1] = rec_data_bufb[j];
                            }
                            if (rec_data_bufb[j] < tongji2[2])
                            {
                                tongji2[2] = rec_data_bufb[j];
                            }
                            j++;
                        }

                        tongji2[0] = (float)pernums;
                        tongji2[3] = tongji2[3] / tongji2[0];
                        tongji2[4] = tongji2[4] / tongji2[0];
                        tongji2[4] = Math.Sqrt(tongji2[4] - tongji2[3] * tongji2[3]);

                        Buffer.BlockCopy(rec_data_bufb, 0, ydata_bufB, 2 * zhnb * pernums, 2 * pernums);
                        tongjiYusB[0][zhnb] = tongji2[1];
                        tongjiYusB[1][zhnb] = tongji2[2];
                        tongjiYusB[2][zhnb] = tongji2[3];
                        tongjiYusB[3][zhnb] = tongji2[4];

                        AutoSaveFileShortb((i + 1), cnt_flame, linenum, rutenum, rec_data_bufb);
                        AutoSaveFileAveStdb((i + 1), cnt_flame, linenum, rutenum, tongji2);
                        zhnb++;
                    }
                }

                double a1a = 0;
                double s1a = 0;
                double a2a = 0;
                double s2a = 0;
                double a3a = 0;
                double s3a = 0;
                double a4a = 0;
                double s4a = 0;
                double a5a = 0;
                double s5a = 0;
                double c1a = 0;
                double c2a = 0;
                double c3a = 0;
                double c4a = 0;
                double c5a = 0;

                double a1b = 0;
                double s1b = 0;
                double a2b = 0;
                double s2b = 0;
                double a3b = 0;
                double s3b = 0;
                double a4b = 0;
                double s4b = 0;
                double a5b = 0;
                double s5b = 0;
                double c1b = 0;
                double c2b = 0;
                double c3b = 0;
                double c4b = 0;
                double c5b = 0;

                if ((cnt_process[0] > (grp_num + 13)) || (cnt_process[1] > (grp_num + 13)))
                {
                    if ((cnt_process[0] > (grp_num + 13)) && (cnt_process[0] > modelsta))
                    {
                        Buffer.BlockCopy(ydata_bufA, modelsta, cdata_buf[0], 0, 2 * grp_num * pernums);
                        Buffer.BlockCopy(tongjiYusA[0], modelsta, tongjiGrp[0], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusA[1], modelsta, tongjiGrp[1], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusA[2], modelsta, tongjiGrp[2], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusA[3], modelsta, tongjiGrp[3], 0, 8 * grp_num);

                        mcorrs0(cdata_buf[0], grp_num * pernums, out a1a, out s1a);
                        mcorrd0(tongjiGrp[0], grp_num, out a2a, out s2a);
                        mcorrd0(tongjiGrp[1], grp_num, out a3a, out s3a);
                        mcorrd0(tongjiGrp[2], grp_num, out a4a, out s4a);
                        mcorrd0(tongjiGrp[3], grp_num, out a5a, out s5a);

                        minaz[0] = 10.0;
                        minaz[1] = 10.0;
                        minaz[2] = 10.0;
                        minaz[3] = 10.0;
                        minaz[4] = 10.0;

                        minap[0] = -1;
                        minap[1] = -1;
                        minap[2] = -1;
                        minap[3] = -1;
                        minap[4] = -1;

                        corstpa = mstepa;
                        cornuma = 0;
                        AutoSaveFileCorrHeada();
                    }

                    if (cnt_process[1] > (grp_num + 13) && (cnt_process[1] > modelstb))
                    {
                        Buffer.BlockCopy(ydata_bufB, modelstb, cdata_buf[1], 0, 2 * grp_num * pernums);
                        Buffer.BlockCopy(tongjiYusB[0], modelstb, tongjiGrp[4], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusB[1], modelstb, tongjiGrp[5], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusB[2], modelstb, tongjiGrp[6], 0, 8 * grp_num);
                        Buffer.BlockCopy(tongjiYusB[3], modelstb, tongjiGrp[7], 0, 8 * grp_num);

                        mcorrs0(cdata_buf[1], grp_num * pernums, out a1b, out s1b);
                        mcorrd0(tongjiGrp[4], grp_num, out a2b, out s2b);
                        mcorrd0(tongjiGrp[5], grp_num, out a3b, out s3b);
                        mcorrd0(tongjiGrp[6], grp_num, out a4b, out s4b);
                        mcorrd0(tongjiGrp[7], grp_num, out a5b, out s5b);

                        minbz[0] = 10.0;
                        minbz[1] = 10.0;
                        minbz[2] = 10.0;
                        minbz[3] = 10.0;
                        minbz[4] = 10.0;

                        minbp[0] = -1;
                        minbp[1] = -1;
                        minbp[2] = -1;
                        minbp[3] = -1;
                        minbp[4] = -1;

                        corstpb = mstepb;
                        cornumb = 0;
                        AutoSaveFileCorrHeadb();
                    }

                    for (int k = modelsta + cacsta; k < (cnt_process[0] - grp_num); k += corstpa)
                    {
                        if (k < (cnt_process[0] - grp_num))
                        {
                            Buffer.BlockCopy(ydata_bufA, 2 * k * pernums, tdata_buf[0], 0, 2 * grp_num * pernums);
                            Buffer.BlockCopy(tongjiYusA[0], 8 * k, tongjiTot[0], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusA[1], 8 * k, tongjiTot[1], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusA[2], 8 * k, tongjiTot[2], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusA[3], 8 * k, tongjiTot[3], 0, 8 * grp_num);

                            c1a = mcorrs(cdata_buf[0], a1a, s1a, tdata_buf[0], grp_num * pernums);
                            if (c1a < minaz[0])
                            {
                                minaz[0] = c1a;
                                minap[0] = k;
                            }
                            checkas[cornuma] = c1a;

                            c2a = mcorrd(tongjiGrp[0], a2a, s2a, tongjiTot[0], grp_num);
                            if (c2a < minaz[1])
                            {
                                minaz[1] = c2a;
                                minap[1] = k;
                            }
                            checkad[0][cornuma] = c2a;
                            c3a = mcorrd(tongjiGrp[1], a3a, s3a, tongjiTot[1], grp_num);
                            if (c3a < minaz[2])
                            {
                                minaz[2] = c3a;
                                minap[2] = k;
                            }
                            checkad[1][cornuma] = c3a;
                            c4a = mcorrd(tongjiGrp[2], a4a, s4a, tongjiTot[2], grp_num);
                            if (c4a < minaz[3])
                            {
                                minaz[3] = c4a;
                                minap[3] = k;
                            }
                            checkad[2][cornuma] = c4a;
                            c5a = mcorrd(tongjiGrp[3], a5a, s5a, tongjiTot[3], grp_num);
                            if (c5a < minaz[4])
                            {
                                minaz[4] = c5a;
                                minap[4] = k;
                            }
                            checkad[3][cornuma] = c5a;

                            AutoSaveFileCorra(cornuma, linenum, 10);
                            cornuma++;
                        }
                    }
                    if (cnt_process[0] > (grp_num + 13))
                    {
                        AutoSaveFileCorrTaila();
                    }

                    for (int k = modelstb + cacstb; k < (cnt_process[1] - grp_num); k += corstpb)
                    {
                        if (k < (cnt_process[1] - grp_num))
                        {
                            Buffer.BlockCopy(ydata_bufB, 2 * k * pernums, tdata_buf[1], 0, 2 * grp_num * pernums);
                            Buffer.BlockCopy(tongjiYusB[0], 8 * k, tongjiTot[4], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusB[1], 8 * k, tongjiTot[5], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusB[2], 8 * k, tongjiTot[6], 0, 8 * grp_num);
                            Buffer.BlockCopy(tongjiYusB[3], 8 * k, tongjiTot[7], 0, 8 * grp_num);

                            c1b = mcorrs(cdata_buf[1], a1b, s1b, tdata_buf[1], grp_num * pernums);
                            if (c1b < minbz[0])
                            {
                                minbz[0] = c1b;
                                minbp[0] = k;
                            }
                            checkbs[cornumb] = c1b;

                            c2b = mcorrd(tongjiGrp[4], a2b, s2b, tongjiTot[4], grp_num);
                            if (c2b < minbz[1])
                            {
                                minbz[1] = c2b;
                                minbp[1] = k;
                            }
                            checkbd[0][cornumb] = c2b;
                            c3b = mcorrd(tongjiGrp[5], a3b, s3b, tongjiTot[5], grp_num);
                            if (c3b < minbz[2])
                            {
                                minbz[2] = c3b;
                                minbp[2] = k;
                            }
                            checkbd[1][cornumb] = c3b;
                            c4b = mcorrd(tongjiGrp[6], a4b, s4b, tongjiTot[6], grp_num);
                            if (c4b < minbz[3])
                            {
                                minbz[3] = c4b;
                                minbp[3] = k;
                            }
                            checkbd[2][cornumb] = c4b;
                            c5b = mcorrd(tongjiGrp[7], a5b, s5b, tongjiTot[7], grp_num);
                            if (c5b < minbz[4])
                            {
                                minbz[4] = c5b;
                                minbp[4] = k;
                            }
                            checkbd[3][cornumb] = c5b;

                            AutoSaveFileCorrb(cornumb, linenum, 11);
                            cornumb++;
                        }
                    }

                    if (cnt_process[1] > (grp_num + 13))
                    {
                        AutoSaveFileCorrTailb();
                    }
                }

                System.Windows.Forms.MessageBox.Show("数据保存完成！", "完成通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (WorkMode == 1)
            {
                mfileh = "L" + (lineloop + 1).ToString("00");
                mfileh += DateTime.Now.Year.ToString("0000");
                mfileh += DateTime.Now.Month.ToString("00");
                mfileh += DateTime.Now.Day.ToString("00");
                mfileh += DateTime.Now.Hour.ToString("00");
                mfileh += DateTime.Now.Minute.ToString("00");
                mfileh += DateTime.Now.Second.ToString("00");

                fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                fs1.Close();
                fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                fs2a.Close();
                fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                fs3a.Close();
                fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                fs4a.Close();
                fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                fs2b.Close();
                fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                fs3b.Close();
                fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                fs4b.Close();
            }

        }
*/
/*        void mcorrs0(ushort[] s1, int length, out double az, out double sz)
        {
            double num = length;

            double ave = 0;
            double std = 0;

            for (int i = 0; i < length; i++)
            {
                ave += (double)s1[i];
                std += (double)s1[i] * (double)s1[i];
            }

            az = ave;
            sz = std;
        }
        double mcorrs(ushort[] s1, double az, double sz, ushort[] s2, int length)
        {
            double num = length;

            double miz = 1.0e-10;
            double ave = 0;
            double std = 0;
            double cor = 0;
            double temf = 0;
            double teml = 0;
            double fcor = 0;

            for (int i = 0; i < length; i++)
            {
                ave += (double)s2[i];
                std += (double)s2[i] * (double)s2[i];
                cor += (double)s1[i] * (double)s2[i];
            }
            temf = num * std - ave * ave;
            teml = num * sz - az * az;
            if (temf * teml < miz)
            {
                fcor = 0;
            }
            else
            {
                fcor = (num * cor - ave * az) / Math.Sqrt(temf * teml);
            }

            return (fcor);
        }

        void mcorrd0(double[] s1, int length, out double az, out double sz)
        {
            double num = length;

            double ave = 0;
            double std = 0;

            for (int i = 0; i < length; i++)
            {
                ave += s1[i];
                std += s1[i] * s1[i];
            }

            az = ave;
            sz = std;
        }
        double mcorrd(double[] s1, double az, double sz, double[] s2, int length)
        {
            double num = length;

            double miz = 1.0e-10;
            double ave = 0;
            double std = 0;
            double cor = 0;
            double temf = 0;
            double teml = 0;
            double fcor = 0;

            for (int i = 0; i < length; i++)
            {
                ave += s2[i];
                std += s2[i] * s2[i];
                cor += s1[i] * s2[i];
            }
            temf = num * std - ave * ave;
            teml = num * sz - az * az;
            if (temf * teml < miz)
            {
                if(((num * cor - ave * az) - Math.Sqrt(temf * teml)) < miz)
                {
                    fcor = 1;
                }
                else
                {
                    fcor = 0;
                }
            }
            else
            {
                fcor = (num * cor - ave * az) / Math.Sqrt(temf * teml);
            }

            return (fcor);
        }
*/
        //点击“清空数据”按钮触发事件
/*        private void btnCleanData_Click(object sender, EventArgs e)
        {
//            tbxRecvData.Text = "";
            tbxSendData.Text = "";
            tbxTxnum.Text = "";

        }*/

         //当指定的计时器间隔已过去而且计时器处于启用状态时发生
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WorkMode == 0)
            {
                if (data_input_status == 0)
                {
                    if ((stp_flg == 0) && (rec_pernum >= 0))
                    {
                        tem_flg = 0;
                        stp_flg = 1;
//                        bak_num = cnt_process[0] + cnt_process[1];
//                        bak_cnt = rec_pernum;
//                        mtimest = Environment.TickCount;
                    }
                    else
                    {
                        if ((stp_flg == 1) && (rec_pernum >= 0))
                        {
//                            mtimeen = Environment.TickCount;
//                            mtimeus = mtimeen - mtimest;
//                            if ((bak_cnt == rec_pernum) && (bak_num == (cnt_process[0] + cnt_process[1])) && (tem_flg == 0) && (mtimeus > 1000))
                            if (begin == 0)
                            {
                                if (cflg == true)
                                {
                                    if (disn < (cnt_process[0] > cnt_process[1] ? cnt_process[1] : cnt_process[0]))
                                    {
                                        disn++;
                                        hasData = true;
                                    }
                                    else
                                    {
                                        tem_flg = 1;
                                        System.Windows.Forms.MessageBox.Show("一次检测完成！", "完成通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    }
                                }
                                else
                                {
                                    tem_flg = 1;
                                    System.Windows.Forms.MessageBox.Show("一次检测完成！", "完成通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }

                                //                                AutoSaveResult();
                                //                                FindPeek((modelsta + cacsta) > (modelstb + cacstb) ? (modelstb + cacstb) : (modelsta + cacsta), (cnt_process[0] - mstepas) > (cnt_process[1] - mstepbs) ? (cnt_process[1] - mstepbs) : (cnt_process[0] - mstepas));
                                //                               AutoAnaylise();
                            }
/*                            else
                            {
                                if ((bak_cnt != rec_pernum) || (bak_num != (cnt_process[0] + cnt_process[1])))
                                {
                                    bak_cnt = rec_pernum;
                                    bak_num = cnt_process[0] + cnt_process[1];
                                    mtimest = Environment.TickCount;
                                }
                            }*/
                        }
                    }

                    /*            if ((rec_pernum == 0) && (stp_flg == 0))
                                {
                                    //                mtimest = 
                                    tem_flg = 0;
                                    stp_flg = 1;
                                    bak_num = rec_totnum;
                                    bak_cnt = cnt_process[0] + cnt_process[1];
                                    mtimest = Environment.TickCount;
                                }
                                else
                                {
                            //        mtimest = Environment.TickCount;

                                    if ((rec_pernum == 0) && (stp_flg == 1))
                                    {
                                        mtimeen = Environment.TickCount;
                                        mtimeus = mtimeen - mtimest;
                                        if ((bak_num == rec_totnum) && (tem_flg == 0) && (mtimeus > 1000))
                                        {
                                            tem_flg = 1;
                                            AutoSaveResult();
                                        }
                                        else
                                        {
                                            if (bak_num != rec_totnum)
                                            {
                                                bak_num = rec_totnum;
                                                mtimest = Environment.TickCount;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        mtimeen = Environment.TickCount;
                                        mtimeus = mtimeen - mtimest;
                                        if ((stp_flg == 1) && (mtimeus > 3000) && (tem_flg == 0))
                                        {
                                            tem_flg = 1;
                                            AutoSaveResult();
                                        }
                                    }
                                }*/

                    //接收到数据，更新实时曲线，否则不进行更新
                    if (hasData == false)
                    {
                        return;
                    }
                    //确保CurveList不为空
                    if (zgc.GraphPane.CurveList.Count <= 0)
                    {
                        return;
                    }
                    //取Graph第一个曲线，也就是第一步：在GraphPane.CurveList集合中查找CurveItem
                    LineItem lineItem = zgc.GraphPane.CurveList[0] as LineItem;

                    if (lineItem == null)
                    {
                        return;
                    }
                    //第二步：在CurveItem中访问PontPairList(或者它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
                    IPointListEdit pointList = lineItem.Points as IPointListEdit;
                    //IPointListEdit pointList_2 = lineItem.Points as IPointListEdit;
                    //If this is null, it means the reference at curve.Points does not support IPointListEdit, so we won't be able to modify it
                    if (pointList == null)
                    {
                        return;
                    }

                    if (disn <= totnum)
                    {
                        for (int i = 0; i < pernums; i++)
                        {
                            list.Add(time, (double)ydata_bufA[(disn - 1) * pernums + i]);
                            list_2.Add(time, (double)ydata_bufB[(disn - 1) * pernums + i]);
                            time += 0.2;
                        }
                        disn++;
                    }

                    if (list.Count >= 6100000)
                    {
                        for (int i = 0; i < pernums; i++)
                        {
                            list.RemoveAt(0);
                        }
                    }

                    if (list_2.Count >= 6100000)
                    {
                        for (int i = 0; i < pernums; i++)
                        {
                            list_2.RemoveAt(0);
                        }
                    }

                    //pointList.Add(++dataNum, data[1]);//这种实时性不高
                    //Console.WriteLine(dataNum);
                    // Keep the X scale at a rolling 30 second interval, with one major step between the max X value and the end of the axis
                    //保持窗口横坐标长度不变
                    Scale xScale = zgc.GraphPane.XAxis.Scale;
/*                    if (time > xScale.Max - xScale.MinorStep)
                    {
                        xScale.Max = time + xScale.MinorStep;
                        xScale.Min = xScale.Max - mwid;
                        //xScale.Min = xScale.Max - 60.0;
                    }*/

//                    time = timeA > timeB ? timeA : timeB;
                    if (time > xScale.Max)
                    {
                        xScale.Max = time;
                        xScale.Min = xScale.Max - mwid;
                        //xScale.Min = xScale.Max - 60.0;
                    }
                    //第三步：调用zgc.AxisChange()方法更新X轴和Y轴的范围
                    zgc.AxisChange();
                    zgc.Refresh();
                    //第四步：调用Form.Invalidate()方法更新图表
                    zgc.Invalidate();
                    //更新完实时曲线，要将标志位hasDate置为false
                    //                Refresh();
//                    AutoSaveFileByteSs();
//                    PerCacing((cnt_process[0] + cnt_process[1]));
//                    FindPeekSs();

//                    hasDataA = false;
//                    hasDataB = false;
                    hasData = false;
                }
                else
                {
                    if (di < 2 * endz)
                    {
                        rec_pernum = pernum;

                        //                        stopTime.Restart();
                        Input_DataControl(di);

                        //接收到数据，更新实时曲线，否则不进行更新
                        if (hasData == false)
                        {
                            di++;
                            return;
                        }
                        //确保CurveList不为空
                        if (zgc.GraphPane.CurveList.Count <= 0)
                        {
                            return;
                        }
                        //取Graph第一个曲线，也就是第一步：在GraphPane.CurveList集合中查找CurveItem
                        LineItem lineItem = zgc.GraphPane.CurveList[0] as LineItem;

                        if (lineItem == null)
                        {
                            return;
                        }
                        //第二步：在CurveItem中访问PontPairList(或者它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
                        IPointListEdit pointList = lineItem.Points as IPointListEdit;
                        //IPointListEdit pointList_2 = lineItem.Points as IPointListEdit;
                        //If this is null, it means the reference at curve.Points does not support IPointListEdit, so we won't be able to modify it
                        if (pointList == null)
                        {
                            return;
                        }

                        for (int i = 0; i < pernums; i++)
                        {
                            list.Add(time, (double)ydata_bufA[(totnum - 1) * pernums + i]);
                            list_2.Add(time, (double)ydata_bufB[(totnum - 1) * pernums + i]);
                            time += 0.2;
                        }

                        if (list.Count >= 6100000)
                        {
                            for (int i = 0; i < pernums; i++)
                            {
                                list.RemoveAt(0);
                            }
                        }

                        if (list_2.Count >= 6100000)
                        {
                            for (int i = 0; i < pernums; i++)
                            {
                                list_2.RemoveAt(0);
                            }
                        }

                        //pointList.Add(++dataNum, data[1]);//这种实时性不高
                        //Console.WriteLine(dataNum);
                        // Keep the X scale at a rolling 30 second interval, with one major step between the max X value and the end of the axis
                        //保持窗口横坐标长度不变
                        Scale xScale = zgc.GraphPane.XAxis.Scale;
/*                        if (time > xScale.Max - xScale.MinorStep)
                        {
                            xScale.Max = time + xScale.MinorStep;
                            xScale.Min = xScale.Max - mwid;
                            //xScale.Min = xScale.Max - 60.0;
                        }*/
//                        time = timeA > timeB ? timeA : timeB;
                        if (time > xScale.Max)
                        {
                            xScale.Max = time;
                            xScale.Min = xScale.Max - mwid;
                            //xScale.Min = xScale.Max - 60.0;
                        }
                        //第三步：调用zgc.AxisChange()方法更新X轴和Y轴的范围
                        zgc.AxisChange();
                        zgc.Refresh();
                        //第四步：调用Form.Invalidate()方法更新图表
                        zgc.Invalidate();
                        //更新完实时曲线，要将标志位hasDate置为false
                        //                Refresh();
                        di++;

//                        hasDataA = false;
//                        hasDataB = false;
                        hasData = false;

//                        while (stopTime.Elapsed.TotalMilliseconds < 200);
//                        stopTime.Stop();
                    }
                    else
                    {
                        if (once == 0)
                        {
                            once = 1;
//                            if (rbnChar.Checked)
//                            {
//                                HoldSaveResult();
//                                FindPeek((modelsta + cacsta) > (modelstb + cacstb) ? (modelstb + cacstb) : (modelsta + cacsta), (cnt_process[0] - mstepas) > (cnt_process[1] - mstepbs) ? (cnt_process[1] - mstepbs) : (cnt_process[0] - mstepas));
//                                AutoAnaylise();
//                            }

                            System.Windows.Forms.MessageBox.Show("数据文件加载完成！", "完成通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }

            }
            else
            {
                if ((isOpen) && (begin == 1))
                {
                    if (mj < mLinenum)
                    {
                        //                        stopTime.Start();
                        //                        mtimest = Environment.TickCount;
                        if (nein <= (worktime * 20))
                        {
                            //接收到数据，更新实时曲线，否则不进行更新
                            if (hasData == false)
                            {
                                return;
                            }
                            //确保CurveList不为空
                            if (zgc.GraphPane.CurveList.Count <= 0)
                            {
                                return;
                            }
                            //取Graph第一个曲线，也就是第一步：在GraphPane.CurveList集合中查找CurveItem
                            LineItem lineItem = zgc.GraphPane.CurveList[0] as LineItem;

                            if (lineItem == null)
                            {
                                return;
                            }
                            //第二步：在CurveItem中访问PontPairList(或者它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
                            IPointListEdit pointList = lineItem.Points as IPointListEdit;
                            //IPointListEdit pointList_2 = lineItem.Points as IPointListEdit;
                            //If this is null, it means the reference at curve.Points does not support IPointListEdit, so we won't be able to modify it
                            if (pointList == null)
                            {
                                return;
                            }

                            if (disn <= totnum)
                            {
                                for (int i = 0; i < pernums; i++)
                                {
                                    list.Add(time, (double)ydata_bufA[(disn - 1) * pernums + i]);
                                    list_2.Add(time, (double)ydata_bufB[(disn - 1) * pernums + i]);
                                    time += 0.2;
                                }
                                nein++;
                                disn++;
                            }

                            if (list.Count >= 6100000)
                            {
                                for (int i = 0; i < pernums; i++)
                                {
                                    list.RemoveAt(0);
                                }
                            }

                            if (list_2.Count >= 6100000)
                            {
                                for (int i = 0; i < pernums; i++)
                                {
                                    list_2.RemoveAt(0);
                                }
                            }

                            //保持窗口横坐标长度不变
                            Scale xScale = zgc.GraphPane.XAxis.Scale;

                            //                            time = timeA > timeB ? timeA : timeB;
                            if (time > xScale.Max)
                            {
                                xScale.Max = time;
                                xScale.Min = xScale.Max - mwid;
                                //xScale.Min = xScale.Max - 60.0;
                            }
                            //第三步：调用zgc.AxisChange()方法更新X轴和Y轴的范围
                            zgc.AxisChange();
                            zgc.Refresh();
                            //第四步：调用Form.Invalidate()方法更新图表
                            zgc.Invalidate();
                            //更新完实时曲线，要将标志位hasDate置为false

                            //                            hasDataA = false;
                            //                            hasDataB = false;
                            hasData = false;

                            //                            mtimeen = Environment.TickCount;
                            //                            mtimeus = mtimeen - mtimest;
                            //                        } while (stopTime.Elapsed.TotalMilliseconds < (worktime * 4300));
                        }
                        else
                        {
                            SensorCurPos = linethickness + mLinestep;
                            //                        if (SensorCurPos > 0)
                            {
                                SensorMovedis = SensorCurPos * millisteps / totaldis;
                                byte[] intBuff = BitConverter.GetBytes(SensorMovedis);
                                end_send[3] = intBuff[1];
                                end_send[4] = intBuff[0];
                                CheckAdd = end_send[0] + end_send[1] + end_send[2] + end_send[3] + end_send[4];
                                intBuff = BitConverter.GetBytes(CheckAdd);
                                end_send[5] = intBuff[0];
                                sp.Write(end_send, 0, end_send.Length);
                            }
                            /*                        else if (SensorCurPos < 0)
                                                    {
                                                        SensorMovedis = -SensorCurPos * millisteps / totaldis;
                                                        byte[] intBuff = BitConverter.GetBytes(SensorMovedis);
                                                        begin_send[3] = intBuff[1];
                                                        begin_send[4] = intBuff[0];
                                                        CheckAdd = begin_send[0] + begin_send[1] + begin_send[2] + begin_send[3] + begin_send[4];
                                                        intBuff = BitConverter.GetBytes(CheckAdd);
                                                        begin_send[5] = intBuff[0];
                                                        sp.Write(begin_send, 0, begin_send.Length);
                                                    }*/

                            nein = 0;
                            mj++;
                        }
                    }
                    else
                    {
                        if (disn < (cnt_process[0] > cnt_process[1] ? cnt_process[1] : cnt_process[0]))
                        {
                            disn++;
                            hasData = true;
                        }
                        else
                        {
                            tem_flg = 1;
                            begin = 0;
                            mj = 0;
                            btnBeginWork.Text = "启动接收"; //按钮显示文字转变
                            System.Windows.Forms.MessageBox.Show("检测完成！", "完成通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
                else if((begin == 0) && (mj > 0))
                {
                    if (disn < totnum)
                    {
                        disn++;
                        hasData = true;
                    }
                    else
                    {                    
                        if (once == 0)
                        {
                            once = 1;

                            tem_flg = 1;
                            mj = 0;
                            System.Windows.Forms.MessageBox.Show("检测中止！", "中止通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }

            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {

                lineItem_2.IsVisible = true;
                Refresh();
            }
            else
            {
                //否则文本框不可以用
                lineItem_2.IsVisible = false;
                Refresh();
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {

                lineItem.IsVisible = true;
                Refresh();
            }
            else
            {
                //否则文本框不可以用
                lineItem.IsVisible = false;
                Refresh();
            }
        }
/*        private void SaveButton_Click(object sender, EventArgs e)
        {
            di = 0;
            startz = 0;
            HoldSaveResult();
*/
            /*            if (cnt_process[rutenum - 10] > 0)
                        {
                            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
                            sfd.DefaultExt = "txt";
                            sfd.Filter = "文本文件(*.txt)|*.txt";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
            //                DoExport(this.lv, sfd.FileName);

                                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.GetEncoding("gb2312"));

                                for (int i = 0; i < cnt_process[rutenum - 10]; i++)
                                {

                                    int len = 0;
                                    string line = "";
                                    string temp = "";

                                    len = 10;
                                    temp = (i + 1).ToString() + " ";
                                    temp = temp.PadRight(len, ' ');
                                    sw.Write(temp);
                                    sw.Write(linenum.ToString() + " ");
                                    sw.Write(rutenum.ToString("X2") + " ");
                                    for (int j = 0; j < pernums; j++)
                                    {
                                        temp = tdata_buf[rutenum - 10][i * pernums + j].ToString() + " ";
                                        line += temp;
                                    }
                                    sw.WriteLine(line);
                                }

                                sw.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("没有采集数据需要保存！");
                        }*//*
        }*/
/*       void AutoSaveFileByte()
       {
            int Length = rec_totnum;

            FileStream fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Append, FileAccess.Write, FileShare.None);
            fs1.Write(Trec_buf, usingbs, Length - usingbs);
            fs1.Close();
       }
*/
       void AutoSaveFileByteSs()
       {
            //            int Length = rec_totnum;

            if (savefilenumber <= 1000)
            {
                FileStream fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Append, FileAccess.Write, FileShare.None);
                fs1.Write(buf1, 0, pernum);
                fs1.Close();
            }
            else
            {
                FileStream fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                fs1.Write(buf1, 0, pernum);
                fs1.Close();
            }
        }

        void AutoSaveFileShortHeada()
        {
            fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs2a.WriteLine(cnt_process[0].ToString());
            fs2a.Close();
        }

        void AutoSaveFileShortHeadb()
        {
            fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs2b.WriteLine(cnt_process[1].ToString());
            fs2b.Close();
        }

        void AutoSaveFileShorta(int num, int flan, int linen, int ruten, ushort[] Src)
        {
            fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            int len = 10;
            string line = "";
            string temp = "";

            temp = num.ToString();
            temp = temp.PadRight(len, ' ');
            temp += (zhna + 1).ToString().PadRight(len, ' ');
            temp += (((double)zhna * 0.05).ToString("0.00") + "s").PadRight(len, ' ');
            temp += (flan.ToString()).PadRight(4, ' ');
            temp += linen.ToString() + " ";
            temp += ruten.ToString("X2") + " ";

            line = "";
            foreach (ushort b in Src)
            {
                line += (b.ToString()).PadRight(len, ' ');
            }
            temp += line;
            fs2a.WriteLine(temp);

            fs2a.Close();
        }

        void AutoSaveFileShortb(int num, int flan, int linen, int ruten, ushort[] Src)
        {
            fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            int len = 10;
            string line = "";
            string temp = "";

            temp = num.ToString();
            temp = temp.PadRight(len, ' ');
            temp += (zhnb + 1).ToString().PadRight(len, ' ');
            temp += (((double)zhnb * 0.05).ToString("0.00") + "s").PadRight(len, ' ');
            temp += (flan.ToString()).PadRight(4, ' ');
            temp += linen.ToString() + " ";
            temp += ruten.ToString("X2") + " ";

            line = "";
            foreach (ushort b in Src)
            {
                line += (b.ToString()).PadRight(len, ' ');
            }
            temp += line;
            fs2b.WriteLine(temp);

            fs2b.Close();
        }

        void AutoSaveFileAveStdHeada()
        {
            fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs3a.WriteLine(cnt_process[0].ToString());
            fs3a.Close();
        }

        void AutoSaveFileAveStdHeadb()
        {
            fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs3b.WriteLine(cnt_process[0].ToString());
            fs3b.Close();
        }

        void AutoSaveFileAveStda(int num, int flan, int linen, int ruten, double[] Src)
        {
            fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            int len = 10;
            string line = "";
            string temp = "";

            temp = num.ToString();
            temp = temp.PadRight(len, ' ');
            temp += (zhna + 1).ToString().PadRight(len, ' ');
            temp += (((double)zhna * 0.05).ToString("0.00") + "s").PadRight(len, ' ');
            temp += (flan.ToString()).PadRight(4, ' ');
            temp += linen.ToString() + " ";
            temp += ruten.ToString("X2") + " ";

            line = (Src[1].ToString()).PadRight(len, ' ');
            line += (Src[2].ToString()).PadRight(len, ' ');
            line += (Src[3].ToString("0.0000")).PadRight(len + 4, ' ');
            line += Src[4].ToString("0.0000");

            temp += line;
            fs3a.WriteLine(temp);

            fs3a.Close();
        }

        void AutoSaveFileAveStdb(int num, int flan, int linen, int ruten, double[] Src)
        {
            fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            int len = 10;
            string line = "";
            string temp = "";

            temp = num.ToString();
            temp = temp.PadRight(len, ' ');
            temp += (zhnb + 1).ToString().PadRight(len, ' ');
            temp += (((double)zhnb * 0.05).ToString("0.00") + "s").PadRight(len, ' ');
            temp += (flan.ToString()).PadRight(4, ' ');
            temp += linen.ToString() + " ";
            temp += ruten.ToString("X2") + " ";

            line = (Src[1].ToString()).PadRight(len, ' ');
            line += (Src[2].ToString()).PadRight(len, ' ');
            line += (Src[3].ToString("0.0000")).PadRight(len + 4, ' ');
            line += Src[4].ToString("0.0000");

            temp += line;
            fs3b.WriteLine(temp);

            fs3b.Close();
        }
/*
        void AutoSaveFileCorrHeada()
        {
            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs4a.WriteLine(corstpa.ToString());
            fs4a.Close();
        }

        void AutoSaveFileCorrHeadb()
        {
            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs4b.WriteLine(corstpa.ToString());
            fs4b.Close();
        }

        void AutoSaveFileCorra(int num, int linen, int ruten)
        {
            int len = 10;
            string line = "";
            string temp = "";

            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            temp = (modelsta + cacsta + num * corstpa).ToString();
            temp = temp.PadRight(len, ' ');
            temp += (((double)(modelsta + cacsta + num * corstpa) * 0.05).ToString("0.00") + "s").PadRight(len, ' ');
            temp += linen.ToString() + " ";
            temp += ruten.ToString("X2") + " ";

            line = (checkas[num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkad[0][num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkad[1][num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkad[2][num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkad[3][num].ToString("0.0000")).PadLeft(len, ' ');
            temp += line;
            fs4a.WriteLine(temp);
            fs4a.Close();        
        }

        void AutoSaveFileCorrb(int num, int linen, int ruten)
        {
            int len = 10;
            string line = "";
            string temp = "";

            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            temp = (modelstb + cacstb + num * corstpb).ToString();
            temp = temp.PadRight(len, ' ');
            temp += (((double)(modelstb + cacstb + num * corstpb) * 0.05).ToString("0.00") + "s").PadRight(len, ' ');
            temp += linen.ToString() + " ";
            temp += ruten.ToString("X2") + " ";

            line = (checkbs[num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkbd[0][num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkbd[1][num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkbd[2][num].ToString("0.0000")).PadLeft(len, ' ');
            line += (checkbd[3][num].ToString("0.0000")).PadLeft(len, ' ');
            temp += line;
            fs4b.WriteLine(temp);
            fs4b.Close();
        }

        void AutoSaveFileCorrTaila()
        {
            int len = 14;
            string line = "";
            string temp = "";

            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            temp = "Channel";
            temp = temp.PadRight(len, ' ');

            line = "Type";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Raw";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Max";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Min";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Average";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Standard";
            line = line.PadRight(len, ' ');
            temp += line;
            fs4a.WriteLine(temp);

            temp = "0A ";
            temp = temp.PadRight(len, ' ');

            line = "Data";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "";
            foreach (double b in minaz)
            {
                line += (b.ToString("0.0000")).PadRight(len, ' ');
            }
            temp += line;
            fs4a.WriteLine(temp);

            temp = "0A ";
            temp = temp.PadRight(len, ' ');

            line = "Position";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "";
            foreach (double b in minap)
            {
                line += (b.ToString()).PadRight(len, ' ');
            }
            temp += line;
            fs4a.WriteLine(temp);
            fs4a.Close();
        }

        void AutoSaveFileCorrTailb()
        {
            int len = 14;
            string line = "";
            string temp = "";

            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            temp = "Channel";
            temp = temp.PadRight(len, ' ');

            line = "Type";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Raw";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Max";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Min";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Average";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "Standard";
            line = line.PadRight(len, ' ');
            temp += line;
            fs4b.WriteLine(temp);

            temp = "0B ";
            temp = temp.PadRight(len, ' ');

            line = "Data";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "";
            foreach (double b in minbz)
            {
                line += (b.ToString("0.0000")).PadRight(len, ' ');
            }
            temp += line;
            fs4b.WriteLine(temp);

            temp = "0B ";
            temp = temp.PadRight(len, ' ');

            line = "Position";
            line = line.PadRight(len, ' ');
            temp += line;

            line = "";
            foreach (double b in minbp)
            {
                line += (b.ToString()).PadRight(len, ' ');
            }
            temp += line;
            fs4b.WriteLine(temp);
            fs4b.Close();
        }*/

        void AutoSaveFileAnalizea()
        {
            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs4a.WriteLine(strBuilder0);
            fs4a.Close();
        }

        void AutoSaveFileAnalizeb()
        {
            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", true, System.Text.Encoding.GetEncoding("gb2312"));

            fs4b.WriteLine(strBuilder0);
            fs4b.Close();
        }

        /// <summary>
        /// 执行导出数据
        /// </summary>
        public void ExportToExecl()
        {
            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xls";
            sfd.Filter = "Excel文件(*.xls)|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
//                DoExport(this.lv, sfd.FileName);
            }
        }
        /// <summary>
        /// 具体导出的方法
        /// </summary>
        /// <param name="listView">ListView</param>
        /// <param name="strFileName">导出到的文件名</param>
        private void DoExport(ListView listView, string strFileName)
        {
            int rowNum = listView.Items.Count;
            int columnNum = listView.Items[0].SubItems.Count;
            int rowIndex = 1;
            int columnIndex = 0;
            if (rowNum == 0 || string.IsNullOrEmpty(strFileName))
            {
                return;
            }
            if (rowNum > 0)
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
                    return;
                }
                xlApp.DefaultFilePath = "";
                xlApp.DisplayAlerts = true;
                xlApp.SheetsInNewWorkbook = 1;
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                //将ListView的列名导入Excel表第一行
                foreach (ColumnHeader dc in listView.Columns)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = dc.Text;
                }
                //将ListView中的数据导入Excel中
                for (int i = 0; i < rowNum; i++)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for (int j = 0; j < columnNum; j++)
                    {
                        columnIndex++;
                        //注意这个在导出的时候加了“\t” 的目的就是避免导出的数据显示为科学计数法。可以放在每行的首尾。
                        xlApp.Cells[rowIndex, columnIndex] = Convert.ToString(listView.Items[i].SubItems[j].Text) + "\t";
                    }
                }
                //例外需要说明的是用strFileName,Excel.XlFileFormat.xlExcel9795保存方式时 当你的Excel版本不是95、97 而是2003、2007 时导出的时候会报一个错误：异常来自 HRESULT:0x800A03EC。 解决办法就是换成strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal。
                xlBook.SaveAs(strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlApp = null;
                xlBook = null;
                MessageBox.Show("Save OK");
            }
        }
        private void OpenButton_Click(object sender, EventArgs e)
        {
            //            int ds = 0;
            FileStream ls = new System.IO.FileStream("work.ini", FileMode.Open, FileAccess.Read, FileShare.None);
            ls.Read(buf1, 0, 27);
            mgrpm = (int)((ushort)(buf1[0] << 8) + (ushort)buf1[1]);
            grp_num = mgrpm;
            modelsta = (int)((ushort)(buf1[2] << 8) + (ushort)buf1[3]);
            modelstb = (int)((ushort)(buf1[4] << 8) + (ushort)buf1[5]);
            cacsta = (int)((ushort)(buf1[6] << 8) + (ushort)buf1[7]);
            cacstb = (int)((ushort)(buf1[8] << 8) + (ushort)buf1[9]);
            mstepa = (int)buf1[10];
            mstepb = (int)buf1[11];
            mstepas = (int)buf1[12];
            mstepbs = (int)buf1[13];
            mwid = (int)((ushort)(buf1[14] << 8) + (ushort)buf1[15]);
            millisteps = (int)((ushort)(buf1[16] << 8) + (ushort)buf1[17]);
            totaldis = (int)((ushort)(buf1[18] << 8) + (ushort)buf1[19]);
            linenumber = (int)buf1[20];
            linethickness = (int)buf1[21];
            linespace = (int)buf1[22];
            timeperzhen = (int)((ushort)(buf1[23] << 8) + (ushort)buf1[24]);
            thdcnt = (int)buf1[25];
            thdper = (int)buf1[26];
            ls.Read(buf1, 0, thdcnt * (thdper + 1));
            for (int i = 0; i < thdcnt; i++)
            {
                for (int j = 0; j < (thdper + 1); j++)
                {
                    thresholda[i][j] = (double)(buf1[i * (thdper + 1) + j]) / 100.0;
                }
            }
            ls.Read(buf1, 0, thdcnt * (thdper + 1));
            for (int i = 0; i < thdcnt; i++)
            {
                for (int j = 0; j < (thdper + 1); j++)
                {
                    thresholdb[i][j] = (double)(buf1[i * (thdper + 1) + j]) / 100.0;
                }
            }
            ls.Read(buf1, 0, 6);
            thresholdc[0] = (double)buf1[0];
            thresholdc[1] = (double)buf1[1];
            thresholdc[2] = (double)buf1[2];
            thresholdd[0] = (double)buf1[3];
            thresholdd[1] = (double)buf1[4];
            thresholdd[2] = (double)buf1[5];
            ls.Close();

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "数据文件(*.dat)|*.dat|数据文件(*.txt)|*.txt|所有文件(*.*)|*.*||";
            if (open.ShowDialog() == DialogResult.OK)
            {
                filePath = open.FileName;

                FileStream Ausr = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);

                int flen = (int)(Ausr.Length);
                Array.Clear(Trec_buf, 0, flen);
                Ausr.Read(Trec_buf, 0, flen);
                Ausr.Close();

                //                list.Clear();
                //                lineItem.Clear();
                //                list_2.Clear();
                //                lineItem_2.Clear();

                //                zgc.AxisChange();
                //                zgc.Refresh();
                //第四步：调用Form.Invalidate()方法更新图表
                //                zgc.Invalidate();
                //更新完实时曲线，要将标志位hasDate置为false

                /*                zgc.GraphPane.XAxis.Scale.MaxAuto = true;
                                zgc.GraphPane.XAxis.Scale.MinAuto = true;
                                zgc.GraphPane.XAxis.Scale.MajorStepAuto = true;
                                zgc.GraphPane.XAxis.Scale.MinorStepAuto = true;

                               zgc.GraphPane.YAxis.Scale.MaxAuto = true;
                                zgc.GraphPane.YAxis.Scale.MinAuto = true;
                                zgc.GraphPane.YAxis.Scale.MajorStepAuto = true;
                                zgc.GraphPane.YAxis.Scale.MinorStepAuto = true;
                                zgc.GraphPane.Y2Axis.Scale.MaxAuto = true;
                                zgc.GraphPane.Y2Axis.Scale.MinAuto = true;
                                zgc.GraphPane.Y2Axis.Scale.MajorStepAuto = true;
                                zgc.GraphPane.Y2Axis.Scale.MinorStepAuto = true;

                                zgc.PanModifierKeys = Keys.None;
                                zgc.ZoomStepFraction = 0.1;

                //                zgc.IsShowHScrollBar = true;
                                zgc.IsShowPointValues = true;
                                zgc.IsZoomOnMouseCenter = true;

                //                hasData = false;

                                tbxRecvData.Text = "";
                                tbxRecvTnum.Text = "";

                                rec_totnum = 0;
                                usingbs = -1;
                                cnt_process[0] = 0;
                                cnt_process[1] = 0;
                                linenum = 0;
                                rutenum = 0;
                */
                //                tem_flg = 0;
                //                stp_flg = 0;
                isHex = true;

//                btnBeginWork.Text = "停止接收"; //按钮显示文字转变

                rec_totnum = flen;
                usingbs = -1;
                tem_flg = 0;
                linenum = 0;
                pernums = (pernum - 7) / 2;
                totnum0 = 0;
                cflg = false;
                disn = 1;
                //                time = 0;
                //                timer1.Interval = 1; //设置timer控件的间隔为50ms，用于实时曲线
                //                timer1.Enabled = true; //timer可用
                //                timer1.Start(); //启动

                for (int i = 0; i < rec_totnum - 1; i++)
                {
                    if (((Trec_buf[i] == 0xEB) && (Trec_buf[i + 1] == 0x90)) &&
                        ((Trec_buf[507 + i] == 0xEB) && (Trec_buf[507 + i + 1] == 0x90)) &&
                        ((Trec_buf[507 * 2 + i] == 0xEB) && (Trec_buf[507 * 2 + i + 1] == 0x90)))
                    {
                        if (((Trec_buf[i + 2] + 1)== (Trec_buf[507 + i + 2])) &&
                            ((Trec_buf[507 + i + 2] + 1) == (Trec_buf[507 * 2 + i + 2])) &&
                            (Trec_buf[i + 4] == 0x0A))
                        {
                            usingbs = i;
                            linenum = Trec_buf[i + 3];
                            break;
                        }
                    }
                }

                if (usingbs >= 0)
                {
                    int mcnt = (flen - usingbs) / pernum;
                    if ((mcnt % 2) != 0)
                    {
                        mcnt -= 1;
                    }

                    string strText = string.Empty;
                    DialogResult mres = InputDialog.Show(out strText);

                    if (mres == DialogResult.OK)
                    {
                        if (strText.Length == 0)
                        {
                            startz = 0;
                            endz = mcnt / 2;
                        }
                        else if (strText.Contains("-") == false)
                        {
                            string[] tet = Regex.Split(strText.Trim(), "-");
                            startz = Convert.ToInt32(tet[0]);
                            endz = mcnt / 2;
                        }
                        else
                        {
                            string[] tet = Regex.Split(strText.Trim(), "-");
                            startz = Convert.ToInt32(tet[0]);
                            endz = Convert.ToInt32(tet[1]);
                        }

                        if (startz < 0)
                        {
                            startz = 0;
                        }
                        if (2 * startz > mcnt - 2)
                        {
                            startz = (mcnt - 2) / 2;
                        }

                        if (endz <= startz)
                        {
                            endz = startz + 1;
                        }
                        if (2 * endz > mcnt)
                        {
                            endz = mcnt / 2;
                        }

                        di = 2 * startz;
                        once = 0;

//                        if (rbnChar.Checked)
                        {
                            int tml = 0;
                            int tml1 = 0;
                            do
                            {
                                tml = filePath.IndexOf('\\', tml + 1);
                                if(tml != -1)
                                {
                                    tml1 = tml;
                                }
                            } while (tml != -1);

                            mfileh = filePath.Substring(tml1 + 1, filePath.Length - tml1 - 1 - 4);
                            tml = mfileh.IndexOf("_r1");
                            if (tml != -1)
                            {
                                mfileh = mfileh.Remove(tml, 3);
                            }
                            mfileh = mfileh.Insert(0,"CL_");
                            if (WorkPath != null)
                            {
                                mfileh = WorkPath + "\\" + mfileh;
                                StreamWriter tf = new System.IO.StreamWriter("Log.inf", false, System.Text.Encoding.GetEncoding("gb2312"));

                                tf.WriteLine(WorkPath + "\\");
                                tf.Close();
                            }
                            else
                            {
                                if(File.Exists("Log.inf"))
                                {
                                    StreamReader tr = new System.IO.StreamReader("Log.inf");
                                    WorkPath = tr.ReadLine();
                                    tr.Close();
                                    mfileh = WorkPath + mfileh;
                                }
                            }

                            /*                            mfileh = "C" + linenum.ToString("00") + "_" + cbxAny2.Text + "_";
                                                        mfileh += DateTime.Now.Year.ToString("0000");
                                                        mfileh += DateTime.Now.Month.ToString("00");
                                                        mfileh += DateTime.Now.Day.ToString("00");
                                                        mfileh += DateTime.Now.Hour.ToString("00");
                                                        mfileh += DateTime.Now.Minute.ToString("00");
                                                        mfileh += DateTime.Now.Second.ToString("00");*/

                            fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                            fs1.Close();
                            fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs2a.Close();
                            fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs3a.Close();
                            fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs4a.Close();
                            fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs2b.Close();
                            fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs3b.Close();
                            fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                            fs4b.Close();
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("不是想要的数据文件，请更换文件再试！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("数据文件加载取消！", "流程通知", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                WorkMode = 0;
                data_input_status = 1;
            }
        }

        public static void ImportMethod()
        {
        }

        private void btnCleanRecData_Click(object sender, EventArgs e)
        {
            tbxRecvData.Text = "";
            tbxRecvTnum.Text = "";
//            tbxSendData.Text = "";
//            tbxTxnum.Text = "";

            rec_pernum = -1;
            rec_totnum = 0;
            usingbs = -1;
            cnt_process[0] = 0;
            cnt_process[1] = 0;
            peknum = 0;
            mpeek[0] = 0;
            mpeek[1] = 0;
            totnum0 = 0;
            cflg = false;
            disn = 1;

            zhna = 0;
            zhnb = 0;
            strBuilder.Clear();

            linenum = 0;
            rutenum = 0;

            tem_flg = 0;
            stp_flg = 0;

            tbxTime.Text = "";
            tbxWarn.Text = "";
            tbxWater.Text = "";
            tbxState.Text = "";

            tbxResult1.Text = "0";
            picbxRes1.Image = imageNormal; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
            tbxResultPos1.Text = "0";
            tbxResult2.Text = "0";
////            picbxRes2.Image = imageNormal; //设置初始状态显示红色，当前工作目录指.exe文件所在文件夹
            tbxResultPos2.Text = "0";

            tbxMSGS.Text = "0";
            tbxXSGS.Text = "0";
            tbxDSGS.Text = "0";
            tbxQTGS.Text = "0";
        }

        private static DataSet doImportExcle(string strFileName)
        {

            if (strFileName == "") return null;
            string extName = System.IO.Path.GetExtension(strFileName);
            //string strConn = xlspro + "Data Source=" + strFileName + ";" + xlsex;
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +"Data Source="+ strFileName +";"+"Extended Properties=Excel 8.0;"; 
            OleDbDataAdapter ExcelDA = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
            //public DataSet ExcelToDS(string Path) 
            //{ 
            //    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +"Data Source="+ Path +";"+"Extended Properties=Excel 8.0;"; 
            //    OleDbConnection conn = new OleDbConnection(strConn); 
            //    conn.Open();   
            //    string strExcel = "";    
            //    OleDbDataAdapter myCommand = null; 
            //    DataSet ds = null; 
            //    strExcel="select * from [sheet1$]"; 
            //    myCommand = new OleDbDataAdapter(strExcel, strConn); 
            //    ds = new DataSet(); 
            //    myCommand.Fill(ds,"table1");    
            //    return ds; 
            //}

            DataSet ExcelDs = new DataSet();
            try
            {
                ExcelDA.Fill(ExcelDs);

            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.ToString());
            }
            return ExcelDs;
        }

        private void ClearTButton_Click(object sender, EventArgs e)
        {
            list.Clear();
            lineItem.Clear();
            list_2.Clear();
            lineItem_2.Clear();

            time = 0;
//            timeB = 0;
//            btimea = 0;
//            btimeb = 0;

/*            zgc.GraphPane.YAxis.Scale.Min = 0; //X轴最小值0
            zgc.GraphPane.YAxis.Scale.Max = 150; //X轴最大值60
            zgc.GraphPane.YAxis.Scale.MinorStep = 3; //X轴小步长1，也就是小间隔
            zgc.GraphPane.YAxis.Scale.MajorStep = 15; //X轴大步长5，也就是显示文字的大间隔
            zgc.GraphPane.Y2Axis.Scale.Min = 0; //X轴最小值0
            zgc.GraphPane.Y2Axis.Scale.Max = 150; //X轴最大值60
            zgc.GraphPane.Y2Axis.Scale.MinorStep = 3; //X轴小步长1，也就是小间隔
            zgc.GraphPane.Y2Axis.Scale.MajorStep = 15; //X轴大步长5，也就是显示文字的大间隔

            zgc.GraphPane.XAxis.Scale.Min = 0; //X轴最小值0
            zgc.GraphPane.XAxis.Scale.Max = 4000; //X轴最大值60
            zgc.GraphPane.XAxis.Scale.MinorStep = 40; //X轴小步长1，也就是小间隔
            zgc.GraphPane.XAxis.Scale.MajorStep = 200; //X轴大步长5，也就是显示文字的大间隔
*/
            zgc.GraphPane.XAxis.Scale.MaxAuto = true;
            zgc.GraphPane.XAxis.Scale.MinAuto = true;
            zgc.GraphPane.XAxis.Scale.MajorStepAuto = true;
            zgc.GraphPane.XAxis.Scale.MinorStepAuto = true;

            zgc.GraphPane.YAxis.Scale.MaxAuto = true;
            zgc.GraphPane.YAxis.Scale.MinAuto = true;
            zgc.GraphPane.YAxis.Scale.MajorStepAuto = true;
            zgc.GraphPane.YAxis.Scale.MinorStepAuto = true;
            zgc.GraphPane.Y2Axis.Scale.MaxAuto = true;
            zgc.GraphPane.Y2Axis.Scale.MinAuto = true;
            zgc.GraphPane.Y2Axis.Scale.MajorStepAuto = true;
            zgc.GraphPane.Y2Axis.Scale.MinorStepAuto = true;

            zgc.PanModifierKeys = Keys.None;
            zgc.ZoomStepFraction = 0.1;

//            zgc.IsShowHScrollBar = true;
            zgc.IsShowPointValues = true;
            zgc.IsZoomOnMouseCenter = true;

            zgc.AxisChange();
            zgc.Refresh();
            zgc.Invalidate();

            hasData = false;
        }

        private void ReapTButton_Click(object sender, EventArgs e)
        {
            zgc.ZoomOutAll(zgc.GraphPane);
        }

//        nfyChangeStatus icon

/*        private void FrmChangeStatus_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;
//                nfyChangeStatus.Visible = true;
            }
        }
*/


        private void btnBeginWork_Click(object sender, EventArgs e)
        {
            if(begin == 0)
            {
                if ((cbxWorkMode.Text.Trim() == "安装调试") || (cbxWorkMode.Text.Trim() == "缺陷定位"))
                {
                    WorkMode = 0;
                }
                if (cbxWorkMode.Text.Trim() == "检测运行")
                {
                    mLinenum = Convert.ToInt32(cbxLineNumber.Text);
                    mLinestep = Convert.ToInt32(cbxLineStep.Text);
                    int mtem = Convert.ToInt32(cbxLineWidth.Text);
                    if (mtem != linethickness)
                    {
                        linethickness = mtem;
                    }
                    worktime = Convert.ToInt32(cbxWorkTime.Text);
                    once = 0;
                    WorkMode = 1;
                }
/*                if (cbxWorkMode.Text.Trim() == "缺陷定位")
                {
                    mLinenum = Convert.ToInt32(cbxLineNumber.Text);
                    mLinestep = Convert.ToInt32(cbxLineStep.Text);
                    int mtem = Convert.ToInt32(cbxLineWidth.Text);
                    if (mtem != linethickness)
                    {
                        linethickness = mtem;
                    }
                    worktime = Convert.ToInt32(cbxWorkTime.Text);
                    WorkMode = 2;
                }*/

                zgc.ZoomOutAll(zgc.GraphPane);

                list.Clear();
                lineItem.Clear();
                list_2.Clear();
                lineItem_2.Clear();

                time = 0;
//                timeB = 0;

/*                zgc.GraphPane.YAxis.Scale.Min = 0; //X轴最小值0
                zgc.GraphPane.YAxis.Scale.Max = 150; //X轴最大值60
                zgc.GraphPane.YAxis.Scale.MinorStep = 3; //X轴小步长1，也就是小间隔
                zgc.GraphPane.YAxis.Scale.MajorStep = 15; //X轴大步长5，也就是显示文字的大间隔
                zgc.GraphPane.Y2Axis.Scale.Min = 0; //X轴最小值0
                zgc.GraphPane.Y2Axis.Scale.Max = 150; //X轴最大值60
                zgc.GraphPane.Y2Axis.Scale.MinorStep = 3; //X轴小步长1，也就是小间隔
                zgc.GraphPane.Y2Axis.Scale.MajorStep = 15; //X轴大步长5，也就是显示文字的大间隔

                zgc.GraphPane.XAxis.Scale.Min = 0; //X轴最小值0
                zgc.GraphPane.XAxis.Scale.Max = 4000; //X轴最大值60
                zgc.GraphPane.XAxis.Scale.MinorStep = 40; //X轴小步长1，也就是小间隔
                zgc.GraphPane.XAxis.Scale.MajorStep = 200; //X轴大步长5，也就是显示文字的大间隔
*/
                zgc.GraphPane.XAxis.Scale.MaxAuto = true;
                zgc.GraphPane.XAxis.Scale.MinAuto = true;
                zgc.GraphPane.XAxis.Scale.MajorStepAuto = true;
                zgc.GraphPane.XAxis.Scale.MinorStepAuto = true;

                zgc.GraphPane.YAxis.Scale.MaxAuto = true;
                zgc.GraphPane.YAxis.Scale.MinAuto = true;
                zgc.GraphPane.YAxis.Scale.MajorStepAuto = true;
                zgc.GraphPane.YAxis.Scale.MinorStepAuto = true;
                zgc.GraphPane.Y2Axis.Scale.MaxAuto = true;
                zgc.GraphPane.Y2Axis.Scale.MinAuto = true;
                zgc.GraphPane.Y2Axis.Scale.MajorStepAuto = true;
                zgc.GraphPane.Y2Axis.Scale.MinorStepAuto = true;

                zgc.PanModifierKeys = Keys.None;
                zgc.ZoomStepFraction = 0.1;

//                zgc.IsShowHScrollBar = true;
                zgc.IsShowPointValues = true;
                zgc.IsZoomOnMouseCenter = true;

                zgc.AxisChange();
                zgc.Refresh();
                zgc.Invalidate();

                hasData = false;

                tbxRecvData.Text = "";
                tbxRecvTnum.Text = "";

                rec_pernum = -1;
                rec_totnum = 0;
                usingbs = -1;
                cnt_process[0] = 0;
                cnt_process[1] = 0;
                zhna = 0;
                zhnb = 0;
                peknum = 0;
                mpeek[0] = 0;
                mpeek[1] = 0;
                totnum0 = 0;
                cflg = false;
                disn = 1;
                savefilenumber = 0;

                strBuilder.Clear();

                linenum = 0;
                rutenum = 0;
                pernums = (pernum - 7) / 2;

                tem_flg = 0;
                stp_flg = 0;

                if (WorkMode == 0)
                {
                    mfileh = "B" + (linenum + 1).ToString("00") + "_" + tbxTime.Text + "_" + savefilenumber.ToString("0000") + "_";
                    mfileh += DateTime.Now.Year.ToString("0000");
                    mfileh += DateTime.Now.Month.ToString("00");
                    mfileh += DateTime.Now.Day.ToString("00");
                    mfileh += DateTime.Now.Hour.ToString("00");
                    mfileh += DateTime.Now.Minute.ToString("00");
                    mfileh += DateTime.Now.Second.ToString("00");
                    if (WorkPath != null)
                    {
                        mfileh = WorkPath + "\\" + mfileh;
                        StreamWriter tf = new System.IO.StreamWriter("Log.inf", false, System.Text.Encoding.GetEncoding("gb2312"));

                        tf.WriteLine(WorkPath + "\\");
                        tf.Close();
                    }
                    else
                    {
                        if (File.Exists("Log.inf"))
                        {
                            StreamReader tr = new System.IO.StreamReader("Log.inf");
                            WorkPath = tr.ReadLine();
                            tr.Close();
                            mfileh = WorkPath + mfileh;
                        }
                    }

                    fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                    fs1.Close();
//                    fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs2a.Close();
//                    fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs3a.Close();
                    fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                    fs4a.Close();
//                    fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs2b.Close();
//                    fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs3b.Close();
                    fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                    fs4b.Close();
                }
                else
                {
                    mfileh = "L01-" + mLinenum.ToString("00") + "_" + tbxTime.Text + "_";
                    mfileh += DateTime.Now.Year.ToString("0000");
                    mfileh += DateTime.Now.Month.ToString("00");
                    mfileh += DateTime.Now.Day.ToString("00");
                    mfileh += DateTime.Now.Hour.ToString("00");
                    mfileh += DateTime.Now.Minute.ToString("00");
                    mfileh += DateTime.Now.Second.ToString("00");
                    if (WorkPath != null)
                    {
                        mfileh = WorkPath + "\\" + mfileh;
                        StreamWriter tf = new System.IO.StreamWriter("Log.inf", false, System.Text.Encoding.GetEncoding("gb2312"));

                        tf.WriteLine(WorkPath + "\\");
                        tf.Close();
                    }
                    else
                    {
                        if (File.Exists("Log.inf"))
                        {
                            StreamReader tr = new System.IO.StreamReader("Log.inf");
                            WorkPath = tr.ReadLine();
                            tr.Close();
                            mfileh = WorkPath + mfileh;
                        }
                    }

                    fs1 = new FileStream(mfileh + "_r1.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                    fs1.Close();
//                    fs2a = new System.IO.StreamWriter(mfileh + "_r2a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs2a.Close();
//                    fs3a = new System.IO.StreamWriter(mfileh + "_r3a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs3a.Close();
                    fs4a = new System.IO.StreamWriter(mfileh + "_r4a.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                    fs4a.Close();
//                    fs2b = new System.IO.StreamWriter(mfileh + "_r2b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs2b.Close();
//                    fs3b = new System.IO.StreamWriter(mfileh + "_r3b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
//                    fs3b.Close();
                    fs4b = new System.IO.StreamWriter(mfileh + "_r4b.txt", false, System.Text.Encoding.GetEncoding("gb2312"));
                    fs4b.Close();
                }

                btnBeginWork.Text = "停止接收"; //按钮显示文字转变
                sp.DiscardInBuffer();
                begin = 1;
            }
            else
            {
                begin = 0;
                btnBeginWork.Text = "启动接收"; //按钮显示文字转变

                if(disn < (cnt_process[0] > cnt_process[1] ? cnt_process[1] : cnt_process[0]))
                {
                    cflg = true;
                }
            }
        }
        //往前浏览
        private void ForwardTButton_Click(object sender, EventArgs e)
        {
            double curX_Max = zgc.GraphPane.XAxis.Scale.Max;
            double curX_Min = zgc.GraphPane.XAxis.Scale.Min;

            curX_Max -= Convert.ToDouble(cbxTLLStep.Text);
            curX_Min -= Convert.ToDouble(cbxTLLStep.Text);

            zgc.GraphPane.XAxis.Scale.Max = curX_Max;
            zgc.GraphPane.XAxis.Scale.Min = curX_Min;

            zgc.PanModifierKeys = Keys.None;
            zgc.ZoomStepFraction = 0.1;

            //                zgc.IsShowHScrollBar = true;
            zgc.IsShowPointValues = true;
            zgc.IsZoomOnMouseCenter = true;

            zgc.AxisChange();
            zgc.Refresh();
            zgc.Invalidate();
        }
        //指定浏览
        private void SomeZhnTButton_Click(object sender, EventArgs e)
        {
            double curX_Max = zgc.GraphPane.XAxis.Scale.Max;
            double curX_Min = zgc.GraphPane.XAxis.Scale.Min;
            double curX_Cen = (curX_Max + curX_Min) / 2;

            if (Convert.ToDouble(cbxTLLStep.Text) > curX_Cen)
            {
                curX_Max += Convert.ToDouble(cbxTLLStep.Text) - curX_Cen;
                curX_Min += Convert.ToDouble(cbxTLLStep.Text) - curX_Cen;
            }
            else if (Convert.ToDouble(cbxTLLStep.Text) < curX_Cen)
            {
                curX_Max -= curX_Cen - Convert.ToDouble(cbxTLLStep.Text);
                curX_Min -= curX_Cen - Convert.ToDouble(cbxTLLStep.Text);
            }

            zgc.GraphPane.XAxis.Scale.Max = curX_Max;
            zgc.GraphPane.XAxis.Scale.Min = curX_Min;

            zgc.PanModifierKeys = Keys.None;
            zgc.ZoomStepFraction = 0.1;

            //                zgc.IsShowHScrollBar = true;
            zgc.IsShowPointValues = true;
            zgc.IsZoomOnMouseCenter = true;

            zgc.AxisChange();
            zgc.Refresh();
            zgc.Invalidate();
        }
        //往后浏览
        private void BehindwardTButton_Click(object sender, EventArgs e)
        {
            double curX_Max = zgc.GraphPane.XAxis.Scale.Max;
            double curX_Min = zgc.GraphPane.XAxis.Scale.Min;

            curX_Max += Convert.ToDouble(cbxTLLStep.Text);
            curX_Min += Convert.ToDouble(cbxTLLStep.Text);

            zgc.GraphPane.XAxis.Scale.Max = curX_Max;
            zgc.GraphPane.XAxis.Scale.Min = curX_Min;

            zgc.PanModifierKeys = Keys.None;
            zgc.ZoomStepFraction = 0.1;

            //                zgc.IsShowHScrollBar = true;
            zgc.IsShowPointValues = true;
            zgc.IsZoomOnMouseCenter = true;

            zgc.AxisChange();
            zgc.Refresh();
            zgc.Invalidate();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
/*            if (this.WindowState == FormWindowState.Minimized)
            {
                //                this.Hide();
                this.ShowInTaskbar = true;
            }
            else
            {
//                this.WindowState = FormWindowState.Maximized;
//                this.Activate();
                this.ShowInTaskbar = true;
            }*/
            asc.controlAutoSize(this);
        }
        /*        private void nfyChangeStatus_Click(object sender, MouseEventArgs e)
                {
                    if (WindowState == FormWindowState.Minimized)
                    {
        //                this.Visible = true;
                        WindowState = FormWindowState.Maximized;
                        this.Activate();
                        this.ShowInTaskbar = true;
        //                nfyChangeStatus.Visible = true;
                    }
                }*/

        private void Form1_Resize(object sender, EventArgs e)
        {
//            asc.controlAutoSize(this);

            //            SetSize();
        }

        //Set the size and location of the ZedGraphControl
        private void SetSize()
        {
            System.Drawing.Rectangle formRect = this.ClientRectangle; //Rectangle：存储一组整数，共四个，表示一个矩形的位置和大小
            formRect.Inflate(-10, -10);
            if (this.WindowState != FormWindowState.Minimized)
            {
                if ((zgc.Size.Width > formRect.Size.Width) && ((zgc.Size.Height + 425) > formRect.Size.Height))
                {
                    zgc.Location = formRect.Location;
                    zgc.Width = formRect.Size.Width;
                    zgc.Height = formRect.Size.Height - 425;
                }
            }
        }
        private void btnSetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog myFBD = new FolderBrowserDialog();
            if(myFBD.ShowDialog() == DialogResult.OK)
            {
                WorkPath = myFBD.SelectedPath;
            }
        }

        private void btnMovSheBei_Click(object sender, EventArgs e)
        {
            if (isOpen == true)
            {
                SensorCurPos = Convert.ToInt32(cbxWorkPos.Text.Trim());

                if (SensorCurPos > 0)
                {
                    SensorMovedis = SensorCurPos * millisteps / totaldis;
                    byte[] intBuff = BitConverter.GetBytes(SensorMovedis);
                    end_send[3] = intBuff[1];
                    end_send[4] = intBuff[0];
                    CheckAdd = end_send[0] + end_send[1] + end_send[2] + end_send[3] + end_send[4];
                    intBuff = BitConverter.GetBytes(CheckAdd);
                    end_send[5] = intBuff[0];
                    sp.Write(end_send, 0, end_send.Length);
                }
                else if (SensorCurPos < 0)
                {
                    SensorMovedis = -SensorCurPos * millisteps / totaldis;
                    byte[] intBuff = BitConverter.GetBytes(SensorMovedis);
                    begin_send[3] = intBuff[1];
                    begin_send[4] = intBuff[0];
                    CheckAdd = begin_send[0] + begin_send[1] + begin_send[2] + begin_send[3] + begin_send[4];
                    intBuff = BitConverter.GetBytes(CheckAdd);
                    begin_send[5] = intBuff[0];
                    sp.Write(begin_send, 0, begin_send.Length);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rbnHex_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void zgc_Load(object sender, EventArgs e)
        {

        }
    }

    class AutoSizeFormClass
    {
        //(1).声明结构,只记录窗体和其控件的初始位置和大小。
        public struct controlRect
        {
            public string Name;

            public int Left;
            public int Top;
            public int Width;
            public int Height;

            public float Size;

            public int TabI;
        }
        //(2).声明 1个对象
        //注意这里不能使用控件列表记录 List nCtrl;，因为控件的关联性，记录的始终是当前的大小。
        //      public List oldCtrl= new List();//这里将西文的大于小于号都过滤掉了，只能改为中文的，使用中要改回西文
        public List<controlRect> oldCtrl = new List<controlRect>();
        int ctrlNo = 0;//1;
                       //(3). 创建两个函数
                       //(3.1)记录窗体和其控件的初始位置和大小,
        public void controllInitializeSize(Control mForm)
        {
            AutoSizeFormClass.controlRect cR;
            cR.Name = mForm.Name;
            cR.Left = mForm.Left;
            cR.Top = mForm.Top;
            cR.Width = mForm.Width;
            cR.Height = mForm.Height;
            cR.Size = mForm.Font.Size;
            cR.TabI = mForm.TabIndex;

            oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
            AddControl(mForm);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                              //this.WindowState = (System.Windows.Forms.FormWindowState)(2);//记录完控件的初始位置和大小后，再最大化
                              //0 - Normalize , 1 - Minimize,2- Maximize
        }
        private void AddControl(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {  //**放在这里，是先记录控件的子控件，后记录控件本身
               //if (c.Controls.Count > 0)
               //    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                controlRect objCtrl;
                objCtrl.Name = c.Name;
                objCtrl.Left = c.Left;
                objCtrl.Top = c.Top;
                objCtrl.Width = c.Width;
                objCtrl.Height = c.Height;
                objCtrl.Size = c.Font.Size;
                objCtrl.TabI = c.TabIndex;
                oldCtrl.Add(objCtrl);
                //**放在这里，是先记录控件本身，后记录控件的子控件
                if (c.Controls.Count > 0)
                    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }
        //(3.2)控件自适应大小,
        public void controlAutoSize(Control mForm)
        {
            if (ctrlNo == 0)
            { //*如果在窗体的Form1_Load中，记录控件原始的大小和位置，正常没有问题，但要加入皮肤就会出现问题，因为有些控件如dataGridView的的子控件还没有完成，个数少
              //*要在窗体的Form1_SizeChanged中，第一次改变大小时，记录控件原始的大小和位置,这里所有控件的子控件都已经形成
                controlRect cR;
                //  cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                cR.Name = mForm.Name;
                cR.Left = 0;
                cR.Top = 0;
                cR.Width = mForm.PreferredSize.Width;
                cR.Height = mForm.PreferredSize.Height;
                cR.Size = mForm.Font.Size;
                cR.TabI = mForm.TabIndex;

                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                AddControl(mForm);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
            }

            float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
            float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//
            ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始
            AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }
        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
            float ctrlFontSize, hSize, wSize;

            //int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
            foreach (Control c in ctl.Controls)
            { //**放在这里，是先缩放控件的子控件，后缩放控件本身
              //if (c.Controls.Count > 0)
              //   AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                ctrLeft0 = oldCtrl[ctrlNo].Left;
                ctrTop0 = oldCtrl[ctrlNo].Top;
                ctrWidth0 = oldCtrl[ctrlNo].Width;
                ctrHeight0 = oldCtrl[ctrlNo].Height;

                ctrlFontSize = oldCtrl[ctrlNo].Size;

                //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例
                //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;
                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                c.Top = (int)((ctrTop0) * hScale);//
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                c.Height = (int)(ctrHeight0 * hScale);//

                wSize = ctrlFontSize * wScale;
                hSize = ctrlFontSize * hScale;
                c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);

                ctrlNo++;//累加序号
                         //**放在这里，是先缩放控件本身，后缩放控件的子控件
                if (c.Controls.Count > 0)
                    AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }
    }
}