namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GameListConfiguration : IEntityTypeConfiguration<GameList>
    {
        public void Configure(EntityTypeBuilder<GameList> builder)
        {
            builder.HasKey(g => g.RecordId);

            builder.Property(g => g.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(g => g.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(g => g.Rating)
                .IsRequired();

            builder.Property(g => g.Date)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
