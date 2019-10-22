/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-07.html
*   
*********/
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ConfigurationDemo2
{
    public enum Gender
    {
        Male,
        Female
    }

    public class ContactInfo
    {
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
    }

    public class Profile
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }

    [Table("ApplicationSettings")]
    public class ApplicationSetting
    {
        private string key;

        [Key]
        public string Key
        {
            get { return key; }
            set { key = value.ToLowerInvariant(); }
        }

        [Required]
        [MaxLength(512)]
        public string Value { get; set; }

        public ApplicationSetting()
        { }

        public ApplicationSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class ApplicationSettingsContext : DbContext
    {
        public ApplicationSettingsContext(DbContextOptions options) : base(options)
        { }

        public DbSet<ApplicationSetting> Settings { get; set; }
    }

    public class DbConfigurationProvider : ConfigurationProvider
    {
        private Action<DbContextOptionsBuilder> _setup;
        private IDictionary<string, string> _initialSettings;

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> setup,
            IDictionary<string, string> initialSettings = null)
        {
            _setup = setup;
            _initialSettings = initialSettings;
        }

        public override void Load()
        {
            DbContextOptionsBuilder<ApplicationSettingsContext> builder =
                new DbContextOptionsBuilder<ApplicationSettingsContext>();
            _setup(builder);
            using (ApplicationSettingsContext dbContext = new ApplicationSettingsContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                this.Data = dbContext.Settings.AnyAsync().Result ?
                    dbContext.Settings.ToDictionaryAsync(it => it.Key, it => it.Value,
                    StringComparer.OrdinalIgnoreCase).Result
                    : this.Initialize(dbContext);
            }
        }

        private IDictionary<string, string> Initialize(ApplicationSettingsContext dbContext)
        {
            foreach (var item in _initialSettings)
            {
                dbContext.Settings.Add(new ApplicationSetting(item.Key, item.Value));
                //dbContext.SaveChanges();
            }

            return _initialSettings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase);
        }
    }

    public class DbConfigurationSource : IConfigurationSource
    {
        private Action<DbContextOptionsBuilder> _setup;
        private IDictionary<string, string> _initialSettings;

        public DbConfigurationSource(Action<DbContextOptionsBuilder> setup,
            IDictionary<string, string> initialSettings = null)
        {
            _setup = setup;
            _initialSettings = initialSettings;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(_setup, _initialSettings);
        }
    }

    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDatabase(this IConfigurationBuilder builder, string connectionStringName,
            IDictionary<string, string> initialSettings = null)
        {
            string connectionString = builder.Build().GetConnectionString(connectionStringName);
            DbConfigurationSource source = new DbConfigurationSource(
                optionsBuilder => optionsBuilder.UseSqlServer(connectionString), initialSettings);
            builder.Add(source);
            return builder;
        }
    }

    class DbConfigurationDemo
    {
        public void Execute()
        {
            var initialSettings = new Dictionary<string, string>
            {
                ["Gender"] = "Male",
                ["Age"] = "18",
                ["ContactInfo:EmailAddress"] = "foo@outlook.com",
                ["ContactInfo:PhoneNo"] = "123456789"
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("connectionString.json")
                .AddDatabase("DefaultDb", initialSettings)
                .Build();


            Profile profile = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(config)
                .BuildServiceProvider()
                .GetService<IOptions<Profile>>()
                .Value;

            Console.WriteLine(profile.Gender);
            Console.WriteLine(profile.Age);
            Console.WriteLine(profile.ContactInfo.EmailAddress);
            Console.WriteLine(profile.ContactInfo.PhoneNo);
        }
    }
}
