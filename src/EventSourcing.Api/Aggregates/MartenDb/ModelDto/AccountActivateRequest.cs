namespace EventSourcing.Api.Aggregates.MartenDb.ModelDto
{
    public class AccountActivateRequest
    {
        public string Owner { get; set; }

        public decimal Balance { get; set; } = 0;
        
        public string Description { get; set; }
    }
}
