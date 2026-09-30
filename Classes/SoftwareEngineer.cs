/****************/
// Mã sinh viên: [YOUR_STUDENT_ID]
// Họ tên:       [YOUR_FULL_NAME]
/****************/

using System;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Represents a specialised software engineering practitioner.
    /// Inherits publicly from Employee, augmenting technical competencies and allowances.
    /// </summary>
    public class SoftwareEngineer : Employee
    {
        private string _primaryLanguage;
        private double _technicalAllowance;

        #region Properties

        /// <summary>
        /// Gets or sets the primary programming language expertise.
        /// </summary>
        public string PrimaryLanguage
        {
            get => _primaryLanguage;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Primary programming language must not be empty.", nameof(value));
                _primaryLanguage = value.Trim();
            }
        }

        /// <summary>
        /// Gets the additional monthly technical allowance granted for specialised skills.
        /// </summary>
        public double TechnicalAllowance => _technicalAllowance;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a SoftwareEngineer with default base salary and technical allowance of 0.0.
        /// </summary>
        /// <param name="id">The unique employee identifier.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="primaryLanguage">The primary programming language.</param>
        public SoftwareEngineer(string id, string fullName, string primaryLanguage)
            : this(id, fullName, 0.0, primaryLanguage, 0.0)
        {
        }

        /// <summary>
        /// Comprehensive constructor initialising both base parameters and technical additions.
        /// </summary>
        /// <param name="id">The unique employee identifier.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="baseSalary">The baseline salary.</param>
        /// <param name="primaryLanguage">The primary programming language.</param>
        /// <param name="technicalAllowance">The technical allowance (must be non-negative).</param>
        public SoftwareEngineer(string id, string fullName, double baseSalary, string primaryLanguage, double technicalAllowance)
            : base(id, fullName, baseSalary)
        {
            if (string.IsNullOrWhiteSpace(primaryLanguage))
                throw new ArgumentException("Primary language must not be empty or white-space.", nameof(primaryLanguage));

            if (technicalAllowance < 0.0)
                throw new ArgumentOutOfRangeException(nameof(technicalAllowance), "Technical allowance cannot be negative.");

            _primaryLanguage = primaryLanguage.Trim();
            _technicalAllowance = technicalAllowance;
        }

        #endregion

        #region Polymorphic Overrides

        /// <summary>
        /// Polymorphically computes monthly expenditure, factoring in both baseline remuneration 
        /// and dedicated technical allowances.
        /// </summary>
        /// <returns>The cumulative monthly monetary cost.</returns>
        public override double CalculateMonthlyCost()
        {
            return BaseSalary + _technicalAllowance;
        }

        /// <summary>
        /// Polymorphically displays specialised engineering profile data.
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"[Engineer] ID: {Id,-8} | Name: {FullName,-22} | Base: £{BaseSalary,10:N2} | Allowance: £{_technicalAllowance,8:N2} | Language: {_primaryLanguage,-10} | Total Cost: £{CalculateMonthlyCost(),10:N2}");
        }

        #endregion

        #region Destructor (Finalizer)

        /// <summary>
        /// Finaliser logging lifecycle destruction of specialised engineering instances.
        /// </summary>
        ~SoftwareEngineer()
        {
            Console.WriteLine($"[Lifecycle] SoftwareEngineer instance finalised: ID '{Id}', Language '{_primaryLanguage}'.");
        }

        #endregion
    }
}