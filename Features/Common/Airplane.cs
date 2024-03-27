using System;
using System.Collections.Generic;

namespace KataTestUnits.Features.Common
{
    /// <summary>
    /// Représente un modèle d'avion avec ses caractéristiques telles que le nombre de rangées et de sièges par rangée.
    /// </summary>
    public class Airplane
    {
        #region Properties

        /// <summary>
        /// Obtient ou définit le nombre de rangées de sièges dans l'avion.
        /// </summary>
        public int NumberOfRows { get; set; }

        /// <summary>
        /// Obtient ou définit le nombre de sièges par rangée dans l'avion.
        /// </summary>
        public int SeatsPerRow { get; set; }

        /// <summary>
        /// Obtient ou définit le nombre de sièges supplémentaires dans la dernière rangée de l'avion.
        /// </summary>
        public int AdditionalRowSeats { get; set; }

        /// <summary>
        /// Obtient le nombre total de sièges dans l'avion.
        /// </summary>
        public int TotalSeats
        {
            get
            {
                // Calcul du nombre total de sièges en tenant compte des rangées et des sièges par rangée
                int totalRegularSeats = NumberOfRows * SeatsPerRow;
                return totalRegularSeats + AdditionalRowSeats;
            }
            set { }
        }

        /// <summary>
        /// Liste des passagers déjà affectés à l'avion
        /// </summary>
        private List<Passenger> assignedPassengers = new List<Passenger>();

        /// <summary>
        /// Propriété calculée pour le nombre de sièges restants dans l'avion
        /// </summary>
        public int RemainingSeats => TotalSeats - assignedPassengers.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructeur qui initialise une nouvelle instance de la classe Airplane avec les spécifications par défaut.
        /// </summary>
        public Airplane()
        {
            // Par défaut, l'avion dispose de 33 rangées de 6 sièges et 1 rangée de 2 sièges
            NumberOfRows = 33;
            SeatsPerRow = 6;
            AdditionalRowSeats = 2;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Méthode pour affecter un passager à l'avion
        /// </summary>
        /// <param name="passenger">Le passager</param>
        public void AssignPassenger(Passenger passenger)
        {
            // Vérifier s'il reste des sièges disponibles dans l'avion
            if (RemainingSeats > 0)
            {
                // Ajouter le passager à la liste des passagers affectés
                assignedPassengers.Add(passenger);
            }
            else
            {
                throw new InvalidOperationException("Aucun siège disponible dans l'avion.");
            }
        }

        /// <summary>
        /// Méthode pour retirer un passager
        /// </summary>
        /// <param name="passenger">Le passager</param>
        public void RemovePassenger(Passenger passenger)
        {
            if (assignedPassengers.Contains(passenger))
            {
                // Retirer le passager de la liste des passagers affectés
                assignedPassengers.Remove(passenger);
            }
            else
            {
                throw new InvalidOperationException("Le passager n'est pas à bord de l'avion.");
            }
        }


        #endregion
    }
}
