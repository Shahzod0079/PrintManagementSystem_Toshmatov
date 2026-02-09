using System;

namespace Print_Management_System.Classes
{
    public class JournalRecord
    {
        public string User { get; set; }               // Фамилия и инициалы
        public string OperationType { get; set; }      // Вид работы (Печать, Копия и т.д.)
        public string Format { get; set; }             // Формат (A4, A3, A2, A1)
        public int Side { get; set; }                  // Сторона (1 или 2)
        public string Color { get; set; }              // Цвет (Ч/Б или ЦВ)
        public int Count { get; set; }                 // Количество
        public decimal Price { get; set; }             // Сумма
        public string Signature { get; set; }          // Подпись
        public DateTime Date { get; set; }             // Дата операции

        public JournalRecord()
        {
            Date = DateTime.Now;
            Signature = "Подпись";
        }

        // Конструктор для создания из TypeOperationsWindow
        public JournalRecord(TypeOperationsWindow operation, string user)
        {
            User = user;
            OperationType = operation.typeOperationText;
            Format = operation.formatText;
            Side = operation.side;
            Color = operation.colorText;
            Count = operation.count;
            Price = (decimal)operation.price;
            Signature = "Подпись";
            Date = DateTime.Now;
        }
    }
}