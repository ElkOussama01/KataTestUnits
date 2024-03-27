using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KataTestUnits.Features
{
    /// <summary>
    /// Classe de tests unitaires pour la classe Passenger
    /// </summary>
    [TestClass]
    public class PassengerTests
    {
        #region Passenger

        /// <summary>
        /// Teste la création d'un passager adulte.
        /// </summary>
        [TestMethod]
        public void TestAdultPassengerCreation()
        {
            // Arrange
            Passenger adult = new Passenger(PassengerType.Adulte, 35, "A", false);

            // Assert
            Assert.AreEqual(PassengerType.Adulte, adult.Type);
            Assert.AreEqual(35, adult.Age);
            Assert.AreEqual("A", adult.FamilyID);
            Assert.IsFalse(adult.NeedsTwoSeats);
        }

        /// <summary>
        /// Vérifie que la création d'un adulte nécessitant deux places fonctionne correctement.
        /// </summary>
        [TestMethod]
        public void TestAdultRequiringTwoSeatsCreation()
        {
            // Arrange
            Passenger adultRequiringTwoSeats = new Passenger(PassengerType.Adulte, 28, "C", true);

            // Assert
            Assert.IsTrue(adultRequiringTwoSeats.NeedsTwoSeats);
        }

        // <summary>
        /// Teste le calcul du prix du billet pour un adulte.
        /// </summary>
        [TestMethod]
        public void TestAdultTicketPrice()
        {
            // Arrange
            var adult = new Passenger(PassengerType.Adulte, 27, "C", false);

            // Assert
            Assert.AreEqual(250, adult.CalculateTicketPrice());
        }

        /// <summary>
        /// Teste le calcul du prix du billet pour un enfant.
        /// </summary>
        [TestMethod]
        public void TestChildTicketPrice()
        {
            // Arrange
            var child = new Passenger(PassengerType.Enfant, 11, "D", false);

            // Assert
            Assert.AreEqual(150, child.CalculateTicketPrice());
        }

        /// <summary>
        /// Teste le calcul du prix du billet pour un adulte nécessitant deux places.
        /// </summary>
        [TestMethod]
        public void TestAdultRequiringTwoSeatsTicketPrice()
        {
            // Arrange
            var adultRequiringTwoSeats = new Passenger(PassengerType.Adulte, 28, "A", true);

            // Assert
            Assert.AreEqual(500, adultRequiringTwoSeats.CalculateTicketPrice());
        }

        #endregion
    }
}
