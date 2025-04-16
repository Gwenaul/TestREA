using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestREA.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RaSqlAgent> RaSqlAgents { get; set; }
    public virtual DbSet<ReaAcce> ReaAcces { get; set; }
    public virtual DbSet<ReaApplication> ReaApplications { get; set; }
    public virtual DbSet<ReaAuditeur> ReaAuditeurs { get; set; }
    public virtual DbSet<ReaChampApplication> ReaChampApplications { get; set; }
    public virtual DbSet<ReaChampProfil> ReaChampProfils { get; set; }
    public virtual DbSet<ReaChampVerrou> ReaChampVerrous { get; set; }
    public virtual DbSet<ReaDirection> ReaDirections { get; set; }
    public virtual DbSet<ReaDroitGroupe> ReaDroitGroupes { get; set; }
    public virtual DbSet<ReaDroitProfil> ReaDroitProfils { get; set; }
    public virtual DbSet<ReaDroitRole> ReaDroitRoles { get; set; }
    public virtual DbSet<ReaDroitUtilisateur> ReaDroitUtilisateurs { get; set; }
    public virtual DbSet<ReaDtUnite> ReaDtUnites { get; set; }
    public virtual DbSet<ReaGroupe> ReaGroupes { get; set; }
    public virtual DbSet<ReaNiveau> ReaNiveaus { get; set; }
    public virtual DbSet<ReaPartenaire> ReaPartenaires { get; set; }
    public virtual DbSet<ReaPeriodicite> ReaPeriodicites { get; set; }
    public virtual DbSet<ReaProfil> ReaProfils { get; set; }
    public virtual DbSet<ReaRole> ReaRoles { get; set; }
    public virtual DbSet<ReaService> ReaServices { get; set; }
    public virtual DbSet<ReaSite> ReaSites { get; set; }
    public virtual DbSet<ReaStatut> ReaStatuts { get; set; }
    public virtual DbSet<ReaTache> ReaTaches { get; set; }
    public virtual DbSet<ReaTypeApplication> ReaTypeApplications { get; set; }
    public virtual DbSet<ReaTypeAuditeur> ReaTypeAuditeurs { get; set; }
    public virtual DbSet<ReaUtilisateur> ReaUtilisateurs { get; set; }
    public virtual DbSet<ReaUtilisateurRh> ReaUtilisateurRhs { get; set; }
    public virtual DbSet<ReaVerrou> ReaVerrous { get; set; }
    public virtual DbSet<VueReaAccesMobile> VueReaAccesMobiles { get; set; }
    public virtual DbSet<VueReaExportAd> VueReaExportAds { get; set; }

    // Ajout de la table ReaChamp
    public virtual DbSet<ReaChamp> ReaChamps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=s44m-reporter2;Database=RESSOURCES_APPLICATIONS_TEST;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RaSqlAgent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RA_SqlAgent");

            entity.Property(e => e.DateCreation)
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.DateExecution).HasColumnName("dateExecution");
            entity.Property(e => e.DureeExecution).HasColumnName("dureeExecution");
            entity.Property(e => e.HeureExecution).HasColumnName("heureExecution");
            entity.Property(e => e.InstanceId).HasColumnName("instance_id");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Server)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("server");
            entity.Property(e => e.StatutExecution).HasColumnName("statutExecution");
            entity.Property(e => e.StepId).HasColumnName("step_id");
        });

        modelBuilder.Entity<ReaAcce>(entity =>
        {
            entity.ToTable("REA_Acces");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateConnexion)
                .HasColumnType("datetime")
                .HasColumnName("dateConnexion");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");

            entity.HasOne(d => d.IdApplicationNavigation).WithMany(p => p.ReaAcces)
                .HasForeignKey(d => d.IdApplication)
                .HasConstraintName("FK_Acces_Application");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.ReaAcces)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_Acces_Utilisateur");
        });

        modelBuilder.Entity<ReaApplication>(entity =>
        {
            entity.ToTable("REA_Application");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Application)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("application");
            entity.Property(e => e.Commentaire)
                .IsUnicode(false)
                .HasColumnName("commentaire");
            entity.Property(e => e.IdType).HasColumnName("idType");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.ReaApplications)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_Application_Type");
        });

        modelBuilder.Entity<ReaAuditeur>(entity =>
        {
            entity.ToTable("REA_Auditeur");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Civilite)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("civilite");
            entity.Property(e => e.CodeTiers)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codeTiers");
            entity.Property(e => e.DateCreation)
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("datetime")
                .HasColumnName("dateNaissance");
            entity.Property(e => e.DerniereConnexion)
                .HasColumnType("datetime")
                .HasColumnName("derniereConnexion");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdTypeAuditeur).HasColumnName("idTypeAuditeur");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.MotPasse)
                .HasMaxLength(50)
                .HasColumnName("motPasse");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telephone");

            entity.HasOne(d => d.IdTypeAuditeurNavigation).WithMany(p => p.ReaAuditeurs)
                .HasForeignKey(d => d.IdTypeAuditeur)
                .HasConstraintName("FK_Auditeur_Type");
        });

        // Configuration pour la table ReaChamp
        modelBuilder.Entity<ReaChamp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Champ");
            entity.ToTable("REA_Champ");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Description)
                .HasColumnName("description");

            // Relation avec REA_ChampVerrou(1 ReaChamp => plusieurs ReaChampVerrou)
            entity.HasOne(e => e.ReaChampVerrou)
                .WithOne(v => v.ReaChamp)
                .HasForeignKey<ReaChampVerrou>(v => v.IdChamp) // Clé étrangère dans ReaChampVerrou
                .HasConstraintName("FK_ChampVerrou_Champ")
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Relation avec REA_ChampProfil (1 ReaChamp => plusieurs ReaChampProfil)
            entity.HasMany(e => e.ReaChampProfils)
                .WithOne(p => p.ReaChamp)
                .HasForeignKey(p => p.IdChamp)
                .HasConstraintName("FK_ChampProfil_Champ") // Clé étrangère dans ReaChampProfil
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Relation avec REA_ChampApplication(1 ReaChamp => plusieurs ReaChampApplication)
                entity.HasMany(e => e.ReaChampApplications)
                    .WithOne(a => a.ReaChamp)
                    .HasForeignKey(a => a.IdChamp)
                    .HasConstraintName("FK_ChampApplication_Champ")
                    .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Configuration pour REA_ChampVerrou
        modelBuilder.Entity<ReaChampVerrou>(entity =>
        {
            entity.HasKey(e => new { e.IdChamp}).HasName("PK_ChampVerrou");
            entity.ToTable("REA_ChampVerrou");

            entity.Property(e => e.IdChamp).HasColumnName("idChamp");
            entity.Property(e => e.IdVerrou).HasColumnName("idVerrou");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");

            // Relation avec REA_Application
            entity.HasOne(e => e.IdApplicationNavigation)
                .WithMany(a => a.ReaChampVerrous)
                .HasForeignKey(a => a.IdApplication)
                .HasConstraintName("FK_ChampVerrou_Application")
                .OnDelete(DeleteBehavior.ClientSetNull);

        });

        // Configuration pour REA_ChampApplication
        modelBuilder.Entity<ReaChampApplication>(entity =>
        {
            entity.HasKey(e => new { e.IdApplication, e.IdChamp }).HasName("PK_ChampApplication");
            entity.ToTable("REA_ChampApplication");

            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.IdChamp).HasColumnName("idChamp");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");

        });

        // Configuration pour REA_ChampProfil
        modelBuilder.Entity<ReaChampProfil>(entity =>
        {
            entity.HasKey(e => new { e.IdChamp, e.IdProfil }).HasName("PK_ChampProfil");
            entity.ToTable("REA_ChampProfil");

            entity.Property(e => e.IdChamp).HasColumnName("idChamp");
            entity.Property(e => e.IdProfil).HasColumnName("idProfil");
            entity.Property(e => e.Autorise).HasColumnName("autorise");

            // Relation avec REA_Profil
            entity.HasOne(e => e.IdProfilNavigation)
                .WithMany(p => p.ReaChampProfils)
                .HasForeignKey(p => p.IdProfil)
                .HasConstraintName("FK_ChampProfil_Profil")
                .OnDelete(DeleteBehavior.ClientSetNull);

        });

        modelBuilder.Entity<ReaDirection>(entity =>
        {
            entity.ToTable("REA_Direction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("libelle");
            entity.Property(e => e.NomDirection)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomDirection");
        });

        modelBuilder.Entity<ReaDroitGroupe>(entity =>
        {
            entity.ToTable("REA_DroitGroupe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autorise).HasColumnName("autorise");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.IdGroupe).HasColumnName("idGroupe");

            entity.HasOne(d => d.IdApplicationNavigation).WithMany(p => p.ReaDroitGroupes)
                .HasForeignKey(d => d.IdApplication)
                .HasConstraintName("FK_DroitGroupe_Application");

            entity.HasOne(d => d.IdGroupeNavigation).WithMany(p => p.ReaDroitGroupes)
                .HasForeignKey(d => d.IdGroupe)
                .HasConstraintName("FK_DroitGroupe_Groupe");
        });

        modelBuilder.Entity<ReaDroitProfil>(entity =>
        {
            entity.ToTable("REA_DroitProfil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autorise).HasColumnName("autorise");
            entity.Property(e => e.IdProfil).HasColumnName("idProfil");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");

            entity.HasOne(d => d.IdProfilNavigation).WithMany(p => p.ReaDroitProfils)
                .HasForeignKey(d => d.IdProfil)
                .HasConstraintName("FK_DroitProfil_Profil");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.ReaDroitProfils)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_DroitProfil_Utilisateur");
        });

        modelBuilder.Entity<ReaDroitRole>(entity =>
        {
            entity.ToTable("REA_DroitRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autorise).HasColumnName("autorise");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.ReaDroitRoles)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_DroitRole_Role");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.ReaDroitRoles)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_DroitRole_Utilisateur");
        });

        modelBuilder.Entity<ReaDroitUtilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_REA_Droit");

            entity.ToTable("REA_DroitUtilisateur");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autorise).HasColumnName("autorise");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");

            entity.HasOne(d => d.IdApplicationNavigation).WithMany(p => p.ReaDroitUtilisateurs)
                .HasForeignKey(d => d.IdApplication)
                .HasConstraintName("FK_DroitUtilisateur_Application");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.ReaDroitUtilisateurs)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_DroitUtilisateur_Utilisateur");
        });

        modelBuilder.Entity<ReaDtUnite>(entity =>
        {
            entity.ToTable("REA_DT_Unites");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archive).HasColumnName("archive");
            entity.Property(e => e.Libelle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<ReaGroupe>(entity =>
        {
            entity.ToTable("REA_Groupe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("libelle");
            entity.Property(e => e.NomGroupe)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomGroupe");
        });

        modelBuilder.Entity<ReaNiveau>(entity =>
        {
            entity.ToTable("REA_Niveau");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<ReaPartenaire>(entity =>
        {
            entity.ToTable("REA_Partenaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdOpenPortal).HasColumnName("idOpenPortal");
            entity.Property(e => e.IdYpareo).HasColumnName("idYpareo");
            entity.Property(e => e.NomPartenaire)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nomPartenaire");
        });

        modelBuilder.Entity<ReaPeriodicite>(entity =>
        {
            entity.ToTable("REA_Periodicite");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Periode)
                .IsUnicode(false)
                .HasColumnName("periode");
        });

        modelBuilder.Entity<ReaProfil>(entity =>
        {
            entity.ToTable("REA_Profil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .IsUnicode(false)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<ReaRole>(entity =>
        {
            entity.ToTable("REA_Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<ReaService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_REA_Log");

            entity.ToTable("REA_Service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateExecution)
                .HasColumnType("datetime")
                .HasColumnName("dateExecution");
            entity.Property(e => e.DateIntervention)
                .HasColumnType("datetime")
                .HasColumnName("dateIntervention");
            entity.Property(e => e.Detail)
                .IsUnicode(false)
                .HasColumnName("detail");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.Niveau).HasColumnName("niveau");
            entity.Property(e => e.Traitement).HasColumnName("traitement");
            entity.Property(e => e.Unite).HasColumnName("unite");

            entity.HasOne(d => d.IdApplicationNavigation).WithMany(p => p.ReaServices)
                .HasForeignKey(d => d.IdApplication)
                .HasConstraintName("FK_Service_Application");

            entity.HasOne(d => d.NiveauNavigation).WithMany(p => p.ReaServices)
                .HasForeignKey(d => d.Niveau)
                .HasConstraintName("FK_Service_Niveau");
        });

        modelBuilder.Entity<ReaSite>(entity =>
        {
            entity.ToTable("REA_Site");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adresse)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.Archive).HasColumnName("archive");
            entity.Property(e => e.CodeDepartement)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codeDepartement");
            entity.Property(e => e.CodeSite)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codeSite");
            entity.Property(e => e.IdDirection).HasColumnName("idDirection");
            entity.Property(e => e.NomSite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomSite");

            entity.HasOne(d => d.IdDirectionNavigation).WithMany(p => p.ReaSites)
                .HasForeignKey(d => d.IdDirection)
                .HasConstraintName("FK_Site_Direction");
        });

        modelBuilder.Entity<ReaStatut>(entity =>
        {
            entity.ToTable("REA_Statut");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<ReaTache>(entity =>
        {
            entity.ToTable("REA_Tache");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actif).HasColumnName("actif");
            entity.Property(e => e.AdresseIp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("adresseIP");
            entity.Property(e => e.Commentaire)
                .IsUnicode(false)
                .HasColumnName("commentaire");
            entity.Property(e => e.CompteService)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("compteService");
            entity.Property(e => e.DetailPeriodicite)
                .IsUnicode(false)
                .HasColumnName("detailPeriodicite");
            entity.Property(e => e.HeureExecution)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("heureExecution");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.Serveur)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serveur");
        });

        modelBuilder.Entity<ReaTypeApplication>(entity =>
        {
            entity.ToTable("REA_TypeApplication");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<ReaTypeAuditeur>(entity =>
        {
            entity.ToTable("REA_TypeAuditeur");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<ReaUtilisateur>(entity =>
        {
            entity.ToTable("REA_Utilisateur");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeCegid)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("codeCegid");
            entity.Property(e => e.CodeCommercial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codeCommercial");
            entity.Property(e => e.CodeCommercial2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codeCommercial2");
            entity.Property(e => e.DateCreation)
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.DateModification)
                .HasColumnType("datetime")
                .HasColumnName("dateModification");
            entity.Property(e => e.EmailUtilisateur)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emailUtilisateur");
            entity.Property(e => e.IdGroupe).HasColumnName("idGroupe");
            entity.Property(e => e.IdPartenaire).HasColumnName("idPartenaire");
            entity.Property(e => e.IdSite).HasColumnName("idSite");
            entity.Property(e => e.IdStatut).HasColumnName("idStatut");
            entity.Property(e => e.IdUtilisateurRh).HasColumnName("idUtilisateurRH");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.MotPasse)
                .HasMaxLength(100)
                .HasColumnName("motPasse");
            entity.Property(e => e.NomUtilisateur)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomUtilisateur");
            entity.Property(e => e.PersonnelExterne).HasColumnName("personnelExterne");
            entity.Property(e => e.PrenomUtilisateur)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("prenomUtilisateur");
            entity.Property(e => e.Telephone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.TestApplication).HasColumnName("testApplication");
            entity.Property(e => e.Titre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titre");
            entity.Property(e => e.UtilisateurTest).HasColumnName("utilisateurTest");

            entity.HasOne(d => d.IdStatutNavigation).WithMany()
                .HasForeignKey(d => d.IdStatut)
                .HasConstraintName("FK_Utilisateur_Statut");

            entity.HasOne(d => d.IdGroupeNavigation).WithMany(p => p.ReaUtilisateurs)
                .HasForeignKey(d => d.IdGroupe)
                .HasConstraintName("FK_Utilisateur_Groupe");

            entity.HasOne(d => d.IdSiteNavigation).WithMany(p => p.ReaUtilisateurs)
                .HasForeignKey(d => d.IdSite)
                .HasConstraintName("FK_Utilisateur_Site");

            entity.HasOne(d => d.IdUtilisateurRhNavigation).WithMany(p => p.ReaUtilisateurs)
                .HasForeignKey(d => d.IdUtilisateurRh)
                .HasConstraintName("FK_Utilisateur_RH");
        });

        modelBuilder.Entity<ReaUtilisateurRh>(entity =>
        {
            entity.ToTable("REA_Utilisateur_RH");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccesApp)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("accesApp");
            entity.Property(e => e.Consignes)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("consignes");
            entity.Property(e => e.DateEntree)
                .HasColumnType("datetime")
                .HasColumnName("dateEntree");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("datetime")
                .HasColumnName("dateNaissance");
            entity.Property(e => e.FinContrat)
                .HasColumnType("datetime")
                .HasColumnName("finContrat");
            entity.Property(e => e.NomUtilisateurRh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomUtilisateurRH");
            entity.Property(e => e.PrenomUtilisateurRh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("prenomUtilisateurRH");
        });

        modelBuilder.Entity<ReaVerrou>(entity =>
        {
            entity.ToTable("REA_Verrou");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateVerrou)
                .HasColumnType("datetime")
                .HasColumnName("dateVerrou");
            entity.Property(e => e.IdApplication).HasColumnName("idApplication");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");
            entity.Property(e => e.NbTentative).HasColumnName("nbTentative");

            entity.HasOne(d => d.IdApplicationNavigation)
                .WithMany(p => p.ReaVerrous)
                .HasForeignKey(d => d.IdApplication)
                .HasConstraintName("FK_Verrou_Application");

            entity.HasOne(d => d.IdUtilisateurNavigation)
                .WithMany(p => p.ReaVerrous)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_Verrou_Utilisateur");
        });

        modelBuilder.Entity<VueReaAccesMobile>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vue_REA_AccesMobile");

            entity.Property(e => e.CodeTiers)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codeTiers");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("datetime")
                .HasColumnName("dateNaissance");
            entity.Property(e => e.IdTypeAuditeur).HasColumnName("idTypeAuditeur");
            entity.Property(e => e.Login)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.MotPasse)
                .HasMaxLength(150)
                .HasColumnName("motPasse");
            entity.Property(e => e.TLibelle)
                .HasMaxLength(35)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("T_LIBELLE");
            entity.Property(e => e.TTiers)
                .HasMaxLength(17)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("T_TIERS");
        });

        modelBuilder.Entity<VueReaExportAd>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vue_REA_ExportAD");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Groupe)
                .HasMaxLength(255)
                .HasColumnName("groupe");
            entity.Property(e => e.IdGroupe).HasColumnName("idGroupe");
            entity.Property(e => e.IdSite).HasColumnName("idSite");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(255)
                .HasColumnName("prenom");
            entity.Property(e => e.Site)
                .HasMaxLength(255)
                .HasColumnName("site");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
