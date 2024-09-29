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
using IoTClient.Clients.Modbus;
using Microsoft.Win32;
namespace modbusStudy
{
    public partial class IotClient_Demo : Form
    {
   

        ModbusTcpClient client = null;
        public IotClient_Demo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                if (client == null || !client.Connected)
                {
                    richTextBox1.Text = "先连接";
                    return;
                }

                var result = client.ReadInt16(numericUpDown2.Value.ToString());  //寄存器初始地址号
                if (result.IsSucceed)
                {
                    richTextBox1.Text = Convert.ToString(result.Value);
                }
               

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

                if (client == null || !client.Connected)
                {
                    richTextBox1.Text = "先连接";
                    return;
                }
                var data = Convert.ToInt16(textBox1.Text);
              
                client.Write(numericUpDown2.Value.ToString(),data);


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
            if (client==null)
                client =helper.getIotClient(txt_ip.Text,int.Parse(txt_port.Text));
            try {

                client.Open();
            }
            catch (Exception ex)
            {


                richTextBox1.Text = "从站连接出错";
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (client != null)
                client.Close();
          
        }
    }
}
