using System;

namespace KataTestUnits
{
    /// <summary>
    /// Classe Passenger
    /// </summary>
    public class Passenger
    {
        #region Properties

        /// <summary>
        /// Obtient ou définit le type de passager (adulte, enfant ou adulte nécessitant deux places).
        /// </summary>
        public PassengerType Type { get; set; }

        /// <summary>
        /// Obtient ou définit l'âge du passager.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de la famille à laquelle le passager appartient, ou "-" s'il n'appartient à aucune famille.
        /// </summary>
        public string FamilyID { get; set; }

        /// <summary>
        /// Obtient ou définit une valeur indiquant si le passager nécessite deux places.
        /// </summary>
        public bool NeedsTwoSeats { get; set; }

        /// <summary>
        /// Obtient ou définit le siège attribué au passager dans l'avion.
        /// </summary>
        public Seat Seat { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructeur pour initialiser une nouvelle instance de la classe Passenger avec les détails spécifiés.
        /// </summary>
        /// <param name="type">Le type de passager (Adulte ou Enfant).</param>
        /// <param name="age">L'âge du passager.</param>
        /// <param name="familyID">L'identifiant de la famille à laquelle le passager appartient, ou "-" s'il n'appartient à aucune famille.</param>
        /// <param name="needsTwoSeats">Une valeur booléenne indiquant si le passager nécessite deux places.</param>
        public Passenger(PassengerType type, int age, string familyID, bool needsTwoSeats)
        {
            this.Type = type;
            this.Age = age;
            this.FamilyID = familyID;
            this.NeedsTwoSeats = needsTwoSeats;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vérifie si le passager enfant est accompagné par un adulte.
        /// </summary>
        /// <returns>True si le passager enfant est accompagné par un adulte, sinon False.</returns>
        public bool IsAccompaniedByAdult(Passenger adult)
        {
            if (Type == PassengerType.Enfant && Age < 12)
            {
                // Vérifier si l'adulte appartient à la même famille que l'enfant
                if (FamilyID == adult.FamilyID)
                {
                    return true;
                }
                else
                {
                    // L'enfant n'est pas accompagné par un adulte de la même famille
                    return false;
                }
            }
            else
            {
                // Ce n'est pas un enfant de moins de 12 ans, ou il n'est pas nécessairement accompagné
                return true;
            }
        }

        /// <summary>
        /// Calcule le prix du billet en fonction du type et de l'âge du passager.
        /// </summary>
        /// <returns>Le prix du billet en euros.</returns>
        public int CalculateTicketPrice()
        {
            if (Type == PassengerType.Adulte)
            {
                if (NeedsTwoSeats)
                {
                    return 500; // Prix pour un adulte nécessitant deux places
                }
                else
                {
                    return 250; // Prix pour un adulte
                }
            }
            else if (Type == PassengerType.Enfant && Age < 12)
            {
                return 150; // Prix pour un enfant de moins de 12 ans
            }
            else
            {
                throw new InvalidOperationException("Type de passager non pris en charge ou âge invalide.");
            }
        }

        #endregion
    }
}