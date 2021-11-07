using System.Collections.Generic;
using System.Linq;

namespace TextDataConverter
{
    /// <summary>
    /// Класс валидации личности
    /// </summary>
    public static class ClassPersonValidator
    {
        /// <summary>
        /// Метод для обработки строковых данных личности
        /// </summary>
        /// <param name="arrayPersonData"> Список с фамилией, именем и отчеством </param>
        /// <returns> Результирующий список с преобразованными данными </returns>
        public static List<string> ValidateListStringWithPersonalData(string[] arrayPersonData)
        {
            var outputListWithData = new List<string>();
            var listPersonData = arrayPersonData?.Where(persData => persData.StringIsWord())
                .ToList();
            switch (listPersonData?.Count)
            {
                //В ИСХОДНОЙ СТРОКЕ НЕ НАШЛОСЬ НИ ОДНОЙ СТРОКИ, СОСТОЯЩЕЙ ИЗ БУКВ
                case 0:
                    outputListWithData.Add("Отсутствует фамилия");
                    outputListWithData.Add("Отсутствует имя");
                    outputListWithData.Add("");
                    break;
                case 1:
                    var stringLength = listPersonData[0].Length;
                    if(stringLength < 2 || stringLength > 50) outputListWithData.Add("Длина фамилии вне диапазона от 2 до 50");
                    else outputListWithData.Add(listPersonData[0]);
                    outputListWithData.Add("Отсутствует имя");
                    outputListWithData.Add("");
                    break;
                case 2:
                    stringLength = listPersonData[0].Length;
                    if (stringLength < 2 || stringLength > 50)
                        outputListWithData.Add("Длина фамилии вне диапазона от 2 до 50");
                    else outputListWithData.Add(listPersonData[0]);
                    stringLength = listPersonData[1].Length;
                    if (stringLength < 2 || stringLength > 50) outputListWithData.Add("Длина имени вне диапазона от 2 до 50");
                    else outputListWithData.Add(listPersonData[1]);
                    outputListWithData.Add("");
                    break;
                case 3:
                    stringLength = listPersonData[0].Length;
                    if (stringLength < 2 || stringLength > 50) outputListWithData.Add("Длина фамилии вне диапазона от 2 до 50");
                    else outputListWithData.Add(listPersonData[0]);
                    stringLength = listPersonData[1].Length;
                    if (stringLength < 2 || stringLength > 50) outputListWithData.Add("Длина имени вне диапазона от 2 до 50");
                    else outputListWithData.Add(listPersonData[1]);
                    stringLength = listPersonData[2].Length;
                    if (stringLength < 2 || stringLength > 50) outputListWithData.Add("Длина отчества вне диапазона от 2 до 50");
                    else outputListWithData.Add(listPersonData[2]);
                    break;
                default:
                    break;
            }

            return outputListWithData;
        }
        /// <summary>
        /// Метод для проверки длины номера телефона
        /// </summary>
        /// <param name="curPhoneNumber"> Номер телефона </param>
        /// <returns> Логическое значение - соответсвует ли номер длине </returns>
        public static bool CheckingLengthOfNumber(ref string curPhoneNumber)
        {
            if (curPhoneNumber?.Length == 10 || curPhoneNumber?.Length == 11)
                return true;
            curPhoneNumber = "\"Номер вне диапазона 10-11 цифр\"";
            return false;
        }
    }
}
