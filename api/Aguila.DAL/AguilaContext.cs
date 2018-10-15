using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aguila.DAL
{
    public partial class AguilaContext : DbContext
    {
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<AnswersDssTracking> AnswersDssTracking { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<AttributesDssTracking> AttributesDssTracking { get; set; }
        public virtual DbSet<CategoryAttributes> CategoryAttributes { get; set; }
        public virtual DbSet<CategoryAttributesDssTracking> CategoryAttributesDssTracking { get; set; }
        public virtual DbSet<ConfigurationsValues> ConfigurationsValues { get; set; }
        public virtual DbSet<CustomerApiKeys> CustomerApiKeys { get; set; }
        public virtual DbSet<Handicaps> Handicaps { get; set; }
        public virtual DbSet<HandicapsDssTracking> HandicapsDssTracking { get; set; }
        public virtual DbSet<Lrdattributes> Lrdattributes { get; set; }
        public virtual DbSet<LrdattributesDssTracking> LrdattributesDssTracking { get; set; }
        public virtual DbSet<LrdattributesLookups> LrdattributesLookups { get; set; }
        public virtual DbSet<LrdattributesLookupsDssTracking> LrdattributesLookupsDssTracking { get; set; }
        public virtual DbSet<Lrdcategories> Lrdcategories { get; set; }
        public virtual DbSet<LrdcategoriesDssTracking> LrdcategoriesDssTracking { get; set; }
        public virtual DbSet<Lrddatas> Lrddatas { get; set; }
        public virtual DbSet<LrddatasDssTracking> LrddatasDssTracking { get; set; }
        //public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<PictureBooks> PictureBooks { get; set; }
        public virtual DbSet<PictureBooksDssTracking> PictureBooksDssTracking { get; set; }
        public virtual DbSet<ProvisionMarkerDss> ProvisionMarkerDss { get; set; }
        public virtual DbSet<SchemaInfoDss> SchemaInfoDss { get; set; }
        public virtual DbSet<ScopeConfigDss> ScopeConfigDss { get; set; }
        public virtual DbSet<ScopeInfoDss> ScopeInfoDss { get; set; }
        public virtual DbSet<SituationAttributes> SituationAttributes { get; set; }
        public virtual DbSet<SituationAttributesDssTracking> SituationAttributesDssTracking { get; set; }
        public virtual DbSet<SituationCategories> SituationCategories { get; set; }
        public virtual DbSet<SituationCategoriesDssTracking> SituationCategoriesDssTracking { get; set; }
        public virtual DbSet<SituationHandicaps> SituationHandicaps { get; set; }
        public virtual DbSet<SituationHandicapsDssTracking> SituationHandicapsDssTracking { get; set; }
        public virtual DbSet<SituationQuestions> SituationQuestions { get; set; }
        public virtual DbSet<SituationQuestionsDssTracking> SituationQuestionsDssTracking { get; set; }
        public virtual DbSet<Situations> Situations { get; set; }
        public virtual DbSet<SituationsDssTracking> SituationsDssTracking { get; set; }
        public virtual DbSet<UserLessonsQueues> UserLessonsQueues { get; set; }
        public virtual DbSet<UserRoundCompletedQuestions> UserRoundCompletedQuestions { get; set; }
        public virtual DbSet<UserRoundCompletedQuestionsDssTracking> UserRoundCompletedQuestionsDssTracking { get; set; }
        public virtual DbSet<UserRoundCompletedSituations> UserRoundCompletedSituations { get; set; }
        public virtual DbSet<UserRoundCompleteds> UserRoundCompleteds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(ConnectionConfig.ConStr);
          //  optionsBuilder.UseSqlServer(@"Data Source = CHETUIWK556\SQL2014EXP; Initial Catalog = Aguila; Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Data Source= DATA-PROD\SQL2014; Initial Catalog=Aguila; user id=Aguila; password=A8u!l@355;");
          // optionsBuilder.UseSqlServer(@"Data Source= Web01\SQL2014; Initial Catalog=AguilaTechnologies; user id=Aguila; password=Agu!l@;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasIndex(e => e.SituationQuestionsId)
                    .HasName("IX_SituationQuestions_Id");

                entity.Property(e => e.Qid)
                    .HasColumnName("qid")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Score).HasColumnType("decimal");

                entity.Property(e => e.SituationQuestionsId).HasColumnName("SituationQuestions_Id");

                entity.HasOne(d => d.SituationQuestions)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.SituationQuestionsId)
                    .HasConstraintName("FK_dbo.Answers_dbo.SituationQuestions_SituationQuestions_Id1");
            });

            modelBuilder.Entity<AnswersDssTracking>(entity =>
            {
                entity.ToTable("Answers_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.ConcurrencyStamp).HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(255)");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.BallFlight).HasMaxLength(50);

                entity.Property(e => e.DatePaid).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.HandicapId).HasDefaultValueSql("0");

                entity.Property(e => e.LockoutEnd).HasColumnType("datetime");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasColumnType("varchar(255)");

                entity.Property(e => e.NormalizedUserName).HasColumnType("varchar(255)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AttributesDssTracking>(entity =>
            {
                entity.ToTable("Attributes_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<CategoryAttributes>(entity =>
            {
                entity.HasIndex(e => e.AttributesId)
                    .HasName("IX_Attributes_Id");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_Category_Id");

                entity.Property(e => e.AttributesId).HasColumnName("Attributes_Id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.HasOne(d => d.Attributes)
                    .WithMany(p => p.CategoryAttributes)
                    .HasForeignKey(d => d.AttributesId)
                    .HasConstraintName("FK_dbo.CategoryAttributes_dbo.Attributes_Attributes_Id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryAttributes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_dbo.CategoryAttributes_dbo.SituationCategories_Category_Id");
            });

            modelBuilder.Entity<CategoryAttributesDssTracking>(entity =>
            {
                entity.ToTable("CategoryAttributes_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<HandicapsDssTracking>(entity =>
            {
                entity.ToTable("Handicaps_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<Lrdattributes>(entity =>
            {
                entity.ToTable("LRDAttributes");

                entity.Property(e => e.LrdattributesId).HasColumnName("LRDAttributesId");
            });

            modelBuilder.Entity<LrdattributesDssTracking>(entity =>
            {
                entity.HasKey(e => e.LrdattributesId)
                    .HasName("PK_DataSync.LRDAttributes_dss_tracking");

                entity.ToTable("LRDAttributes_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.LrdattributesId })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.LrdattributesId)
                    .HasColumnName("LRDAttributesId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<LrdattributesLookups>(entity =>
            {
                entity.ToTable("LRDAttributesLookups");

                entity.HasIndex(e => e.LrdattributesId)
                    .HasName("IX_LRDAttributesId");

                entity.HasIndex(e => e.LrddataLrddataId)
                    .HasName("IX_LRDdata_LRDdataId");

                entity.Property(e => e.LrdattributesId).HasColumnName("LRDAttributesId");

                entity.Property(e => e.LrdcategoryId)
                    .HasColumnName("LRDCategoryId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LrddataLrddataId).HasColumnName("LRDdata_LRDdataId");

                entity.HasOne(d => d.Lrdattributes)
                    .WithMany(p => p.LrdattributesLookups)
                    .HasForeignKey(d => d.LrdattributesId)
                    .HasConstraintName("FK_dbo.LRDAttributesLookups_dbo.LRDAttributes_LRDAttributesId");

                entity.HasOne(d => d.LrddataLrddata)
                    .WithMany(p => p.LrdattributesLookups)
                    .HasForeignKey(d => d.LrddataLrddataId)
                    .HasConstraintName("FK_dbo.LRDAttributesLookups_dbo.LRDdatas_LRDdata_LRDdataId");
            });

            modelBuilder.Entity<LrdattributesLookupsDssTracking>(entity =>
            {
                entity.ToTable("LRDAttributesLookups_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<Lrdcategories>(entity =>
            {
                entity.HasKey(e => e.LrdcategoryId)
                    .HasName("PK_dbo.LRDCategories");

                entity.ToTable("LRDCategories");

                entity.HasIndex(e => e.LrddataLrddataId)
                    .HasName("IX_LRDdata_LRDdataId");

                entity.Property(e => e.LrdcategoryId).HasColumnName("LRDCategoryId");

                entity.Property(e => e.LrddataLrddataId).HasColumnName("LRDdata_LRDdataId");

                entity.HasOne(d => d.LrddataLrddata)
                    .WithMany(p => p.Lrdcategories)
                    .HasForeignKey(d => d.LrddataLrddataId)
                    .HasConstraintName("FK_dbo.LRDCategories_dbo.LRDdatas_LRDdata_LRDdataId");
            });

            modelBuilder.Entity<LrdcategoriesDssTracking>(entity =>
            {
                entity.HasKey(e => e.LrdcategoryId)
                    .HasName("PK_DataSync.LRDCategories_dss_tracking");

                entity.ToTable("LRDCategories_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.LrdcategoryId })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.LrdcategoryId)
                    .HasColumnName("LRDCategoryId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<Lrddatas>(entity =>
            {
                entity.HasKey(e => e.LrddataId)
                    .HasName("PK_dbo.LRDdatas");

                entity.ToTable("LRDdatas");

                entity.Property(e => e.LrddataId).HasColumnName("LRDdataId");

                entity.Property(e => e.ContentLocation).HasColumnName("contentLocation");

                entity.Property(e => e.Lrdtype).HasColumnName("LRDType");

                entity.Property(e => e.VideoUrl).HasColumnName("videoUrl");
            });

            modelBuilder.Entity<LrddatasDssTracking>(entity =>
            {
                entity.HasKey(e => e.LrddataId)
                    .HasName("PK_DataSync.LRDdatas_dss_tracking");

                entity.ToTable("LRDdatas_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.LrddataId })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.LrddataId)
                    .HasColumnName("LRDdataId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            //modelBuilder.Entity<MigrationHistory>(entity =>
            //{
            //    entity.HasKey(e => new { e.MigrationId, e.ContextKey })
            //        .HasName("PK_dbo.__MigrationHistory");

            //    entity.ToTable("__MigrationHistory");

            //    entity.Property(e => e.MigrationId).HasMaxLength(150);

            //    entity.Property(e => e.ContextKey).HasMaxLength(300);

            //    entity.Property(e => e.Model).IsRequired();

            //    entity.Property(e => e.ProductVersion)
            //        .IsRequired()
            //        .HasMaxLength(32);
            //});

            modelBuilder.Entity<PictureBooks>(entity =>
            {
                entity.Property(e => e.AndroidUrl).HasColumnName("AndroidURL");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Url).HasColumnName("URL");
            });

            modelBuilder.Entity<PictureBooksDssTracking>(entity =>
            {
                entity.ToTable("PictureBooks_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<ProvisionMarkerDss>(entity =>
            {
                entity.HasKey(e => new { e.OwnerScopeLocalId, e.ObjectId })
                    .HasName("PK_DataSync.provision_marker_dss");

                entity.ToTable("provision_marker_dss", "DataSync");

                entity.Property(e => e.OwnerScopeLocalId).HasColumnName("owner_scope_local_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ProvisionDatetime)
                    .HasColumnName("provision_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProvisionLocalPeerKey).HasColumnName("provision_local_peer_key");

                entity.Property(e => e.ProvisionScopeLocalId).HasColumnName("provision_scope_local_id");

                entity.Property(e => e.ProvisionScopePeerKey).HasColumnName("provision_scope_peer_key");

                entity.Property(e => e.ProvisionScopePeerTimestamp).HasColumnName("provision_scope_peer_timestamp");

                entity.Property(e => e.ProvisionTimestamp).HasColumnName("provision_timestamp");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnName("version")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<SchemaInfoDss>(entity =>
            {
                entity.HasKey(e => new { e.SchemaMajorVersion, e.SchemaMinorVersion })
                    .HasName("PK_DataSync.schema_info_dss");

                entity.ToTable("schema_info_dss", "DataSync");

                entity.Property(e => e.SchemaMajorVersion).HasColumnName("schema_major_version");

                entity.Property(e => e.SchemaMinorVersion).HasColumnName("schema_minor_version");

                entity.Property(e => e.SchemaExtendedInfo)
                    .IsRequired()
                    .HasColumnName("schema_extended_info")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ScopeConfigDss>(entity =>
            {
                entity.HasKey(e => e.ConfigId)
                    .HasName("PK_DataSync.scope_config_dss");

                entity.ToTable("scope_config_dss", "DataSync");

                entity.Property(e => e.ConfigId)
                    .HasColumnName("config_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ConfigData)
                    .IsRequired()
                    .HasColumnName("config_data")
                    .HasColumnType("xml");

                entity.Property(e => e.ScopeStatus)
                    .HasColumnName("scope_status")
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<ScopeInfoDss>(entity =>
            {
                entity.HasKey(e => e.SyncScopeName)
                    .HasName("PK_DataSync.scope_info_dss");

                entity.ToTable("scope_info_dss", "DataSync");

                entity.Property(e => e.SyncScopeName)
                    .HasColumnName("sync_scope_name")
                    .HasMaxLength(100);

                entity.Property(e => e.ScopeConfigId).HasColumnName("scope_config_id");

                entity.Property(e => e.ScopeId)
                    .HasColumnName("scope_id")
                    .HasDefaultValueSql("newid()");

                entity.Property(e => e.ScopeLocalId)
                    .HasColumnName("scope_local_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ScopeRestoreCount)
                    .HasColumnName("scope_restore_count")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ScopeSyncKnowledge).HasColumnName("scope_sync_knowledge");

                entity.Property(e => e.ScopeTimestamp)
                    .HasColumnName("scope_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeTombstoneCleanupKnowledge).HasColumnName("scope_tombstone_cleanup_knowledge");

                entity.Property(e => e.ScopeUserComment).HasColumnName("scope_user_comment");
            });

            modelBuilder.Entity<SituationAttributes>(entity =>
            {
                entity.HasIndex(e => e.AttributesId)
                    .HasName("IX_AttributesId");

                entity.HasIndex(e => e.SituationId)
                    .HasName("IX_SituationId");

                entity.Property(e => e.AttributesId).HasDefaultValueSql("0");

                entity.HasOne(d => d.Attributes)
                    .WithMany(p => p.SituationAttributes)
                    .HasForeignKey(d => d.AttributesId)
                    .HasConstraintName("FK_dbo.SituationAttributes_dbo.Attributes_AttributesId");

                entity.HasOne(d => d.Situation)
                    .WithMany(p => p.SituationAttributes)
                    .HasForeignKey(d => d.SituationId)
                    .HasConstraintName("FK_dbo.SituationAttributes_dbo.Situations_SituationId");
            });

            modelBuilder.Entity<SituationAttributesDssTracking>(entity =>
            {
                entity.ToTable("SituationAttributes_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<SituationCategoriesDssTracking>(entity =>
            {
                entity.ToTable("SituationCategories_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<SituationHandicaps>(entity =>
            {
                entity.HasIndex(e => e.SituationId)
                    .HasName("IX_SituationId");

                entity.Property(e => e.HandicapId).HasDefaultValueSql("0");

                entity.Property(e => e.NotUsed).HasColumnName("notUsed");

                entity.Property(e => e.SituationId).HasDefaultValueSql("0");

                entity.HasOne(d => d.NotUsedNavigation)
                    .WithMany(p => p.SituationHandicaps)
                    .HasForeignKey(d => d.NotUsed)
                    .HasConstraintName("FK_dbo.SituationHandicaps_dbo.Handicaps_Handicap_Id");

                entity.HasOne(d => d.Situation)
                    .WithMany(p => p.SituationHandicaps)
                    .HasForeignKey(d => d.SituationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituationHandicaps_Situations");
            });

            modelBuilder.Entity<SituationHandicapsDssTracking>(entity =>
            {
                entity.ToTable("SituationHandicaps_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<SituationQuestions>(entity =>
            {
                entity.HasIndex(e => e.SituationId1)
                    .HasName("IX_Situation_Id1");

                entity.Property(e => e.SituationId).HasColumnName("Situation_Id");

                entity.Property(e => e.SituationId1).HasColumnName("Situation_Id1");

                entity.HasOne(d => d.SituationId1Navigation)
                    .WithMany(p => p.SituationQuestions)
                    .HasForeignKey(d => d.SituationId1)
                    .HasConstraintName("FK_dbo.SituationQuestions_dbo.Situations_Situation_Id1");
            });

            modelBuilder.Entity<SituationQuestionsDssTracking>(entity =>
            {
                entity.ToTable("SituationQuestions_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<Situations>(entity =>
            {
                entity.HasIndex(e => e.BookId)
                    .HasName("IX_Book_Id");

                entity.HasIndex(e => e.ImageFileId)
                    .HasName("IX_ImageFile_Id");

                entity.HasIndex(e => e.NotSituationHandicapId)
                    .HasName("IX_SituationHandicap_Id");

                entity.HasIndex(e => e.SituationCategoryId)
                    .HasName("IX_SituationCategory_Id");

                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.Property(e => e.Deleted).HasDefaultValueSql("0");

                entity.Property(e => e.Elevation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImageFileId).HasColumnName("ImageFile_Id");

                entity.Property(e => e.IsFirstHole).HasDefaultValueSql("0");

                entity.Property(e => e.LineType).HasColumnName("lineType");

                entity.Property(e => e.NextHoleSituationId).HasDefaultValueSql("0");

                entity.Property(e => e.NotSituationHandicapId).HasColumnName("notSituationHandicap_Id");

                entity.Property(e => e.PinCoordinate).HasColumnName("pinCoordinate");

                entity.Property(e => e.Published)
                    .HasColumnName("published")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SituationCategoryId).HasColumnName("SituationCategory_Id");

                entity.Property(e => e.StartCoordinate).HasColumnName("startCoordinate");

                entity.Property(e => e.TargetCoordinate).HasColumnName("targetCoordinate");

                entity.Property(e => e.Unpublished)
                    .HasColumnName("unpublished")
                    .HasDefaultValueSql("0");
                
                entity.Property(e => e.VoiceOverUrl).HasColumnName("VoiceOverURL");

                entity.Property(e => e.WindDirection).HasMaxLength(2);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.SituationsBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_dbo.Situations_dbo.PictureBookConfigs_Book_Id");

                entity.HasOne(d => d.ImageFile)
                    .WithMany(p => p.SituationsImageFile)
                    .HasForeignKey(d => d.ImageFileId)
                    .HasConstraintName("FK_dbo.Situations_dbo.PictureBookConfigs_ImageFile_Id");

                entity.HasOne(d => d.SituationCategory)
                    .WithMany(p => p.Situations)
                    .HasForeignKey(d => d.SituationCategoryId)
                    .HasConstraintName("FK_dbo.Situations_dbo.SituationCategories_SituationCategory_Id");
            });

            modelBuilder.Entity<SituationsDssTracking>(entity =>
            {
                entity.ToTable("Situations_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<UserLessonsQueues>(entity =>
            {
                entity.Property(e => e.AspNetUsers_Id).HasColumnName("AspNetUsers_Id");

                entity.Property(e => e.LessonsAndRulesId).HasColumnName("LessonsAndRules_Id");
            });

            modelBuilder.Entity<UserRoundCompletedQuestions>(entity =>
            {
                entity.HasIndex(e => e.UserRoundCompletedSituationsId)
                    .HasName("IX_UserRoundCompletedSituations_Id");

                entity.Property(e => e.Qscore)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserRoundCompletedSituationsId).HasColumnName("UserRoundCompletedSituations_Id");

                entity.HasOne(d => d.UserRoundCompletedSituations)
                    .WithMany(p => p.UserRoundCompletedQuestions)
                    .HasForeignKey(d => d.UserRoundCompletedSituationsId)
                    .HasConstraintName("FK_dbo.UserRoundCompletedQuestions_dbo.UserRoundCompletedSituations_UserRoundCompletedSituations_Id");
            });

            modelBuilder.Entity<UserRoundCompletedQuestionsDssTracking>(entity =>
            {
                entity.ToTable("UserRoundCompletedQuestions_dss_tracking", "DataSync");

                entity.HasIndex(e => new { e.LocalUpdatePeerTimestamp, e.Id })
                    .HasName("local_update_peer_timestamp_index");

                entity.HasIndex(e => new { e.LastChangeDatetime, e.SyncRowIsTombstone, e.LocalUpdatePeerTimestamp })
                    .HasName("tombstone_index");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateScopeLocalId).HasColumnName("create_scope_local_id");

                entity.Property(e => e.LastChangeDatetime)
                    .HasColumnName("last_change_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalCreatePeerKey).HasColumnName("local_create_peer_key");

                entity.Property(e => e.LocalCreatePeerTimestamp).HasColumnName("local_create_peer_timestamp");

                entity.Property(e => e.LocalUpdatePeerKey).HasColumnName("local_update_peer_key");

                entity.Property(e => e.LocalUpdatePeerTimestamp)
                    .IsRequired()
                    .HasColumnName("local_update_peer_timestamp")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ScopeCreatePeerKey).HasColumnName("scope_create_peer_key");

                entity.Property(e => e.ScopeCreatePeerTimestamp).HasColumnName("scope_create_peer_timestamp");

                entity.Property(e => e.ScopeUpdatePeerKey).HasColumnName("scope_update_peer_key");

                entity.Property(e => e.ScopeUpdatePeerTimestamp).HasColumnName("scope_update_peer_timestamp");

                entity.Property(e => e.SyncRowIsTombstone).HasColumnName("sync_row_is_tombstone");

                entity.Property(e => e.UpdateScopeLocalId).HasColumnName("update_scope_local_id");
            });

            modelBuilder.Entity<UserRoundCompletedSituations>(entity =>
            {
                entity.HasIndex(e => e.UserRoundCompletedId)
                    .HasName("IX_UserRoundCompleted_Id");

                entity.Property(e => e.AspNetUsers_Id).HasColumnName("AspNetUsers_Id");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("0");

                entity.Property(e => e.DateCompleted).HasColumnType("datetime");

                entity.Property(e => e.Sscore)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserRoundCompletedId).HasColumnName("UserRoundCompleted_Id");

                entity.HasOne(d => d.UserRoundCompleted)
                    .WithMany(p => p.UserRoundCompletedSituations)
                    .HasForeignKey(d => d.UserRoundCompletedId)
                    .HasConstraintName("FK_dbo.UserRoundCompletedSituations_dbo.UserRoundCompleteds_UserRoundCompleted_Id");
            });

            modelBuilder.Entity<UserRoundCompleteds>(entity =>
            {
                entity.Property(e => e.AspNetUsers_Id).HasColumnName("AspNetUsers_Id");

                entity.Property(e => e.DateCompleted).HasColumnType("datetime");

                entity.Property(e => e.Rscore)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");
            });
        }
    }
}