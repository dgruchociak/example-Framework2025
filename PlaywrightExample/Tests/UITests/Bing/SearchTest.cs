// using NUnit.Framework;
// using Playwright.Pages.Bing;
//
// namespace Playwright.Tests.UITests.Bing;
//
// public class SearchTests : BrowserTestFixture
// {
//     private HomePage _homePage;
//     private SearchResultsPage _searchResultsPage;
//
//     [SetUp]
//     public async Task TestSetUp()
//     {
//         _homePage = new HomePage(Page);
//         _searchResultsPage = new SearchResultsPage(Page);
//         await _homePage.WaitForPageLoadAsync();
//         try
//         {
//             await _homePage.AcceptCookies();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     [Test]
//     public async Task SearchReturnsResults()
//     {
//         await _homePage.SearchAsync("playwright");
//         await _searchResultsPage.WaitForPageLoadAsync();
//
//         var resultsCount = await _searchResultsPage.GetResultsCountAsync();
//         Assert.That(resultsCount, Is.GreaterThan(0), "Search should return results");
//     }
// }