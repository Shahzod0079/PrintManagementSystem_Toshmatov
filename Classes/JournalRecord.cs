using System;

namespace Print_Management_System.Classes
{
    public class JournalRecord
    {
        public string User { get; set; }               
        public string OperationType { get; set; }    
        public string Format { get; set; }        
        public int Side { get; set; }                  
        public string Color { get; set; }              
        public int Count { get; set; }                 
        public decimal Price { get; set; }             
        public string Signature { get; set; }         
        public DateTime Date { get; set; }           

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