namespace Playwright.Core.Entity;

public class CreateRequestBuilder
{
    private string _postcode = "90210";
    private string _country = "United States";

    public CreateRequestBuilder WithPostCode(string postcode)
    {
        _postcode = postcode;
        return this;
    }
    
    public CreateRequestBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }
    
    public ZippoGetModel Build()
    {
        if (string.IsNullOrWhiteSpace(_country))
        {
            throw new InvalidOperationException("User name cannot be empty when building CreateUserRequest.");
        }

        return new ZippoGetModel()
        {
            postcode = _postcode,
            country = _country,
        };
    }
}