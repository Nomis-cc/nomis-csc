namespace Nomis.CscExplorer.Interfaces.Models
{
    /// <summary>
    /// Csc wallet score.
    /// </summary>
    public class CscWalletScore
    {
        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public CscWalletStats? Stats { get; set; }
    }
}