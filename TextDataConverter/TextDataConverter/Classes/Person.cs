namespace TextDataConverter
{
    /// <summary>
    /// Класс личности
    /// </summary>
    internal class Person
    {
        /// <summary>
        /// Чиловой идентификатор
        /// </summary>
        internal int Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        internal string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        internal string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        internal string Patronymic { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        internal string PhoneNumber { get; set; }
    }
}
