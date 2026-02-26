namespace xUnit.Stagehand.Tests;

public class BankAccountTests
{
    [ArrangeFact]
    public TestBankAccount Arrange_FundedAccount()
    {
        var account = new TestBankAccount("ACC-12345");
        account.Deposit(500m);

        Assert.Equal(500m, account.Balance);
        Assert.True(account.IsActive);

        return account;
    }

    [ArrangeFact]
    public (TestBankAccount account, TestBankAccountBuilder builder)
        Arrange_Tuple_FundedAccount()
    {
        var accountNumber = "Some Number";
        var initialBalance = 500;
        var builder = new TestBankAccountBuilder()
            .WithAccountNumber(accountNumber)
            .WithBalance(initialBalance);

        var account = builder.Build();

        Assert.Equal(initialBalance, account.Balance);
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.True(account.IsActive);

        return (account, builder);
    }

    [Fact]
    public void Act_TransferBetweenAccounts_ShouldWork()
    {
        // Arrange
        var (sourceAccount, builder) = Arrange_Tuple_FundedAccount();
        var destinationAccount = builder
            .WithAccountNumber("Other Number")
            .WithBalance(0m) 
            .Build();

        // 3. Act
        sourceAccount.Withdraw(200m, destinationAccount);

        // 4. Assert
        Assert.Equal(300m, sourceAccount.Balance);
        Assert.Equal(200m, destinationAccount.Balance);
    }

    [Fact]
    public void Act_Withdraw_ValidAmount_DecreasesBalance()
    {
        // Arrange
        TestBankAccount sut = Arrange_FundedAccount();

        // Act
        sut.Withdraw(100m);

        // Assert
        Assert.Equal(400m, sut.Balance);
    }

    [Fact]
    public void Update_An_Account()
    {
        // Arrange
        TestBankAccount sut = Arrange_FundedAccount();

        // Act
        sut.Withdraw(100m);

        // Assert
        Assert.Equal(400m, sut.Balance);
    }
}
