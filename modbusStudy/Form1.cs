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
namespace modbusStudy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var modbusClient = getClient();
            try
            {
               
                modbusClient.Connect();
                int[] result = modbusClient.ReadHoldingRegisters((int)numericUpDown2.Value, 1);  //寄存器初始地址号
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    builder.Append(Convert.ToString(result[i]) + "\n");
                }
                richTextBox1.Text = builder.ToString();
                modbusClient.Disconnect();
            }
            catch (Exception ex)
            {
                modbusClient.Disconnect();

                richTextBox1.Text = "从站连接出错";
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var modbusClient = getClient();
            try
            { 
                modbusClient.Connect();

                modbusClient.WriteSingleRegister((int)numericUpDown2.Value,int.Parse( textBox1.Text));


                modbusClient.Disconnect();
            }
            catch (Exception ex)
            {
                modbusClient.Disconnect();
            }
        }
        private ModbusClient getClient()
        {
            ModbusClient modbusClient = new ModbusClient(txt_ip.Text, int.Parse(txt_port.Text))
            {
                Parity = Parity.None, //校验位
                StopBits = StopBits.One, //停止位
                ConnectionTimeout = 500
            };

            return modbusClient;
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
    }
}
