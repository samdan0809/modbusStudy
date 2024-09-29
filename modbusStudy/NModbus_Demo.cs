using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using NModbus;
using NModbus.Device;

namespace modbusStudy
{
    public partial class NModbus_Demo : Form
    {


        IModbusMaster client = null;

        public NModbus_Demo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                if (client == null || client.Transport == null)
                {
                    richTextBox1.Text = "先连接";
                    return;
                }

                var result = client.ReadHoldingRegisters(1, (ushort)numericUpDown2.Value, 1);  //寄存器初始地址号

                richTextBox1.Text = Convert.ToString(result[0]);


            }
            catch (Exception ex)
            {


                richTextBox1.Text = ex.Message;
            }


        }



        private void button3_Click(object sender, EventArgs e)
        {

            try
            {

                if (client == null || client.Transport == null)
                {
                    richTextBox1.Text = "先连接";
                    return;
                }
                var data = Convert.ToUInt16(textBox1.Text);
              
                client.WriteSingleRegister(1,(ushort)numericUpDown2.Value,data);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var me = (CheckBox)sender;
            if (me.Checked)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

      

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (client==null || client.Transport==null)
                client =helper.getNClient(txt_ip.Text,int.Parse(txt_port.Text));
            try {

               
            }
            catch (Exception ex)
            {


                richTextBox1.Text = "从站连接出错";
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (client != null)
                client.Dispose();
          
        }
    }
}
