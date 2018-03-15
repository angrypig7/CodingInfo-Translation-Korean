using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppClient
{
    static class TCPProtocolConstants
    {
        public const byte NULL = 255;

        public const short REQ_SEQRCH = 0x01;
        public const short REQ_DATA = 0x02;
    }

    interface IProtocolUsable
    {
        byte[] getBytes();
        int getSize();
    }

    class Message : IProtocolUsable
    {
        Header header;
        Body body;

        byte[] bytes;

        public byte[] getBytes()
        {
            header.getBytes().CopyTo(bytes, 0);
            body.getBytes().CopyTo(bytes, header.getSize());
            return bytes;
        }

        public int getSize()
        {
            return header.getSize() + body.getSize();
        }
    }

    class Header : IProtocolUsable
    {
        public short MSGTYPE { get; set; }
        public short MSGSEQ { get; set; }
        public byte LASTMSG { get; set; }


        public byte[] getBytes()
        {
            byte[] bytes = new byte[8];

            BitConverter.GetBytes(MSGTYPE).CopyTo(bytes, 0);
            BitConverter.GetBytes(MSGSEQ).CopyTo(bytes, 2);
            BitConverter.GetBytes(LASTMSG).CopyTo(bytes, 4);
            bytes[7] = TCPProtocolConstants.NULL;
            return bytes;

        }

        public int getSize()
        {
            return 8;
        }
    }

    class Body : IProtocolUsable
    {
        public byte[] DATA { get; set; }

        public byte[] getBytes()
        {
            return DATA;
        }

        public int getSize()
        {
            return DATA.Length;
        }
    }
}
