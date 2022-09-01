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
        
        // make DeckRepository Read only to protect from uneccessary access
        private readonly DeckRepository deckRepository;

        // creates global variables to track the active deck and card numbers
        int decknum;
        int cardnum;
        public Modify(int deckid)
        {
            InitializeComponent();
            deckRepository = new DeckRepository();
            // Takes passed in deck id to find deck to populate datagrid
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
            // performs a refresh of the datagrid by pulling all cards attached to deck detaching and reattching to db
            List<Card> carddata = new List<Card>(deckRepository.GetDeckCards(decknum));
            gridCards.ItemsSource = null;
            gridCards.ItemsSource = carddata;
        }
        private void BtnHomeClick(object sender, RoutedEventArgs e)
        {
            // Closes current form opens Main Window
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }
        private void BtnUpdateClick(object sender, RoutedEventArgs e)
        {
            // updates card selected and existing in db
            // verifies txtboxes aren't empty
            if (txtPrompt.Text != String.Empty && txtAnswer.Text != String.Empty)
            {
                // rewrites card in db by finding the card in db and updating fields
                Card updatecard = deckRepository.FindCard(cardnum);
                // if statement to verify card exists
                if (updatecard != null)
                {
                    updatecard.Question = txtPrompt.Text;
                    updatecard.Answer = txtAnswer.Text;              
                    deckRepository.UpdateCard(cardnum, updatecard);
                    MessageBox.Show($"Card details updated!");
                    RefreshGrid();
                    txtAnswer.Clear();
                    txtPrompt.Clear();
                    gridCards.CurrentCell.Equals(1);
                }
            }
        }
        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // Completes search for card in relation to active card number
            Card deletecard = deckRepository.FindCard(cardnum);
            // checks to validate a card is created based on a selection in datagrid
            if (deletecard != null)
            {
                // Gives last chance to cancel deletion
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
            BtnAddCard.Visibility = Visibility.Hidden;
            BtnDelete.Visibility = Visibility.Hidden;
            BtnUpdate.Visibility = Visibility.Hidden;
            // enable section to enter new deck title
            lblEnterTitle.Visibility = Visibility.Visible;
            txtDeckTitle.Visibility = Visibility.Visible;
            BtnSubmit.Visibility = Visibility.Visible;
        }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // creates new deck with unique id and saves it to the DB
            DeckRepository deckRepository = new DeckRepository();
            Deck newDeck = new Deck{ Name = txtDeckTitle.Text};
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
            BtnAddCard.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
            BtnUpdate.Visibility = Visibility.Visible;
            txtAnswer.Clear();
            txtPrompt.Clear();
        }
        private void RowSelected(object sender, SelectionChangedEventArgs e)
        {
            if (gridCards.CurrentItem is Card selectedcard)
            {
                cardnum = selectedcard.Id;
                txtPrompt.Text = Convert.ToString(selectedcard.Question);
                txtAnswer.Text = Convert.ToString(selectedcard.Answer);
            }
        }
        private void BtnAddCardClick(object sender, RoutedEventArgs e)
        {
            //Card addCard = gridCards.CurrentItem as Card;
            // Have to prevent duplicate id's 
            // ensures fields are valid
                if (txtAnswer.Text != String.Empty && txtPrompt.Text != String.Empty)
                {
                Card newCard = new Card
                {
                    Question = txtPrompt.Text,
                    Answer = txtAnswer.Text,
                    Deck_Id = decknum
                };
                deckRepository.AddCard(newCard);
                    MessageBox.Show($"{newCard.Question} - Card added");
                    txtPrompt.Clear();
                    txtAnswer.Clear();
                }
            RefreshGrid();
        }
    }
}
