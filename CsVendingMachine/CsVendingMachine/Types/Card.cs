using System;

namespace CsVendingMachine.Types
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CardType Type { get; set; }
        public string Pin { get; set; }
        public Account Account { get; set; }
    }
}