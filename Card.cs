//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flashcards
{
    using System;
    using System.Collections.Generic;
    
    public partial class Card
    {
        public int Id { get; set; }
        public int Deck_Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Known { get; set; }
    
        public virtual Deck Deck { get; set; }
    }
}
