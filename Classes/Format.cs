using System.Collections.Generic;

namespace Print_Management_System.Classes
{

    public class Format
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <param name="id">Код формата</param>
        /// <param name="name">Наименование формата</param>
        /// <param name="description">Описание формата</param>
        public Format(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    public static class FormatManager
    {

        /// <returns>Список форматов</returns>
        public static List<Format> GetAllFormats()
        {
            return new List<Format>
            {
                new Format(1, "A4", "Стандартный формат А4 (210×297 мм)"),
                new Format(2, "A3", "Формат А3 (297×420 мм)"),
                new Format(3, "A2", "Формат А2 (420×594 мм)"),
                new Format(4, "A1", "Формат А1 (594×841 мм)")
            };
        }

        /// <param name="id">Идентификатор формата</param>
        /// <returns>Найденный формат или null</returns>
        public static Format GetFormatById(int id)
        {
            var formats = GetAllFormats();
            return formats.Find(f => f.Id == id);
        }


        /// <param name="name">Наименование формата</param>
        /// <returns>Найденный формат или null</returns>
        public static Format GetFormatByName(string name)
        {
            var formats = GetAllFormats();
            return formats.Find(f => f.Name == name);
        }
    }
}