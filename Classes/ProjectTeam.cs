/****************/
// Mã sinh viên: 202418838
// Họ tên:       Bùi Tuấn Anh
/****************/

using System;
using System.Collections.Generic;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Represents a project team aggregate managing personnel allocation.
    /// Establishes non-owning aggregation relationships with Employee instances,
    /// enforcing strict identity uniqueness and leadership lifecycle invariants.
    /// </summary>
    public class ProjectTeam
    {
        private readonly string _projectCode;
        private string _projectName;
        private Employee? _leader;
        private readonly List<Employee> _members;

        #region Properties

        /// <summary>
        /// Gets the immutable unique project code.
        /// </summary>
        public string ProjectCode => _projectCode;

        /// <summary>
        /// Gets or sets the project designation name.
        /// </summary>
        public string ProjectName
        {
            get => _projectName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Project name must not be empty or white-space.", nameof(value));
                _projectName = value.Trim();
            }
        }

        /// <summary>
        /// Gets the designated team leader.
        /// </summary>
        public Employee? Leader => _leader;

        /// <summary>
        /// Exposes a read-only view of allocated members to preserve internal encapsulation.
        /// </summary>
        public IReadOnlyList<Employee> Members => _members.AsReadOnly();

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises an empty project team without an initially designated leader.
        /// </summary>
        /// <param name="projectCode">The unique identifier code for the project.</param>
        /// <param name="projectName">The name of the project.</param>
        public ProjectTeam(string projectCode, string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectCode))
                throw new ArgumentException("Project code must not be empty or white-space.", nameof(projectCode));

            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException("Project name must not be empty or white-space.", nameof(projectName));

            _projectCode = projectCode.Trim();
            _projectName = projectName.Trim();
            _leader = null;
            _members = new List<Employee>();
        }

        /// <summary>
        /// Initialises a project team with a leader, automatically enrolling the leader into the member roster.
        /// </summary>
        /// <param name="projectCode">The unique project code.</param>
        /// <param name="projectName">The name of the project.</param>
        /// <param name="leader">The appointed team leader.</param>
        public ProjectTeam(string projectCode, string projectName, Employee leader)
            : this(projectCode, projectName)
        {
            if (leader == null)
                throw new ArgumentNullException(nameof(leader), "Initial team leader cannot be null.");

            ChangeLeader(leader);
        }

        #endregion

        #region Member and Leadership Management

        /// <summary>
        /// Verifies whether an employee with the specified identifier is allocated to this team.
        /// </summary>
        /// <param name="employeeId">The identifier string to query.</param>
        /// <returns>True if present; otherwise, False.</returns>
        public bool Contains(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                return false;

            for (int i = 0; i < _members.Count; i++)
            {
                if (string.Equals(_members[i].Id, employeeId.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a personnel instance to the team roster.
        /// Guarantees that duplicate identifiers are rejected.
        /// </summary>
        /// <param name="employee">The employee instance to allocate.</param>
        /// <returns>True if added successfully; False if already present.</returns>
        public bool AddMember(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Cannot add a null employee reference.");

            if (Contains(employee.Id))
                return false;

            _members.Add(employee);
            return true;
        }

        /// <summary>
        /// Overloaded addition method allowing immediate promotion to team leader.
        /// </summary>
        /// <param name="employee">The employee instance to allocate.</param>
        /// <param name="makeLeader">If true, designates the employee as team leader.</param>
        /// <returns>True if operation succeeded; False if duplicate addition occurred without leadership change.</returns>
        public bool AddMember(Employee employee, bool makeLeader)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Cannot add a null employee reference.");

            if (makeLeader)
            {
                ChangeLeader(employee);
                return true;
            }

            return AddMember(employee);
        }

        /// <summary>
        /// Removes an employee from the team roster.
        /// Enforces the invariant: an active leader cannot be expunged without prior replacement.
        /// </summary>
        /// <param name="employeeId">The identifier of the employee to remove.</param>
        /// <returns>True if removal succeeded; False if rejected or not found.</returns>
        public bool RemoveMember(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                return false;

            // Invariant guard: An active leader cannot be expunged
            if (_leader != null && string.Equals(_leader.Id, employeeId.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            for (int i = 0; i < _members.Count; i++)
            {
                if (string.Equals(_members[i].Id, employeeId.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    _members.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Reassigns leadership to a new candidate.
        /// Automatically enrolls the individual into the team roster if not previously affiliated.
        /// </summary>
        /// <param name="newLeader">The employee assuming leadership.</param>
        public void ChangeLeader(Employee newLeader)
        {
            if (newLeader == null)
                throw new ArgumentNullException(nameof(newLeader), "New leader cannot be null.");

            // Invariant guard: Leader must be an active member of the project roster
            if (!Contains(newLeader.Id))
            {
                _members.Add(newLeader);
            }

            _leader = newLeader;
        }

        #endregion

        #region Financial and Reporting Services

        /// <summary>
        /// Calculates the cumulative monthly cost of all allocated members via polymorphic dispatch.
        /// </summary>
        /// <returns>The aggregated monthly financial commitment.</returns>
        public double CalculateTotalMonthlyCost()
        {
            double totalCost = 0.0;
            for (int i = 0; i < _members.Count; i++)
            {
                totalCost += _members[i].CalculateMonthlyCost();
            }
            return totalCost;
        }

        /// <summary>
        /// Displays comprehensive team structure, detailing leadership and individual member profiles.
        /// </summary>
        public void DisplayTeam()
        {
            Console.WriteLine(new string('=', 95));
            Console.WriteLine($"PROJECT TEAM: [{_projectCode}] {_projectName.ToUpper()}");
            Console.WriteLine($"CURRENT LEADER: {(_leader != null ? $"{_leader.FullName} (ID: {_leader.Id})" : "NONE APPOINTED")}");
            Console.WriteLine($"ALLOCATED MEMBERS ({_members.Count}):");
            Console.WriteLine(new string('-', 95));

            for (int i = 0; i < _members.Count; i++)
            {
                Console.Write($"  {i + 1,2}. ");
                _members[i].DisplayInfo();
            }

            Console.WriteLine(new string('-', 95));
            Console.WriteLine($"TOTAL MONTHLY COMMITMENT: £{CalculateTotalMonthlyCost(),10:N2}");
            Console.WriteLine(new string('=', 95));
        }

        #endregion

        #region Destructor (Finalizer)

        /// <summary>
        /// Finaliser releasing internal linkage structures whilst preserving aggregated Employee instances.
        /// </summary>
        ~ProjectTeam()
        {
            _members.Clear();
            Console.WriteLine($"[Lifecycle] ProjectTeam '{_projectCode}' finalised. Aggregated personnel references cleared.");
        }

        #endregion
    }
}