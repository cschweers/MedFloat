using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFloat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] value16 = new byte[] { 0xF6, 0xF4 };

            MedFloat16 sfloat16 = new MedFloat16(value16);
            Console.WriteLine($"Class test");
            Console.WriteLine($"Mantissa: {sfloat16.Mantissa}, Exponent: {sfloat16.Exponent}");
            Console.WriteLine($"Value: {sfloat16.ToFloat()}");

            Console.WriteLine($"Function test");
            Console.WriteLine($"Value: {MedFloat16.ToSFloat(value16)}");

            Console.WriteLine();
            byte[] value32 = new byte[] { 0x03, 0x0A, 0x00, 0xFE };

            MedFloat32 sfloat32 = new MedFloat32(value32);
            Console.WriteLine($"Class test");
            Console.WriteLine($"Mantissa: {sfloat32.Mantissa}, Exponent: {sfloat32.Exponent}");
            Console.WriteLine($"Value: {sfloat32.ToFloat()}");

            Console.WriteLine($"Function test");
            Console.WriteLine($"Value: {MedFloat32.ToSFloat(value32)}");

            return;
        }
    }
}
