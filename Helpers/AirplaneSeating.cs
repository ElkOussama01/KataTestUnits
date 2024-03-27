using System.Collections.Generic;

namespace KataTestUnits.Features.Common
{
    /// <summary>
    /// AirplaneSeating Helper
    /// </summary>
    public static class AirplaneSeating
    {
        #region Methods

        /// <summary>
        /// Méthode pour affecter les passagers à l'avion en respectant les contraintes de disposition.
        /// </summary>
        /// <param name="passengers">La liste des passagers à embarquer.</param>
        /// <param name="airplane">L'avion dans lequel les passagers doivent être embarqués.</param>
        /// <returns>La liste des passagers embarqués dans l'avion.</returns>
        public static List<Passenger> AssignPassengers(List<Passenger> passengers, Airplane airplane)
        {
            var assignedPassengers = new List<Passenger>();

            foreach (var passenger in passengers)
            {
                // Vérifier si l'avion a des sièges disponibles
                if (airplane.RemainingSeats > 0)
                {
                    // Affecter le passager à l'avion
                    airplane.AssignPassenger(passenger);

                    // Ajouter le passager à la liste des passagers affectés
                    assignedPassengers.Add(passenger);
                }
                else
                {
                    // Si l'avion est plein, arrêter l'affectation des passagers
                    break;
                }
            }

            return assignedPassengers;
        }

        #endregion
    }
}