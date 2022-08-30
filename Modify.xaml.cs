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
        FlashCardEntities entity;
        DeckRepository deckRepository;

        int decknum;
        int cardnum;
        public Modify(int deckid)
        {
            InitializeComponent();
            deckRepository = new DeckRepository();
            entity = new FlashCardEntities();
            
            Deck titledeck = deckRepository.FindDeck(deckid);
            lblTitle.Content = titledeck.Name;
            //var deckdata = deckRepository.GetDeckCards(deckid);
            List <Card> carddata = new List<Card>(deckRepository.GetDeckCards(deckid));
            
            gridCards.ItemsSource = carddata;
            decknum = deckid;
            

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
                
                List<Card> carddata = new List<Card>(deckRepository.GetDeckCards(decknum));
                gridCards.ItemsSource = null;
                gridCards.ItemsSource = carddata;
                txtAnswer.Clear();
                txtPrompt.Clear();
                gridCards.CurrentCell.Equals(1);
            }
        }
        private void RowSelected(object sender, SelectionChangedEventArgs e)
        {
            Card selectedcard = gridCards.CurrentItem as Card;
            
            if (selectedcard != null)
            {
                cardnum = selectedcard.Id;
                txtPrompt.Text = Convert.ToString(selectedcard.Question);
                txtAnswer.Text = Convert.ToString(selectedcard.Answer);
                MessageBox.Show($"selected card id: {cardnum}");
            }
            else
            {
                txtAnswer.Clear();
                txtPrompt.Clear();
            }

        }


        private void NoSelection(object sender, SelectedCellsChangedEventArgs e)
        {
            txtAnswer.Clear();
            txtPrompt.Clear();
        }
    }
}
