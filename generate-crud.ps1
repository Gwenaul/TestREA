$entities = @(
    "RA_SqlAgent",
    "REA_Acces",
    "REA_Application",
    "REA_Auditeur",
    "REA_ChampApplication",
    "REA_ChampProfil",
    "REA_ChampVerrou",
    "REA_Direction",
    "REA_DroitGroupe",
    "REA_DroitProfil",
    "REA_DroitRole",
    "REA_DroitUtilisateur",
    "REA_Groupe",
    "REA_Niveau",
    "REA_Partenaire",
    "REA_Periodicite",
    "REA_Profil",
    "REA_Role",
    "REA_Service",
    "REA_Site",
    "REA_Statut",
    "REA_Tache",
    "REA_TypeApplication",
    "REA_TypeAuditeur",
    "REA_Utilisateur",
    "REA_Utilisateur_RH",
    "REA_Verrou"
)

foreach ($entity in $entities) {
    Write-Host "Génération du CRUD pour $entity..."
    dotnet aspnet-codegenerator controller -name ${entity}Controller -m $entity -dc AppDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
}

Write-Host "Génération des CRUD terminée !"