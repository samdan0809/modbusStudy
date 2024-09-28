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
using EasyModbus;
using Microsoft.Win32;
namespace modbusStudy
{
    public partial class Form1 : Form
    {
        ModbusServer server=null;

        ModbusClient client = null;
        public Form1()
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

                int[] result = client.ReadHoldingRegisters((int)numericUpDown2.Value, 1);  //寄存器初始地址号
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    builder.Append(Convert.ToString(result[i]) + "\n");
                }
                richTextBox1.Text = builder.ToString();

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
                client.WriteSingleRegister((int)numericUpDown2.Value, data);


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

        private void button5_Click(object sender, EventArgs e)
        {
            server = helper.getServer(int.Parse(txt_port_slave.Text));

            var me=(Button)sender;
            if (me.Text.Contains("运行从站")){
                server.Listen();
                timer_slave.Start();
                
              

                me.Text = "停止运行";
            }
            else
            {
                if (server!=null  )
                server.StopListening();
                timer_slave.Stop();
                me.Text = "运行从站";
            }
            

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (client==null)
                client =helper. getClient(txt_ip.Text,int.Parse(txt_port.Text));
            try {
                client.Connect();
            }
            catch (Exception ex)
            {


                richTextBox1.Text = "从站连接出错";
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (client != null)
                client.Disconnect();
          
        }

        private void timer_slave_Tick(object sender, EventArgs e)
        {
            if (button5.Text.Contains("停止"))
            {
                for (int i = 0; i < 10; i++)
                {

                    short val = server.holdingRegisters[i+1];


                    var controls = this.Controls.Find("data_" + i, true);
                    if (controls != null && controls.Length > 0)
                    {
                        controls[0].Text = val.ToString();
                    }
                }
               
            }
        }
       

        
    }
}
