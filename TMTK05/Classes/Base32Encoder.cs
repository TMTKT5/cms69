#region

using System.Text;

#endregion

namespace TMTK05.Classes
{
    /// <summary>
    ///     Encodes text into Base32. Taken from
    ///     http: //www.codeproject.com/Articles/35492/Base32-encoding-implementation-in-NET
    /// </summary>
    public sealed class Base32Encoder
    {
        #region Private Fields

        private const string DefEncodingTable = "abcdefghijklmnopqrstuvwxyz234567";
        private const char DefPadding = '=';

        private readonly byte[] _dTable;
        private readonly string _eTable; //Encoding table
        private readonly char _padding;

        #endregion Private Fields

        #region Public Constructors

        public Base32Encoder()
            : this(DefEncodingTable, DefPadding)
        {
        }

        private Base32Encoder(string encodingTable, char padding)
        {
            _eTable = encodingTable;
            _padding = padding;
            _dTable = new byte[0x80];
            InitialiseDecodingTable();
        }

        #endregion Public Constructors

        #region Public Methods

        public string Encode(byte[] input)
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

                output.Append(_eTable[(b1 >> 3) & 0x1f]);
                output.Append(_eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                output.Append(_eTable[(b2 >> 1) & 0x1f]);
                output.Append(_eTable[((b2 << 4) | (b3 >> 4)) & 0x1f]);
                output.Append(_eTable[((b3 << 1) | (b4 >> 7)) & 0x1f]);
                output.Append(_eTable[(b4 >> 2) & 0x1f]);
                output.Append(_eTable[((b4 << 3) | (b5 >> 5)) & 0x1f]);
                output.Append(_eTable[b5 & 0x1f]);
            }

            switch (specialLength)
            {
                case 1:
                {
                    var b1 = input[normalLength] & 0xff;
                    output.Append(_eTable[(b1 >> 3) & 0x1f]);
                    output.Append(_eTable[(b1 << 2) & 0x1f]);
                    output.Append(_padding)
                        .Append(_padding)
                        .Append(_padding)
                        .Append(_padding)
                        .Append(_padding)
                        .Append(_padding);
                    break;
                }

                case 2:
                {
                    var b1 = input[normalLength] & 0xff;
                    var b2 = input[normalLength + 1] & 0xff;
                    output.Append(_eTable[(b1 >> 3) & 0x1f]);
                    output.Append(_eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                    output.Append(_eTable[(b2 >> 1) & 0x1f]);
                    output.Append(_eTable[(b2 << 4) & 0x1f]);
                    output.Append(_padding).Append(_padding).Append(_padding).Append(_padding);
                    break;
                }
                case 3:
                {
                    var b1 = input[normalLength] & 0xff;
                    var b2 = input[normalLength + 1] & 0xff;
                    var b3 = input[normalLength + 2] & 0xff;
                    output.Append(_eTable[(b1 >> 3) & 0x1f]);
                    output.Append(_eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                    output.Append(_eTable[(b2 >> 1) & 0x1f]);
                    output.Append(_eTable[((b2 << 4) | (b3 >> 4)) & 0x1f]);
                    output.Append(_eTable[(b3 << 1) & 0x1f]);
                    output.Append(_padding).Append(_padding).Append(_padding);
                    break;
                }
                case 4:
                {
                    var b1 = input[normalLength] & 0xff;
                    var b2 = input[normalLength + 1] & 0xff;
                    var b3 = input[normalLength + 2] & 0xff;
                    var b4 = input[normalLength + 3] & 0xff;
                    output.Append(_eTable[(b1 >> 3) & 0x1f]);
                    output.Append(_eTable[((b1 << 2) | (b2 >> 6)) & 0x1f]);
                    output.Append(_eTable[(b2 >> 1) & 0x1f]);
                    output.Append(_eTable[((b2 << 4) | (b3 >> 4)) & 0x1f]);
                    output.Append(_eTable[((b3 << 1) | (b4 >> 7)) & 0x1f]);
                    output.Append(_eTable[(b4 >> 2) & 0x1f]);
                    output.Append(_eTable[(b4 << 3) & 0x1f]);
                    output.Append(_padding);
                    break;
                }
            }

            return output.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        private void InitialiseDecodingTable()
        {
            for (var i = 0; i < _eTable.Length; i++)
            {
                _dTable[_eTable[i]] = (byte) i;
            }
        }

        #endregion Private Methods

        //Decoding table
    }
}