using System;
using System.Collections.Generic;
using System.Linq;

namespace KataTestUnits
{
    /// <summary>
    /// Classe Family qui représente une famille de passagers dans l'avion.
    /// </summary>
    public class Family
    {
        #region Properties

        /// <summary>
        /// Liste des membres de la famille
        /// </summary>
        public List<Passenger> Members { get; set; } = new List<Passenger>();

        #endregion

        #region Methods

        /// <summary>
        /// Ajoute un membre à la famille.
        /// </summary>
        /// <param name="passenger">Le passager à ajouter à la famille.</param>
        public void AddMember(Passenger passenger)
        {
            Members.Add(passenger);
        }

        /// <summary>
        /// Supprime un membre de la famille.
        /// </summary>
        /// <param name="passenger">Le passager à supprimer de la famille.</param>
        public void DeleteMember(Passenger passenger)
        {
            if (passenger == null)
            {
                throw new ArgumentNullException(nameof(passenger), "Le passager ne peut pas être null.");
            }

            Members.Remove(passenger);
        }

        /// <summary>
        /// Met à jour les informations d'un membre de la famille.
        /// </summary>
        /// <param name="oldPassenger">Le passager à mettre à jour.</param>
        /// <param name="newPassenger">Le nouveau passager avec les informations mises à jour.</param>
        public void UpdateMember(Passenger oldPassenger, Passenger newPassenger)
        {
            if (oldPassenger == null)
            {
                throw new ArgumentNullException(nameof(oldPassenger), "L'ancien passager ne peut pas être null.");
            }

            if (newPassenger == null)
            {
                throw new ArgumentNullException(nameof(newPassenger), "Le nouveau passager ne peut pas être null.");
            }

            // Recherche l'index de l'ancien passager dans la liste
            int index = Members.IndexOf(oldPassenger);

            if (index == -1)
            {
                throw new ArgumentException("Le passager à mettre à jour n'a pas été trouvé dans la famille.", nameof(oldPassenger));
            }

            // Remplace l'ancien passager par le nouveau passager
            Members[index] = newPassenger;
        }

        /// <summary>
        /// Calcule le prix total des billets pour tous les membres de la famille.
        /// </summary>
        /// <returns>Le prix total en euros.</returns>
        public int CalculateTotalPrice()
        {
            // Calculer la somme des prix des billets pour tous les membres de la famille
            return Members.Sum(member => member.CalculateTicketPrice());
        }

        /// <summary>
        /// Vérifie si un passager peut être ajouté à la famille en respectant les contraintes.
        /// </summary>
        /// <param name="passenger">Le passager à vérifier.</param>
        /// <returns>True si le passager peut être ajouté, sinon False.</returns>
        public bool CanAddMember(Passenger passenger)
        {
            // Vérifie si le nombre maximum d'adultes et d'enfants n'est pas dépassé dans la famille
            if (passenger.Type == PassengerType.Adulte && Members.Count(member => member.Type == PassengerType.Adulte) >= 2)
            {
                return false;
            }

            if (passenger.Type == PassengerType.Enfant && Members.Count(member => member.Type == PassengerType.Enfant) >= 3)
            {
                return false;
            }

            // Vérifie si un enfant de moins de 12 ans ne peut pas être ajouté seul
            if (passenger.Type == PassengerType.Enfant && passenger.Age < 12 && Members.All(member => member.Type != PassengerType.Adulte))
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}