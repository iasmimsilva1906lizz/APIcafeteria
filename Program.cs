

// app.MapGet("/", () => "API da cafeteria esta no ar!");

// app.MapGet("/api/livros", () =>
// {
//     return Results.Ok(new[]
//     {
//         new { id = 1, titulo = "Dom Casmurro", disponivel = true },
//         new { id = 2, titulo = "Capitães da Areia", disponivel = false }
//     });
// });

// app.MapGet("/api/livros/{id:int}", (int id) =>
// {
//     if (id == 1) return Results.Ok(new { id = 1, titulo = "Dom Casmurro", disponivel = true });
//     if (id == 2) return Results.Ok(new { id = 2, titulo = "Capitães da Areia", disponivel = false });
//     return Results.NotFound(new { mensagem = "Livro não encontrado." });
// });

// app.MapPost("/api/livros", async (HttpRequest requisicao) =>
// {
//     using JsonDocument documento = await JsonDocument.ParseAsync(requisicao.Body);
//     string titulo = documento.RootElement.GetProperty("titulo").GetString() ?? "";
//     return Results.Created("/api/livros/3", new { id = 3, titulo, disponivel = true });
// });

// app.MapPut("/api/livros/{id:int}", async (int id, HttpRequest requisicao) =>
// {
//     if (id != 1 && id != 2) return Results.NotFound(new { mensagem = "Livro não encontrado." });
//     using JsonDocument documento = await JsonDocument.ParseAsync(requisicao.Body);
//     string titulo = documento.RootElement.GetProperty("titulo").GetString() ?? "";
//     return Results.Ok(new { id, titulo, disponivel = true, mensagem = "Livro atualizado." });
// });

// app.MapDelete("/api/livros/{id:int}", (int id) =>
// {
//     if (id != 1 && id != 2) return Results.NotFound(new { mensagem = "Livro não encontrado." });
//     return Results.NoContent();
// });




//    


using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var cafes = new List<Cafe>
{
    new Cafe(1, "caramela", "media", true, 5),
    new Cafe (2, "running club", "media", true, 2)
};

app.MapGet("/api/cafes",() =>
{
    return Results.Ok(cafes);
});

app.MapGet("/api/cafes/{id:int}", (int id) =>
{
    var CafeEncontrado = cafes.Find(cafe => cafe.id == id);

    if (CafeEncontrado is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(CafeEncontrado);
});


app.MapPost("/api/cafes", ( CafeDTO dados ) =>
{
    var proximoId = cafes.Count +1;
    var novoCafe = new Cafe(proximoId, dados.nome, dados.torra, true, dados.quantidades);
    cafes.Add(novoCafe);
    return Results.Created($"/api/cafes/{novoCafe.id}", novoCafe);
    
});

app.MapPut("/api/cafes/{id:int}", (int id, CafeAtualizadoDTO dados ) =>
{
    int indice = cafes.FindIndex(cafeDaLista => cafeDaLista.id == id);
    if (indice == -1)
{
    return Results.NotFound();
}
var atualizado = new Cafe (id, dados.nome, dados.torra ,dados.disponivel, dados.quantidades);
cafes[indice] = atualizado;
return Results.Ok(atualizado);
});

app.MapDelete("/api/cafes/{id:int}", (int id) =>
{
    int indice = cafes.FindIndex(cafeDaLista => cafeDaLista.id == id);
    if (indice == -1)
{
    return Results.NotFound();
}

cafes.RemoveAt(indice);

return Results.NoContent();
});


// app.MapDelete("/api/cafes/{id int }", (int id) =>
// {
//     int indice  = cafes.FindIndex(cafeDaLista = cafeDaLista.id == id);
//     if(indice == -1)
//     {
//         return Results.Ok
//     }
// });

app.Run();

record Cafe (int id, string nome, string torra, bool disponivel, int quantidades);

record CafeDTO ( string nome, string torra, int quantidades);

record CafeAtualizadoDTO (string nome, string torra, bool disponivel, int quantidades);