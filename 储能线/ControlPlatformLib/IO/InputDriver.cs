using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public class InputDriver
    {
        public string strDriverName;
        IInputAction actionInput;
        int iInputNo;
        public string strRemark;
        bool bIgnore = false;
        public InputData ioData;
        public bool bPreStatus;
        public bool bready = false;
        bool bEdgeTaskOnGoing=false;
        object lockObj = new object();
        bool bRisingEdge;
        bool bFallingEdge;
        CommonTools.LimitedQueue<bool> risingQuene;
        CommonTools.LimitedQueue<bool> fallingQuene;
        public void Init(InputData data)
        {
            ioData = data;
            strDriverName = data.strIOName;
            try
            {
                if (HardwareManage.hardwardDictionary[ioData.InputCardName] is IInputAction)
                {
                    risingQuene = new CommonTools.LimitedQueue<bool>(2);
                    fallingQuene = new CommonTools.LimitedQueue<bool>(2);
                    actionInput = (IInputAction)HardwareManage.hardwardDictionary[ioData.InputCardName];
                    iInputNo = ioData.iInputNo;
                    bIgnore = ioData.bignore;
                    strRemark = ioData.strRemark;
                    bready = true;
                    //Thread risingThread = new Thread(RisingThread);
                    //risingThread.IsBackground = true;
                    //risingThread.Start();
                }
            }
            catch
            {

            }
        }
        bool bCheck=true;
        private void RisingThread()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (bCheck)
                {
                    risingQuene.Enqueue(actionInput.GetInputBit(iInputNo));
                    if (!risingQuene.FirstOrDefault() && risingQuene.LastOrDefault())
                    {
                        bCheck = false;
                        bRisingEdge = true;
                    }
                }
            }
        }

        public bool GetOn()
        {
            if (ioData.bignore)
            {
                return true;
            }
            if (bready == false)
            {
                return false;
            }
            return actionInput.GetInputBit(iInputNo);
        }
        public bool GetOff()
        {
            if (ioData.bignore)
            {
                return true;
            }
            if (bready == false)
            {
                return false;
            }
            return !actionInput.GetInputBit(iInputNo);
        }
        public bool On
        {
            get
            {
                if (ioData.bignore)
                {
                    return true;
                }
                if (bready == false)
                {
                    return false;
                }
                return actionInput.GetInputBit(iInputNo);
            }
        }
        public bool Off
        {
            get
            {
                if (ioData.bignore)
                {
                    return true;
                }
                if (bready == false)
                {
                    return false;
                }
                return !actionInput.GetInputBit(iInputNo);
            }
        }
        bool risingEdge;
        public async Task<bool> RisingEdge()
        {
            if (bEdgeTaskOnGoing)
                return false;
            try
            {
                risingEdge = false;
                if (ioData.bignore)
                {
                    return true;
                }
                if (bready == false)
                {
                    return false;
                }
                var task = new Task<bool>(() =>
                {
                    bEdgeTaskOnGoing = true;
                    risingQuene.Clear();
                    while (true)
                    {
                        Thread.Sleep(1);
                        risingQuene.Enqueue(actionInput.GetInputBit(iInputNo));
                        if (risingQuene.Count == 2 && !risingQuene.FirstOrDefault() && risingQuene.LastOrDefault())
                        {
                            risingEdge = true;
                            bEdgeTaskOnGoing = false;
                            return risingEdge;
                        }
                        else if(risingQuene.Count == 2 && risingQuene.FirstOrDefault() && risingQuene.LastOrDefault())
                        {
                            risingEdge = true;
                            bEdgeTaskOnGoing = false;
                            return risingEdge;
                        }
                    }
                });
                task.Start();
                await task;
                return risingEdge;
            }
            catch (Exception ex)
            {
                bEdgeTaskOnGoing = false;
                return false;
            }
        }

        public bool RisingEdges
        {
            get
            {
                if (ioData.bignore)
                {
                    return true;
                }
                if (bready == false)
                {
                    return false;
                }
                return risingEdge;
            }
            set { risingEdge = value; }
        }


        public async Task<bool> FallingEdge()
        {
            bool fallingEdge = false;
            if (ioData.bignore)
            {
                return true;
            }
            if (bready == false)
            {
                return false;
            }
            var task= new Task<bool>(() => {
                fallingQuene.Clear();
                while (true)
                {
                    Thread.Sleep(1);
                    fallingQuene.Enqueue(actionInput.GetInputBit(iInputNo));
                    if (!fallingQuene.FirstOrDefault() && fallingQuene.LastOrDefault())
                    {
                        fallingEdge = true;
                        return fallingEdge;
                    }
                }
            });
            task.Start();
            await task;
            return fallingEdge;
        }
    }
}
