using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using System.Data.SqlClient;

namespace WorldGeneralLib
{
    public class SHSFComunicate
    {
        TextBox txtBoxCurrentPressure;
        ToolStripStatusLabel toolStripStatus;
        SHSFData m_data;
        bool bInitOK = false;
        public SHSFComunicate(TextBox txtBox, ToolStripStatusLabel toolStrip)
        {
            txtBoxCurrentPressure = txtBox;
            toolStripStatus = toolStrip;
            toolStripStatus.Click += toolStripStatus_Click;
            m_data = SHSFData.LoadObj(toolStrip.Text);
            Init();
        
        }

        void toolStripStatus_Click(object sender, EventArgs e)
        {
            FormSHSF frm = new FormSHSF(m_data);
            frm.ShowDialog();
        }
        public bool Init()
        {
            
            //"Data Source=WEIDY\\SQLEXPRESS;Initial Catalog=SHSFTest;User ID=sa;Password=123456";
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = m_data.ConnectStr;
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        toolStripStatus.BackColor = Color.Green;
                    }
                    conn.Close();
                    if (conn.State == ConnectionState.Closed)
                    {
                        toolStripStatus.BackColor = Color.Green;
                    }
                    bInitOK=true;
                }
            }
            catch //(Exception eP)
            {
                toolStripStatus.BackColor = Color.Red;
                bInitOK = false;
            }
            bInitOK = true;
            Action action=()=>
            {
                if (bInitOK)
                {
                    AddRunMessage("Connect OK");
                }
                else
                {
                    AddRunMessage("Connect NG");
                }
            };
            txtBoxCurrentPressure.Invoke(action);
            return bInitOK;
        }
        public string UpdateData(string strStep,string strInput)
        {
            if (bInitOK == false)
                return "#Error NotConnnect";
            //"Data Source=WEIDY\\SQLEXPRESS;Initial Catalog=SHSFTest;User ID=sa;Password=123456";
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = m_data.ConnectStr;
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("MonitorPortal", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@BU", System.Data.SqlDbType.NVarChar));
                    sqlCommand.Parameters["@BU"].Value = m_data.BuName;

                    sqlCommand.Parameters.Add(new SqlParameter("@Station", System.Data.SqlDbType.NVarChar));
                    sqlCommand.Parameters["@Station"].Value = m_data.Station;

                    sqlCommand.Parameters.Add(new SqlParameter("@Step", System.Data.SqlDbType.NVarChar));
                    sqlCommand.Parameters["@Step"].Value = strStep;

                    sqlCommand.Parameters.Add(new SqlParameter("@InPutStr", System.Data.SqlDbType.NVarChar));
                    sqlCommand.Parameters["@InPutStr"].Value = strInput;

                    sqlCommand.Parameters.Add(new SqlParameter("@OutPutStr", System.Data.SqlDbType.NVarChar, 4000));// 注意：对于输出参数，必须指定参数的尺寸   
                    sqlCommand.Parameters["@OutPutStr"].Direction = ParameterDirection.Output;

                    sqlCommand.Parameters.Add(new SqlParameter("@retval", System.Data.SqlDbType.Int));
                    sqlCommand.Parameters["@retval"].Direction = ParameterDirection.ReturnValue;

                    sqlCommand.ExecuteNonQuery(); // 开始执行存储过程   
                    AddRunMessage(sqlCommand.Parameters["@InPutStr"].Value.ToString());
                    return sqlCommand.Parameters["@InPutStr"].Value.ToString();
                }
            }
            catch (Exception eP)
            {
                AddRunMessage(eP.Message);
                return eP.Message;
            }
        }
        public void AddRunMessage(string strMessage)
        {
            if (txtBoxCurrentPressure == null)
                return;
            Action action = () =>
            {
                try
                {
                    int iTotal = 0;
                    int iLenght = txtBoxCurrentPressure.Lines.Length;
                    txtBoxCurrentPressure.AppendText(DateTime.Now.ToString("HH:mm:ss") + ":  " + strMessage + "\r\n");
                    if (txtBoxCurrentPressure.Lines.Length > 200)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            iTotal = iTotal + txtBoxCurrentPressure.Lines[i].Length + 2;
                        }
                        txtBoxCurrentPressure.Text = txtBoxCurrentPressure.Text.Substring(iTotal);
                    }
                }
                catch
                {

                }
            };
            try
            {
                txtBoxCurrentPressure.Invoke(action);
            }
            catch
            {

            }
        }
    }
}
