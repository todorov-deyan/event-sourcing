﻿namespace EventSourcing.Api.Aggregates.MartenDb.ModelDto
{
    public class AccountCreateRequest
    {
        public string Owner { get; set; }

        public decimal Balance { get; set; } = 0;
        
        public string Description { get; set; }
    }
}
