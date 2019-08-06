using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Project_2__WPF_
{
    /// <summary>
    /// this class use radian
    /// </summary>
    static class MyMath
    {
        public static double ToDegrees(double a)
        {
            return a * 180 / Math.PI;
        }

        public static double ToRadian(double a)
        {
            return a * Math.PI / 180;
        }

        public static double Sin(double a, bool isRadians)
        {
            if (isRadians == false)
                a = ToRadian(a);

            a = CorrectInputForSinTanCos(a);
            if (EqualAny(a, new double[] { -Math.PI * 2, -Math.PI, 0, Math.PI, Math.PI * 2 }))
                return 0;

            return Math.Sin(a);
        }

        public static double Cos(double a, bool isRadians)
        {
            if (isRadians == false)
                a = ToRadian(a);

            a = CorrectInputForSinTanCos(a);
            if (EqualAny(a, new double[] { -Math.PI * 3 / 2, -Math.PI / 2, Math.PI / 2, Math.PI * 3 / 2 }))
                return 0;

            return Math.Cos(a);
        }

        public static double Tan(double a, bool isRadians)
        {
            if (isRadians == false)
                a = ToRadian(a);

            a = CorrectInputForSinTanCos(a);
            if (EqualAny(a, new double[] { -Math.PI * 2, -Math.PI, 0, Math.PI, Math.PI * 2 }))
                return 0;

            return Math.Tan(a);
        }

        public static double Asin(double a, bool isRadians)
        {
            a = Math.Asin(a);

            if (isRadians == false)
                a = ToDegrees(a);

            return a;
        }

        public static double Acos(double a, bool isRadians)
        {
            a = Math.Acos(a);

            if (isRadians == false)
                a = ToDegrees(a);

            return a;
        }

        public static double Atan(double a, bool isRadians)
        {
            a = Math.Atan(a);

            if (isRadians == false)
                a = ToDegrees(a);

            return a;
        }

        public static double Ln(double a)
        {
            return Math.Log(a);
        }

        public static double Log10(double a)
        {
            return Math.Log10(a);
        }

        public static double Sqrt(double a)
        {
            return Math.Sqrt(a);
        }

        public static double Pow(double a, double v)
        {
            return Math.Pow(a, v);
        }

        public static double Factorial(int a)
        {
            if (a < 0)
                throw new FactorialNegativeNumberException();
            int result = 1;

            for (int i = 1; i <= a; i++)
                result *= i;

            return result;
        }

        private static double CorrectInputForSinTanCos(double a)
        {
            while (a > Math.PI * 2)
                a -= Math.PI * 2;
            while (a < -Math.PI * 2)
                a += Math.PI * 2;
            return a;
        }

        public static bool EqualAny(double a, double[] array)
        {
            foreach (double number in array)
            {
                if (Equal(a, number) == true)
                    return true;
            }
            return false;
        }

        public static bool Equal(double a, double v)
        {
            if (Math.Abs(a - v) > Math.Pow(10, -10))
                return false;
            return true;
        }
    }
}
