using ApartmentMonitoring.Entity.Entities;
using Microsoft.EntityFrameworkCore;


namespace ApartmentMonitoring.Infrastructure.Context
{
	public class DataBaseContext : DbContext
	{
		public DbSet<ApartmentMonitoring.Entity.Entities.User> Users { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.DailyUserActivity> DailyUserActivities { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.Subscription> Subscriptions { get; set; }

		public DbSet<ApartmentMonitoring.Entity.Entities.Message> Messages { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.Notification> Notifications { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.Apartment> Apartments { get; set; }

	//	public DbSet<ApartmentView> ApartmentViews { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.InviteCode> InviteCodes { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.Banner> Banners { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.Chat> Chats { get; set; }
		public DbSet<ApartmentMonitoring.Entity.Entities.ChatMessage> ChatMessages { get; set; }


		public DataBaseContext(DbContextOptions<DataBaseContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ApartmentMonitoring.Entity.Entities.User>()
				.HasMany(u=>u.Apartments)
				.WithOne(a=>a.User);

			modelBuilder
				.Entity<ApartmentMonitoring.Entity.Entities.User>()
				.HasMany(u => u.Subscriptions)
				.WithOne(a => a.User);

			modelBuilder
				.Entity<User>()
				.HasKey(x => x.Id);
		
			modelBuilder.Entity<DailyUserActivity>()
				.HasOne(x => x.User)
				.WithMany(u => u.Activities)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			//---
			modelBuilder.Entity<DailyUserActivity>()
				.HasKey(x => x.Id);

			//----
			modelBuilder
				.Entity<Apartment>()
				.HasKey(x => x.Id);

			//modelBuilder.Entity<ApartmentView>()
			//	.HasKey(v => v.Id);

			//modelBuilder.Entity<ApartmentView>()
			//	.HasOne(v => v.Apartment)
			//	.WithMany(a => a.Views)
			//	.HasForeignKey(v => v.ApartmentId);

			//modelBuilder.Entity<ApartmentView>()
			//	.HasOne(v => v.User)
			//	.WithMany()
			//	.HasForeignKey(v => v.UserId)
			//	.OnDelete(DeleteBehavior.Restrict);

			modelBuilder
				.Entity<Message>()
				.HasKey(x => x.Id);

			modelBuilder
			.Entity<Subscription>()
			.HasKey(x => x.Id);


			//---
			modelBuilder.Entity<InviteCode>()
				.HasKey(i => i.Id);
			
			modelBuilder.Entity<InviteCode>()
				.Property(i => i.Id)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<ApartmentMonitoring.Entity.Entities.InviteCode>()
				.HasOne(i => i.User)
				.WithMany(u => u.InviteCodes)
				.HasForeignKey(i => i.UserId)
				.OnDelete(DeleteBehavior.SetNull);


			//Banner
			modelBuilder.Entity<Banner>()
				.HasKey(b => b.Id);

			modelBuilder.Entity<Banner>()
				.Property(b => b.Title).HasMaxLength(100);


			//Chat
			modelBuilder.Entity<Chat>()
				.HasOne(c => c.Apartment)
				.WithMany()
				.HasForeignKey(c => c.ApartmentId);

			modelBuilder.Entity<Chat>()
				.HasOne(c => c.Initiator)
				.WithMany()
				.HasForeignKey(c => c.InitiatorId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Chat>()
				.HasOne(c => c.Receiver)
				.WithMany()
				.HasForeignKey(c => c.ReceiverId)
				.OnDelete(DeleteBehavior.NoAction);

			// ChatMessage
			modelBuilder.Entity<ChatMessage>()
				.HasOne(m => m.Chat)
				.WithMany(c => c.Messages)
				.HasForeignKey(m => m.ChatId);

			modelBuilder.Entity<ChatMessage>()
				.HasOne(m => m.Sender)
				.WithMany()
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
