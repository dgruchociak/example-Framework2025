using NUnit.Framework;
using Playwright.Pages.CommitQuality.Components;

namespace Playwright.Tests.UITests.CommitQuality;

public class ProductsTests : BrowserTestFixture
{
    private ProductsComponent _productsComponent;
    
    [SetUp]
    public new async Task SetUp()
    {
        _productsComponent = new ProductsComponent(Page);
        await _productsComponent.WaitForPageLoadAsync();
    }
    
    //1 dodanie produktu
    [Test]
    public async Task Add_New_Product_Test_01()
    {
        var addProductPage = await _productsComponent
            .ClickAddProductButton();
        var productName = await addProductPage.SubmitNewProduct();
        await _productsComponent.ClickShowMoreButton();
        var productList = await _productsComponent.GetProductsList();
        var products = productList.Where(x => x.ContainsKey("name") && x["name"] == productName).ToList();
        
        Assert.That(products.Count, Is.EqualTo(1));
        Assert.That(products[0]["name"], Is.EqualTo(productName));
    }
    
    //2 filtrowanie produtkow
    [Test]
    public async Task Filter_Product_Test_02()
    {
        await _productsComponent.ClickShowMoreButton();
        var productsList = await _productsComponent.GetProductsList();
        await _productsComponent.FilterProductsByName("Product 1");
        var filteredProductsList = await _productsComponent.GetProductsList();
        Assert.That(productsList.Count, Is.Not.EqualTo(filteredProductsList.Count));
        Assert.That(filteredProductsList.Count, Is.EqualTo(5));
    }
    
    //3 wyciagniecie z listy elementów z nazwą 'Product 2' i zsumowanie ich ceny
    [Test]
    public async Task Sum_Product2_Prices_Test_03()
    {
        await _productsComponent.ClickShowMoreButton();
        var productsList = await _productsComponent.GetProductsList();
        var priceSumOfProduct2 = 
            productsList.Where(x => x.ContainsKey("name") && x["name"] == "Product 2")
                .Select(x => x["price"])
                .Sum(double.Parse);
        
        Assert.That(priceSumOfProduct2, Is.EqualTo(90));
    }
}