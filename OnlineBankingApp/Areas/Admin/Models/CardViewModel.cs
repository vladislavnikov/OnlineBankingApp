namespace OnlineBankingApp.Areas.Admin.Models
{
    public class CardViewModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public double Balance { get; set; }

        public string UserId { get; set; }
    }
}
