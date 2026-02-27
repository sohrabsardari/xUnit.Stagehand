# xUnit.Stagehand
![CI Status](https://github.com/sohrabsardari/xUnit.Stagehand/actions/workflows/ci.yml/badge.svg)


# xUnit.Stagehand 🎭

**xUnit.Stagehand** is a lightweight extension for xUnit designed to solve the "Arrange" mess in complex testing scenarios. It allows you to create **reusable, returnable, and parameterized** Arrange stages that maintain the full xUnit test lifecycle.

## 🚀 Why Stagehand?

In standard xUnit, "Arrange" setup is often hidden in private helper methods or duplicated across tests. These private methods don't show up in test reports and can't easily use `InlineData` or `MemberData`.

**Stagehand** turns your setup logic into first-class citizens:

* **Return Values:** Your setup methods can return the objects you need (SUT, DTOs, Builders).
* **Report Visibility:** Every "Arrange" stage is executed and reported by the xUnit runner.
* **Data-Driven:** Use `InlineData` and `MemberData` directly on your setup stages.

---

## 📦 Installation

Install the package via NuGet:

```bash
dotnet add package xUnit.Stagehand

```

---

## 🛠 Usage

### 1. Basic Setup with `[ArrangeFact]`

Use `[ArrangeFact]` when you have a standard setup that you want to reuse across multiple tests.

```csharp
public class BankAccountTests
{
    // STAGE: Validates that a standard active account can be created.
    [ArrangeFact]
    public TestBankAccount Arrange_StandardActiveAccount()
    {
        var account = new TestBankAccountBuilder()
            .WithAccountNumber("ACC-123")
            .WithBalance(200m)
            .Build();

        // Self-validation: The "Simple Scenario" test
        Assert.Equal(200m, account.Balance);
        Assert.True(account.IsActive);
        
        return account;
    }

    [Fact]
    public void Act_Deposit_ShouldIncreaseBalance()
    {
        // Reuse the verified stage
        var sut = Arrange_StandardActiveAccount();

        sut.Deposit(100m);

        Assert.Equal(300m, sut.Balance);
    }
}
```

### 2. Parameterized Setup with `[ArrangeTheory]`

Use `[ArrangeTheory]` when your setup depends on external data. It works seamlessly with `[InlineData]` and `[MemberData]`.

```csharp
public class BankAccountTheoryTests
{
    // STAGE: Validates that accounts are created correctly for various balances.
    [ArrangeTheory]
    [InlineData(100)]
    [InlineData(500)]
    public TestBankAccount Arrange_AccountWithBalance(decimal initialBalance)
    {
        var account = new TestBankAccountBuilder()
            .WithBalance(initialBalance)
            .Build();

        // Self-validation: The "Simple Scenario" test for each data row
        Assert.Equal(initialBalance, account.Balance);
        
        return account;
    }

    [Theory]
    [InlineData(100)]
    [InlineData(500)]
    public void Act_Withdraw_ShouldDecreaseBalance(decimal initialBalance)
    {
        // Arrange: Call the parameterized stage
        var sut = Arrange_AccountWithBalance(initialBalance);

        sut.Withdraw(50m);

        Assert.Equal(initialBalance - 50m, sut.Balance);
    }
}

```

---

## 🧩 Compatibility

* **.NET 6.0, 8.0, 9.0, 10.0**
* **.NET Standard 2.0**
* **xUnit 2.x**

---

## 📜 License

This project is licensed under the **MIT License**.
