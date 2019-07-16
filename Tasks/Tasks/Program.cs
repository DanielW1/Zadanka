using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteLine(task4());
            //WriteLine(task5());
            // WriteLine(task6());
            //WriteLine(task7());
            //WriteLine(task8());
            //WriteLine(task11());
            //WriteLine(task12());
            Read();
        }

        /*
        Zaprezentowana wersja rozwiązania wykorzystuje możliwości języka C#. Nie wykorzystałem wgl parametru wskazujacego na liczbę wprowadzanych
        wartości, gdyż nie był mi potrzebny. Jeżeli będzie taka potrzeba mogę oczywiście również rozwiązać zadanie z wykorzystaniem bardziej podstawowych
        funkcji języka.
        */
        private static string task4()
        {
            Int32.TryParse(ReadLine(), out int arraySize);
            var items = ReadLine();

            var arrayItems = items.Split(new char[] { ' ' });
            List<string> list = new List<string>(arrayItems);
            list.Reverse();
            var result = String.Join(' ', list.ToArray());
            WriteLine(result);
            return result; 

        }

        private static string task5()
        {
            string input = ReadLine();
            var charArray = input.ToCharArray();

            int arrayLength = charArray.Length;
            for(int i = 0; i<((int)(arrayLength>>1)); i++)
            {
                if (charArray[i] != charArray[arrayLength - 1 - i]) return "NO";
            }
            return "YES";
        }

        private static string task6()
        {
            var items1 = ReadLine();
            var items2 = ReadLine();

            var itemsList1 = items1.Split(new char[] { ' ' }).ToList();
            var itemsList2 = items2.Split(new char[] { ' ' }).ToList();

            foreach(var item in itemsList1)
            {
                itemsList2.Remove(item);
            }

            return itemsList2.Count == 0? "YES": "NO";
        }

        private static string task7()
        {
            string input;
            List<string> list = new List<string>();
            //Wczytywanie liczb do momentu wprowadzenia entera
            do
            {
                input = ReadLine();
                list.Add(input);

            } while (input != "");
            list.RemoveAt(list.Count - 1);
            List<string> binaryRepresentations = new List<string>();
            List<int> powerOf2 = new List<int>();
            foreach(var item in list)
            {
                //Zapisanie liczby w ciągu binarnym skonwertowane do stringa, a nastepnie do tablicy char
                var binaryString = Convert.ToString(Int32.Parse(item), 2).ToCharArray();
                for(int i = binaryString.Length-1; i>=0; i--)
                {
                    if(binaryString[i] == '1')
                    {
                        //Jeżeli mamy '1' to możemy za pomocą indeksu obliczyć odpowiednią potęgę 2
                        var power =(int)Math.Pow(2, binaryString.Length-1-i);
                        if (!powerOf2.Contains(power))
                        {
                            powerOf2.Add(power);
                        }
                    }
                    
                }


            }
            powerOf2.Sort((x, y) => x - y);
            return String.Join(", ", powerOf2.Select(x => x.ToString()).ToArray());
        }

        private static string task8()
        {
            Int16.TryParse(ReadLine(), out short testCount);
            string[] tests = new string[testCount];

            for (var i = 0; i<testCount; i++)
            {
                tests[i] = ReadLine();
            }

            StringBuilder result = new StringBuilder("");

            foreach(var elem in tests)
            {
               var testItems = elem.Split(new char[] { ' ' });
                var lastElem = int.Parse(testItems[1]);

                int primeCount = 0;
                //Aby zrównoleglić proces wykorzystano bibliotekę TPL
                Parallel.For(int.Parse(testItems[0]), lastElem, i => {
                    if (isPrime(i)) primeCount++;
                });
                result.AppendLine(primeCount.ToString());

            }

            return result.ToString();


        }
        /*
         * Algorytm sprawdzający, czy jest to liczba pierwsza
         * */
        private static bool isPrime(int number)
        {
            if(number <= 1) return false;

            if (number <= 3) return true;

            if (number % 2 == 0 || number % 3 == 0) return false;

            for (var i = 5; i * i <= number; i = i + 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;

        }

        private static int task11()
        {
            Int32.TryParse(ReadLine(), out int n);
            var numbers = ReadLine();
            var charArray = numbers.Replace(" ", "").ToCharArray();
            List<char> list = new List<char>(charArray);
            //Wyszukujemy cyfrę najczęściej występującą poprzez pogrupowanie rekordów i wyciągnięcie tego o największej ilości powtórzeń
            var result = list.GroupBy(e => e)
                .Select(group => new { key = group.Key, count = group.Count() }) .OrderBy(x => x.count).ThenBy(x=> x.key).Last();

            return Int32.Parse(result?.key.ToString());
        }

        private static int task12()
        {
           Int32.TryParse(ReadLine(), out int n);
            var numbers = ReadLine();
            var arrayNumber = numbers.Split(new char[] { ' ' });
            List<string> stringNumberList = new List<string>(arrayNumber);
            //Wartości w liście to liczby zapisane jako string. W liczbach tych rozdzielamy znaki i sumujemy wartości konwertowane do typy Int32
            var result = stringNumberList.Select(x => new List<char>(x.ToCharArray()).Select(y => Int32.Parse(y.ToString())).Sum()).ToList();
            var maxSumDigit = result.Max();
            //Jeżeli jest tylko jedna liczba spełniająca warunek to zwracamy jej index, jeżlei nie nie sprawdzamy która z pozostałych liczb jest największa
            if( result.Where(x => x == maxSumDigit).Count() == 1)
            {
                return result.IndexOf(maxSumDigit);
            }
            else
            {
                List<int> indexes = new List<int>();
                for(int i=0; i<result.Count; i++)
                {
                    //zbieramy indexy liczb o maxymalnej sumie cyfr
                    if (result.ElementAt(i) == maxSumDigit)
                    {
                        indexes.Add(i);
                    }
                }

                int maxValue=0, maxIndex = indexes.ElementAt(0);
                foreach(var index in indexes)
                {
                    if(Int32.Parse(stringNumberList.ElementAt(index)) > maxValue)
                    {
                        maxValue = Int32.Parse(stringNumberList.ElementAt(index));
                        maxIndex = index;
                    }
                    
                }
                return maxIndex;
                
            }

        }

    }
}
