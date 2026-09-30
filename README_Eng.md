# Project Team and Personnel Management System

An Object-Oriented Programming (OOP) implementation in C# (.NET) demonstrating core principles, domain invariants, and non-owning aggregation lifecycle semantics.

---

## 🏗️ Architecture & Key OOP Principles

1. **Inheritance & Specialisation:**
   * `SoftwareEngineer` publicly inherits from `Employee`, augmenting technical competencies (`primaryLanguage`) and remuneration (`technicalAllowance`).
2. **Polymorphism (Dynamic Dispatch):**
   * Runtime polymorphic overrides for `CalculateMonthlyCost()` and `DisplayInfo()`.
3. **Non-Owning Aggregation:**
   * `ProjectTeam` maintains non-owning references to `Employee` instances. Personnel lifecycles remain completely independent of project lifecycles.
4. **Encapsulation & Domain Invariants:**
   * Member identity uniqueness (`Contains()`).
   * Team leader integrity ($Leader \neq null \implies Leader \in Members$).
   * Active leader retention guard (cannot remove active leader without prior succession).

---

## 📁 Project Structure

```text
.
├── Classes
│   ├── Employee.cs
│   ├── ProjectTeam.cs
│   └── SoftwareEngineer.cs
├── OOP_Exercise_03.csproj
├── README.md
└── Test.cs
```

---

## 🚀 How to Build and Run

### Prerequisites
* .NET SDK 6.0+ (or managed via [Pixi](https://pixi.sh))

### Execution
Run via standard .NET CLI:
```bash
dotnet run
```
Or via Pixi environment:
```bash
pixi run dotnet run
```

---

## 🧪 Verification Test Scenario (15 Steps)
The application automatically executes a 15-step verification suite in `Test.cs`, proving constructor overloading, salary increments, duplicate rejections, leader protection, multi-project aggregation, and memory decoupling upon scope termination.