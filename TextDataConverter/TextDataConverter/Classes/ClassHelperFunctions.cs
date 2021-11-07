using System;
using System.Linq;

namespace TextDataConverter
{
    /// <summary>
    /// Статический класс, содержащий методы расширения
    /// </summary>
    public static class ClassHelperFunctions
    {
        /// <summary>
        /// Метод для возврата числовых символов из строки
        /// </summary>
        /// <param name="inCorrectStringPhoneNumber"> Входная строка для корректировки </param>
        /// <returns> Строка, содержащая только числа </returns>
        public static string CorrectionNumberPhone(this string inCorrectStringPhoneNumber)
        {
            if (string.IsNullOrEmpty(inCorrectStringPhoneNumber)) return string.Empty;
            //НАЧАЛО ЛЮБОГО НОМЕРА
            string firstSymbolPhoneNumber = "7";
            //ВЫБОРКА ЧИСЛОВЫХ СИМВОЛОВ
            var digitStr = inCorrectStringPhoneNumber.Where(char.IsDigit)
                .Aggregate("", (current, charSymbol) => current + charSymbol);
            //ПРОВЕРКА ПЕРВОГО СИМВОЛА В НОМЕРЕ
            switch (digitStr[0])
            {
                case '7':
                    break;
                case '8':
                    digitStr = digitStr.Remove(0, 1).Insert(0, firstSymbolPhoneNumber);
                    break;
                default:
                    digitStr = digitStr.Insert(0, firstSymbolPhoneNumber);
                    break;
            }
            return digitStr;
        }

        /// <summary>
        /// Метод для проверки, имеет ли строка только буквенное содержание (Letter)
        /// </summary>
        /// <param name="stringForChecking"> Входная строка для проверки </param>
        /// <returns> Логическое значение </returns>
        public static bool StringIsWord(this string stringForChecking)
        {
            if (string.IsNullOrEmpty(stringForChecking)) return false;
            return stringForChecking.Length == stringForChecking.Count(char.IsLetter);
        }
            

        /// <summary>
        /// ПОИСК СРЕДИ МАССИВА СТРОК ЭЛЕМЕНТОВ СОДЕРЖАЩИХ ЧИСЛА - СОСТАВНЫЕ ЧАСТИ НОМЕРА ТЕЛЕФОНА
        /// </summary>
        /// <param name="inputStringArray"> ВХОДНОЙ МАССИВ СТРОК </param>
        public static string FindPhoneNumber(this string[] inputStringArray)
        {
            if(inputStringArray == null || inputStringArray.Length == 0) return string.Empty;
            return inputStringArray.Aggregate(string.Empty,
                (current1, stringItem) => stringItem?.Where(char.IsDigit)
                    .Aggregate(current1, (current, symbol) => current + symbol));
        } 
    }
}
