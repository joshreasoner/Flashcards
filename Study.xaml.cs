using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flashcards
{
    /// <summary>
    /// Interaction logic for Study.xaml
    /// </summary>
    /// 

    
    public partial class Study : Window
    {
        FlashCardsEntities entity;
        DeckRepository deckRepository;
        int decknum;
        int cardnum=0;
        
        public Study(int deckid)
        {
            InitializeComponent();
            entity = new FlashCardsEntities();
            deckRepository = new DeckRepository();

            Deck deck = deckRepository.FindDeck(deckid);
            decknum = deck.Id;
            lblQuestion.Content = deck.Name;

            // Grab all the cards that belong to a specific deck
            List<Card> activecards = new List<Card>(deckRepository.GetDeckCards(decknum));


            // Grab the first card from the filtered set above

            Card activecard = activecards[cardnum];
            // Set the test question
            txtPrompt.Text = activecard.Question;

        }
        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            // TODO: Add logic to not be negative
            if (cardnum == 0) { BtnBack.IsEnabled = false; }
            else
            {
                BtnFwd.IsEnabled = true;
                BtnBack.IsEnabled = true;
                cardnum--;
                // Find the new active card based on the global card num index 
                List<Card> fwdcards = new List<Card>(deckRepository.GetDeckCards(decknum));
                Card activecard = fwdcards[cardnum];
                txtPrompt.Text = activecard.Question;
            }
        }

        private void BtnForwardClick(object sender, RoutedEventArgs e)
        {
            // Increment the global cardnum since we pressed the forward button 
            //TODO:  Add logic that you can't click forward longer than the length of the list - 1.
            List<Card> studycards = new List<Card>(deckRepository.GetDeckCards(decknum));
            
            if (cardnum == studycards.Count()-1) { BtnFwd.IsEnabled = false; }
            else
            {
                BtnFwd.IsEnabled = true;
                BtnBack.IsEnabled = true;
                cardnum++;
                // Find the new active card based on the global card num index 
                List<Card> fwdcards = new List<Card>(deckRepository.GetDeckCards(decknum));
                Card activecard = fwdcards[cardnum];
                txtPrompt.Text = activecard.Question;
            }
        }
        private void BtnHomeClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        private void BtnPromptClick(object sender, RoutedEventArgs e)
        {
            // Flip object from Q to A or A to Q on click
            List<Card> fwdcards = new List<Card>(deckRepository.GetDeckCards(decknum));
            Card activecard = fwdcards[cardnum];

            if (txtPrompt.Text == activecard.Question) { txtPrompt.Text = activecard.Answer; } // Q or A
            else { txtPrompt.Text = activecard.Question; }


        }
    }
}
