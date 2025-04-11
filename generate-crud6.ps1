$entities = @(
    "ReaDroitProfil"
)

foreach ($entity in $entities) {
    Write-Host "Génération du CRUD pour $entity..."
    dotnet aspnet-codegenerator controller -name ${entity}Controller -m $entity -dc AppDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
}

Write-Host "Génération des CRUD terminée !"