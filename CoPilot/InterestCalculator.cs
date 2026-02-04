namespace CoPilot
{
    /// <summary>
    /// Provides methods for calculating interest rates and loan payments.
    /// </summary>
    public class InterestCalculator
    {
        /// <summary>
        /// Gets or sets the principal amount (loan amount or investment).
        /// </summary>
        public decimal PrincipalAmount { get; set; }

        /// <summary>
        /// Gets or sets the annual interest rate as a percentage (e.g., 5.5 for 5.5%).
        /// </summary>
        public decimal AnnualInterestRate { get; set; }

        /// <summary>
        /// Gets or sets the term in years.
        /// </summary>
        public int TermInYears { get; set; }

        /// <summary>
        /// Initializes a new instance of the InterestCalculator class.
        /// </summary>
        public InterestCalculator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the InterestCalculator class with specified values.
        /// </summary>
        /// <param name="principalAmount">The principal amount.</param>
        /// <param name="annualInterestRate">The annual interest rate as a percentage.</param>
        /// <param name="termInYears">The term in years.</param>
        public InterestCalculator(decimal principalAmount, decimal annualInterestRate, int termInYears)
        {
            PrincipalAmount = principalAmount;
            AnnualInterestRate = annualInterestRate;
            TermInYears = termInYears;
        }

        /// <summary>
        /// Calculates simple interest: I = P * r * t
        /// </summary>
        /// <returns>The simple interest amount.</returns>
        public decimal CalculateSimpleInterest()
        {
            return PrincipalAmount * (AnnualInterestRate / 100) * TermInYears;
        }

        /// <summary>
        /// Calculates the total amount with simple interest: A = P + I
        /// </summary>
        /// <returns>The total amount (principal + simple interest).</returns>
        public decimal CalculateSimpleInterestTotal()
        {
            return PrincipalAmount + CalculateSimpleInterest();
        }

        /// <summary>
        /// Calculates compound interest: A = P(1 + r/n)^(nt)
        /// </summary>
        /// <param name="compoundingFrequency">Number of times interest is compounded per year (e.g., 12 for monthly, 4 for quarterly).</param>
        /// <returns>The total amount with compound interest.</returns>
        public decimal CalculateCompoundInterest(int compoundingFrequency = 12)
        {
            if (compoundingFrequency <= 0)
                throw new ArgumentException("Compounding frequency must be greater than 0.", nameof(compoundingFrequency));

            double p = (double)PrincipalAmount;
            double r = (double)AnnualInterestRate / 100;
            double n = compoundingFrequency;
            double t = TermInYears;

            double amount = p * Math.Pow(1 + (r / n), n * t);
            return (decimal)amount;
        }

        /// <summary>
        /// Calculates the compound interest earned (total - principal).
        /// </summary>
        /// <param name="compoundingFrequency">Number of times interest is compounded per year.</param>
        /// <returns>The compound interest earned.</returns>
        public decimal CalculateCompoundInterestEarned(int compoundingFrequency = 12)
        {
            return CalculateCompoundInterest(compoundingFrequency) - PrincipalAmount;
        }

        /// <summary>
        /// Calculates the monthly payment for a loan using the amortization formula:
        /// M = P * [r(1+r)^n] / [(1+r)^n - 1]
        /// </summary>
        /// <returns>The monthly payment amount.</returns>
        public decimal CalculateMonthlyPayment()
        {
            if (AnnualInterestRate == 0)
                return PrincipalAmount / (TermInYears * 12);

            double p = (double)PrincipalAmount;
            double r = (double)AnnualInterestRate / 100 / 12; // Monthly interest rate
            double n = TermInYears * 12; // Total number of payments

            double monthlyPayment = p * (r * Math.Pow(1 + r, n)) / (Math.Pow(1 + r, n) - 1);
            return (decimal)monthlyPayment;
        }

        /// <summary>
        /// Calculates the total amount paid over the life of the loan.
        /// </summary>
        /// <returns>The total amount paid (monthly payment * number of payments).</returns>
        public decimal CalculateTotalPayment()
        {
            return CalculateMonthlyPayment() * TermInYears * 12;
        }

        /// <summary>
        /// Calculates the total interest paid over the life of the loan.
        /// </summary>
        /// <returns>The total interest paid (total payment - principal).</returns>
        public decimal CalculateTotalInterestPaid()
        {
            return CalculateTotalPayment() - PrincipalAmount;
        }

        /// <summary>
        /// Calculates the effective annual rate (EAR) for compound interest:
        /// EAR = (1 + r/n)^n - 1
        /// </summary>
        /// <param name="compoundingFrequency">Number of times interest is compounded per year.</param>
        /// <returns>The effective annual rate as a percentage.</returns>
        public decimal CalculateEffectiveAnnualRate(int compoundingFrequency = 12)
        {
            if (compoundingFrequency <= 0)
                throw new ArgumentException("Compounding frequency must be greater than 0.", nameof(compoundingFrequency));

            double r = (double)AnnualInterestRate / 100;
            double n = compoundingFrequency;

            double ear = Math.Pow(1 + (r / n), n) - 1;
            return (decimal)(ear * 100);
        }

        /// <summary>
        /// Returns a summary of all calculations.
        /// </summary>
        /// <returns>A formatted string with calculation results.</returns>
        public string GetSummary()
        {
            return $"Principal: {PrincipalAmount:C}\n" +
                   $"Annual Interest Rate: {AnnualInterestRate:F2}%\n" +
                   $"Term: {TermInYears} years\n" +
                   $"Simple Interest: {CalculateSimpleInterest():C}\n" +
                   $"Simple Interest Total: {CalculateSimpleInterestTotal():C}\n" +
                   $"Compound Interest (Monthly): {CalculateCompoundInterest():C}\n" +
                   $"Monthly Payment: {CalculateMonthlyPayment():C}\n" +
                   $"Total Payment: {CalculateTotalPayment():C}\n" +
                   $"Total Interest Paid: {CalculateTotalInterestPaid():C}";
        }

        /// <summary>
        /// Validates whether all required fields have valid values.
        /// </summary>
        /// <returns>True if all fields are valid; otherwise, false.</returns>
        public bool IsValid()
        {
            return PrincipalAmount > 0 &&
                   AnnualInterestRate >= 0 &&
                   TermInYears > 0;
        }
    }
}
