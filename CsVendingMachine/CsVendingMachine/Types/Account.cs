using System;
using System.Collections.Generic;

namespace CsVendingMachine.Types
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<Card> Cards { get; set; }
    }
}