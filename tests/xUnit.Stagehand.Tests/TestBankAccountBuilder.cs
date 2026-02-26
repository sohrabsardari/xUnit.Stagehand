namespace xUnit.Stagehand.Tests;

public class TestBankAccountBuilder
{
    private string _accountNumber = "DEFAULT-ACC-001";
    private decimal _initialBalance = 0m;
    private bool _shouldBeActive = true;

    public TestBankAccountBuilder WithAccountNumber(string accountNumber)
    {
        _accountNumber = accountNumber;
        return this;
    }

    public TestBankAccountBuilder WithBalance(decimal balance)
    {
        _initialBalance = balance;
        return this;
    }

    public TestBankAccountBuilder Inactive()
    {
        _shouldBeActive = false;
        return this;
    }


    public TestBankAccount Build()
    {
        var account = new TestBankAccount(_accountNumber);

        if (_initialBalance > 0)
        {
            account.Deposit(_initialBalance);
        }

        return account;
    }
}