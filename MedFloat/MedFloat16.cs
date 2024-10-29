using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFloat
{
    /* aka SFLOAT */
    /* 16-bit float */
    /* 12-bit mantissa, 4-bit exponent */
    public class MedFloat16
    {
        public MedFloat16(int mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;
        }

        public MedFloat16(byte[] value)
        {
            ParseData(value);
        }

        public MedFloat16(byte[] value, int index, int length = 2)
        {
            if (value.Length < index + length)
            {
                throw new ArgumentException("not enough data in array to convert to MedFloat16");
            }
            byte[] data = new byte[length];
            Array.Copy(value, index, data, 0, length);
            ParseData(data);
        }

        public float ToFloat()
        {
            return (float)(Mantissa * Math.Pow(10, Exponent));
        }

        public static float ToSFloat(byte[] value)
        {
            MedFloat16 sfloat = new MedFloat16(value);
            return sfloat.ToFloat();
        }

        private void ParseData(byte[] value)
        {
            if (value.Length == 2)
            {
                ParseUInt16((UInt16)(value[0] + (value[1] << 8)));
            }
            else
            {
                throw new ArgumentException("MedFloat16 => need array of 2 byte");
            }
        }

        private void ParseUInt16(UInt16 value)
        {
            Mantissa = UnsignedToSigned((value & 0x0FFF), 12);
            Exponent = UnsignedToSigned((value >> 12), 4);
        }

        private static int ToInt(byte value)
        {
            return value & 0xFF;
        }

        private static int UnsignedToSigned(int unsigned, int size)
        {
            int signed = (int)unsigned;
            if ((unsigned & (1 << size - 1)) != 0)
            {
                signed = (int)(-1 * ((1 << size - 1) - (unsigned & ((1 << size - 1) - 1))));
            }
            return signed;
        }

        #region Properties

        public int Mantissa { get; private set; }

        public int Exponent { get; private set; }

        #endregion

        #region Constants

        public const int NAN = 0x07FF;
        public const int POSITVE_INFINTY = 0x07FE;
        public const int NEGATIVE_INFINTY = 0x0802;
        public const int NRes = 0x0800;
        public const int Rfu = 0x0801;

        #endregion
    }
}
