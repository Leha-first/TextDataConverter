using System.Windows;
using Microsoft.Win32;

namespace TextDataConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для обработки нажатия на кнопку "Указать путь к исходному файлу"
        /// </summary>
        /// <param name="sender"> Button </param>
        /// <param name="e"> Данные о событии RoutedEventArgs </param>
        private async void ButtonBaseOnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {
                Filter = "Text|*.txt"
            };
            if (openFileDialog.ShowDialog() != true)
            {
                LabelForResult.Content = string.Empty;
                TextBlockWithFilePath.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(openFileDialog.FileName)) return;
            TextBlockWithFilePath.Text = openFileDialog.FileName;
            var fileProcessor = new FileProcessor(openFileDialog.FileName);
            var thrownException = await fileProcessor.StartProcessingSelectedFileAsync();

            LabelForResult.Content = thrownException == null ?
                "Операция конвертирования прошла успешно. Файл сохранен на Ваш Рабочий стол" :
                "Операция завешилась ошибкой -" + thrownException.Message;
        }
    }
}
