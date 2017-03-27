namespace CsVendingMachine.Types
{
    public class VendResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public Card Card { get; set; }
    }
}