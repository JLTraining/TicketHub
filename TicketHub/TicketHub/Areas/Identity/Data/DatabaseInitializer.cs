using Microsoft.Extensions.DependencyInjection;
using TicketHub.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TicketHub.Areas.Identity.Data
{
    public class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Ensure the database is created
                context.Database.EnsureCreated();
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<IdentityRole>()
                {
                 new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                 new IdentityRole { Name = "Store-Manager", NormalizedName = "STORE-MANAGER" },
                 new IdentityRole { Name = "Member", NormalizedName = "MEMBER" }
});
                context.SaveChanges();
                }
               
                
                // Users
                if (!context.Event.Any())
                {
                    context.Event.AddRange(new List<Event>()
{
                            new Event() { Title = "Tomorrowland", Date = new DateTime(2023, 7, 15, 18, 0, 0), Description = "Experience the world's greatest electronic dance music festival in Boom, Belgium." },
                            new Event() { Title = "Coachella", Date = new DateTime(2023, 4, 14, 12, 0, 0), Description = "Join the iconic music and arts festival in the desert of California, USA." },
                            new Event() { Title = "Glastonbury Festival", Date = new DateTime(2023, 6, 28, 9, 0, 0), Description = "Celebrate contemporary performing arts at the legendary festival in Somerset, England." },
                            new Event() { Title = "Wacken Open Air", Date = new DateTime(2023, 8, 3, 10, 0, 0), Description = "Rock out to heavy metal music at the world's largest open-air metal festival in Wacken, Germany." },
                            new Event() { Title = "Burning Man", Date = new DateTime(2023, 8, 27, 16, 0, 0), Description = "Immerse yourself in a unique and vibrant community at the annual gathering in Black Rock City, Nevada." },
                            new Event() { Title = "EDC Las Vegas", Date = new DateTime(2023, 5, 19, 20, 0, 0), Description = "Experience the electrifying atmosphere of Electric Daisy Carnival in Las Vegas, USA." },
                            new Event() { Title = "Roskilde Festival", Date = new DateTime(2023, 6, 30, 14, 0, 0), Description = "Enjoy a diverse lineup of music genres and cultural experiences at Northern Europe's largest festival in Roskilde, Denmark." },
                            new Event() { Title = "Reading and Leeds Festival", Date = new DateTime(2023, 8, 25, 12, 0, 0), Description = "Rock, indie, alternative, and more genres come together at the dual-site festival in England." },
                            new Event() { Title = "Lollapalooza", Date = new DateTime(2023, 7, 21, 15, 30, 0), Description = "Celebrate music, art, and culture at the legendary festival held in multiple cities worldwide." },
                            new Event() { Title = "SXSW", Date = new DateTime(2023, 3, 10, 11, 0, 0), Description = "Experience the convergence of film, music, and technology at South by Southwest in Austin, Texas." },
                            new Event() { Title = "Ultra Music Festival", Date = new DateTime(2023, 3, 24, 16, 0, 0), Description = "Dance to the beats of top electronic music artists at the renowned festival held in Miami, USA." },
                            new Event() { Title = "Montreux Jazz Festival", Date = new DateTime(2023, 7, 7, 19, 0, 0), Description = "Savor the sounds of jazz, blues, and rock music on the shores of Lake Geneva, Switzerland." },
                            new Event() { Title = "Electric Forest", Date = new DateTime(2023, 6, 22, 17, 30, 0), Description = "Immerse yourself in a magical forest setting with an eclectic lineup of music and art in Rothbury, Michigan." },
                            new Event() { Title = "Sziget Festival", Date = new DateTime(2023, 8, 9, 14, 0, 0), Description = "Celebrate the spirit of freedom and diversity at the week-long music and arts festival in Budapest, Hungary." },
                            new Event() { Title = "Primavera Sound", Date = new DateTime(2023, 6, 2, 18, 0, 0), Description = "Discover a carefully curated lineup of indie, pop, and alternative artists in Barcelona, Spain." },
                            new Event() { Title = "Rock in Rio", Date = new DateTime(2023, 9, 15, 20, 0, 0), Description = "Join the iconic music festival that has rocked cities like Rio de Janeiro and Lisbon with top international acts." },
                            new Event() { Title = "New Orleans Jazz & Heritage Festival", Date = new DateTime(2023, 4, 28, 11, 0, 0), Description = "Experience the soulful sounds of jazz, blues, and gospel in the heart of New Orleans, Louisiana." },
                            new Event() { Title = "Mawazine Festival", Date = new DateTime(2023, 5, 20, 15, 0, 0), Description = "Celebrate the sounds of Africa and the Arab world at the vibrant music festival in Rabat, Morocco." },
                            new Event() { Title = "Festival Internacional de Benicassim", Date = new DateTime(2023, 7, 13, 19, 30, 0), Description = "Enjoy a mix of music genres, including indie, rock, and electronic, on the Spanish coast." },
                            new Event() { Title = "Splendour in the Grass", Date = new DateTime(2023, 7, 28, 14, 0, 0), Description = "Delight in a weekend of music and arts at one of Australia's premier music festivals in Byron Bay." },
                            new Event() { Title = "Fuji Rock Festival", Date = new DateTime(2023, 7, 21, 12, 0, 0), Description = "Experience Japan's largest outdoor music festival, set against the beautiful backdrop of Mount Fuji." },
                            new Event() { Title = "Osheaga Music and Arts Festival", Date = new DateTime(2023, 8, 4, 15, 0, 0), Description = "Discover a diverse lineup of international and local artists in Montreal, Canada." },
                            new Event() { Title = "Wireless Festival", Date = new DateTime(2023, 7, 7, 16, 0, 0), Description = "Get ready for a high-energy urban music experience in London, United Kingdom." },
                            new Event() { Title = "Melt Festival", Date = new DateTime(2023, 7, 14, 18, 0, 0), Description = "Immerse yourself in electronic music at an industrial open-air venue in Ferropolis, Germany." },
                            new Event() { Title = "Rock Werchter", Date = new DateTime(2023, 6, 29, 14, 30, 0), Description = "Rock to the sounds of international rock, pop, and indie artists in Werchter, Belgium." },
                            new Event() { Title = "Sonar Festival", Date = new DateTime(2023, 6, 15, 17, 0, 0), Description = "Experience cutting-edge electronic music and multimedia arts in Barcelona, Spain." },
                            new Event() { Title = "Austin City Limits Music Festival", Date = new DateTime(2023, 10, 6, 12, 0, 0), Description = "Enjoy a diverse lineup of music genres in the live music capital of the world, Austin, Texas." },
                            new Event() { Title = "Electric Zoo", Date = new DateTime(2023, 9, 1, 20, 0, 0), Description = "Join the electronic music party on Randall's Island, New York City, with top DJs and artists." },
                            new Event() { Title = "Outlook Festival", Date = new DateTime(2023, 8, 30, 12, 0, 0), Description = "Experience the ultimate bass music and underground culture festival in Fort Punta Christo, Croatia." },
                            new Event() { Title = "Snowbombing", Date = new DateTime(2023, 4, 10, 10, 0, 0), Description = "Combine winter sports and electronic music at the snow-covered mountains of Mayrhofen, Austria." },
                            new Event() { Title = "Sunburn Festival", Date = new DateTime(2023, 12, 29, 16, 0, 0), Description = "Welcome the new year with India's biggest electronic music festival in Goa." },
                            new Event() { Title = "Cactus Festival", Date = new DateTime(2023, 7, 8, 15, 0, 0), Description = "Discover a unique blend of music genres, from rock to blues, in Bruges, Belgium." },
                            new Event() { Title = "Le Guess Who?", Date = new DateTime(2023, 11, 9, 19, 0, 0), Description = "Experience a diverse and boundary-pushing music festival in the picturesque city of Utrecht, Netherlands." },
                            new Event() { Title = "Sea Dance Festival", Date = new DateTime(2023, 8, 16, 18, 30, 0), Description = "Dance to the beats of electronic and pop music on the sandy beaches of Montenegro." },
});
                    context.SaveChanges();

                    if (!context.Users.Any())
                    {
                        context.Users.AddRange(new List<ApplicationUser>()
                        {
                                new ApplicationUser() { FirstName = "Justs", LastName = "Liepins", Alias = "Justes45", Email = "justsliepins@inbox.lv", EmailConfirmed = true, UserName = "Justes45" },
                                new ApplicationUser() { FirstName = "John", LastName = "Doe", Alias = "JD123", Email = "johndoe@example.com", EmailConfirmed = true, UserName = "JD123" },
                                new ApplicationUser() { FirstName = "Jane", LastName = "Smith", Alias = "JSmith", Email = "janesmith@example.com", EmailConfirmed = true, UserName = "JSmith" },
                                new ApplicationUser() { FirstName = "Emma", LastName = "Johnson", Alias = "EmJ", Email = "emmajohnson@example.com", EmailConfirmed = true, UserName = "EmJ" },
                                new ApplicationUser() { FirstName = "Michael", LastName = "Williams", Alias = "MWill", Email = "michaelwilliams@example.com", EmailConfirmed = true, UserName = "MWill" },
                                new ApplicationUser() { FirstName = "Olivia", LastName = "Brown", Alias = "OBrown", Email = "oliviabrown@example.com", EmailConfirmed = true, UserName = "OBrown" },
                                new ApplicationUser() { FirstName = "James", LastName = "Taylor", Alias = "JT123", Email = "jamestaylor@example.com", EmailConfirmed = true, UserName = "JT123" },
                                new ApplicationUser() { FirstName = "Sophia", LastName = "Miller", Alias = "SophM", Email = "sophiamiller@example.com", EmailConfirmed = true, UserName = "SophM" },
                                new ApplicationUser() { FirstName = "William", LastName = "Anderson", Alias = "WAnd", Email = "williamanderson@example.com", EmailConfirmed = true, UserName = "WAnd" },
                                new ApplicationUser() { FirstName = "Ava", LastName = "Thomas", Alias = "AT123", Email = "avathomas@example.com", EmailConfirmed = true, UserName = "AT123" },
                                new ApplicationUser() { FirstName = "Liam", LastName = "Lee", Alias = "LiamL", Email = "liamlee@example.com", EmailConfirmed = true, UserName = "LiamL" },
                                new ApplicationUser() { FirstName = "Isabella", LastName = "Lewis", Alias = "BellaL", Email = "isabellalewis@example.com", EmailConfirmed = true, UserName = "BellaL" },
                                new ApplicationUser() { FirstName = "Noah", LastName = "Walker", Alias = "NoahW", Email = "noahwalker@example.com", EmailConfirmed = true, UserName = "NoahW" },
                                new ApplicationUser() { FirstName = "Mia", LastName = "Hall", Alias = "MiaH", Email = "miahall@example.com", EmailConfirmed = true, UserName = "MiaH" },
                                new ApplicationUser() { FirstName = "Benjamin", LastName = "Young", Alias = "BY321", Email = "benjaminyoung@example.com", EmailConfirmed = true, UserName = "BY321" },
                                new ApplicationUser() { FirstName = "Charlotte", LastName = "Scott", Alias = "CScott", Email = "charlottescott@example.com", EmailConfirmed = true, UserName = "CScott" },
                                new ApplicationUser() { FirstName = "Elijah", LastName = "Perez", Alias = "EPerez", Email = "elijahperez@example.com", EmailConfirmed = true, UserName = "EPerez" },
                                new ApplicationUser() { FirstName = "Amelia", LastName = "Robinson", Alias = "AmRob", Email = "ameliarobinson@example.com", EmailConfirmed = true, UserName = "AmRob" },
                                new ApplicationUser() { FirstName = "Lucas", LastName = "Cook", Alias = "LC123", Email = "lucascook@example.com", EmailConfirmed = true, UserName = "LC123" },
                                new ApplicationUser() { FirstName = "Harper", LastName = "Hill", Alias = "HarperH", Email = "harperhill@example.com", EmailConfirmed = true, UserName = "HarperH" },
                                new ApplicationUser() { FirstName = "Alexander", LastName = "Carter", Alias = "AlexC", Email = "alexandercarter@example.com", EmailConfirmed = true, UserName = "AlexC" },
                                new ApplicationUser() { FirstName = "Sofia", LastName = "Ward", Alias = "SW123", Email = "sofiaward@example.com", EmailConfirmed = true, UserName = "SW123" },
                                new ApplicationUser() { FirstName = "Henry", LastName = "Turner", Alias = "HenryT", Email = "henryturner@example.com", EmailConfirmed = true, UserName = "HenryT" },
                                new ApplicationUser() { FirstName = "Avery", LastName = "Bailey", Alias = "AveryB", Email = "averybailey@example.com", EmailConfirmed = true, UserName = "AveryB" },
                                new ApplicationUser() { FirstName = "Jackson", LastName = "Cooper", Alias = "JC321", Email = "jacksoncooper@example.com", EmailConfirmed = true, UserName = "JC321" },
                                new ApplicationUser() { FirstName = "Scarlett", LastName = "Kelly", Alias = "ScarKelly", Email = "scarlettkelly@example.com", EmailConfirmed = true, UserName = "ScarKelly" },
                                new ApplicationUser() { FirstName = "Daniel", LastName = "Barnes", Alias = "DB123", Email = "danielbarnes@example.com", EmailConfirmed = true, UserName = "DB123" },
                                new ApplicationUser() { FirstName = "Luna", LastName = "Murphy", Alias = "LunaM", Email = "lunamurphy@example.com", EmailConfirmed = true, UserName = "LunaM" },
                                new ApplicationUser() { FirstName = "Matthew", LastName = "Gonzalez", Alias = "MattG", Email = "matthewgonzalez@example.com", EmailConfirmed = true, UserName = "MattG" }
                        });


                    };

                    context.SaveChanges();

                }
                
                if (!context.Ticket.Any())
                {
                    List<Ticket> tickets = new();
                    List<string> RowSeatIndex = new List<string>();
                    List<string> RowSeatIndexInt = new List<string>();

                    for (int i = 0; i < 150; i++)
                    {
                       
                        string sellerId = GetRandomSellerId(context);
                        string randomRow = GenerateRandomRow();
                        string randomRowInt = GenerateRandomRowInt();
                        string randomSeat = GenerateRandomSeat();

                        int IntOrLetter = new Random().Next(1, 3); // Generate a value between 1 and 2 (inclusive)
                        bool isDuplicate = false;

                        if (IntOrLetter == 1)
                        {
                            foreach (string item in RowSeatIndex)
                            {
                                if (item == randomRow + randomSeat)
                                {
                                    isDuplicate = true;
                                    break;

                                }
                            }
                            if (isDuplicate == false)
                            {
                                Ticket ticket = new Ticket()
                                {

                                    EventId = GetRandomEventId(context),
                                    SellerId = sellerId,
                                    Price = GenerateRandomPrice(),
                                    Quantity = GenerateRandomQuantity(),
                                    Row = randomRow,
                                    Seat = randomSeat,
                                    isListed = true
                                };

                                RowSeatIndex.Add(randomRow + randomSeat);
                                tickets.Add(ticket);
                            }

                        }
                        else if (IntOrLetter == 2) // Check if the value is 2
                        {
                            foreach (string item in RowSeatIndexInt)
                            {
                                if (item == randomRowInt + randomSeat)
                                {
                                    isDuplicate = true;
                                    break;
                                }
                            }
                            if (isDuplicate == false)
                            {
                                Ticket ticket = new Ticket()
                                {
                                    EventId = GetRandomEventId(context),
                                    SellerId = sellerId,
                                    Price = GenerateRandomPrice(),
                                    Quantity = GenerateRandomQuantity(),
                                    Row = randomRowInt,
                                    Seat = randomSeat
                                };
                                tickets.Add(ticket);
                                RowSeatIndexInt.Add(randomRowInt + randomSeat);
                            }
                        }
                        if (isDuplicate == true) isDuplicate = false;
                    }

                    context.Ticket.AddRange(tickets);
                    context.SaveChanges();
                }

            }
        }

        /*Some Helper methods*/
        private static string GetRandomSellerId(ApplicationDbContext context) 
        {
            var sellerIds = context.User.Select(u => u.Id).ToArray();
            return sellerIds[new Random().Next(0, sellerIds.Length)];
        }

        private static int GetRandomEventId(ApplicationDbContext context) 
        {
            var eventIds = context.Event.Select(u => u.Id).ToArray();
            return eventIds[new Random().Next(1, eventIds.Length)];
        }

        private static decimal GenerateRandomPrice()
        {
            return new Random().Next(10, 301);
        }

        private static int GenerateRandomQuantity()
        {
            return new Random().Next(1, 4);
        }

        private static string GenerateRandomSeat()
        {
            return new Random().Next(1, 101).ToString();
        }

        private static string GenerateRandomRow()
        {
            Random random = new Random();
            int rowNumber = random.Next(1, 27); 
            char rowChar = (char)('A' + rowNumber - 1); 
            return rowChar.ToString();
        }


        private static string GenerateRandomRowInt()
        {
            return new Random().Next(1, 60).ToString();
        }
        
    }
}
