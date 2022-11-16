namespace EventSourcing.Api.Aggregates.ModelDto
{
    public abstract class AccountDeactivateRequest
    {
        public string Owner { get; set; }

        public decimal Balance { get; set; } = 0;
        
        public string Description { get; set; }
    }
}
