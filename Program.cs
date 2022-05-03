using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            // .WithOrigins(new[] { "http://localhost:8080", "http://localhost:8081" })
            //.AllowCredentials()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-2.1&tabs=aspnetcore2x#fileextensioncontenttypeprovider

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".obj"] = "model/obj";
provider.Mappings[".mtl"] = "model/mtl";
provider.Mappings[".fbx"] = "model/fbx";
provider.Mappings[".gltf"] = "model/gltf";


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.UseFileServer(enableDirectoryBrowsing: true);
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.Run();
