using ControlPlatformLib;
using FullyAutomaticLaserJetCoder.MainTask;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.SerialPorts;

namespace FullyAutomaticLaserJetCoder
{
    public partial class 手动操作窗体 : Form
    {
        public string stfPath = System.Environment.CurrentDirectory + "\\FlowDocument";
     //   public mes mes;
    //    public ComeOut_process ComeOut;
       // public Weld_Process Weld;
        public MarkTask m_MarkTask;
      //  public Laser_PowerOn_And_Off Laser_PowerOn;
        public MainControl MainControls= MainControl.Instance();
     //   public Feed_process FeedTask;
        public Method Methods;
        private object lockObj = new object();
        public TestDateCom TestDateC;
        public int OpenForm = 0;
     //   RunClass RunClass = RunClass.Instance();
        ProductionData ProductionD = ProductionData.Instance();
        public string ReadstfPath = System.Environment.CurrentDirectory + "\\FlowDocument";
        public List<KeyValuePair<string, KeyValuePair<string, string>>> RunItem_List = new List<KeyValuePair<string, KeyValuePair<string, string>>>();
        public List<KeyValuePair<string, string>> RunItem_List1111 = new List<KeyValuePair<string, string>>();
        List<string> ListStr = new List<string>();
        public 手动操作窗体()
        {
            InitializeComponent();
        }

        private void 板卡IO_Click(object sender, EventArgs e)
        {
            click = 1;
            dataGridViewInput.Rows.Clear();
            for (int i=0;i < IOManage.IODoc.m_InputDataList.Count;i++)
            {
                if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googol")
                {
                    dataGridViewInput.Rows.Add(IOManage.IODoc.m_InputDataList[i]);
                    dataGridViewInput.Rows[i].Cells[0].Value = IOManage.IODoc.m_InputDataList[i].strIOName;
                    dataGridViewInput.Rows[i].Cells[1].Value = IOManage.IODoc.m_InputDataList[i].iInputNo;
                }
            }
            timer1.Start();

            dataGridViewOutput.Rows.Clear();
            for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googol")
                {
                    dataGridViewOutput.Rows.Add(IOManage.IODoc.m_OutputDataList[i]);
                    dataGridViewOutput.Rows[i].Cells[0].Value = IOManage.IODoc.m_OutputDataList[i].strIOName;
                    dataGridViewOutput.Rows[i].Cells[1].Value = IOManage.IODoc.m_OutputDataList[i].iOutputNo;
                }
            }
        }
        int click = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int j = 0;
            if (click == 1)
            {
                for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googol")
                    {
                        bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[i].strIOName].GetOn();
                        dataGridViewOutput.Rows[i].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                    }
                }
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googol")
                    {
                        bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                        dataGridViewInput.Rows[i].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                    }
                }
            }
            if (click == 2)
            {
                j = 0;
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo0")
                    {
                        
                        bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                        dataGridViewInput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                  
                }
                j = 0;
                for (int k = 0; k < IOManage.IODoc.m_OutputDataList.Count; k++)
                {

                    if (IOManage.IODoc.m_OutputDataList[k].OutputCardName == "Googlo0")
                    {
                        bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[k].strIOName].GetOn();
                        dataGridViewOutput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                }
            }
            if (click == 3)
            {
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo1")
                    {
                        bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                      
                       dataGridViewInput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                      
                        j++;
                    }
                    
                }
               
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo1")
                    {
                        bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[i].strIOName].GetOn();
                        dataGridViewOutput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                }
            }
            if (click == 4)
            {
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo2")
                    {
                        bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                        dataGridViewInput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                   
                }
              
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo2")
                    {
                        bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[i].strIOName].GetOn();
                        dataGridViewOutput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                }
            }
            if (click == 5)
            {
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo3")
                    {
                        bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                        dataGridViewInput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                    
                }
                
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo3")
                    {
                        bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[i].strIOName].GetOn();
                        dataGridViewOutput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                }
            }
            if (click == 6)
            {
                j = 0;
                for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo4")
                    {
                        bool bOn = IOManage.InputDrivers.drivers[IOManage.IODoc.m_InputDataList[i].strIOName].GetOn();
                        dataGridViewInput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                    
                }
                j = 0;
               
                for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
                {
                    if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo4")
                    {
                        bool bOn = IOManage.OutputDrivers.drivers[IOManage.IODoc.m_OutputDataList[i].strIOName].GetOn();
                        dataGridViewOutput.Rows[j].Cells[2].Style.BackColor = bOn ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                        j++;
                    }
                }
          

            }

            if (DateSave.Instance().Production.ModelNo != "" && 机种选择.Text == "")
            {
                机种选择.Text = DateSave.Instance().Production.ModelNo;

                if (机种选择.Text != "")
                {
                    var files = Directory.GetFiles(stfPath + "\\" + 机种选择.Text, "*.csv");
                    if (运行流程选择.Items.Count > 0)
                    {
                        运行流程选择.Items.Clear();
                    }
                    foreach (var file in files)
                    {
                        string fileNameExt = file.Substring(file.LastIndexOf("\\") + 1); //获取文件名，不带路径
                        string filePath = file.Substring(0, file.LastIndexOf("\\"));//获取文件路径，不带文件名
                        运行流程选择.Items.Add(fileNameExt);
                    }
                    运行流程选择.SelectedIndex = 0;
                }
            }
        }
        private void 手动操作窗体_Load(object sender, EventArgs e)
        {
        
            OpenForm = 0;
            List<String> list = new List<string>();
            DirectoryInfo root = new DirectoryInfo(stfPath);
            DirectoryInfo[] di = root.GetDirectories();
            for (int i = 0; i < di.Length; i++)
            {
                机种选择.Items.Add(di[i]);
            }
            timer1.Start();
        }

        private void 模块1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            click = 2;
            int j = 0;
            dataGridViewInput.Rows.Clear();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo0")
                {
                    dataGridViewInput.Rows.Add(IOManage.IODoc.m_InputDataList[i]);
                    dataGridViewInput.Rows[j].Cells[0].Value = IOManage.IODoc.m_InputDataList[i].strIOName;
                    dataGridViewInput.Rows[j].Cells[1].Value = IOManage.IODoc.m_InputDataList[i].iInputNo;
                    j++;
                }
            }
            dataGridViewOutput.Rows.Clear();
            j = 0;
            for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo0")
                {
                    dataGridViewOutput.Rows.Add(IOManage.IODoc.m_OutputDataList[i]);
                    dataGridViewOutput.Rows[j].Cells[0].Value = IOManage.IODoc.m_OutputDataList[i].strIOName;
                    dataGridViewOutput.Rows[j].Cells[1].Value = IOManage.IODoc.m_OutputDataList[i].iOutputNo;
                    j++;
                }
            }
        }

        private void 模块2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            click = 3;
            int j = 0;
            dataGridViewInput.Rows.Clear();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo1")
                {
                    dataGridViewInput.Rows.Add(IOManage.IODoc.m_InputDataList[i]);
                    dataGridViewInput.Rows[j].Cells[0].Value = IOManage.IODoc.m_InputDataList[i].strIOName;
                    dataGridViewInput.Rows[j].Cells[1].Value = IOManage.IODoc.m_InputDataList[i].iInputNo;
                    j++;
                }
            }


            dataGridViewOutput.Rows.Clear();
            j = 0;
            for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo1")
                {
                    dataGridViewOutput.Rows.Add(IOManage.IODoc.m_OutputDataList[i]);
                    dataGridViewOutput.Rows[j].Cells[0].Value = IOManage.IODoc.m_OutputDataList[i].strIOName;
                    dataGridViewOutput.Rows[j].Cells[1].Value = IOManage.IODoc.m_OutputDataList[i].iOutputNo;
                    j++;
                }
            }
        }

        private void 模块3_Click(object sender, EventArgs e)
        {
            //if (DateSave.Instance().Production.ModelNo != "" && 机种选择.Text == "")
            //{
            //    机种选择.Text = DateSave.Instance().Production.ModelNo;

            //    if (机种选择.Text != "")
            //    {
            //        var files = Directory.GetFiles(stfPath + "\\" + 机种选择.Text, "*.csv");
            //        if (运行流程选择.Items.Count > 0)
            //        {
            //            运行流程选择.Items.Clear();
            //        }
            //        foreach (var file in files)
            //        {
            //            string fileNameExt = file.Substring(file.LastIndexOf("\\") + 1); //获取文件名，不带路径
            //            string filePath = file.Substring(0, file.LastIndexOf("\\"));//获取文件路径，不带文件名
            //            运行流程选择.Items.Add(fileNameExt);
            //        }
            //        运行流程选择.SelectedIndex = 0;
            //    }


            //}
            timer1.Start();
            click = 4;
            int j = 0;
            dataGridViewInput.Rows.Clear();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo2")
                {
                    dataGridViewInput.Rows.Add(IOManage.IODoc.m_InputDataList[i]);
                    dataGridViewInput.Rows[j].Cells[0].Value = IOManage.IODoc.m_InputDataList[i].strIOName;
                    dataGridViewInput.Rows[j].Cells[1].Value = IOManage.IODoc.m_InputDataList[i].iInputNo;
                    j++;
                }
            }

            dataGridViewOutput.Rows.Clear();
            j = 0;
            for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo2")
                {
                    dataGridViewOutput.Rows.Add(IOManage.IODoc.m_OutputDataList[i]);
                    dataGridViewOutput.Rows[j].Cells[0].Value = IOManage.IODoc.m_OutputDataList[i].strIOName;
                    dataGridViewOutput.Rows[j].Cells[1].Value = IOManage.IODoc.m_OutputDataList[i].iOutputNo;
                    j++;
                }
            }
        }

        private void 模块4_Click(object sender, EventArgs e)
        {
            timer1.Start();
            click = 5;
            int j = 0;
            dataGridViewInput.Rows.Clear();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo3")
                {
                    dataGridViewInput.Rows.Add(IOManage.IODoc.m_InputDataList[i]);
                    dataGridViewInput.Rows[j].Cells[0].Value = IOManage.IODoc.m_InputDataList[i].strIOName;
                    dataGridViewInput.Rows[j].Cells[1].Value = IOManage.IODoc.m_InputDataList[i].iInputNo;
                    j++;
                }
            }
            dataGridViewOutput.Rows.Clear();
            j = 0;
            for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo3")
                {
                    dataGridViewOutput.Rows.Add(IOManage.IODoc.m_OutputDataList[i]);
                    dataGridViewOutput.Rows[j].Cells[0].Value = IOManage.IODoc.m_OutputDataList[i].strIOName;
                    dataGridViewOutput.Rows[j].Cells[1].Value = IOManage.IODoc.m_OutputDataList[i].iOutputNo;
                    j++;            
                }
            }
        }

        private void 模块5_Click(object sender, EventArgs e)
        {
            timer1.Start();
            click = 6;
            int j = 0;
            dataGridViewInput.Rows.Clear();
            for (int i = 0; i < IOManage.IODoc.m_InputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_InputDataList[i].InputCardName == "Googlo4")
                {
                    dataGridViewInput.Rows.Add(IOManage.IODoc.m_InputDataList[i]);
                    dataGridViewInput.Rows[j].Cells[0].Value = IOManage.IODoc.m_InputDataList[i].strIOName;
                    dataGridViewInput.Rows[j].Cells[1].Value = IOManage.IODoc.m_InputDataList[i].iInputNo;
                    j++;
                }
            }
            dataGridViewOutput.Rows.Clear();
            j = 0;
            for (int i = 0; i < IOManage.IODoc.m_OutputDataList.Count; i++)
            {
                if (IOManage.IODoc.m_OutputDataList[i].OutputCardName == "Googlo4")
                {
                    dataGridViewOutput.Rows.Add(IOManage.IODoc.m_OutputDataList[i]);
                    dataGridViewOutput.Rows[j].Cells[0].Value = IOManage.IODoc.m_OutputDataList[i].strIOName;
                    dataGridViewOutput.Rows[j].Cells[1].Value = IOManage.IODoc.m_OutputDataList[i].iOutputNo;
                    j++;
                  
                }              
            }
        }

        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridViewOutput_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
            int CIndex = e.ColumnIndex;
            if (CIndex == 3&& e.RowIndex != -1)
            {
                string strName = dataGridViewOutput.Rows[e.RowIndex].Cells[0].Value.ToString();
                IOManage.OUTPUT(strName).SetOutBit(true);
            }
            else if(CIndex == 4 && e.RowIndex != -1)
            {
                string strName = dataGridViewOutput.Rows[e.RowIndex].Cells[0].Value.ToString();
                IOManage.OUTPUT(strName).SetOutBit(false);
            }
        }

        private void 手动进料_Click(object sender, EventArgs e)
        {

        }

        private void 手动操作窗体_FormClosed(object sender, FormClosedEventArgs e)
        {
            OpenForm = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private Point mouse_offset;
        private void 手动操作窗体_MouseDown(object sender, MouseEventArgs e)
        {
         //   mouse_offset = new Point(-e.X, -e.Y);
        }

        private void 手动操作窗体_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button==MouseButtons.Left)
            //{
            //    Point mouse = Control.MousePosition;
            //    mouse.Offset(mouse_offset.X, mouse_offset.Y);
            //    this.Location = mouse;

            //} 
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private Point mouse_offset12;
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset12 = new Point(-e.X, -e.Y);
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouse = Control.MousePosition;
                mouse.Offset(mouse_offset12.X, mouse_offset12.Y);
                this.Location = mouse;

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
      //  FormTableDriver FormTable;
        private void 机种更新_Click(object sender, EventArgs e)
        {
            if (机种选择.Text != "")
            {
                var files = Directory.GetFiles(stfPath + "\\" + 机种选择.Text, "*.csv");
                if (运行流程选择.Items.Count>0)
                {
                    运行流程选择.Items.Clear();
                }
                foreach (var file in files)
                {
                    string fileNameExt = file.Substring(file.LastIndexOf("\\") + 1); //获取文件名，不带路径
                    string filePath = file.Substring(0, file.LastIndexOf("\\"));//获取文件路径，不带文件名
                    运行流程选择.Items.Add(fileNameExt);
                     //  TableManage.LoadData();
                }
                string stfPath12 = stfPath + "\\" + 机种选择.Text + "\\TablesDoc.xml";
                string stfPath55 = System.Environment.CurrentDirectory + "\\Parameter\\TablesDoc.xml";
                if (File.Exists(stfPath55))
                {
                    File.Delete(stfPath55);
                }
                if (!File.Exists(stfPath55))
                {
                    File.Copy(stfPath12, stfPath55);
                }
                MessageBox.Show("更新完成");
            }
            else
            {
                MessageBox.Show("数据更新失败");

            }
        }
      
        private void 运行流程_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            if (MainModule.FormMain.bAuto)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先停止自动运行！");
                return;
            }
            
            if (RunClass.Instance().Run_OneCase == null)
            {
                if (机种选择.Text != "" && 运行流程选择.Text != "")
                {
                    DateSave.Instance().Production.IsStop = false;
                    RunClass.Instance().RunClass_IsFinish = false;
                    RunClass.Instance().runTask(ReadstfPath + "\\" + 机种选择.Text + "\\" + 运行流程选择.Text);
                }
            }
            else
            {
                if (RunClass.Instance().Run_OneCase.IsAlive == true)
                {
                    MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先停止运行！");
                    return;
                }
                if (机种选择.Text != "" && 运行流程选择.Text != "")
                {
                    DateSave.Instance().Production.IsStop = false;
                    RunClass.Instance().RunClass_IsFinish = false;
                    RunClass.Instance().runTask(ReadstfPath + "\\" + 机种选择.Text + "\\" + 运行流程选择.Text);
                }
            }                    
        }
        public void ReadCode(string path)// Read  code
        {
            List<string> ReadListStr = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                ReadListStr.Add(line);
            }
            for (int i = 1; i < ReadListStr.Count; i++)
            {
                string[] Code = ReadListStr[i].Split(',');
                if (Code[0].Contains("轴运动"))
                {
                    RunItem_List.Add(new KeyValuePair<string, KeyValuePair<string, string>>(Code[0], new KeyValuePair<string, string>(Code[1], "")));
                    RunItem_List1111.Add(new KeyValuePair<string, string>(Code[1], ""));
                    ListStr.Add(Code[1]);

                }
                else
                {
                    RunItem_List.Add(new KeyValuePair<string, KeyValuePair<string, string>>(Code[0], new KeyValuePair<string, string>(Code[1], Code[2])));
                    RunItem_List1111.Add(new KeyValuePair<string, string>(Code[1], Code[2]));
                    ListStr.Add(Code[1]);

                }

            }
        }
        public  Method Meth = Method.Instance();
        public MainControl MainControl= MainControl.Instance();
        private void 机种保存_Click(object sender, EventArgs e)
        {
            if (机种选择.Text != "")
            {
                DateSave.Instance().SaveDoc();
                DateSave.Instance().Production.ModelNo = 机种选择.Text;
                DateSave.Instance().SaveDoc_Other(机种选择.Text);
               // TableManage.tablesDoc.SaveDoc();
                //pointdatacom.Items.Add(fileNameExt);
                TableManage.tablesDoc.SaveDocOtherForder(stfPath + "\\" + 机种选择.Text + "\\TablesDoc" + ".xml");
                MessageBox.Show("机种数据更新完成");
            }
            else
            {
                MessageBox.Show("机种数据更新失败");

            }
        }
        private void 添加机种_Click(object sender, EventArgs e)
        {
            if (新机种号.Text!="")
            {
                机种选择.Items.Add(新机种号.Text);
                string stfPathForder = System.Environment.CurrentDirectory;
              //  MainControl.ProductionData.CreatFolderNew(stfPathForder+ "\\FlowDocument\\" + 新机种号.Text);
            }     
        }

        private void 添加数据_Click(object sender, EventArgs e)
        {
            if (数据名号.Text != "")
            {
                //机种选择.Items.Add(新机种号.Text);
                //string stfPathForder = System.Environment.CurrentDirectory;
                //MainControl.ProductionData.CreatFolderNew(stfPathForder + "\\FlowDocument\\" + 新机种号.Text);
            }
            
        }

        private void 手动过MES_Click(object sender, EventArgs e)
        {
            if (手动过MES_Sn.Text != "")
            {
             //   string ISOK = mes.Instance().CellToolingPlate(手动过MES_Sn.Text.Replace("\r\n", ""));
                string sww = mes.Instance().userCode;
                string sww12 = mes.Instance().deviceCode;
                string result = "";
                string MesStr = "";
                for (int i=0;i<16;i++)
                {

                    //  string da=    mes.Instance().WipTest(ISOK, "PASS", sww, sww12, "", "");//上传数据
                     MesStr = "|波形号:" + "1"
                                             + "|速度:" + "50"
                                             + "|加速度:" + "5" + "|基准高度:" + "121" + "|最大功率:" + "4000" + "|反馈功率:" + "2900" + "|焊接高度:" + "121" + "|焊接半径:" + "4" + "|离焦量:" + "1" + "|";
                    //mes.Instance().WipTest(DateSave.Instance().Production.DataReceivedstrSN.Replace("\r\n", ""), "PASS", mes.Instance().userCode, mes.Instance().deviceCode, "", "");//上传数据

                     result = mes.Instance().WipTest(手动过MES_Sn.Text.Replace("\r\n", ""), "PASS", mes.Instance().userCode, mes.Instance().deviceCode, "", "");//过站校验

                    if (result == "OK")
                    {
                        string res = mes.Instance().OfflineUploadData(手动过MES_Sn.Text.Replace("\r\n", ""), 1.ToString(), "weld", "PASS", mes.Instance().userCode, mes.Instance().deviceCode, "", MesStr);//上传数据
                        if (res == "OK")
                        {
                          //  MessageBox.Show(res);
                         //   return;
                        }
                    }
                    else
                    {
                       // MessageBox.Show(result);
                     //   return;
                    }

                }
             
             
            }
           
        }

        private void 扫码_Click(object sender, EventArgs e)
        {
            string value = "T";
            SerialPortDataManage.m_SerilPorts["扫码枪"].GetData(ref value);
            if (value != "")
            {
                手动过MES_Sn.Text = value;
            }
            else
            {
                MessageBox.Show("手动扫码失败");

            }
        

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!MainModule.FormMain.bHomeReady)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先回原点！");
                return;
            }
            if (MainModule.FormMain.bAuto)
            {
                MainModule.FormMain.m_formAlarm.InsertAlarmMessage("请先停止自动运行！");
                return;
            }


            string XX = textBox1.Text;
            string YY = textBox2.Text;
            string SendDate = "Offset;" + XX + ";" + YY + ";" + "0;";
            Socket_server.Instance().sendDataToMac(SendDate);
        }
    }
}
