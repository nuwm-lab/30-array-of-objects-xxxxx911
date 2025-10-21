// ...existing code...
using System;
using System.Globalization;

namespace LabWork
{
    
    /// <summary>
    /// Клас, що представляє пряму ax + by + c = 0
    /// Інкапсуляція полів a, b, c через властивості тільки для читання.
    /// Містить методи для перевірки належності точки прямій.
    /// </summary>
    class Line
    {
        // приватні поля
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        // Публічні лише для читання властивості
        public double A => _a;
        public double B => _b;
        public double C => _c;

        // Конструктор
        public Line(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        // Перевірка чи належить точка (x, y) цій прямій з допустимою похибкою eps
        public bool ContainsPoint(double x, double y, double eps = 1e-9)
        {
            return Math.Abs(_a * x + _b * y + _c) <= eps;
        }

        // Зручний рядковий опис прямої
        public override string ToString()
        {
            return $"{_a}x + {_b}y + {_c} = 0";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Короткі інструкції для користувача (українською)
            Console.WriteLine("Варіант 13. Масив прямих виду ax + by + c = 0.");
            Console.Write("Введіть кількість прямих n: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Невірне значення n. Завершення програми.");
                return;
            }

            var lines = new Line[n];
            var culture = CultureInfo.CurrentCulture;

            // Введення коефіцієнтів прямих
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Пряма #{i + 1}: введіть a, b, c через пробіл (наприклад: 1 2 -3)");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Порожній ввід. Спроба знову.");
                    i--;
                    continue;
                }

                string[] parts = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3
                    || !double.TryParse(parts[0], NumberStyles.Float, culture, out double a)
                    || !double.TryParse(parts[1], NumberStyles.Float, culture, out double b)
                    || !double.TryParse(parts[2], NumberStyles.Float, culture, out double c))
                {
                    Console.WriteLine("Невірний формат. Спроба знову.");
                    i--;
                    continue;
                }

                lines[i] = new Line(a, b, c);
            }

            // Введення двох точок
            Console.WriteLine("Введіть першу точку x1 y1 через пробіл:");
            if (!TryReadPoint(out double x1, out double y1))
            {
                Console.WriteLine("Невірний ввід точки. Завершення програми.");
                return;
            }

            Console.WriteLine("Введіть другу точку x2 y2 через пробіл:");
            if (!TryReadPoint(out double x2, out double y2))
            {
                Console.WriteLine("Невірний ввід точки. Завершення програми.");
                return;
            }

            // Для кожної прямої визначити, чи належить хоча б одна із точок
            Console.WriteLine("\nРезультати перевірки належності точок прямим:");
            for (int i = 0; i < n; i++)
            {
                var line = lines[i];
                bool contains1 = line.ContainsPoint(x1, y1);
                bool contains2 = line.ContainsPoint(x2, y2);

                if (contains1 && contains2)
                {
                    Console.WriteLine($"Пряма #{i + 1} ({line}) проходить через обидві точки.");
                }
                else if (contains1)
                {
                    Console.WriteLine($"Пряма #{i + 1} ({line}) містить першу точку ({x1}, {y1}).");
                }
                else if (contains2)
                {
                    Console.WriteLine($"Пряма #{i + 1} ({line}) містить другу точку ({x2}, {y2}).");
                }
                else
                {
                    Console.WriteLine($"Пряма #{i + 1} ({line}) не містить жодної з точок.");
                }
            }

            // Знайти прямі, які проходять через обидві точки
            Console.WriteLine("\nПрямі, що проходять через обидві точки:");
            bool foundAny = false;
            for (int i = 0; i < n; i++)
            {
                if (lines[i].ContainsPoint(x1, y1) && lines[i].ContainsPoint(x2, y2))
                {
                    Console.WriteLine($"Пряма #{i + 1}: {lines[i]}");
                    foundAny = true;
                }
            }

            if (!foundAny)
            {
                Console.WriteLine("Немає прямої з масиву, що проходить через обидві точки.");
            }
        }

        // Допоміжний метод для читання точки з консолі (декілька спроб)
        private static bool TryReadPoint(out double x, out double y)
        {
            var culture = CultureInfo.CurrentCulture;
            x = y = 0;
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            string[] parts = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2
                || !double.TryParse(parts[0], NumberStyles.Float, culture, out x)
                || !double.TryParse(parts[1], NumberStyles.Float, culture, out y))
            {
                return false;
            }

            return true;
        }
    }
}
// ...existing code...
