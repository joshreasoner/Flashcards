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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DeckRepository deckRepository;
        public MainWindow()
        {
            InitializeComponent();
            // Have to call Loaddecks function twice
            // 1. to populate decks 2. to make visible once they are populated
            LoadDecks();
            DecksInvisible();
            LoadDecks();
            // Lower 3 buttons visibility is dependant on deck button click
            BtnStudy.Visibility = Visibility.Hidden;
            BtnEdit.Visibility = Visibility.Hidden;
            BtnDelete.Visibility = Visibility.Hidden;
        }
        private void BtnsVisible()
        {
            // Makes lower 3 buttons visible
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
        }
        private void DecksInvisible()
        {
            // Makes all deck buttons invisible
            BtnDeck1.Visibility = Visibility.Collapsed;
            BtnDeck2.Visibility = Visibility.Collapsed;
            BtnDeck3.Visibility = Visibility.Collapsed;
            BtnDeck4.Visibility = Visibility.Collapsed;
            BtnDeck5.Visibility = Visibility.Collapsed;
            BtnDeck6.Visibility = Visibility.Collapsed;
        }
        private void DisableDeckBtns()
        {
            // Disables all deck buttons
            BtnDeck1.IsEnabled = false;
            BtnDeck2.IsEnabled = false;
            BtnDeck3.IsEnabled = false;
            BtnDeck4.IsEnabled = false;
            BtnDeck5.IsEnabled = false;
            BtnDeck6.IsEnabled = false;
        }
        private void EnableDeckBtns()
        {
            // Enables all deck buttons
            BtnDeck1.IsEnabled = true;
            BtnDeck2.IsEnabled = true;
            BtnDeck3.IsEnabled = true;
            BtnDeck4.IsEnabled = true;
            BtnDeck5.IsEnabled = true;
            BtnDeck6.IsEnabled = true;        
        }
 
        private void LoadDecks()
        {
            // Method to populate each deck button with each card's data for use in methods.
            
            DeckRepository deckRepository = new DeckRepository();
            List<Deck> sortdeck = new List<Deck>(deckRepository.GetAllDecks());
            // Locates index of lowest Deck ID (Primary Key) of decks in deck table
            int mindecks = sortdeck.Min(deck => deck.Id);
            foreach (Deck deck in deckRepository.GetAllDecks())
            {
                // Load logic to set the deck names on txtboxes from database
                if (mindecks == 1) { txtDeck1.Text = deck.Name; BtnDeck1.Tag = deck.Id; BtnDeck1.Visibility = Visibility.Visible; }
                else if (mindecks == 2) { txtDeck2.Text = deck.Name; BtnDeck2.Tag = deck.Id; BtnDeck2.Visibility = Visibility.Visible; }
                else if (mindecks == 3) { txtDeck3.Text = deck.Name; BtnDeck3.Tag = deck.Id; BtnDeck3.Visibility = Visibility.Visible; }
                else if (mindecks == 4) { txtDeck4.Text = deck.Name; BtnDeck4.Tag = deck.Id; BtnDeck4.Visibility = Visibility.Visible; }
                else if (mindecks == 5) { txtDeck5.Text = deck.Name; BtnDeck5.Tag = deck.Id; BtnDeck5.Visibility = Visibility.Visible; }
                else if (mindecks == 6) { txtDeck6.Text = deck.Name; BtnDeck6.Tag = deck.Id; BtnDeck6.Visibility = Visibility.Visible; }
                mindecks++;
            }
        }
        private void MovetoStudy(int id)
        {
            deckRepository = new DeckRepository();
            
            Deck studydeck = deckRepository.FindDeck(id);
            // Have to count cards in deck to handle null ref exception
            // prevents end user from entering the study deck with no cards
            int cardcount = studydeck.Cards.Count();
            if (cardcount > 0)
            {
                Study study = new Study(id);
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
            else { MessageBox.Show($"No cards in {studydeck.Name} to study"); }
        }
        private void BtnStudyClick(object sender, RoutedEventArgs e)
        {
            // Goes to the study page
            // DeckId number value is passed in study object on btnstudy click w/ btn# enabled
            // logic to switch windows

            if (BtnDeck1.IsEnabled == true){MovetoStudy((int)BtnDeck1.Tag);}
            else if (BtnDeck2.IsEnabled == true){MovetoStudy((int)BtnDeck2.Tag);}
            else if (BtnDeck3.IsEnabled == true){MovetoStudy((int)BtnDeck3.Tag); }
            else if (BtnDeck4.IsEnabled == true){MovetoStudy((int)BtnDeck4.Tag); }
            else if (BtnDeck5.IsEnabled == true){MovetoStudy((int)BtnDeck5.Tag); }
            else if (BtnDeck6.IsEnabled == true){MovetoStudy((int)BtnDeck6.Tag); }
        }
        private void MovetoModify(int id)
        {
            // creates new Modify form. Opens new form and closes main window.
            Modify modify = new Modify(id);
            this.Visibility = Visibility.Collapsed;
            modify.Visibility = Visibility.Visible;
        }
        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            // Open up Modify Deck Window
            // Check to see which Deck button is active passes variable present in
            // button tag to the move to modify function
            if (BtnDeck1.IsEnabled == true) { MovetoModify((int)BtnDeck1.Tag); }
            else if (BtnDeck2.IsEnabled == true) { MovetoModify((int)BtnDeck2.Tag); }
            else if (BtnDeck3.IsEnabled == true) { MovetoModify((int)BtnDeck3.Tag); }
            else if (BtnDeck4.IsEnabled == true) { MovetoModify((int)BtnDeck4.Tag); }
            else if (BtnDeck5.IsEnabled == true) { MovetoModify((int)BtnDeck5.Tag); }
            else if (BtnDeck6.IsEnabled == true) { MovetoModify((int)BtnDeck6.Tag); }
        }
        private void ConfirmDelete(int btnId)
        {
            // Displays a message box to confirm deletion of entire deck
            // Based on database settings it will delete all cards once deck is deleted.

            // Button Tag is passed into method call
            deckRepository = new DeckRepository();
            Deck deletedeck = deckRepository.FindDeck(btnId);
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Are you sure you want to delete {deletedeck.Name} and all it's cards?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                deckRepository.DeleteDeck(deletedeck);
            }
            // Refreshing and reloading button objects on form
            DecksInvisible();
            LoadDecks();
            ResetButtons();
            this.InitializeComponent();
            
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Passes Btn Tag# to Confirm Delete Method when specific button is enabled
            if (BtnDeck1.IsEnabled == true) {ConfirmDelete((int)BtnDeck1.Tag); }
            else if (BtnDeck2.IsEnabled == true) {ConfirmDelete((int)BtnDeck2.Tag); }
            else if (BtnDeck3.IsEnabled == true){ConfirmDelete((int)BtnDeck3.Tag);}
            else if (BtnDeck4.IsEnabled == true){ConfirmDelete((int)BtnDeck4.Tag);}
            else if (BtnDeck5.IsEnabled == true){ConfirmDelete((int)BtnDeck5.Tag);}
            else if (BtnDeck6.IsEnabled == true){ConfirmDelete((int)BtnDeck6.Tag);}
            ResetButtons();
        }
        #region Deck Button Click Events
        // Button click evens relateed to each button for a deck of cards
        // When button is click it flags the one that is enabled
        // first disables all buttons, then enables the clicked one as true
        private void BtnDeck1_Click(object sender, RoutedEventArgs e)
        {
            DisableDeckBtns();
            BtnDeck1.IsEnabled = true;
            BtnsVisible();
        }
        private void BtnDeck2Click(object sender, RoutedEventArgs e)
        {
            DisableDeckBtns();
            BtnDeck2.IsEnabled = true;
            BtnsVisible();
        }

        private void BtnDeck3Click(object sender, RoutedEventArgs e)
        {
            DisableDeckBtns();
            BtnDeck3.IsEnabled = true;
            BtnsVisible();
        }

        private void BtnDeck4Click(object sender, RoutedEventArgs e)
        {
            DisableDeckBtns();
            BtnDeck4.IsEnabled = true;
            BtnsVisible();
        }

        private void BtnDeck5Click(object sender, RoutedEventArgs e)
        {
            DisableDeckBtns();
            BtnDeck5.IsEnabled = true;
            BtnsVisible();
        }

        private void BtnDeck6Click(object sender, RoutedEventArgs e)
        {
            DisableDeckBtns();
            BtnDeck6.IsEnabled = true;
            BtnsVisible();
        }
        #endregion
        private void ResetButtons()
        {
            // Resets form to the initial state 
            EnableDeckBtns();
            BtnStudy.Visibility = Visibility.Hidden;
            BtnEdit.Visibility = Visibility.Hidden;
            BtnDelete.Visibility = Visibility.Hidden;
        }
        private void MainDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // this event is to create a way to reset the form to the initial state by
            // double clicking on the main form.
            EnableDeckBtns();
            BtnStudy.Visibility = Visibility.Hidden;
            BtnEdit.Visibility = Visibility.Hidden;
            BtnDelete.Visibility = Visibility.Hidden;
        }
    }
}
