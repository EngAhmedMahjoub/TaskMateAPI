var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vite default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var tasks = new List<object>(); // In-memory tasks list for now

app.MapGet("/api/tasks", () => tasks);
app.MapPost("/api/tasks", async (HttpContext context) =>
{
    var task = await context.Request.ReadFromJsonAsync<object>();
    tasks.Add(task!);
    return Results.Created("/api/tasks", task);
});

app.Run();
