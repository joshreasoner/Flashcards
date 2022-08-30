using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Flashcards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private void BtnInvisible()
        {
            BtnDeck1.Visibility = Visibility.Collapsed;
            BtnDeck2.Visibility = Visibility.Collapsed;
            BtnDeck3.Visibility = Visibility.Collapsed;
            BtnDeck4.Visibility = Visibility.Collapsed;
            BtnDeck5.Visibility = Visibility.Collapsed;
            BtnDeck6.Visibility = Visibility.Collapsed;
        }
        private void ClearDeckBtns()
        {
            BtnDeck1.IsEnabled = false;
            BtnDeck2.IsEnabled = false;
            BtnDeck3.IsEnabled = false;
            BtnDeck4.IsEnabled = false;
            BtnDeck5.IsEnabled = false;
            BtnDeck6.IsEnabled = false;
        }

        public MainWindow()
        {
            InitializeComponent();

        }
        private void MainInitialized(object sender, System.EventArgs e)
        {
            BtnInvisible();
     
            DeckRepository deckRepository = new DeckRepository();
            FlashCardEntities entities = new FlashCardEntities();

            // Count Deck to see how many buttons to enable

            List<Deck> sortdeck = new List<Deck>(deckRepository.GetAllDecks());
            int numofdecks = sortdeck.Count();

            foreach (Deck deck in deckRepository.GetAllDecks())
            {
                // Load logic to set the deck names on txtboxes from database
                if (numofdecks == 6) { txtDeck1.Text = deck.Name; BtnDeck1.Tag = deck.Id; BtnDeck1.Visibility = Visibility.Visible; }
                else if (numofdecks == 5) { txtDeck2.Text = deck.Name; BtnDeck2.Tag = deck.Id; BtnDeck2.Visibility = Visibility.Visible; }
                else if (numofdecks == 4) { txtDeck3.Text = deck.Name; BtnDeck3.Tag = deck.Id; BtnDeck3.Visibility = Visibility.Visible; }
                else if (numofdecks == 3) { txtDeck4.Text = deck.Name; BtnDeck4.Tag = deck.Id; BtnDeck4.Visibility = Visibility.Visible; }
                else if (numofdecks == 2) { txtDeck5.Text = deck.Name; BtnDeck5.Tag = deck.Id; BtnDeck5.Visibility = Visibility.Visible; }
                else if (numofdecks == 1) { txtDeck6.Text = deck.Name; BtnDeck6.Tag = deck.Id; BtnDeck6.Visibility = Visibility.Visible; }
                numofdecks--;
            }

            BtnStudy.Visibility = Visibility.Hidden;
            BtnEdit.Visibility = Visibility.Hidden;
        }

        private void BtnStudyClick(object sender, RoutedEventArgs e)
        {
            // Goes to the study page
            // DeckId number value is passed in study object on btnstudy click w/ btn# enabled
            // logic to switch windows

            if (BtnDeck1.IsEnabled == true)
            {
                Study study = new Study((int)BtnDeck1.Tag);
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
            else if (BtnDeck2.IsEnabled == true)
            {
                Study study = new Study((int)BtnDeck2.Tag);
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
            else if (BtnDeck3.IsEnabled == true)
            {
                Study study = new Study((int)BtnDeck3.Tag);
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
            else if (BtnDeck4.IsEnabled == true)
            {
                Study study = new Study((int)BtnDeck4.Tag);
                MessageBox.Show($"{BtnDeck4.Tag}");
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
            else if (BtnDeck5.IsEnabled == true)
            {
                Study study = new Study((int)BtnDeck5.Tag);
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
            else if (BtnDeck6.IsEnabled == true)
            {
                Study study = new Study((int)BtnDeck1.Tag);
                this.Visibility = Visibility.Collapsed;
                study.Visibility = Visibility.Visible;
            }
        }

        private void BtnNewClick(object sender, RoutedEventArgs e)
        {
            // Open up Create Deck Window
            AddNew addform = new AddNew(1);
            this.Visibility = Visibility.Collapsed;
            addform.Visibility = Visibility.Visible;

        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            // Open up Modify Deck Window
            Modify modify = new Modify(1);
            this.Visibility = Visibility.Collapsed;
            modify.Visibility = Visibility.Visible;
        }

        private void BtnDeck1Click(object sender, RoutedEventArgs e)
        {
            ClearDeckBtns();
            // set btn to selected(enabled)
            BtnDeck1.IsEnabled = true;

            // logic for ui to look pressed

            // make option btns visible
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

        private void BtnDeck2Click(object sender, RoutedEventArgs e)
        {
            ClearDeckBtns();
            BtnDeck2.IsEnabled = true;
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

        private void BtnDeck3Click(object sender, RoutedEventArgs e)
        {
            ClearDeckBtns();
            BtnDeck3.IsEnabled = true;
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

        private void BtnDeck4Click(object sender, RoutedEventArgs e)
        {
            ClearDeckBtns();
            BtnDeck4.IsEnabled = true;
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

        private void BtnDeck5Click(object sender, RoutedEventArgs e)
        {
            ClearDeckBtns();
            BtnDeck5.IsEnabled = true;
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

        private void BtnDeck6Click(object sender, RoutedEventArgs e)
        {
            ClearDeckBtns();
            BtnDeck6.IsEnabled = true;
            BtnStudy.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

    }
}
