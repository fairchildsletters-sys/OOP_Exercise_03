# Project Team and Staff Management System

An object-oriented programming (OOP) application implemented in C# (.NET), realising the principles of inheritance, polymorphism, non-owning aggregation, and specified business invariants.

---

## 1. Architecture and OOP Principles

1. **Inheritance:**
   * The `SoftwareEngineer` class publicly inherits from `Employee`, extending attributes for the primary programming language (`primaryLanguage`) and technical allowance (`technicalAllowance`).
2. **Polymorphism:**
   * Runtime method overriding for monthly cost calculation (`CalculateMonthlyCost()`) and information output (`DisplayInfo()`).
3. **Non-owning Aggregation:**
   * The `ProjectTeam` class maintains non-owning reference associations to `Employee` objects. The lifecycle of staff members remains entirely independent of the project team.
4. **Encapsulation and Model Invariants:**
   * Member uniqueness: Duplicate employee identifiers within the same team are strictly disallowed (`Contains()`).
   * Team leader integrity: The team leader must belong to the active member roster ($Leader \neq null \implies Leader \in Members$).
   * Leader preservation: Disallows the removal of an active team leader before appointing a designated successor.

---

## 2. Directory Structure

```text
.
├── Classes
│   ├── Employee.cs
│   ├── ProjectTeam.cs
│   └── SoftwareEngineer.cs
├── Test
│   └── Test.cs
├── OOP_Labw_03.csproj
├── Design.pdf
└── README.md

```

---

## 3. Build and Execution Guide

### Environment Prerequisites

* .NET SDK 6.0 or higher (or managed via the Pixi environment).

### Execution Commands

Using the standard .NET CLI:

```bash
dotnet run

```

Or via the Pixi environment:

```bash
pixi run dotnet run

```

---

## 4. Test Suite (15 Steps)

The verification suite in `Test.cs` automatically executes all 15 specification-compliant test procedures: validating constructor overloads, salary increments (fixed-amount and percentage-based), duplicate addition rejection, team leader protection mechanisms, multi-team aggregation, and confirming staff persistence following project team destruction.