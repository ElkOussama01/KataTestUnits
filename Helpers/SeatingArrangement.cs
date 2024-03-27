using System.Collections.Generic;
using System.Linq;

namespace KataTestUnits.Features
{
    /// <summary>
    /// SeatingArrangement Helper
    /// </summary>
    public static class SeatingArrangement
    {
        #region Constants

        /// <summary>
        /// Nombre total de rangées dans l'avion
        /// </summary>
        private const int TotalRows = 34;

        /// <summary>
        /// Nombre total de sièges dans chaque rangée
        /// </summary>
        private const int SeatsPerRow = 6;

        #endregion

        #region Methods

        /// <summary>
        /// Affecte les passagers à des sièges en veillant à ce que les membres d'une même famille soient assis ensemble.
        /// </summary>
        /// <param name="passengers">La liste des passagers à placer dans l'avion.</param>
        /// <returns>Une liste de familles avec leurs membres assis dans l'avion.</returns>
        public static List<Family> AssignSeating(List<Passenger> passengers)
        {
            List<Family> families = new List<Family>();

            foreach (Passenger passenger in passengers)
            {
                // Recherche d'une famille existante à laquelle le passager peut appartenir
                Family existingFamily = families.FirstOrDefault(family => family.CanAddMember(passenger));

                if (existingFamily != null)
                {
                    existingFamily.AddMember(passenger);
                }
                else
                {
                    // Création d'une nouvelle famille pour le passager
                    Family newFamily = new Family();
                    newFamily.AddMember(passenger);
                    families.Add(newFamily);
                }
            }

            // Affectation des sièges aux passagers de chaque famille
            foreach (var family in families)
            {
                AssignSeatsToFamilyMembers(family);
            }

            return families;
        }

        /// <summary>
        /// Affecte les sièges aux membres de la famille.
        /// </summary>
        /// <param name="family">La famille à laquelle les sièges doivent être affectés.</param>
        private static void AssignSeatsToFamilyMembers(Family family)
        {
            // Trie des membres de la famille par type (adulte/enfant) et âge
            List<Passenger> sortedMembers = family.Members.OrderBy(member => member.Type).ThenBy(member => member.Age).ToList();

            // Affectation des sièges en commençant par la première rangée
            int currentRow = 1;
            int currentSeat = 1;

            foreach (Passenger member in sortedMembers)
            {
                // Affectation du siège au membre de la famille
                member.Seat = new Seat(currentRow, currentSeat);

                // Passage au siège suivant dans la même rangée ou à la première place de la rangée suivante si nécessaire
                currentSeat++;
                if (currentSeat > SeatsPerRow)
                {
                    currentSeat = 1;
                    currentRow++;
                }
            }
        }

        /// <summary>
        /// Vérifie si les membres de chaque famille sont assis ensemble dans l'avion.
        /// </summary>
        /// <param name="families">Liste des familles à vérifier.</param>
        /// <returns>True si toutes les familles sont assises ensemble, sinon False.</returns>
        public static bool FamiliesAreSeatedTogether(List<Family> families)
        {
            foreach (var family in families)
            {
                int? row = null;
                foreach (var member in family.Members)
                {
                    if (row == null)
                    {
                        row = member.Seat.Row;
                    }
                    else if (member.Seat.Row != row && member.Seat.Row != row + 1 && member.Seat.Row != row - 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Vérifie si toutes les familles sont assises ensemble dans l'avion.
        /// </summary>
        /// <param name="boardedPassengers">La liste des passagers embarqués dans l'avion.</param>
        /// <returns>True si toutes les familles sont ensemble, sinon False.</returns>
        public static bool FamiliesAreTogether(List<Passenger> boardedPassengers)
        {
            // Parcourt tous les passagers embarqués dans l'avion
            foreach (var passenger in boardedPassengers)
            {
                // Récupère tous les membres de la famille du passager actuel dans la liste des passagers embarqués
                var familyMembers = boardedPassengers.Where(p => p.FamilyID == passenger.FamilyID).ToList();

                // Vérifie si le nombre de membres de chaque famille correspond au nombre de membres présents dans la liste des passagers embarqués
                if (familyMembers.Count != boardedPassengers.Count(p => p.FamilyID == passenger.FamilyID))
                {
                    // Si tous les membres de la famille ne sont pas présents, renvoie False
                    return false;
                }
            }

            // Si tous les membres de chaque famille sont présents, renvoie True
            return true;
        }

        #endregion
    }
}
