using System.Text.Json.Serialization;
using AheadOfTime;
using ApiModels;
using Scalar.AspNetCore;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.UseUrls("http://localhost:5303");

// In AOT projects, Reflection has quite limited support:
_ = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddOpenApi();
builder.Services.AddValidation();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
app.MapTalkEndpoints();

app.Run();

[JsonSerializable(typeof(TalkModel))]
[JsonSerializable(typeof(CreateTalkModel))]
[JsonSerializable(typeof(List<TalkModel>))]
[JsonSerializable(typeof(HttpValidationProblemDetails))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
