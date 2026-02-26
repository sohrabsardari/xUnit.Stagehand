namespace xUnit.Stagehand.Tests;

public class BankAccountTheoryTests
{
    [ArrangeTheory]
    [InlineData(100, "Standard")]
    [InlineData(10000, "VIP")]
    [InlineData(0, "New")]
    public TestBankAccount Arrange_AccountWithBalance(decimal initialBalance, string accountType)
    {
        var accountNumber = $"{accountType}-123";

        var account = new TestBankAccountBuilder()
            .WithAccountNumber(accountNumber)
            .WithBalance(initialBalance)
            .Build();

        Assert.Equal(initialBalance, account.Balance);
        Assert.Equal(accountNumber,account.AccountNumber);

        return account;
    }

    [Theory]
    [InlineData(100, "Standard")]
    [InlineData(10000, "VIP")]
    public void Act_Deposit_IncreasesBalance(decimal initialBalance, string type)
    {
        // 1. Arrange
        var sut = Arrange_AccountWithBalance(initialBalance, type);

        // 2. Act
        sut.Deposit(50m);

        // 3. Assert
        Assert.Equal(initialBalance + 50m, sut.Balance);
    }


    public static IEnumerable<object[]> AccountScenarios =>
        new List<object[]>
        {
            new object[] { "SAVINGS-01", 1000m, true },
            new object[] { "DEBT-01", 0m, true },
            new object[] { "CLOSED-01", 50m, false } 
        };

    [ArrangeTheory]
    [MemberData(nameof(AccountScenarios))]
    public TestBankAccount Arrange_ComplexAccount(string accNo, decimal balance, bool active)
    {
        var builder = new TestBankAccountBuilder()
            .WithAccountNumber(accNo)
            .WithBalance(balance);

        if (!active)
        {
            builder.Inactive();
        }

        return builder.Build();
    }

    [Theory]
    [MemberData(nameof(AccountScenarios))]
    public void Act_BalanceCheck_ShouldMatchInitialData(string accNo, decimal balance, bool active)
    {
        var sut = Arrange_ComplexAccount(accNo, balance, active);

        Assert.Equal(accNo, sut.AccountNumber);
        Assert.Equal(balance, sut.Balance);
    }
}