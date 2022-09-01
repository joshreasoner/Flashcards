using System;
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
using System.Windows.Shapes;

namespace Flashcards
{
    /// <summary>
    /// Interaction logic for Modify.xaml
    /// </summary>
    public partial class Modify : Window
    {
        FlashCardsEntities entity;
        DeckRepository deckRepository;

        int decknum;
        int cardnum;
        public Modify(int deckid)
        {
            InitializeComponent();
            deckRepository = new DeckRepository();
            entity = new FlashCardsEntities();
            Deck titledeck = deckRepository.FindDeck(deckid);
            lblTitle.Content = titledeck.Name;
            decknum = deckid;
            RefreshGrid();
            lblEnterTitle.Visibility = Visibility.Hidden;
            txtDeckTitle.Visibility = Visibility.Hidden;
            BtnSubmit.Visibility = Visibility.Hidden;
        }
        private void RefreshGrid()
        {
            List<Card> carddata = new List<Card>(deckRepository.GetDeckCards(decknum));
            gridCards.ItemsSource = null;
            gridCards.ItemsSource = carddata;
        }
        private void BtnHomeClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }
        private void BtnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (txtPrompt.Text != String.Empty && txtAnswer.Text != String.Empty)
            {
                Card updatecard = deckRepository.FindCard(cardnum);
                updatecard.Question = txtPrompt.Text;
                updatecard.Answer = txtAnswer.Text;
                // known - t/f               
                deckRepository.UpdateCard(cardnum, updatecard);
               /* if (checkRead.IsChecked == true) { bookupdate.Read = true; }
                else { bookupdate.Read = false; };*/
                MessageBox.Show($"Card details updated!");
                RefreshGrid();
                txtAnswer.Clear();
                txtPrompt.Clear();
                gridCards.CurrentCell.Equals(1);
            }
        }
        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            Card deletecard = deckRepository.FindCard(cardnum);

            if (deletecard != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure you want to delete {deletecard.Question}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    deckRepository.DeleteCard(deletecard);
                    RefreshGrid();
                    txtAnswer.Clear();
                    txtPrompt.Clear();
                }
            }
        }
        private void BtnNewDeckClick(object sender, RoutedEventArgs e)
        {
            // disable everything but Home
            txtAnswer.Visibility = Visibility.Hidden;
            lblAnswer.Visibility = Visibility.Hidden;
            txtPrompt.Visibility = Visibility.Hidden;
            lblPrompt.Visibility = Visibility.Hidden;
            gridCards.Visibility = Visibility.Hidden;
            lblTitle.Visibility = Visibility.Hidden;
            // enable section to enter new deck title
            lblEnterTitle.Visibility = Visibility.Visible;
            txtDeckTitle.Visibility = Visibility.Visible;
            BtnSubmit.Visibility = Visibility.Visible;
            
        }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // creates new deck with unique id and saves it to the DB
            DeckRepository deckRepository = new DeckRepository();
            Deck newDeck = new Deck();
            newDeck.Name = txtDeckTitle.Text;
            deckRepository.AddDeck(newDeck);
            MessageBox.Show($"{newDeck.Name} Deck added");
            lblTitle.Content = newDeck.Name;
            decknum = newDeck.Id;
            RefreshGrid();
            // hide elements related to deck submit
            txtDeckTitle.Visibility = Visibility.Hidden;
            BtnSubmit.Visibility = Visibility.Hidden;
            lblEnterTitle.Visibility = Visibility.Hidden;
            lblTitle.Visibility = Visibility.Visible;

            // Make rest of form visible again
            txtAnswer.Visibility = Visibility.Visible;
            lblAnswer.Visibility = Visibility.Visible;
            txtPrompt.Visibility = Visibility.Visible;
            lblPrompt.Visibility = Visibility.Visible;
            gridCards.Visibility = Visibility.Visible;
            lblTitle.Visibility = Visibility.Visible;
        }


        private void RowSelected(object sender, SelectionChangedEventArgs e)
        {
            Card selectedcard = gridCards.CurrentItem as Card;
            
            if (selectedcard != null)
            {
                cardnum = selectedcard.Id;
                txtPrompt.Text = Convert.ToString(selectedcard.Question);
                txtAnswer.Text = Convert.ToString(selectedcard.Answer);
            }
        }

        private void BtnAddCardClick(object sender, RoutedEventArgs e)
        {
            Card addCard = gridCards.CurrentItem as Card;
            // Have to prevent duplicate id's 
            // ensures fields are valid
                if (txtAnswer.Text != String.Empty && txtPrompt.Text != String.Empty)
                {
                    Card newCard = new Card();
                    newCard.Question = txtPrompt.Text;
                    newCard.Answer = txtAnswer.Text;
                    newCard.Deck_Id = decknum;
                    deckRepository.AddCard(newCard);
                    MessageBox.Show($"{newCard.Question} - Card added");
                    txtPrompt.Clear();
                    txtAnswer.Clear();
                }
            
            RefreshGrid();
        }
    }
}
