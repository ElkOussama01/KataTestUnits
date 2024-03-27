namespace KataTestUnits
{
    /// <summary>
    /// Classe Seat qui représente un siège dans l'avion.
    /// </summary>
    public class Seat
    {
        #region Properties

        /// <summary>
        /// Obtient ou définit le numéro de rangée du siège.
        /// </summary>
        public int Row;

        /// <summary>
        /// Obtient ou définit le numéro de siège dans la rangée.
        /// </summary>
        public int Number;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructeur qui initialise une nouvelle instance de la classe Seat avec les numéros de rangée et de siège spécifiés.
        /// </summary>
        /// <param name="row">Le numéro de rangée du siège.</param>
        /// <param name="number">Le numéro de siège dans la rangée.</param>
        public Seat(int row, int number)
        {
            this.Row = row;
            this.Number = number;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retourne une représentation textuelle du siège sous la forme "Row X, Seat Y".
        /// </summary>
        /// <returns>La représentation textuelle du siège.</returns>
        public override string ToString()
        {
            return $"Row {Row}, Seat {Number}";
        }

        #endregion
    }
}