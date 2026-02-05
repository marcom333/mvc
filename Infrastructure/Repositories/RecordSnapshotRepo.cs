using System.Diagnostics;
using Application.Entities;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RecordSnapshotRepo {

    private readonly DapperContext _context;

    public RecordSnapshotRepo(DapperContext context) {
        _context = context;
    }

    public long Builder(RecordSnapshot record) {
        var con = _context.GetConnection();
        string sql = @"
        SELECT
            SnapshotId,

            IntField01, IntField02,
            IntField03, IntField04, IntField05, IntField06,
            IntField07, IntField08, IntField09, IntField10,

            DecimalField01, DecimalField02, DecimalField03,
            DecimalField04, DecimalField05,

            DateField01, DateField02, DateField03,
            DateField04, DateField05,

            BitField01, BitField02, BitField03,
            BitField04, BitField05,

            StringField01, StringField02,
            StringField03, StringField04, StringField05, StringField06,
            StringField07, StringField08, StringField09, StringField10,

            CreatedAt, UpdatedAt
        FROM dbo.RecordSnapshot
        /**where**/";
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(sql);

        var parameters = new DynamicParameters();

        if (record.IntField01.HasValue)
        {
            builder.Where("IntField01 = @IntField01");
            parameters.Add("IntField01", record.IntField01);
        }

        if (record.IntField02.HasValue)
        {
            builder.Where("IntField02 >= @IntField02");
            parameters.Add("IntField02", record.IntField02);
        }

        if (record.DecimalField01.HasValue)
        {
            builder.Where("DecimalField01 = @DecimalField01");
            parameters.Add("DecimalField01", record.DecimalField01);
        }

        if (record.DateField01.HasValue)
        {
            // SARGable date comparison
            builder.Where("DateField01 >= @DateFrom AND DateField01 < DATEADD(DAY, 1, @DateFrom)");
            parameters.Add("DateFrom", record.DateField01.Value.Date);
        }

        if (!string.IsNullOrWhiteSpace(record.StringField01))
        {
            builder.Where("StringField01 = @StringField01");
            parameters.Add("StringField01", record.StringField01);
        }

        if (record.BitField01.HasValue)
        {
            builder.Where("BitField01 = @BitField01");
            parameters.Add("BitField01", record.BitField01);
        }

        if (!string.IsNullOrWhiteSpace(record.StringField02))
        {
            builder.Where("StringField02 LIKE @StringField02");
            parameters.Add("StringField02", $"%{record.StringField02}%");
        }

        if (record.DateField02.HasValue)
        {
            // Also SARGable
            builder.Where("DateField02 >= @Date2From AND DateField02 < DATEADD(DAY, 1, @Date2From)");
            parameters.Add("Date2From", record.DateField02.Value.Date);
        }
        
        con.Query<RecordSnapshot>(
            template.RawSql,
            parameters
        ).ToList();
        
        return 0;
    }

    public long NoBuilder(RecordSnapshot record) {
        var con = _context.GetConnection();
        string sql = @"
        SELECT
            SnapshotId,

            IntField01, IntField02,
            IntField03, IntField04, IntField05, IntField06,
            IntField07, IntField08, IntField09, IntField10,

            DecimalField01, DecimalField02, DecimalField03,
            DecimalField04, DecimalField05,

            DateField01, DateField02, DateField03,
            DateField04, DateField05,

            BitField01, BitField02, BitField03,
            BitField04, BitField05,

            StringField01, StringField02,
            StringField03, StringField04, StringField05, StringField06,
            StringField07, StringField08, StringField09, StringField10,

            CreatedAt, UpdatedAt
        FROM dbo.RecordSnapshot
        WHERE
            -- Optional parameter pattern (NON-SARGable)
            (@IntField01 IS NULL OR IntField01 = @IntField01)

            AND (@IntField02 IS NULL OR IntField02 >= @IntField02)

            AND (@DecimalField01 IS NULL OR DecimalField01 = @DecimalField01)

            -- Function on column (NON-SARGable)
            AND (@DateField01 IS NULL OR CONVERT(DATE, DateField01) = @DateField01)

            -- ISNULL on column (NON-SARGable)
            AND ISNULL(StringField01, '') = ISNULL(@StringField01, '')

            -- Expression on column (NON-SARGable)
            AND (BitField01 + 0) = ISNULL(@BitField01, BitField01)

            -- More optional OR patterns for extra pain
            AND (@StringField02 IS NULL OR StringField02 LIKE '%' + @StringField02 + '%')

            AND (@DateField02 IS NULL OR DATEDIFF(DAY, DateField02, @DateField02) = 0);";
        con.Query<RecordSnapshot>(sql, record);
        return 0;
    }

    public void Init() {
        var con = _context.GetConnection();
        var sql = @"
        INSERT INTO dbo.RecordSnapshot
        (
            IntField01, IntField02, IntField03, IntField04, IntField05,
            IntField06, IntField07, IntField08, IntField09, IntField10,
            DecimalField01, DecimalField02, DecimalField03, DecimalField04, DecimalField05,
            DateField01, DateField02, DateField03, DateField04, DateField05,
            BitField01, BitField02, BitField03, BitField04, BitField05,
            StringField01, StringField02, StringField03, StringField04, StringField05,
            StringField06, StringField07, StringField08, StringField09, StringField10,
            CreatedAt, UpdatedAt
        )
        VALUES
        (
            @IntField01, @IntField02, @IntField03, @IntField04, @IntField05,
            @IntField06, @IntField07, @IntField08, @IntField09, @IntField10,
            @DecimalField01, @DecimalField02, @DecimalField03, @DecimalField04, @DecimalField05,
            @DateField01, @DateField02, @DateField03, @DateField04, @DateField05,
            @BitField01, @BitField02, @BitField03, @BitField04, @BitField05,
            @StringField01, @StringField02, @StringField03, @StringField04, @StringField05,
            @StringField06, @StringField07, @StringField08, @StringField09, @StringField10,
            @CreatedAt, @UpdatedAt
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";

        var snapshot = RecordSnapshotFactory.CreateRandom();

        var newId = con.QuerySingle<int>(sql, snapshot);

        snapshot.SnapshotId = newId;

    }
    
}

public static class RecordSnapshotFactory
{
    private static readonly Random _random = new Random();

    public static RecordSnapshot CreateRandom()
    {
        return new RecordSnapshot
        {
            IntField01 = RandomNullableInt(0, 1000),
            IntField02 = RandomNullableInt(0, 1000),
            IntField03 = RandomNullableInt(0, 1000),
            IntField04 = RandomNullableInt(0, 1000),
            IntField05 = RandomNullableInt(0, 1000),
            IntField06 = RandomNullableInt(0, 1000),
            IntField07 = RandomNullableInt(0, 1000),
            IntField08 = RandomNullableInt(0, 1000),
            IntField09 = RandomNullableInt(0, 1000),
            IntField10 = RandomNullableInt(0, 1000),

            DecimalField01 = RandomNullableDecimal(),
            DecimalField02 = RandomNullableDecimal(),
            DecimalField03 = RandomNullableDecimal(),
            DecimalField04 = RandomNullableDecimal(),
            DecimalField05 = RandomNullableDecimal(),

            DateField01 = RandomNullableDate(),
            DateField02 = RandomNullableDate(),
            DateField03 = RandomNullableDate(),
            DateField04 = RandomNullableDate(),
            DateField05 = RandomNullableDate(),

            BitField01 = RandomNullableBool(),
            BitField02 = RandomNullableBool(),
            BitField03 = RandomNullableBool(),
            BitField04 = RandomNullableBool(),
            BitField05 = RandomNullableBool(),

            StringField01 = RandomNullableString("SF01"),
            StringField02 = RandomNullableString("SF02"),
            StringField03 = RandomNullableString("SF03"),
            StringField04 = RandomNullableString("SF04"),
            StringField05 = RandomNullableString("SF05"),
            StringField06 = RandomNullableString("SF06"),
            StringField07 = RandomNullableString("SF07"),
            StringField08 = RandomNullableString("SF08"),
            StringField09 = RandomNullableString("SF09"),
            StringField10 = RandomNullableString("SF10"),

            CreatedAt = DateTime.UtcNow.AddMinutes(-_random.Next(0, 100000)),
            UpdatedAt = RandomNullableDate()
        };
    }

    // ---------- Helpers ----------

    private static int? RandomNullableInt(int min, int max)
        => Chance(0.7) ? _random.Next(min, max) : null;

    private static decimal? RandomNullableDecimal()
        => Chance(0.7) ? Math.Round((decimal)_random.NextDouble() * 1000, 2) : null;

    private static DateTime? RandomNullableDate()
        => Chance(0.7)
            ? DateTime.UtcNow.AddDays(-_random.Next(0, 365))
            : null;

    private static bool? RandomNullableBool()
        => Chance(0.7) ? _random.Next(0, 2) == 1 : null;

    private static string? RandomNullableString(string prefix)
        => Chance(0.7) ? $"{prefix}_{_random.Next(1, 5000)}" : null;

    private static bool Chance(double probability)
        => _random.NextDouble() < probability;
}
