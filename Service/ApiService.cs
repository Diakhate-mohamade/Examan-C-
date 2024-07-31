using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WPFModernVerticalMenu.Model;

namespace WPFModernVerticalMenu.Service
{
    /// <summary>
    /// Classe ApiService gérant les interactions avec une API pour les opérations CRUD sur les utilisateurs.
    /// </summary>
    public class ApiService : IDisposable
    {
        private readonly HttpClient _httpClient;  // Instance d'HttpClient pour envoyer des requêtes HTTP.
        private const string BaseUrl = "http://localhost/backend/";  // URL de base pour l'API.

        /// <summary>
        /// Constructeur de la classe ApiService. Initialise HttpClient avec l'URL de base et un délai d'attente.
        /// </summary>
        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl),  // Définit l'URL de base pour les requêtes HTTP.
                Timeout = TimeSpan.FromSeconds(30)  // Définit un délai d'attente de 30 secondes.
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  // Spécifie que le client accepte les réponses en JSON.
        }

        /// <summary>
        /// Récupère la liste des utilisateurs à partir de l'API.
        /// </summary>
        /// <returns>Liste des utilisateurs.</returns>
        public async Task<List<Utilisateur>> GetUtilisateursAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("list.php");  // Envoie une requête GET pour obtenir la liste des utilisateurs.
                response.EnsureSuccessStatusCode();  // Vérifie que la réponse HTTP est un succès.
                var responseBody = await response.Content.ReadAsStringAsync();  // Lit le contenu de la réponse en tant que chaîne.

                // Vérifie si la réponse contient des données JSON valides.
                if (string.IsNullOrEmpty(responseBody))
                {
                    Console.WriteLine("La réponse de l'API est vide.");
                    return new List<Utilisateur>();
                }

                return JsonConvert.DeserializeObject<List<Utilisateur>>(responseBody);  // Désérialise la réponse JSON en une liste d'utilisateurs.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur de requête HTTP dans GetUtilisateurs: {ex.Message}");  // Affiche une erreur de requête HTTP.
                return new List<Utilisateur>();  // Retourne une liste vide en cas d'erreur.
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erreur de désérialisation JSON dans GetUtilisateurs: {ex.Message}");  // Affiche une erreur de désérialisation JSON.
                return new List<Utilisateur>();  // Retourne une liste vide en cas d'erreur.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans GetUtilisateurs: {ex.Message}");  // Affiche une erreur générale.
                return new List<Utilisateur>();  // Retourne une liste vide en cas d'erreur.
            }
        }

        /// <summary>
        /// Récupère un utilisateur spécifique à partir de l'API en utilisant son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur.</param>
        /// <returns>Utilisateur correspondant à l'identifiant.</returns>
        public async Task<Utilisateur> GetUtilisateurAsync(int id)
        {
            if (id <= 0)
            {
                Console.WriteLine("L'identifiant de l'utilisateur doit être supérieur à zéro.");
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync($"details.php?id={id}");  // Envoie une requête GET pour obtenir les détails d'un utilisateur spécifique.
                response.EnsureSuccessStatusCode();  // Vérifie que la réponse HTTP est un succès.
                var responseBody = await response.Content.ReadAsStringAsync();  // Lit le contenu de la réponse en tant que chaîne.

                if (string.IsNullOrEmpty(responseBody))
                {
                    Console.WriteLine("La réponse de l'API est vide.");
                    return null;
                }

                return JsonConvert.DeserializeObject<Utilisateur>(responseBody);  // Désérialise la réponse JSON en un utilisateur.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur de requête HTTP dans GetUtilisateur: {ex.Message}");  // Affiche une erreur de requête HTTP.
                return null;  // Retourne null en cas d'erreur.
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erreur de désérialisation JSON dans GetUtilisateur: {ex.Message}");  // Affiche une erreur de désérialisation JSON.
                return null;  // Retourne null en cas d'erreur.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans GetUtilisateur: {ex.Message}");  // Affiche une erreur générale.
                return null;  // Retourne null en cas d'erreur.
            }
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur via l'API.
        /// </summary>
        /// <param name="utilisateur">Utilisateur à ajouter.</param>
        /// <returns>Indique si l'ajout a réussi.</returns>
        public async Task<bool> AjouterUtilisateurAsync(Utilisateur utilisateur)
        {
            if (utilisateur == null)
            {
                Console.WriteLine("L'utilisateur à ajouter ne peut pas être null.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(utilisateur.Nom) || string.IsNullOrWhiteSpace(utilisateur.Prenom) || utilisateur.Age <= 0)
            {
                Console.WriteLine("Les données de l'utilisateur sont invalides.");
                return false;
            }

            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nom", utilisateur.Nom),
                    new KeyValuePair<string, string>("prenom", utilisateur.Prenom),
                    new KeyValuePair<string, string>("age", utilisateur.Age.ToString())
                });

                var response = await _httpClient.PostAsync("create.php", content);  // Envoie une requête POST pour ajouter un utilisateur.
                response.EnsureSuccessStatusCode();  // Vérifie que la réponse HTTP est un succès.
                var responseBody = await response.Content.ReadAsStringAsync();  // Lit le contenu de la réponse en tant que chaîne.
                var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(responseBody);  // Désérialise la réponse JSON en un dictionnaire.
                return result.ContainsKey("success") && result["success"];  // Vérifie si l'ajout a réussi.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur de requête HTTP dans AjouterUtilisateur: {ex.Message}");  // Affiche une erreur de requête HTTP.
                return false;  // Retourne false en cas d'erreur.
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erreur de désérialisation JSON dans AjouterUtilisateur: {ex.Message}");  // Affiche une erreur de désérialisation JSON.
                return false;  // Retourne false en cas d'erreur.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans AjouterUtilisateur: {ex.Message}");  // Affiche une erreur générale.
                return false;  // Retourne false en cas d'erreur.
            }
        }

        /// <summary>
        /// Met à jour un utilisateur via l'API.
        /// </summary>
        /// <param name="utilisateur">Utilisateur à mettre à jour.</param>
        /// <returns>Indique si la mise à jour a réussi.</returns>
        public async Task<bool> MettreAJourUtilisateurAsync(Utilisateur utilisateur)
        {
            if (utilisateur == null)
            {
                Console.WriteLine("L'utilisateur à mettre à jour ne peut pas être null.");
                return false;
            }

            if (utilisateur.Id <= 0 || string.IsNullOrWhiteSpace(utilisateur.Nom) || string.IsNullOrWhiteSpace(utilisateur.Prenom) || utilisateur.Age <= 0)
            {
                Console.WriteLine("Les données de l'utilisateur sont invalides.");
                return false;
            }

            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", utilisateur.Id.ToString()),
                    new KeyValuePair<string, string>("nom", utilisateur.Nom),
                    new KeyValuePair<string, string>("prenom", utilisateur.Prenom),
                    new KeyValuePair<string, string>("age", utilisateur.Age.ToString())
                });

                var response = await _httpClient.PostAsync("update.php", content);  // Envoie une requête POST pour mettre à jour un utilisateur.
                response.EnsureSuccessStatusCode();  // Vérifie que la réponse HTTP est un succès.
                var responseBody = await response.Content.ReadAsStringAsync();  // Lit le contenu de la réponse en tant que chaîne.
                var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(responseBody);  // Désérialise la réponse JSON en un dictionnaire.
                return result.ContainsKey("success") && result["success"];  // Vérifie si la mise à jour a réussi.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur de requête HTTP dans MettreAJourUtilisateur: {ex.Message}");  // Affiche une erreur de requête HTTP.
                return false;  // Retourne false en cas d'erreur.
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erreur de désérialisation JSON dans MettreAJourUtilisateur: {ex.Message}");  // Affiche une erreur de désérialisation JSON.
                return false;  // Retourne false en cas d'erreur.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans MettreAJourUtilisateur: {ex.Message}");  // Affiche une erreur générale.
                return false;  // Retourne false en cas d'erreur.
            }
        }

        /// <summary>
        /// Supprime un utilisateur via l'API.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à supprimer.</param>
        /// <returns>Indique si la suppression a réussi.</returns>
        public async Task<bool> SupprimerUtilisateurAsync(int id)
        {
            if (id <= 0)
            {
                Console.WriteLine("L'identifiant de l'utilisateur doit être supérieur à zéro.");
                return false;
            }

            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", id.ToString())
                });

                var response = await _httpClient.PostAsync("delete.php", content);  // Envoie une requête POST pour supprimer un utilisateur.
                response.EnsureSuccessStatusCode();  // Vérifie que la réponse HTTP est un succès.
                var responseBody = await response.Content.ReadAsStringAsync();  // Lit le contenu de la réponse en tant que chaîne.
                var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(responseBody);  // Désérialise la réponse JSON en un dictionnaire.
                return result.ContainsKey("success") && result["success"];  // Vérifie si la suppression a réussi.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur de requête HTTP dans SupprimerUtilisateur: {ex.Message}");  // Affiche une erreur de requête HTTP.
                return false;  // Retourne false en cas d'erreur.
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erreur de désérialisation JSON dans SupprimerUtilisateur: {ex.Message}");  // Affiche une erreur de désérialisation JSON.
                return false;  // Retourne false en cas d'erreur.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans SupprimerUtilisateur: {ex.Message}");  // Affiche une erreur générale.
                return false;  // Retourne false en cas d'erreur.
            }
        }

        /// <summary>
        /// Libère les ressources utilisées par HttpClient.
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();  // Dispose de l'instance HttpClient si elle n'est pas null.
        }
    }
}
