using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);
//No Singleton não importa quantas requisições/instâncias eu fizer o Id será sempre o mesmo; 
builder.Services.AddSingleton<PrimaryService>();
//No Scope o Id será sem o mesmo dentro da mesma requisições/instâncias;
builder.Services.AddScoped<SecondaryService>();
//No Transient o Id SEMPRE a cada requisições/instâncias;
builder.Services.AddTransient<TertiaryService>();

var app = builder.Build();



app.MapGet("/", (PrimaryService primaryService, SecondaryService secondaryService, TertiaryService tertiaryService) => new
{

    Id = Guid.NewGuid(),
    PrimaryServiceId = primaryService.Id,
    SecondaryService = new
    {
        Id = secondaryService.Id,
        PrimaryServiceId = secondaryService.PrimaryServiceId
    },
    TertiaryService = new
    {
        Id = tertiaryService.Id,
        PrimaryServiceId = tertiaryService.PrimaryServiceId,
        SecondaryServiceId = tertiaryService.SecondaryServiceId,
        SecondaryServiceNewInstanceId = tertiaryService.SecondaryServiceNewInstanceId

    }


});

app.Run();
