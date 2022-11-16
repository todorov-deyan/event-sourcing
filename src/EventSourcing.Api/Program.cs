
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common;

using FluentValidation;
using FluentValidation.AspNetCore;
using Marten;
using Marten.Events.Projections;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Weasel.Core;

namespace EventSourcing.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Fluent Validation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //MediatR
            builder.Services.AddMediatR(typeof(Program));

            var connString = builder.Configuration.GetConnectionString(Constants.DbConnection);

            builder.Services.AddDbContext<CustomEsDbContext>(cfg => cfg.UseNpgsql(connString));

            //MartenDB
            builder.Services.AddMarten(opt =>
            {
                opt.UseDefaultSerialization(EnumStorage.AsString, nonPublicMembersStorage: NonPublicMembersStorage.All);

                opt.Connection(connString);

                opt.DatabaseSchemaName = "martendb_event_sourcing";

                opt.Projections.SelfAggregate<Account>(ProjectionLifecycle.Live);
            });

            //Repositories
            builder.Services.AddScoped<IMartenRepository<Account>, MartenRepository<Account>>();
            builder.Services.AddScoped<ICustomEsRepository<Account>, CustomEsRepository<Account>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}