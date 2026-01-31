using System.Collections.Generic;

namespace Print_Management_System.Classes
{

    public class TypeOperation
    {

        public int Id { get; set; }


        public string Name { get; set; }

 
        public string Description { get; set; }


        /// <param name="id">Идентификатор типа операции</param>
        /// <param name="name">Наименование типа операции</param>
        /// <param name="description">Описание типа операции</param>
        public TypeOperation(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    public static class TypeOperationManager
    {
        /// <returns>Список типов операций</returns>
        public static List<TypeOperation> GetAllTypeOperations()
        {
            return new List<TypeOperation>
            {
                new TypeOperation(1, "Печать", "Печать документа на принтере"),
                new TypeOperation(2, "Копия", "Копирование документа на копировальном аппарате"),
                new TypeOperation(3, "Сканирование", "Сканирование документа в цифровой формат"),
                new TypeOperation(4, "Ризограф", "Тиражирование на ризографе")
            };
        }

        /// <param name="id">Идентификатор типа операции</param>
        /// <returns>Найденный тип операции или null</returns>
        public static TypeOperation GetTypeOperationById(int id)
        {
            var operations = GetAllTypeOperations();
            return operations.Find(o => o.Id == id);
        }

        /// <param name="name">Наименование типа операции</param>
        /// <returns>Найденный тип операции или null</returns>
        public static TypeOperation GetTypeOperationByName(string name)
        {
            var operations = GetAllTypeOperations();
            return operations.Find(o => o.Name == name);
        }

        /// <param name="id">Идентификатор типа операции</param>
        /// <returns>true если тип операции существует</returns>
        public static bool TypeOperationExists(int id)
        {
            return GetTypeOperationById(id) != null;
        }
    }
}