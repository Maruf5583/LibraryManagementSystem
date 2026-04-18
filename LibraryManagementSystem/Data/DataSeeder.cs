using Bogus;
using Dapper;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;

public class DataSeeder
{
    private readonly string _connectionString;

    public DataSeeder(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task SeedAllAsync()
    {
        using var db = new SqlConnection(_connectionString);

        // ❗ Prevent duplicate seeding
        var count = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Book");
        if (count > 0) return;

        var faker = new Faker();

        // ---------------- GENRE ----------------
        var genres = new[]
        {
            "Technology","Self Improvement","History","Science","Fiction"
        };

        foreach (var g in genres)
        {
            await db.ExecuteAsync("INSERT INTO Genre (GenreName) VALUES (@Name)", new { Name = g });
        }

        // ---------------- AUTHORS ----------------
        var authors = new Faker<Author>()
            .RuleFor(x => x.AuthorName, f => f.Name.FullName())
            .Generate(50);

        await db.ExecuteAsync("INSERT INTO Author (AuthorName) VALUES (@AuthorName)", authors);

        // ---------------- BOOKS ----------------
        var books = new List<dynamic>();

        for (int i = 0; i < 500; i++)
        {
            books.Add(new
            {
                Title = faker.Lorem.Sentence(3),
                ShortDesc = faker.Lorem.Sentence(10),
                ISBN = faker.Random.Replace("978##########"),
                GenreId = faker.Random.Int(1, 5),
                Publication = faker.Company.CompanyName(),
                PublishedYear = faker.Date.Past(20).Year,
                Pages = faker.Random.Int(100, 800),
                Length = faker.Random.Decimal(7, 12),
                Width = faker.Random.Decimal(5, 8),
                Height = faker.Random.Decimal(1, 3),
                Image = "default.jpg"
            });
        }

        await db.ExecuteAsync(@"
            INSERT INTO Book 
            (Title, ShortDesc, ISBN, GenreId, Publication, PublishedYear, Pages, Length, Width, Height, Image)
            VALUES (@Title, @ShortDesc, @ISBN, @GenreId, @Publication, @PublishedYear, @Pages, @Length, @Width, @Height, @Image)
        ", books);

        // ---------------- BOOK COPY ----------------
        var copies = new List<dynamic>();

        for (int i = 1; i <= 500; i++)
        {
            copies.Add(new
            {
                BookId = i,
                Barcode = "BC" + i,
                IsAvailable = 1,
                Condition = "Good"
            });
        }

        await db.ExecuteAsync(@"
            INSERT INTO BookCopy (BookId, Barcode, IsAvailable, Condition)
            VALUES (@BookId, @Barcode, @IsAvailable, @Condition)
        ", copies);

        // ---------------- MEMBERS ----------------
        var members = new Faker<Member>()
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Mobile, f => f.Phone.PhoneNumber("01#########"))
            .RuleFor(x => x.PasswordHash, "hashed")
            .RuleFor(x => x.Salt, Guid.NewGuid())
            .RuleFor(x => x.IsActive, true)
            .RuleFor(x => x.Remarks, f => f.Lorem.Sentence())
            .Generate(200);

        await db.ExecuteAsync(@"
            INSERT INTO Member (Name, Email, Mobile, PasswordHash, Salt, IsActive, Remarks)
            VALUES (@Name, @Email, @Mobile, @PasswordHash, @Salt, @IsActive, @Remarks)
        ", members);

        // ---------------- REVIEWS ----------------
        var reviews = new List<dynamic>();

        for (int i = 0; i < 300; i++)
        {
            reviews.Add(new
            {
                MemberId = faker.Random.Int(1, 200),
                BookId = faker.Random.Int(1, 500),
                Ratings = faker.Random.Int(3, 5),
                IsRecommended = true,
                Comments = faker.Lorem.Sentence(),
                IsApproved = 1,
                ReviewedOn = DateTime.Now
            });
        }

        await db.ExecuteAsync(@"
            INSERT INTO BookReview 
            (MemberId, BookId, Ratings, IsRecommended, Comments, IsApproved, ReviewedOn)
            VALUES (@MemberId, @BookId, @Ratings, @IsRecommended, @Comments, @IsApproved, @ReviewedOn)
        ", reviews);
    }
}
