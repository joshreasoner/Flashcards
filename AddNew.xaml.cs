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
    /// Interaction logic for AddNew.xaml
    /// </summary>
    public partial class AddNew : Window
    {
        int decknum;
        DeckRepository deckRepository;
        public AddNew(int deckid)
        {
            InitializeComponent();
            deckRepository = new DeckRepository();
            lblTitle.Visibility = Visibility.Hidden;
            groupAddNew.Visibility = Visibility.Hidden;
            decknum = deckid;
        }

        private void BtnSubmitClick(object sender, RoutedEventArgs e)
        {
            DeckRepository deckRepository = new DeckRepository();
            Deck newDeck = new Deck();
            newDeck.Name = txtDeckTitle.Text;
            deckRepository.AddDeck(newDeck);
            MessageBox.Show($"{newDeck.Name} Deck added");
            txtDeckTitle.Visibility = Visibility.Hidden;
            BtnTitle.Visibility = Visibility.Hidden;
            lblEnterTitle.Visibility = Visibility.Hidden;
            lblTitle.Content = newDeck.Name;
            lblTitle.Visibility = Visibility.Visible;
            decknum = newDeck.Id;
            MessageBox.Show($"{decknum}");
            groupAddNew.Visibility = Visibility.Visible;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        private void BtnAddCard_Click(object sender, RoutedEventArgs e)
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
    }
}
