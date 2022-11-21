namespace EventSourcing.Api.Aggregates.ModelDto
{
    public abstract class AccountActivateRequest
    {
        public decimal Balance { get; set; }
        
        public string Description { get; set; }
    }
}
