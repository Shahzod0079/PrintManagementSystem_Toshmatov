namespace Print_Management_System.Classes
{

    public class PrintOperation
    {

        public string TypeOperationText { get; set; }

        public string FormatText { get; set; }

        public string ColorText { get; set; }


        public int TypeOperationId { get; set; }

        public int FormatId { get; set; }

        public int SideCount { get; set; }

        public bool IsColor { get; set; }

        public bool Is50PercentOpacity { get; set; }

        public int PageCount { get; set; }


        public decimal Price { get; set; }


        /// <returns>Общая стоимость операции</returns>
        public decimal CalculateTotalPrice()
        {

            return Price;
        }

        /// <returns>Описание операции</returns>
        public string GetOperationDescription()
        {
            return $"{TypeOperationText} | {FormatText} | {ColorText} | {SideCount} сторона(ы)";
        }

        /// <returns>true если данные корректны</returns>
        public bool IsValid()
        {
            return TypeOperationId > 0 &&
                   FormatId > 0 &&
                   PageCount > 0 &&
                   Price >= 0;
        }
    }
}