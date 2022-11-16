namespace EventSourcing.Api.Aggregates.ModelDto
{
    public abstract class AccountActivateRequest
    {
        public string Owner { get; set; }

        public decimal Balance { get; set; }
        
        public string Description { get; set; }
    }
}
