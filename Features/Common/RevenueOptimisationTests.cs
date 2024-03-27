using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace KataTestUnits.Features.Common
{
    /// <summary>
    /// Classe de tests unitaires pour vérifier l'optimisation du chiffre d'affaires.
    /// </summary>
    [TestClass]
    public class RevenueOptimisationTests
    {
        #region Test methods

        /// <summary>
        /// Teste l'optimisation du chiffre d'affaires.
        /// </summary>
        [TestMethod]
        public void TestRevenueOptimization()
        {
            // Arrange
            var passengers = GeneratePassengerList(); // Générer une liste de passagers avec différentes configurations
            var airplane = new Airplane(); // Créer une instance de l'avion
            var expectedRevenue = CalculateExpectedRevenue(passengers); // Calculer le chiffre d'affaires attendu

            // Act
            var assignedPassengers = AirplaneSeating.AssignPassengers(passengers, airplane); // Affecter les passagers à l'avion
            var actualRevenue = CalculateTotalRevenue(assignedPassengers); // Calculer le chiffre d'affaires réel

            // Assert
            Assert.IsTrue(actualRevenue >= expectedRevenue, $"Le chiffre d'affaires réalisé ({actualRevenue}) doit être supérieur ou égal au chiffre d'affaires attendu ({expectedRevenue}).");
        }

        /// <summary>
        /// Méthode pour calculer le chiffre d'affaires total à partir de la liste des passagers embarqués.
        /// </summary>
        /// <param name="passengers">La liste des passagers embarqués dans l'avion.</param>
        /// <returns>Le chiffre d'affaires total généré par les passagers embarqués.</returns>
        public static decimal CalculateTotalRevenue(List<Passenger> passengers)
        {
            decimal totalRevenue = 0;

            foreach (var passenger in passengers)
            {
                // Calcul du prix du billet pour chaque passager
                decimal ticketPrice = passenger.CalculateTicketPrice();

                // Ajout du prix du billet au chiffre d'affaires total
                totalRevenue += ticketPrice;
            }

            return totalRevenue;
        }


        #endregion

        /// <summary>
        /// Méthode de calcul du chiffre d'affaires attendu pour les tests.
        /// </summary>
        /// <param name="passengers">La liste des passagers à considérer.</param>
        /// <returns>Le chiffre d'affaires attendu pour la liste de passagers donnée.</returns>
        private static decimal CalculateExpectedRevenue(List<Passenger> passengers)
        {
            decimal totalRevenue = 0;

            foreach (var passenger in passengers)
            {
                // Calcule le prix du billet pour chaque passager en fonction de son type et s'il nécessite deux places
                decimal ticketPrice = passenger.Type == PassengerType.Adulte ? 250 :
                                      passenger.Type == PassengerType.Enfant ? 150 : 500;

                // Ajoute le prix du billet au chiffre d'affaires total
                totalRevenue += ticketPrice;
            }

            return totalRevenue;
        }


        /// <summary>
        /// Méthode de génération de la liste de passagers pour les tests.
        /// </summary>
        /// <returns>Une liste de passagers pour les tests.</returns>
        private static List<Passenger> GeneratePassengerList()
        {
            // Crée une nouvelle liste de passagers
            var passengers = new List<Passenger>();

            // Ajout de quelques passagers pour les tests
            // Adulte seul
            passengers.Add(new Passenger(PassengerType.Adulte, 21, "R", false));
            // Enfant seul
            passengers.Add(new Passenger(PassengerType.Enfant, 6, "R", false));
            // Adulte seul
            passengers.Add(new Passenger(PassengerType.Adulte, 24, "R", false));
            // Enfant seul
            passengers.Add(new Passenger(PassengerType.Enfant, 7, "O", false));
            // Adulte seul
            passengers.Add(new Passenger(PassengerType.Adulte, 46, "Q", false));
            // Enfant seul
            passengers.Add(new Passenger(PassengerType.Enfant, 10, "Q", false));

            // Ajout d'une famille avec deux adultes et deux enfants
            var familyAAdult1 = new Passenger(PassengerType.Adulte, 33, "P", false);
            var familyAAdult2 = new Passenger(PassengerType.Adulte, 37, "P", false);
            var familyAChild1 = new Passenger(PassengerType.Enfant, 3, "P", false);
            var familyAChild2 = new Passenger(PassengerType.Enfant, 11, "P", false);
            passengers.Add(familyAAdult1);
            passengers.Add(familyAAdult2);
            passengers.Add(familyAChild1);
            passengers.Add(familyAChild2);

            // Ajout d'une famille avec un adulte nécessitant deux places et un enfant
            var familyBAdult = new Passenger(PassengerType.Adulte, 28, "C", true);
            var familyBChild = new Passenger(PassengerType.Enfant, 2, "C", false);
            passengers.Add(familyBAdult);
            passengers.Add(familyBChild);

            // Retourne la liste générée de passagers
            return passengers;
        }
    }
}
