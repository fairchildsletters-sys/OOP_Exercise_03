/****************/
// Mã sinh viên: [YOUR_STUDENT_ID]
// Họ tên:       [YOUR_FULL_NAME]
/****************/

using System;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Test execution driver executing the 15-step scenario specified in the requirements.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "OOP Assignment - Project Team and Personnel Verification";

            PrintHeader("VERIFICATION TEST SCENARIO (15 MANDATORY STEPS)");

            // Step 1: Create two Employee instances using different constructors
            PrintStep(1, "Instantiate two Employee instances via two distinct constructors");
            Employee emp1 = new Employee("EMP01", "Alice Smith", 3000.0);
            Employee emp2 = new Employee("EMP02", "Bob Jones"); // Overload defaulting base salary to 0.0
            emp1.DisplayInfo();
            emp2.DisplayInfo();

            // Step 2: Create two SoftwareEngineer instances using different constructors
            PrintStep(2, "Instantiate two SoftwareEngineer instances via two distinct constructors");
            SoftwareEngineer eng1 = new SoftwareEngineer("ENG01", "Charlie Brown", "C#"); // Base salary & allowance = 0
            SoftwareEngineer eng2 = new SoftwareEngineer("ENG02", "Diana Prince", 4500.0, "TypeScript", 1200.0);
            eng1.DisplayInfo();
            eng2.DisplayInfo();

            // Step 3: Increase salary of one personnel by a fixed amount
            PrintStep(3, "Increase salary of an employee (Bob Jones) by a fixed amount (£2,500.00)");
            Console.WriteLine($"[Before] Salary: £{emp2.BaseSalary:N2}");
            emp2.IncreaseSalary(2500.0);
            Console.WriteLine($"[After]  Salary: £{emp2.BaseSalary:N2}");

            // Step 4: Increase salary of another personnel by percentage
            PrintStep(4, "Increase salary of another personnel (Diana Prince) by percentage (10%)");
            Console.WriteLine($"[Before] Base: £{eng2.BaseSalary:N2} | Total Cost: £{eng2.CalculateMonthlyCost():N2}");
            eng2.IncreaseSalary(10.0, byPercentage: true);
            Console.WriteLine($"[After]  Base: £{eng2.BaseSalary:N2} | Total Cost: £{eng2.CalculateMonthlyCost():N2}");

            // Step 5: Create a project team without a leader
            PrintStep(5, "Create a ProjectTeam without an initial leader");
            ProjectTeam teamAlpha = new ProjectTeam("PRJ-ALPHA", "Cloud Migration Platform");
            Console.WriteLine($"Team initialised: Code = '{teamAlpha.ProjectCode}', Name = '{teamAlpha.ProjectName}', Leader = {(teamAlpha.Leader == null ? "None" : teamAlpha.Leader.FullName)}");

            // Step 6: Add a personnel to the team using AddMember(employee)
            PrintStep(6, "Add an employee (Alice Smith) to teamAlpha using AddMember(employee)");
            bool addedEmp1 = teamAlpha.AddMember(emp1);
            Console.WriteLine($"Addition status: {addedEmp1} | Total members: {teamAlpha.Members.Count}");

            // Step 7: Add an engineer using AddMember(employee, true) to appoint as leader
            PrintStep(7, "Add a software engineer (Diana Prince) using AddMember(employee, true) as Leader");
            bool addedEng2 = teamAlpha.AddMember(eng2, makeLeader: true);
            Console.WriteLine($"Operation status: {addedEng2}");
            Console.WriteLine($"Current Leader: {teamAlpha.Leader?.FullName} (ID: {teamAlpha.Leader?.Id})");
            Console.WriteLine($"Leader enrolled in members roster: {teamAlpha.Contains(eng2.Id)}");

            // Step 8: Attempt to re-add an already existing member (Duplicate prevention)
            PrintStep(8, "Attempt to re-add an existing member (Alice Smith) to verify invariant");
            bool duplicateAttempt = teamAlpha.AddMember(emp1);
            Console.WriteLine($"Re-addition result: {duplicateAttempt} (Expected: False - duplicate successfully rejected)");

            // Step 9: Display member roster via polymorphic invocation
            PrintStep(9, "Display full team roster demonstrating runtime polymorphic dispatch");
            teamAlpha.DisplayTeam();

            // Step 10: Compute total monthly personnel expenditure
            PrintStep(10, "Calculate total monthly personnel commitment for the team");
            double totalCost = teamAlpha.CalculateTotalMonthlyCost();
            Console.WriteLine($"Total monthly financial expenditure for teamAlpha: £{totalCost:N2}");

            // Step 11: Attempt to remove current leader and verify operation is rejected
            PrintStep(11, "Attempt to remove active leader (Diana Prince) to test invariant protection");
            bool removeLeaderAttempt = teamAlpha.RemoveMember("ENG02");
            Console.WriteLine($"Removal result: {removeLeaderAttempt} (Expected: False - active leader cannot be expunged)");
            Console.WriteLine($"Leader remains unchanged: {teamAlpha.Leader?.FullName}");

            // Step 12: Change leader, then remove the previous leader
            PrintStep(12, "Reassign leadership to Alice Smith, then expunge former leader (Diana Prince)");
            Console.WriteLine("[Action] Promoting Alice Smith to Leader...");
            teamAlpha.ChangeLeader(emp1);
            Console.WriteLine($"New appointed Leader: {teamAlpha.Leader?.FullName}");

            Console.WriteLine("[Action] Removing former leader Diana Prince (ENG02)...");
            bool removeFormerLeader = teamAlpha.RemoveMember("ENG02");
            Console.WriteLine($"Removal result: {removeFormerLeader} (Expected: True - successfully expunged)");
            Console.WriteLine($"Members count after removal: {teamAlpha.Members.Count}");

            // Step 13: Create a second team and add an employee already in team 1 (Aggregation proof)
            PrintStep(13, "Create teamBeta and allocate Alice Smith (shared with teamAlpha)");
            ProjectTeam teamBeta = new ProjectTeam("PRJ-BETA", "AI Analytics Engine");
            teamBeta.AddMember(emp1); // Alice Smith belongs to both teamAlpha and teamBeta
            teamBeta.AddMember(eng1);
            Console.WriteLine($"teamBeta members count: {teamBeta.Members.Count}");
            Console.WriteLine($"Alice Smith allocated to teamAlpha: {teamAlpha.Contains(emp1.Id)}");
            Console.WriteLine($"Alice Smith allocated to teamBeta:  {teamBeta.Contains(emp1.Id)}");

            // Step 14 & 15: Destroy second team via local scope and prove personnel survives
            PrintStep(14, "Destroy a secondary team by terminating a local execution scope");
            ExecuteLocalScopeTeamLifecycle(emp1);

            PrintStep(15, "Demonstrate personnel from destroyed team still survives intact in memory");
            Console.WriteLine("[Verification] Calling polymorphic DisplayInfo() on survivor personnel:");
            emp1.DisplayInfo();
            Console.WriteLine($"Integrity Check: Alice Smith's ID is '{emp1.Id}' and Base Salary is £{emp1.BaseSalary:N2}. Object is fully functional.");

            PrintHeader("ALL 15 VERIFICATION STEPS SUCCESSFULLY EXECUTED");
            Console.WriteLine("\nPress any key to trigger garbage collection and exit...");
            Console.ReadKey();

            // Trigger garbage collection to visually observe finalisers
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Isolated local scope used to demonstrate non-owning aggregation lifecycle semantics.
        /// </summary>
        private static void ExecuteLocalScopeTeamLifecycle(Employee sharedEmployee)
        {
            Console.WriteLine("  --> [Scope Entered] Creating transient ProjectTeam 'PRJ-GAMMA'...");
            ProjectTeam transientTeam = new ProjectTeam("PRJ-GAMMA", "Ephemeral Research Lab", sharedEmployee);
            Console.WriteLine($"  --> [Scope Active]  transientTeam created with leader: {transientTeam.Leader?.FullName}");
            Console.WriteLine("  --> [Scope Exiting] transientTeam goes out of scope and becomes eligible for GC.");
        }

        #region Console Formatting Helpers

        private static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('*', 80));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('*', 80));
            Console.ResetColor();
        }

        private static void PrintStep(int stepNumber, string description)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[STEP {stepNumber:D2}] {description}");
            Console.ResetColor();
        }

        #endregion
    }
}