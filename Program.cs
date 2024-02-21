using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> countriesList = new()
{
    { 1, "United States" },
    { 2, "Canada" },
    { 3, "United Kingdom" },
    { 4, "India" },
    { 5, "Japan" }
};

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/countries", async (context) =>
    {
        context.Response.StatusCode = 200;
        foreach (var country in countriesList)
        {

            await context.Response.WriteAsync($"{country.Key},{country.Value}\n");
        }

    });
    endpoints.MapGet("/countries/{id:int}", async (context) =>
    {

        if (context.Request.RouteValues.ContainsKey("id") == false)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("The CountryID should be between 1 and 100");
        }
        int countryID = Convert.ToInt32(context.Request.RouteValues["id"]);

        if (countryID>100 || countryID < 1)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("The CountryID should be between 1 and 100");

        }
        else if (countryID>5)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]");

        }
        else
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync($"{countriesList[countryID]}");
        }
    });
});


app.Run(async (context) =>
{
    await context.Response.WriteAsync("No Response");
});

app.Run();
