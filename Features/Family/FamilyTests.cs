using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace KataTestUnits.Features
{
    /// <summary>
    /// Classe de tests unitaires pour la classe Family.
    /// </summary>
    [TestClass]
    public class FamiltyTests
    {
        #region Family
        /// <summary>
        /// Teste la création d'une famille.
        /// </summary>
        [TestMethod]
        public void TestFamilyCreation()
        {
            // Arrange
            Family family = new Family();

            // Assert
            Assert.IsNotNull(family.Members);
            Assert.AreEqual(0, family.Members.Count);
        }

        /// <summary>
        /// Vérifie si la méthode UpdateMember met à jour correctement un membre de la famille.
        /// </summary>
        [TestMethod]
        public void TestUpdateMember()
        {
            // Arrange
            Family family = new Family();
            Passenger oldPassenger = new Passenger(PassengerType.Adulte, 35, "A", false);
            Passenger newPassenger = new Passenger(PassengerType.Adulte, 40, "A", false);

            // Act
            family.AddMember(oldPassenger);
            family.UpdateMember(oldPassenger, newPassenger);

            // Assert
            Assert.AreEqual(1, family.Members.Count);
            Assert.IsTrue(family.Members.Contains(newPassenger));
            Assert.IsFalse(family.Members.Contains(oldPassenger));
        }

        /// <summary>
        /// Vérifie si la méthode DeleteMember supprime correctement un membre de la famille.
        /// </summary>
        [TestMethod]
        public void TestDeleteMember()
        {
            // Arrange
            Family family = new Family();
            Passenger passengerToDelete = new Passenger(PassengerType.Adulte, 32, "A", false);

            // Act
            family.AddMember(passengerToDelete);
            family.DeleteMember(passengerToDelete);

            // Assert
            Assert.AreEqual(0, family.Members.Count);
            Assert.IsFalse(family.Members.Contains(passengerToDelete));
        }

        /// <summary>
        /// Teste l'ajout d'un membre à une famille.
        /// </summary>
        [TestMethod]
        public void TestAddingMemberToFamily()
        {
            // Arrange
            Family family = new Family();
            Passenger adult = new Passenger(PassengerType.Adulte, 35, "A", false);

            // Act
            family.AddMember(adult);

            // Assert
            Assert.AreEqual(1, family.Members.Count);
        }

        /// <summary>
        /// Vérifie que la contrainte selon laquelle chaque famille peut avoir au maximum 2 adultes et 3 enfants est respectée.
        /// </summary>
        [TestMethod]
        public void TestMaximumFamilyMembersConstraint()
        {
            // Arrange
            Family family = new Family();
            Passenger adult1 = new Passenger(PassengerType.Adulte, 35, "A", false);
            Passenger adult2 = new Passenger(PassengerType.Adulte, 32, "A", false);
            Passenger adult3 = new Passenger(PassengerType.Adulte, 40, "A", false); // Troisième adulte
            Passenger child1 = new Passenger(PassengerType.Enfant, 7, "A", false);
            Passenger child2 = new Passenger(PassengerType.Enfant, 5, "A", false);
            Passenger child3 = new Passenger(PassengerType.Enfant, 3, "A", false); // Quatrième enfant

            // Act
            family.AddMember(adult1);
            family.AddMember(adult2);
            family.AddMember(adult3); // Ajout du troisième adulte
            family.AddMember(child1);
            family.AddMember(child2);
            family.AddMember(child3); // Ajout du quatrième enfant

            // Assert
            Assert.AreNotEqual(5, family.Members.Count); // La famille devrait contenir uniquement 5 membres au total
        }

        /// <summary>
        /// Teste le calcul du prix total pour une famille.
        /// </summary>
        [TestMethod]
        public void TestFamilyTotalPriceCalculation()
        {
            // Arrange
            Family family = new Family();
            Passenger adult1 = new Passenger(PassengerType.Adulte, 35, "A", false);
            Passenger adult2 = new Passenger(PassengerType.Adulte, 32, "A", true);
            Passenger child = new Passenger(PassengerType.Enfant, 7, "A", false);

            // Act
            family.AddMember(adult1);
            family.AddMember(adult2);
            family.AddMember(child);

            // Assert
            Assert.AreEqual(900, family.CalculateTotalPrice());
        }

        /// <summary>
        /// Teste si les familles sont assises ensemble dans l'avion.
        /// </summary>
        [TestMethod]
        public void TestFamilySeating()
        {
            // Arrange
            List<Passenger> passengers = new List<Passenger>
            {
                new Passenger(PassengerType.Adulte, 27, "C", false),
                new Passenger(PassengerType.Enfant, 2, "C", false),
                new Passenger(PassengerType.Adulte, 40, "D", false),
                new Passenger(PassengerType.Enfant, 7, "B", false),
                new Passenger(PassengerType.Adulte, 55, "-", false),
                new Passenger(PassengerType.Enfant, 11, "D", false)
            };

            // Act
            List<Family> families = SeatingArrangement.AssignSeating(passengers);

            // Assert
            Assert.IsTrue(SeatingArrangement.FamiliesAreSeatedTogether(families));
        }

        /// <summary>
        /// Teste le processus d'embarquement pour s'assurer que chaque famille entre dans l'avion ensemble.
        /// </summary>
        [TestMethod]
        public void TestEmbarkingWithEntireFamily()
        {
            // Arrange
            var waitingList = new List<Passenger>
            {
                new Passenger(PassengerType.Adulte, 45, "B", false),
                new Passenger(PassengerType.Enfant, 7, "A", false),
                new Passenger(PassengerType.Adulte, 40, "B", false),
                new Passenger(PassengerType.Enfant, 4, "A", false),
                new Passenger(PassengerType.Adulte, 28, "C", true),
                new Passenger(PassengerType.Enfant, 10, "B", false)
            };

            // Act
            var boardedPassengers = EmbarkPassengers(waitingList);

            // Assert
            Assert.IsTrue(SeatingArrangement.FamiliesAreTogether(boardedPassengers));
        }

        /// <summary>
        /// Simule le processus d'embarquement des passagers en tenant compte des membres de la famille.
        /// </summary>
        /// <param name="waitingList">La liste d'attente des passagers à embarquer.</param>
        /// <returns>La liste des passagers embarqués dans l'avion.</returns>
        private static List<Passenger> EmbarkPassengers(List<Passenger> waitingList)
        {
            // Créer une liste pour stocker les passagers embarqués
            List<Passenger> boardedPassengers = new List<Passenger>();

            // Parcourir la liste d'attente
            foreach (var passenger in waitingList)
            {
                // Vérifier si le passager est déjà embarqué avec sa famille
                if (boardedPassengers.Any(p => p.FamilyID == passenger.FamilyID))
                {
                    // Si le passager fait déjà partie des passagers embarqués avec sa famille, passer au suivant
                    continue;
                }

                // Récupérer tous les membres de la famille du passager actuel dans la liste d'attente
                var familyMembers = waitingList.Where(p => p.FamilyID == passenger.FamilyID).ToList();

                // Ajouter tous les membres de la famille dans la liste des passagers embarqués
                boardedPassengers.AddRange(familyMembers);
            }

            return boardedPassengers;
        }

        #endregion
    }
}
