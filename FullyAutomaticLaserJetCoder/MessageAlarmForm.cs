using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FullyAutomaticLaserJetCoder
{
    public partial class MessageAlarmForm : Form
    {
        private AutoResetEvent resultResetEvent = new AutoResetEvent(false);
        public string FormHint = "";
        DialogResult dialogResult = DialogResult.None;
        public MessageAlarmForm()
        {
            InitializeComponent();
        }

        private void MessageAlarmForm_Load(object sender, EventArgs e)
        {

        }
        public static DialogResult Show(string title, string message)
        {
            MessageAlarmForm messageAlarmForm = new MessageAlarmForm();
            return messageAlarmForm.ShowForm(title, message);
        }


        public DialogResult ShowForm(string title, string message)
        {
            label_title.Text = title;
            label_message.Text = message;

            this.TopMost = true;
            this.Show();
            //resultResetEvent.WaitOne();

            return dialogResult;

        }

        private void MessageAlarmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            resultResetEvent.Set();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormHint = "hint";
            this.Hide();
            dialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormHint = "hint";
            this.Hide();    
            dialogResult = DialogResult.Cancel;
        }
        public string InputBox(string Caption, string Hint, string Default)
        {

            label_title.Text = Caption;
            label_message.Text = Hint+"+"+ Default;
            try
            {
                button1.DialogResult = DialogResult.OK;
                if (ShowDialog() == DialogResult.OK)
                {
                      return "OK";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                 return null;
            }
            finally
            {
                Dispose();
            }
        }
    }

}
