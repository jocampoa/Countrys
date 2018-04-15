﻿namespace Countrys.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        #region Properties
        public System.Data.Entity.DbSet<Countrys.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Countrys.Domain.UserType> UserTypes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupTeam> GroupTeams { get; set; }

        public DbSet<StatusMatch> StatusMatches { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<BoardStatus> BoardStatus { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Prediction> Predictions { get; set; }
        #endregion

        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new MatchesMap());
        }
        #endregion
    }
}
