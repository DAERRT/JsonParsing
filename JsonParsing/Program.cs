namespace JsonParsing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] coefficients_task1 = new double[,]
                {
                    { 0.15, 0.05, -0.08, 0.14},
                    { 0.32, -0.13, -0.12, 0.11},
                    { 0.17, 0.06, -0.08, 0.12},
                    { 0.21, -0.16, 0.36, 0}
                };
            double[] free_coefficients_task1 = { -0.48, 1.24, 1.15, -0.88 };

            double[,] coefficients_task2 = new double[,]
    {
                    { 0.45, 0.08, -0.42},
                    { -0.08, 0.65, 0.14},
                    { -0.15, 0.23, 0.14},
    };
            double[] free_coefficients_task2 = { -0.03, -0.32, 0.55 };
            double[] free_coefficients_task2_1 = { -0.03, -0.32, 0.55 };
            Console.WriteLine("----------------Задание 1-----------------");
            Console.WriteLine("Метод Якоби");
            Console.WriteLine(Sxodimost(coefficients_task1));
            var a = Jakobi(coefficients_task1, free_coefficients_task1);
            Console.WriteLine("Колличество итераций = " + a[0]);
            for (int i = 1; i < a.Length - 1; i++)
            {
                Console.WriteLine($"x{i} = {a[i]:F6}");
            }
            ;
            Console.WriteLine($"Погрешность = {a[a.Length - 1]:F6}");
            Console.WriteLine("----------------Задание 2-----------------");
            Console.WriteLine("Метод Якоби");
            Console.WriteLine(Sxodimost(coefficients_task2));
            var a1 = Jakobi(coefficients_task2, free_coefficients_task2);
            Console.WriteLine("Колличество итераций = " + a1[0]);
            for (int i = 1; i < a1.Length - 1; i++)
            {
                Console.WriteLine($"x{i} = {a1[i]:F6}");
            }
            ;
            Console.WriteLine($"Погрешность = {a1[a1.Length - 1]:F6}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Метод Зейделя");
            var a2 = Zeidel(coefficients_task2, free_coefficients_task2_1); ;
            Console.WriteLine("Колличество итераций = " + a2[0]);
            for (int i = 1; i < a2.Length - 1; i++)
            {
                Console.WriteLine($"x{i} = {a2[i]:F6}");
            }
            ;
            Console.WriteLine($"Погрешность = {a2[a2.Length - 1]:F6}");
        }

        static string Sxodimost(double[,] coefficients)
        {
            double sxodimost = 0;

            for (int i = 0; i < coefficients.GetLength(0); i++)
            {
                double a = 0;
                for (int j = 0; j < coefficients.GetLength(1); j++)
                {
                    a += Math.Abs(coefficients[i, j]);
                }
                if (a > sxodimost)
                {
                    sxodimost = a;
                }
            }
            if (sxodimost <= 1)
            {
                return $"Сходимость = {sxodimost} < 1.сходиться.";
            }
            else
            {
                return $"Сходимость = {sxodimost} > 1. не сходиться.";
            }
        }

        static double[] Jakobi(double[,] coefficients, double[] free_coefficients)
        {
            //Свободные члены в качестве нулевого приблежения
            double[] past_iteration = free_coefficients;
            double[] real_iteration = new double[free_coefficients.Length];
            double error_pland = 0.001;
            double real_error = error_pland + 1;
            int iter_counter = 1;
            for (int g = 1; g < 101; g++)
            {
                if (!(real_error <= error_pland))
                {
                    for (int i = 0; i < coefficients.GetLength(0); i++)
                    {
                        double sum = 0;
                        for (int j = 0; j < coefficients.GetLength(1); j++)
                        {
                            sum += coefficients[i, j] * past_iteration[j];
                        }
                        real_iteration[i] = Math.Round(sum, 6);
                    }
                    for (int i = 0; i < past_iteration.Length; i++)
                    {
                        past_iteration[i] = Math.Abs(real_iteration[i] - past_iteration[i]);
                    }
                    real_error = past_iteration.Max();
                    Array.Copy(real_iteration, past_iteration, past_iteration.Length);
                    iter_counter = g;
                }
                else break;
            }
            double[] result = new double[real_iteration.Length + 2];
            result[0] = iter_counter;
            int c = 0;
            for (int i = 1; i <= real_iteration.Length; i++)
            {
                result[i] = real_iteration[c];
                c++;
            }
            result[result.Length - 1] = real_error;
            return result;

        }

        static double[] Zeidel(double[,] coefficients, double[] free_coefficients)
        {
            //Свободные члены в качестве нулевого приблежения
            double[] past_iteration = free_coefficients;
            double[] real_iteration = new double[free_coefficients.Length];
            double error_pland = 0.001;
            double real_error = error_pland + 1;
            int iter_counter = 1;
            for (int g = 1; g < 101; g++)
            {
                if (!(real_error <= error_pland))
                {
                    for (int i = 0; i < coefficients.GetLength(0); i++)
                    {
                        double sum = 0;
                        for (int j = 0; j < coefficients.GetLength(1); j++)
                        {
                            if (i == 0)
                            {
                                sum += coefficients[i, j] * past_iteration[j];
                            }
                            else if (i == 1)
                            {
                                if (j == 0)
                                {
                                    sum += coefficients[i, j] * real_iteration[j];
                                }
                                else
                                {
                                    sum += coefficients[i, j] * past_iteration[j];
                                }

                            }
                            else
                            {
                                if (j == 0 | j == 1)
                                {
                                    sum += coefficients[i, j] * real_iteration[j];
                                }
                                else
                                {
                                    sum += coefficients[i, j] * past_iteration[j];
                                }
                            }

                        }
                        real_iteration[i] = Math.Round(sum, 6);
                    }
                    for (int i = 0; i < past_iteration.Length; i++)
                    {
                        past_iteration[i] = Math.Abs(real_iteration[i] - past_iteration[i]);
                    }
                    real_error = past_iteration.Max();
                    Array.Copy(real_iteration, past_iteration, past_iteration.Length);
                    iter_counter = g;
                }
                else break;
            }
            double[] result = new double[real_iteration.Length + 2];
            result[0] = iter_counter;
            int c = 0;
            for (int i = 1; i <= real_iteration.Length; i++)
            {
                result[i] = real_iteration[c];
                c++;
            }
            result[result.Length - 1] = real_error;
            return result;
        }
    }
}
