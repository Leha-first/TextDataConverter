using Xunit;
using TextDataConverter;
using System.Collections.Generic;

namespace TextDataConverterTests
{
    /// <summary>
    /// Класс тестрования функциональности TextDataConverter
    /// </summary>
    public class TextDataConverterTestClass
    {
        [Theory]
        [InlineData(new []{"sdf","dsfsd", "435", "34234 656,2","dfsdf345"}, "435342346562345")]
        [InlineData(new[] { "", null, "+7(800)678-44-55" }, "78006784455")]
        [InlineData(null, "")]
        public void ClassHelperFunctionsFindPhoneNumberTest(string[] inputStringArray, string expected)
        {
            //РЕЗУЛЬТАТА ПРЕОБРАЗОВАНИЯ МЕТОДА FindPhoneNumber
            var actualResult = inputStringArray.FindPhoneNumber();
            //ПРОВЕРКА РЕЗУЛЬТАТА
            Assert.Equal(expected, actualResult);
        }
        
        [Theory]
        [InlineData("8967902310", "7967902310")]
        [InlineData("8-985-001-02-03", "79850010203")]
        [InlineData("+7(985) 450-12-34", "79854501234")]
        [InlineData("9031225060", "79031225060")]
        [InlineData("8 920 122 50 43", "79201225043")]
        [InlineData("(925)143-23-15", "79251432315")]
        [InlineData("16867", "716867")]
        [InlineData(null , "")]
        public void ClassHelperFunctionsCorrectionNumberPhoneTest(string inputPhoneNumber, string expected)
        {
            //РЕЗУЛЬТАТА ПРЕОБРАЗОВАНИЯ МЕТОДА CorrectionNumberPhone
            var actualResult = inputPhoneNumber.CorrectionNumberPhone();
            //ПРОВЕРКА РЕЗУЛЬТАТА
            Assert.Equal(expected, actualResult);
        }

        [Theory]
        [InlineData("435342346562345", false)]
        [InlineData("new90Word", false)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void ClassHelperFunctionsStringIsWordTest(string inputStringValue, bool expected)
        {
            //РЕЗУЛЬТАТА ПРЕОБРАЗОВАНИЯ МЕТОДА StringIsWord
            var actualResult = inputStringValue.StringIsWord();
            //ПРОВЕРКА РЕЗУЛЬТАТА
            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public void ClassPersonValidatorValidateListStringWithPersonalDataTest()
        {
            var listPersonData = new [] {"Егоров", "Николай", "fsdf334", "", null, "23432423"};
            //РЕЗУЛЬТАТА ПРЕОБРАЗОВАНИЯ МЕТОДА ValidateListStringWithPersonalData
            var actualResult = ClassPersonValidator.ValidateListStringWithPersonalData(listPersonData);
            var expected = new List<string> { "Егоров", "Николай", "" };
            //ПРОВЕРКА РЕЗУЛЬТАТА
            Assert.Equal(expected, actualResult);
        }

        [Theory]
        [InlineData("546456456", false)]
        [InlineData("546454344343326456", false)]
        [InlineData("89605889321", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void ClassPersonValidatorCheckingLengthOfNumberTest(string inputNumberPhone, bool expected)
        {
            //РЕЗУЛЬТАТА ПРЕОБРАЗОВАНИЯ МЕТОДА CheckingLengthOfNumber
            var actualResult = ClassPersonValidator.CheckingLengthOfNumber(ref inputNumberPhone);
            //ПРОВЕРКА РЕЗУЛЬТАТА
            Assert.Equal(expected, actualResult);
        }
    }
}
