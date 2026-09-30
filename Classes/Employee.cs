/****************/
// Mã sinh viên: [YOUR_STUDENT_ID]
// Họ tên:       [YOUR_FULL_NAME]
/****************/

using System;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Represents a base employee within the organisation.
    /// Manages essential identity, baseline salary, and provides polymorphic 
    /// contracts for cost calculation and information display.
    /// </summary>
    public class Employee
    {
        // Internal state backing fields (Encapsulation)
        private readonly string _id;
        private string _fullName;
        private double _baseSalary;

        #region Properties

        /// <summary>
        /// Gets the immutable unique identifier of the employee.
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Gets or sets the full name of the employee.
        /// Must not be null, empty, or consisting solely of white-space characters.
        /// </summary>
        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Full name must not be empty or white-space.", nameof(value));
                _fullName = value.Trim();
            }
        }

        /// <summary>
        /// Gets the baseline monthly salary of the employee.
        /// Guaranteed to be non-negative.
        /// </summary>
        public double BaseSalary => _baseSalary;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor initialising an employee with designated fallback values.
        /// </summary>
        public Employee() : this("UNKNOWN", "Unnamed employee", 0.0)
        {
        }

        /// <summary>
        /// Overloaded constructor initialising an employee with an ID and full name.
        /// The baseline salary defaults to 0.0.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="fullName">The full name of the employee.</param>
        public Employee(string id, string fullName) : this(id, fullName, 0.0)
        {
        }

        /// <summary>
        /// Comprehensive constructor initialising an employee with validation of invariants.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="fullName">The full name of the employee.</param>
        /// <param name="baseSalary">The baseline salary (must be non-negative).</param>
        public Employee(string id, string fullName, double baseSalary)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Employee ID must not be empty or white-space.", nameof(id));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name must not be empty or white-space.", nameof(fullName));

            if (baseSalary < 0.0)
                throw new ArgumentOutOfRangeException(nameof(baseSalary), "Base salary cannot be negative.");

            _id = id.Trim();
            _fullName = fullName.Trim();
            _baseSalary = baseSalary;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Increases the employee's baseline salary by a fixed numerical amount.
        /// </summary>
        /// <param name="amount">The fixed amount to add (must be strictly positive).</param>
        public void IncreaseSalary(double amount)
        {
            IncreaseSalary(amount, byPercentage: false);
        }

        /// <summary>
        /// Overloaded method to increase the baseline salary either by percentage or by fixed amount.
        /// </summary>
        /// <param name="value">The increment value (must be strictly positive).</param>
        /// <param name="byPercentage">True to increment by percentage; False for a fixed amount.</param>
        public void IncreaseSalary(double value, bool byPercentage)
        {
            if (value <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(value), "Salary increment value must be positive.");

            if (byPercentage)
            {
                _baseSalary += _baseSalary * (value / 100.0);
            }
            else
            {
                _baseSalary += value;
            }
        }

        /// <summary>
        /// Calculates the total monthly cost of this employee to the organisation.
        /// Virtual method intended for polymorphic overriding by derived specialisations.
        /// </summary>
        /// <returns>The monthly monetary cost.</returns>
        public virtual double CalculateMonthlyCost()
        {
            return _baseSalary;
        }

        /// <summary>
        /// Polymorphic display routine outputting personnel information to the standard console.
        /// </summary>
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"[Employee] ID: {_id,-8} | Name: {_fullName,-22} | Base Salary: £{_baseSalary,10:N2} | Total Cost: £{CalculateMonthlyCost(),10:N2}");
        }

        #endregion

        #region Destructor (Finalizer)

        /// <summary>
        /// Finaliser utilised to observe object lifecycle transitions during garbage collection.
        /// </summary>
        ~Employee()
        {
            Console.WriteLine($"[Lifecycle] Employee instance finalised: ID '{_id}', Name '{_fullName}'.");
        }

        #endregion
    }
}