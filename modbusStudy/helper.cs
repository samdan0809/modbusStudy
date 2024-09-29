using EasyModbus;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IoTClient.Clients.Modbus;

using NModbus;
using NModbus.Device;
using System.Net;
using System.Net.Sockets;

using System.Net.Sockets;
namespace modbusStudy
{
    public class helper
    {
        public static ModbusClient getClient(string ip,int port)
        {
            ModbusClient modbusClient = new ModbusClient(ip, port)
            {
                Parity = Parity.None, //校验位
                StopBits = StopBits.One, //停止位
                ConnectionTimeout = 500
            };

            return modbusClient;
        }
        public static ModbusServer getServer(int port)
        {
            ModbusServer server = new ModbusServer()
            {
                Parity = Parity.None, //校验位
                StopBits = StopBits.One, //停止位
                Port = port,

            };

            return server;
        }

        public static ModbusTcpClient getIotClient(string ip, int port)
        {
            var client = new ModbusTcpClient(ip,port);
            return client;
        }
      
        public static IModbusMaster getNClient(string ip, int port)
        {
            var tcpClient = new TcpClient( );
            tcpClient.Connect(ip, port);

          
            var factory = new NModbus.ModbusFactory();
            var client=factory.CreateMaster(tcpClient);

            

            return client;
        }

    }
}
