#region

using System.Collections.Generic;
using System.Text;

#endregion

namespace TMTK05.Classes
{
    /// <summary>
    ///     Encodes text into Base32. Taken from
    ///     http://www.codeproject.com/Articles/35492/Base32-encoding-implementation-in-NET
    /// </summary>
    public class Base32Encoder
    {
        #region Private Fields

        private const string DEF_ENCODING_TABLE = "abcdefghijklmnopqrstuvwxyz234567";
        private const char DEF_PADDING = '=';

        private readonly byte[] dTable;
        private readonly string eTable; //Encoding table
        private readonly char padding;

        #endregion Private Fields

        #region Public Constructors

        public Base32Encoder()
            : this(DEF_ENCODING_TABLE, DEF_PADDING)
        {
        }

        public Base32Encoder(char padding)
            : this(DEF_ENCODING_TABLE, padding)
        {
        }

        public Base32Encoder(string encodingTable)
            : this(encodingTable, DEF_PADDING)
        {
        }

        public Base32Encoder(string encodingTable, char padding)
        {
            eTable = encodingTable;
            this.padding = padding;
            dTable = new byte[0x80];
            InitialiseDecodingTable();
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual byte[] Decode(string data)
        {
            var outStream = new List<byte>();

            var length = data.Length;
            while (length > 0)
            {
                if (!Ignore(data[length - 1])) break;
                length--;
            }

            var i = 0;
            var finish = length - 8;
            for (i = NextI(data, i, finish); i < finish; i = NextI(data, i, finish))
            {
                var b1 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b2 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b3 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b4 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b5 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b6 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b7 = dTable[data[i++]];
                i = NextI(data, i, finish);
                var b8 = dTable[data[i++]];

                outStream.Add((byte) ((b1 << 3) | (b2 >> 2)));
                outStream.Add((byte) ((b2 << 6) | (b3 << 1) | (b4 >> 4)));
                outStream.Add((byte) ((b4 << 4) | (b5 >> 1)));
                outStream.Add((byte) ((b5 << 7) | (b6 << 2) | (b7 >> 3)));
                outStream.Add((byte) ((b7 << 5) | b8));
            }
            DecodeLastBlock(outStream,
                data[length - 8], data[length - 7], data[length - 6], data[length - 5],
                data[length - 4], data[length - 3], data[length - 2], data[length - 1]);

            return outStream.ToArray();
        }

        public virtual string Encode(byte[] input)
        {
            var output = new StringBuilder();
            var specialLength = input.Length%5;
            var normalLength = input.Length - specialLength;
            for (var i = 0; i < normalLength; i += 5)
            {
                var b1 = input[i] & 0xff;
                var b2 = input[i + 1] & 0xff;
                var b3 = input[i + 2] & 0xff;
                var b4 = input[i + 3] & 0xff;
                var b5 = input[i + 4] & 0xff;

                output.Append(eTable[(b1 >> 3) & 0x1f]);
                output.Append(eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                output.Append(eTable[(b2 >> 1) & 0x1f]);
                output.Append(eTable[((b2 << 4) | (b3 >> 4)) & 0x1f]);
                output.Append(eTable[((b3 << 1) | (b4 >> 7)) & 0x1f]);
                output.Append(eTable[(b4 >> 2) & 0x1f]);
                output.Append(eTable[((b4 << 3) | (b5 >> 5)) & 0x1f]);
                output.Append(eTable[b5 & 0x1f]);
            }

            switch (specialLength)
            {
                case 1:
                {
                    var b1 = input[normalLength] & 0xff;
                    output.Append(eTable[(b1 >> 3) & 0x1f]);
                    output.Append(eTable[(b1 << 2) & 0x1f]);
                    output.Append(padding)
                        .Append(padding)
                        .Append(padding)
                        .Append(padding)
                        .Append(padding)
                        .Append(padding);
                    break;
                }

                case 2:
                {
                    var b1 = input[normalLength] & 0xff;
                    var b2 = input[normalLength + 1] & 0xff;
                    output.Append(eTable[(b1 >> 3) & 0x1f]);
                    output.Append(eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                    output.Append(eTable[(b2 >> 1) & 0x1f]);
                    output.Append(eTable[(b2 << 4) & 0x1f]);
                    output.Append(padding).Append(padding).Append(padding).Append(padding);
                    break;
                }
                case 3:
                {
                    var b1 = input[normalLength] & 0xff;
                    var b2 = input[normalLength + 1] & 0xff;
                    var b3 = input[normalLength + 2] & 0xff;
                    output.Append(eTable[(b1 >> 3) & 0x1f]);
                    output.Append(eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                    output.Append(eTable[(b2 >> 1) & 0x1f]);
                    output.Append(eTable[((b2 << 4) | (b3 >> 4)) & 0x1f]);
                    output.Append(eTable[(b3 << 1) & 0x1f]);
                    output.Append(padding).Append(padding).Append(padding);
                    break;
                }
                case 4:
                {
                    var b1 = input[normalLength] & 0xff;
                    var b2 = input[normalLength + 1] & 0xff;
                    var b3 = input[normalLength + 2] & 0xff;
                    var b4 = input[normalLength + 3] & 0xff;
                    output.Append(eTable[(b1 >> 3) & 0x1f]);
                    output.Append(eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                    output.Append(eTable[(b2 >> 1) & 0x1f]);
                    output.Append(eTable[((b2 << 4) | (b3 >> 4)) & 0x1f]);
                    output.Append(eTable[((b3 << 1) | (b4 >> 7)) & 0x1f]);
                    output.Append(eTable[(b4 >> 2) & 0x1f]);
                    output.Append(eTable[(b4 << 3) & 0x1f]);
                    output.Append(padding);
                    break;
                }
            }

            return output.ToString();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual int DecodeLastBlock(ICollection<byte> outStream, char c1, char c2, char c3, char c4, char c5,
            char c6, char c7, char c8)
        {
            if (c3 == padding)
            {
                var b1 = dTable[c1];
                var b2 = dTable[c2];
                outStream.Add((byte) ((b1 << 3) | (b2 >> 2)));
                return 1;
            }

            if (c5 == padding)
            {
                var b1 = dTable[c1];
                var b2 = dTable[c2];
                var b3 = dTable[c3];
                var b4 = dTable[c4];
                outStream.Add((byte) ((b1 << 3) | (b2 >> 2)));
                outStream.Add((byte) ((b2 << 6) | (b3 << 1) | (b4 >> 4)));
                return 2;
            }

            if (c6 == padding)
            {
                var b1 = dTable[c1];
                var b2 = dTable[c2];
                var b3 = dTable[c3];
                var b4 = dTable[c4];
                var b5 = dTable[c5];

                outStream.Add((byte) ((b1 << 3) | (b2 >> 2)));
                outStream.Add((byte) ((b2 << 6) | (b3 << 1) | (b4 >> 4)));
                outStream.Add((byte) ((b4 << 4) | (b5 >> 1)));
                return 3;
            }

            if (c8 == padding)
            {
                var b1 = dTable[c1];
                var b2 = dTable[c2];
                var b3 = dTable[c3];
                var b4 = dTable[c4];
                var b5 = dTable[c5];
                var b6 = dTable[c6];
                var b7 = dTable[c7];

                outStream.Add((byte) ((b1 << 3) | (b2 >> 2)));
                outStream.Add((byte) ((b2 << 6) | (b3 << 1) | (b4 >> 4)));
                outStream.Add((byte) ((b4 << 4) | (b5 >> 1)));
                outStream.Add((byte) ((b5 << 7) | (b6 << 2) | (b7 >> 3)));
                return 4;
            }
            else
            {
                var b1 = dTable[c1];
                var b2 = dTable[c2];
                var b3 = dTable[c3];
                var b4 = dTable[c4];
                var b5 = dTable[c5];
                var b6 = dTable[c6];
                var b7 = dTable[c7];
                var b8 = dTable[c8];
                outStream.Add((byte) ((b1 << 3) | (b2 >> 2)));
                outStream.Add((byte) ((b2 << 6) | (b3 << 1) | (b4 >> 4)));
                outStream.Add((byte) ((b4 << 4) | (b5 >> 1)));
                outStream.Add((byte) ((b5 << 7) | (b6 << 2) | (b7 >> 3)));
                outStream.Add((byte) ((b7 << 5) | b8));
                return 5;
            }
        }

        protected bool Ignore(char c)
        {
            return (c == '\n') || (c == '\r') || (c == '\t') || (c == ' ') || (c == '-');
        }

        protected void InitialiseDecodingTable()
        {
            for (var i = 0; i < eTable.Length; i++)
            {
                dTable[eTable[i]] = (byte) i;
            }
        }

        protected int NextI(string data, int i, int finish)
        {
            while ((i < finish) && Ignore(data[i])) i++;

            return i;
        }

        #endregion Protected Methods

        //Decoding table
    }
}