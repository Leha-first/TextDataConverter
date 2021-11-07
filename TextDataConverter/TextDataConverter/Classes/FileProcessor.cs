using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Collections.Generic;

namespace TextDataConverter
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public class FileProcessor
    {
        private readonly string _selectedFilePath;

        /// <summary>
        /// Переопределенный конструктор
        /// </summary>
        /// <param name="selectedFilePath"> Путь к выбранному файлу </param>
        public FileProcessor(string selectedFilePath)
        {
            _selectedFilePath = selectedFilePath;
        }
        /// <summary>
        /// Метод с основной логикой конвертации файла
        /// </summary>
        /// <returns> Возникшее исключение Exception </returns>
        public async Task<Exception> StartProcessingSelectedFileAsync()
        {
            try
            {
                string fileStringContent;
                await using (var fileStream = File.OpenRead(_selectedFilePath))
                {
                    var byteArray = new byte[fileStream.Length];
                    fileStream.Read(byteArray, 0, byteArray.Length);
                    fileStringContent = Encoding.Default.GetString(byteArray);
                }

                CreateListWithPersonBasedOnInputTextFile(out var listWithPersons, fileStringContent);
                GenerateJsonMarkup(out var outputJsonData, listWithPersons);
                //ПУТЬ К СОХРАНЯЕМОМУ ФАЙЛУ
                var pathSaveFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) +
                                   @"\data.json";
                using var jdoc = JsonDocument.Parse(outputJsonData);
                await using var fs = File.OpenWrite(pathSaveFile);
                await using var writer = new Utf8JsonWriter(fs,
                    new JsonWriterOptions
                    {
                        Indented = true,
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
                    });
                jdoc.WriteTo(writer);
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return new Exception("У Вас отсутсвуют права для сохранения на Ваш рабочий стол");
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// МЕТОД ГЕНЕРАЦИИ JSON-РАЗМЕТКИ
        /// </summary>
        /// <param name="outputJsonData"> СТРОКА С РАЗМЕТКОЙ </param>
        /// <param name="listWithPersons"> СПИСОК С ПЕРСОНАМИ </param>
        private void GenerateJsonMarkup(out string outputJsonData, List<Person> listWithPersons)
        {
            var currentStringBuilder = new StringBuilder();
            currentStringBuilder.Append("{\n\t\"contacts\": [\n\t\t");

            foreach (var personData in listWithPersons) {
                currentStringBuilder.Append("{\"id\": " + personData.Id + "," + "\"lastname\": \"" + personData.Surname + "\"," +
                                        "\"firstname\":\"" + personData.Name + "\"," + "\"middlename\":\"" + personData.Patronymic + "\"," +
                                        "\"phone\":" + personData.PhoneNumber + "},\n\t\t");
            }

            currentStringBuilder.Remove(currentStringBuilder.Length - 4, 4);
            currentStringBuilder.Append("\n], \"count\": " + listWithPersons.Count + "\n}");
            outputJsonData = currentStringBuilder.ToString();
        }
        /// <summary>
        /// Метод создания списка личностей из исходного файла
        /// </summary>
        /// <param name="listWithPersons"> Список с данными личностей </param>
        /// <param name="fileContent"> Строка - содержимое исходного файла </param>
        private void CreateListWithPersonBasedOnInputTextFile(out List<Person> listWithPersons, string fileContent)
        {
            listWithPersons = new List<Person>();
            var i = 1;
            foreach (var item in fileContent.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                var personDataInOneRow = item.Replace('\t', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
                //ПОЛУЧЕНИЕ СКОРРЕКТИРОВАННОГО СПИСКА СТРОК
                //ПЕРЕДАЧА В МЕТОД СПИСКА СТРОК, СОСТОЯЩИХ ТОЛЬКО ИЗ БУКВ
                var resultPersData =
                    ClassPersonValidator.ValidateListStringWithPersonalData(personDataInOneRow);
                //ПОИСК ЧИСЕЛ - НОМЕРА ТЕЛЕФОНА - СРЕДИ ЭЛЕМЕНТОВ МАССИВА
                var foundedPhoneNumber = personDataInOneRow.FindPhoneNumber();
                //ВАЛИДАЦИЯ НАЙДЕННОГО НОМЕРА НА ДЛИНУ
                var isSucessLengthValidate = ClassPersonValidator.CheckingLengthOfNumber(ref foundedPhoneNumber);
                //КОРРЕКТИРОВКА НОМЕРА
                if (isSucessLengthValidate)
                    foundedPhoneNumber = foundedPhoneNumber.CorrectionNumberPhone();

                listWithPersons.Add(new Person
                {
                    Id = i,
                    Surname = resultPersData[0],
                    Name = resultPersData[1],
                    Patronymic = resultPersData[2],
                    PhoneNumber = foundedPhoneNumber
                });
                i++;
            }
        }
    }
}
