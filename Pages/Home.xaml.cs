using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFModernVerticalMenu.Model;
using WPFModernVerticalMenu.Service;

namespace WPFModernVerticalMenu.Pages
{
    /// <summary>
    /// Page d'accueil pour la gestion des utilisateurs.
    /// </summary>
    public partial class Home : Page
    {
        private readonly ApiService _apiService;

        public Home()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        /// <summary>
        /// Charge la liste des utilisateurs depuis le service API et l'affiche dans la ListBox.
        /// </summary>
        private async void ChargerUtilisateurs_Click(object sender, RoutedEventArgs e)
        {
            var utilisateurs = await _apiService.GetUtilisateursAsync();
            UtilisateursListBox.ItemsSource = utilisateurs;
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur en utilisant les données saisies et recharge la liste des utilisateurs.
        /// </summary>
        private async void AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderDonneesSaisies(out int age))
            {
                var utilisateur = new Utilisateur
                {
                    Nom = NomTextBox.Text,
                    Prenom = PrenomTextBox.Text,
                    Age = age
                };

                var success = await _apiService.AjouterUtilisateurAsync(utilisateur);
                if (success)
                {
                    MessageBox.Show("Utilisateur ajouté avec succès !");
                    ChargerUtilisateurs_Click(sender, e);  // Recharge la liste pour afficher le nouvel utilisateur.
                    Effacer(); // Appel à la méthode Effacer après l'ajout
                }
                else
                {
                    MessageBox.Show("Échec de l'ajout de l'utilisateur.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer des données valides. Assurez-vous que l'âge est entre 18 et 100.");
            }
        }

        /// <summary>
        /// Met à jour les informations de l'utilisateur sélectionné en utilisant les données saisies.
        /// </summary>
        private async void MettreAJourUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (UtilisateursListBox.SelectedItem is Utilisateur selectedUtilisateur)
            {
                if (ValiderDonneesSaisies(out int age))
                {
                    selectedUtilisateur.Nom = NomTextBox.Text;
                    selectedUtilisateur.Prenom = PrenomTextBox.Text;
                    selectedUtilisateur.Age = age;

                    var success = await _apiService.MettreAJourUtilisateurAsync(selectedUtilisateur);
                    if (success)
                    {
                        MessageBox.Show("Utilisateur mis à jour avec succès !");
                        ChargerUtilisateurs_Click(sender, e);  // Recharge la liste pour afficher les modifications.
                        Effacer(); // Appel à la méthode Effacer après la mise à jour
                    }
                    else
                    {
                        MessageBox.Show("Échec de la mise à jour de l'utilisateur.");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez entrer des données valides. Assurez-vous que l'âge est entre 18 et 100.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à mettre à jour.");
            }
        }

        /// <summary>
        /// Supprime l'utilisateur sélectionné et recharge la liste des utilisateurs.
        /// </summary>
        private async void SupprimerUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (UtilisateursListBox.SelectedItem is Utilisateur selectedUtilisateur)
            {
                var success = await _apiService.SupprimerUtilisateurAsync(selectedUtilisateur.Id);
                if (success)
                {
                    MessageBox.Show("Utilisateur supprimé avec succès !");
                    ChargerUtilisateurs_Click(sender, e);  // Recharge la liste pour refléter la suppression.
                    Effacer(); // Appel à la méthode Effacer après la suppression
                }
                else
                {
                    MessageBox.Show("Échec de la suppression de l'utilisateur.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.");
            }
        }

        /// <summary>
        /// Remplit les champs de saisie avec les données de l'utilisateur sélectionné dans la ListBox.
        /// </summary>
        private void UtilisateursListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UtilisateursListBox.SelectedItem is Utilisateur selectedUtilisateur)
            {
                NomTextBox.Text = selectedUtilisateur.Nom;
                PrenomTextBox.Text = selectedUtilisateur.Prenom;
                AgeTextBox.Text = selectedUtilisateur.Age.ToString();
            }
        }

        /// <summary>
        /// Valide les données saisies dans les champs.
        /// </summary>
        private bool ValiderDonneesSaisies(out int age)
        {
            age = 0;
            if (string.IsNullOrWhiteSpace(NomTextBox.Text) ||
                string.IsNullOrWhiteSpace(PrenomTextBox.Text) ||
                !int.TryParse(AgeTextBox.Text, out age) ||
                age < 18 || age > 100)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Vérifie si le texte saisi est un chiffre.
        /// </summary>
        private void AgeTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        /// <summary>
        /// Valide que l'âge est entre 18 et 100 et affiche un message d'erreur si ce n'est pas le cas.
        /// </summary>
        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AgeTextBox.Text, out int age))
            {
                if (age < 18 || age > 100)
                {
                    AgeTextBox.Background = Brushes.Pink;
                }
                else
                {
                    AgeTextBox.Background = Brushes.White;
                }
            }
            else
            {
                AgeTextBox.Background = Brushes.Pink;
            }
        }

        /// <summary>
        /// Méthode pour vider les champs de texte, recharger la liste des utilisateurs, et replacer le focus sur le champ prénom.
        /// </summary>
        private async void Effacer()
        {
            // Vider les champs de texte
            NomTextBox.Text = string.Empty;
            PrenomTextBox.Text = string.Empty;
            AgeTextBox.Text = string.Empty;

            // Recharger la liste des utilisateurs
            var utilisateurs = await _apiService.GetUtilisateursAsync();
            UtilisateursListBox.ItemsSource = utilisateurs;

            PrenomTextBox.Focus();
        }
    }
}
