using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace CricketTeamManager
{
    public partial class MainWindow : Window
    {
        // ObservableCollection to bind to the ListBox
        private ObservableCollection<string> playerRoster;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the ObservableCollection
            playerRoster = new ObservableCollection<string>();

            // Bind the ListBox to the ObservableCollection
            PlayerListBox.ItemsSource = playerRoster;

            // Attach event handlers for Add and Remove buttons
            AddPlayerButton.Click += AddPlayerButton_Click;
            RemovePlayerButton.Click += RemovePlayerButton_Click;
        }

        // Event handler for adding a player
        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = PlayerNameTextBox.Text.Trim();

            // Validation: Ensure the name is not empty or duplicate
            if (string.IsNullOrWhiteSpace(playerName) || playerName == "Enter player name")
            {
                MessageBox.Show("Player name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (playerRoster.Contains(playerName))
            {
                MessageBox.Show("Player already exists in the roster.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Add the player to the roster
            playerRoster.Add(playerName);
            PlayerNameTextBox.Clear();
            PlayerNameTextBox.Text = "Enter player name";
            PlayerNameTextBox.Foreground = Brushes.Gray; // Reset placeholder text
            MessageBox.Show($"Player '{playerName}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for removing a player
        private void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedPlayer = PlayerListBox.SelectedItem as string;

            // Validation: Ensure a player is selected
            if (selectedPlayer == null)
            {
                MessageBox.Show("Please select a player to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Remove the selected player
            playerRoster.Remove(selectedPlayer);
            MessageBox.Show($"Player '{selectedPlayer}' removed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Handle TextBox focus for placeholder text
        private void PlayerNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PlayerNameTextBox.Text == "Enter player name")
            {
                PlayerNameTextBox.Text = string.Empty;
                PlayerNameTextBox.Foreground = Brushes.Black; // Reset text color
            }
        }

        private void PlayerNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
            {
                PlayerNameTextBox.Text = "Enter player name";
                PlayerNameTextBox.Foreground = Brushes.Gray; // Set placeholder text color
            }
        }
    }
}
