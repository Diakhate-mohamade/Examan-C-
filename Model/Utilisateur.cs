using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFModernVerticalMenu.Model
{
    public class Utilisateur
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nom")]
        public string Nom { get; set; }

        [JsonProperty("prenom")]
        public string Prenom { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }

}
