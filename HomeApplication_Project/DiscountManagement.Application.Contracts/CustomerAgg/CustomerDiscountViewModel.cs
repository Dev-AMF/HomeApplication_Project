namespace DiscountManagement.Application.Contracts.CustomerAgg
{
    public class CustomerDiscountViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
    }

}
