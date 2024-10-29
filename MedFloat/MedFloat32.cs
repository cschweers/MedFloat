using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFloat
{

    public class MedFloat32
    {
        public MedFloat32(int mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;
        }

        public MedFloat32(byte[] value)
        {
            ParseData(value);
        }

        public MedFloat32(byte[] value, int index, int length = 4)
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
            MedFloat32 sfloat = new MedFloat32(value);
            return sfloat.ToFloat();
        }

        private void ParseData(byte[] value)
        {
            if (value.Length == 4)
            {
                UInt32 value32 = (UInt32)(value[0] + (value[1] << 8) + (value[2] << 16) + (value[3] << 24));
                ParseUInt32((UInt32)value32);
            }
            else
            {
                throw new ArgumentException("MedFloat32 => need array of 4 byte");
            }
        }

        private void ParseUInt32(UInt32 value)
        {
            Mantissa = UnsignedToSigned((int)(value & 0x00FFFFFF), 24);
            Exponent = UnsignedToSigned((int)(value >> 24), 8);
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

        public const int NAN = 0x007FFFFF;
        public const int POSITVE_INFINTY = 0x007FFFFE;
        public const int NEGATIVE_INFINTY = 0x00800002;
        public const int NRes = 0x00800000;
        public const int Rfu = 0x00800001;

        #endregion

    }
}
