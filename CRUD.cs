using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    // CRUD.CS is a file to provide quick functionality for program to interact with database
    interface ICRUD
    {
        void AddDeck(Deck deck);
        void AddCard(Card deck);
        void DeleteCard(Card card);
        void DeleteDeck(Deck deck);
        void UpdateCard(int id, Card card);
        ICollection<Deck> GetAllDecks();
        IQueryable<Card> GetDeckCards(int idDeck);
        Card FindCard(int id);
        Deck FindDeck(int id);
    }
    class DeckRepository : ICRUD
    {
        private readonly FlashCardsEntities entities;

        public DeckRepository()
        {
            entities = new FlashCardsEntities();
        }
        public void AddCard(Card card)
        {
            // adds card to deck (cards table) on db
            entities.Cards.Add(card);
            entities.SaveChanges();
        }
        public void AddDeck(Deck deck)
        {
            // appends db deck table with new deck
            entities.Decks.Add(deck);
            entities.SaveChanges();
        }
        public void DeleteCard(Card card)
        {
            // removes single card from db
            entities.Cards.Remove(card);
            entities.SaveChanges();
        }
        public void DeleteDeck(Deck deck)
        {
            // removes specific deck and saves changes to db
            entities.Decks.Remove(deck);
            entities.SaveChanges();
        }
        public Card FindCard(int id)
        {
            // returns specific card
            return entities.Cards.Find(id);
        }
        public Deck FindDeck(int id)
        {
            // returns specific deck
            return entities.Decks.Find(id);
        }
        public ICollection<Deck> GetAllDecks()
        {
            // Returns all decks in db
            return entities.Decks.ToList();
        }
        public IQueryable<Card> GetDeckCards(int idDeck)
        {
            // LINQ of db to get all cards matching specified deck number
            return from Card card in entities.Cards
                   where card.Deck_Id == idDeck
                   select card;
        }
        public void UpdateCard(int id, Card card)
        {
            // queries db based on card id and overwrites corresponding card
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
