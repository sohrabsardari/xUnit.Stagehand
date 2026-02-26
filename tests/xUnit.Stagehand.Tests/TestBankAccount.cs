namespace xUnit.Stagehand.Tests;

public class TestBankAccount
{
    public string AccountNumber { get; }
    public decimal Balance { get; private set; }
    public bool IsActive { get; private set; } = true;

    public TestBankAccount(string accountNumber)
    {
        AccountNumber = accountNumber;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount;
    }

    public void Withdraw(decimal amount, TestBankAccount destinationAccount)
    {
        Withdraw(amount);
        destinationAccount.Deposit(amount);
    }
}