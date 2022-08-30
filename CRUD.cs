using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    
    interface ICRUD
    {
        void AddDeck(Deck deck);
        void AddCard(Card deck);
        void DeleteCard(Card card);
        void UpdateCard(int id, Card card);
        ICollection<Card> GetAllCards();
        ICollection<Deck> GetAllDecks();
        IQueryable<Card> GetDeckCards(int idDeck);
        Card FindCard(int id);
        Deck FindDeck(int id);
        int GetMinId();

    }
    class DeckRepository : ICRUD
    {
        FlashCardEntities entities;


        public DeckRepository()
        {
            entities = new FlashCardEntities();
        }

        public void AddCard(Card card)
        {
            entities.Cards.Add(card);
            entities.SaveChanges();
        }

        public void AddDeck(Deck deck)
        {
            entities.Decks.Add(deck);
            entities.SaveChanges();
        }

        public void DeleteCard(Card card)
        {
            entities.Cards.Remove(card);
            entities.SaveChanges();
        }

        public Card FindCard(int id)
        {
            return entities.Cards.Find(id);
        }

        public Deck FindDeck(int id)
        {
            return entities.Decks.Find(id);
        }

        public ICollection<Card> GetAllCards()
        {
            return entities.Cards.ToList();
        }

        public ICollection<Deck> GetAllDecks()
        {
            return entities.Decks.ToList();
        }

        public IQueryable<Card> GetDeckCards(int idDeck)
        {
            return from Card card in entities.Cards
                   where card.Deck_Id == idDeck
                   select card;
        }

        public int GetMinId()
        {
            return entities.Decks.Min(v => v.Id);
        }

        public void UpdateCard(int id, Card card)
        {
            var cardtoupdate = entities.Cards.Find(id);
            cardtoupdate.Id = card.Id;
            cardtoupdate.Deck_Id = card.Deck_Id;
            cardtoupdate.Question = card.Question;
            cardtoupdate.Answer = card.Answer;
            cardtoupdate.Known = card.Known;
            entities.SaveChanges();
        }
    }
}
