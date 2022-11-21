namespace EventSourcing.Api.Common
{
    public static class Constants
    {
        public const string CustomEventSourcingDbScheme = "custom_event_sourcing";

        public const string DbConnection = "Postgre";

        //Create Account
        public const int OwnerNameMinLenght = 1;

        public const int OwnerNameMaxLenght = 256;

        public const int BalanceValueMinValue = 2;

        public const int DescriptionSymbolsMinLenght = 5;

        public const int DescriptionSymbolsMaxLenght = 256;


        //Account Creation - Error messages
        public const string OwnerNameRequiredErrorMessage = "Please enter an owner name";

        public const string BalanceValueRequiredErrorMessage = "Please enter a balance";

        public const string DescriptionSymbolsRequiredErrorMessage = "Please enter a description";

    }
}
